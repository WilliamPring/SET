/*    
 * Filename: Default.aspx.cs
 * Assignment: Assignment 4 WDD
 * By: Naween Mehanmal and William Pring
 * Date: November 16, 2015
 * Description: This is the code behind which enables the event driven part of the program and also has the basis of the 
 * Hi-Low game logic, which is altered through different events and control events 
 */
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hi_Low_Game
{
    public partial class _Default : System.Web.UI.Page
    {
        Random newNumber = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false) //When the page is ran once at the beginning, initiliaze certain variables, this if statement will not be false for the remainder of the program 
            {
                ViewState["caseCount"] = 1; //Start of the question, prompting user for the name (Contains the logic behind it)
                ViewState["status"] = "true"; //True if the name of the user is not contained yet by the program's ViewState
            }
        }

        protected void submit(object sender, EventArgs e)
        {
            if(IsValid) //Ideal so that users can't accidentally get past the client's side validation 
            {
                spotInGameQuestions();  
            }         
        }
     
        public void spotInGameQuestions()
        {         
            if((int)ViewState["caseCount"] == 1) //Ask the user for a number
            {
                gameTextbox.Visible = true; //Show the textbox again when deciding to replay the game
                buttonID.Text = "Submit"; //Rename the button to 'submit', so user knows what the button is implying 

                ViewState["caseCount"] = 2; //2nd case condition so that the next question can get prompt

                body.Attributes.Add("bgcolor", "white"); //Change the background colour back to default, which is white. 

                if (ViewState["status"].ToString() == "true") //Grab the ViewState's variable for name through the text in the textbox, if the variable doesn't have a value for name yet
                {
                  ViewState["Name"] = gameTextbox.Text;                  
                }       
                
                gameTextbox.Text = ""; //Clear the textbox                 
                mainLabel.Text = "Welcome " + ViewState["Name"].ToString() + "!<br />Enter in a number: "; //New question for the label to display 

                /** Change the regex of the validator **/
                validateRegex.ValidationExpression = "\\d+";
                validateRegex.ErrorMessage = "Must only be a number!"; //New display message to correspond to the 2nd question for number entering 

            }
            else if ((int)ViewState["caseCount"] == 2) //The game starts, and the user will enter in answers, handled here 
            {
                int number = 0;
                ViewState["maxChoice"] = gameTextbox.Text;

                if (ViewState["maxChoice"].ToString() == "1") //Cannot enter in 1 as the seedF number, would make the game illogical 
                {
                    gameTextbox.Text = ""; 
                    mainLabel.Text = "You cannot enter in 1 as your choice of number! Please retry again.";                      
                }
                else
                {

                    ViewState["caseCount"] = 3;

                    gameTextbox.Text = ""; //Clear the textbox  
                    ViewState["minChoice"] = 1; //Start off as 1 for the minimum choice and work your way up 
                    mainLabel.Text = ViewState["Name"].ToString() + ", enter in a number between " + ViewState["minChoice"].ToString() + " and " + ViewState["maxChoice"].ToString() + ":";

                    //Generate the random number within a specified range 
                    number = Int32.Parse(ViewState["maxChoice"].ToString());
                    randomNumberGenerator(number);
                    //mainLabel.Text += "<br />Easter Bunny says answer is: " + ViewState["gameAnswer"].ToString();
                }


            }
            else if ((int)ViewState["caseCount"] == 3) //check if user input base on the game logic condition
            {
                gameEngineLogic(); 
            }
        }

        public void randomNumberGenerator(int number)
        {
            ViewState["gameAnswer"] = newNumber.Next(1, number);
        }

        public void gameEngineLogic()
        {
            int userAnswer = Int32.Parse(gameTextbox.Text);
            int maxChoice = Int32.Parse(ViewState["maxChoice"].ToString());
            int minChoice = Int32.Parse(ViewState["minChoice"].ToString());
            int theGameAnswer = Int32.Parse(ViewState["gameAnswer"].ToString()); 
            
            if(userAnswer == theGameAnswer)
            {
                mainLabel.Text = "Congratulations you won!";
                gameTextbox.Visible = false;
                body.Attributes.Add("bgcolor", "orange");

                //Re-ask if they want to play again

                buttonID.Text = "Play Again";
                ViewState["caseCount"] = 1;

                ViewState["status"] = "false";
            }
            else if(userAnswer > theGameAnswer)
            {
                if(userAnswer < maxChoice)
                {
                    ViewState["maxChoice"] = userAnswer; 
                    gameTextbox.Text = "";
                    mainLabel.Text = "Number was too high! Enter in a number between " + ViewState["minChoice"].ToString() + " and " + ViewState["maxChoice"].ToString() + ":";
                }
                else
                {
                    gameTextbox.Text = "";
                    mainLabel.Text = "Number was out of range! Enter in a number between " + ViewState["minChoice"].ToString() + " and " + ViewState["maxChoice"].ToString() + ":";
                }
            }
            else if(userAnswer < theGameAnswer)
            {
                if (userAnswer > minChoice)
                {
                    ViewState["minChoice"] = userAnswer;
                    gameTextbox.Text = "";
                    mainLabel.Text = "Number was too low! Enter in a number between " + ViewState["minChoice"].ToString() + " and " + ViewState["maxChoice"].ToString() + ":";
                }
                else
                {
                    gameTextbox.Text = "";
                    mainLabel.Text = "Number was out of range! Enter in a number between " + ViewState["minChoice"].ToString() + " and " + ViewState["maxChoice"].ToString() + ":";
                }
            }
        }
    }
}