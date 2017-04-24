using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Controls;
using MyShape = Shape.Shape;

namespace Paint.ShapeList.Implementations
{
    [Serializable]
    [DataContract]
    class Painter : IShapeList
    {
        private static Painter painter;
        private Stack<MyShape> buffer;
        public Canvas Canvas { get; set; }
        [DataMember]
        public List<MyShape> ShapesList { get; set; }

        public bool CanGoToForwardStep => buffer.Count > 0;
        public bool CanGoToBackStep => ShapesList.Count > 0;
        public bool CanClean => ShapesList.Count > 0;

        private Painter(Canvas canvasPainter)
        {
            Canvas = canvasPainter;
            ShapesList = new List<MyShape>();
            buffer = new Stack<MyShape>();
        }

        public static Painter GetPainter(Canvas canvas)
        {
            return new Painter(canvas);
           // return painter ?? (painter = new Painter(canvas));
        }

        public void DrawAll()
        {
            foreach (var shape in ShapesList)
                shape.Draw(Canvas);
        }

        public void CleanAll()
        {
            ShapesList = new List<MyShape>();
            Canvas.Children.Clear();
        }

        public void AddNewShapeToList(MyShape shape)
        {
            AddToList(shape);
            if (CanGoToForwardStep)
                buffer = new Stack<MyShape>();
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

        private void AddToList(MyShape shape)
        {
            ShapesList.Add(shape);
            shape.Draw(Canvas);
        }

        private MyShape RemoveFromList()
        {
            var shape = ShapesList.Last<MyShape>();
            ShapesList.Remove(shape);
            Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
            return shape;
        }
    }
}
