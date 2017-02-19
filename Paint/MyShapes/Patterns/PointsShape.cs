using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint
{
    abstract class PointsShape : Shape
    {
        public PointCollection Points { get; set; }

        public PointsShape(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness) 
            : base(fill, stroke, strokeThickness)
        {
            Points = new PointCollection();
            for (int i = 0; i < pointsX.Length; i++)
                Points.Add(new Point(pointsX[i], pointsY[i]));
        }
    }

    abstract class PointsShapeDrawer
    {
        public abstract Shape Create(int[] pointsX, int[] pointsY, Brush fill, Brush stroke, double strokeThickness);
    }
}
