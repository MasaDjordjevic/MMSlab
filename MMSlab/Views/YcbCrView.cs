using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;

namespace MMSlab.Views
{
    public partial class YcbCrView : UserControl, IView
    {
        #region Variables
        private Bitmap bitmap = null;
        private Bitmap[] channels = new Bitmap[3];
        private Rectangle[] rectangles = new Rectangle[4];
        private double zoom = 0.2;
        private Chart[] charts = new Chart[4];

        private int selectedChannel = -1;
        #endregion

        #region Properties
        public Bitmap Bitmap
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
                this.channels = this.Strategy.generateImages(this.Bitmap);
                this.OnResize(null);
            }
        }
        #endregion
        //unused
        public double Zoom { get; set; }

        public string SelectedChannel
        {
            get
            {
                switch (this.selectedChannel)
                {
                    case 0: return "Y";
                    case 1: return "Cb";
                    case 2: return "Cr";
                }
                return null;
            }
        }


        private splitViewStrategy strategy;
        public splitViewStrategy Strategy
        {
            get
            {
                return this.strategy;
            }

            set
            {
                this.strategy = value;
                for (int i = 1; i < 4; i++)
                {
                    this.charts[i].Visible = false;
                }
                this.Click -= this.YcbCrView_Click;
                this.Click += this.Strategy.isSelectable ? new System.EventHandler(this.YcbCrView_Click) : null;
                this.selectedChannel = -1;
                this.OnResize(null);
            }
        }

        public new void BringToFront()
        {
            base.BringToFront();
        }

        public YcbCrView()
        {
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.AutoSize = true;
            InitializeComponent();

            this.charts[1] = this.chart1;
            this.charts[2] = this.chart2;
            this.charts[3] = this.chart3;
            for (int i = 1; i < 4; i++)
            {
                this.charts[i].Series.Add("s");
                this.charts[i].Series["s"]["PointWidth"] = "1";
                this.charts[i].Visible = false;
            }
        }


        private void SetRectangles()
        {
            if (this.Bitmap != null)
            {
                int width = (this.ClientSize.Width - this.AutoScrollPosition.X) / 2;
                int height = (this.ClientSize.Height - this.AutoScrollPosition.Y) / 2;

                double scale = Math.Min((double)width / (double)this.Bitmap.Width, (double)height / (double)this.Bitmap.Height);

                int imgWidth = (int)(this.Bitmap.Width * scale);
                int imgHeight = (int)(this.Bitmap.Height * scale);
                int wOffset = (int)((width - imgWidth) / 2.0);
                int hOffset = (int)((height - imgHeight) / 2.0);
                int wStart = this.AutoScrollPosition.X + wOffset;
                int hStart = this.AutoScrollPosition.Y;

                this.rectangles[0] = new Rectangle(wStart, hStart, imgWidth, imgHeight);
                this.rectangles[1] = new Rectangle(wStart + width, hStart, imgWidth, imgHeight);
                this.rectangles[2] = new Rectangle(wStart, hStart + height + 2 * hOffset, imgWidth, imgHeight);
                this.rectangles[3] = new Rectangle(wStart + width, hStart + height + 2 * hOffset, imgWidth, imgHeight);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Bitmap != null && !this.Strategy.ImageType)
            {
                this.SetRectangles();
                for (int i = 1; i <= 3; i++)
                {
                    this.charts[i].Location = new Point(this.rectangles[i].X, this.rectangles[i].Y);
                    this.charts[i].Size = new Size(this.rectangles[i].Width, this.rectangles[i].Height);
                }
            }
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;

            if (this.Bitmap != null)
            {

                if (this.Strategy.ImageType)
                {
                    this.SetRectangles();
                    g.DrawImage(this.Bitmap, this.rectangles[0]);
                    g.DrawImage(this.channels[0], this.rectangles[1]);
                    g.DrawImage(this.channels[1], this.rectangles[2]);
                    g.DrawImage(this.channels[2], this.rectangles[3]);

                    if (this.selectedChannel > -1)
                    {
                        g.DrawRectangle(new Pen(Color.Red), this.rectangles[this.selectedChannel + 1]);
                    }

                    return;
                }


                g.DrawImage(this.Bitmap, this.rectangles[0]);

                int[][] data = this.Strategy.generateHistogramStatistics(this.Bitmap);
                for (int j = 0; j < 3; j++)
                {
                    this.charts[j + 1].Series["s"].Points.Clear();
                    this.charts[j + 1].Series["s"].Points.DataBindY(data[j]);
                    this.charts[j + 1].Visible = true;
                }
            }

        }

        private void YcbCrView_Click(object sender, EventArgs e)
        {
            int x = MousePosition.X;
            int y = MousePosition.Y;

            int width = (this.ClientSize.Width - this.AutoScrollPosition.X) / 2;
            int height = (this.ClientSize.Height - this.AutoScrollPosition.Y) / 2;
            if (x < width && y > height)
            {
                this.selectedChannel = 1;
            }
            if (x > width && y > height)
            {
                this.selectedChannel = 2;
            }
            if (x > width && y < height)
            {
                this.selectedChannel = 0;
            }

            Invalidate();
        }
    }
}
