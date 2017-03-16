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
    }
}
