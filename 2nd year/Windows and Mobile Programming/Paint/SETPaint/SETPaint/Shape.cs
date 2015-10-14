/*
File name: Shape.cs
Project: Assigment 3 SETPaint
By: William Pring and Naween Mehanmal
Date: October 7, 2015
Description: This is the main class that will deal with the main form
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETPaint
{
    /*
       Name: Shape
       Purpose: Defines and declares the specifics and functionality of what an abstract vehicle class would normally contain 

       Data Members :  myPen - A Pen type in which has to specify the width and colour
                       myBrush - 
                       myPenStartPoint- A Point type which is the starting point of the mouse
                        myPenEndPoint - A Point type which is the ending point of the mouse 
       Type:  Nothing
   */
    abstract class Shape
    {
        /******************* Data Mambers *******************/

        protected Pen myPen;
        protected SolidBrush myBrush; 

        protected Point myPenStartPoint;
        protected Point myPenEndPoint;

        /******************* Constructor *******************/

        public Shape()
        {
            myPen = new Pen(Color.Black);
            myBrush = new SolidBrush(Color.White);
            myPenEndPoint = new Point(0, 0);
            myPenStartPoint = new Point(0, 0);
        }

        public Shape(Point myPenStartPoint, Point myPenEndPoint, Pen myPen)
        {
            this.myPenStartPoint = myPenStartPoint;
            this.myPenEndPoint = myPenEndPoint;
            this.myPen = new Pen(myPen.Color, myPen.Width);
        }

        /******************* Properties *******************/

        public SolidBrush MyBrush
        {
            get
            {
                return myBrush; 
            }
            set
            {
                myBrush = value; 
            }
        }

        public Point startingPoint
        {
            get
            {
                return myPenStartPoint;
            }
            set
            {
                myPenStartPoint = value;
            }
        }

        public Point endingPoint
        {
            get
            {
                return myPenEndPoint;
            }
            set
            {
                myPenEndPoint = value;
            }
        }

        public Pen MyPen
        {
            get
            {
                return myPen;
            }
            set
            {
                myPen.Color = value.Color;
                myPen.Width = value.Width; 
            }
        }

        /******************* Abstract Method *******************/

        abstract public void Draw(Graphics toDraw);
        
    }
}
