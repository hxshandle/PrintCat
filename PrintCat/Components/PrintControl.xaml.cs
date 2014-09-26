﻿using PrintCat.Utils;
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

namespace PrintCat.Components
{
  /// <summary>
  /// Interaction logic for PrintControl.xaml
  /// </summary>
  public partial class PrintControl : UserControl
  {
    public PrintControl()
    {
      InitializeComponent();
    }

    private void Print_Image(object sender, RoutedEventArgs e)
    {
      PrintCatImage.PrintImage();
    }
  }
}
