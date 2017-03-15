using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint.ShapeForDrawing.WidthShapeForDrawing.Implementations
{
    class RoundRectangleForDrawing : WidthShapeForDrawning.WidthShapeForDrawing
    {
        public RoundRectangleForDrawing()
        {
            ShapeForDrawing = new Rectangle();
            ((Rectangle) ShapeForDrawing).RadiusX =
                ((Rectangle) ShapeForDrawing).RadiusY = Math.Min(ShapeForDrawing.Width, ShapeForDrawing.Height)/10;
        }
    }
}
