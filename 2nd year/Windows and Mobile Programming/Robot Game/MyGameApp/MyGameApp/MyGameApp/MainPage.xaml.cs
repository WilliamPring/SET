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
    ///     
    public sealed partial class MainPage : Page
    {
        private int canvasRows;
        private int canvasColumns;

        private int canvasYCell;
        private int canvasXCell;


        Random randBomb;        

        List<Rectangle> bombList;  //list ofthe bomb being stored
        List<Rectangle> tilesList; 


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
            tilesList = new List<Rectangle>(); 

            currBomb = 0;
            positonOfBombToDelete = 0;

            x = 8;
            y = 8;

            maxBombCount = (int)(x * y * (0.40)); //Formula for max number of mines 


            



        }
     
        private void Main_Page_Loaded_Event(object sender, RoutedEventArgs e)
        {
            this.canvasRows = (int)myCanvas.ActualHeight;
            this.canvasColumns = (int)myCanvas.ActualWidth;


            this.canvasXCell = canvasColumns / 8;
            this.canvasYCell = canvasRows / 8;

            myRobot.Width  = (double)canvasXCell;
            myRobot.Height = (double)canvasXCell;

            for (int i = 1; i <= 8; i++)
            {
                for(int boundary = 1; boundary <= 8; boundary++)
                {
                    Rectangle mineBox = new Rectangle();
                    mineBox.Width = canvasXCell / 2;
                    mineBox.Height = canvasXCell;

                    Canvas.SetTop(mineBox, 82 * i);
                    Canvas.SetLeft(mineBox, 124 * boundary);

                    mineBox.Fill = new SolidColorBrush(Colors.LightGreen);

                    tilesList.Add(mineBox); //Keep track of tiles
                }                         
            }
            List<Rectangle> randomList = new List<Rectangle>();

            Random r = new Random();
            int randomIndex = 0;
            while (tilesList.Count > 0)
            {
                randomIndex = r.Next(0, tilesList.Count); //Choose a random object in the list
                randomList.Add(tilesList[randomIndex]); //add it to the new, random list
                tilesList.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            tilesList = randomList; 
            for (int i = 0; i < 40; i++)
            {
                positonOfBombToDelete = randBomb.Next(0, tilesList.Count);
                tilesList.RemoveAt(positonOfBombToDelete);
            }

            foreach(var bomb in tilesList)
            {
                myCanvas.Children.Insert(0, bomb); //Draw
            }    
        }

        /** Click Related Event Handlers **/

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            yAxisStoryBoard.Begin();

          //  myRobotAnimationYAxis.From = this.canvasRows - this.canvasXCell;
            myRobotAnimationYAxis.To = 0;


            if (myRobot.Margin == new Thickness(this.canvasRows - this.canvasXCell, 0 * 1, 0, 0))
            {
                 myCanvas.Children.Remove(bombList.ElementAt(0));
            }


            //foreach(var bomb in bombList)
            //{
            //    myCanvas.Children.Remove(bomb);

            //}


        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            yAxisStoryBoard.Begin();

           // myRobotAnimationYAxis.From = 0;
            myRobotAnimationYAxis.To = this.canvasRows - this.canvasXCell; //- this.canvasXCell;


            if (myRobot.Margin == new Thickness(0, this.canvasRows - this.canvasXCell, 0, 0))
            {
                myCanvas.Children.Remove(bombList.ElementAt(0));
            }

        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            xAxisStoryBoard.Begin();

           // myRobotAnimationXAxis.From = 0;
            myRobotAnimationXAxis.To = this.canvasColumns - this.canvasXCell;
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            xAxisStoryBoard.Begin();

           // myRobotAnimationXAxis.From = this.canvasColumns - this.canvasXCell;
            myRobotAnimationXAxis.To = 0;
        }
    }
}
