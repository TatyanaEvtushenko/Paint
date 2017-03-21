using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Paint.Shapes
{
    interface IEditable
    {
        void Edit(Canvas canvas, ShapeParams param);
    }
}
