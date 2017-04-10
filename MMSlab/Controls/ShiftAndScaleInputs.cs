using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab.Controls
{
    public partial class ShiftAndScaleInputs : UserControl
    {
        public event EventHandler YShiftChanged;
        public event EventHandler YScaleChanged;
        public event EventHandler CbShiftChanged;
        public event EventHandler CbScaleChanged;
        public event EventHandler CrShiftChanged;
        public event EventHandler CrScaleChanged;

        public int YShift
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.textBox1.Text);
                }
                catch {}
                return 0;
            }
        }

        public double YScale
        {
            get
            {
                try
                {
                    return Convert.ToDouble(this.textBox2.Text);
                }
                catch { }
                return 1;
            }
        }

        public int CbShift
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.textBox3.Text);
                }
                catch { }
                return 0;
            }
        }

        public double CbScale
        {
            get
            {
                try
                {
                    return Convert.ToDouble(this.textBox4.Text);
                }
                catch { }
                return 1;
            }
        }

        public int CrShift
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.textBox5.Text);
                }
                catch { }
                return 1;
            }
        }

        public double CrScale
        {
            get
            {
                try
                {
                    return Convert.ToDouble(this.textBox6.Text);
                }
                catch { }
                return 1;
            }
        }

        public ShiftAndScaleInputs()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.YShiftChanged?.Invoke(this, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.YScaleChanged?.Invoke(this, e);
        }

        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.CbShiftChanged?.Invoke(this, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.CbScaleChanged?.Invoke(this, e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            this.CrShiftChanged?.Invoke(this, e);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            this.CrScaleChanged?.Invoke(this, e);
        }
    }
}
