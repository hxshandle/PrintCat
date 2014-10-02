using PrintCat.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PrintCat.Utils
{
  public static class PrintCatImage
  {

    private static PrintCatImageImplement PrintCatImageIns;

    public static ColorBalanceControl ColorBalanceCtrl { get; set; }
    public static ImageProjcessControl ImageProgressCtrl { get; set; }
    public static CropImageControl CropImageCtrl { get; set; }
    internal static void ShoworiginalImage()
    {
      PrintCatImageIns.ShowOriginalImage();
    }


    internal static void Init(BitmapSource bitmapSource, System.Windows.Controls.Image image)
    {
      PrintCatImageIns = new PrintCatImageImplement(bitmapSource, image);
    }

    internal static void ApplyColorBalance(int rValue, int gValue, int bValue)
    {
      PrintCatImageIns.ApplyColorBalance((byte)bValue, (byte)gValue, (byte)rValue);
    }

    internal static void PrintImage()
    {
      PrintCatImageIns.PrintImage();
    }
    internal static void updateImageCtrl(System.Windows.Controls.Image img)
    {
      PrintCatImageIns.OriginalBitmapSource = img.Source as BitmapSource;
      PrintCatImageIns.ProcessedBitmapSource = img.Source as BitmapSource; 
    }


    internal static BitmapSource getProcessedImage()
    {
      return PrintCatImageIns.ProcessedBitmapSource;
    }
  }
}
