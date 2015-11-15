using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hi_Low_Game
{
    public partial class _Default : System.Web.UI.Page
    {
        string playerName;
        string playerEnteredNumber; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false)
            {
                playerName = "";
                ViewState["caseCount"] = 1;  
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
                ViewState["caseCount"] = 2; 

                ViewState["Name"] = gameTextbox.Text; 
                playerName = ViewState["Name"].ToString(); 
                
                gameTextbox.Text = ""; //Clear the textbox                 
                mainLabel.Text = "Welcome " + playerName + "!<br />Enter in a number: "; //New question for the label to display 

                /** Change the regex of the validator **/
                validateRegex.ValidationExpression = "\\d+";
                validateRegex.ErrorMessage = "Must only be a number!";

            }
            else if ((int)ViewState["caseCount"] == 2) //The game starts, and the user will enter in answers, handled here 
            {
                int number = 0; 
                ViewState["caseCount"] = 3;

                ViewState["Number"] = gameTextbox.Text;
                playerEnteredNumber = ViewState["Number"].ToString();

                gameTextbox.Text = ""; //Clear the textbox                 
                mainLabel.Text = ViewState["Name"].ToString() + ", enter in a number between 1 and " + playerEnteredNumber.ToString() + ":";
               
                //Generate the random number within a specified range 
                number = Int32.Parse(ViewState["Number"].ToString());
                randomNumberGenerator(number); 
                mainLabel.Text += "<br />Easter Bunny says answer is: " + ViewState["gameAnswer"].ToString();
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

        }
    }
}