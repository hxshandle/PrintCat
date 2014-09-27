using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrintCat.Utils
{
  class PrintImage
  {


    internal void Print(System.Windows.Controls.Image ImageCtrl)
    {
      PrintDialog printDlg = new PrintDialog();

      if (printDlg.ShowDialog() == true)
      {
        printDlg.PrintVisual(ImageCtrl, "My First Print Job");
      }
    }
  }
}
