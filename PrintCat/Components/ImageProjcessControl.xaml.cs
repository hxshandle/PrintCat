using AForge.Imaging.Filters;
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

  public class ImageDimension
  {
    public double width { get; set; }
    public double height { get; set; }
    // if isLandscape is true means the width is full.
    public bool isLandscape { get; set; }
    public double left { get; set; }
    public double top { get; set; }

    public ImageDimension(double w, double h, double l, double t, bool isL)
    {
      this.width = w;
      this.height = h;
      this.left = l;
      this.top = t;
      this.isLandscape = isL;
    }
  }
  /// <summary>
  /// Interaction logic for ImageProjcessControl.xaml
  /// </summary>
  public partial class ImageProjcessControl : UserControl
  {
    public ImageProjcessControl()
    {
      InitializeComponent();
    }
    public bool HasImage { get { return this.hasImage; } }
    AdornerLayer aLayer;
    bool hasImage = false;
    bool mouseDown = false; // Set to 'true' when mouse is held down.
    Point mouseDownPos; // The point where the mouse button was clicked down.
    bool isMoveRectangle = false;
    Point selectBoxPos = new Point();
    ImageDimension imageDimension = null;
    CurrentImageHandler currentImageHandler = null;
    int brightnessAdjValue = 10;


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
      if (!hasImage) return;
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
      Dictionary<String, double> canvasDim = _getCanvasDim();



      //double _w = canvasDim["Width"] - offsetX;
      //double _h = canvasDim["Height"] - offsetY;
      double _leftEdgeX = imageDimension.left;
      double _topEdgeY = imageDimension.top;
      double _rightEdgeX = imageDimension.left + imageDimension.width - offsetX;
      double _botttomEdgeY = imageDimension.top + imageDimension.height - offsetY;
      pos.Y = pos.Y > _botttomEdgeY ? _botttomEdgeY : pos.Y;
      pos.Y = pos.Y < _topEdgeY ? _topEdgeY : pos.Y;
      pos.X = pos.X < _leftEdgeX ? _leftEdgeX : pos.X;
      pos.X = pos.X > _rightEdgeX ? _rightEdgeX : pos.X;
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

    internal void clearSelectBox()
    {
      selectionBox.Width = 0;
      selectionBox.Height = 0;
      selectionBox.Visibility = Visibility.Collapsed;
    }

    private Dictionary<String, double> _getCanvasDim()
    {
      Dictionary<String, double> result = new Dictionary<String, double>();
      result.Add("Width", theCanvas.ActualWidth);
      result.Add("Height", theCanvas.ActualHeight);
      return result;

    }

    public void Resize()
    {
      if (hasImage)
      {
        this.DisplayImage(theImage.Source as BitmapSource);
      }
    }

    public void DisplayImage(BitmapSource source)
    {
      //BitmapSource bitmapImage = new BitmapImage(new Uri(uri));
      setDisplayImage(source);
      hasImage = true;
      brightnessAdjValue = 10;
      currentImageHandler = new CurrentImageHandler(this, source);
    }


    internal void setDisplayImage(BitmapSource bitmapImage)
    {
      double _imageW = bitmapImage.Width;
      double _imageH = bitmapImage.Height;
      theCanvas.Cursor = Cursors.Cross;
      Dictionary<String, double> canvasDim = _getCanvasDim();
      imageDimension = getImageDim(_imageW, _imageH);
      Console.WriteLine(String.Format("Display image {0}X{1}", _imageW, _imageH));
      theImage.Source = bitmapImage;
      theImage.Width = imageDimension.width;
      theImage.Height = imageDimension.height;
      Canvas.SetLeft(theImage, imageDimension.left);
      Canvas.SetTop(theImage, imageDimension.top);


    }

    private ImageDimension getImageDim(double imageWidth, double ImageHeight)
    {
      Dictionary<String, double> canvasDim = _getCanvasDim();
      ImageDimension result = null;
      double canvasWidth = canvasDim["Width"];
      double canvasHeight = canvasDim["Height"];
      if (imageWidth > ImageHeight)
      {
        // 水平照片
        double newHeight = canvasWidth * ImageHeight / imageWidth;
        result = new ImageDimension(canvasWidth, newHeight, 0, (canvasHeight - newHeight) / 2, true);
        if (newHeight > canvasHeight)
        {
          double newWidth = canvasHeight * imageWidth / ImageHeight;
          // 这里还是垂直照片
          result = new ImageDimension(newWidth, canvasHeight, (canvasWidth - newWidth) / 2, 0, false);
        }

      }
      else
      {
        // 垂直照片
        double newWidth = canvasHeight * imageWidth / ImageHeight;
        result = new ImageDimension(newWidth, canvasHeight, (canvasWidth - newWidth) / 2, 0, false);
      }
      return result;
    }

    //点击剪裁
    private void btn_crop_Click(object sender, RoutedEventArgs e)
    {

      BitmapSource bitmapSource = currentImageHandler.GetCropedImageSource();
      Console.WriteLine(String.Format("Croped image size pixel-{0}X{1} ,size-{2}X{3} ", bitmapSource.PixelWidth, bitmapSource.PixelHeight, bitmapSource.Width, bitmapSource.Height));
      double scaleX = bitmapSource.PixelWidth / imageDimension.width;
      double scaleY = bitmapSource.PixelHeight / imageDimension.height;
      int width = (int)(selectionBox.Width * scaleX);
      int height = (int)(selectionBox.Height * scaleY);
      double selectBoxLeft = Canvas.GetLeft(selectionBox);
      double selectBoxTop = Canvas.GetTop(selectionBox);
      int left = (int)((selectBoxLeft - imageDimension.left) * scaleX);
      int top = (int)((selectBoxTop - imageDimension.top) * scaleY);
      Console.WriteLine(String.Format("Cropped image size is {0}X{1}", width, height));

      CroppedBitmap croppedBitmap = new CroppedBitmap(bitmapSource, new Int32Rect(left, top, width, height));
      Image croppedImage = new Image();
      croppedImage.Source = croppedBitmap;
      currentImageHandler.CroppedImage = croppedImage;
      setDisplayImage(croppedBitmap);
      clearSelectBox();

    }

    private void Button_Filter_Click(object sender, RoutedEventArgs e)
    {
      //currentImageHandler.ColorFitler.ExecuteFitler();
    }

    internal void ExceRGBFilter(ChannelFiltering filter)
    {
      if (currentImageHandler != null)
      {
        currentImageHandler.ColorFitler.ExecuteFitler(filter);
      }
    }

    internal void Lightness(int p)
    {
      if (!this.HasImage)
      {
        return;
      }
      brightnessAdjValue += p * 5;
      //brightnessAdjValue = brightnessAdjValue > 1 ? 1 : brightnessAdjValue;
      //brightnessAdjValue = brightnessAdjValue < -1 ? -1 : brightnessAdjValue;
      //BrightnessCorrection
      Console.WriteLine("brightnessAdjValue->" + brightnessAdjValue);
      BrightnessCorrection bc = new BrightnessCorrection(brightnessAdjValue);
      currentImageHandler.ColorFitler.ExecuteFitler(bc);

      
        
    }
  }
}
