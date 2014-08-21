using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PrintCat.Utils
{
  class PrintCatImageImplement
  {
    public BitmapSource OriginalBitmapSource { get; set; }
    public Bitmap OriginalBitmap { get; set; }
    public System.Windows.Controls.Image ImageCtrl { get; set; }


    public PrintCatImageImplement(BitmapSource source, System.Windows.Controls.Image image)
    {
      this.OriginalBitmapSource = source;
      this.ImageCtrl = image;
      this.OriginalBitmap = ImageHelper.BitmapFromSource(source);
      
    }

    internal void ApplyColorBalance(int rValue, int gValue, int bValue)
    {
      Bitmap newBitmap = BitmapColorBalance.ColorBalance(OriginalBitmap, (byte)bValue, (byte)gValue, (byte)rValue);
      BitmapSource newSource = ImageHelper.ConvertBitmap(newBitmap);
      ImageCtrl.Source = newSource;
    }

    internal void ShowOriginalImage()
    {
      ImageCtrl.Source = OriginalBitmapSource;
    }
  }
}
