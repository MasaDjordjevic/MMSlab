using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MMSlab.Models
{
    public class Model
    {
        Bitmap bitmap;

        public void SetBitmap(string fileLocation)
        {
            this.bitmap = (Bitmap)Bitmap.FromFile(fileLocation);
        }

        public Bitmap GetBitmap()
        {
            return this.bitmap;
        }
    }
}
