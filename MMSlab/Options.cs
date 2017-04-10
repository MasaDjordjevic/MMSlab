using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab
{
    public class Options
    {
        private int weight;
        public int Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
                this.controller.WeightChangedRedo();
            }
        }

        private double zoom;
        public double Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                this.zoom = value;
                this.controller.ZoomChanged();
            }
        }


        private bool coreMode = true;
        public bool CoreMode
        {
            get
            {
                return this.coreMode;
            }
            set
            {
                this.coreMode = value;
                this.controller.SetMode();
            }
        }
        private Controllers.Controller controller;
        
        public ShiftAndScaleOptions ShiftAndScaleOptions { get; set;}

        public Options(Controllers.Controller controller)
        {
            this.controller = controller;
            this.ShiftAndScaleOptions = new ShiftAndScaleOptions();
        }
    }
}
