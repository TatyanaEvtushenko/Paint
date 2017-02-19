using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    abstract class WidthShape : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Angle { get; set; }

        public WidthShape(int width, int height, int angle, Brushes fill, Brushes stroke, int strokeThickness) : base(fill, stroke, strokeThickness)
        {
            Width = width;
            Height = height;
            Angle = angle;
        }
    }

    abstract class WidthShapeDrawer
    {
        public abstract Shape Draw(int width, int height, int angle, Brushes fill, Brushes stroke, int strokeThickness);
    }
}
