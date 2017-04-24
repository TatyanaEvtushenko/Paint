using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Shape;
using Shape.Heirs;
using Shape.Interfaces;

namespace UserShape
{
    public class UserShapeeFactory : IShapeFactory
    {
        public Shape.Shape Create(ShapeParams param)
        {
            return new UserShape(param.X, param.Y, param.Width, param.Height, param.Angle, param.Fill, param.Stroke, param.StrokeThickness, param.ShapesList);
        }

        public System.Windows.Shapes.Shape CreateShapeForDrawing(ShapeParams param)
        {
            //var brush = new DrawingGroup();
            //foreach (var temp in param.ShapesList)
            //{
            //    if (temp is WidthShape)
            //    {
            //        var sh = (WidthShape)temp;
            //        var rect = new Rect(new Point(sh.X, sh.Y), new Point(sh.Width, sh.Height));

            //        var geometry = new GeometryDrawing { Pen = new Pen(new SolidColorBrush(Colors.Black), param.StrokeThickness) };
            //        brush.Children.Add(geometry);
            //    }
            //}
            
            var canva = new Canvas();
            foreach (var temp in param.ShapesList)
            {
                canva.Children.Add(temp.CreateShapeForDrawing());
            }

            var shape = new System.Windows.Shapes.Rectangle
            {
                Width = param.Width,
                Height = param.Height,
                RenderTransform = new RotateTransform { Angle = param.Angle },
                Fill = new VisualBrush() { Visual = canva },
            Stroke = new SolidColorBrush(Colors.Black)
            };
            shape.SetValue(Canvas.LeftProperty, param.X);
            shape.SetValue(Canvas.TopProperty, param.Y);
            return shape;
        }

        public System.Windows.Shapes.Shape GetShapeIcon()
        {
            return new System.Windows.Shapes.Rectangle
            {
                Margin = new Thickness(5, 0, 5, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 15,
                Width = 40,
                RadiusX = 3,
                RadiusY = 3
            };
        }
    }
}
