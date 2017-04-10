using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Filters
{
    public class FilterOptions
    {
        public int Weight { get; set; }

        public int Dimension { get; set; }

        public ShiftAndScaleOptions ShiftAndScaleOptions { get; set; }

        public FilterOptions(int weight)
        {
            this.Weight = weight;
            this.Dimension = 3;
        }

        public FilterOptions(int weight, int dimension)
        {
            this.Weight = weight;
            this.Dimension = dimension;
        }

        public FilterOptions(ShiftAndScaleOptions opt)
        {
            this.ShiftAndScaleOptions = opt;
        }
    }
}
