using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab
{
    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RGB(byte r, byte g, byte b)
        {
            this.R = r;
            this.B = b;
            this.G = g;
        }
    }

    public class YCbCr
    {
        public byte Y { get; set; }
        public byte Cb { get; set; }
        public byte Cr { get; set; }

        public YCbCr(byte y, byte b, byte r)
        {
            this.Y = y;
            this.Cb = b;
            this.Cr = r;
        }
    }

    public class ColorModels
    {
        public static RGB YCbCrToRGB(YCbCr ycbcr)
        {
            float r = Math.Max(0.0f, Math.Min(1.0f, (float)(ycbcr.Y + 1.4022 * (ycbcr.Cr-128))));
            float g = Math.Max(0.0f, Math.Min(1.0f, (float)(ycbcr.Y - 0.3456 * (ycbcr.Cb-128) - 0.7145 * (ycbcr.Cr-128))));
            float b = Math.Max(0.0f, Math.Min(1.0f, (float)(ycbcr.Y + 1.7710 * (ycbcr.Cb-128))));

            return new RGB((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }


        public static YCbCr RGBtoYCbCr(RGB rgb)
        {
            byte resY = (byte)((0.299 * rgb.R) + (0.587 * rgb.G) + (0.114 * rgb.B));
            byte resCb = (byte)(128 - (0.168736 * rgb.R) - (0.331264 * rgb.G) + (0.5 * rgb.B));
            byte resCr = (byte)(128 + (0.5 * rgb.R) - (0.418688 * rgb.G) - (0.081312 * rgb.B));

            return new YCbCr(resY, resCb, resCr);
        }

    }
  

}
