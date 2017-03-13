using Paint.Shapes;
using Paint.Shapes.PointShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class PolygonFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Polygon(param.PointsX, param.PointsY, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
