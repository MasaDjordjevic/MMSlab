using MMSlab.Huffman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.YImageFormat
{
    public class YImageFormat
    {
        public static void SaveToFile(string fileName, Bitmap b, string channel)
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(b.Width);
                writer.Write(b.Height);
                writer.Write(channel);

                DownsampleFormat bDwn = new DownsampleFormat(b);
                byte[] image = bDwn.DownsampleToByteArray(channel);

                HuffmanTree tree = new HuffmanTree(image);
                byte[] dictionary = tree.SerializeToBytes();
                writer.Write(dictionary.Length);
                writer.Write(dictionary);

                BitArray encodedImage = tree.Encode(image);
                byte[] encodedImageBytes = new byte[encodedImage.Length / 8 + (encodedImage.Length % 8 == 0 ? 0 : 1)];
                encodedImage.CopyTo(encodedImageBytes, 0);
                writer.Write(encodedImage.Length);
                writer.Write(encodedImageBytes.Length);
                writer.Write(encodedImageBytes);
            }
        }

        public static Bitmap ReadFromFile(string fileName)
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                string channel = reader.ReadString();
                int dictLength = reader.ReadInt32();
                byte[] dict = reader.ReadBytes(dictLength);

                HuffmanTree tree = new HuffmanTree();
                tree.DeserializeFromBytes(dict);

                int imgLength = reader.ReadInt32();
                int imgBytesLenght = reader.ReadInt32();
                byte[] img = reader.ReadBytes(imgBytesLenght);

                BitArray bits = new BitArray(img);
                bits.Length = imgLength;
                byte[] decodedImage = tree.Decode(bits, width, height);

                DownsampleFormat dwn = new DownsampleFormat();
                Bitmap ret = dwn.GenerateFromByteArray(decodedImage, channel, width, height);
                return ret;
            }      
        }
    }
}
