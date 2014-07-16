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

namespace PrintCat
{
  /// <summary>
  /// Interaction logic for ImageProjcessControl.xaml
  /// </summary>
  public partial class ImageProjcessControl : UserControl
  {
    public ImageProjcessControl()
    {
      InitializeComponent();
    }
    AdornerLayer aLayer;

    bool mouseDown = false; // Set to 'true' when mouse is held down.
    Point mouseDownPos; // The point where the mouse button was clicked down.
    bool isMoveRectangle = false;
    Point selectBoxPos = new Point();

    private bool isRectangleMove(MouseButtonEventArgs e)
    {
      bool result = false;
      Point _point = e.GetPosition(theCanvas);
      if (selectionBox.Visibility == Visibility.Visible)
      {
        double _left = Canvas.GetLeft(selectionBox);
        double _top = Canvas.GetTop(selectionBox);
        double _width = selectionBox.Width;
        double _height = selectionBox.Height;
        if (_point.X > _left && _point.X < _left + _width && _point.Y > _top && _point.Y < _top + _height)
        {
          result = true;
        }
      }

      return result;
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      // Capture and track the mouse.
      isMoveRectangle = isRectangleMove(e);
      mouseDown = true;
      mouseDownPos = e.GetPosition(theCanvas);
      theCanvas.CaptureMouse();
      if (!isMoveRectangle)
      {
        // Initial placement of the drag selection box.         
        Canvas.SetLeft(selectionBox, mouseDownPos.X);
        Canvas.SetTop(selectionBox, mouseDownPos.Y);
        selectionBox.Width = 0;
        selectionBox.Height = 0;

        // Make the drag selection box visible.
        selectionBox.Visibility = Visibility.Visible;
      }


    }

    private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
    {
      // Release the mouse capture and stop tracking it.
      mouseDown = false;
      theCanvas.ReleaseMouseCapture();

      // Hide the drag selection box.
      //selectionBox.Visibility = Visibility.Collapsed;
      aLayer = AdornerLayer.GetAdornerLayer(selectionBox);
      aLayer.Add(new ResizingAdorner(selectionBox));

      Point mouseUpPos = e.GetPosition(theCanvas);
      double _x = Canvas.GetLeft(selectionBox);
      double _y = Canvas.GetTop(selectionBox);
      selectBoxPos = new Point(_x, _y);



      // TODO: 
      //
      // The mouse has been released, check to see if any of the items 
      // in the other canvas are contained within mouseDownPos and 
      // mouseUpPos, for any that are, select them!
      //
    }

    private void _drawRectangle(Point mousePos)
    {
      if (mouseDownPos.X < mousePos.X)
      {
        Canvas.SetLeft(selectionBox, mouseDownPos.X);
        selectionBox.Width = mousePos.X - mouseDownPos.X;
      }
      else
      {
        Canvas.SetLeft(selectionBox, mousePos.X);
        selectionBox.Width = mouseDownPos.X - mousePos.X;
      }

      if (mouseDownPos.Y < mousePos.Y)
      {
        Canvas.SetTop(selectionBox, mouseDownPos.Y);
        selectionBox.Height = mousePos.Y - mouseDownPos.Y;
      }
      else
      {
        Canvas.SetTop(selectionBox, mousePos.Y);
        selectionBox.Height = mouseDownPos.Y - mousePos.Y;
      }
    }

    private void _moveRectangle(Point mousePos)
    {
      double _moveX = mousePos.X - mouseDownPos.X;
      double _moveY = mousePos.Y - mouseDownPos.Y;
      double _left = selectBoxPos.X + _moveX;
      double _top = selectBoxPos.Y + _moveY;
      Point _adjPos = _adjustMousePos(new Point(_left, _top), selectionBox.Width, selectionBox.Height);
      Canvas.SetLeft(selectionBox, _adjPos.X);
      Canvas.SetTop(selectionBox, _adjPos.Y);

    }

    private Point _adjustMousePos(Point pos, double offsetX = 0, double offsetY = 0)
    {
      double _w = theCanvas.ActualWidth - offsetX;
      double _h = theCanvas.ActualHeight - offsetY;
      pos.Y = pos.Y > _h ? _h : pos.Y;
      pos.Y = pos.Y < 0 ? 0 : pos.Y;
      pos.X = pos.X < 0 ? 0 : pos.X;
      pos.X = pos.X > _w ? _w : pos.X;
      return pos;
    }

    private void Grid_MouseMove(object sender, MouseEventArgs e)
    {
      if (mouseDown)
      {
        // When the mouse is held down, reposition the drag selection box.

        Point mousePos = e.GetPosition(theCanvas);
        mousePos = _adjustMousePos(mousePos);
        if (!isMoveRectangle)
        {
          _drawRectangle(mousePos);
        }
        else
        {
          _moveRectangle(mousePos);
        }

      }
    }
  }
}
