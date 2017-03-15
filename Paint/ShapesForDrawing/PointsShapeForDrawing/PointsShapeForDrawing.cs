using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.ShapeForDrawning
{
    abstract class PointsShapeForDrawing : ShapeForDrawingType
    {
        public override void ChangeShapeForDrawing(ShapeParams param)
        {
            base.ChangeShapeForDrawing(param);
            ((Polyline)ShapeForDrawing).Points = new PointCollection();
            for (int i = 0; i < param.PointsX.Length; i++)
                ((Polyline)ShapeForDrawing).Points.Add(new Point(param.PointsX[i], param.PointsY[i]));
        }
    }
}
