using System.Windows.Media;

namespace Paint.Shapes.WidthShapes
{
    abstract class WidthShape : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Angle { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        protected WidthShape(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness) 
            : base(fill, stroke, strokeThickness)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
        }
    }
}
