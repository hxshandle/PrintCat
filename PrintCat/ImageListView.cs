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
      spyer.ImageListView = this;
    }


    public delegate void DeleFunc(String path);
    
    private void _add(String path)
    {
      Image img = new Image();
      BitmapImage bitImg = new BitmapImage(new Uri(path));
      img.Source = bitImg;
      img.Width = 64;
      img.Height = 64;
      this.lstImage.Items.Add(img);
      //lstImage.SelectedItem = lstImage.Items.GetItemAt(lstImage.Items.Count - 1);
      lstImage.Items.MoveCurrentToFirst();
    }


    public void AddImage(String path)
    {
      var searchPattern = new Regex(@"$(?<=\.(png|jpg))", RegexOptions.IgnoreCase);
      FileInfo f = new FileInfo(path);
      if (searchPattern.IsMatch(f.Name))
      {
        //_add(f.FullName);
        System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                        new DeleFunc(_add), f.FullName);
      }
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
          //Console.WriteLine("image -> " + f.FullName);
          _add(f.FullName);
          
        }
      }
    }


  }
}
