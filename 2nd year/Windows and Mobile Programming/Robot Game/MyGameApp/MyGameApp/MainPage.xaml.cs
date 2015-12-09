using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



using Windows.UI.Xaml.Shapes;
using Windows.UI; 

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyGameApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private int canvasRows;
        private int canvasColumns;

        private int canvasYCell;
        private int canvasXCell;


        Random randBomb;        

        List<Rectangle> bombList;  //list ofthe bomb being stored
        Random bombToDelete;  //what bomb to delete in the bombList

        int x; //Represent the X Grid
        int y; //Represent the Y Grid

        int maxBombCount; //The max bomb that can be in this program

        int currBomb; //the current amount of bomb in this project
        int positonOfBombToDelete; //which bomb to delete



        public MainPage()
        {
            this.InitializeComponent();

            randBomb     = new Random();
            bombToDelete = new Random();

            bombList = new List<Rectangle>();

            currBomb = 0;
            positonOfBombToDelete = 0;

            x = 8;
            y = 8;

            maxBombCount = (int)(x * y * (0.40)); //Formula for max number of mines 
            currBomb = randBomb.Next(10, maxBombCount - 5); //Calculate number of mines for the game       
     
            
            



        }


        private void Main_Page_Loaded_Event(object sender, RoutedEventArgs e)
        {
            this.canvasRows = (int)myCanvas.ActualHeight;
            this.canvasColumns = (int)myCanvas.ActualWidth;


            this.canvasXCell = canvasColumns / 8;
            this.canvasYCell = canvasRows / 8;

            myRobot.Width  = (double)canvasXCell;
            myRobot.Height = (double)canvasXCell;

            int spacesDown  = 0;
            int spacesRight = 0; 


            //Create and add all the bombs to the game
            for (int i = 0; i < currBomb; i++)
            {
                //Create rectangle dynamically

                Rectangle mineBox = new Rectangle();
                //Always have it the same size and colour
                mineBox.Width  = canvasXCell / 2;
                mineBox.Height = canvasXCell;
                mineBox.Fill = new SolidColorBrush(Colors.LightGreen);

                //Randomizing the margin it is located from the canvas and screen layout
                //mineBox.Margin = new Thickness(randBomb.Next(0, canvasColumns) - 50, randBomb.Next(0, canvasRows) - 100, randBomb.Next(0, canvasRows) + 100, randBomb.Next(0, canvasColumns) + 50);

                spacesRight = randBomb.Next(1, 7);
                spacesDown  = randBomb.Next(1, 11);               


                mineBox.Margin = new Thickness(canvasXCell * spacesRight, canvasXCell * spacesDown, 0, 0);

                myCanvas.Children.Insert(0, mineBox); //Insert it into the canvas 

                bombList.Add(mineBox); //Add to the total mine list in the game 
            }


           // spacesDown = canvasXCell;





        }

        /** Click Related Event Handlers **/

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            yAxisStoryBoard.Begin();

            myRobotAnimationYAxis.From = this.canvasRows - this.canvasXCell;
            myRobotAnimationYAxis.To = 0;


            


        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            yAxisStoryBoard.Begin();

            myRobotAnimationYAxis.From = 0;
            myRobotAnimationYAxis.To = this.canvasRows - this.canvasXCell; //- this.canvasXCell;
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            xAxisStoryBoard.Begin();

            myRobotAnimationXAxis.From = 0;
            myRobotAnimationXAxis.To = this.canvasColumns - this.canvasXCell;
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            xAxisStoryBoard.Begin();

            myRobotAnimationXAxis.From = this.canvasColumns - this.canvasXCell;
            myRobotAnimationXAxis.To = 0;
        }
    }
}
