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
        private Pen myPen;
        private ThreadRepository tRepoStartShadowLine;
        private Point myLineStartPoint;
        private Point myLineEndPoint;
        private Random rnd = new Random();
        private bool status;
        private static Object thisLock = new Object();
        private int vector;
        private int borderX;
        private int borderY; 

        public Line()
        {
            tRepoStartShadowLine = new ThreadRepository();
            myPen = new Pen(Color.Red, 1);
            vector = 0;
            status = true;
            myLineEndPoint = new Point(0, 0);
            myLineEndPoint = new Point(0, 0);
        }
        public Line(Point myLineStartPoint, Point myLineEndPoint, Pen myPen)
        {
            this.myLineStartPoint = myLineStartPoint;
            this.myLineEndPoint = myLineEndPoint;
            this.myPen = myPen;
            status = true;
           
        }


        public void changeVelocityForEndPoint(MoveLine whereToMoveForEnd)
        {
            lock (thisLock)
            {
                Main_Mystify temp = new Main_Mystify();
                int randomNumber = 0;
                vector = rnd.Next(1, 8);
                randomNumber = rnd.Next(-5, 10);
                int checkForBounce = 0;

                int width = temp.MaxXScreen;
                int height = temp.MaxYScreen;
                if (vector == 1)
                {

                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y + 8 + randomNumber;


                }
                //NORTH EAST
                else if (vector == 2)
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X + 8 + randomNumber;
                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y + 8 + randomNumber;


                }
                //EAST
                else if (vector == 3)
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X + 8 + randomNumber;

                }
                //EAST South
                else if (vector == 4)
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X + 8 + randomNumber;
                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y - 8 + randomNumber;

                }
                //South
                else if (vector == 5)
                {
                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y - 8 + randomNumber;

                }
                //south West
                else if (vector == 6)
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X - 8 + randomNumber;
                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y - 8 + randomNumber;

                }
                //West
                else if (vector == 7)
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X - 8 + randomNumber;

                }
                //West North
                else
                {
                    whereToMoveForEnd.myPrevEnd.X = whereToMoveForEnd.myPrevEnd.X - 8 + randomNumber;

                    whereToMoveForEnd.myPrevEnd.Y = whereToMoveForEnd.myPrevEnd.Y + 8 + randomNumber;

                }
            }
        }

        public void changeVelocityForStartPoint(MoveLine whereToMoveForStart)
        {
            Main_Mystify temp = new Main_Mystify();

            int width = temp.MaxXScreen;
            int height = temp.MaxYScreen;
            int randomNumber = 0;
            Main_Mystify tempCheckBoxRange = new Main_Mystify();
            randomNumber = rnd.Next(-10, 2);
            int tryingScreenSize = 0;
            int checkForBounce = 0;
            vector = rnd.Next(1, 8);
            //if vector is North 
            if (vector == 1)
            {
                checkForBounce = whereToMoveForStart.myPrevStart.Y + 5 + randomNumber;
                if (checkForBounce > height)
                {

                }
                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y + 5 + randomNumber;

            }
            //NORTH EAST
            else if (vector == 2)
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X + 5 + randomNumber;

                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y + 5 + randomNumber;


            }
            //EAST
            else if (vector == 3)
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X + 5 + randomNumber;
            }
            //EAST South
            else if (vector == 4)
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X + 5 + randomNumber;
                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y - 5 + randomNumber;
            }
            //South
            else if (vector == 5)
            {
                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y - 5 + randomNumber;
            }
            //south West
            else if (vector == 6)
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X - 5 + randomNumber;
                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y - 5 + randomNumber;
            }
            //West
            else if (vector == 7)
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X - 5 + randomNumber;
            }
            //West North
            else
            {
                whereToMoveForStart.myPrevStart.X = whereToMoveForStart.myPrevStart.X - 5 + randomNumber;
                whereToMoveForStart.myPrevStart.Y = whereToMoveForStart.myPrevStart.Y + 5 + randomNumber;

            }
        }


        private Point SetVector()
        {
            int vector = rnd.Next(1, 8);
            Point directionToMove = new Point();

            if (vector == 1)
            {
                directionToMove.X = -7;
                directionToMove.Y = 7;
            }

            if (vector == 2)
            {
                directionToMove.X = 0;
                directionToMove.Y = 7;
            }

            if (vector == 3)
            {
                directionToMove.X = 7;
                directionToMove.Y = 7;
            }

            if (vector == 4)
            {
                directionToMove.X = 7;
                directionToMove.Y = 0;
            }

            if (vector == 5)
            {
                directionToMove.X = 7;
                directionToMove.Y = -7;
            }

            if (vector == 6)
            {
                directionToMove.X = 0;
                directionToMove.Y = -7;
            }

            if (vector == 7)
            {
                directionToMove.X = -7;
                directionToMove.Y = -7;
            }

            if (vector == 8)
            {
                directionToMove.X = -7;
                directionToMove.Y = 0;
            }

            return directionToMove;
        }



        public void drawShadowLine(object drawingShadowLines)
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
                    tempDrawingShadowLines.myTempPen.Color = Color.Red;
       
                }
                else
                {
                    //changeVelocityForStartPoint(tempDrawingShadowLines);
                    //changeVelocityForEndPoint(tempDrawingShadowLines);
                    tempDrawingShadowLines.toDraw.DrawLine(tempDrawingShadowLines.myTempPen, tempDrawingShadowLines.myPrevStart, tempDrawingShadowLines.myPrevEnd);
                    tempDrawingShadowLines.listToPass.Add(new Line(tempDrawingShadowLines.myPrevStart, tempDrawingShadowLines.myPrevEnd, tempDrawingShadowLines.myTempPen));
                    //drawingShadowLines = myTempStruct;
                    Thread.Sleep(50);
                }
            }
        


        public void drawS(MoveLine drawingShadowLines)
        {
            lock (thisLock)
            {

                Point tempEnd = new Point();
                Point temStart = new Point();


                if (drawingShadowLines.listToPass.Count == 7)
                {

                    //start temp
                    temStart = drawingShadowLines.listToPass.ElementAt(0).myLineStartPoint;
                    //end temp
                    tempEnd = drawingShadowLines.listToPass.ElementAt(0).myLineEndPoint;
                    drawingShadowLines.myTempPen.Color = Color.White;
                    drawingShadowLines.toDraw.DrawLine(drawingShadowLines.myTempPen, temStart, tempEnd);
                    drawingShadowLines.listToPass.RemoveAt(0);
                    drawingShadowLines.myTempPen.Color = Color.Red;
        
                }
                else
                {
                    //changeVelocityForStartPoint(drawingShadowLines);
                    // changeVelocityForEndPoint(drawingShadowLines);
                    Move(drawingShadowLines);
                    drawingShadowLines.toDraw.DrawLine(drawingShadowLines.myTempPen, drawingShadowLines.myPrevStart, drawingShadowLines.myPrevEnd);
                    drawingShadowLines.listToPass.Add(new Line(drawingShadowLines.myPrevStart, drawingShadowLines.myPrevEnd, drawingShadowLines.myTempPen));
                    //drawingShadowLines = myTempStruct;
                    Thread.Sleep(80);
                }
            }

        }

        public void Move(MoveLine moveIt)
        {
            moveIt.myPrevStart.X = moveIt.myPrevStart.X + moveIt.VelocityForStartingPoint.X;
            moveIt.myPrevStart.Y = moveIt.myPrevStart.Y + moveIt.VelocityForStartingPoint.Y;
            moveIt.myPrevEnd.X = moveIt.myPrevEnd.X  + moveIt.VelocityForEndingPoint.X;
            moveIt.myPrevEnd.Y = moveIt.myPrevEnd.Y + moveIt.VelocityForEndingPoint.Y;

        }
        public void draw(object toDraw)
        {
            MoveLine shadowLines = new MoveLine();
            Line temp = new Line();
            Graphics drawing = (Graphics)toDraw;
            List<Line> Main = new List<Line>();
            //filling up class
            shadowLines.toDraw = drawing;
            shadowLines.listToPass = Main;
            shadowLines.myTempPen = myPen;
            shadowLines.myPrevEnd = myLineEndPoint;
            shadowLines.myPrevStart = myLineStartPoint;
            shadowLines.VelocityForEndingPoint = SetVector();
            shadowLines.VelocityForStartingPoint = SetVector();
            //add line to the list
            shadowLines.listToPass.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
            
            while (status)
            {
                lock (thisLock)
                {
                    //draw the first line
                    //tRepoStartShadowLine.AddClass("ShadowLine", new ParameterizedThreadStart(temp.drawShadowLine), shadowLines);
                    drawS(shadowLines); 
                }
            }

        }

       
      


        }
}
