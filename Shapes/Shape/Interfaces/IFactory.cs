namespace Shape.Interfaces
{
    public interface IShapeFactory
    {
        Shape Create(ShapeParams param);
        System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param);
        System.Windows.Shapes.Shape GetShapeIcon();
    }
}
