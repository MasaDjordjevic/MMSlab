using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Views
{
    public class YCbCrStrategy : splitViewStrategy
    {
        public override Bitmap[] generateImages(Bitmap b)
        {
            Bitmap[] channels = new Bitmap[3];
            for (int i = 0; i < 3; i++)
            {
                channels[i] = (Bitmap)b.Clone();
            }
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            BitmapData[] channelsBmData = new BitmapData[3];
            System.IntPtr[] cScan0 = new IntPtr[3];
            for (int i = 0; i < 3; i++)
            {
                channelsBmData[i] = channels[i].LockBits(new Rectangle(0, 0, channels[i].Width, channels[i].Height), ImageLockMode.ReadWrite, channels[i].PixelFormat);
                cScan0[i] = channelsBmData[i].Scan0;
            }

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* Y = (byte*)(void*)cScan0[0];
                byte* Cb = (byte*)(void*)cScan0[1];
                byte* Cr = (byte*)(void*)cScan0[2];


                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        YCbCr ycbcr = ColorModels.RGBtoYCbCr(new RGB(red, green, blue));

                        //Y
                        Y[2] = ycbcr.Y;
                        Y[1] = ycbcr.Y;
                        Y[0] = ycbcr.Y;

                        //Cb
                        RGB cbRGB = ColorModels.YCbCrToRGB(new YCbCr(ycbcr.Y, ycbcr.Cb, ycbcr.Y));
                        Cb[2] = cbRGB.R;
                        Cb[1] = cbRGB.G;
                        Cb[0] = (byte)(cbRGB.B + ycbcr.Cb);

                        //Cr
                        RGB crRGB = ColorModels.YCbCrToRGB(new YCbCr(ycbcr.Y, ycbcr.Y, ycbcr.Cr));
                        Cr[2] = (byte)(crRGB.R + ycbcr.Cr);
                        Cr[1] = 0;
                        Cr[0] = 0;



                        p += 3;
                        Y += 3;
                        Cb += 3;
                        Cr += 3;
                    }
                    p += nOffset;
                    Y += nOffset;
                    Cb += nOffset;
                    Cr += nOffset;
                }
            }

            b.UnlockBits(bmData);
            for (int i = 0; i < 3; i++)
            {
                channels[i].UnlockBits(channelsBmData[i]);
            }

            return channels;
        }        
    }
}
