using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMSlab.Views;

namespace MMSlab
{
    public partial class MainForm : Form
    {

        Models.IModel model = new Models.Model();
        private IView simpleView, ycbcrView;
        private Controllers.Controller controller;
        private Options options;

        public MainForm()
        {
            loadComponents();
            
            InitializeComponent();
        }

        private void loadComponents()
        {
            this.simpleView = new SimpleImageView();
            UserControl simpleView = (UserControl)this.simpleView;
            simpleView.Location = new System.Drawing.Point(0, 0);
            simpleView.Name = "simple view";
            Controls.Add(simpleView);

            this.ycbcrView = new YcbCrView();
            UserControl ycbcrView = (UserControl)this.ycbcrView;
            ycbcrView.Location = new System.Drawing.Point(0, 0);
            ycbcrView.Name = "ycbcr view";
            Controls.Add(ycbcrView);
        }


        private void loadImage()
        {
            this.controller.LoadImage("G:\\mob slike\\5.7. bekstvo\\testSlika.jpg");
        }

        private void ycbcrToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.controller.SetView(ycbcrToolStripMenuItem.Checked ? this.ycbcrView : this.simpleView);       
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.controller = new Controllers.Controller(this.model, this.simpleView, new CommonControls(this.statusLabel, this.progressBar));
            this.options = new Options(this.controller);
            this.controller.options = this.options;
            loadImage();
        }

        private void brightnessFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.BrightnessFilter();
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.ContrastFilter();
        }

        private void guassialBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.GaussianBlur();
        }

        private void textBoxPlaceholder1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.R || e.KeyCode == Keys.A) && textBoxPlaceholder1.Text.Length > 0)
            {
                textBoxPlaceholder1.Text = textBoxPlaceholder1.Text.Remove(textBoxPlaceholder1.Text.Length - 1);
                int val;
                try
                {
                    val = Convert.ToInt32(textBoxPlaceholder1.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter a number");
                    textBoxPlaceholder1.Text = "";
                    return;
                }

                if(e.KeyCode == Keys.A)
                {
                    this.controller.ReloadImage();
                }

                this.options.Weight = val;
                textBoxPlaceholder1.SelectionStart = textBoxPlaceholder1.Text.Length;
                textBoxPlaceholder1.SelectionLength = 0;
                return;
            }
        }

        private void win32CoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.options.CoreMode = !this.options.CoreMode;
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.ReloadImage();
            this.textBoxPlaceholder1.Text = "";
        }

        private void guassianBlurInplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.GaussianBlur(true);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "G:\\mob slike\\5.7. bekstvo";
            openFileDialog.Filter = "Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files(*.png)|*.png|All valid files|*.bmp/*.jpg/*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.controller.LoadImage(openFileDialog.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
