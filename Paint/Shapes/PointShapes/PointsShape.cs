using System.Windows;
using System.Windows.Media;

namespace Paint.Shapes.PointShapes
{
    abstract class PointsShape : Shape
    {
        public PointCollection Points { get; set; }

        protected PointsShape(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness) 
            : base(fill, stroke, strokeThickness)
        {
            Points = new PointCollection();
            for (int i = 0; i < pointsX.Length; i++)
                Points.Add(new Point(pointsX[i], pointsY[i]));
        }
    }
}
