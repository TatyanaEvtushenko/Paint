using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;
using Shape.Heirs;
using Shape.Interfaces;

namespace RoundRectangle
{
    [DataContract]
    class RoundRectangle : WidthShape, ISelectable, IEditable
    {
        public RoundRectangle(double x, double y, double width, double height, double angle, Brush fill, Brush stroke, double strokeThickness)
            : base(x, y, width, height, angle, fill, stroke, strokeThickness)
        { }

        public override System.Windows.Shapes.Shape CreateShapeForDrawing()
        {
            var roundRectangle = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Fill,
                RenderTransform = new RotateTransform() { Angle = Angle },
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            roundRectangle.SetValue(Canvas.LeftProperty, X);
            roundRectangle.SetValue(Canvas.TopProperty, Y);
            roundRectangle.RadiusX = roundRectangle.RadiusY = (Height < Width ? Height : Width) / 10;
            return roundRectangle;
        }
    }
}
