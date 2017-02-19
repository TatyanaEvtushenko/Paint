using System.Windows;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Rectangle : WidthShape
    {
        public Rectangle(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var rectangle = new library.Ellipse();
            rectangle.Width = Width;
            rectangle.Height = Height;
            rectangle.Fill = Fill;
            rectangle.Stroke = Stroke;
            rectangle.StrokeThickness = StrokeThickness;
            return rectangle;
        }
    }

    class RectangleDrawer : WidthShapeDrawer
    {
        public override Shape Create(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new Rectangle(width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
