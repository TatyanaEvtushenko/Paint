using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes.Implementations
{
    [DataContract]
    class Polyline : PointsShape
    {
        public Polyline(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
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
