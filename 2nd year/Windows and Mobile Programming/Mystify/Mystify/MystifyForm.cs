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


        /// <summary>
        /// Initializes a new instance of the <see cref="Main_Mystify"/> class.
        /// </summary>
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
            //random point for x
            randomX = rndPoint.Next(0, pnScreen.Width);
            //random point for x1
            randomX1 = rndPoint.Next(0, pnScreen.Width);
            //random point for y
            RandomY = rndPoint.Next(0, pnScreen.Height );
            //random point Y1
            RandomY1 = rndPoint.Next(0 , pnScreen.Height );
            //start point with the x and y
            startLinePoint = new Point(randomX, RandomY);
            //end point 
            endLinePoint = new Point(randomX1, RandomY1);
            myLine =  new Line(startLinePoint, endLinePoint, penType);
            //name thread and add it the function and pass the graphic var
            tRepo.Add("Th", new ParameterizedThreadStart(myLine.draw), g);
           
        }

        private void bnResume_Click(object sender, EventArgs e)
        {
            //resume the waitone
            Line.wait_handle.Set();
            bnNewStick.Enabled = true;

        }

        private void bnEnd_Click(object sender, EventArgs e)
        {
            Line.wait_handle.Set();
            //end the loop
            Line.status = false;
            //join all threads
            tRepo.JoinAll();
            //clear the list
            tRepo.EndAll(); 
            //set the screen to white
            pnScreen.BackColor = Color.White;
            //refresh the screen
            pnScreen.Invalidate();
            bnNewStick.Enabled = true;
            //set it back to true just in case person want to add new sticks again
            Line.status = true;

        }

        private void Main_Mystify_FormClosing(object sender, FormClosingEventArgs e)
        {
            //end the threads loop
            Line.status = false;
            Line.wait_handle.Set();
            //wait
            tRepo.JoinAll();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //set the line count
            Line.lineCount = trackBar1.Value; 
        }
    }       
}