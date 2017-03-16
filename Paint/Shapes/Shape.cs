using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paint.Factory;

namespace Paint.Shapes
{
    [DataContract]
    abstract class Shape : IShape
    {
        public string Description => this.GetType().ToString();
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
            canvas.Children.Add(shape);
        }
    }
}

