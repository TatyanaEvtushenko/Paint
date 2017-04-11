using System.Runtime.Serialization;
using System.Windows.Media;
using Shape.Heirs;
using Shape.Interfaces;

namespace Polyline
{
    [DataContract]
    class Polyline : PointsShape, ISelectable, IEditable
    {
        public Polyline(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        public override System.Windows.Shapes.Shape CreateShapeForDrawing()
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
