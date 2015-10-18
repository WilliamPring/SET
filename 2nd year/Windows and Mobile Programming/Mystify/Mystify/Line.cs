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
        Random newRnd = new Random();
        Pen myPen; 
        Point myLineStartPoint;
        Point myLineEndPoint;
        private int rnd;
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
            rnd = 0;
        }
        public void RandomNumber()
        {
            rnd = newRnd.Next(1, 10);
        }
        public void draw(object toDraw)
        {
            Graphics drawing = (Graphics)toDraw;
            while (status)
            {
                drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                Thread.Sleep(50);
                myLineStartPoint.X = myLineStartPoint.X - rnd;
                myLineStartPoint.Y = myLineStartPoint.Y - rnd;
                myLineEndPoint.X = myLineEndPoint.X - rnd;
                myLineEndPoint.Y = myLineEndPoint.Y - rnd;
                drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                Thread.Sleep(50);
            }
        }


    }
}
