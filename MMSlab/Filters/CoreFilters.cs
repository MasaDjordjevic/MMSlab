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
        private Views.CommonControls commonControls;

        public CoreFilters(Views.CommonControls commonControls)
        {
            this.commonControls = commonControls;
        }
        public bool GaussianBlurInplace(Bitmap b, int nWeight = 4)
        {
            return this.GaussianBlurAlg(b, nWeight, true);
        }

        public bool GaussianBlur(Bitmap b, int nWeight = 4)
        {
            return this.GaussianBlurAlg(b, nWeight, false);
        }

        public bool GaussianBlurAlg(Bitmap b, int nWeight = 4, bool inplace = false)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;

            return ConvFilters.Conv3x3(b, m, inplace);
        }
        public bool Brightness(Bitmap b, int nBrightness)
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
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public bool Contrast(Bitmap b, int nContrast)
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
                    commonControls.progress = (int) (progress);

                }
            }

            b.UnlockBits(bmData);

            return true;
        }

    }
}
