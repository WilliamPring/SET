using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hi_Low_Game
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false)
            {
                ViewState["caseCount"] = 1;
                ViewState["status"] = "true"; 
            }
        }

        protected void submit(object sender, EventArgs e)
        {
            if(IsValid)
            {
                spotInGameQuestions(); 
            }         
        }
     
        public void spotInGameQuestions()
        {         
            if((int)ViewState["caseCount"] == 1) //Ask the user for a number
            {
                gameTextbox.Visible = true; //Show the textbox again when deciding to replay the game

                ViewState["caseCount"] = 2;

                body.Attributes.Add("bgcolor", "white");

                if (ViewState["status"].ToString() == "true")
                {
                  ViewState["Name"] = gameTextbox.Text;                  
                }       
                
                gameTextbox.Text = ""; //Clear the textbox                 
                mainLabel.Text = "Welcome " + ViewState["Name"].ToString() + "!<br />Enter in a number: "; //New question for the label to display 

                /** Change the regex of the validator **/
                validateRegex.ValidationExpression = "\\d+";
                validateRegex.ErrorMessage = "Must only be a number!";

            }
            else if ((int)ViewState["caseCount"] == 2) //The game starts, and the user will enter in answers, handled here 
            {
                int number = 0;
                ViewState["maxChoice"] = gameTextbox.Text;

                if (ViewState["maxChoice"].ToString() == "1")
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
            else if ((int)ViewState["caseCount"] == 3)
            {
                gameEngineLogic(); 
            }
        }

        public void randomNumberGenerator(int number)
        {
            Random newNumber = new Random();
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