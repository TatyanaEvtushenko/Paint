using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes
{
    [DataContract]
    abstract class Shape : IShape
    {
        [DataMember]
        public Brush Fill { get; set; }
        [DataMember]
        public Brush Stroke { get; set; }
        [DataMember]
        public double StrokeThickness { get; set; }
        [DataMember]
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
