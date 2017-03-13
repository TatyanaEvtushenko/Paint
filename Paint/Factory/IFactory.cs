using Paint.Shapes;

namespace Paint.Factory
{
    interface IShapeFactory
    {
        Shape Create(ShapeParams param);
    }
}
