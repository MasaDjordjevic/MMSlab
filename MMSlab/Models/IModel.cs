using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Models
{
    public interface IModel
    {
        void SetBitmap(string fileLocation);
        System.Drawing.Bitmap GetBitmap();
    }
}
