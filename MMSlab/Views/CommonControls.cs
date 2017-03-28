using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Views
{
    public class CommonControls
    {
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private LollipopProgressBar progressBar { get; set; }

        public CommonControls(System.Windows.Forms.ToolStripStatusLabel statusLabel, LollipopProgressBar progressBar)
        {
            this.statusLabel = statusLabel;
            this.progressBar = progressBar;
        }

        public int progress
        {
            get { return this.progressBar.Value; }
            set
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Value = value;
                }
            }
        }

        public string status
        {
            get { return this.statusLabel.Text; }
            set
            {
                if (this.statusLabel != null)
                {
                    this.statusLabel.Text = value;
                }
            }
        }
    }
}
