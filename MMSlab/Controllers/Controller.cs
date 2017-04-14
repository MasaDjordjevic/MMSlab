using MMSlab.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSlab.Controllers
{
    public class Controller : IController
    {
        private Models.IModel model;
        private Views.IView view;
        private delegate bool currentFilterDelegate(Bitmap b, Filters.FilterOptions opt);
        private currentFilterDelegate currentFilterFunction;
        public Options options { get; set; }
        private Filters.IFilter currentFilter;

        private List<Action> undoList = new List<Action>(5);
        private List<Action> redoListe = new List<Action>(5);

        private Thread workerThread = null;

        public Views.CommonControls commonControls { get; set; }

        private Form parent;

        public Controller(Models.IModel model, Views.IView view, Views.CommonControls commonControls, Form parent)
        {
            this.model = model;
            this.view = view;
            this.view.BringToFront();
            this.commonControls = commonControls;
            this.currentFilter = new Filters.CoreFilters(this.commonControls);
            this.parent = parent;
        }

        private void ThreadFilter()
        {
            Bitmap newBitmap = (Bitmap)this.model.Bitmap.Clone();
            this.currentFilterFunction(newBitmap, new FilterOptions(this.options.Weight));
            this.model.Bitmap = newBitmap;
            this.view.Bitmap = (Bitmap)this.model.Bitmap.Clone();
            this.parent.Enabled = true;
        }


        public void SetMode()
        {
            if (this.options.CoreMode)
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

        public string GetSelectedChannel()
        {
            return this.view.SelectedChannel;
        }



        public void LoadImage(string fileLocation)
        {
            if (this.model.Bitmap != null)
                this.DoAction("Load");

            if (!this.model.LoadBitmap(fileLocation))
                return;           

            this.SetImage(this.model.Bitmap);
        }

        public void ReloadImage()
        {
            this.DoAction("Reload");
            if (!this.model.LoadBitmap(this.model.FileLocation))
                return;

            this.SetImage(this.model.Bitmap);
        }

        public void ReloadImageModel()
        {
            this.model.LoadBitmap(this.model.FileLocation);
        }

        public void SetImage(System.Drawing.Bitmap bitmap)
        {
            this.model.Bitmap = bitmap;
            this.view.Bitmap = (Bitmap)bitmap.Clone();
            this.commonControls.status = bitmap.Width.ToString() + " x " + bitmap.Height.ToString() + "         " + (this.model.FileSize / 1024).ToString() + "KB";
        }

        public void FilterModel(string filterName = null)
        {
            this.DoAction(filterName);
            this.workerThread = new Thread(new ThreadStart(this.ThreadFilter));
            this.parent.Enabled = false;
            this.workerThread.Start();
        }

        public void BrightnessFilter()
        {
            this.currentFilterFunction = this.currentFilter.Brightness;
            this.options.Weight = 40;
        }

        public void ContrastFilter()
        {
            this.currentFilterFunction = this.currentFilter.Contrast;
            this.options.Weight = 40;
        }

        public void GaussianBlur(bool inplace = false)
        {
            this.DoAction("GaussianBlur");

            if (inplace)
            {
                this.currentFilterFunction = this.currentFilter.GaussianBlurInplace;
            }
            else
            {
                this.currentFilterFunction = this.currentFilter.GaussianBlur;

            }
            this.options.Weight = 10;
        }

        public void EdgeDetectionHorizontal()
        {
            this.currentFilterFunction = this.currentFilter.EdgeDetectHorizontal;
            this.options.Weight = 0;
        }

        public void Water()
        {
            this.currentFilterFunction = this.currentFilter.Water;
            this.options.Weight = 15;
        }

        public void WeightChangedRedo()
        {
            if (this.currentFilterFunction == null)
            {
                MessageBox.Show("Select filter first");
                return;
            }
            this.FilterModel();
        }

        public void ZoomChanged()
        {
            this.view.Zoom = this.options.Zoom;
            this.view.Bitmap = this.model.Bitmap;
        }

        public void showUndoStack()
        {
            this.commonControls.listView.LargeImageList.Images.Clear();
            this.commonControls.listView.Items.Clear();
            for (int i = 0; i < this.undoList.Count; i++)
            {
                this.commonControls.listView.LargeImageList.Images.Add(this.undoList[i].Bitmap);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                this.commonControls.listView.Items.Add(item);
            }

            this.commonControls.listView.Items.Add("--Redo--");

            for (int i = 0; i < this.redoListe.Count; i++)
            {
                this.commonControls.listView.LargeImageList.Images.Add(this.redoListe[i].Bitmap);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = i + this.undoList.Count;
                this.commonControls.listView.Items.Add(item);
            }

        }

        public void DoAction(string name)
        {
            undoList.Push(new Action((Bitmap)this.model.Bitmap.Clone(), name));
            this.showUndoStack();
            this.redoListe.Clear();
        }

        public void UndoAction()
        {
            Action a = undoList.Pop();
            if (a == null)
                return;
            redoListe.Push(new Action((Bitmap)this.model.Bitmap.Clone(), a.Name));
            this.showUndoStack();
            this.model.Bitmap = a.Bitmap;
            this.view.Bitmap = this.model.Bitmap;

        }

        public void RedoAction()
        {
            Action a = redoListe.Pop();
            if (a == null)
                return;
            undoList.Push(new Action((Bitmap)this.model.Bitmap.Clone(), a.Name));
            this.showUndoStack();
            this.model.Bitmap = a.Bitmap;
            this.view.Bitmap = this.model.Bitmap;

        }

        public void ShiftAndScale()
        {
            this.DoAction("ShiftAndScale");
            this.ReloadImage();
            this.currentFilter.ShiftAndScale(this.model.Bitmap, new FilterOptions(this.options.ShiftAndScaleOptions));
            this.currentFilterFunction = this.currentFilter.ShiftAndScale;
            this.view.Bitmap = this.model.Bitmap;
        }

    }
}
