using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Giny.SpellTree
{
    internal class CanvasDraw
    {
        public static void Triangle(double x, double y, double width, Brush color, Canvas cv)
        {
            Polygon triangle = new Polygon();
            triangle.Points = new PointCollection() { new Point(1 * width, 1 * width), new Point(3 * width, 1 * width), new Point(2 * width, -1 * width) };
            triangle.Fill = color;
            cv.Children.Add(triangle);
            triangle.SetValue(Canvas.LeftProperty, x - (width * 2));
            triangle.SetValue(Canvas.TopProperty, y);
        }
        public static Ellipse Circle(double x, double y, double radius, Brush color, Canvas cv)
        {
            Ellipse circle = new Ellipse()
            {
                Width = radius,
                Height = radius,
                Fill = color,
            };
            cv.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, x - radius / 2);
            circle.SetValue(Canvas.TopProperty, y - radius / 2);

            return circle;
        }

        public static Size MeasureTextSize(string text, FontFamily fontFamily, double fontSize, FontStyle fontStyle, FontWeight fontWeight)
        {
            var formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily, fontStyle, fontWeight, FontStretches.Normal),
                fontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display);

            return new Size(formattedText.Width, formattedText.Height);
        }

        public static TextBlock Text(string content, double x, double y, Brush color, Canvas cv, bool alignCenter = true, double rotation = 0d)
        {
            TextBlock block = new TextBlock();

            


            block.Text = content;
            block.Foreground = color;
            // block.FontWeight = FontWeights.Bold;
            block.FontSize = 16;
            block.TextAlignment = TextAlignment.Center;
            block.FontFamily = MainWindow.AppFont;


            var size = MeasureTextSize(content, block.FontFamily, block.FontSize, block.FontStyle, block.FontWeight);

            cv.Children.Add(block);

            Canvas.SetZIndex(block, 999);

            block.RenderTransform = new RotateTransform(rotation, size.Width / 2, size.Height / 2);

            if (alignCenter)
            {

                block.SetValue(Canvas.LeftProperty, x - size.Width / 2d);
                block.SetValue(Canvas.TopProperty, y - size.Height / 2d);
            }
            else
            {
                block.SetValue(Canvas.LeftProperty, x);
                block.SetValue(Canvas.TopProperty, y);
            }

            block.IsHitTestVisible = false;

            return block;
        }
        public static Line Line(double x1, double y1, double x2, double y2, Brush color, Canvas canvas, double opacity = 1d, double strokeThickness = 1d)
        {
            Line myLine = new Line();
            myLine.Stroke = color;

            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.Opacity = opacity;
            myLine.StrokeThickness = strokeThickness;
            Canvas.SetZIndex(myLine, -1);
            canvas.Children.Add(myLine);
            return myLine;
        }
    }
}
