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
    public partial class Form1 : Form
    {

        Models.IModel model = new Models.Model();
        private IView simpleView, ycbcrView;
        private Controllers.Controller controller;


        public Form1()
        {
            loadComponents();
            loadImage();
            InitializeComponent();
        }

        private void loadComponents()
        {
            this.simpleView = new SimpleImageView();
            this.simpleView.Location = new System.Drawing.Point(0, 0);
            this.simpleView.Name = "simple view";
            this.Controls.Add(this.simpleView);

            this.ycbcrView = new YcbCrView();
            this.ycbcrView.Location = new System.Drawing.Point(0, 0);
            this.ycbcrView.Name = "ycbcr view";
            this.Controls.Add(this.ycbcrView);

            this.controller = new Controllers.Controller(this.model, this.simpleView);

        }

        private void loadImage()
        {
            this.controller.LoadImage("G:\\mob slike\\5.7. bekstvo\\CameraZOOM-20140706061246.jpg");
        }

        private void ycbcrToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.controller.SetView(ycbcrToolStripMenuItem.Checked ? this.ycbcrView : this.simpleView);       
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
