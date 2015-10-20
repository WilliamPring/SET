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
        List<List <Line> > ListOfThreadOfLine = new List<List<Line>>();
        private int counter;
        private Random newRnd = new Random();
        private Pen myPen;
        ThreadRepository tRepoStartShadowLine = new ThreadRepository();
        private Point myLineStartPoint;
        private Point myLineEndPoint;
        private int rnd;
        bool status;
        static private Object thisLock = new Object();
        public struct lineInfo
        {
           public List<Line> listToPass;
           public Graphics toDraw;
           public Point myPrevEnd;
           public Point myPrevStart;
           public Pen myTempPen;
        }


        public Line()
        {
            myPen = new Pen(Color.Black); 
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
            rnd = newRnd.Next(-6, 10);
        }

        public void MoveLine(Graphics drawing)
        {

        }
        public void drawShadowLine(object drawingShadowLines)
        {
            lock (thisLock)
            {

                MoveLine tempDrawingShadowLines = (MoveLine)drawingShadowLines;
                Point tempEnd = new Point();
                Point temStart = new Point();
                //penType = Pen(Color.Black, 2);


                if (tempDrawingShadowLines.listToPass.Count == 4)
                {

                    //start temp
                    temStart = tempDrawingShadowLines.listToPass.ElementAt(0).myLineStartPoint;
                    //end temp
                    tempEnd = tempDrawingShadowLines.listToPass.ElementAt(0).myLineEndPoint;
                    tempDrawingShadowLines.myTempPen.Color = Color.White;
                    tempDrawingShadowLines.toDraw.DrawLine(tempDrawingShadowLines.myTempPen, temStart, tempEnd);
                    tempDrawingShadowLines.listToPass.RemoveAt(0);
                    //myTempStruct.listToPass.RemoveRange(0, 1);
                    //drawingShadowLines = myTempStruct;
                    Thread.Sleep(20);
                    tempDrawingShadowLines.myTempPen.Color = Color.Black;
                }
                else
                {
                    //RandomNumber();
                    tempDrawingShadowLines.myPrevStart.X = tempDrawingShadowLines.myPrevStart.X + 5;
                    //RandomNumber();
                    tempDrawingShadowLines.myPrevStart.Y = tempDrawingShadowLines.myPrevStart.Y + 3;
                    //RandomNumber();
                    tempDrawingShadowLines.myPrevEnd.X = tempDrawingShadowLines.myPrevEnd.X + 5;
                    //RandomNumber();
                    tempDrawingShadowLines.myPrevEnd.Y = tempDrawingShadowLines.myPrevEnd.Y + 3;
                    tempDrawingShadowLines.toDraw.DrawLine(tempDrawingShadowLines.myTempPen, tempDrawingShadowLines.myPrevStart, tempDrawingShadowLines.myPrevEnd);
                    tempDrawingShadowLines.listToPass.Add(new Line(tempDrawingShadowLines.myPrevStart, tempDrawingShadowLines.myPrevEnd, tempDrawingShadowLines.myTempPen));
                    //drawingShadowLines = myTempStruct;
                    Thread.Sleep(20);
                }
            }
        
        }
        public void draw(object toDraw)
        {
            MoveLine shadowLines = new MoveLine();
            Line temp = new Line();
            Graphics drawing = (Graphics)toDraw;
            List<Line> Main = new List<Line>();
            shadowLines.toDraw = drawing;
            shadowLines.listToPass = Main;
            shadowLines.myTempPen = myPen;
            shadowLines.myPrevEnd = myLineEndPoint;
            shadowLines.myPrevStart = myLineStartPoint;
            //add line to the list
            shadowLines.listToPass.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
            
            while (status)
            {
                lock (thisLock)
                {
                    //draw the first line
                    tRepoStartShadowLine.AddClass("ShadowLine", new ParameterizedThreadStart(temp.drawShadowLine), shadowLines);
                }
            }

        }

       
      


        }
}
