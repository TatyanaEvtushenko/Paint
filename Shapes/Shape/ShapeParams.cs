using System.Collections.Generic;
using System.Windows.Media;

namespace Shape
{
    public struct ShapeParams
    {
        public Brush Fill, Stroke;
        public double[] PointsX, PointsY;
        public double StrokeThickness, X, Y, Width, Height, Angle;
        public List<Shape> ShapesList;
    }
} 
