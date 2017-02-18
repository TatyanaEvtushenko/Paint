using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    class Rectangle : Ellipse
    {
        public int RadiusX { get; set; }
        public int RadiusY { get; set; }
    }

    //class RectangleDrawer : Drawer
    //{
    //    public override Shape Draw()
    //    {
    //        return new Rectangle();
    //    }
    //}
}
