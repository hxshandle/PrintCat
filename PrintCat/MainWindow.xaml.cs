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
        theImageControl.DisplayImage(image.Source as BitmapSource);
      }
      catch (Exception ex)
      {
        return;
      }
    }



    private void lightLess_Click(object sender, RoutedEventArgs e)
    {
      theImageControl.Lightness(-1);
    }

    private void lightMore_Click(object sender, RoutedEventArgs e)
    {
      theImageControl.Lightness(1);
    }

    private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      theImageControl.Resize();
    }

  }
}
