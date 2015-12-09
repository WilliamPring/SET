using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameApp
{
    class Bomb
    {
        /** DATA MEMBERS **/

        int xPosition;
        int yPosition;
        
        int RectPointA;
        int RectPointB;
        int RectPointC;
        int RectPointD;

        string color;



        /** METHODS **/

        public Bomb(int xPosition, int yPosition, string color)
        {
            this.XPosition = xPosition;
            this.YPosition = yPosition;
            this.color = color;
        }



        /** PROPERTIES **/

        public int XPosition
        {
            get
            {
                return xPosition;
            }

            set
            {
                xPosition = value;
            }
        }

        public int YPosition
        {
            get
            {
                return yPosition;
            }

            set
            {
                yPosition = value;
            }
        } 
    }
}
