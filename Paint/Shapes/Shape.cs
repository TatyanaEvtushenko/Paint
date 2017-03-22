using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Paint.Factory;

namespace Paint.Shapes
{
    [DataContract]
    abstract class Shape : IShape
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

        public abstract System.Windows.Shapes.Shape CreateShapeForDrawing(); 

        public void Draw(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            canvas.Children.Add(shape);
        }

        protected abstract System.Windows.Shapes.Shape GetShapeOnCanvas(Canvas canvas);

        public void Selecte(Canvas canvas)
        {
            var shapeOnCanvas = GetShapeOnCanvas(canvas);
            shapeOnCanvas.StrokeDashArray = DoubleCollection.Parse("2");
        }

        public void Unselecte(Canvas canvas)
        {
            var shapeOnCanvas = GetShapeOnCanvas(canvas);
            shapeOnCanvas.StrokeDashArray = null;
        }

        protected virtual void EditParams(ShapeParams param)
        {
            Fill = param.Fill;
            Stroke = param.Stroke;
            StrokeThickness = param.StrokeThickness;
        }

        protected virtual void EditShapeOnCanvas(System.Windows.Shapes.Shape shape)
        {
            shape.Fill = Fill;
            shape.Stroke = Stroke;
            shape.StrokeThickness = StrokeThickness;
        }

        public void Edit(Canvas canvas, ShapeParams param)
        {
            var shapeOnCanvas = GetShapeOnCanvas(canvas);
            EditParams(param);
            EditShapeOnCanvas(shapeOnCanvas);
        }
    }
}

