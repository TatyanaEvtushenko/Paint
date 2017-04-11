using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shape.Heirs
{
    [DataContract]
    public abstract class PointsShape : Shape
    {
        [DataMember]
        public PointCollection Points { get; set; }

        protected PointsShape(double[] pointsX, double[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(fill, stroke, strokeThickness)
        {
            Points = new PointCollection();
            for (int i = 0; i < pointsX.Length; i++)
                Points.Add(new Point(pointsX[i], pointsY[i]));
        }

        protected override System.Windows.Shapes.Shape GetShapeOnCanvas(Canvas canvas)
        {
            var shape = CreateShapeForDrawing();
            return
                canvas.Children.OfType<System.Windows.Shapes.Shape>()
                    .FirstOrDefault(x => Equals(x.Fill, shape.Fill) && Equals(x.Stroke, shape.Stroke) &&
                                         Equals(x.StrokeThickness, shape.StrokeThickness) &&
                                         x.GetType() == shape.GetType() &&
                                         x.GetValue(Canvas.LeftProperty) == shape.GetValue(Canvas.LeftProperty) &&
                                         x.GetValue(Canvas.TopProperty) == shape.GetValue(Canvas.TopProperty));
        }

        protected override void EditParams(ShapeParams param)
        {
            base.EditParams(param);
            Points = new PointCollection();
            for (int i = 0; i<param.PointsY.Length; i++)
                Points.Add(new Point(param.PointsX[i], param.PointsY[i]));
        }

        protected override void EditShapeOnCanvas(System.Windows.Shapes.Shape shape)
        {
            base.EditShapeOnCanvas(shape);
        }
    }
}
