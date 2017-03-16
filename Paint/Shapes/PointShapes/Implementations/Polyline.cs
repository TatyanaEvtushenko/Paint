using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes.Implementations
{
    [DataContract]
    class Polyline : PointsShape
    {
        public Polyline(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        protected override System.Windows.Shapes.Shape CreateShapeForDrawing()
        {
            return new System.Windows.Shapes.Polyline
            {
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Points = Points
            };
        }
    }
}
