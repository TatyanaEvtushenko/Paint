using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes
{
    abstract class Shape : IShape
    {
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }
        public string Description { get; set; }

        protected Shape(Brush fill, Brush stroke, double strokeThickness)
        {
            Fill = fill;
            Stroke = stroke;
            StrokeThickness = strokeThickness;
            Description = this.ToString();
        }

        protected abstract FrameworkElement CreateShapeForDrawing();

        public void Draw(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            canvas.Children.Add(shape);
        }
    }
}
