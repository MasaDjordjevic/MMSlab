using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Filters
{
    public class SimpleFilters : IFilter
    {
        private Views.CommonControls commonControls;

        public SimpleFilters(Views.CommonControls commonControls)
        {
            this.commonControls = commonControls;
        }

        private byte fixByte(int x)
        {
            return (byte)Math.Max(0, Math.Min(255, x));
        }

        private Color newColor(int r, int g, int b)
        {
            return Color.FromArgb(fixByte(r), fixByte(g), fixByte(b));
        }

        public bool GaussianBlur(Bitmap b, int nWeight = 4)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;
            return true;
            //return ConvFilters.Conv3x3(b, m);
        }

        public bool GaussianBlurInplace(Bitmap b, int nWeight = 4)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;
            return true;
            //return ConvFilters.Conv3x3(b, m);
        }
        public bool Brightness(Bitmap b, int nBrightness)
        {
            if (nBrightness < -255 || nBrightness > 255)
                return false;

            commonControls.progress = 0;
            double step = 100.0 / b.Height;
            double progress = 0;

            for (int y = 0; y < b.Height; ++y)
            {
                for (int x = 0; x < b.Width; ++x)
                {
                    Color c = b.GetPixel(x, y);
                    b.SetPixel(x, y, newColor(c.R + nBrightness, c.G + nBrightness, c.B + nBrightness));                    
                }
                progress += step;
                commonControls.progress = (int)(progress);
            }
           

            return true;
        }

        public bool Contrast(Bitmap b, int nContrast)
        {
            if (nContrast < -100) return false;
            if (nContrast > 100) return false;

            double contrast = (100.0 + nContrast) / 100.0;
            contrast *= contrast;


            commonControls.progress = 0;
            double step = 100.0 / b.Height;
            double progress = 0;
            for (int y = 0; y < b.Height; ++y)
            {
                for (int x = 0; x < b.Width; ++x)
                {
                    Color c = b.GetPixel(x, y);
                    int red, green, blue;                    
                    red = (int)(((((c.R/ 255.0) - 0.5) * contrast) +0.5) *255.0);
                    green = (int)(((((c.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0);
                    blue = (int)(((((c.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0);

                    b.SetPixel(x, y, newColor(red, green, blue));
                }
                progress += step;
                commonControls.progress = (int)(progress);
            }

            return true;
        }

    }
}
