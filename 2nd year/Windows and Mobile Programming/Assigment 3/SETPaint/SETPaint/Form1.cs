using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    public partial class SETPaint : Form
    {
        private int penWidth;
        private Point p0, p1, p2, p3;
        //private CrossHair ch1, ch2, ch3, ch4;
        private Color penColour = new Color();
        private Point[] DifferentPoint = new Point[4];
        private bool mouseDown;

        public SETPaint()
        {
            penColour = Color.Blue;
            DifferentPoint[0] = p0;
            DifferentPoint[1] = p1;
            DifferentPoint[2] = p2;
            DifferentPoint[3] = p3;
            penWidth = 0;
            mouseDown = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {   
            //set the value of the width
            penWidth = LineThickness.Value;
            //Invalidates the entire surface of the control and causes the control to be redrawn.
            pnDrawScreen.Invalidate();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbEllipse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Invalidates the entire surface of the control and causes the control to be redrawn.
            
        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            statusBar.Text = string.Format("X: {0} , Y: {0} ");

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            //putting the defult colour to equal to pen colour which is blue in this case
            cDialog.Color = penColour;
            //show the different colours
            cDialog.ShowDialog();
            //change colour base on user desire
            penColour = cDialog.Color;
            //Invalidates the entire surface of the control and causes the control to be redrawn.
            pnDrawScreen.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
