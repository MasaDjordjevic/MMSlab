using MMSlab;
using MMSlab.Huffman;
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
            Bitmap test = CreateTestBitmap();
            HuffmanTree tree = new HuffmanTree(test);            
            PrintCodes(tree.Root);
            BitArray code = tree.Encode(test);
            foreach(var a in tree.CodeDict.Keys)
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
