using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrintCat
{

    public class CurrentImageHandler
    {
        //private Image theImage;

        public Image CurrentImage { set; get; }
        public ImageFitler ColorFitler { set; get; }


        public CurrentImageHandler(Image theImage)
        {
            // TODO: Complete member initialization
            CurrentImage = theImage;
            ColorFitler = new ImageFitler(this);
        }

    }
}
