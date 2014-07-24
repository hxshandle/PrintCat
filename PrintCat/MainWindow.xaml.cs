﻿using System;
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
        theImageControl.clearSelectBox();
        theImageControl.setDisplayImage(new BitmapImage(new Uri(source)));
      }
      catch (Exception ex)
      {
        return;
      }
    }

    private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      int OldValue = (int)e.OldValue;
      int NewValue = (int)e.NewValue;
      if (OldValue == NewValue) return;
      Console.WriteLine(String.Format("current value is {0} -> {1}", (int)e.OldValue, (int)e.NewValue));
      ColorRange Rrange = new ColorRange(0, NewValue, ColorRangeType.RED);
      List<ColorRange> filters = new List<ColorRange>();
      filters.Add(Rrange);
      theImageControl.ExceRGBFilter(filters);
    }
  }
}
