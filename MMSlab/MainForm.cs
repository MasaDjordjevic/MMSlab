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

            LollipopToggleText.CheckForIllegalCrossThreadCalls = false;
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

            this.SetShiftAndScaleEventHandlers();
        }

        private void SetShiftAndScaleEventHandlers()
        {
            //Y
            this.shiftAndScaleInputs1.YShiftChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.YShift = this.shiftAndScaleInputs1.YShift; };
            this.shiftAndScaleInputs1.YScaleChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.YScale = this.shiftAndScaleInputs1.YScale; };
            //Cb
            this.shiftAndScaleInputs1.CbShiftChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.CbShift = this.shiftAndScaleInputs1.CbShift; };
            this.shiftAndScaleInputs1.CbScaleChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.CbScale = this.shiftAndScaleInputs1.CbScale; };
            //Cr
            this.shiftAndScaleInputs1.CrShiftChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.CrShift = this.shiftAndScaleInputs1.CrShift; };
            this.shiftAndScaleInputs1.CrScaleChanged += (object sender2, EventArgs e2) => { this.options.ShiftAndScaleOptions.CrScale = this.shiftAndScaleInputs1.CrScale; };
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
                catch (Exception ex)
                {
                    MessageBox.Show("Enter a number");
                    textBoxPlaceholder1.Text = "";
                    return;
                }

                if (e.KeyCode == Keys.A)
                {
                    this.controller.ReloadImageModel();
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
            if (this.controller.GetSelectedChannel() != null)
            {
                MessageBox.Show(this.controller.GetSelectedChannel());
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "G:\\mob slike\\5.7. bekstvo";
            saveFileDialog.Filter = "Lab files (*.a)|*.a|Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files(*.png)|*.png";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                string f = saveFileDialog.FileName;

                if (this.controller.GetSelectedChannel() != null && f.EndsWith(".a"))
                    YImageFormat.YImageFormat.SaveToFile(saveFileDialog.FileName, this.model.Bitmap, this.controller.GetSelectedChannel());

                if (f.EndsWith(".jpg"))
                    this.model.Bitmap.Save(f, System.Drawing.Imaging.ImageFormat.Jpeg);

                if (f.EndsWith(".bmp"))
                    this.model.Bitmap.Save(f, System.Drawing.Imaging.ImageFormat.Bmp);

                if (f.EndsWith(".png"))
                    this.model.Bitmap.Save(f, System.Drawing.Imaging.ImageFormat.Png);

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
            if (historamToggle.Checked)
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
            openFileDialog.Filter = "All valid files|*.a;*.bmp;*.jpg;*.png|Lab files (*.a)|*.a|Jpeg files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files(*.png)|*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog.FileName.EndsWith(".a"))
                    {
                        Bitmap b = YImageFormat.YImageFormat.ReadFromFile(openFileDialog.FileName);
                        this.controller.SetImage(b);
                    }
                    else
                    {
                        this.controller.LoadImage(openFileDialog.FileName);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void downsampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((YcbCrView)ycbcrView).Strategy = new DownsamplingStrategy();
            this.controller.SetView(this.ycbcrView);

        }

        private void shiftAndScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.ShiftAndScale();
        }

        private void toolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripMenuItem1.Checked)
            {
                ((YcbCrView)ycbcrView).Strategy = new GuassianBlurStrategy();
                this.controller.SetView(this.ycbcrView);
            }
            else
            {
                this.controller.SetView(this.simpleView);

            }
        }
    }
}
