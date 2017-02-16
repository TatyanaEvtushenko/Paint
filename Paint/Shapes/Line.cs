using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    class Line : Shape
    {
        public Line(Brushes fill, Brushes stroke, byte strokeThickness) : base(fill, stroke, strokeThickness) { }
    }

    class LineDrawer : Drawer
    {
        public override Shape Draw()
        {
            return new Line();
        }
    }
}
