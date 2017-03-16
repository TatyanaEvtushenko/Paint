using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes
{
    [DataContract]
    abstract class WidthShape : Shape
    {
        [DataMember]
        public double Width { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Angle { get; set; }
        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }

        protected WidthShape(double x, double y, double width, double height, double angle, Brush fill, Brush stroke, double strokeThickness)
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
