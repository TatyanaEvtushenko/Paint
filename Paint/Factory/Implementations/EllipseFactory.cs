using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class EllipseFactory :  IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Ellipse(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }

        public System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param)
        {
            var shape = new System.Windows.Shapes.Ellipse
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
    }
}
