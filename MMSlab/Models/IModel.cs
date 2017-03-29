using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Models
{
    public interface IModel
    {
        System.Drawing.Bitmap Bitmap { get; set; }
        long FileSize { get; set; }
        string FileLocation { get; set; }
        void LoadBitmap(string fileLocation);
        
    }
}
