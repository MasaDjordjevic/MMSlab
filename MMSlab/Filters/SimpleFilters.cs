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

        private static byte fixByte(int x)
        {
            return (byte)Math.Max(0, Math.Min(255, x));
        }

        public static Color newColor(int r, int g, int b)
        {
            return Color.FromArgb(fixByte(r), fixByte(g), fixByte(b));
        }

        public bool GaussianBlurArg(Bitmap b, int nWeight = 4, bool inplace = false)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;
            m.matrix[0, 1] = m.matrix[1, 0] = m.matrix[1, 2] = m.matrix[2, 1] = 2;
            m.matrix[1, 1] = nWeight;

            return ConvFilters.Conv3x3NewSafe(b, m, inplace);
        }

        public bool GaussianBlurNew(Bitmap b, FilterOptions opt)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = opt.Weight;
            m.matrix[0, 1] = m.matrix[1, 0] = m.matrix[1, 2] = m.matrix[2, 1] = 2;
            //m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = opt.Weight + 12;

            return ConvFilters.Conv3x3NewSafe(b, m, false);
        }

        public bool GaussianBlur(Bitmap b, FilterOptions opt)
        {
            return this.GaussianBlurArg(b, opt.Weight, false);
        }

        public bool GaussianBlurInplace(Bitmap b, FilterOptions opt)
        {
            return this.GaussianBlurArg(b, opt.Weight, true);
        }

        public bool Brightness(Bitmap b, FilterOptions opt)
        {
            return this.BrightnessAlg(b, opt.Weight);
        }
        public bool BrightnessAlg(Bitmap b, int nBrightness)
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


        public bool Contrast(Bitmap b, FilterOptions opt)
        {
            return this.ContrastAlg(b, opt.Weight);
        }

        public bool ContrastAlg(Bitmap b, int nContrast)
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
                    red = (int)(((((c.R / 255.0) - 0.5) * contrast) + 0.5) * 255.0);
                    green = (int)(((((c.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0);
                    blue = (int)(((((c.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0);

                    b.SetPixel(x, y, newColor(red, green, blue));
                }
                progress += step;
                commonControls.progress = (int)(progress);
            }

            return true;
        }

        public bool EdgeDetectHorizontalAlg(Bitmap b)
        {
            Bitmap bmTemp = (Bitmap)b.Clone();


            for (int y = 1; y < b.Height - 1; ++y)
            {
                for (int x = 3; x < b.Width - 3; ++x)
                {
                    //Red
                    RGBInt pixel = (RGBInt)bmTemp.GetPixel(x - 3, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x - 2, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x - 1, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x + 1, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x + 2, y + 1)
                         + (RGBInt)bmTemp.GetPixel(x + 3, y + 1)
                         - (RGBInt)bmTemp.GetPixel(x - 3, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x - 2, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x - 1, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x + 1, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x + 2, y - 1)
                         - (RGBInt)bmTemp.GetPixel(x + 3, y - 1);

                    RGB fixedPixel = pixel.ConvertToRGB();

                

                    b.SetPixel(x, y + 1, Color.FromArgb(fixedPixel.R, fixedPixel.G, fixedPixel.B));
                }
            }


            return true;
        }

        public bool EdgeDetectHorizontal(Bitmap b, FilterOptions opt)
        {
            return this.EdgeDetectHorizontalAlg(b);
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

            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = x + ((double)nWave * Math.Sin(2.0 * Math.PI * (float)y / 128.0));
                    newY = y + ((double)nWave * Math.Cos(2.0 * Math.PI * (float)x / 128.0));

                    pt[x, y].X = (newX > 0 && newX < nWidth) ? (int)newX : 0;
                    pt[x, y].Y = (newY > 0 && newY < nHeight) ? (int)newY : 0;
                }

            OffsetFilterAbs(b, pt);

            return true;
        }

        public static bool OffsetFilterAbs(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            int xOffset, yOffset;


            for (int y = 0; y < b.Height; ++y)
            {
                for (int x = 0; x < b.Width; ++x)
                {
                    xOffset = offset[x, y].X;
                    yOffset = offset[x, y].Y;

                    if (yOffset >= 0 && yOffset < b.Height && xOffset >= 0 && xOffset < b.Width)
                    {
                        b.SetPixel(x, y, bSrc.GetPixel(xOffset, yOffset));
                    }
                    
                }
            }

            return true;
        }

    }
}
