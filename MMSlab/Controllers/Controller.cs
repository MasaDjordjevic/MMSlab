using MMSlab.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab.Controllers
{
    public class Controller: IController
    {
        private Models.IModel model;
        private Views.IView view;
        private delegate bool currentFilterDelegate(Bitmap b, Filters.FilterOptions opt);
        private currentFilterDelegate currentFilterFunction;
        public Options options { get; set; }
        private Filters.IFilter currentFilter;
        

        public Views.CommonControls commonControls { get; set; }

        public Controller(Models.IModel model, Views.IView view, Views.CommonControls commonControls)
        {
            this.model = model;
            this.view = view;
            this.view.BringToFront();
            this.commonControls = commonControls;
            this.currentFilter = new Filters.CoreFilters(this.commonControls);
        }

        public void SetMode()
        {
            if(this.options.CoreMode)
            {
                this.currentFilter = new Filters.CoreFilters(this.commonControls);
            }
            else
            {
                this.currentFilter = new Filters.SimpleFilters(this.commonControls);
            }
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

        public void ReloadImage()
        {
            this.model.LoadBitmap(this.model.FileLocation);
            this.SetImage(this.model.Bitmap);
        }

        public void SetImage(System.Drawing.Bitmap bitmap)
        {
            this.model.Bitmap = bitmap;
            this.view.Bitmap = this.model.Bitmap;
            this.commonControls.status = bitmap.Width.ToString() + " x " + bitmap.Height.ToString() + "         " + (this.model.FileSize/1024).ToString() + "KB";
        }

        public void BrightnessFilter()
        {
            this.currentFilter.Brightness(this.model.Bitmap, new FilterOptions(40));
            this.currentFilterFunction = this.currentFilter.Brightness;
            this.view.Bitmap = this.model.Bitmap;
        }

        public void ContrastFilter()
        {
            this.currentFilter.Contrast(this.model.Bitmap, new FilterOptions(40));
            this.currentFilterFunction = this.currentFilter.Contrast;
            this.view.Bitmap = this.model.Bitmap;
        }

        public void GaussianBlur(bool inplace = false)
        {            
            if(inplace)
            {
                this.currentFilterFunction = this.currentFilter.GaussianBlurInplace;
            }
            else
            {
                this.currentFilterFunction = this.currentFilter.GaussianBlur;

            }
            this.currentFilterFunction(this.model.Bitmap, new FilterOptions(4));
            this.view.Bitmap = this.model.Bitmap;
        }

        public void EdgeDetectionHorizontal()
        {
            this.currentFilter.EdgeDetectHorizontal(this.model.Bitmap, null);
            this.currentFilterFunction = this.currentFilter.EdgeDetectHorizontal;
            this.view.Bitmap = this.model.Bitmap;
        }

        public void Water()
        {
            this.currentFilter.Water(this.model.Bitmap, new FilterOptions(15));
            this.currentFilterFunction = this.currentFilter.Water;
            this.view.Bitmap = this.model.Bitmap;
        }

        public void WeightChangedRedo()
        {
            if(this.currentFilterFunction == null)
            {
                MessageBox.Show("Select filter first");
                return;
            }
            this.currentFilterFunction(this.model.Bitmap, new FilterOptions(this.options.Weight));
            this.view.Bitmap = this.model.Bitmap;
        }

        public void ZoomChanged()
        {
            this.view.Zoom = this.options.Zoom;
            this.view.Bitmap = this.model.Bitmap;
        }

    }
}
