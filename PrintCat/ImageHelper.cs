using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PrintCat
{

  public static class ImageHelper
  {
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);
    public static BitmapSource ConvertBitmap(Bitmap source)
    {
      IntPtr hBitmap = source.GetHbitmap();
      BitmapSource ret = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

      DeleteObject(hBitmap);
      return ret;
    }

    public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
    {
      Bitmap bitmap;
      using (var outStream = new MemoryStream())
      {
        BitmapEncoder enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bitmapsource));
        enc.Save(outStream);
        bitmap = new Bitmap(outStream);
      }
      return bitmap;
    }
  }
}
