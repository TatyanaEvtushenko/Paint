using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes.Implementations
{
    [DataContract]
    class RoundRectangle : WidthShape
    {
        public RoundRectangle(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var roundRectangle = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Fill,
                RenderTransform = new RotateTransform() { Angle = Angle },
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            roundRectangle.SetValue(Canvas.LeftProperty, X);
            roundRectangle.SetValue(Canvas.TopProperty, Y);
            roundRectangle.RadiusX = roundRectangle.RadiusY =  Height < Width ? Height / 10 : Width / 10;
            return roundRectangle;
        }
    }
}
