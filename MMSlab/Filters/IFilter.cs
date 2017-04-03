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
        bool GaussianBlur(Bitmap b, FilterOptions opt);
        bool GaussianBlurInplace(Bitmap b, FilterOptions opt);

        bool Brightness(Bitmap b, FilterOptions opt);
        bool Contrast(Bitmap b, FilterOptions opt);

        bool EdgeDetectHorizontal(Bitmap b, FilterOptions opt);
        bool Water(Bitmap b, FilterOptions opt);


    }
}
