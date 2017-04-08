using MMSlab.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Views
{
    public class GuassianBlurStrategy : splitViewStrategy
    {
        public override Bitmap[] generateImages(Bitmap b)
        {
            Bitmap[] channels = new Bitmap[3];
            for (int i = 0; i < 3; i++)
            {
                channels[i] = (Bitmap)b.Clone();
            }

            CoreFilters filter = new CoreFilters(null);
            //SimpleFilters simpleFilter = new SimpleFilters(null);

            //channels[0] = ConvFilters.ExtendBitmap(b, 10);
            //channels[1] = ConvFilters.ExtendBitmap(b, 20);
            //channels[2] = ConvFilters.ExtendBitmap(b, 50);

            //channels[2] = ConvFilters.ExtendBitmap(channels[1], 3);

            filter.GaussianBlur(channels[0], new FilterOptions(10, 3));
            //channels[1] = ConvFilters.ExtendBitmap(b, 3/2);

            filter.GaussianBlur(channels[1], new FilterOptions(10, 5));
            filter.GaussianBlur(channels[2], new FilterOptions(10, 7));

            return channels;

        }
    }
}
