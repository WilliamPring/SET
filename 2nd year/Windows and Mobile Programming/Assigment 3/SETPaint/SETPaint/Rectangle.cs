/*
File name: Rectangle.cs
Project: Assigment 3 SETPaint
By: William Pring and Naween Mehanmal
Date: October 7, 2015
Description: This class inherits from the Shape class and include the function that will be needed 
to draw the rectangle
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
        Name: Rectangle 
        Purpose: Defines and declares the specifics and functionality of what an derived Shape class
        Data Members :  None
        Type: Protected Inheritance, the members are inherited from the parent class and are only relevant to the parent and child class
    */
    class Rectangle : Shape
    {
        /******************* Constructor *******************/

        public Rectangle() : base()
        {          
        }

        public Rectangle(Point myPenStartPoint, Point myPenEndPoint)
        {
            this.myPenStartPoint = myPenStartPoint;
            this.myPenEndPoint = myPenEndPoint;
        }

        /******************* Overrided Methods *******************/

        public override void Draw(Graphics toDraw)
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            int height = 0;
            int width = 0; 

            if ((myPenStartPoint.X < myPenEndPoint.X) && (myPenStartPoint.Y < myPenEndPoint.Y))
            {
                xCoordinate = myPenStartPoint.X;
                yCoordinate = myPenStartPoint.Y;
                width = myPenEndPoint.X - myPenStartPoint.X;
                height = myPenEndPoint.Y - myPenStartPoint.Y;
            }
            else if((myPenStartPoint.X > myPenEndPoint.X) && (myPenStartPoint.Y > myPenEndPoint.Y))
            {
                xCoordinate = myPenEndPoint.X;
                yCoordinate = myPenEndPoint.Y;
                width = myPenStartPoint.X - myPenEndPoint.X;
                height = myPenStartPoint.Y - myPenEndPoint.Y;
            }
            else if((myPenStartPoint.X < myPenEndPoint.X) && (myPenStartPoint.Y > myPenEndPoint.Y))
            {
                xCoordinate = myPenStartPoint.X;
                yCoordinate = myPenEndPoint.Y;
                width = myPenEndPoint.X - myPenStartPoint.X;
                height = myPenStartPoint.Y - myPenEndPoint.Y;
            }
            else if((myPenStartPoint.X > myPenEndPoint.X) && (myPenStartPoint.Y < myPenEndPoint.Y))
            {
                xCoordinate = myPenEndPoint.X;
                yCoordinate = myPenStartPoint.Y;
                width = myPenStartPoint.X - myPenEndPoint.X;
                height = myPenEndPoint.Y - myPenStartPoint.Y;
            }
            toDraw.DrawRectangle(myPen, xCoordinate, yCoordinate, width, height);
            toDraw.FillRectangle(myBrush, xCoordinate, yCoordinate, width, height);
        }
    }
}
