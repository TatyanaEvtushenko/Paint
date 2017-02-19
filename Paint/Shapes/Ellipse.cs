using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using library = System.Windows.Shapes;

namespace Paint
{
    class Ellipse : WidthShape
    {
        public Ellipse(int width, int height, int angle, Brushes fill, Brushes stroke, int strokeThickness) 
            : base(width, height, angle, fill, stroke, strokeThickness)
        {

        }
    }

    class EllipseDrawer : ShapeDrawer
    {
        public override Shape Create()
        {

            return Shape;
        }

        private library.Ellipse Draw(int width, int height, int angle, Brush fill, Brush stroke, int strokeThickness)
        {
            var ellipse = new library.Ellipse();
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Fill = fill;
            ellipse.Stroke = stroke;
            ellipse.StrokeThickness = strokeThickness;
            
        }
    }
}
