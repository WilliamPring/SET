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
        public volatile static ManualResetEvent wait_handle = new ManualResetEvent(true);

        private Pen myPen;
        private ThreadRepository tRepoStartShadowLine;
        private Point myLineStartPoint;
        private Point myLineEndPoint;
        private Point VelocityForEndingPoint = new Point();
        private Point VelocityForStartingPoint = new Point();
        private Random rnd = new Random();
        public static volatile bool status = true;
        private static Object thisLock = new Object();

        List<Line> MainList = new List<Line>();
        public Line()
        {
            tRepoStartShadowLine = new ThreadRepository();
            myPen = new Pen(Color.Red, 1);
            myLineEndPoint = new Point(0, 0);
            myLineEndPoint = new Point(0, 0);
        }
        public Line(Point myLineStartPoint, Point myLineEndPoint, Pen myPen)
        {
            this.myLineStartPoint = myLineStartPoint;
            this.myLineEndPoint = myLineEndPoint;
            this.myPen = myPen;           
        }

       

        private Point SetVector()
        {
            int vector = rnd.Next(1, 8);
            Point directionToMove = new Point();
            //North
            if (vector == 1)
            {
     
                directionToMove.X = 0;
                directionToMove.Y = 4;
            }

            if (vector == 2)
            {
                directionToMove.X = 0;
                directionToMove.Y = 4;
            }

            if (vector == 3)
            {
                directionToMove.X = 4;
                directionToMove.Y = 4;
            }

            if (vector == 4)
            {
                directionToMove.X = 4;
                directionToMove.Y = 0;
            }

            if (vector == 5)
            {
                directionToMove.X = 4;
                directionToMove.Y = -4;
            }

            if (vector == 6)
            {
                directionToMove.X = 0;
                directionToMove.Y = -4;
            }

            if (vector == 7)
            {
                directionToMove.X = -4;
                directionToMove.Y = -4;
            }

            if (vector == 8)
            {
                
            }

            return directionToMove;
        }
        public void drawS(Graphics ShadowLines)
        {
          
                Point tempEnd = new Point();
                Point tempStart = new Point();
                

                if (MainList.Count == 6)
                {
                    tempStart = MainList.ElementAt(0).myLineStartPoint;
                    tempEnd = MainList.ElementAt(0).myLineEndPoint;
                    myPen.Color = Color.White;
                    ShadowLines.DrawLine(myPen, tempStart, tempEnd);
                    MainList.RemoveAt(0);
                    myPen.Color = Color.Red; 
        
                }
                else
                {
                    Move();
                    ShadowLines.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                    MainList.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
                }
          

        }

      public void Move()
      {
            bool StartLoop = true;
            for (;;)
            {
                StartLoop = true;
                myLineStartPoint.X = myLineStartPoint.X + VelocityForStartingPoint.X;
                myLineStartPoint.Y = myLineStartPoint.Y + VelocityForStartingPoint.Y;
                if (myLineStartPoint.X <= 0)
                {
                    myLineStartPoint.X = 0;
                    StartLoop = false;
                }
                if (myLineStartPoint.X > 326)
                {
                    myLineStartPoint.X = 326;
                    StartLoop = false;
                }
                if (myLineStartPoint.Y <= 0)
                {
                    myLineStartPoint.Y = 0;
                    StartLoop = false;
                }
                if (myLineStartPoint.Y > 308)
                {
                    myLineStartPoint.Y = 308;
                    StartLoop = false;
                }
                if (StartLoop == true)
                {
                    break;
                }
                else
                {
                    VelocityForStartingPoint = SetVector();
                }
            }

            for (;;)
            {
                StartLoop = true;
                myLineEndPoint.X = myLineEndPoint.X + VelocityForEndingPoint.X;
                myLineEndPoint.Y = myLineEndPoint.Y + VelocityForEndingPoint.Y;
                if (myLineEndPoint.X <= 0)
                {
                    myLineEndPoint.X = 0;
                    StartLoop = false;
                }
                if (myLineEndPoint.X > 326)
                {
                    myLineEndPoint.X = 326;
                    StartLoop = false;
                }
                if (myLineEndPoint.Y <= 0)
                {
                    myLineEndPoint.Y = 0;
                    StartLoop = false;
                }
                if (myLineEndPoint.Y > 308)
                {
                    myLineEndPoint.Y = 308;
                    StartLoop = false;
                }
                if (StartLoop == true)
                {
                    break;
                }
                else
                {
                    VelocityForEndingPoint = SetVector();
                }
            }

       

        }
        public void draw(object toDraw)
        {
            
            Graphics drawing = (Graphics)toDraw;
            VelocityForEndingPoint = SetVector();
            VelocityForStartingPoint = SetVector();
            //add line to the list
            MainList.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));

            while (status)
            {
                wait_handle.WaitOne();
                lock (thisLock)
                { 
                   drawS(drawing);
                   Thread.Sleep(5);
                }
            }

        }

       
      


        }
}
