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
using System.IO;

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
            ((YcbCrView)ycbcrView).Strategy = new YCbCrStrategy();
            Controls.Add(ycbcrView);
        }

        private void loadImage()
        {
            this.controller.LoadImage("G:\\mob slike\\5.7. bekstvo\\testSlika.jpg");
            this.listView1.LargeImageList = new ImageList();
            this.listView1.LargeImageList.ImageSize = new Size(70, 70);
            this.listView1.View = View.LargeIcon;
        }
    

        private void ycbcrToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ((YcbCrView)ycbcrView).Strategy = new YCbCrStrategy();
            this.controller.SetView(ycbcrToolStripMenuItem.Checked ? this.ycbcrView : this.simpleView);
            this.trackBar1.Enabled = !ycbcrToolStripMenuItem.Checked;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.controller = new Controllers.Controller(this.model, this.simpleView, new CommonControls(this.statusLabel, this.progressBar, this.listView1));
            this.options = new Options(this.controller);
            this.controller.options = this.options;
            loadImage();
        }

        private void brightnessFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.BrightnessFilter();
            this.textBoxPlaceholder1.Visible = true;
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.ContrastFilter();
            this.textBoxPlaceholder1.Visible = true;
        }

        private void guassialBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.GaussianBlur();
            this.textBoxPlaceholder1.Visible = true;
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
            this.textBoxPlaceholder1.Visible = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files(*.png)|*.png";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                this.model.Bitmap.Save(saveFileDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.options.Zoom = (double)trackBar1.Value / 10.0;
        }

        private void ycbcrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.historamToggle.Visible = ycbcrToolStripMenuItem.Checked;
        }

        private void historamToggle_CheckedChanged(object sender, EventArgs e)
        {
            if(historamToggle.Checked)
            {
                ((YcbCrView)ycbcrView).Strategy = new YCbCrHistogramStrategy(false);
                return;
            }
            else
            {
                ((YcbCrView)ycbcrView).Strategy = new YCbCrStrategy();
            }
        }

        private void edgeDetectonHorisontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.EdgeDetectionHorizontal();
        }

        private void waterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.Water();
            this.textBoxPlaceholder1.Visible = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ((YcbCrView)ycbcrView).Strategy = new GuassianBlurStrategy();
            this.controller.SetView(this.ycbcrView);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.UndoAction();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.RedoAction();
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
