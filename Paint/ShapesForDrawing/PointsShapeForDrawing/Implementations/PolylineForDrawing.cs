using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint.ShapeForDrawing.PointsShapeForDrawing.Implementations
{
    class PolylineForDrawing : ShapeForDrawning.PointsShapeForDrawing
    {
        public PolylineForDrawing()
        {
            ShapeForDrawing = new Polyline();
        }
    }
}
