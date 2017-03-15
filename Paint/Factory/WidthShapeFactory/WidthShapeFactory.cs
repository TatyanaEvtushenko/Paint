
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Factory.WidthShapeFactory
{
    abstract class WidthShapeFactory : ShapeForDrawning
    {
        public void ChangeShapeForDrawing(Point lastPoint, Point newPoint)
        {
            double minX, maxX, minY, maxY;
            FindMinAndMax(lastPoint.X, newPoint.X, out minX, out maxX);
            FindMinAndMax(lastPoint.Y, newPoint.Y, out minY, out maxY);
            ShapeForDrawing.Width = maxX - maxY;
            ShapeForDrawing.Height = maxY - minY;
            ShapeForDrawing.SetValue(Canvas.LeftProperty, minX);
            ShapeForDrawing.SetValue(Canvas.TopProperty, minY);
        }

        private void FindMinAndMax(double val1, double val2, out double min, out double max)
        {
            if (val1 > val2)
            {
                min = val2;
                max = val1;
            }
            else
            {
                max = val2;
                min = val1;
            }

        }
    }
}
