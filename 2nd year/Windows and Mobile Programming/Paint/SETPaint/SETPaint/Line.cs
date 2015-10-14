/*
File name: Line.cs
Project: Assigment 3 SETPaint
By: William Pring and Naween Mehanmal
Date: October 7, 2015
Description: This is the class for in which any Rectangle and Eclipse inherit from
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
    Name: Line
       Purpose: Defines and declares the specifics and functionality of what an abstract vehicle class would normally contain
       Data Members: None
       Type: Abstract - So that this is the base class for other sub-classes, and so that an object cannot be instantiated from this class
       */
    class Line : Shape
    {
        /******************* Constructor *******************/

        public Line() : base()
        {            
        }

        public Line(Point myPenStartPoint, Point myPenEndPoint)
        {
            this.myPenStartPoint = myPenStartPoint;
            this.myPenEndPoint = myPenEndPoint;
        }

        /******************* Overrided Methods *******************/

        override public void Draw(Graphics toDraw)
        {
            toDraw.DrawLine(myPen, myPenStartPoint, myPenEndPoint);
        }
    }
}
