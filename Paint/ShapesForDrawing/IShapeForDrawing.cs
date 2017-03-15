using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    interface IShapeForDrawing
    {
        System.Windows.Shapes.Shape ShapeForDrawing { get; set; }

        void ChangeShapeForDrawing(ShapeParams param);
    }
}
