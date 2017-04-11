using System.Windows.Controls;

namespace Shape.Interfaces
{
    public interface ISelectable
    {
        void Selecte(Canvas canvas);
        void Unselecte(Canvas canvas);
    }
}
