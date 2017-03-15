using System.Windows.Controls;

namespace Paint.ShapeForDrawing.WidthShapeForDrawning
{
    abstract class WidthShapeForDrawing : ShapeForDrawingType
    {
        public override void ChangeShapeForDrawing(ShapeParams param)
        {
            base.ChangeShapeForDrawing(param);
            ShapeForDrawing.Width = param.Width;
            ShapeForDrawing.Height = param.Height;
            ShapeForDrawing.SetValue(Canvas.LeftProperty, param.X);
            ShapeForDrawing.SetValue(Canvas.TopProperty, param.Y);
        }
    }
}
