using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Huffman
{
    public class HuffmanTree
    {
        public HuffmanNode Root { get; set; }
        public Dictionary<byte, BitArray> ValueDict = new Dictionary<byte, BitArray>();
        public Dictionary<BitArray, byte> CodeDict = new Dictionary<BitArray, byte>();



        public static List<HuffmanNode> GetList(Bitmap b)
        {
            int[] stats = ColorModels.generateFrequencyStatistics(b);
            List<HuffmanNode> retList = new List<HuffmanNode>();
            for(int i=0; i<256; i++)
            {
                retList.Add(new HuffmanNode((byte)i, stats[i]));
            }
            retList.Sort();
            return retList;
        }

        public HuffmanTree(Bitmap b)
        {
            this.Root = ListToTree(GetList(b));
            this.SetCodes();
        }

        public static HuffmanNode ListToTree(List<HuffmanNode> list)
        {
            while(list.Count > 1)
            {
                list.Add(new HuffmanNode(list[0], list[1]));
                list.RemoveRange(0, 2);
                list.Sort();
            }
            if(list.Count == 1)
            {
                return list[0];
            }

            return null;
        }

        public void SetCodes()
        {
            SetCodes(this.Root);
        }
        public void SetCodes(HuffmanNode root, string code = "")
        {

            if (root == null)
                return;
            if(root.left == null && root.right == null)
            {
                root.code = code;
                BitArray bCode = root.code.ToBitArray();
                this.ValueDict.Add(root.value, bCode);
                this.CodeDict.Add(bCode, root.value);
                return;
            }
            SetCodes(root.left, code + "0");
            SetCodes(root.right, code + "1");
        }
        
        
        
    }
}
