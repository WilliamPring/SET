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
            rnd = newRnd.Next(-5, 5);
        }

        public void MoveLine(Graphics drawing)
        {

        }
        public void drawShadowLine(object drawingShadowLines)
        {
            
            lineInfo myTempStruct = new lineInfo();
            myTempStruct = (lineInfo)drawingShadowLines;
            drawingShadowLines = myTempStruct;
           
            RandomNumber();
            myLineStartPoint.X = myLineStartPoint.X - rnd;
            RandomNumber();
            myLineStartPoint.Y = myLineStartPoint.Y - rnd;
            RandomNumber();
            myLineEndPoint.X = myLineEndPoint.X - rnd;
            RandomNumber();
            myLineEndPoint.Y = myLineEndPoint.Y - rnd;
            myTempStruct.toDraw.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
            myTempStruct.listToPass.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
            drawingShadowLines = myTempStruct;
            
        
        }
        public void draw(object toDraw)
        {
            Line temp = new Line(); 
            //casting a new graphinc to the object
            Graphics drawing = (Graphics)toDraw;
            //list of lines
            List<Line> Main = new List<Line>();
            //declaring a struct
            lineInfo myStruct= new lineInfo();
            //setting the list to eachother
            myStruct.listToPass = Main;
            //setting each graphic to each other
            myStruct.toDraw = drawing;
            //adding the fisrt Line in the list
            myStruct.listToPass.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));              
            //ListOfThreadOfLine.Add(drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
            while (status)
            {
                lock (thisLock)
                {
                    //draw the first line
                    drawing.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                    tRepoStartShadowLine.AddStuct("ShadowLine", new ParameterizedThreadStart(temp.drawShadowLine), ref myStruct);
                    Thread.Sleep(50);
                }
            }

        }

       
            /*
            myLine.Draw();
            Thread.Sleep(200);
            myLine.Move();
            new Thread (function for tail lines) ;

            myLine.Draw();
                -> Graphics;
                drawTheLine on the graphics/ Bitmap /...
            */


        }
}
