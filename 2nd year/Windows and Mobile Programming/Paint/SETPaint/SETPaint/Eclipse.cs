using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETPaint
{
    /*
     Name: Ellipse 
     Purpose: Defines and declares the specifics and functionality of what an derived Shape class

     Data Members :  None

     Type: Protected Inheritance, the members are inherited from the parent class and are only relevant to the parent and child class
    */
    class Ellipse : Shape
    {
        /******************* Constructor *******************/

        public Ellipse() : base()
        {
        }

        public Ellipse(Point myPenStartPoint, Point myPenEndPoint)
        {
            this.myPenStartPoint = myPenStartPoint;
            this.myPenEndPoint = myPenEndPoint;
        }

        /******************* Overrided Methods *******************/

        public override void Draw(Graphics toDraw)
        {
            toDraw.DrawEllipse(myPen, myPenStartPoint.X, myPenStartPoint.Y, myPenEndPoint.X - myPenStartPoint.X, myPenEndPoint.Y - myPenStartPoint.Y);
            toDraw.FillEllipse(myBrush, myPenStartPoint.X, myPenStartPoint.Y, myPenEndPoint.X - myPenStartPoint.X, myPenEndPoint.Y - myPenStartPoint.Y);
        }
    }
}
