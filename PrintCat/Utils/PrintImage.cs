using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrintCat.Utils
{
  class PrintImage
  {


    internal void Print(System.Windows.Controls.Image ImageCtrl)
    {
      PrintDialog printDlg = new PrintDialog();

      if (printDlg.ShowDialog() == true)
      {
        printDlg.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.NorthAmerica4x6);
        printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;

        System.Windows.Size size = new System.Windows.Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
        StackPanel stackPanel = new StackPanel { Orientation = Orientation.Vertical, RenderSize = size };

        //TextBlock title = new TextBlock { Text = @"Form", FontSize = 20 };
        //stackPanel.Children.Add(title);

        TransformGroup transformGroup = new TransformGroup();
        ScaleTransform scaleTransform = new ScaleTransform(1,1);
        transformGroup.Children.Add(scaleTransform);
        
        Image image = new Image { Source = ImageCtrl.Source, Stretch = Stretch.Uniform };
        
        //image.RenderTransform = new ScaleTransform(1, 1);

        BitmapSource imgSrouce = ImageCtrl.Source as BitmapSource;
        if (imgSrouce.PixelWidth < imgSrouce.PixelHeight)
        {
          
          image.LayoutTransform = new RotateTransform(90);
        }

        image.RenderTransform = transformGroup;
        

        stackPanel.Children.Add(image);

        stackPanel.Measure(size);
        stackPanel.Arrange(new Rect(new Point(0, 0), stackPanel.DesiredSize));

        printDlg.PrintVisual(stackPanel, @"Form image");
        //printDlg.PrintVisual(ImageCtrl, "My First Print Job");
      }
    }
  }
}
