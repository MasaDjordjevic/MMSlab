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

        public FilterOptions(int weight)
        {
            this.Weight = weight;
        }
    }
}
