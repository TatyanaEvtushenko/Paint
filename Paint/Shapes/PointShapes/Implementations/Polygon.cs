using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes.Implementations
{
    [DataContract]
    class Polygon : PointsShape
    {
        public Polygon(System.Windows.Shapes.Polygon shape)
        {
            ShapeForDrawing = shape;
        }
    }
}
