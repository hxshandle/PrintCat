using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrintCat.Components
{
  class SliderColorProperties
  {
    public static readonly DependencyProperty LeftColorProperty =
        DependencyProperty.RegisterAttached(
            "LeftColor",
            typeof(String),
            typeof(SliderColorProperties));

    public static readonly DependencyProperty RightColorProperty =
        DependencyProperty.RegisterAttached(
            "RightColor",
            typeof(String),
            typeof(SliderColorProperties));

    public static String GetLeftColor(DependencyObject obj)
    {
      return (String)obj.GetValue(LeftColorProperty);
    }

    public static void SetLeftColor(DependencyObject obj, String value)
    {
      obj.SetValue(LeftColorProperty, value);
    }

    public static String GetRightColor(DependencyObject obj)
    {
      return (String)obj.GetValue(RightColorProperty);
    }

    public static void SetRightColor(DependencyObject obj, String value)
    {
      obj.SetValue(RightColorProperty, value);
    }
  }
}
