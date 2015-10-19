﻿using System;
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
    public partial class Form1 : Form
    {
        Graphics g;
        private bool run;
        Thread t;
        ThreadRepository tRepo = new ThreadRepository();
        private Point startLinePoint;
        private Point endLinePoint;
        Line myLine = new Line();
        Random rndPoint = new Random();
        private int randomX;
        private int RandomY;
        private int randomX1;
        private int RandomY1;
        private Pen penType;
        private Color penColour = new Color();
        private int penWidth;
        private Object thisLock = new Object();

        public Form1()
        {
            InitializeComponent();
            run = true; 
            penWidth = 2;
            g = this.pnScreen.CreateGraphics();
            pnScreen.BackColor = Color.White; 
            penColour = Color.Black;
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

        }




        private void bnNewStick_Click(object sender, EventArgs e)
        {
            randomX = rndPoint.Next(0, pnScreen.Width);
            randomX1 = rndPoint.Next(0, pnScreen.Width);
            RandomY = rndPoint.Next(0 , pnScreen.Height );
            RandomY1 = rndPoint.Next(0 , pnScreen.Height );
            startLinePoint = new Point(randomX, RandomY);
            endLinePoint = new Point(randomX1, RandomY1);
            myLine =  new Line(startLinePoint, endLinePoint, penType);
            tRepo.Add("Th", new ParameterizedThreadStart(myLine.draw), g);
            Thread.Sleep(1000);
        }
    }       
}