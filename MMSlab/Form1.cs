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

        Models.Model model = new Models.Model();
        private IView simpleView, ycbcrView;
        private IView currentView;


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

        }

        private void loadImage()
        {
            this.model.SetBitmap("G:\\mob slike\\5.7. bekstvo\\CameraZOOM-20140706061246.jpg");
            this.simpleView.Bitmap = this.model.GetBitmap();           
        }

        private void ycbcrToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if(ycbcrToolStripMenuItem.Checked)
            {
                this.setView(this.ycbcrView);
            }
            else
            {
                this.setView(this.simpleView);
            }

            this.currentView.Bitmap = this.model.GetBitmap();
        }

        private void setView(IView view)
        {
            this.currentView = view;
            this.currentView.BringToFront();
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
                    this.model.SetBitmap(openFileDialog.FileName);                    
                    
                    this.currentView.Bitmap = this.model.GetBitmap();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
