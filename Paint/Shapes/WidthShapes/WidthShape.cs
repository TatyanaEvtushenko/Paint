using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Shapes.WidthShapes
{
    [DataContract]
    abstract class WidthShape : Shape
    {
        [DataMember]
        public double Width { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Angle { get; set; }
        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }

        protected WidthShape(double x, double y, double width, double height, double angle, Brush fill, Brush stroke, double strokeThickness)
            : base(fill, stroke, strokeThickness)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Angle = angle;
        }

        protected override System.Windows.Shapes.Shape GetShapeOnCanvas(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            var shapesOnCanvas = canvas.Children.OfType<System.Windows.Shapes.Shape>();
            return shapesOnCanvas.FirstOrDefault(x => 
                                         Equals(x.Fill, shape.Fill) && Equals(x.Stroke, shape.Stroke) &&
                                         Equals(x.StrokeThickness, shape.StrokeThickness) &&
                                         x.GetType() == shape.GetType() && Equals(x.Width, shape.Width) &&
                                         Equals(x.Height, shape.Height));
        }

        protected override void EditParams(ShapeParams param)
        {
            base.EditParams(param);
            Width = param.Width;
            Height = param.Height;
            Angle = param.Angle;
            X = param.X;
            Y = param.Y;
        }

        protected override void EditShapeOnCanvas(System.Windows.Shapes.Shape shape)
        {
            base.EditShapeOnCanvas(shape);
            shape.Width = Width;
            shape.Height = Height;
            shape.SetValue(Canvas.LeftProperty, X);
            shape.SetValue(Canvas.TopProperty, Y);
            shape.RenderTransform = new RotateTransform {Angle = Angle};
        }
    }
}
