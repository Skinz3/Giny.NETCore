using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Giny.SpellTree.Graphics
{
    public class Popup
    {
        private Canvas CanvasPopup
        {
            get;
            set;
        }
        private Border BorderPopup
        {
            get;
            set;
        }

        public Popup()
        {
            CanvasPopup = new Canvas();


            BorderPopup = new Border();

            BorderPopup.Width = 200;
            BorderPopup.Height = 100;
            BorderPopup.Background = new SolidColorBrush(Node.NodeDefaultColor);

            // Set the padding on the container
            BorderPopup.Padding = new Thickness(10); // Adjust the padding value as needed
            BorderPopup.CornerRadius = new CornerRadius(8);                      // Add the canvas as a child of the container
            BorderPopup.Child = CanvasPopup;
        }

        public void Open(Canvas canvas, string message, double x, double y)
        {
            Canvas.SetTop(BorderPopup, y);
            Canvas.SetLeft(BorderPopup, x);
            TextBlock textBlock = new TextBlock();

            textBlock.Text = message;
            textBlock.Foreground = Brushes.White;
            textBlock.FontSize = 16;
            textBlock.FontFamily = MainWindow.AppFont;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            var size = CanvasDraw.MeasureTextSize(message, textBlock.FontFamily, textBlock.FontSize, textBlock.FontStyle, textBlock.FontWeight);

            BorderPopup.Width = size.Width + 30;

            CanvasPopup.Width = BorderPopup.Width;

            BorderPopup.Height = size.Height + 30;
            CanvasPopup.Height = BorderPopup.Height;


            CanvasPopup.Children.Add(textBlock);

            canvas.Children.Add(BorderPopup);
            Canvas.SetZIndex(BorderPopup, 10000);

           // CanvasPopup.Opacity = 0.5d;
            BorderPopup.Opacity = 0.8d;


        }

        public void Close(Canvas canvas)
        {
            CanvasPopup.Children.Clear();
            canvas.Children.Remove(BorderPopup);
        }

        public void SetPosition(Canvas canvas, double x, double y)
        {
            Canvas.SetTop(BorderPopup, y);
            Canvas.SetLeft(BorderPopup, x);
        }
    }
}
