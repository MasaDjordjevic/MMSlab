using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MMSlab.Models
{
    public class Model : IModel
    {
        
        public Bitmap Bitmap { get; set; }

        public void LoadBitmap(string fileLocation)
        {
            this.Bitmap = (Bitmap)Bitmap.FromFile(fileLocation);
        }

        
    }
}
