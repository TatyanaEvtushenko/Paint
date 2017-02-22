using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Polygon : PointsShape
    {
        public Polygon(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness) 
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var polygon = new library.Polygon();
            polygon.Fill = Fill;
            polygon.Stroke = Stroke;
            polygon.StrokeThickness = StrokeThickness;
            polygon.Points = Points;
            return polygon;
        }
    }

    class PolygonDrawer : ShapeDrawer
    {
        public override Shape Create(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness)
        { 
            return new Polygon(pointsX, pointsY, fill, stroke, strokeThickness);
        }
    }
}
