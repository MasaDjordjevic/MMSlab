using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Controllers
{
    public class Controller: IController
    {
        private Models.IModel model;
        private Views.IView view;

        public Views.CommonControls commonControls { get; set; }

        public Controller(Models.IModel model, Views.IView view)
        {
            this.model = model;
            this.view = view;
            this.view.BringToFront();
        }

        public void SetView(Views.IView view)
        {
            this.view = view;
            this.view.Bitmap = this.model.Bitmap;
            this.view.BringToFront();
        }        

        public void LoadImage(string fileLocation)
        {
            this.model.LoadBitmap(fileLocation);
            this.SetImage(this.model.Bitmap);
        }

        public void SetImage(System.Drawing.Bitmap bitmap)
        {
            this.model.Bitmap = bitmap;
            this.view.Bitmap = this.model.Bitmap;
            this.commonControls.status = bitmap.Width.ToString() + " x " + bitmap.Height.ToString() + "         " + (this.model.FileSize/1024).ToString() + "KB";
        }
    }
}
