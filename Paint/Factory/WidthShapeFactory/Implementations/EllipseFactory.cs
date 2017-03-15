using System.Windows.Media;
using Paint.Shapes;
using Paint.Shapes.WidthShapes.Implementations;

namespace Paint.Factory.WidthShapeFactory.Implementations
{
    class EllipseFactory : WidthShapeFactory, IShapeFactory
    {
        public EllipseFactory()
        {
            ShapeForDrawing = new System.Windows.Shapes.Ellipse {Stroke = new SolidColorBrush(Colors.Black)};
        }

        public Shape Create(ShapeParams param)
        {
            return new Ellipse(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness);
        }
    }
}
