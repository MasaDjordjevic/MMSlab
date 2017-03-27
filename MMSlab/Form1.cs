using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab
{
    public partial class Form1 : Form
    {

        Models.Model model = new Models.Model();
        private Views.IView simpleView, ycbcrView;


        public Form1()
        {
            loadComponents();
            loadImage();
            InitializeComponent();
        }

        private void loadComponents()
        {
            this.simpleView = new Views.SimpleImageView();    
            this.simpleView.Location = new System.Drawing.Point(0, 0);
            this.simpleView.Name = "simple view";
            this.Controls.Add(this.simpleView);

            this.ycbcrView = new Views.YcbCrView();
            this.ycbcrView.Location = new System.Drawing.Point(0, 0);
            this.ycbcrView.Name = "ycbcr view";            
            this.Controls.Add(this.ycbcrView);
            this.ycbcrView.BringToFront();

        }

        private void loadImage()
        {
            this.model.SetBitmap("G:\\mob slike\\5.7. bekstvo\\CameraZOOM-20140706061246.jpg");
            this.ycbcrView.Bitmap = this.model.GetBitmap();
           
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
                    
                    this.simpleView.Bitmap = this.model.GetBitmap();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
