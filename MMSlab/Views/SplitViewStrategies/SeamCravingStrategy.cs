using MMSlab.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab.Views.SplitViewStrategies
{
    public class SeamCravingStrategy : splitViewStrategy
    {
        private YcbCrView view;
        private CommonControls commonControls;
        public int repetition = 40;
        public short alg = 0;
        private Thread workerThread = null;
        private Form form;


        public SeamCravingStrategy(YcbCrView view, CommonControls commonControls, Form form)
        {
            this.view = view;
            this.commonControls = commonControls;
            this.form = form;
        }
        public void Redo()
        {           
            this.RedoChannels();
        }

        private void RedoChannelsWorker()
        {
            ConvFilters conv = new ConvFilters(null);

            this.commonControls.progress = 0;
            double step = 100.0 / repetition;
            double progress = 0;
            for (int i = 0; i < repetition; i++)
            {
                Bitmap c2 = (Bitmap)this.view.channels[0].Clone();
                CoreFilters.ToGrayscale(c2);

                conv.EdgeDetectConvolution(c2, this.alg);
                this.view.channels[2] = c2;
                this.view.channels[0] = seams(this.view.channels[2], this.view.channels[0], i % 2);


                progress += step;
                commonControls.progress = (int)(progress);
            }
            this.repetition = 10;
            this.form.Enabled = true;
            this.view.Invalidate();
        }

        private void RedoChannels()
        {
            this.form.Enabled = false;
            this.workerThread = new Thread(new ThreadStart(this.RedoChannelsWorker));
            this.workerThread.Start();
        }
      


        public override Bitmap[] generateImages(Bitmap b)
        {
            Bitmap g = (Bitmap)b.Clone();
            CoreFilters.ToGrayscale(g);
            for (int i = 0; i < 3; i++)
            {
                this.view.channels[i] = (Bitmap)g.Clone();
            }


            ConvFilters conv = new ConvFilters(null);

            conv.EdgeDetectConvolution(this.view.channels[1], 0);
            this.view.channels[0] = (Bitmap)b.Clone();
           // this.view.channels[2] = (Bitmap)this.view.channels[1].Clone();

            this.RedoChannels();

            return this.view.channels;
        }

        public Bitmap seams(Bitmap b, Bitmap original, int direction = 0)
        {
            Bitmap ext = ConvFilters.ExtendBitmap((Bitmap)b.Clone(), 1, Color.White);

            BitmapData bmData = ext.LockBits(new Rectangle(0, 0, ext.Width, ext.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);


            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            long[,] seams = new long[b.Width, b.Height];
            int[,] path = new int[b.Width, b.Height];
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - ext.Width * 3;

                p += stride;
                p += 3;

                for (int x = 0; x < b.Width; ++x)
                {
                    seams[x, 0] = p[0];
                    p += 3;
                }

                p += 3;
                p += nOffset;

                p = (byte*)(void*)Scan0;
                p += stride * 2;

                for (int y = 1; y < b.Height; y++)
                {
                    p += 3;
                    for (int x = 0; x < b.Width; x++)
                    {
                        int val = p[0];
                        int up = p[-stride];
                        int uL = p[-stride - 3];
                        int uR = p[-stride + 3];
                        int min = Math.Min(Math.Min(up, uL), uR);

                        if (min == up)
                        {
                            path[x, y] = 0;
                        }
                        else if (min == uL)
                        {
                            path[x, y] = -1;
                        }
                        else if (min == uR)
                        {
                            path[x, y] = +1;
                        }

                        seams[x, y] = min + val;

                        p += 3;
                    }
                    p += 3;
                    p += nOffset;
                }
            }
            ext.UnlockBits(bmData);


            int h = b.Height - 1;
            long minVal = Int32.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < b.Width; i++)
            {
                int iDirection = direction == 0 ? i : b.Width - i - 1;
                if (seams[iDirection, h] < minVal)
                {
                    minVal = seams[iDirection, h];
                    minIndex = iDirection;
                }
            }

            int[] pom = new int[b.Height];
            int k = 0;
            for (int i = h; i >= 0; i--)
            {
                pom[k++] = path[minIndex, i];
                minIndex += path[minIndex, i];

            }

            int[] removed = new int[b.Height];
            for (int i = h; i >= 0; i--)
            {
                removed[i] = minIndex;
                b.SetPixel(minIndex, i, Color.Red);
                minIndex += path[minIndex, i];
            }

            original = removePixels(original, removed);
            return original;
        }

        public Bitmap removePixels(Bitmap b, int[] rem)
        {
            Bitmap retVal = new Bitmap(b.Width - 1, b.Height);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmRet = retVal.LockBits(new Rectangle(0, 0, retVal.Width, retVal.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);


            IntPtr srcPtr = bmData.Scan0;
            IntPtr destPtr = bmRet.Scan0;
            byte[] buffer = new byte[bmData.Stride];
            for (int i = 0; i < bmData.Height; ++i)
            {
                Marshal.Copy(srcPtr, buffer, 0, buffer.Length);
                byte[] pom = new byte[bmRet.Stride];
                int rem3 = rem[i] * 3;
                Buffer.BlockCopy(buffer, 0, pom, 0, rem3);
                Buffer.BlockCopy(buffer, rem3 + 3, pom, rem3, b.Width * 3 - rem3 - 3);

                Marshal.Copy(pom, 0, destPtr, pom.Length);


                srcPtr += bmData.Stride;
                destPtr += bmRet.Stride;

            }

            b.UnlockBits(bmData);
            retVal.UnlockBits(bmRet);

            return retVal;
        }

    }
}
