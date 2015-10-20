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
        public Point VelocityForStartingPoint;
        public Point VelocityForEndingPoint;

        public MoveLine()
        {
            myTempPen = new Pen(Color.Red, 1);
            myPrevEnd = new Point(0, 0);
            myPrevStart = new Point(0, 0);
            VelocityForStartingPoint = new Point(0, 0);
            VelocityForEndingPoint = new Point(0, 0);

        }


}
}
