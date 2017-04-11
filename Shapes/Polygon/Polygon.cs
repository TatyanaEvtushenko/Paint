using System.Runtime.Serialization;
using System.Windows.Media;
using Shape.Heirs;
using Shape.Interfaces;

namespace Polygon
{
    [DataContract]
    class Polygon : PointsShape, ISelectable, IEditable
    {
        public Polygon(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        public override System.Windows.Shapes.Shape CreateShapeForDrawing()
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
