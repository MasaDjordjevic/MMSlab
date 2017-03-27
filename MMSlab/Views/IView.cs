using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MMSlab.Views
{
    public abstract class IView : UserControl
    {
        public abstract System.Drawing.Bitmap Bitmap { get; set; }

        public IView()
        {
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.AutoSize = true;
        }
    }
}
