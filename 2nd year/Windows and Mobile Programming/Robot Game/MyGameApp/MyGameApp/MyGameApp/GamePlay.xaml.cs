/*
File name: GamePlay.xaml.cs
Project: Windows 10 universal Application
By: William Pring and Naween Mehanmal
Date: 
Description: This contains the core logic in how our game operates as well as anything involving the main
xaml page
*/

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

//Needed for the accelerometer 

using Windows.UI.Core;
using Windows.Devices.Sensors;
using Windows.Graphics.Display;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyGameApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePlay : Page
    {
        private int canvasRows;
        private int canvasColumns;

        private int canvasYCell;
        private int canvasXCell;

        private DispatcherTimer myDispatcherTimer;
        private DispatcherTimer acceleromterTimer;
        private DispatcherTimer resizeTimer;

        private Player myPlayer;

        private Random randBomb;

        private List<Rectangle> bombList;  //list ofthe bomb being stored

        private Random bombToDelete;  //what bomb to delete in the bombList

        private int x; //Represent the X Grid
        private int y; //Represent the Y Grid

        private int maxBombCount; //The max bomb that can be in this program

        private int currBomb; //the current amount of bomb in this project
        private int positonOfBombToDelete; //which bomb to delete


        private int lastRobotXPos;
        private int lastRobotYPos;

        private int time;
        private bool pauseStatus;

        private int totalCount;
        private int delBomb;

        private int boundaryNum;

        private string playerName;

        private ContentDialog YouWon;

        //accelerometer
        private Accelerometer myAccelerometer;
        private uint desReportInterval;

        private List<Player> LeaderBoard;
        private int lastIndexAvailable;
        private int boundaryNumForX;
        private bool displayStatus;


        public GamePlay()
        {
            this.InitializeComponent();
            //randoms
            randBomb = new Random();
            bombToDelete = new Random();
            //list of bombs
            bombList = new List<Rectangle>();

            currBomb = 0;
            positonOfBombToDelete = 0;
            //grid layout
            x = 8;
            y = 8;
            
            lastRobotXPos = 0;
            lastRobotYPos = 0;

            maxBombCount = (int)(x * y * (0.40)); //Formula for max number of mines 
            currBomb = randBomb.Next(10, maxBombCount); //Calculate number of mines for the game  

            time = 0;
            //pausing the game and pausing our function of the game
            pauseStatus = true;

            totalCount = 0;
            delBomb = 0;

            boundaryNum = 0;
            lastIndexAvailable = 9;
            //list of players who play the game
            LeaderBoard = new List<Player>();

            //Create and define the accelerometer 

            myAccelerometer = Accelerometer.GetDefault();
            //check if it not null
            if (myAccelerometer != null)
            {
                uint minReportInterval = myAccelerometer.MinimumReportInterval;
                desReportInterval = minReportInterval > 16 ? minReportInterval : 16;

                // Set up a DispatchTimer
                acceleromterTimer = new DispatcherTimer();
                acceleromterTimer.Tick += accelerometerMovements;
                //every tick will be 50 mill sec
                acceleromterTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                acceleromterTimer.Start(); //Start the timer 
            }

            //For rotating phone

            displayStatus = false; 

            Window.Current.SizeChanged += CurrentSizeChanged;

            LeaderBoard = new List<Player>(); 

            this.NavigationCacheMode = NavigationCacheMode.Required;

        }

        /*
             Name: CurrentSizeChanged 
             Purpose: This will be used as a timer to check the phones view
             Data Members : object sender, WindowSizeChangedEventArgs e
             Return: void
        */
        private void CurrentSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            // Set up a DispatchTimer
            resizeTimer = new DispatcherTimer();
            resizeTimer.Tick += resizeScreen;
            resizeTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            resizeTimer.Start(); //Start the timer 

        }

        /*
          Name: CurrentSizeChanged 
          Purpose: This will be when the view of the phone gets changed and change the dimension
          Data Members : object sender, WindowSizeChangedEventArgs e
          Return: void
     */
        private void resizeScreen(object sender, object obj2)
        {
            var newBoundary = Window.Current.Bounds;
            //sdfasdf
            if (newBoundary.Height > newBoundary.Width)
            {
                this.canvasRows = (int)myCanvas.ActualHeight;
                this.canvasColumns = (int)myCanvas.ActualWidth;
                
                this.canvasXCell = canvasColumns / 10;
                this.canvasYCell = canvasRows / 10;

                lastRobotXPos = canvasXCell;
                lastRobotYPos = canvasYCell;
                //asfdSZDsdafasdf
                myRobot.Height = canvasXCell;
                myRobot.Width = canvasXCell;
                lastIndexAvailable = 9;
                changeBombPositions();
            }
            else
            {            
                this.canvasRows = (int)newBoundary.Height;
                this.canvasColumns = (int)newBoundary.Width;

                this.canvasXCell = canvasColumns / 10;
                this.canvasYCell = canvasRows / 10;

                this.canvasColumns -= canvasYCell;
                this.canvasRows -= canvasXCell - 25; 

                lastRobotXPos = canvasXCell;
                lastRobotYPos = canvasYCell;
                lastIndexAvailable = 7;
                changeBombPositions();
            }

            resizeTimer.Stop();
        }

        /*
            Name: changeBombPositions 
            Purpose: This will be used and be called everythime the view of the phone get changed and will
            change the position of the bomb to suit the desire view 
            Data Members : object sender, WindowSizeChangedEventArgs e
             Return: void
        */
        private void changeBombPositions()
        {
            foreach(var myBomb in bombList)
            {
                int i = randBomb.Next(1, 8);
                int j = randBomb.Next(1, 8);

                myBomb.Height = canvasXCell / 2;
                myBomb.Width = canvasXCell;
                
                Canvas.SetLeft(myBomb, canvasXCell * i);
                Canvas.SetTop(myBomb, canvasYCell * j);
            }

            Canvas.SetLeft(textBlock, canvasXCell * 8);
            Canvas.SetTop(textBlock, canvasXCell);


            Canvas.SetLeft(myBombCount, canvasXCell * 8);
            Canvas.SetTop(myBombCount, canvasXCell * 2);

            Canvas.SetLeft(deletedBombs, canvasXCell * 8);
            Canvas.SetTop(deletedBombs, canvasXCell * 3);
        }

        /*
            Name: GameLayout 
            Purpose: This will layout all the bomb and titles and get called in the start
            Data Members : void
             Return: void
        */
        private void GameLayout()
        {     
            //grid is 10 x 10
            this.canvasXCell = canvasColumns / 10;
            this.canvasYCell = canvasRows / 10;

            myRobot.Width = (double)canvasXCell;
            myRobot.Height = (double)canvasXCell;

            //Position the textblocks
            Canvas.SetLeft(textBlock, canvasXCell * 8);
            Canvas.SetTop(textBlock, canvasXCell);


            Canvas.SetLeft(myBombCount, canvasXCell * 8);
            Canvas.SetTop(myBombCount, canvasXCell * 2);

            Canvas.SetLeft(deletedBombs, canvasXCell * 8);
            Canvas.SetTop(deletedBombs, canvasXCell * 3);
        }

        /*
            Name: GameLayout 
            Purpose: Will start the timer of the game
            Data Members : object sender, RoutedEventArgs e
            Return: void
        */
        private void Game_Loaded(object sender, RoutedEventArgs e)
        {
            //Create the timer
            try
            {
                if(totalCount != 0)
                {
                    myDispatcherTimer.Start();
                }
            }
            catch (Exception)
            {
                playGame();
            }
        }

        /*
          Name: accelerometerMovements 
          Purpose: Will get the accelormeter movement and will move the robot up, down, left or right
          Data Members : object sender, object obj2
          Return: void
      */

        public void accelerometerMovements(object sender, object obj2)
        {
            AccelerometerReading reading = myAccelerometer.GetCurrentReading();

            if (reading != null)
            {
                //move robot right
                if(reading.AccelerationX > 0.1)
                {
                    MoveRobotRight();
                }
                //move robot left
                else if(reading.AccelerationX < -0.1)
                {
                    MoveRobotLeft(); 
                }
                //move robot down
                else if(reading.AccelerationY < -0.1)
                {
                    MoveRobotDown();
                }
                //move robot up
                else if(reading.AccelerationY > -0.1)
                {
                    MoveRobotUp();
                }
            }
        }




        /*
        Name: createRandomBombs 
        Purpose: This will generate bombs randomly and make sure no 2 bomb is the same 
        Data Members : void
        Return: void
    */

        private void createRandomBombs()
        {
            //putting bomb in all of the grid
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
            //shuffling the bombs around to make it more random
            while (bombList.Count > 0)
            {
                randomIndex = r.Next(0, bombList.Count); //Choose a random object in the list
                randomList.Add(bombList[randomIndex]); //add it to the new, random list
                bombList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            bombList = randomList;
            totalCount = bombList.Count; //Total number of bombs in the game

            //removing bombs from the original list and putting in the new one
            for (int i = 0; i < 40; i++)
            {
                positonOfBombToDelete = randBomb.Next(0, bombList.Count);
                bombList.RemoveAt(positonOfBombToDelete);
            }

            totalCount = bombList.Count; 
            myBombCount.Text = "Left" + totalCount.ToString();

            //drawing each bomb
            foreach (var bomb in bombList)
            {
                myCanvas.Children.Insert(0, bomb); //Draw
            }
        }




        /*
            Name: MoveRobotUp 
            Purpose: Move the robot up and display it on the screen and deactivate bombs, update screen
            with correct information
            Data Members : void
            Return: void
        */

        private void MoveRobotUp()
        {
            int counter = 0;
            //if game is pause
            if (pauseStatus)
            {
                //check to see if robot is on bomb
                if (lastRobotYPos == this.canvasRows - canvasXCell)
                {
                    if(lastIndexAvailable == 7)
                    {
                        yAxisStoryBoard.Begin();

                        myRobotAnimationYAxis.From = lastRobotYPos;
                        lastRobotYPos -= boundaryNum;
                        myRobotAnimationYAxis.To = lastRobotYPos;
                    }
                    else
                    {
                        yAxisStoryBoard.Begin();

                        myRobotAnimationYAxis.From = lastRobotYPos;
                        lastRobotYPos -= boundaryNum;
                        myRobotAnimationYAxis.To = lastRobotYPos;
                    }                
                }
                else if (lastRobotYPos > 0)
                {
                    yAxisStoryBoard.Begin();
                    myRobotAnimationYAxis.From = lastRobotYPos;
                    lastRobotYPos -= canvasYCell;
                    myRobotAnimationYAxis.To = lastRobotYPos;

                    foreach (var bomb in bombList)
                    {
                        if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                        {
                            myCanvas.Children.Remove(bomb);
                            bombList.RemoveAt(counter);
                            totalCount = bombList.Count;
                            myBombCount.Text = "Left: " + totalCount.ToString();
                            delBomb++;
                            deletedBombs.Text = "Del: " + delBomb.ToString();
                            break;
                        }

                        counter++;
                    }
                }
            }
        }


        /*
       Name: MoveRobotDown 
       Purpose: Move the robot Down and display it on the screen and deactivate bombs, update screen
       with correct information
       Data Members : void
       Return: void
   */
        private void MoveRobotDown()
        {        
            int counter = 0;

            if (pauseStatus)
            {

                if (lastRobotYPos == canvasYCell * lastIndexAvailable)
                {
                    if(lastIndexAvailable == 7)
                    {
                        yAxisStoryBoard.Begin();

                        myRobotAnimationYAxis.From = lastRobotYPos;
                        boundaryNum = (this.canvasRows - canvasXCell) - lastRobotYPos;
                        lastRobotYPos += (this.canvasRows - canvasXCell) - lastRobotYPos;
                        myRobotAnimationYAxis.To = lastRobotYPos;
                    }
                    else
                    {
                        yAxisStoryBoard.Begin();

                        myRobotAnimationYAxis.From = lastRobotYPos;
                        boundaryNum = (this.canvasRows - canvasXCell) - lastRobotYPos;
                        lastRobotYPos += (this.canvasRows - canvasXCell) - lastRobotYPos;
                        myRobotAnimationYAxis.To = lastRobotYPos;
                    }     

                }
                else if (lastRobotYPos < this.canvasRows - this.canvasXCell)
                {
                    yAxisStoryBoard.Begin();
                    myRobotAnimationYAxis.From = lastRobotYPos;
                    lastRobotYPos += canvasYCell;
                    myRobotAnimationYAxis.To = lastRobotYPos;

                    foreach (var bomb in bombList)
                    {
                        if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                        {
                            myCanvas.Children.Remove(bomb);
                            bombList.RemoveAt(counter);
                            totalCount = bombList.Count;
                            myBombCount.Text = "Left: " + totalCount.ToString();
                            delBomb++;
                            deletedBombs.Text = "Del: " + delBomb.ToString();
                            break;
                        }

                        counter++;
                    }
                }
            }
        }

        /*
            Name: MoveRobotRight
            Purpose: Move the robot Right and display it on the screen and deactivate bombs, update screen
            with correct information
            Data Members : void
            Return: void
        */
        private void MoveRobotRight()
        {
            int counter = 0;

            if (pauseStatus)
            {
                if(lastIndexAvailable == 7 && lastRobotXPos == 424)
                {
                    xAxisStoryBoard.Begin();
                    myRobotAnimationXAxis.From = lastRobotXPos;
                    boundaryNumForX = this.canvasColumns - this.canvasXCell - lastRobotXPos;
                    lastRobotXPos += this.canvasColumns - this.canvasXCell - lastRobotXPos;
                    myRobotAnimationXAxis.To = this.canvasColumns - this.canvasXCell;
                }
                else if (lastRobotXPos < this.canvasColumns - this.canvasXCell)
                {
                    xAxisStoryBoard.Begin();
                    myRobotAnimationXAxis.From = lastRobotXPos;
                    lastRobotXPos += canvasXCell;
                    myRobotAnimationXAxis.To = lastRobotXPos;

                    foreach (var bomb in bombList)
                    {
                        if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                        {
                            myCanvas.Children.Remove(bomb);
                            bombList.RemoveAt(counter);
                            totalCount = bombList.Count;
                            myBombCount.Text = "Left: " + totalCount.ToString();
                            delBomb++;
                            deletedBombs.Text = "Del: " + delBomb.ToString();
                            break;
                        }

                        counter++;
                    }
                }
            }
        }
        /*
            Name: MoveRobotLeft 
            Purpose: Move the robot Left and display it on the screen and deactivate bombs, update screen
            with correct information
            Data Members : void
            Return: void
        */

        private void MoveRobotLeft()
        {
            
            int counter = 0;

            if (pauseStatus)
            {
                if (lastIndexAvailable == 7 && lastRobotXPos == this.canvasColumns - this.canvasXCell)
                {
                    xAxisStoryBoard.Begin();
                    myRobotAnimationXAxis.From = lastRobotXPos;
                    lastRobotXPos -= boundaryNumForX;
                    myRobotAnimationXAxis.To = 424;
                }
                else if (lastRobotXPos > 0)
                {
                    xAxisStoryBoard.Begin();
                    myRobotAnimationXAxis.From = lastRobotXPos;
                    lastRobotXPos -= canvasXCell;
                    myRobotAnimationXAxis.To = lastRobotXPos;
                }

                foreach (var bomb in bombList)
                {
                    if (lastRobotYPos == Canvas.GetTop(bomb) && lastRobotXPos == Canvas.GetLeft(bomb))
                    {
                        myCanvas.Children.Remove(bomb);
                        bombList.RemoveAt(counter);
                        totalCount = bombList.Count;
                        myBombCount.Text = "Left: " + totalCount.ToString();
                        delBomb++;
                        deletedBombs.Text = "Del: " + delBomb.ToString();

                        break;
                    }

                    counter++;
                }
            }
        }




        /*
         Name: Pause_Time 
         Purpose: Pause and resume button
         Data Members : object sender, TappedRoutedEventArgs e
         Return: void
     */
        private void Pause_Time(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                //check status to see if the next button should be pause or resume
                if (pauseStatus)
                {
                    pauseStatus = false;
                    myDispatcherTimer.Stop();
                    pauseButton.Icon = new SymbolIcon(Symbol.Play);
                }
                else
                {
                    pauseStatus = true;
                    myDispatcherTimer.Start();
                    pauseButton.Icon = new SymbolIcon(Symbol.Pause);
                }
            }
            catch(Exception)
            {
                pauseButton.Icon = new SymbolIcon(Symbol.Pause);
            }
        }


        /*
            Name: Pause_Time 
            Purpose: Go the leaderboard
            Data Members : object sender, TappedRoutedEventArgs e
            Return: void
        */

        private void Go_To_Leaderboard(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                myDispatcherTimer.Stop();

                //Go to leaderboard page
                this.Frame.Navigate(typeof(BlankPage1), LeaderBoard);
            }
            catch(Exception)
            {
                this.Frame.Navigate(typeof(BlankPage1), LeaderBoard);
            }
        }



        /*
            Name: EachTick 
            Purpose: Get the position of the bomb, time, handling a win condition, entering name
            Data Members : object o, object o1
            Return: void
        */
        public void EachTick(object o, object o1)
        {
            if (totalCount > 0)
            {
                xPos.Text = "X Pos: " + lastRobotXPos;
                yPos.Text = "Y Pos: " + lastRobotYPos;

                time++;
                textBlock.Text = "Sec: " + time.ToString();
            }
            else
            {
                xPos.Text = "X Pos: " + lastRobotXPos;
                yPos.Text = "Y Pos: " + lastRobotYPos;

                myDispatcherTimer.Stop();

                YouWon = new ContentDialog()
                {
                    Title = " You Won ",
                    PrimaryButtonText = "Accept",
                    SecondaryButtonText = "Cancel",
                };
                StackPanel myPannel = new StackPanel();
                myPannel.Children.Add(new TextBlock
                {
                    Text = "Enter your UserName: ",

                });

                TextBox myBoxDisplay = new TextBox();

                myPannel.Children.Add(myBoxDisplay);
                YouWon.Content = myPannel;

                YouWon.PrimaryButtonClick += addToLeaderBoard;

                YouWon.ShowAsync();


                restartGame.IsEnabled = true;

            }
        }

        /*
            Name: addToLeaderBoard 
            Purpose: Display information about the player and sending it
            Data Members : object sender, object obj2
            Return: void
        */
        private void addToLeaderBoard(object sender, object obj2)
        {
            playerName = ((TextBox)((StackPanel)((ContentDialog)sender).Content).Children[1]).Text;

            myPlayer = new Player(playerName, time);
            LeaderBoard.Add(myPlayer);
            LeaderBoard.Sort(delegate (Player one, Player two) { return one.GetTime.CompareTo(two.GetTime); }); 
        }


        /*
            Name: Start_Game 
            Purpose: Start the game 
            Data Members : object sender, TappedRoutedEventArgs e
            Return: void
         */
        private void Start_Game(object sender, TappedRoutedEventArgs e)
        {
            playGame();
        }


        /*
           Name: Start_Game 
           Purpose: Start the game but setting the correct parmeters and reset everything
           Data Members : object sender, TappedRoutedEventArgs e
           Return: void
        */
        private void playGame()
        {
            this.canvasRows = (int)myCanvas.ActualHeight;
            this.canvasColumns = (int)myCanvas.ActualWidth;

            GameLayout();
            createRandomBombs(); //Create the mines

            time = 0; //Reset the time

            myDispatcherTimer = new DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); // 1 sec
            myDispatcherTimer.Tick += EachTick;
            myDispatcherTimer.Start();

            restartGame.IsEnabled = false;
        }

        /*
           Name: rotateScreen_Click 
           Purpose: Rotate the screen and lock it
           Return: void
           Data Members : object sender, TappedRoutedEventArgs e
           */
        private void rotateScreen_Click(object sender, RoutedEventArgs e)
        {
            displayStatus = !(displayStatus); 

            if (displayStatus)
            {
                DisplayProperties.AutoRotationPreferences = DisplayProperties.CurrentOrientation;

            }
            else
            {
                DisplayProperties.AutoRotationPreferences = DisplayOrientations.Portrait | DisplayOrientations.Landscape;
            }

        }
    }
}
