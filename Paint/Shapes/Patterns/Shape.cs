using System.Windows.Media;

namespace Paint
{
    abstract class Shape
    {
        public Brushes Fill { get; set; }
        public Brushes Stroke { get; set; }
        public int StrokeThickness { get; set; }

        public Shape(Brushes fill, Brushes stroke, int strokeThickness)
        {
            Fill = fill;
            Stroke = stroke;
            StrokeThickness = strokeThickness;
        }
    }
}
