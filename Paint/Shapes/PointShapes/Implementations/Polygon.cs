using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes.Implementations
{
    [DataContract]
    class Polygon : PointsShape
    {
        public Polygon(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        protected override System.Windows.Shapes.Shape CreateShapeForDrawing()
        {
            return new System.Windows.Shapes.Polygon
            {
                Fill = Fill,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Points = Points
            };
        }
    }
}
