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
    //public Bitmap OriginalBitmap { get; set; }
    public BitmapSource ProcessedBitmapSource { get; set; }
    public System.Windows.Controls.Image ImageCtrl { get; set; }


    public PrintCatImageImplement(BitmapSource source, System.Windows.Controls.Image image)
    {
      this.OriginalBitmapSource = source;
      this.ProcessedBitmapSource = source;
      this.ImageCtrl = image;
      //this.OriginalBitmap = ImageHelper.BitmapFromSource(source);
      
    }

    internal void ApplyColorBalance(int rValue, int gValue, int bValue)
    {
      Bitmap newBitmap = BitmapColorBalance.ColorBalance(ImageHelper.BitmapFromSource(OriginalBitmapSource), (byte)bValue, (byte)gValue, (byte)rValue);
      BitmapSource ProcessedBitmapSource = ImageHelper.ConvertBitmap(newBitmap);
      ImageCtrl.Source = ProcessedBitmapSource;
    }

    internal void ShowOriginalImage()
    {
      ImageCtrl.Source = OriginalBitmapSource;
    }

    internal void PrintImage()
    {
      PrintImage pi = new PrintImage();
      System.Windows.Controls.Image pImage = new System.Windows.Controls.Image();
      pImage.Source = ImageCtrl.Source;
      
      pi.Print(pImage);
    }
  }
}
