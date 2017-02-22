using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    abstract class Shape
    {
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }
        public string Description { get; set; }

        public Shape(Brush fill, Brush stroke, double strokeThickness)
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

    class ShapeDrawer
    {
        public virtual Shape Create(double x, double y, int width, int height, int angle, Brush fill, Brush stroke, double strokeThickness) { return null; }

        public virtual Shape Create(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness) { return null; }
    }
}
