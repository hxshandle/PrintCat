using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PrintCat
{

  public class CurrentImageHandler
  {
    //private Image theImage;

    public Image CurrentImage { set; get; }
    public ImageFitler ColorFitler { set; get; }
     
    private String OriginalUri { set; get; }
    private BitmapSource OriginalBitmapSource { get; set; }
    private ImageProjcessControl Control { get; set; }
    public Image CroppedImage { get; set; }




    public CurrentImageHandler(ImageProjcessControl control, String uri)
    {
      // TODO: Complete member initialization
      Control = control;
      ColorFitler = new ImageFitler(this);
      OriginalUri = uri;
      OriginalBitmapSource = new BitmapImage(new Uri(OriginalUri));
      CurrentImage = control.theImage;
      CroppedImage = null;
    }



    internal BitmapSource GetCropedImageSource()
    {
      if (CroppedImage == null)
      {
        Console.WriteLine("Cropped image is null");
        return OriginalBitmapSource;
      }
      Console.WriteLine("using cropped image");
      return CroppedImage.Source as BitmapSource;
    }
  }
}
