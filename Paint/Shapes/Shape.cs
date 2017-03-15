using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Paint.Factory;

namespace Paint.Shapes
{
    abstract class Shape : IShape
    {
        public string Description => this.GetType().ToString();
        public Brush Fill => ShapeForDrawing.Fill;
        public Brush Stroke => ShapeForDrawing.Stroke;
        public double StrokeThickness => ShapeForDrawing.StrokeThickness;

        public Shape(ShapeParams param)
        {
            Fill = param.Fill
        }

        public void Draw(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            canvas.Children.Add(shape);
        }

        public abstract System.Windows.Shapes.Shape CreateShapeForDrawing();
    }
}
