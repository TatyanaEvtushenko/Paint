using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Paint.Factory;

namespace Paint.Shapes
{
    [DataContract]
    abstract class Shape : IShape, ISelectable
    {
        public string Description => this.GetType().Name;
        [DataMember]
        public Brush Fill { get; set; }
        [DataMember]
        public Brush Stroke { get; set; }
        [DataMember]
        public double StrokeThickness { get; set; }

        protected Shape(Brush fill, Brush stroke, double strokeThickness)
        {
            Fill = fill;
            Stroke = stroke;
            StrokeThickness = strokeThickness;
        }

        protected abstract System.Windows.Shapes.Shape CreateShapeForDrawing(); 

        public void Draw(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            shape.Focusable = true;
            shape.MouseRightButtonDown += (sender, args) => ChangeSelecting((System.Windows.Shapes.Shape) sender);
            shape.LostKeyboardFocus += (sender, args) => UnselecteShape((System.Windows.Shapes.Shape) sender);
            canvas.Children.Add(shape);
        }

        public void ChangeSelecting(System.Windows.Shapes.Shape shape)
        {
            if (!(this is ISelectable))
                return;
            if (IsSelected(shape))
                UnselecteShape(shape);
            else
                SelecteShape(shape);
        }
        
        private bool IsSelected(System.Windows.Shapes.Shape shape)
        {
            return shape.StrokeDashArray.Count != 0;
        }

        private void SelecteShape(System.Windows.Shapes.Shape shape)
        {
            shape.StrokeDashArray = DoubleCollection.Parse("2");
            Keyboard.Focus(shape);
        }

        private void UnselecteShape(System.Windows.Shapes.Shape shape)
        {
            if (this is ISelectable)
                shape.StrokeDashArray.Clear();
        }
    }
}

