using System.Windows;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class RoundRectangle : Rectangle
    {
        public RoundRectangle(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var roundRectangle = (library.Rectangle)base.CreateShapeForDrawing();
            roundRectangle.RadiusX = roundRectangle.RadiusY =  Height < Width ? Height / 10 : Width / 10;
            return roundRectangle;
        }
    }

    class RoundRectangleDrawer : WidthShapeDrawer
    {
        public override Shape Create(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new RoundRectangle(x, y, width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
