using System.Windows;
using Paint.Shapes;

namespace Paint.Factory
{
    interface IShapeFactory
    {
        Shape Create(ShapeParams param);
        System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param);
    }
}
