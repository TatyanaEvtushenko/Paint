using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes
{
    [DataContract]
    abstract class WidthShape : Shape
    {
        public double Width => ShapeForDrawing.Width;
        public double Height => ShapeForDrawing.Height;
        public double X => (double)ShapeForDrawing.GetValue(Canvas.LeftProperty);
        public double Y => (double)ShapeForDrawing.GetValue(Canvas.TopProperty);
    }
}
