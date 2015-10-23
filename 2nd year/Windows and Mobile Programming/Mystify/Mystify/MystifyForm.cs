/*
File name: MystifyForm.cs
Project: Mystify
By: William Pring 
Date: October 23, 2015
Description: The main events that get will call the other functions to start the program
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mystify
{
    /*
          Name: Mystify
          Purpose: Protected Inheritance, Inherits from the Form class
          Data Members :  g - Graphic that deals with draw
                          tRepo - ThreadRepository obj
                          startLinePoint- A Point type which is the starting point
                          endLinePoint - A Point type which is the ending point
                          myLine - A line type that takes color and width
                          rndPoint- create random points
                          maxXScreen - Max Width of the screen
                          maxYScreen - Max Height of the screen
                          randomX - random x for the point
                          randomX1 - random x1 for the point 
                          randomY - random points for the y
                          randomY1 - random points for the y1
                          penType - pen type that his widht and color
                          penColour - Set pen color
                          penWidth - Pen width
                          thisLock - locking objects
          Type:  Nothing

           */

    public partial class Main_Mystify : Form
    {

        
        //the graphic that gets pass around to draw on the screen
        Graphics g;
        ThreadRepository tRepo = new ThreadRepository();
        private Point startLinePoint;
        private Point endLinePoint;
        Line myLine = new Line();
        Random rndPoint = new Random();
        
        //random points
        private int maxXScreen;
        private int maxYScreen;
        private int randomX;
        private int RandomY;
        private int randomX1;
        private int RandomY1;
        //random Points

        //the pen that gets pass with all it attributes
        private Pen penType;
        //set pen colour
        private Color penColour = new Color();
        //set pen width
        private int penWidth;
        //set the lock 
        private Object thisLock = new Object();


        public Main_Mystify()
        {
            InitializeComponent();
            penWidth = 1;
            g = this.pnScreen.CreateGraphics();
            pnScreen.BackColor = Color.White; 
            penColour = Color.Red;
            penType = new Pen(penColour, penWidth);
            startLinePoint = new Point(0, 0);
            endLinePoint = new Point(0, 0);
            randomX = 0;
            RandomY = 0;
            RandomY1 = 0;
            randomX = 0;
        }

        private void pnScreen_Paint(object sender, PaintEventArgs e)
        {

        }

    

        private void bnPause_Click(object sender, EventArgs e)
        {
            //wait_handle() is static and then you set the reset 
            Line.wait_handle.Reset();
            //disable the stick create when in puasue
            bnNewStick.Enabled = false; 
        }




        private void bnNewStick_Click(object sender, EventArgs e)
        {
            //set the max screen width
            maxXScreen = pnScreen.Width;
            //set the max screen height
            maxYScreen = pnScreen.Height;
            randomX = rndPoint.Next(0, pnScreen.Width);
            randomX1 = rndPoint.Next(0, pnScreen.Width);
            RandomY = rndPoint.Next(0, pnScreen.Height );
            RandomY1 = rndPoint.Next(0 , pnScreen.Height );
            startLinePoint = new Point(randomX, RandomY);
            endLinePoint = new Point(randomX1, RandomY1);
            myLine =  new Line(startLinePoint, endLinePoint, penType);
            tRepo.Add("Th", new ParameterizedThreadStart(myLine.draw), g);
           
        }

        private void bnResume_Click(object sender, EventArgs e)
        {
            Line.wait_handle.Set();
            bnNewStick.Enabled = true;

        }

        private void bnEnd_Click(object sender, EventArgs e)
        {
            Line.wait_handle.Set();
            //end the loop
            Line.status = false;
            //
            tRepo.JoinAll();
            tRepo.EndAll(); 
            pnScreen.BackColor = Color.White;
            pnScreen.Invalidate();
            bnNewStick.Enabled = true;
            //set it back to true just in case person want to add new sticks again
            Line.status = true;

        }

        private void Main_Mystify_FormClosing(object sender, FormClosingEventArgs e)
        {
            Line.status = false;
            Line.wait_handle.Set();
            tRepo.JoinAll();
        }
    }       
}