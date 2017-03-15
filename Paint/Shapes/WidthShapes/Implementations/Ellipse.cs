using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes.Implementations
{
    [DataContract]
    class Ellipse : WidthShape
    {
        public Ellipse(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = Width,
                Height = Height,
                Fill = Fill,
                RenderTransform = new RotateTransform() {Angle = Angle},
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            ellipse.SetValue(Canvas.LeftProperty, X);
            ellipse.SetValue(Canvas.TopProperty, Y);
            return ellipse;
        }
    }
}
