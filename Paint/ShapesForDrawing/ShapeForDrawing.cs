using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    [DataContract]
    abstract class ShapeForDrawingType : IShapeForDrawing
    {
        [DataMember]
        public System.Windows.Shapes.Shape ShapeForDrawing { get; set; }

        public virtual void ChangeShapeForDrawing(ShapeParams param)
        {
            ShapeForDrawing.Fill = param.Fill;
            ShapeForDrawing.Stroke = param.Stroke;
            ShapeForDrawing.StrokeThickness = param.StrokeThickness;
        }
    }
}
