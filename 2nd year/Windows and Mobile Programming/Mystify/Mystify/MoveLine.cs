using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify
{
    class MoveLine
    {
        public List<Line> listToPass = new List<Line>();
        public Graphics toDraw;
        public Pen myTempPen;
        public Point myPrevEnd;
        public Point myPrevStart;
        
        public MoveLine()
        {

        }


    }
}
