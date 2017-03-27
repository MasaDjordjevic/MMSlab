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
        public Controller(Models.IModel model, Views.IView view)
        {
            this.model = model;
            this.view = view;
            this.view.BringToFront();
        }

        public void SetView(Views.IView view)
        {
            this.view = view;
            this.view.Bitmap = this.model.GetBitmap();
            this.view.BringToFront();
        }

        public void LoadImage(string fileLocation)
        {
            this.model.SetBitmap(fileLocation);
            this.view.Bitmap = this.model.GetBitmap();
        }
    }
}
