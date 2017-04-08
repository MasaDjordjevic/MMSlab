using MMSlab;
using MMSlab.Huffman;
using MMSlab.YImageFormat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            RGB a = new RGB(20, 200,12);
            YCbCr y = ColorModels.RGBtoYCbCr(a);
            RGB aRestored = ColorModels.YCbCrToRGB(y);

       }

        public static void DownsampleTest()
        {
            Bitmap test = CreateTestBitmapLarge();
            DownsampleFormat down = new DownsampleFormat(test);
            byte[,] Y = DownsampleFormat.Downsample(down.Y);
            byte[,] Cb = DownsampleFormat.Downsample(down.Cb);
            byte[,] Cr = DownsampleFormat.Downsample(down.Cr);

            byte[,] Yrestored = DownsampleFormat.Restore(Y, 8, 8);

        }

        public static void HuffmanTest()
        {
            Bitmap test = CreateTestBitmap();

            HuffmanTree tree = new HuffmanTree(test);
            PrintCodes(tree.Root);
            BitArray code = tree.Encode(test);
            foreach (var a in tree.CodeDict.Keys)
            {
                if (a.Length < 4)
                {
                    Console.WriteLine("Dict: " + a + " Val: " + tree.CodeDict[a]);
                }
            }
            Bitmap decoded = tree.Decode(code, 2, 2);

            Color[] dec = new Color[4];
            dec[0] = decoded.GetPixel(0, 0);
            dec[1] = decoded.GetPixel(0, 1);
            dec[2] = decoded.GetPixel(1, 0);
            dec[3] = decoded.GetPixel(1, 1);
        }

        public static Bitmap CreateTestBitmap()
        {
            int size = 2;
            Bitmap ret = new Bitmap(size, size, PixelFormat.Format24bppRgb);
            ret.SetPixel(0, 0, Color.White);
            ret.SetPixel(0, 1, Color.Red);
            ret.SetPixel(1, 0, Color.Red);
            ret.SetPixel(1, 1, Color.Green);

            return ret;
        }

        public static Bitmap CreateTestBitmapLarge()
        {
            int size = 8;
            Bitmap ret = new Bitmap(size, size, PixelFormat.Format24bppRgb);
            ret.SetPixel(0, 0, Color.Gray);
            ret.SetPixel(0, 1, Color.Gray);
            ret.SetPixel(0, 2, Color.White);
            ret.SetPixel(0, 3, Color.White);
            ret.SetPixel(0, 4, Color.Gray);
            ret.SetPixel(0, 5, Color.Gray);
            ret.SetPixel(0, 6, Color.White);
            ret.SetPixel(0, 7, Color.White);
            ret.SetPixel(1, 0, Color.Gray);
            ret.SetPixel(1, 1, Color.Gray);
            ret.SetPixel(1, 2, Color.White);
            ret.SetPixel(1, 3, Color.White);
            ret.SetPixel(1, 4, Color.Gray);
            ret.SetPixel(1, 5, Color.Gray);
            ret.SetPixel(1, 6, Color.White);
            ret.SetPixel(1, 7, Color.White);

            ret.SetPixel(2, 0, Color.White);
            ret.SetPixel(2, 1, Color.White);
            ret.SetPixel(2, 2, Color.Gray);
            ret.SetPixel(2, 3, Color.Gray);
            ret.SetPixel(2, 4, Color.White);
            ret.SetPixel(2, 5, Color.White);
            ret.SetPixel(2, 6, Color.Gray);
            ret.SetPixel(2, 7, Color.Gray);
            ret.SetPixel(3, 0, Color.White);
            ret.SetPixel(3, 1, Color.White);
            ret.SetPixel(3, 2, Color.Gray);
            ret.SetPixel(3, 3, Color.Gray);
            ret.SetPixel(3, 4, Color.White);
            ret.SetPixel(3, 5, Color.White);
            ret.SetPixel(3, 6, Color.Gray);
            ret.SetPixel(3, 7, Color.Gray);

            ret.SetPixel(4, 0, Color.Gray);
            ret.SetPixel(4, 1, Color.Gray);
            ret.SetPixel(4, 2, Color.White);
            ret.SetPixel(4, 3, Color.White);
            ret.SetPixel(4, 4, Color.Gray);
            ret.SetPixel(4, 5, Color.Gray);
            ret.SetPixel(4, 6, Color.White);
            ret.SetPixel(4, 7, Color.White);
            ret.SetPixel(5, 0, Color.Gray);
            ret.SetPixel(5, 1, Color.Gray);
            ret.SetPixel(5, 2, Color.White);
            ret.SetPixel(5, 3, Color.White);
            ret.SetPixel(5, 4, Color.Gray);
            ret.SetPixel(5, 5, Color.Gray);
            ret.SetPixel(5, 6, Color.White);
            ret.SetPixel(5, 7, Color.White);

            ret.SetPixel(6, 0, Color.White);
            ret.SetPixel(6, 1, Color.White);
            ret.SetPixel(6, 2, Color.Gray);
            ret.SetPixel(6, 3, Color.Gray);
            ret.SetPixel(6, 4, Color.White);
            ret.SetPixel(6, 5, Color.White);
            ret.SetPixel(6, 6, Color.Gray);
            ret.SetPixel(6, 7, Color.Gray);
            ret.SetPixel(7, 0, Color.White);
            ret.SetPixel(7, 1, Color.White);
            ret.SetPixel(7, 2, Color.Gray);
            ret.SetPixel(7, 3, Color.Gray);
            ret.SetPixel(7, 4, Color.White);
            ret.SetPixel(7, 5, Color.White);
            ret.SetPixel(7, 6, Color.Gray);
            ret.SetPixel(7, 7, Color.Gray);

            return ret;
        }

        public static void PrintCodes(HuffmanNode root, string s = "")
        {
            if (root == null)
                return;
            if (root.left == null && root.right == null && root.frequency > 0)
            {
                Console.WriteLine("Val : {0} -  Code : {1} - Frequency : {2}", root.value, root.code, root.frequency);
                return;
            }
            PrintCodes(root.left);
            PrintCodes(root.right);
        }
    }
}
