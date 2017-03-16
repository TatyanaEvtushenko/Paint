using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Paint.Shapes;
using Paint.Shapes.PointShapes.Implementations;

namespace Paint.Factory.Implementations
{
    class PolylineFactory : IShapeFactory
    {
        public Shape Create(ShapeParams param)
        {
            return new Polyline(param.PointsX, param.PointsY, param.Fill, param.Stroke, param.StrokeThickness);
        }

        public System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param)
        {
            var shape = new System.Windows.Shapes.Polyline
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Points = new PointCollection()
            };
            for (int i = 0; i < param.PointsY.Length; i++)
                shape.Points.Add(new Point(param.PointsX[i], param.PointsY[i]));
            return shape;
        }
    }
}
