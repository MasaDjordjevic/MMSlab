using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Huffman
{
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        public sbyte value;
        public int frequency;
        public string code;
        public HuffmanNode left;
        public HuffmanNode right;


        public HuffmanNode(sbyte value, int frequency = 1)
        {
            this.value = value;
            this.frequency = frequency;
        }

        public HuffmanNode(HuffmanNode n1, HuffmanNode n2)
        {
            //swap, n1 should be < n2
            if(n1.frequency > n2.frequency)
            {
                HuffmanNode pom = n1;
                n1 = n2;
                n2 = pom;
            }

            this.left = n1;
            this.right = n2;
            this.frequency = n1.frequency + n2.frequency;            
        }

        public int CompareTo(HuffmanNode otherNode)
        {
            return this.frequency.CompareTo(otherNode.frequency);
        }

    }
}
