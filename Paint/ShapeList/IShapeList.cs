using System.Collections.Generic;
using MyShape = Shape.Shape;

namespace Paint.ShapeList
{
    interface IShapeList
    {
        List<MyShape> ShapesList { get; set; }
        bool CanGoToForwardStep { get; }
        bool CanGoToBackStep { get; }
        bool CanClean { get; }

        void AddNewShapeToList(MyShape shape);
        void DrawAll();
        void CleanAll();
        void GoToForwardStep();
        void GoToBackStep();
    }
}
