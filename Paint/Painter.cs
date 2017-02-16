using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Painter
    {
        private static Painter instance;

        public List<Shape> ShapesList { get; set; }

        private Painter() { }

        public static Painter getInstance()
        {
            int pppppppppppp;
            if (instance == null)
                instance = new Painter(); 
            return instance;
        }
    }
}
