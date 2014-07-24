﻿

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

  public class ColorRange
  {
    public IntRange Range { get; set; }
    public ColorRangeType Type { get; set; }


    public ColorRange(int low, int up, ColorRangeType type)
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

    public void ExecuteFitler(List<ColorRange> filters)
    {
      BitmapSource bs = currentImageHandler.CurrentImage.Source as BitmapSource;
      List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
      colors.Add(System.Windows.Media.Colors.WhiteSmoke);
      BitmapPalette bp = new BitmapPalette(colors);
      ChannelFiltering filter = new ChannelFiltering();
      foreach (ColorRange f in filters)
      {
        if (f.Type == ColorRangeType.RED)
        {
          filter.Red = f.Range;
        }
        if (f.Type == ColorRangeType.GREEN)
        {
          filter.Green = f.Range;
        }
        if (f.Type == ColorRangeType.BLUE)
        {
          filter.Blue = f.Range;
        }
      }
      //filter.Red = new IntRange(100, 255);
      //filter.Green = new IntRange(20, 100);
      //filter.Blue = new IntRange(1, 100);
      Bitmap bitmap = ImageHelper.BitmapFromSource(currentImageHandler.CurrentImage.Source as BitmapSource);

      filter.ApplyInPlace(bitmap);
      BitmapSource newBitmapSouce = ImageHelper.ConvertBitmap(bitmap);
      this.currentImageHandler.CurrentImage.Source = newBitmapSouce;
    }
  }
}
