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
        }

        protected override void OnResize(EventArgs e)
        {
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
                int hStart = this.AutoScrollPosition.Y + hOffset;

                this.SetChannels();

                g.DrawImage(this.Bitmap, new Rectangle(wStart, hStart, imgWidth, imgHeight));
                g.DrawImage(this.channels[0], new Rectangle(wStart + width, hStart, imgWidth, imgHeight));
                g.DrawImage(this.channels[1], new Rectangle(wStart, hStart + height, imgWidth, imgHeight));
                g.DrawImage(this.channels[2], new Rectangle(wStart + width, hStart + height, imgWidth, imgHeight));

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
                        RGB yRGB = new RGB(ycbcr.Y, ycbcr.Y, ycbcr.Y);
                        Y[2] = yRGB.R;
                        Y[1] = yRGB.G;
                        Y[0] = yRGB.B;

                        //Cb
                        RGB cbRGB = ColorModels.YCbCrToRGB(new YCbCr(0, ycbcr.Cb, 0));
                        Cb[2] = cbRGB.R;
                        Cb[1] = cbRGB.G;
                        Cb[0] = cbRGB.B;

                        //Cr
                        RGB crRGB = ColorModels.YCbCrToRGB(new YCbCr(0, 0, ycbcr.Cr));
                        Cr[2] = crRGB.R;
                        Cr[1] = crRGB.G;
                        Cr[0] = crRGB.B;



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
