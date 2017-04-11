using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shape;
using Shape.Interfaces;

namespace RoundRectangle
{
    public class RoundRectangleFactory : IShapeFactory
    {
        public Shape.Shape Create(ShapeParams param)
        {
            return new RoundRectangle(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
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
            shape.RadiusX = shape.RadiusY = (shape.Height < shape.Width ? shape.Height : shape.Width) / 10;
            return shape;
        }

        public System.Windows.Shapes.Shape GetShapeIcon()
        {
            return new System.Windows.Shapes.Rectangle
            {
                Margin = new Thickness(5, 0, 5, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 15,
                Width = 40,
                RadiusX = 3,
                RadiusY = 3
            };
        }
    }
}
