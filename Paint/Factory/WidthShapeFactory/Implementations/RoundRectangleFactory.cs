using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class RoundRectangleFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new RoundRectangle(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }

        public FrameworkElement CreateShapeForDrawing(Point lastPoint, Point newPoint)
        {
            var shape = new System.Windows.Shapes.Rectangle
            {
                Width = Math.Abs(newPoint.X - lastPoint.X),
                Height = Math.Abs(newPoint.Y - lastPoint.Y),
                Stroke = new SolidColorBrush(Colors.Black)
            };
            shape.SetValue(Canvas.LeftProperty, lastPoint.X);
            shape.SetValue(Canvas.TopProperty, lastPoint.Y);
            shape.RadiusX = shape.RadiusY = (shape.Height < shape.Width ? shape.Height : shape.Width) / 10;
            return shape;
        }
    }
}
