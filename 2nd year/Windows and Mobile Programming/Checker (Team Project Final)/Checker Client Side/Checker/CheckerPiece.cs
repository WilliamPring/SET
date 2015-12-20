/*    
 * Filename: CheckerPiece.cs
 * Assignment: WMP Final Project 
 * By: Naween Mehanmal and William Pring, Denys Politiuk
 * Date: December 16, 2015
 * Description: This Class will be for the internal checker piece that will have logic behind it
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Checker
{

    /*
     Name: Checker 
     Purpose: The Checker Class for the Checker Pieces that will generated for 2 side the red and organge
     Data Members :  None
     Type: None
    */
    public class CheckerPiece
    {
        //true reprsent white and false represent black
        private bool colorStatus;
        //x pos
        private int xPos;
        //y pos
        private int yPos;
        private SolidColorBrush checkerColor;
        bool isKing;
        /*
            Name: CheckerPiece()
            Description: Constructor the Checker Piece that take 4 parmeter
            Parameter(s): int xPos, int yPos, bool colorStatus, bool isKing
            Return: Nothing
        */
        public CheckerPiece(int xPos, int yPos, bool colorStatus, bool isKing)
        {
            //Positions for x
            this.xPos = xPos;
            //Position for Y
            this.yPos = yPos;
            this.isKing = isKing;
            //Image related data members
            this.colorStatus = colorStatus;


        }

        //Default
        public CheckerPiece()
        {

        }


        /* Properties */

        
        public int XPos
        {
            get
            {
                return xPos;
            }
        }

        public int YPos
        {
            get
            {
                return yPos;
            }
        }
           


        public SolidColorBrush CheckerColor
        {
            get
            {
                return checkerColor;
            }
        }
        
    }
}

