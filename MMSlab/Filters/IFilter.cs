using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Filters
{
    public interface IFilter
    {
        bool GaussianBlur(Bitmap b, int nWeight = 4);
        bool GaussianBlurInplace(Bitmap b, int nWeight = 4);

        bool Brightness(Bitmap b, int nBrightness);
        bool Contrast(Bitmap b, int nContrast);

    }
}
