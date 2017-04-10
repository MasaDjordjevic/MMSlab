using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab
{
    public class ShiftAndScaleOptions
    {
        public int YShift { get; set; }
        public double YScale { get; set; }
        public int CbShift { get; set; }
        public double CbScale { get; set; }
        public int CrShift { get; set; }
        public double CrScale { get; set; }


        public ShiftAndScaleOptions()
        {
            YScale = CbScale = CrScale = 1;
        }
    }
}
