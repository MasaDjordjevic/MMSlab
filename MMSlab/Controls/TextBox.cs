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
    public partial class TextBox : UserControl
    {

        string fontColor = "#999999";
        string placeholder = "Enter text here";

        public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        public string FontColor
        {
            get { return fontColor; }
            set
            {
                fontColor = value;
                Invalidate();
            }
        }

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                Invalidate();
            }
        }

        public TextBox()
        {

            InitializeComponent();
            this.textBox1.Dock = DockStyle.Fill;

            this.textBox1.Text = placeholder;
        }

        
       
        public void RemovePlaceholder(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        public void AddPlaceholder(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = placeholder;
        }
    }
}
