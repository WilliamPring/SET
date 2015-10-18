using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mystify
{
    class Line
    {
        Pen myPen; 
        Point myLineStartPoint;
        Point myLineEndPoint; 
        bool status;
        public Line()
        {
         //do nothing
        }
        public Line(Point myLineStartPoint, Point myLineEndPoint, Pen myPen)
        {
            this.myLineStartPoint = myLineStartPoint;
            this.myLineEndPoint = myLineEndPoint;
            this.myPen = myPen;
            status = true;  
        }
        public void draw(object toDraw)
        {
            Graphics drawing = (Graphics)toDraw;
            while (status)
            {
                drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                Thread.Sleep(50);
                myLineStartPoint.X = myLineStartPoint.X - 10;
                myLineStartPoint.Y = myLineStartPoint.Y - 10;
                drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                Thread.Sleep(50);
            }
        }


    }
}
