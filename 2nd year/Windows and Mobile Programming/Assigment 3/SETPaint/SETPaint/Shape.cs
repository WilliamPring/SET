using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETPaint
{
    class Shape
    {
        private Pen myPen; 
        public Shape()
        {
            myPen = new Pen(Color.Black);
        }
        public Pen MyPen
        {
            get
            {
                return myPen;
            }

        }
        public Shape(Pen myPen)
        {
            this.myPen = new Pen(myPen.Color, myPen.Width);
        }
        
    }
}
