using System.Runtime.Serialization;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes
{
    [DataContract]
    abstract class WidthShape : Shape
    {
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Angle { get; set; }
        [DataMember]
        public double X { get; set; }
        [DataMember]
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
