using System.Windows;
using System.Windows.Media;
using Shape;
using Shape.Interfaces;

namespace Polygon
{
    public class PolygonFactory : IShapeFactory
    {
        public Shape.Shape Create(ShapeParams param)
        {
            return new Polygon(param.PointsX, param.PointsY, param.Fill, param.Stroke, param.StrokeThickness);
        }

        public System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param)
        {
            var shape = new System.Windows.Shapes.Polygon
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Points = new PointCollection()
            };
            for (int i = 0; i < param.PointsY.Length; i++)
                shape.Points.Add(new Point(param.PointsX[i], param.PointsY[i]));
            return shape;
        }

        public System.Windows.Shapes.Shape GetShapeIcon()
        {
            return new System.Windows.Shapes.Polygon
            {
                Margin = new Thickness(5, 0, 5, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Points = new PointCollection { new Point(0, 15), new Point(20, 0), new Point(40, 15) }
            };
        }
    }
}
