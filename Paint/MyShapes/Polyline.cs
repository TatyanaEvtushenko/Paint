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
    class Polyline : PointsShape
    {
        public Polyline(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness)
            : base(pointsX, pointsY, fill, stroke, strokeThickness)
        { }

        protected override FrameworkElement CreateShapeForDrawing()
        {
            var polyline = new library.Polyline();
            polyline.Stroke = Stroke;
            polyline.StrokeThickness = StrokeThickness;
            polyline.Points = Points;
            return polyline;
        }
    }

    class PolylineDrawer : PointsShapeDrawer
    {
        public override Shape Create(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness)
        {
            return new Polyline(pointsX, pointsY, fill, stroke, strokeThickness);
        }
    }
}
