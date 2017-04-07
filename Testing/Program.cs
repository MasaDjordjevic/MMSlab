using MMSlab.Huffman;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        public static Bitmap CreateTestBitmap()
        {
            int size = 2;
            Bitmap ret = new Bitmap(size, size);
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
            if (root.left == null && root.right == null)
            {
                Console.WriteLine("Val : {0} -  Code : {1} - Frequency : {2}", root.value, root.code, root.frequency);
                return;
            }
            PrintCodes(root.left);
            PrintCodes(root.right);
        }
    }
}
