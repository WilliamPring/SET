using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAD
{
    public partial class GameUI : Form
    {
        System.Drawing.Graphics graphicsObj;
        Pen myPen;
        Rectangle rectangle;
        SolidBrush solidBrush;
        int yPos;
        int xPos;
        int widthBorder;
        Random rand;
        public GameUI()
        {
            InitializeComponent();
            xPos = 2;
            yPos = 2;
            widthBorder = 1;
            graphicsObj = displayPnl.CreateGraphics();
            myPen = new Pen(Color.Black, widthBorder);
            rectangle = new Rectangle(xPos, yPos, 50, 50);
            solidBrush = new SolidBrush(Color.PaleVioletRed);
            rand = new Random();
        }
        private void bttnRight_Click(object sender, EventArgs e)
        {
            if ((xPos + 54 + widthBorder) <= displayPnl.Width)
            {
                xPos += 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
            else
            {
                int t = 0;
                t++;
            }
        }
        private void bttnUp_Click(object sender, EventArgs e)
        {
            if (yPos >=0)
            {
                yPos -= 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }
        private void bttnDown_Click(object sender, EventArgs e)
        {
            if ((yPos + 54 + widthBorder) <= displayPnl.Height)
            {
                yPos += 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }

        private void bttnLeft_Click(object sender, EventArgs e)
        {
            if (xPos >= 0)
            {
                xPos -= 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }

        private void displayPnl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(solidBrush, rectangle);
            graphicsObj.DrawRectangle(myPen, rectangle);
        }

        private void bttnFill_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogFilling.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                solidBrush = new SolidBrush(colorDialogFilling.Color);
                displayPnl.Invalidate();
            }
        }

        private void bttnBorder_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogFilling.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                myPen = new Pen(colorDialogFilling.Color);
                displayPnl.Invalidate();
            }
        }

        private void bttnSendRandMove_Click(object sender, EventArgs e)
        {
            int randomMove = rand.Next(1, 4);
            //up
            if (randomMove==1)
            {
                bttnUp_Click(sender, e);
            }
            //down
            else if (randomMove ==2)
            {
                bttnDown_Click(sender, e);
            }
            //left
            else if(randomMove ==3)
            {
                bttnLeft_Click(sender, e);
            }
            //right
            else
            {
                bttnRight_Click(sender, e);
            }
        }
    }
}
