using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class RectangleFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Rectangle(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
