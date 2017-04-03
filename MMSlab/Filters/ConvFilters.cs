using MMSlab.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab
{
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        //public int dim;
        //public int[,] coefArray;
        public int Factor = 1;
        public int Offset = 0;

        //public ConvMatrix(int dim)
        //{
        //    this.dim = dim;
        //    this.coefArray = new int[dim, dim];
        //}

        //public void SetAll(int nVal)
        //{            
        //    for (int i = 0; i < dim; i++)
        //    {
        //        for (int j = 0; j < dim; j++)
        //        {
        //            coefArray[i, j] = nVal;
        //        }
        //    }
        //}

        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }

    }


    public static class ConvFilters
    {
        public static bool Conv3x3Safe(Bitmap b, ConvMatrix m, bool inplace = false)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bDest = (Bitmap)b.Clone();

            int nWidth = b.Width - 2;
            int nHeight = b.Height - 2;

            for (int y = 0; y < nHeight; ++y)
            {
                for (int x = 0; x < nWidth; ++x)
                {
                    Color tl = b.GetPixel(x, y);
                    Color tm = b.GetPixel(x + 1, y);
                    Color tr = b.GetPixel(x + 2, y);
                    Color ml = b.GetPixel(x, y + 1);
                    Color mm = b.GetPixel(x + 1, y + 1);
                    Color mr = b.GetPixel(x + 2, y + 1);
                    Color bl = b.GetPixel(x, y + 2);
                    Color bm = b.GetPixel(x + 1, y + 2);
                    Color br = b.GetPixel(x + 2, y + 2);


                    //red
                    int red = (int)((((tl.R * m.TopLeft) + (tm.R * m.TopMid) + (tr.R * m.TopRight) +
                        (ml.R * m.MidLeft) + (mm.R * m.Pixel) + (mr.R * m.MidRight) +
                        (bl.R * m.BottomLeft) + (bm.R * m.BottomMid) + (br.R * m.BottomRight)) / m.Factor) + m.Offset);

                    //green
                    int green = (int)((((tl.G * m.TopLeft) + (tm.G * m.TopMid) + (tr.G * m.TopRight) +
                        (ml.G * m.MidLeft) + (mm.G * m.Pixel) + (mr.G * m.MidRight) +
                        (bl.G * m.BottomLeft) + (bm.G * m.BottomMid) + (br.G * m.BottomRight)) / m.Factor) + m.Offset);

                    //blue
                    int blue = (int)((((tl.B * m.TopLeft) + (tm.B * m.TopMid) + (tr.B * m.TopRight) +
                          (ml.B * m.MidLeft) + (mm.B * m.Pixel) + (mr.B * m.MidRight) +
                          (bl.B * m.BottomLeft) + (bm.B * m.BottomMid) + (br.B * m.BottomRight)) / m.Factor) + m.Offset);

                    Color newC = SimpleFilters.newColor(red, green, blue);

                    if (inplace)
                    {
                        b.SetPixel(x, y, newC);
                    }
                    else
                    {
                        bDest.SetPixel(x, y, newC);
                    }
                }

            }

            return true;
        }

        public static bool Conv3x3(Bitmap b, ConvMatrix m, bool inplace = false)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = inplace ? (byte*)(void*)Scan0 : (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
    }
}
