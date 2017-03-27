﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSlab.Views
{
    public interface IView
    {
        System.Drawing.Bitmap Bitmap { get; set; }
        void BringToFront();
    }
}
