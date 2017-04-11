using System.Windows.Controls;

namespace Shape.Interfaces
{
    public interface IEditable
    {
        void Edit(Canvas canvas, ShapeParams param);
    }
}
