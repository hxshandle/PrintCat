

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

  public enum ColorRangeType
  {
    RED,
    GREEN,
    BLUE
  }

  public class ColorFilter
  {
    public IntRange Range { get; set; }
    public ColorRangeType Type { get; set; }


    public ColorFilter(int low, int up, ColorRangeType type)
    {
      Range = new IntRange(low, up);
      this.Type = type;
    }
  }

  public class ImageFitler
  {
    private CurrentImageHandler currentImageHandler;

    public ImageFitler(CurrentImageHandler currentImageHandler)
    {
      // TODO: Complete member initialization
      this.currentImageHandler = currentImageHandler;
    }

    public void ExecuteFitler(IInPlaceFilter filter)
    {
      BitmapSource bitmapSource = currentImageHandler.GetCropedImageSource().Clone();
      Bitmap bitmap = ImageHelper.BitmapFromSource(bitmapSource);

      filter.ApplyInPlace(bitmap);
      BitmapSource newBitmapSouce = ImageHelper.ConvertBitmap(bitmap);
      this.currentImageHandler.CurrentImage.Source = newBitmapSouce;

    }
  }
}
