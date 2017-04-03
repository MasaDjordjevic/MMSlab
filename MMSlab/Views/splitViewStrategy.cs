using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MMSlab.Views
{
    public abstract class splitViewStrategy
    {
        public virtual Bitmap[] generateImages(Bitmap b)
        {
            return null;
        }
        public virtual int[][] generateCharts(Bitmap b)
        {
            return null;
        }

        public bool ImageType { get; set; }

        public splitViewStrategy(bool imageType = true)
        {
            this.ImageType = imageType;
        }

    }
}
