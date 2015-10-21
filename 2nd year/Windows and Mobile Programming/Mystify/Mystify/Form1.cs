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
    public partial class Main_Mystify : Form
    {
        Graphics g;
        private bool run;
        Thread t;

        ThreadRepository tRepo = new ThreadRepository();
        private Point startLinePoint;
        private Point endLinePoint;
        Line myLine = new Line();
        Random rndPoint = new Random();
        private int maxXScreen;
        private int maxYScreen;
        
        private int randomX;
        private int RandomY;
        private int randomX1;
        private int RandomY1;
        private Pen penType;
        private Color penColour = new Color();
        private int penWidth;
        private Object thisLock = new Object();


        public Main_Mystify()
        {
            InitializeComponent();
            run = true; 
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
            Line.wait_handle.Reset(); 
        }




        private void bnNewStick_Click(object sender, EventArgs e)
        {
            maxXScreen = pnScreen.Width;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bnResume_Click(object sender, EventArgs e)
        {
            Line.wait_handle.Set(); 
        }

        private void bnEnd_Click(object sender, EventArgs e)
        {

        }
    }       
}