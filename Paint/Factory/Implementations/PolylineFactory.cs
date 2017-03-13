using Paint.Shapes;
using Paint.Shapes.PointShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class PolylineFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Polyline(param.PointsX, param.PointsY, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
