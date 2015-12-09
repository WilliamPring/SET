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

        Random bombToDelete;  //what bomb to delete in the bombList

        int x; //Represent the X Grid
        int y; //Represent the Y Grid

        int maxBombCount; //The max bomb that can be in this program

        int currBomb; //the current amount of bomb in this project
        int positonOfBombToDelete; //which bomb to delete


        int lastRobotXPos;
        int lastRobotYPos; 



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


            lastRobotXPos = 0;
            lastRobotYPos = 0;

            maxBombCount = (int)(x * y * (0.40)); //Formula for max number of mines 
            currBomb = randBomb.Next(10, maxBombCount); //Calculate number of mines for the game  
        }


        private void Main_Page_Loaded_Event(object sender, RoutedEventArgs e)
        {
            this.canvasRows = (int)myCanvas.ActualHeight;
            this.canvasColumns = (int)myCanvas.ActualWidth;


            this.canvasXCell = canvasColumns / 8;
            this.canvasYCell = canvasRows / 8;

            myRobot.Width  = (double)canvasXCell;
            myRobot.Height = (double)canvasXCell;

            createRandomBombs();      
        }


        private void createRandomBombs()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int boundary = 1; boundary <= 8; boundary++)
                {
                    Rectangle mineBox = new Rectangle();
                    mineBox.Width = canvasXCell / 2;
                    mineBox.Height = canvasXCell;

                    Canvas.SetTop(mineBox, canvasYCell * i);
                    Canvas.SetLeft(mineBox, canvasXCell * boundary);

                    mineBox.Fill = new SolidColorBrush(Colors.LightGreen);

                    bombList.Add(mineBox); //Keep track of tiles
                }
            }

            List<Rectangle> randomList = new List<Rectangle>();

            Random r = new Random();
            int randomIndex = 0;
            while (bombList.Count > 0)
            {
                randomIndex = r.Next(0, bombList.Count); //Choose a random object in the list
                randomList.Add(bombList[randomIndex]); //add it to the new, random list
                bombList.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            bombList = randomList;
            for (int i = 0; i < 40; i++)
            {
                positonOfBombToDelete = randBomb.Next(0, bombList.Count);
                bombList.RemoveAt(positonOfBombToDelete);
            }

            foreach (var bomb in bombList)
            {
                myCanvas.Children.Insert(0, bomb); //Draw
            }
        }

        /** Click Related Event Handlers **/

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(lastRobotYPos > 0)
            {
                yAxisStoryBoard.Begin();
                myRobotAnimationYAxis.From = lastRobotYPos;
                lastRobotYPos -= canvasYCell;
                myRobotAnimationYAxis.To = lastRobotYPos;

            }

            foreach(var bomb in bombList)
            {
                if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                {
                    myCanvas.Children.Remove(bomb);
                }
            }               
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (lastRobotYPos <= this.canvasRows - this.canvasXCell)
            {
                yAxisStoryBoard.Begin();
                myRobotAnimationYAxis.From = lastRobotYPos;
                lastRobotYPos += canvasYCell;
                myRobotAnimationYAxis.To = lastRobotYPos;

                foreach(var bomb in bombList)
                {
                    if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                    {
                        myCanvas.Children.Remove(bomb);
                    }
                }           
            }       
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            if (lastRobotXPos < this.canvasColumns - this.canvasXCell)
            {
                xAxisStoryBoard.Begin();
                myRobotAnimationXAxis.From = lastRobotXPos;
                lastRobotXPos += canvasXCell;
                myRobotAnimationXAxis.To = lastRobotXPos;

                foreach(var bomb in bombList)
                {
                    if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                    {
                        myCanvas.Children.Remove(bomb);
                    }
                }   
            }
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            if (lastRobotXPos > 0)
            {
                xAxisStoryBoard.Begin();
                myRobotAnimationXAxis.From = lastRobotXPos;
                lastRobotXPos -= canvasXCell;
                myRobotAnimationXAxis.To = lastRobotXPos;
            }

            foreach(var bomb in bombList)
            {
                if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                {
                    myCanvas.Children.Remove(bomb);
                }
            }              
        }
    }
}

