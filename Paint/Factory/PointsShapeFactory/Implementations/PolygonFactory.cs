using System.Windows;
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

        public FrameworkElement CreateShapeForDrawing(Point lastPoint, Point newPoint)
        {
            var shape = new System.Windows.Shapes.Rectangle();
            return shape;
        }
    }
}
