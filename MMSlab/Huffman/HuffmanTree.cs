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



        public static List<HuffmanNode> GetList(Bitmap b)
        {
            int[] stats = ColorModels.generateFrequencyStatistics(b);
            List<HuffmanNode> retList = new List<HuffmanNode>();
            for (int i = 0; i < 256; i++)
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

        public BitArray Encode(Bitmap b)
        {
            List<bool> result = new List<bool>();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int[] data = new int[256];

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;


                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        result.AddRange(this.ValueDict[p[0]]);
                        p++;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return new BitArray(result.ToArray());
        }

        public Bitmap Decode(BitArray bits, int width, int height)
        {
            Bitmap b = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int[] data = new int[256];

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;
                int widthCounter = 0;
                
                string testString = "";

                for (int i = 0; i < bits.Length; i++)
                {
                    testString += bits[i].ToStr();

                    if (this.CodeDict.ContainsKey(testString))
                    {
                        byte pom = this.CodeDict[testString];
                        p[0] = pom;
                        p++;
                        widthCounter++;
                        if(widthCounter == nWidth)
                        {
                            widthCounter = 0;
                            p += nOffset;
                        }
                        testString = "";
                    }
                }
            }

            b.UnlockBits(bmData);

            return b;
        }

    }
}
