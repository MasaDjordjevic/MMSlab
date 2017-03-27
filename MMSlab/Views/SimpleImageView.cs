using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab.Views
{
    public partial class SimpleImageView : IView
    {

        #region Variables
        private Bitmap bitmap = null;
        private double zoom = 0.5;
        #endregion

        #region Properties
        public override Bitmap Bitmap
        {
            get
            {
                return this.bitmap;
            }

            set
            {
                if (value == null) return;                
                this.AutoScrollMinSize = new Size((int)(value.Width * zoom), (int)(value.Height * zoom));
                this.bitmap = value;
                base.Invalidate();
            }
        }
        #endregion

        public SimpleImageView()
        {           
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            if (this.Bitmap != null)
            {
                g.DrawImage(this.Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(this.Bitmap.Width * zoom), (int)(this.Bitmap.Height * zoom)));
            }

        }
    }
}
