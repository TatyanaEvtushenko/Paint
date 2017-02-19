using System.Windows;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class RoundRectangle : Rectangle
    {
        public RoundRectangle(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
            : base(width, height, angle, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var roundRectangle = (library.Rectangle)base.CreateShapeForDrawing();
            roundRectangle.RadiusX = roundRectangle.RadiusY = Width % 10;
            return roundRectangle;
        }
    }

    class RoundRectangleDrawer : WidthShapeDrawer
    {
        public override Shape Create(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness)
        {
            return new RoundRectangle(width, height, angle, fill, stroke, strokeThickness);
        }
    }
}
