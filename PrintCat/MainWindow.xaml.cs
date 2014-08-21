using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Controls.Primitives;
using AForge.Imaging.Filters;
using PrintCat.Components;
using PrintCat.Utils;

namespace PrintCat
{
  /// <summary>
  /// MainWindow.xaml 的交互逻辑
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
      ImageListView imageListView = new ImageListView(lstImage);


    }

    void list_image_item_double_click(object sender, MouseButtonEventArgs e)
    {

      try
      {
        Image image = (Image)e.OriginalSource;
        String source = image.Source.ToString();

        PrintCatImage.Init(image.Source as BitmapSource, theImageControl.theImage);
        theImageControl.clearSelectBox();
        //theImageControl.setDisplayImage(new BitmapImage(new Uri(source)));
        theImageControl.DisplayImage(source);
      }
      catch (Exception ex)
      {
        return;
      }
    }

    private void ApplayRGBFilter(String type, RoutedPropertyChangedEventArgs<double> e)
    {
      if (!theImageControl.HasImage)
      {
        return;
      }
      int OldValue = (int)e.OldValue;
      int NewValue = (int)e.NewValue;
      if (OldValue == NewValue) return;
      ChannelFiltering filter = new ChannelFiltering();
      filter.Red = new AForge.IntRange(0, (int)RSlider.Value);
      filter.Green = new AForge.IntRange(0, (int)GSlider.Value);
      filter.Blue = new AForge.IntRange(0, (int)BSlider.Value);
      theImageControl.ExceRGBFilter(filter);
    }

    private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      ApplayRGBFilter("R", e);
    }

    private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      ApplayRGBFilter("G", e);
    }

    private void BSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      ApplayRGBFilter("B", e);
    }

    private void lightLess_Click(object sender, RoutedEventArgs e)
    {
      theImageControl.Lightness(-1);
    }

    private void lightMore_Click(object sender, RoutedEventArgs e)
    {
      theImageControl.Lightness(1);
    }
  }
}
