using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace SETPaint
{
    /*
     Name: FrmSetPaint 
     Purpose:
     Data Members :  None
     Type: Protected Inheritance, Inherits from the Form class
    */
    public partial class FrmSetPaint : Form
    {
        private List<Shape> myShape = new List<Shape>();
        Graphics g;
        private Point initialPoint;
        private Point endPoint;
        private Color penColour = new Color();
        SolidBrush brushColour; 

        /***** Coordinates *****/

        private int X;
        private int Y;
        private int penWidth;

        /***** Status Flags *****/

        private bool drawLineShape;
        private bool drawRectangleShape;
        private bool drawEllipseShape; 
              
        private Pen penType;

        private bool mouseDown;

        /***** Different Shapes *****/ 

        Line newLineShape;
        Rectangle newRectangleShape;
        Ellipse newEllipseShape; 
        

        public FrmSetPaint()
        {
            InitializeComponent();
            X = 0;
            Y = 0;
            penWidth = 5; //Default width size

            penColour = Color.Black;
            brushColour = new SolidBrush(Color.White);
            penType = new Pen(penColour, penWidth);

            drawLineShape      = false;
            drawRectangleShape = false;
            drawEllipseShape   = false;

            endPoint = new Point(0, 0);
            initialPoint = new Point(0, 0); 

            //Create the screen graphics
            g = this.pnDrawScreen.CreateGraphics();

            this.mouseDown = false;

            rbLine.Checked = true;
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
            penType.Width = LineThickness.Value;
            //Invalidates the entire surface of the control and causes the control to be redrawn.
            //pnDrawScreen.Invalidate();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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

        private void rbEllipse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }    

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }


        /************************** Drawing Shape Related Events **************************/

        private void pnDrawScreen_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDown = true;

            if (rbLine.Checked == true)
            {
                initialPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the starting point of the line
                endPoint = pnDrawScreen.PointToClient(Cursor.Position);
                newLineShape = new Line(initialPoint, endPoint);

                newLineShape.MyPen.Color = penType.Color;
                newLineShape.MyBrush.Color = brushColour.Color;
                newLineShape.MyPen.Width = penType.Width;
                newLineShape.MyPen.DashPattern = new float[] {5F, 5F, 5F, 5F};

                drawLineShape      = true; //Handle line
                drawRectangleShape = false;
                drawEllipseShape   = false;

                X = e.X;
                Y = e.Y;
                toolStripStatusLabel1.Text = "X: " + X.ToString() + " Y: " + Y.ToString();
            }
            else if(rbRectangle.Checked == true)
            {
                initialPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the starting point of the line
                endPoint = pnDrawScreen.PointToClient(Cursor.Position);
                newRectangleShape = new Rectangle(initialPoint, endPoint);

                newRectangleShape.MyPen.Color = penType.Color;
                newRectangleShape.MyBrush.Color = brushColour.Color;
                newRectangleShape.MyPen.Width = penType.Width;
                newRectangleShape.MyPen.DashPattern = new float[] {5F, 5F, 5F, 5F}; 

                drawRectangleShape = true; //Handle rectangle 
                drawLineShape      = false;
                drawEllipseShape   = false;

                X = e.X;
                Y = e.Y;
                toolStripStatusLabel1.Text = "X: " + X.ToString() + " Y: " + Y.ToString();
            }
            else if(rbEllipse.Checked == true)
            {
                initialPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the starting point of the line
                endPoint = pnDrawScreen.PointToClient(Cursor.Position);
                newEllipseShape = new Ellipse(initialPoint, endPoint);

                newEllipseShape.MyPen.Color = penType.Color;
                newEllipseShape.MyBrush.Color = brushColour.Color;
                newEllipseShape.MyPen.Width = penType.Width;
                newEllipseShape.MyPen.DashPattern = new float[] { 5F, 5F, 5F, 5F };

                drawEllipseShape   = true; //Handle ellipse 
                drawLineShape      = false;
                drawRectangleShape = false;

                X = e.X;
                Y = e.Y;
                toolStripStatusLabel1.Text = "X: " + X.ToString() + " Y: " + Y.ToString();
            }
        }


        private void pnDrawScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseDown == true)
            {
                try
                {
                    pnDrawScreen.Refresh();

                    if (drawLineShape)
                    {
                        newLineShape.endingPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the end point 
                        newLineShape.Draw(g);
                    }
                    else if (drawRectangleShape)
                    {
                        newRectangleShape.endingPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the end point 
                        newRectangleShape.Draw(g);
                    }
                    else if (drawEllipseShape)
                    {
                        newEllipseShape.endingPoint = pnDrawScreen.PointToClient(Cursor.Position); //Get the end point 
                        newEllipseShape.Draw(g);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }   
        }  


        private void pnDrawScreen_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;

            if (drawLineShape)
            {
                myShape.Add(newLineShape);                
            }
            else if (drawRectangleShape)
            {
                myShape.Add(newRectangleShape);
            }
            else if (drawEllipseShape)
            {
                myShape.Add(newEllipseShape);
            }

            pnDrawScreen.Invalidate();      
        }


        private void pnDrawScreen_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                foreach (Shape selectShape in myShape)
                {
                    selectShape.MyPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    selectShape.Draw(e.Graphics);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }        
        }

        /************************** Check Changes Related Events **************************/

        private void rbRectangle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbLine_CheckedChanged(object sender, EventArgs e)
        {

        }


        /************************** Menu Related Events **************************/
        
        private void saveAsFile_Click(object sender, EventArgs e)
        {
            int width = pnDrawScreen.Size.Width;
            int height = pnDrawScreen.Size.Height;

            SaveFileDialog saveFileFromDialog = new SaveFileDialog();
            saveFileFromDialog.InitialDirectory = "c:\\";
            saveFileFromDialog.Filter = "Image files (.jpg, .jpeg, .jpe, .jfif, .png) | .jpg; .jpeg; .jpe; .jfif; .png";
            saveFileFromDialog.Title = "Saveing Image";
            Bitmap bitmap = new Bitmap(width, height);
            bitmap = (Bitmap)Image.FromFile(saveFileFromDialog.FileName);
            Bitmap newBitmap = new Bitmap(bitmap);
            bitmap.Dispose();
            newBitmap.Save(saveFileFromDialog.FileName.ToString());
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            string orginalString = "";
            string subString = "";

            OpenFileDialog openFileFromDialog = new OpenFileDialog();
            openFileFromDialog.Filter = "Image files (.jpg, .jpeg, .jpe, .jfif, .png)|.jpg; .jpeg; .jpe; .jfif; .png";
            if (openFileFromDialog.ShowDialog() == DialogResult.OK)
            {
                orginalString = openFileFromDialog.FileName;
                int lastBackSlash = orginalString.LastIndexOf('\\');
                pnDrawScreen.BackgroundImage = Image.FromFile(openFileFromDialog.FileName);
                subString = orginalString.Substring(lastBackSlash + 1);
                this.Text = subString + "- SETPaint";
            }
        }
        /************************** Other Events **************************/
        
        private void fillColourButton_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();

            //putting the defult colour to equal to pen colour which is blue in this case
            cDialog.Color = brushColour.Color;

            //show the different colours
            cDialog.ShowDialog();

            //change colour base on user desire
            brushColour.Color = cDialog.Color;
        }

        private void lineColourButton_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();

            //putting the defult colour to equal to pen colour which is blue in this case
            cDialog.Color = penColour;

            //show the different colours
            cDialog.ShowDialog();

            //change colour base on user desire
            penType.Color = cDialog.Color;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            pnDrawScreen.Invalidate();
            //pnDrawScreen.Refresh();
            
        }

        private void clearScreenButton(object sender, EventArgs e)
        {
            myShape.Clear();
            pnDrawScreen.BackgroundImage = null; //Clear everything on the screen 
            pnDrawScreen.Invalidate();          
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutbox = new AboutBox();
            aboutbox.ShowDialog();
        }
    }
}
