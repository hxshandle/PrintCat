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
    private string[] extensions = { ".jpg", ".png", ".jpeg" };

    static String spyPath = PrintCat.Properties.Settings.Default.SpyFolder;


    public FolderSpy()
    {
      FileSystemWatcher fsw = new FileSystemWatcher();
      fsw.Path = spyPath;
      fsw.EnableRaisingEvents = true;                                                     
      fsw.Created += new FileSystemEventHandler(this.fileSystemWatcher_EventHandle);
      Console.WriteLine("Folder Syper created...");
    }



    void fileSystemWatcher_EventHandle(object sender, FileSystemEventArgs e)
    {
      var ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();
      if (extensions.Any(ext.Equals))
      {
        Console.WriteLine("new file created");
      }
      
    }
  }
}
