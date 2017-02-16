using System.Windows.Media;

namespace Paint
{
    abstract class Shape
    {
        public Brushes Fill { get; set; }
        public Brushes Stroke { get; set; }
        public byte StrokeThickness { get; set; }

        public Shape(Brushes fill, Brushes stroke, byte strokeThickness)
        {
            Fill = fill;
            Stroke = stroke;
            StrokeThickness = strokeThickness;
        }
    }

    abstract class Drawer
    {
        public abstract Shape Draw();
    }
}
