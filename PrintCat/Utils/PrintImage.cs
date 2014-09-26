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
       PrintDialog printDialog = new PrintDialog();
      if (printDialog.ShowDialog() == true)
      {
        printDialog.PrintVisual(ImageCtrl, "My First Print Job");
      }
    }
  }
}
