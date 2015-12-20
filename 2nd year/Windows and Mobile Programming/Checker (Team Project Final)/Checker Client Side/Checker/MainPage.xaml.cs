/*    
 * Filename: CheckerPiece.cs
 * Assignment: WMP Final Project 
 * By: Naween Mehanmal and William Pring, Denys Politiuk
 * Date: December 16, 2015
 * Description: This file contains all of the game logic and UI transition that occurs throughout the checker program. 
 *
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using CheckersClient;

namespace Checker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int prevRow;
        private int prevColumn;

        private int desRow;
        private int desColumnn;

        private bool colorOfPiece; //TRUE is RED    FALSE is ORANGE
        private bool moveKing = false;

        private bool isFirstClick;


        private SolidColorBrush myPieceColor = new SolidColorBrush();
        private SolidColorBrush myBorderColor = new SolidColorBrush();

        private List<CheckerPiece> WallYouCannotMove;
        private List<CheckerPiece> RedPiece;
        private List<CheckerPiece> OrgPiece;

        private Canvas zero = new Canvas();
        private Canvas currSquareSelected = new Canvas(); //Spot on the square specified 

        //Multidimensional representation of the board 
        private Canvas[,] boardSquares;

        //Connection Stream
        private TCPIPconnectorClient connectionStream;

        private bool isAbleToMove;

        private string playerColor;

        private DispatcherTimer openingGame = new DispatcherTimer();
        private DispatcherTimer middleGame = new DispatcherTimer();


        public MainPage()
        {
            this.InitializeComponent();

            isFirstClick = true; //Select the first click option

            prevRow = 0;
            prevColumn = 0;
            desRow = 0;
            desColumnn = 0;

            WallYouCannotMove = new List<CheckerPiece>();
            //Red Piece 
            RedPiece = new List<CheckerPiece>();
            //Organge Piece
            OrgPiece = new List<CheckerPiece>();

            isAbleToMove = false; //Initially can't do anything

            middleGame.Interval = new TimeSpan(0, 0, 0, 0, 100);
            middleGame.Tick += MiddleGameUpdate;
        }

        /// <summary>
        /// First event called when the grid page is loaded 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            int relHeightTile = (int)myGrid.ActualHeight / 12;
            int relWidthTile = (int)myGrid.ActualWidth / 10;




            CreateCheckerPiece();
            //createWall();
        }

        private void Clicked(object sender, TappedRoutedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            connectionStream = e.Parameter as TCPIPconnectorClient; //Reference to the connection 

            openingGame.Interval = new TimeSpan(0, 0, 0, 0, 100);
            openingGame.Tick += FirstGameMove;
            openingGame.Start();

        }



        /// <summary>
        /// The initial move started in the game. This will allows for all of the coordinates being registered and being able to be 
        /// referenced on the code behind from the xaml page. Each grid contains its own canvas, reference is key to access these grid
        /// parent and child elements. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="obj2"></param>
        private void FirstGameMove(object sender, object obj2)
        {
            openingGame.Stop();

            boardSquares = new Canvas[,]{
                                       { Row1Col0, zero, Row1Col2, zero, Row1Col4, zero, Row1Col6, zero},    //0
                                       { zero, Row2Col1, zero, Row2Col3, zero, Row2Col5, zero, Row2Col7 },   //1 
                                       { Row3Col0, zero, Row3Col2, zero, Row3Col4, zero, Row3Col6, zero },   //2
                                       { zero, Row4Col1, zero, Row4Col3, zero, Row4Col5, zero, Row4Col7 },   //3 
                                       { Row5Col0, zero, Row5Col2, zero, Row5Col4, zero, Row5Col6, zero },   //4
                                       { zero, Row6Col1, zero, Row6Col3, zero, Row6Col5, zero, Row6Col7 },   //5
                                       { Row7Col0, zero, Row7Col2, zero, Row7Col4, zero, Row7Col6, zero },   //6 
                                       { zero, Row8Col1, zero, Row8Col3, zero, Row8Col5, zero, Row8Col7 } }; //7

            string msg = "";

            do
            {
                msg = connectionStream.Read();
            }
            while (msg == "!wait");

            if (msg == "!startOne")
            {
                // your turn
                isAbleToMove = true;
                playerColor = "orange";
                gameOutcomeMsg.Text = "You are Orange";
                turnStatus.Text = "YOUR TURN";


            }
            else if (msg == "!startTwo")
            {
                // you wait for your turn
                isAbleToMove = false;
                turnStatus.Text = "NOT YOUR TURN";

                playerColor = "red";
                gameOutcomeMsg.Text = "You are Red";

                string input = this.connectionStream.Read();
                if (input == "!prepare")
                {
                    this.connectionStream.Send("!ok");
                    Message tmp = Message.Deserialize(this.connectionStream.Read());
                    this.connectionStream.Send("!ok");
                    //UPDATE BOARD FROM THE OTHER PLAYER

                    CheckMove(tmp.color, tmp.king, tmp.currentX + 1, tmp.currentY, tmp.newX + 1, tmp.newY);

                    prevRow = tmp.currentY;
                    prevColumn = tmp.currentX;

                    desRow = tmp.newY;
                    desColumnn = tmp.newX;

                    if (tmp.king == true) //Check if a king piece returned 
                    {
                        if (tmp.color == true) //Red king piece
                        {
                            myBorderColor = new SolidColorBrush(Colors.White);
                            myPieceColor = new SolidColorBrush(Colors.Red);
                            MoveCheckerPiece(true); //Move the KING checker piece
                        }
                        else //Orange king piece
                        {
                            myBorderColor = new SolidColorBrush(Colors.White);
                            myPieceColor = new SolidColorBrush(Colors.Orange);
                            MoveCheckerPiece(true); //Move the KING checker piece
                        }
                    }
                    else
                    {
                        if (tmp.color == true) //Piece being moved is red
                        {
                            myBorderColor = new SolidColorBrush(Colors.Red);
                            myPieceColor = new SolidColorBrush(Colors.Red);
                            MoveCheckerPiece(false); //Move the REGULAR RED checker piece 
                            CheckIfKing(true); //Check if the red piece is a king now
                        }
                        else
                        {
                            myBorderColor = new SolidColorBrush(Colors.Orange);
                            myPieceColor = new SolidColorBrush(Colors.Orange);
                            MoveCheckerPiece(true); //Move the REGULAR ORANGE checker piece
                            CheckIfKing(false); //Check if the orange piece is a king now
                        }
                    }

                    //
                }

                if (input == "!left")
                {
                    this.isAbleToMove = false;
                    turnStatus.Text = "ENEMY LEFT";
                }
                else
                {
                    input = this.connectionStream.Read();
                    if (input == "!turn")
                    {
                        this.isAbleToMove = true;
                        turnStatus.Text = "YOUR TURN";
                    }

                    if (input == "!left")
                    {
                        this.isAbleToMove = false;
                        turnStatus.Text = "ENEMY LEFT";
                    }
                }
            }
        }


        //CHECK FOR TURN
        /// <summary>
        /// Method called to enter move, the user will decide on a first coordinate and an end coordinate to move the checker piece
        /// across the board 
        /// </summary>
        private void enterMove()
        {
            bool moveAcceptable = false;

            try
            {
                if (isFirstClick)
                {
                    isFirstClick = false; //Select another coordinate to determine movement 

                    prevColumn = (int)currSquareSelected.GetValue(Grid.ColumnProperty); //Prev Column
                    prevRow = (int)currSquareSelected.GetValue(Grid.RowProperty); //Prev Row                

                    //Get the colour of the piece
                    myPieceColor = (SolidColorBrush)currSquareSelected.Children.ElementAt(0).GetValue(Ellipse.FillProperty);

                    //Check if the brush resembles king
                    myBorderColor = (SolidColorBrush)currSquareSelected.Children.ElementAt(0).GetValue(Ellipse.StrokeProperty);

                    if (myPieceColor.Color == Colors.Red && playerColor == "red")
                    {
                        //Piece that was selected
                        boardSquares[prevRow - 1, prevColumn].Background = new SolidColorBrush(Colors.Yellow);

                        colorOfPiece = true; //COLOR OF PIECE IS RED

                        if (myBorderColor.Color == Colors.White)
                        {
                            moveKing = true;
                        }
                        else
                        {
                            moveKing = false;
                        }
                    }
                    else if (myPieceColor.Color == Colors.Orange && playerColor == "orange")
                    {
                        //Piece that was selected
                        boardSquares[prevRow - 1, prevColumn].Background = new SolidColorBrush(Colors.Yellow);

                        colorOfPiece = false; //COLOR OF PIECE IS ORANGE

                        if (myBorderColor.Color == Colors.White)
                        {
                            moveKing = true;
                        }
                        else
                        {
                            moveKing = false;
                        }
                    }
                    else
                    {
                        //Wrong color choice
                        isFirstClick = true; //Redo the click move 
                    }
                }
                else
                {
                    boardSquares[prevRow - 1, prevColumn].Background = new SolidColorBrush(Colors.Black);

                    isFirstClick = true; //Allow to select new coordinates to click on for movement
                    desColumnn = (int)currSquareSelected.GetValue(Grid.ColumnProperty); //Current Column
                    desRow = (int)currSquareSelected.GetValue(Grid.RowProperty); //Current Row

                    //Now send the information off to the server! 
                    moveAcceptable = CheckMove(colorOfPiece, moveKing, prevColumn + 1, prevRow, desColumnn + 1, desRow); //Send this info the game logic controller 

                    if (moveAcceptable) //Returns true, change checker position on board
                    {
                        Message msg = new Message();
                        msg.color = colorOfPiece;
                        msg.king = moveKing;
                        msg.currentX = prevColumn;
                        msg.currentY = prevRow;
                        msg.newX = desColumnn;
                        msg.newY = desRow;

                        this.connectionStream.Send("!send");
                        this.connectionStream.Read();
                        this.connectionStream.Send(Message.Serialize(msg));
                        this.connectionStream.Read();

                        if (myBorderColor.Color == Colors.White) //Check if there is a border color of white (meaining king piece)
                        {
                            MoveCheckerPiece(true); //Move the KING checker piece
                        }
                        else
                        {
                            //No stroke present, thus error, so imply that it's a regulat checker piece 

                            MoveCheckerPiece(false); //Move the REGULAR checker piece 

                            if (myPieceColor.Color == Colors.Orange) //Change into dark orange if required
                            {
                                CheckIfKing(false);
                            }
                            else //Change into dark red if required 
                            {
                                CheckIfKing(true);
                            }
                        }

                        bool win = false;

                        if (playerColor == "orange")
                        {
                            if (this.RedPiece.Count == 0)
                            {
                                this.connectionStream.Send("!win");
                                win = true;
                            }
                        }
                        else
                        {
                            if (this.OrgPiece.Count == 0)
                            {
                                this.connectionStream.Send("!win");
                                win = true;
                            }
                        }
                        if (!win)
                        {
                            this.connectionStream.Send("!end");
                            turnStatus.Text = "NOT YOUR TURN";
                        }
                        else
                        {
                            turnStatus.Text = "YOU WON";
                        }
                        this.isAbleToMove = false;
                        this.middleGame.Start();

                    }
                }
            }
            catch (Exception exp)
            {
                //Post the error
                gameOutcomeMsg.Text = "Error with the move entered!"; 
            }
        }

        /// <summary>
        /// The method called during gameplay, so that when one player makes a move and his/her board is updated as well as the other
        /// opponents checker board being updated 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ojb"></param>
        private void MiddleGameUpdate(object sender, object ojb)
        {
            this.middleGame.Stop();

            string input = this.connectionStream.Read();
            if (input == "!prepare")
            {
                this.connectionStream.Send("!ok");
                Message tmp = Message.Deserialize(this.connectionStream.Read());
                this.connectionStream.Send("!ok");
                //UPDATE BOARD FROM THE OTHER PLAYER
                CheckMove(tmp.color, tmp.king, tmp.currentX + 1, tmp.currentY, tmp.newX + 1, tmp.newY);

                prevRow = tmp.currentY;
                prevColumn = tmp.currentX;

                desRow = tmp.newY;
                desColumnn = tmp.newX;

                if (tmp.king == true) //Check if a king piece returned 
                {
                    if (tmp.color == true) //Red king piece
                    {
                        myBorderColor = new SolidColorBrush(Colors.White);
                        myPieceColor = new SolidColorBrush(Colors.Red);
                        MoveCheckerPiece(true); //Move the KING checker piece
                    }
                    else //Orange king piece
                    {
                        myBorderColor = new SolidColorBrush(Colors.White);
                        myPieceColor = new SolidColorBrush(Colors.Orange);
                        MoveCheckerPiece(true); //Move the KING checker piece
                    }
                }
                else
                {
                    if (tmp.color == true) //Piece being moved is red
                    {
                        myBorderColor = new SolidColorBrush(Colors.Red);
                        myPieceColor = new SolidColorBrush(Colors.Red);
                        MoveCheckerPiece(false); //Move the REGULAR RED checker piece 
                        CheckIfKing(true); //Check if the red piece is a king now
                    }
                    else
                    {
                        myBorderColor = new SolidColorBrush(Colors.Orange);
                        myPieceColor = new SolidColorBrush(Colors.Orange);
                        MoveCheckerPiece(true); //Move the REGULAR ORANGE checker piece
                        CheckIfKing(false); //Check if the orange piece is a king now
                    }
                }
            }

            if (input == "!left")
            {
                turnStatus.Text = "ENEMY LEFT";
                isAbleToMove = false;
            }
            else
            {

                input = this.connectionStream.Read();
                bool lost = false;
                if (input == "!lost")
                {
                    turnStatus.Text = "YOU LOST";
                    isAbleToMove = false;
                    lost = true;
                }

                if (lost == false)
                {
                    if (input == "!turn")
                    {
                        this.isAbleToMove = true;
                        turnStatus.Text = "YOUR TURN";
                    }
                    if (input == "!left")
                    {
                        turnStatus.Text = "ENEMY LEFT";
                        isAbleToMove = false;
                    }
                }
            }
        }



        /// <summary>
        /// The event called when the user decides to make a move and click on the board, the event will condition to see if the user
        /// is able to make a move, or will do no action if it's not that player's turn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickBoard(object sender, TappedRoutedEventArgs e)
        {
            currSquareSelected = sender as Canvas;

            if (isAbleToMove) //So user must wait before being able to make a move
            {
                enterMove();
            }
        }



        /// <summary>
        /// Method checking to see if the piece placed on the current coordinate of the Checker Board indicates if the piece is a 
        /// regular checker piece or a king checker piece, both that vary in movements for the game logic, distinction of the two
        /// is important 
        /// </summary>
        /// <param name="colorStat"></param>
        /// <returns></returns>
        private bool CheckIfKing(bool colorStat)
        {
            bool kingStatus = false;

            if (colorStat) //For red piece
            {
                if ((desRow == 8) && (desColumnn == 1 || desColumnn == 3 || desColumnn == 5 || desColumnn == 7))
                {
                    myPieceColor = new SolidColorBrush(Colors.Red);
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    kingStatus = true;
                }
            }
            else //For orange piece 
            {
                if ((desRow == 1) && (desColumnn == 0 || desColumnn == 2 || desColumnn == 4 || desColumnn == 6))
                {
                    myPieceColor = new SolidColorBrush(Colors.Orange);
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    kingStatus = true;
                }
            }

            return kingStatus;
        }


        /// <summary>
        /// Involves changing the UI board design, the coordinates involved determine which part of the board will erase the
        /// checker piece and or draw and create a new one. This is just a visual representation of the game logic for the user. 
        /// </summary>
        /// <param name="isChangeStrokeColor"></param>
        private void MoveCheckerPiece(bool isChangeStrokeColor)
        {

            //Clear square if jump was done move was performed

            if ((prevRow - 2 == desRow && prevColumn + 2 == desColumnn))
            {
                boardSquares[prevRow - 1, prevColumn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                boardSquares[prevRow - 2, prevColumn + 1].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);


                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 100);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myPieceColor); //Stroke Color


                if (isChangeStrokeColor)
                {
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    //boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeThicknessProperty, 3); //Border color of yellow
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myBorderColor); //Stroke Color

                }

            }
            else if (prevRow - 2 == desRow && prevColumn - 2 == desColumnn)
            {
                boardSquares[prevRow - 1, prevColumn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                boardSquares[prevRow - 2, prevColumn - 1].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);


                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 100);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myPieceColor); //Stroke Color


                if (isChangeStrokeColor)
                {
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    //boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeThicknessProperty, 3); //Border color of yellow
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myBorderColor); //Stroke Color

                }
            }
            else if (prevRow + 2 == desRow && prevColumn - 2 == desColumnn)
            {
                boardSquares[prevRow - 1, prevColumn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                boardSquares[prevRow, prevColumn - 1].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                //boardSquares[prevRow, prevColumn - 1].Background = new SolidColorBrush(Colors.Purple);

                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 100);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myPieceColor); //Stroke Color


                if (isChangeStrokeColor)
                {
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    //boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeThicknessProperty, 3); //Border color of yellow
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myBorderColor); //Stroke Color

                }
            }
            else if (prevRow + 2 == desRow && prevColumn + 2 == desColumnn)
            {
                boardSquares[prevRow - 1, prevColumn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                boardSquares[prevRow, prevColumn + 1].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);
                //boardSquares[prevRow, prevColumn + 1].Background = new SolidColorBrush(Colors.Purple); 

                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 100);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myPieceColor); //Stroke Color


                if (isChangeStrokeColor)
                {
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    //boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeThicknessProperty, 3); //Border color of yellow
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myBorderColor); //Stroke Color

                }
            }
            else
            {
                boardSquares[prevRow - 1, prevColumn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 0);

                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.FillProperty, myPieceColor);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.OpacityProperty, 100);
                boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myPieceColor); //Stroke Color


                if (isChangeStrokeColor)
                {
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, new SolidColorBrush(Colors.White)); //Border color of yellow
                    boardSquares[desRow - 1, desColumnn].Children.ElementAt(0).SetValue(Ellipse.StrokeProperty, myBorderColor); //Stroke Color

                }
            }
        }


        /// <summary>
        /// The event that is triggered when the user clicks the 'Go Back' button, meaning they exit the game and leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goBackToStartUp_Click(object sender, RoutedEventArgs e)
        {
            this.connectionStream.Close();
            this.connectionStream.Dispose();
            this.Frame.Navigate(typeof(StartUpMenu)); //Go back to start up page            
        }


        /** GAME LOGIC AND ENGINE OF THE GAME **/
       

       
        public bool moveOrgKing(int curX, int curY, int desX, int desY)
        {
            bool RedPieceThere = false;
            bool OrgPieceThere = false;
            bool status = false;
            bool canMove = true;
            if (((curX + 1 == desX) || (curX - 1 == desX)) && (curY != desY))
            {
                int count = 0;
                int myOrgCount = 0;
                foreach (CheckerPiece myOrg in OrgPiece)
                {
                    if ((curX == myOrg.XPos) && (curY == myOrg.YPos))
                    {
                        myOrgCount = count;
                    }
                    if ((desX == myOrg.XPos) && (desY == myOrg.YPos))
                    {
                        canMove = false;
                        break;
                    }
                    count++;
                }
                if (canMove == false)
                {
                    status = false;
                }
                else
                {
                    //check apponent pieces if it there 
                    foreach (CheckerPiece myRed in RedPiece)
                    {
                        if ((myRed.XPos == desX) && (myRed.YPos == desY))
                        {
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove == false)
                    {
                        status = false;
                    }
                    else
                    {
                        OrgPiece.RemoveAt(myOrgCount);
                        OrgPiece.Add(new CheckerPiece(desX, desY, false, true));
                        status = true;
                    }
                }
            }
            if (((curY + 2 == desY) && ((curX + 2 == desX))) || ((curY - 2 == desY) && (curX - 2 == desX)) || ((curY - 2 == desY) && (curX + 2 == desX)) || ((curY + 2 == desY) && (curX - 2 == desX)))
            {
                //check oppnent pieces
                foreach (CheckerPiece CheckRedPieces in RedPiece)
                {
                    if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                    {
                        RedPieceThere = true;
                        break;
                    }
                }
                if (RedPieceThere == true)
                {
                    status = false;
                }
                else
                {
                    //check your pieces if it interfear
                    foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                    {
                        if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                        {
                            OrgPieceThere = true;
                            break;
                        }

                    }

                    //change status
                    if (OrgPieceThere == true)
                    {
                        status = false;
                    }
                    else
                    {
                        int pos1X = 0;
                        int pos1Y = 0;
                        int pos2X = 0;
                        int pos2Y = 0;
                        bool statusCondition1 = true;
                        bool statusCondition2 = true;
                        int counterToDelete = 0;
                        int removeAtSet1 = 0;
                        int removeAtSet2 = 0;
                        foreach (CheckerPiece redPiecesDelete in RedPiece)
                        {

                            if (((redPiecesDelete.XPos == curX - 1) && (redPiecesDelete.YPos == curY - 1))
                            || ((redPiecesDelete.XPos == curX - 1) && (redPiecesDelete.YPos == curY + 1)))
                            {
                                pos1X = redPiecesDelete.XPos;
                                pos1Y = redPiecesDelete.YPos;
                                statusCondition1 = false;
                                removeAtSet1 = counterToDelete;
                            }
                            if (((redPiecesDelete.XPos == curX + 1) && (redPiecesDelete.YPos == curY - 1)) ||
                               ((redPiecesDelete.XPos == curX + 1) && (redPiecesDelete.YPos == curY + 1)))
                            {
                                pos2X = redPiecesDelete.XPos;
                                pos2Y = redPiecesDelete.YPos;
                                statusCondition2 = false;
                                removeAtSet2 = counterToDelete;
                            }
                            counterToDelete++;
                        }

                        int deleteOrgList = 0;
                        foreach (CheckerPiece OrgPiecesDelete in OrgPiece)
                        {
                            if ((OrgPiecesDelete.XPos == curX) && (OrgPiecesDelete.YPos == curY))
                            {
                                break;
                            }
                            deleteOrgList++;
                        }




                        if ((statusCondition1 == false) && (statusCondition2 == false))
                        {
                            if (pos1X - 1 == desX)
                            {
                                RedPiece.RemoveAt(removeAtSet1);
                            }
                            else
                            {

                                RedPiece.RemoveAt(removeAtSet2);
                            }
                            OrgPiece.RemoveAt(deleteOrgList);

                            OrgPiece.Add(new CheckerPiece(desX, desY, false, true));

                            status = true;
                        }
                        else if ((statusCondition1 == false) && (statusCondition2 == true))
                        {
                            OrgPiece.RemoveAt(deleteOrgList);

                            OrgPiece.Add(new CheckerPiece(desX, desY, false, true));

                            RedPiece.RemoveAt(removeAtSet1);
                            status = true;

                        }
                        else if ((statusCondition1 == true) && (statusCondition2 == false))
                        {

                            RedPiece.RemoveAt(removeAtSet2);
                            OrgPiece.RemoveAt(deleteOrgList);
                            OrgPiece.Add(new CheckerPiece(desX, desY, false, true));


                            status = true;
                        }
                        else
                        {
                            status = false;
                        }


                        //loop to delete
                    }
                }

            }






            return status;
        }
        /// <summary>
        /// Move the red king Piece and check for the special condition compare to the other piece
        /// </summary>
        /// <param name="curX"></param>
        /// <param name="curY"></param>
        /// <param name="desX"></param>
        /// <param name="desY"></param>
        /// <returns></returns>
        public bool moveRedKing(int curX, int curY, int desX, int desY)
        {
            bool RedPieceThere = false;
            bool canMove = true;
            bool OrgPieceThere = false;
            bool status = false;

            //Check movement regular

            if (((curX + 1 == desX) || (curX - 1 == desX)) && (curY != desY))
            {
                int count = 0;
                int myRedCount = 0;
                foreach (CheckerPiece myRed in RedPiece)
                {
                    if ((curX == myRed.XPos) && (curY == myRed.YPos))
                    {
                        myRedCount = count;
                    }
                    if ((desX == myRed.XPos) && (desY == myRed.YPos))
                    {
                        canMove = false;
                        break;
                    }
                    count++;
                }
                if (canMove == false)
                {
                    status = false;
                }
                else
                {
                    //check apponent pieces if it there 
                    foreach (CheckerPiece myOrg in OrgPiece)
                    {
                        if ((myOrg.XPos == desX) && (myOrg.YPos == desY))
                        {
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove == false)
                    {
                        status = false;
                    }
                    else
                    {
                        RedPiece.RemoveAt(myRedCount);
                        RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                        status = true;
                    }
                }
            }

            if (((curX + 2 == desX) || (curX - 2 == desX)) && ((curY + 2 == desY) || (curY - 2 == desY)))
            {
                //check oppnent pieces
                foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                {
                    if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                    {
                        OrgPieceThere = true;
                        break;
                    }
                }
                if (OrgPieceThere == true)
                {
                    status = false;
                }
                else
                {
                    //check your pieces if it interfear
                    foreach (CheckerPiece CheckRedPieces in RedPiece)
                    {
                        if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                        {
                            RedPieceThere = true;
                            break;
                        }

                    }

                    //change status
                    if (RedPieceThere == true)
                    {
                        status = false;
                    }
                    else
                    {
                        int pos1X = 0;
                        int pos1Y = 0;
                        int pos2X = 0;
                        int pos2Y = 0;
                        bool statusCondition1 = true;
                        bool statusCondition2 = true;
                        int counterToDelete = 0;
                        int removeAtSet1 = 0;
                        int removeAtSet2 = 0;
                        //check to see if the kill is on the left and right
                        foreach (CheckerPiece orgPiecesDelete in OrgPiece)
                        {
                            if (((orgPiecesDelete.XPos == curX + 1) && (orgPiecesDelete.YPos == curY + 1))
                            || ((orgPiecesDelete.XPos == curX + 1) && (orgPiecesDelete.YPos == curY - 1)))
                            {
                                pos1X = orgPiecesDelete.XPos;
                                pos1Y = orgPiecesDelete.YPos;
                                statusCondition1 = false;
                                removeAtSet1 = counterToDelete;
                            }
                            if (((orgPiecesDelete.XPos == curX - 1) && (orgPiecesDelete.YPos == curY - 1)) ||
                               ((orgPiecesDelete.XPos == curX + 1) && (orgPiecesDelete.YPos == curY + 1)))

                            {
                                pos2X = orgPiecesDelete.XPos;
                                pos2Y = orgPiecesDelete.YPos;
                                statusCondition2 = false;
                                removeAtSet2 = counterToDelete;
                            }
                            counterToDelete++;
                        }
                        int deleteOrgList = 0;
                        foreach (CheckerPiece OrgPiecesDelete in RedPiece)
                        {
                            if ((OrgPiecesDelete.XPos == curX) && (OrgPiecesDelete.YPos == curY))
                            {
                                break;
                            }
                            deleteOrgList++;
                        }



                        if ((statusCondition1 == false) && (statusCondition2 == false))
                        {
                            if (pos1X - 1 == desX)
                            {
                                OrgPiece.RemoveAt(removeAtSet1);
                            }
                            else
                            {
                                OrgPiece.RemoveAt(removeAtSet2);
                            }
                            RedPiece.RemoveAt(deleteOrgList);
                            if (desX == 8)
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                            }
                            else
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, false));
                            }
                            status = true;
                        }
                        else if ((statusCondition1 == false) && (statusCondition2 == true))
                        {
                            OrgPiece.RemoveAt(removeAtSet1);
                            RedPiece.RemoveAt(deleteOrgList);

                            RedPiece.Add(new CheckerPiece(desX, desY, true, true));

                            status = true;

                        }
                        else if ((statusCondition1 == true) && (statusCondition2 == false))
                        {

                            OrgPiece.RemoveAt(removeAtSet2);
                            RedPiece.RemoveAt(deleteOrgList);

                            RedPiece.Add(new CheckerPiece(desX, desY, true, true));

                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                }

            }
            return status;
        }

        /// <summary>
        /// Check the move to be move for the king or regular piece
        /// </summary>
        /// <param name="color"></param>
        /// <param name="isKingPiece"></param>
        /// <param name="curX"></param>
        /// <param name="curY"></param>
        /// <param name="desX"></param>
        /// <param name="desY"></param>
        /// <returns></returns>

        bool CheckMove(bool color, bool isKingPiece, int curX, int curY, int desX, int desY)
        {
            //IF isKingPiece is TRUE that means its a king piece

            List<CheckerPiece> Piece = new List<CheckerPiece>();
            bool status = false; //Return to UI to see if move chosen was valid
            if (isKingPiece == true)
            {
                if (color == true)
                {
                    bool StatusRedKing = moveRedKing(curX, curY, desX, desY);
                    if (StatusRedKing == true)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                else
                {
                    bool statusOrgKing = true;
                    statusOrgKing = moveOrgKing(curX, curY, desX, desY);
                    if (statusOrgKing == true)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            else
            {


                if ((curX == desX) && (curY == desY)) //If user clicked the same square, then obviously the move was incorrect
                {
                    status = false;
                }
                else
                {
                    if (color == true) //Perform logic validation for the RED Piece 
                    {
                        bool statusRed = moveRedPiece(curX, curY, desX, desY);

                        if (statusRed == true)
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    else //Perform logic validation for the ORANGE piece
                    {
                        bool statusOrange = moveOrgPiece(curX, curY, desX, desY); //Moves the orange piece, checks if move is valid

                        if (statusOrange == true)
                        {
                            status = true; //Move valid
                        }
                        else
                        {
                            status = false;
                        }
                    }
                }
            }

            return status;
        }










        //LOGIC FOR MOVING ORANGE PIECE 

/// <summary>
/// move regular organge pieces
/// </summary>
/// <param name="curX"></param>
/// <param name="curY"></param>
/// <param name="desX"></param>
/// <param name="desY"></param>
/// <returns></returns>
        public bool moveOrgPiece(int curX, int curY, int desX, int desY)
        {
            bool status = false; //Returns if the move was valid or not
            bool RedPieceThere = false;
            bool OrgPieceThere = false;

            //check the position of the pieces to see if its in the cordinate
            if ((curY - 1 == desY) && ((curX + 1 == desX) || (curX - 1 == desX)))
            {
                //check to see if you piece is there or not
                foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                {
                    if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                    {
                        OrgPieceThere = true; //Found an orange piece on the space user wants to go to
                        break;
                    }
                }

                if (OrgPieceThere == true)
                {
                    status = false; //Move was invalid because of the same piece being on the spot
                }
                else
                {
                    //check to see if there red pices there or not
                    foreach (CheckerPiece CheckRedPieces in RedPiece)
                    {
                        if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                        {
                            RedPieceThere = true;
                            break;
                        }
                    }
                    //if red piece is there that means you cannot move
                    if (RedPieceThere == true)
                    {
                        status = false; //Move was invalid because of the another piece being on the spot
                    }
                    else
                    {
                        //if no pieces are there then move
                        int i = 0;
                        foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                        {
                            if ((curX == CheckOrgPieces.XPos) && (curY == CheckOrgPieces.YPos))
                            {
                                OrgPiece.RemoveAt(i);
                                if (desX == 1)
                                {
                                    OrgPiece.Add(new CheckerPiece(desX, desY, false, true));
                                }
                                else
                                {
                                    OrgPiece.Add(new CheckerPiece(desX, desY, false, false));
                                }

                                break;
                            }
                            i++;
                        }
                        //if no pieces are there then move
                        status = true;
                    }
                }

            }



            //check for a kill
            if ((curY - 2 == desY) && ((curX + 2 == desX) || (curX - 2 == desX)))
            {
                //check oppnent pieces
                foreach (CheckerPiece CheckRedPieces in RedPiece)
                {
                    if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                    {
                        RedPieceThere = true;
                        break;
                    }
                }
                if (RedPieceThere == true)
                {
                    status = false;
                }
                else
                {
                    //check your pieces if it interfear
                    foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                    {
                        if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                        {
                            OrgPieceThere = true;
                            break;
                        }

                    }

                    //change status
                    if (OrgPieceThere == true)
                    {
                        status = false;
                    }
                    else
                    {
                        int pos1X = 0;
                        int pos1Y = 0;
                        int pos2X = 0;
                        int pos2Y = 0;
                        bool statusCondition1 = true;
                        bool statusCondition2 = true;
                        int counterToDelete = 0;
                        int removeAtSet1 = 0;
                        int removeAtSet2 = 0;
                        foreach (CheckerPiece redPiecesDelete in RedPiece)
                        {
                            if ((redPiecesDelete.XPos == curX - 1) && (redPiecesDelete.YPos == curY - 1))
                            {
                                pos1X = redPiecesDelete.XPos;
                                pos1Y = redPiecesDelete.YPos;
                                statusCondition1 = false;
                                removeAtSet1 = counterToDelete;
                            }
                            if ((redPiecesDelete.XPos == curX + 1) && (redPiecesDelete.YPos == curY - 1))
                            {
                                pos2X = redPiecesDelete.XPos;
                                pos2Y = redPiecesDelete.YPos;
                                statusCondition2 = false;
                                removeAtSet2 = counterToDelete;
                            }
                            counterToDelete++;
                        }

                        int deleteOrgList = 0;
                        foreach (CheckerPiece OrgPiecesDelete in OrgPiece)
                        {
                            if ((OrgPiecesDelete.XPos == curX) && (OrgPiecesDelete.YPos == curY))
                            {
                                break;
                            }
                            deleteOrgList++;
                        }




                        if ((statusCondition1 == false) && (statusCondition2 == false))
                        {
                            if (pos1X - 1 == desX)
                            {
                                RedPiece.RemoveAt(removeAtSet1);
                            }
                            else
                            {

                                RedPiece.RemoveAt(removeAtSet2);
                            }
                            OrgPiece.RemoveAt(deleteOrgList);
                            if (desX == 1)
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, true));
                            }
                            else
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, false));
                            }
                            status = true;
                        }
                        else if ((statusCondition1 == false) && (statusCondition2 == true))
                        {
                            OrgPiece.RemoveAt(deleteOrgList);
                            if (desX == 1)
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, true));
                            }
                            else
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, false));
                            }
                            RedPiece.RemoveAt(removeAtSet1);
                            status = true;

                        }
                        else if ((statusCondition1 == true) && (statusCondition2 == false))
                        {

                            RedPiece.RemoveAt(removeAtSet2);
                            OrgPiece.RemoveAt(deleteOrgList);
                            if (desX == 1)
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, true));
                            }
                            else
                            {
                                OrgPiece.Add(new CheckerPiece(desX, desY, false, false));
                            }

                            status = true;
                        }
                        else
                        {
                            status = false;
                        }  //loop to delete
                    }
                }

            }

            return status;
        }



        /// <summary>
        /// Move regular red piece
        /// </summary>
        /// <param name="curX"></param>
        /// <param name="curY"></param>
        /// <param name="desX"></param>
        /// <param name="desY"></param>
        /// <returns></returns>
        public bool moveRedPiece(int curX, int curY, int desX, int desY)
        {
            bool status = false; //Returns if the move was valid or not
            bool RedPieceThere = false;
            bool OrgPieceThere = false;
            if ((curY + 1 == desY) && ((curX + 1 == desX) || (curX - 1 == desX)))
            {
                foreach (CheckerPiece CheckRedPieces in RedPiece)
                {
                    if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                    {
                        RedPieceThere = true; //Found an orange piece on the space user wants to go to
                        break;
                    }
                }
                if (RedPieceThere == true)
                {
                    status = false; //Move was invalid because of the same piece being on the spot
                }
                else
                {
                    foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                    {
                        if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                        {
                            OrgPieceThere = true;
                            break;
                        }
                    }
                    //if red piece is there that means you cannot move
                    if (OrgPieceThere == true)
                    {
                        status = false; //Move was invalid because of the another piece being on the spot
                    }
                    else
                    {
                        //if no pieces are there then move
                        int i = 0;
                        foreach (CheckerPiece CheckRedPieces in RedPiece)
                        {
                            if ((curX == CheckRedPieces.XPos) && (curY == CheckRedPieces.YPos))
                            {
                                RedPiece.RemoveAt(i);

                                if (desX == 8)
                                {
                                    RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                                }
                                else
                                {
                                    RedPiece.Add(new CheckerPiece(desX, desY, true, false));
                                }
                                break;
                            }
                            i++;
                        }
                        //if no pieces are there then move
                        status = true;
                    }
                }

            }


            if ((curY + 2 == desY) && ((curX + 2 == desX) || (curX - 2 == desX)))
            {
                //check oppnent pieces
                foreach (CheckerPiece CheckOrgPieces in OrgPiece)
                {
                    if ((desX == CheckOrgPieces.XPos) && (desY == CheckOrgPieces.YPos))
                    {
                        OrgPieceThere = true;
                        break;
                    }
                }
                if (OrgPieceThere == true)
                {
                    status = false;
                }
                else
                {
                    //check your pieces if it interfear
                    foreach (CheckerPiece CheckRedPieces in RedPiece)
                    {
                        if ((desX == CheckRedPieces.XPos) && (desY == CheckRedPieces.YPos))
                        {
                            RedPieceThere = true;
                            break;
                        }

                    }

                    //change status
                    if (RedPieceThere == true)
                    {
                        status = false;
                    }
                    else
                    {
                        int pos1X = 0;
                        int pos1Y = 0;
                        int pos2X = 0;
                        int pos2Y = 0;
                        bool statusCondition1 = true;
                        bool statusCondition2 = true;
                        int counterToDelete = 0;
                        int removeAtSet1 = 0;
                        int removeAtSet2 = 0;
                        foreach (CheckerPiece orgPiecesDelete in OrgPiece)
                        {
                            if (((orgPiecesDelete.XPos == curX - 1) && (orgPiecesDelete.YPos == curY + 1)) || ((orgPiecesDelete.XPos == curX - 1) && (orgPiecesDelete.YPos == curY - 1)))
                            {
                                pos1X = orgPiecesDelete.XPos;
                                pos1Y = orgPiecesDelete.YPos;
                                statusCondition1 = false;
                                removeAtSet1 = counterToDelete;
                            }
                            if (((orgPiecesDelete.XPos == curX + 1) && (orgPiecesDelete.YPos == curY + 1)) || ((orgPiecesDelete.XPos == curX + 1) && (orgPiecesDelete.YPos == curY - 1)))
                            {
                                pos2X = orgPiecesDelete.XPos;
                                pos2Y = orgPiecesDelete.YPos;
                                statusCondition2 = false;
                                removeAtSet2 = counterToDelete;
                            }
                            counterToDelete++;
                        }
                        int deleteOrgList = 0;
                        foreach (CheckerPiece OrgPiecesDelete in RedPiece)
                        {
                            if ((OrgPiecesDelete.XPos == curX) && (OrgPiecesDelete.YPos == curY))
                            {
                                break;
                            }
                            deleteOrgList++;
                        }



                        if ((statusCondition1 == false) && (statusCondition2 == false))
                        {
                            if (pos1X - 1 == desX)
                            {
                                OrgPiece.RemoveAt(removeAtSet1);
                            }
                            else
                            {
                                OrgPiece.RemoveAt(removeAtSet2);
                            }
                            RedPiece.RemoveAt(deleteOrgList);
                            if (desX == 8)
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                            }
                            else
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, false));
                            }
                            status = true;
                        }
                        else if ((statusCondition1 == false) && (statusCondition2 == true))
                        {
                            OrgPiece.RemoveAt(removeAtSet1);
                            RedPiece.RemoveAt(deleteOrgList);
                            if (desX == 8)
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                            }
                            else
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, false));
                            }
                            status = true;

                        }
                        else if ((statusCondition1 == true) && (statusCondition2 == false))
                        {

                            OrgPiece.RemoveAt(removeAtSet2);
                            RedPiece.RemoveAt(deleteOrgList);
                            if (desX == 8)
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, true));
                            }
                            else
                            {
                                RedPiece.Add(new CheckerPiece(desX, desY, true, false));
                            }
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                }

            }


            return status;
        }



        //CREATING THE CHECKER PIECES ON THE GAME LOGIC SIDE 


            /// <summary>
            /// Create Checker Piece on the board for Red and Organge
            /// </summary>
        public void CreateCheckerPiece()
        {
            //loop through a grid for the y position
            for (int i = 1; i <= 8; i++)
            {
                //loop through the gird for the x position
                for (int w = 1; w <= 8; w++)
                {
                    //put a checker piece only if i is less then 3 because 3 represent the all the Black Piece
                    if (i <= 3)
                    {
                        if (i % 2 != 0)
                        {
                            if (w % 2 != 0)
                            {
                                RedPiece.Add(new CheckerPiece(w, i, false, false));
                            }
                        }
                        else
                        {
                            if (w % 2 == 0)
                            {
                                RedPiece.Add(new CheckerPiece(w, i, false, false));
                            }
                        }

                    }
                    if (i >= 6)
                    {
                        if (i % 2 == 0)
                        {
                            if (w % 2 == 0)
                            {
                                OrgPiece.Add(new CheckerPiece(w, i, false, false));
                            }
                        }
                        else
                        {
                            if (w % 2 != 0)
                            {
                                OrgPiece.Add(new CheckerPiece(w, i, false, false));
                            }
                        }
                    }
                }
            }
        }
    }
}