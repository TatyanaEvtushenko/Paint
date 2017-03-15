using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.Shapes.PointShapes
{
    [DataContract]
    abstract class PointsShape : Shape
    {
        public PointCollection Points => ((Polyline)ShapeForDrawing).Points;
    }
}
