using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Ellipse : WidthShape
    {
        public Ellipse(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var ellipse = new library.Ellipse();
            ellipse.Width = Width;
            ellipse.Height = Height;
            ellipse.Fill = Fill;
            ellipse.RenderTransform = new RotateTransform() { Angle = Angle };
            ellipse.Stroke = Stroke;
            ellipse.StrokeThickness = StrokeThickness;
            ellipse.SetValue(Canvas.LeftProperty, X);
            ellipse.SetValue(Canvas.TopProperty, Y);
            return ellipse;
        }
    }

    class EllipseDrawer : WidthShapeDrawer
    {
        public override Shape Create(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new Ellipse(x, y, width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
