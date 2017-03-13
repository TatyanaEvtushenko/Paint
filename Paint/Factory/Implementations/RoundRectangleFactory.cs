using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class RoundRectangleFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new RoundRectangle(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
