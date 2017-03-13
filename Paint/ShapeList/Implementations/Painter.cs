using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Paint.Shapes;

namespace Paint.ShapeList.Implementations
{
    class Painter : IShapeList
    {
        private static Painter painter;
        private Stack<Shape> buffer;
        private Canvas canvas;

        public List<Shape> ShapesList { get; set; }

        public bool CanGoToForwardStep => buffer.Count > 0;
        public bool CanGoToBackStep => ShapesList.Count > 0;
        public bool CanClean => ShapesList.Count > 0;

        private Painter(Canvas canvasPainter)
        {
            canvas = canvasPainter;
            ShapesList = new List<Shape>();
            buffer = new Stack<Shape>();
        }

        public static Painter GetPainter(Canvas canvas)
        {
            return painter ?? (painter = new Painter(canvas));
        }

        public void DrawAll()
        {
            foreach (var shape in ShapesList)
                shape.Draw(canvas);
        }

        public void CleanAll()
        {
            ShapesList = new List<Shape>();
            canvas.Children.Clear();
        }

        public void AddNewShapeToList(Shape shape)
        {
            AddToList(shape);
            if (CanGoToForwardStep)
                buffer = new Stack<Shape>();
        }

        public void GoToForwardStep()
        {
            var shape = buffer.Pop();
            AddToList(shape);       
        }

        public void GoToBackStep()
        {
            var shape = RemoveFromList();
            buffer.Push(shape);
        }

        private void AddToList(Shape shape)
        {
            ShapesList.Add(shape);
            shape.Draw(canvas);
        }

        private Shape RemoveFromList()
        {
            var shape = ShapesList.Last<Shape>();
            ShapesList.Remove(shape);
            canvas.Children.RemoveAt(canvas.Children.Count - 1);
            return shape;
        }
    }
}
