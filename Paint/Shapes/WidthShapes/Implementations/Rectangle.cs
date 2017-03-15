using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes.Implementations
{
    [DataContract]
    class Rectangle : WidthShape
    {
        public Rectangle(System.Windows.Shapes.Rectangle shape)
        {
            ShapeForDrawing = shape;
        }
    }
}
