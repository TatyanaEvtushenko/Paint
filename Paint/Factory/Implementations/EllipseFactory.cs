using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class EllipseFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Ellipse(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
