using System.Windows;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Ellipse : WidthShape
    {
        public Ellipse(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness) 
            : base(width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var ellipse = new library.Ellipse();
            ellipse.Width = Width;
            ellipse.Height = Height;
            ellipse.Fill = Fill;
            ellipse.Stroke = Stroke;
            ellipse.StrokeThickness = StrokeThickness;
            return ellipse;
        }
    }

    class EllipseDrawer : WidthShapeDrawer
    {
        public override Shape Create(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new Ellipse(width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
