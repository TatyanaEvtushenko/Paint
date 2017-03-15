using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes.Implementations
{
    [DataContract]
    class Rectangle : WidthShape
    {
        public Rectangle(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var rectangle = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Fill,
                RenderTransform = new RotateTransform() {Angle = Angle},
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            rectangle.SetValue(Canvas.LeftProperty, X);
            rectangle.SetValue(Canvas.TopProperty, Y);
            return rectangle;
        }
    }
}
