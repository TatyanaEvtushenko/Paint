using System.Windows.Media;

namespace Paint
{
    abstract class WidthShape : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Angle { get; set; }

        public WidthShape(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness) 
            : base(fill, stroke, strokeThickness)
        {
            Width = width;
            Height = height;
            Angle = angle;
        }
    }

    abstract class WidthShapeDrawer
    {
        public abstract Shape Create(int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness);
    }
}
