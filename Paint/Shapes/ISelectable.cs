using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Paint.Shapes
{
    interface ISelectable
    {
        void ChangeSelecting(System.Windows.Shapes.Shape shape);
    }
}
