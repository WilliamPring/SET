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


        private Accelerometer myAccelerometer;
        private uint desReportInterval;

        private List<Player> LeaderBoard;

        private int lastIndexAvailable;
        private int boundaryNumForX; 



        public GamePlay()
        {
            this.InitializeComponent();

            randBomb = new Random();
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

            time = 0;

            pauseStatus = true;

            totalCount = 0;
            delBomb = 0;

            boundaryNum = 0;
            lastIndexAvailable = 9; 

            //Create and define the accelerometer 

            myAccelerometer = Accelerometer.GetDefault();

            if (myAccelerometer != null)
            {
                uint minReportInterval = myAccelerometer.MinimumReportInterval;
                desReportInterval = minReportInterval > 16 ? minReportInterval : 16;

                // Set up a DispatchTimer
                acceleromterTimer = new DispatcherTimer();
                acceleromterTimer.Tick += accelerometerMovements;
                acceleromterTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
                acceleromterTimer.Start(); //Start the timer 
            }

            //For rotating phone

            Window.Current.SizeChanged += CurrentSizeChanged;

            LeaderBoard = new List<Player>(); 

            this.NavigationCacheMode = NavigationCacheMode.Required;

        }


        private void CurrentSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            // Set up a DispatchTimer
            resizeTimer = new DispatcherTimer();
            resizeTimer.Tick += resizeScreen;
            resizeTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            resizeTimer.Start(); //Start the timer 

        }


        private void resizeScreen(object sender, object obj2)
        {
            var newBoundary = Window.Current.Bounds;

            if (newBoundary.Height > newBoundary.Width)
            {
                this.canvasRows = (int)myCanvas.ActualHeight;
                this.canvasColumns = (int)myCanvas.ActualWidth;
                
                this.canvasXCell = canvasColumns / 10;
                this.canvasYCell = canvasRows / 10;

                lastRobotXPos = canvasXCell;
                lastRobotYPos = canvasYCell;

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

        
        private void GameLayout()
        {     

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



        public void accelerometerMovements(object sender, object obj2)
        {
            AccelerometerReading reading = myAccelerometer.GetCurrentReading();

            if (reading != null)
            {
                if(reading.AccelerationX > 0.1)
                {
                    MoveRobotRight();
                }
                else if(reading.AccelerationX < -0.1)
                {
                    MoveRobotLeft(); 
                }
                else if(reading.AccelerationY < -0.1)
                {
                    MoveRobotDown();
                }
                else if(reading.AccelerationY > -0.1)
                {
                    MoveRobotUp();
                }
            }
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
            totalCount = bombList.Count; //Total number of bombs in the game


            for (int i = 0; i < 40; i++)
            {
                positonOfBombToDelete = randBomb.Next(0, bombList.Count);
                bombList.RemoveAt(positonOfBombToDelete);
            }

            totalCount = bombList.Count; 
            myBombCount.Text = "Left" + totalCount.ToString();


            foreach (var bomb in bombList)
            {
                myCanvas.Children.Insert(0, bomb); //Draw
            }
        }




        /** COMMAND BAR RELATED EVENT HANDLED CODE **/


        private void MoveRobotUp()
        {
            int counter = 0;

            if (pauseStatus)
            {
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




        /** Pause and Resume Button Event **/

        private void Pause_Time(object sender, TappedRoutedEventArgs e)
        {
            try
            {
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



        //GAME WON RELATED EVENTS

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


        private void addToLeaderBoard(object sender, object obj2)
        {
            playerName = ((TextBox)((StackPanel)((ContentDialog)sender).Content).Children[1]).Text;

            myPlayer = new Player(playerName, time);
            LeaderBoard = new List<Player>();
            LeaderBoard.Add(myPlayer);
            LeaderBoard.Sort(); 
        }



        private void Start_Game(object sender, TappedRoutedEventArgs e)
        {
            playGame();
        }



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


    }
}
