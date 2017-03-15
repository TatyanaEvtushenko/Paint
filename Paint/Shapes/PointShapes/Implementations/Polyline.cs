using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes.Implementations
{
    [DataContract]
    class Polyline : PointsShape
    {
        public Polyline(System.Windows.Shapes.Polyline shape)
        {
            ShapeForDrawing = shape;
        }
    }
}
