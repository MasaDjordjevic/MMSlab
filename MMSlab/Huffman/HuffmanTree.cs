using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Huffman
{
    public static class HuffmanTree
    {
        public static List<HuffmanNode> GetList(Bitmap b)
        {
            int[] stats = ColorModels.generateFrequencyStatistics(b);
            List<HuffmanNode> retList = new List<HuffmanNode>();
            for(int i=0; i<256; i++)
            {
                retList.Add(new HuffmanNode((sbyte)i, stats[i]));
            }
            retList.Sort();

            return retList;
        }


        public static void ListToTree(List<HuffmanNode> list)
        {
            while(list.Count > 1)
            {
                list.Add(new HuffmanNode(list[0], list[1]));
                list.RemoveRange(1, 2);
                list.Sort();
            }
        }

        public static void SetCodes(HuffmanNode root, string code = "")
        {
            if (root == null)
                return;
            if(root.left == null && root.right == null)
            {
                root.code = code;
                return;
            }
            SetCodes(root.left, code + "0");
            SetCodes(root.right, code + "1");
        }
    }
}
