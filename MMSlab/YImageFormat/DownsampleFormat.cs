using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.YImageFormat
{
    public class DownsampleFormat
    {
        public Bitmap Bitmap { get; private set; }
        public byte[,] Y { get; private set; }
        public byte[,] Cb { get; private set; }
        public byte[,] Cr { get; private set; }

        public DownsampleFormat(Bitmap b)
        {
            this.Bitmap = (Bitmap)b.Clone();
            this.SetChannels();
        }

        public DownsampleFormat()
        {
        }

        private void SetChannels()
        {
            Bitmap b = this.Bitmap;
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            this.Y = new byte[b.Height, b.Width];
            this.Cb = new byte[b.Height, b.Width];
            this.Cr = new byte[b.Height, b.Width];

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        //Y[y, x] = p[2];
                        //Cb[y, x] = p[1];
                        //Cr[y, x] = p[0];

                        YCbCr ycbcr = ColorModels.RGBtoYCbCr(new RGB(red, green, blue));
                        Y[y, x] = ycbcr.Y;
                        Cb[y, x] = ycbcr.Cb;
                        Cr[y, x] = ycbcr.Cr;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
        }

        public byte[] DownsampleToByteArray(string channel)
        {
            byte[,] Y = this.Y;
            byte[,] Cb = this.Cb;
            byte[,] Cr = this.Cr;

            if (channel == "Cb" || channel == "Cr")
            {
                Y = Downsample(this.Y);
            }

            if (channel == "Cr" || channel == "Y")
            {
                Cb = Downsample(this.Cb);
            }

            if (channel == "Cb" || channel == "Y")
            {
                Cr = Downsample(this.Cr);
            }

            byte[] ret = new byte[Y.Length + Cb.Length + Cr.Length];

            Buffer.BlockCopy(Y, 0, ret, 0, Y.Length);
            Buffer.BlockCopy(Cb, 0, ret, Y.Length, Cb.Length);
            Buffer.BlockCopy(Cr, 0, ret, Y.Length + Cb.Length, Cr.Length);

            return ret;
        }

        public Bitmap GenerateFromByteArray(byte[] data, string channel, int width, int height)
        {
            int regularLenght = width * height;
            int downsempledLenght = GetDownsampledSize(width, height);
            int Ylen, Cblen, Crlen, YWidth, CbWidth, CrWidth;
            Ylen = Cblen = Crlen = downsempledLenght;
            YWidth = CbWidth = CrWidth = GetDownsampledWidth(width);
            if (channel == "Y")
            {
                Ylen = regularLenght;
                YWidth = width;
            }
            if (channel == "Cb")
            {
                Cblen = regularLenght;
                CbWidth = width;
            }
            if (channel == "Cr")
            {
                Crlen = regularLenght;
                CrWidth = width;
            }


            byte[] y, cb, cr;
            y = new byte[regularLenght];
            cb = new byte[regularLenght];
            cr = new byte[regularLenght];
            Array.Copy(data, 0, y, 0, Ylen);
            Array.Copy(data, Ylen, cb, 0, Cblen);
            Array.Copy(data, Ylen + Cblen, cr, 0, Crlen);

            this.Y = Pom(y, YWidth, height);
            this.Cb = Pom(cb, CbWidth, height);
            this.Cr = Pom(cr, CrWidth, height);



            if (channel == "Cb" || channel == "Cr")
            {
                this.Y = Restore(this.Y, width, height);
            }

            if (channel == "Cr" || channel == "Y")
            {
                this.Cb = Restore(this.Cb, width, height);
            }

            if (channel == "Cb" || channel == "Y")
            {
                this.Cr = Restore(this.Cr, width, height);
            }

            this.Bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            this.SetBitampFromChannels();

            return this.Bitmap;
        }

        public static byte[,] Pom(byte[] arr, int width, int height)
        {
            byte[,] ret = new byte[height, width];
            int w = width * sizeof(byte);
            for (int i = 0; i < height; i++)
            {
                Buffer.BlockCopy(arr, i * w, ret, i * w, w);
            }
            return ret;
        }

        public Bitmap SetBitampFromChannels()
        {
            Bitmap b = this.Bitmap;
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        RGB rgb = ColorModels.YCbCrToRGB(new YCbCr(Y[y, x], Cb[y, x], Cr[y, x]));
                        p[2] = rgb.R;
                        p[1] = rgb.G;
                        p[0] = rgb.B;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return b;
        }

        public Bitmap DownsampleImage(string channel)
        {

            byte[,] Y = this.Y;
            byte[,] Cb = this.Cb;
            byte[,] Cr = this.Cr;

            if (channel == "Cb" || channel == "Cr")
            {
                byte[,] Ys = Downsample(this.Y);
                Y = Restore(Ys, this.Bitmap.Width, this.Bitmap.Height);
            }

            if (channel == "Cr" || channel == "Y")
            {
                byte[,] Cbs = Downsample(this.Cb);
                Cb = Restore(Cbs, this.Bitmap.Width, this.Bitmap.Height);
            }

            if (channel == "Cb" || channel == "Y")
            {
                byte[,] Crs = Downsample(this.Cr);
                Cr = Restore(Crs, this.Bitmap.Width, this.Bitmap.Height);
            }

            Bitmap b = (Bitmap)this.Bitmap.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        RGB rgb = ColorModels.YCbCrToRGB(new YCbCr(Y[y, x], Cb[y, x], Cr[y, x]));
                        p[2] = rgb.R;
                        p[1] = rgb.G;
                        p[0] = rgb.B;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return b;
        }

        public static int GetDownsampledSize(int width, int height)
        {
            int h2 = height;
            int w2 = GetDownsampledWidth(width);
            return w2 * h2;
        }

        private static int GetDownsampledWidth(int width)
        {
            return ((width + 1) / 4) * 2;
        }

        public static byte[,] Downsample(byte[,] bytes)
        {
            int height = bytes.GetLength(0);
            int width = bytes.GetLength(1);
            int h2 = height;
            int w2 = GetDownsampledWidth(width);
            byte[,] ret = new byte[h2, w2];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < w2 * 2; j++)
                {
                    if ((i / 2 + j / 2) % 2 == 0)
                    {
                        ret[i, j / 4 * 2 + j % 2] = bytes[i, j];
                    }
                }
            return ret;
        }

        public static byte[,] Restore(byte[,] bytes, int width, int height)
        {
            byte[,] ret = new byte[height, width];
            int bH = bytes.GetLength(0);
            int bW = bytes.GetLength(1);
            for (int i = 0; i < bH; i++)
                for (int j = 0; j < bW; j++)
                {
                    int x = j / 2 * 4 + j % 2;
                    ret[i, x] = bytes[i, j];
                    if (x + 2 < width)
                    {
                        ret[i, x + 2] = bytes[i, j];
                    }
                }

            return ret;
        }





    }
}
