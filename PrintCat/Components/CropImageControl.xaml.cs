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
  /// Interaction logic for CropImage.xaml
  /// </summary>
  public partial class CropImageControl : UserControl
  {
    public CropImageControl()
    {
      InitializeComponent();
    }

    internal void enableActions()
    {
      crop4x6Btn.IsEnabled = true;
    }

    private void CropBtn_Click(object sender, RoutedEventArgs e)
    {
      PrintCatImage.ImageProgressCtrl.CropCustomlizeImage();
    }

    internal void enableCrop()
    {
      CropBtn.IsEnabled = true;
    }

    internal void disableCrop()
    {
      CropBtn.IsEnabled = false;
    }

    private void crop4x6Btn_Click(object sender, RoutedEventArgs e)
    {
      BitmapSource source = PrintCatImage.getProcessedImage();
      int pw = source.PixelWidth;
      int ph = source.PixelHeight;
      int left = 0;
      int top = 0;
      int width = 0;
      int height = 0;
      //Landscape
      if (pw > ph)
      {
        int w = (int)3 * ph / 2;
        left = (pw - w) / 2;
        width = w;
        height = ph;
      }else{
        int h = (int)3 * pw / 2;
        top = (ph - h) / 2;
        if (h > ph)
        {
          height = ph;
          width = (int)2 * ph / 3;
          top = 0;
          left = (int)(pw - width) / 2;
        }
        else
        {
          width = pw;
          height = h;
        }
        
      }

      CroppedBitmap croppedBitmap = new CroppedBitmap(source, new Int32Rect(left, top, width, height));
      PrintCatImage.ImageProgressCtrl.CropFixedImage(croppedBitmap);
      
    }
  }
}
