using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrintCat
{
  class FolderSpy
  {
    private List<FileInfo> imageList = new List<FileInfo>();

    static String spyPath = "g:\\tmp";


    public FolderSpy()
    {
      FileSystemWatcher fsw = new FileSystemWatcher();
      fsw.Path = spyPath;
      fsw.Filter = "*.png|*.jpeg|*.jpg";
    }


  }
}
