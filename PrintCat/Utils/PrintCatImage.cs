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
  }
}
