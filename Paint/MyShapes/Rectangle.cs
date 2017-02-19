using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Rectangle : WidthShape
    {
        public Rectangle(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var rectangle = new library.Ellipse();
            rectangle.Width = Width;
            rectangle.Height = Height;
            rectangle.Fill = Fill;
            rectangle.Stroke = Stroke;
            rectangle.StrokeThickness = StrokeThickness;
            rectangle.SetValue(Canvas.LeftProperty, X);
            rectangle.SetValue(Canvas.TopProperty, Y);
            return rectangle;
        }
    }

    class RectangleDrawer : WidthShapeDrawer
    {
        public override Shape Create(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new Rectangle(x, y, width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
