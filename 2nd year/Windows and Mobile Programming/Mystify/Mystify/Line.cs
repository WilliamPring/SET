/*
File name: Line.cs
Project: Mystify
By: William Pring 
Date: October 23, 2015
Description: Recreate Mystify in Windows allowing users to add sticks, pause, resume and close the thread 
gracefully
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Mystify
{
    /*
   Name: Program 
   Purpose: Line class which will change veloctiy of the points, create points, draw points and a thread that creates lines
   Type: None
    */

    class Line
    {

        /*
           Name: Line
           Purpose: Defines and declares the specifics and functionality of what my Line would be able to do 
           Data Members : thisLock - Lock important resources so other threads must wait 
                          wait_handle - Will pause threads depending on the signel 
                          myLineStartPoint- A Point type which is the starting point
                          myLineEndPoint - A Point type which is the ending point
                          VelocityForEndingPoint - A end point for the myLineEndPoint Speed
                          VelocityForStartingPoint A Start point for the myLineStartPoint Speed
                          rnd - random number 
                          status - for the loop so the thread will continue to do it purpose
                          MainList - conatiner for the list of lines (each thread will have it own threads)
           Type:  Nothing

            */
        //a lock for my lines so other thread cannot acess it
        private static Object thisLock = new Object();
        //This will be use throughout the program so it can puase threads
        public volatile static ManualResetEvent wait_handle = new ManualResetEvent(true);
        /// <summary>
        /// My pen
        /// </summary>
        private Pen myPen;
        /// <summary>
        /// My line start point
        /// </summary>
        private Point myLineStartPoint;
        /// <summary>
        /// My line end point
        /// </summary>
        private Point myLineEndPoint;
        //velocity 
        /// <summary>
        /// The velocity for ending point
        /// </summary>
        private Point VelocityForEndingPoint = new Point();
        /// <summary>
        /// The velocity for starting point
        /// </summary>
        private Point VelocityForStartingPoint = new Point();
        /// <summary>
        /// The random
        /// </summary>
        private Random rnd = new Random();
        /// <summary>
        /// The status
        /// </summary>
        public static volatile bool status = true;
        /// <summary>
        /// The line count
        /// </summary>
        public static int lineCount = 4;
        //containers of lines
        /// <summary>
        /// The main list
        /// </summary>
        List<Line> MainList = new List<Line>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        public Line()
        {
            myPen = new Pen(Color.Red, 1);
            myLineEndPoint = new Point(0, 0);
            myLineEndPoint = new Point(0, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="myLineStartPoint">My line start point.</param>
        /// <param name="myLineEndPoint">My line end point.</param>
        /// <param name="myPen">My pen.</param>
        public Line(Point myLineStartPoint, Point myLineEndPoint, Pen myPen)
        {
            this.myLineStartPoint = myLineStartPoint;
            this.myLineEndPoint = myLineEndPoint;
            this.myPen = myPen;
        }



        /// <summary>
        /// Sets the vector.
        /// </summary>
        /// <returns>directionToMove</returns>
        private Point SetVector()
        {
            //random number
            int vector = rnd.Next(1, 8);
            Point directionToMove = new Point();
            //the different cordinates that one can use each number represnt N, NE, E, ES, S, SW, W, WN, N
            //each change represents slope on a graph so rise/run
            if (vector == 1)
            {
     
                directionToMove.X = -4;
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
                directionToMove.X = -4;
                directionToMove.Y = 0;
            }
            //return the move which is the points
            return directionToMove;
        }

        /// <summary>
        /// Draws the ln.
        /// </summary>
        /// <param name="ShadowLines">The shadow lines.</param>
        public void drawLn(Graphics ShadowLines)
        {
                //main uses to delete the line
                Point tempEnd = new Point();
                Point tempStart = new Point();
                

                if (MainList.Count == lineCount)
                {
                    //get the start point
                    tempStart = MainList.ElementAt(0).myLineStartPoint;
                // get the end ping
                    tempEnd = MainList.ElementAt(0).myLineEndPoint;
                //set the pen color to white
                    myPen.Color = Color.White;
                //draw the line so it looks like it disapper
                    ShadowLines.DrawLine(myPen, tempStart, tempEnd);
                //remove from list
                    MainList.RemoveAt(0);
                //set the colour back to red
                    myPen.Color = Color.Red; 
        
                }
                else
                {
                //call the move function
                    Move();
                //get the points back and draw the line
                    ShadowLines.DrawLine(myPen, myLineStartPoint, myLineEndPoint);
                //added the drawed line to the list 
                    MainList.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
                }
          

        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public void Move()
      {
            
            bool StartLoop = true;
            for (;;)
            {
                StartLoop = true;
                myLineStartPoint.X = myLineStartPoint.X + VelocityForStartingPoint.X;
                myLineStartPoint.Y = myLineStartPoint.Y + VelocityForStartingPoint.Y;
                //goes over the width
                if (myLineStartPoint.X < 0)
                {
                    myLineStartPoint.X = 0;
                    StartLoop = false;
                }
                //goes over the width
                if (myLineStartPoint.X > 326)
                {
                    myLineStartPoint.X = 326;
                    StartLoop = false;
                }
                //goes over the widht
                if (myLineStartPoint.Y < 0)
                {
                    myLineStartPoint.Y = 0;
                    StartLoop = false;
                }
                //goes over the width
                if (myLineStartPoint.Y > 308)
                {
                    myLineStartPoint.Y = 308;
                    StartLoop = false;
                }
                //if the startloop is true which means its sucessful under the conditions
                //leave the loop 
                if (StartLoop == true)
                {
                    break;
                }
                else
                {
                    //if not sucesfull then change the vector so get new point
                    VelocityForStartingPoint = SetVector();
                }
            }
            //this is the same logic as above for this is changing the other point which the is the ending point
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


        /// <summary>
        /// Draws the specified to draw.
        /// </summary>
        /// <param name="toDraw">To draw.</param>
        public void draw(object toDraw)
        {
            
            Graphics drawing = (Graphics)toDraw;
            VelocityForEndingPoint = SetVector();
            VelocityForStartingPoint = SetVector();
            //add line to the list
            MainList.Add(new Line(myLineStartPoint, myLineEndPoint, myPen));
            //loop until status is equal true
            while (status)
            {
                //pause threads
                wait_handle.WaitOne();
                //lock threads
                lock (thisLock)
                { 
                   drawLn(drawing);
                    //sleep threads
                    Thread.Sleep(5);
                }
            }

        }

       
      


        }
}
