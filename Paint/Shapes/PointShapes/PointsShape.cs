using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.Shapes.PointShapes
{
    [DataContract]
    abstract class PointsShape : Shape
    {
        [DataMember]
        public PointCollection Points { get; set; }

        protected PointsShape(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(fill, stroke, strokeThickness)
        {
            Points = new PointCollection();
            for (int i = 0; i < pointsX.Length; i++)
                Points.Add(new Point(pointsX[i], pointsY[i]));
        }
    }
}
