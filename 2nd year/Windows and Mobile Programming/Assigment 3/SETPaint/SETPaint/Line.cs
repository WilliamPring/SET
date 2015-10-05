using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETPaint
{
    class Line : Shape
    {
        private Point myPenStartPoint;
        private Point myPenEndPoint;
        public Line()
        {
            myPenEndPoint = new Point(0, 0);
            myPenStartPoint = new Point(0, 0);
        } 
        public Line(Point myPenStartPoint, Point myPenEndPoint, Pen pen) : base(pen)
        {
            this.myPenStartPoint = myPenStartPoint;
            this.myPenEndPoint = myPenEndPoint;
        }

    }
}
