using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Filters
{
    public class CoreFilters : IFilter
    {
        private Views.CommonControls commonControls { get; set; }

        public CoreFilters(Views.CommonControls commonControls)
        {
            this.commonControls = commonControls;
        }
        public bool GaussianBlurInplace(Bitmap b, FilterOptions opt)
        {
            return this.GaussianBlurAlg(b, opt.Weight, opt.Dimension, true);
        }

        public bool GaussianBlur(Bitmap b, FilterOptions opt)
        {
            return this.GaussianBlurAlg(b, opt.Weight, opt.Dimension, false);
        }

        public bool GaussianBlurAlg(Bitmap b, int nWeight = 4, int dimension = 3, bool inplace = false)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.e = nWeight;
            m.b = m.d = m.f = m.h = 2;
            m.Factor = nWeight + 12;

            ConvFilters conv = new ConvFilters(this.commonControls);
            return conv.Conv(b, m, dimension, inplace);
        }

        public bool Brightness(Bitmap b, FilterOptions opt)
        {
            return this.BrightnessAlg(b, opt.Weight);
        }

        public bool BrightnessAlg(Bitmap b, int nBrightness)
        {
            if (nBrightness < -255 || nBrightness > 255)
                return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int nVal = 0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;


                commonControls.progress = 0;
                double step = 100.0 / b.Height;
                double progress = 0;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(p[0] + nBrightness);

                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;

                        ++p;
                    }
                    p += nOffset;

                    progress += step;
                    commonControls.progress = (int)(progress);
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public bool Contrast(Bitmap b, FilterOptions opt)
        {
            return this.ContrastAlg(b, opt.Weight);
        }

        public bool ContrastAlg(Bitmap b, int nContrast)
        {
            if (nContrast < -100) return false;
            if (nContrast > 100) return false;

            double pixel = 0, contrast = (100.0 + nContrast) / 100.0;

            contrast *= contrast;

            int red, green, blue;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            commonControls.progress = 0;
            double step = 100.0 / b.Height;
            double progress = 0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        //red
                        pixel = ((((p[2] / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte)pixel;

                        //green
                        pixel = ((((p[1] / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte)pixel;

                        //blue
                        pixel = ((((p[0] / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                    progress += step;
                    commonControls.progress = (int)(progress);

                }
            }

            b.UnlockBits(bmData);

            return true;
        }


        public bool EdgeDetectHorizontal(Bitmap b, FilterOptions opt)
        {
            return this.EdgeDetectHorizontalAlg(b);
        }

        public bool EdgeDetectHorizontalAlg(Bitmap b)
        {
            Bitmap bmTemp = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = bmTemp.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;


            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                int nPixel = 0;

                p += stride;
                p2 += stride;

                commonControls.progress = 0;
                double step = 100.0 / b.Height;
                double progress = 0;
                for (int y = 1; y < b.Height - 1; ++y)
                {
                    p += 9;
                    p2 += 9;

                    for (int x = 9; x < nWidth - 9; ++x)
                    {
                        nPixel = ((p2 + stride - 9)[0] +
                            (p2 + stride - 6)[0] +
                            (p2 + stride - 3)[0] +
                            (p2 + stride)[0] +
                            (p2 + stride + 3)[0] +
                            (p2 + stride + 6)[0] +
                            (p2 + stride + 9)[0] -
                            (p2 - stride - 9)[0] -
                            (p2 - stride - 6)[0] -
                            (p2 - stride - 3)[0] -
                            (p2 - stride)[0] -
                            (p2 - stride + 3)[0] -
                            (p2 - stride + 6)[0] -
                            (p2 - stride + 9)[0]);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        (p + stride)[0] = (byte)nPixel;

                        ++p;
                        ++p2;
                    }

                    p += 9 + nOffset;
                    p2 += 9 + nOffset;

                    progress += step;
                    commonControls.progress = (int)(progress);
                }
            }

            b.UnlockBits(bmData);
            bmTemp.UnlockBits(bmData2);

            return true;
        }

        public bool Water(Bitmap b, FilterOptions opt)
        {
            return this.WaterAlg(b, opt.Weight);
        }

        public bool WaterAlg(Bitmap b, int nWave = 15)
        {
            int nWidth = b.Width;
            int nHeight = b.Height;

            Point[,] pt = new Point[nWidth, nHeight];


            double newX, newY;

            commonControls.progress = 0;
            double step = 100.0 / nWidth;
            double progress = 0;
            for (int x = 0; x < nWidth; ++x)
            {
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = x + ((double)nWave * Math.Sin(2.0 * Math.PI * (float)y / 128.0));
                    newY = y + ((double)nWave * Math.Cos(2.0 * Math.PI * (float)x / 128.0));

                    pt[x, y].X = (newX > 0 && newX < nWidth) ? (int)newX : 0;
                    pt[x, y].Y = (newY > 0 && newY < nHeight) ? (int)newY : 0;
                }
                progress += step;
                commonControls.progress = (int)(progress);
            }

            OffsetFilterAbs(b, pt);

            return true;
        }

        public static bool OffsetFilterAbs(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;
                int nWidth = b.Width;
                int nHeight = b.Height;

                int xOffset, yOffset;


                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = offset[x, y].X;
                        yOffset = offset[x, y].Y;

                        if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                        {
                            p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                            p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                            p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }



        public bool ShiftAndScale(Bitmap b, FilterOptions opt)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int nVal = 0;
            ShiftAndScaleOptions o = opt.ShiftAndScaleOptions;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;
                int yy, cb, cr;


                commonControls.progress = 0;
                double step = 100.0 / b.Height;
                double progress = 0;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        YCbCr ycbcr = ColorModels.RGBtoYCbCr(new RGB(p[2], p[1], p[0]));
                        yy = (byte)((ycbcr.Y + o.YShift) * o.YScale);
                        cb = (byte)((ycbcr.Cb + o.CbShift) * o.CbScale);
                        cr = (byte)((ycbcr.Cr + o.CrShift) * o.CrScale);
                        ycbcr.Y = (byte)Math.Max(Math.Min(255, yy), 0);
                        ycbcr.Cb = (byte)Math.Max(Math.Min(255, cb), 0);
                        ycbcr.Cr = (byte)Math.Max(Math.Min(255, cr), 0);

                        RGB rgb = ColorModels.YCbCrToRGB(ycbcr);
                        p[0] = rgb.B;
                        p[1] = rgb.G;
                        p[2] = rgb.R;


                        p += 3;
                    }
                    p += nOffset;

                    progress += step;
                    commonControls.progress = (int)(progress);
                }
            }

            b.UnlockBits(bmData);

            return true;
        }


        public static void ToGrayscale(Bitmap b)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
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
                        byte val = (byte)(p[0] * 0.11 + p[1] * 0.59 + p[2] * 0.3);
                        p[0] = val;
                        p[1] = val;
                        p[2] = val;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return;
        }
    }
}
