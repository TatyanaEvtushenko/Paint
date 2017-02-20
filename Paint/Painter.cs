using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paint
{
    class Painter
    {
        private static Painter instance;
        private Stack<Shape> buffer;
        private Canvas canvas;

        public List<Shape> ShapesList { get; set; }

        private Painter(Canvas canvasPainter)
        {
            canvas = canvasPainter;
            ShapesList = new List<Shape>();
            buffer = new Stack<Shape>();
        }

        public static Painter getInstance(Canvas canvas)
        {
            if (instance == null)
                instance = new Painter(canvas); 
            return instance;
        }

        public void DrawShapesList()
        {
            foreach (var shape in ShapesList)
                shape.Draw(canvas);
        }

        public void AddNewShapeToList(Shape shape)
        {
            AddToList(shape);
            if (CanGoToForwardStep())
                buffer = new Stack<Shape>();
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
            return shape;
        }

        public bool CanGoToForwardStep()
        {
            return buffer.Count > 0;
        }

        public bool CanGoToBackStep()
        {
            return ShapesList.Count > 0;
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
    }
}
