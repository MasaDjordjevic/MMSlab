using MMSlab.YImageFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Views
{
    public class DownsamplingStrategy : splitViewStrategy 
    {
        public override Bitmap[] generateImages(Bitmap b)
        {
            Bitmap[] channels = new Bitmap[3];

            DownsampleFormat bDwn = new DownsampleFormat(b);

            channels[0] = bDwn.DownsampleImage("Y");
            channels[1] = bDwn.DownsampleImage("Cb");            
            channels[2] = bDwn.DownsampleImage("Cr");

            return channels;
        }

         
    }
}
