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
        public int a = 0, b = 0, c = 0;
        public int d = 0, e = 1, f = 0;
        public int g = 0, h = 0, i = 0;
        public int Factor = 1;
        public int Offset = 0;

        public void SetAll(int nVal)
        {
            a = b = c = d = e = f = g = h = i = nVal;
        }

        public int[,] GetMatix(int dim)
        {
            if (dim == 3)
            {
                return new int[3, 3]
                {
                    {a, b, c},
                    {d, e, f},
                    {g, h, i}
                };
            }


            if (dim == 5)
            {
                return new int[5, 5]
                {
                   {a, a, b, b, c},
                   {d, a, b, c, c},
                   {d, d, e, f, f},
                   {g, g, h, i, f},
                   {g, h, h, i, i}
                };
            }

            if (dim == 7)
            {
                return new int[7, 7]
                {
                   {a, a, 0, b, b, 0, c},
                   {0, a, a, b, b, c, c},
                   {d, d, a, b, c, c, 0},
                   {d, d, d, e, f, f, f},
                   {0, g, g, h, i, f, f},
                   {g, g, h, h, i, i, 0},
                   {g, 0, h, h, 0, i, i}
                };
            }

            return null;

        }
    }


    public static class ConvFilters
    {
        public static bool ConvSafe(Bitmap b, ConvMatrix m, int dimension, bool inplace = false)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            int nWidth = b.Width - 2;
            int nHeight = b.Height - 2;
            int[,] matrix = m.GetMatix(dimension);
            if (matrix == null) return false;

            for (int y = 0; y < nHeight; ++y)
            {
                for (int x = 0; x < nWidth; ++x)
                {

                    RGBInt pixel = new RGBInt(0, 0, 0);
                    for (int i = 0; i < dimension; i++)
                    {
                        for (int j = 0; j < dimension; j++)
                        {

                            if (inplace)
                            {
                                pixel += matrix[i, j] * (RGBInt)b.GetPixel(x + i, y + j);
                            }
                            else
                            {
                                pixel += matrix[i, j] * (RGBInt)bSrc.GetPixel(x + i, y + j);
                            }
                        }
                    }

                    pixel = (pixel / m.Factor) + m.Offset;

                    Color newColor = pixel.ConvertToColor();
                    b.SetPixel(x + dimension / 2, y + dimension / 2, newColor);

                }

            }

            return true;
        }

        public static bool Conv(Bitmap b, ConvMatrix m, int dimension = 3, bool inplace = false)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = inplace ? (byte*)(void*)Scan0 : (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;
                int max = b.Width * b.Height;
                int nPixel;
                int[,] matrix = m.GetMatix(dimension);
                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {

                        for (int r = 0; r < 3; r++)
                        {
                            nPixel = 0;
                            for (int i = 0; i < dimension ; i++)
                            {
                                for (int j =0; j < dimension ; j++)
                                {
                                    //int src;
                                    //if (x + j < 0 || x + j > b.Width)
                                    //{
                                    //    src = 255;
                                    //}
                                    //else
                                    //{
                                    //    if (y + i < 0 || y + i > b.Height)
                                    //    {
                                    //        src = 255;
                                    //    }
                                    //    else
                                    //    {
                                    //        src = pSrc[j * 3 + r + stride * i];
                                    //    }
                                    //}

                                    nPixel += pSrc[j * 3 + r + stride * i] * matrix[i , j];
                                }
                            }

                            nPixel = nPixel / m.Factor + m.Offset;
                            if (nPixel < 0) nPixel = 0;
                            if (nPixel > 255) nPixel = 255;
                            p[3 + r + stride] = (byte)nPixel;
                        }

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
                        nPixel = ((((pSrc[2] * m.a) + (pSrc[5] * m.b) + (pSrc[8] * m.c) +
                            (pSrc[2 + stride] * m.d) + (pSrc[5 + stride] * m.e) + (pSrc[8 + stride] * m.f) +
                            (pSrc[2 + stride2] * m.g) + (pSrc[5 + stride2] * m.h) + (pSrc[8 + stride2] * m.i)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.a) + (pSrc[4] * m.b) + (pSrc[7] * m.c) +
                            (pSrc[1 + stride] * m.d) + (pSrc[4 + stride] * m.e) + (pSrc[7 + stride] * m.f) +
                            (pSrc[1 + stride2] * m.g) + (pSrc[4 + stride2] * m.h) + (pSrc[7 + stride2] * m.i)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.a) + (pSrc[3] * m.b) + (pSrc[6] * m.c) +
                            (pSrc[0 + stride] * m.d) + (pSrc[3 + stride] * m.e) + (pSrc[6 + stride] * m.f) +
                            (pSrc[0 + stride2] * m.g) + (pSrc[3 + stride2] * m.h) + (pSrc[6 + stride2] * m.i)) / m.Factor) + m.Offset);

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
