using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PrintCat
{
  class ImageListView
  {
    private List<Image> imageList = new List<Image>();
    FolderSpy spyer = new FolderSpy();
    private ListView lstImage;

    public ImageListView(ListView lstImage)
    {
      // TODO: Complete member initialization
      this.lstImage = lstImage;
      this.initImageList();
    }

    private void initImageList()
    {
      var searchPattern = new Regex(@"$(?<=\.(png|jpg))", RegexOptions.IgnoreCase);
      DirectoryInfo fdir = new DirectoryInfo(PrintCat.Properties.Settings.Default.SpyFolder);
      FileInfo[] images = fdir.GetFiles("*.*", SearchOption.AllDirectories);
      //var imageFiles = Directory.GetFiles(PrintCat.Properties.Settings.Default.SpyFolder).Where(f => searchPattern.IsMatch(f));
      foreach (FileInfo f in images)
      {
        if (searchPattern.IsMatch(f.Name))
        {
          Console.WriteLine("image -> " + f.FullName);
         
          Image img = new Image();
          BitmapImage bitImg = new BitmapImage(new Uri(f.FullName));

          img.Source = bitImg;

          img.Width = 64;
          img.Height = 64;
          this.lstImage.Items.Add(img);
        }
      }
    }
  }
}
