﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MMSlab.Views
{
    public partial class YcbCrView : UserControl, IView
    {
        #region Variables
        private Bitmap bitmap = null;
        private Bitmap[] channels = new Bitmap[3];
        private double zoom = 0.2;
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
                for (int i = 0; i < 3; i++)
                {
                    this.channels[i] = (Bitmap)value.Clone();
                }

                base.Invalidate();
            }
        }
        #endregion
        //unused
        public double Zoom { get; set; }


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
            this.chart1.Series.Add("Y");
            this.chart1.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series["Y"]["PointWidth"] = "1";
        }

        protected override void OnResize(EventArgs e)
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

              
                this.chart1.Location = new Point(wStart + width, hStart);
                this.chart1.Size = new Size(imgWidth, imgHeight);
            }
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;

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

              //  this.SetChannels();

                g.DrawImage(this.Bitmap, new Rectangle(wStart, hStart, imgWidth, imgHeight));
                this.chart1.Series["Y"].Points.Clear();
                //this.chart1.Location = new Point(wStart + width, hStart);
                //this.chart1.Size = new Size(imgWidth, imgHeight);

                this.chart1.Series["Y"].Points.AddXY(10, 20);
                this.chart1.Series["Y"].Points.AddXY(20, 30);
                this.chart1.Series["Y"].Points.AddXY(30, 40);
                this.chart1.Series["Y"].Points.AddXY(130, 60);
                this.chart1.Series["Y"].Points.AddXY(230, 20);
                this.chart1.Series["Y"].Points.AddXY(150, 150);
                this.chart1.Series["Y"].Points.AddXY(200, 5);
                this.chart1.Series["Y"].Points.AddXY(170, 180);
                

                // g.DrawImage(this.channels[0], new Rectangle(wStart + width, hStart, imgWidth, imgHeight));
                // g.DrawImage(this.channels[1], new Rectangle(wStart, hStart + height + 2*hOffset, imgWidth, imgHeight));
                // g.DrawImage(this.channels[2], new Rectangle(wStart + width, hStart + height + 2*hOffset, imgWidth, imgHeight));

            }

        }

        private void SetChannels()
        {
            Bitmap b = this.Bitmap;
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            BitmapData[] channelsBmData = new BitmapData[3];
            System.IntPtr[] cScan0 = new IntPtr[3];
            for (int i = 0; i < 3; i++)
            {
                channelsBmData[i] = this.channels[i].LockBits(new Rectangle(0, 0, this.channels[i].Width, this.channels[i].Height), ImageLockMode.ReadWrite, this.channels[i].PixelFormat);
                cScan0[i] = channelsBmData[i].Scan0;
            }

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* Y = (byte*)(void*)cScan0[0];
                byte* Cb = (byte*)(void*)cScan0[1];
                byte* Cr = (byte*)(void*)cScan0[2];


                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        YCbCr ycbcr = ColorModels.RGBtoYCbCr(new RGB(red, green, blue));

                        //Y
                        Y[2] = ycbcr.Y;
                        Y[1] = ycbcr.Y;
                        Y[0] = ycbcr.Y;

                        //Cb
                        RGB cbRGB = ColorModels.YCbCrToRGB(new YCbCr(ycbcr.Y, ycbcr.Cb, ycbcr.Y));
                        Cb[2] = cbRGB.R;
                        Cb[1] = cbRGB.G;
                        Cb[0] = (byte)(cbRGB.B + ycbcr.Cb);

                        //Cr
                        RGB crRGB = ColorModels.YCbCrToRGB(new YCbCr(ycbcr.Y, ycbcr.Y, ycbcr.Cr));
                        Cr[2] = (byte)(crRGB.R + ycbcr.Cr);
                        Cr[1] = 0;
                        Cr[0] = 0;



                        p += 3;
                        Y += 3;
                        Cb += 3;
                        Cr += 3;
                    }
                    p += nOffset;
                    Y += nOffset;
                    Cb += nOffset;
                    Cr += nOffset;
                }
            }

            b.UnlockBits(bmData);
            for (int i = 0; i < 3; i++)
            {
                this.channels[i].UnlockBits(channelsBmData[i]);
            }
        }

    }
}
