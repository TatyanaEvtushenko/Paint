using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint.ShapeForDrawing.PointsShapeForDrawing.Implementations
{
    class PolygonForDrawing : ShapeForDrawning.PointsShapeForDrawing
    {
        public PolygonForDrawing()
        {
            ShapeForDrawing = new Polygon();
        }
    }
}
