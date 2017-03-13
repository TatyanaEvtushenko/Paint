using System.Collections.Generic;
using Paint.Shapes;

namespace Paint.ShapeList
{
    interface IShapeList
    {
        List<Shape> ShapesList { get; set; }
        bool CanGoToForwardStep { get; }
        bool CanGoToBackStep { get; }
        bool CanClean { get; }

        void AddNewShapeToList(Shape shape);
        void DrawAll();
        void CleanAll();
        void GoToForwardStep();
        void GoToBackStep();
    }
}
