using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MMSlab.Views
{
    public class YCbCrHistogramStrategy : splitViewStrategy
    {
        public YCbCrHistogramStrategy(bool imageType = true) : base(imageType)
        {
            
        }

        public override int[][] generateHistogramStatistics(Bitmap b)
        {
            DataPoint[,] channels = new DataPoint[3, 256];
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int[][] data = new int[3][];
            for(int i = 0; i< 3; i++)
            {
                data[i] = new int[256];
            }

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

                        YCbCr ycbcr = ColorModels.RGBtoYCbCr(new RGB(red, green, blue));
                        data[0][ycbcr.Y]++;
                        data[1][ycbcr.Cb]++;
                        data[2][ycbcr.Cr]++;


                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return data;
        }
    }
}
