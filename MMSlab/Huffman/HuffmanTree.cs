using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Huffman
{
    public class HuffmanTree
    {
        public HuffmanNode Root { get; set; }
        public Dictionary<byte, List<bool>> ValueDict = new Dictionary<byte, List<bool>>();
        public Dictionary<string, byte> CodeDict = new Dictionary<string, byte>();



        public static List<HuffmanNode> GetList(byte[] data)
        {
            int[] stats = generateFrequencyStatistics(data);
            List<HuffmanNode> retList = new List<HuffmanNode>();
            for (int i = 0; i < 256; i++)
            {
                retList.Add(new HuffmanNode((byte)i, stats[i]));
            }
            retList.Sort();
            return retList;
        }

        public static int[] generateFrequencyStatistics(byte[] data)
        {            
            int[] ret = new int[256];

            for(int i = 0; i < data.Length; i++)
            {
                ret[data[i]]++;
            }  
                       
            return ret;
        }

        public HuffmanTree(byte[] data)
        {
            this.Root = ListToTree(GetList(data));
            this.SetCodes();
        }

        public static HuffmanNode ListToTree(List<HuffmanNode> list)
        {
            while (list.Count > 1)
            {
                list.Add(new HuffmanNode(list[0], list[1]));
                list.RemoveRange(0, 2);
                list.Sort();
            }
            if (list.Count == 1)
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
            if (root.left == null && root.right == null)
            {
                root.code = code;
                this.ValueDict.Add(root.value, root.code.ToBoolList());
                this.CodeDict.Add(root.code, root.value);
                return;
            }
            SetCodes(root.left, code + "0");
            SetCodes(root.right, code + "1");
        }

        public BitArray Encode(byte[] data)
        {
            List<bool> result = new List<bool>();

            for(int i = 0; i < data.Length; i++)
            {
                result.AddRange(this.ValueDict[data[i]]);
            }

            return new BitArray(result.ToArray());
        }

        public byte[] Decode(BitArray bits, int width, int height)
        {
            List<byte> list = new List<byte>();
                
                string testString = "";

                for (int i = 0; i < bits.Length; i++)
                {
                    testString += bits[i].ToStr();

                    if (this.CodeDict.ContainsKey(testString))
                    {
                        list.Add(this.CodeDict[testString]);                       
                        testString = "";
                    }
                }


            return list.ToArray();
        }

    }
}
