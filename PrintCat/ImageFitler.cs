

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

using System.Drawing;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;


namespace PrintCat
{
   
    public class ImageFitler
    {
        private CurrentImageHandler currentImageHandler;

        public ImageFitler(CurrentImageHandler currentImageHandler)
        {
            // TODO: Complete member initialization
            this.currentImageHandler = currentImageHandler;
        }

        public void ExecuteFitler()
        {
            BitmapSource bs = currentImageHandler.CurrentImage.Source as BitmapSource;
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.WhiteSmoke);
            BitmapPalette bp = new BitmapPalette(colors);
            ChannelFiltering filter = new ChannelFiltering();
            filter.Red = new IntRange(100, 255);
            filter.Green = new IntRange(20, 100);
            filter.Blue = new IntRange(1, 100);
            
            //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap;
            //filter.ApplyInPlace(currentImageHandler.CurrentImage.Source);

            System.Windows.MessageBox.Show(bp.ToString());
        }
    }
}
