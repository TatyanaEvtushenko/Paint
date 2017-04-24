using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shape;
using Shape.Heirs;
using Shape.Interfaces;

namespace UserShape
{
    [DataContract]
    class UserShape : WidthShape, ISelectable, IEditable
    {
        [DataMember]
        private List<Shape.Shape> shapes;

        public UserShape(double x, double y, double width, double height, double angle, Brush fill, Brush stroke,
            double strokeThickness, List<Shape.Shape> shapes)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        {
            this.shapes = shapes;
        }

        public override System.Windows.Shapes.Shape CreateShapeForDrawing()
        {
            var canva = new Canvas();
            foreach (var temp in shapes)
            {
                canva.Children.Add(temp.CreateShapeForDrawing());
            }

            var rectangle = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                RenderTransform = new RotateTransform { Angle = Angle },
                Fill = new VisualBrush() { Visual = canva },
            };
            rectangle.SetValue(Canvas.LeftProperty, X);
            rectangle.SetValue(Canvas.TopProperty, Y);
            return rectangle;
        }

        protected override System.Windows.Shapes.Shape GetShapeOnCanvas(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            var shapesOnCanvas = canvas.Children.OfType<System.Windows.Shapes.Shape>();
            return shapesOnCanvas.FirstOrDefault(x =>
                                         Equals(x.StrokeThickness, shape.StrokeThickness) &&
                                         x.GetType() == shape.GetType() && Equals(x.Width, shape.Width) &&
                                         Equals(x.Height, shape.Height));
        }

        private UIElementCollection GetShapes(Canvas canvas)
        {
            var shapeOnCanvas = GetShapeOnCanvas(canvas);
            var brush = (VisualBrush) shapeOnCanvas.Fill;
            var canva = (Canvas) brush.Visual;
            return canva.Children;
        }

        public override void Selecte(Canvas canvas)
        {
            var shapesOnCanvas = GetShapes(canvas);
            foreach (var shape in shapesOnCanvas.OfType<System.Windows.Shapes.Shape>())
            {
                shape.StrokeDashArray = DoubleCollection.Parse("2");
            }
        }

        public override void Unselecte(Canvas canvas)
        {
            var shapesOnCanvas = GetShapes(canvas);
            foreach (var shape in shapesOnCanvas.OfType<System.Windows.Shapes.Shape>())
            {
                shape.StrokeDashArray = null;
            }
        }

        protected override void EditParams(ShapeParams param)
        {
            Width = param.Width;
            Height = param.Height;
            Angle = param.Angle;
            X = param.X;
            Y = param.Y;
        }

        protected override void EditShapeOnCanvas(System.Windows.Shapes.Shape shape)
        {
            shape.Width = Width;
            shape.Height = Height;
            shape.SetValue(Canvas.LeftProperty, X);
            shape.SetValue(Canvas.TopProperty, Y);
            shape.RenderTransform = new RotateTransform { Angle = Angle };
        }
    }
}
