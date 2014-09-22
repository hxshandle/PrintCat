using PrintCat.Utils;
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
  /// Interaction logic for ColorBalanceControl.xaml
  /// </summary>
  public partial class ColorBalanceControl : UserControl
  {
    public ColorBalanceControl()
    {
      InitializeComponent();
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (e.OldValue == e.NewValue)
      {
        return;
      }
      int rValue = (int)RSlider.Value;
      int gValue = (int)GSlider.Value;
      int bValue = (int)BSlider.Value;
      float adjRValue = 0f;
      float adjGValue = 0f;
      float adjBValue = 0f;

      if (rValue < 0)
      {
        adjBValue += -rValue;
        adjGValue += -rValue;
      }
      else
      {
        adjRValue += rValue;
      }
      if (gValue < 0)
      {
        adjRValue += -gValue;
        adjBValue += -gValue;
      }
      else
      {
        adjGValue += gValue;
      }
      if (bValue < 0)
      {
        adjRValue += -bValue;
        adjGValue += -bValue;
      }
      else
      {
        adjBValue += bValue;
      }

      int adjRValueP = (int)Math.Round(255 * (1 - adjRValue / 100));
      int adjGValueP = (int)Math.Round(255 * (1 - adjGValue / 100));
      int adjBValueP = (int)Math.Round(255 * (1 - adjBValue / 100));

      if (rValue == 0 && gValue == 0 && bValue == 0)
      {
        PrintCatImage.ShoworiginalImage();
      }
      else
      {
        PrintCatImage.ApplyColorBalance(adjRValueP, adjGValueP, adjBValueP);
      }
    }
  }
}
