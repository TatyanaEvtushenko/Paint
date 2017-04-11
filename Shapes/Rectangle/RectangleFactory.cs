using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shape;
using Shape.Interfaces;

namespace Rectangle
{
    public class RectangleFactory : IShapeFactory
    {
        public Shape.Shape Create(ShapeParams param)
        {
            return new Rectangle(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }

        public System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param)
        {
            var shape = new System.Windows.Shapes.Rectangle
            {
                Width = param.Width,
                Height = param.Height,
                RenderTransform = new RotateTransform { Angle = param.Angle },
                Stroke = new SolidColorBrush(Colors.Black)
            };
            shape.SetValue(Canvas.LeftProperty, param.X);
            shape.SetValue(Canvas.TopProperty, param.Y);
            return shape;
        }

        public System.Windows.Shapes.Shape GetShapeIcon()
        {
            return new System.Windows.Shapes.Rectangle
            {
                Margin = new Thickness(5, 0, 5, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 15,
                Width = 40
            };
        }
    }
}
