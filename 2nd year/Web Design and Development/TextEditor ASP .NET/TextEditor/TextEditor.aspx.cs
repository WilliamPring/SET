
/*    
 * Filename: TextEditor.aspx.cs
 * Assignment: Assignment 5 WDD
 * By: Naween Mehanmal and William Pring
 * Date: November 16, 2015
 * Description: Asp .Net Text Editor that save to a folder on the server, also can pick 
 */ 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TextEditor
{
    public partial class TextEditor : System.Web.UI.Page
    {

        /*	
            Function Name: Page_Load
            return: Void
            Parameters: object sender, EventArgs e
            Purpose: When the page first loads
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //create directory for MyFiles
                Directory.CreateDirectory(Server.MapPath("MyFiles"));
                //state for the mySaveFile
                ViewState["mySaveFile"] = "";
                fillListBox(); //Initiliaze the listbox with file names
                
            }
        }


        /*	
            Function Name: saveButton_Click
            return: Void
            Parameters: object sender, EventArgs e
            Purpose: When you are save file
        */
        protected void saveButton_Click(object sender, EventArgs e)
        {
            //view state if empty and null
            if (ViewState["mySaveFile"].ToString() != "" && ViewState["mySaveFile"].ToString() != null)
            {
                saveErrorMsg.InnerText = ""; //Clear the error message 
                openErrorMsg.InnerText = ""; //Clear the error message 


                //textPad.InnerText = ViewState["mySaveFile"].ToString();
                saveFile(ViewState["mySaveFile"].ToString());
            }
            else
            {
                saveErrorMsg.InnerText = "*Did not specify file name to SaveAs!\n";
                openErrorMsg.InnerText = ""; //Clear the error message 
            }
        }


        /*	
             Function Name: saveAsButton_Click
             return: Void
             Parameters: object sender, EventArgs e
             Purpose: When you save a file to a new file
         */
        protected void saveAsButton_Click(object sender, EventArgs e)
        {
            string saveFileName = saveAsTextBox.Text;

            saveErrorMsg.InnerText = ""; //Clear the error message 
            openErrorMsg.InnerText = ""; //Clear the error message 

            saveAsTextBox.Text = ""; //Clear out the SaveAs textbox 
            ViewState["mySaveFile"] = saveFileName;
            saveFile(saveFileName); //Save the file to the root directory      
        }



        /*	
             Function Name: saveFile
             return: Void
             Parameters: object sender, EventArgs e
             Purpose: When you save a current file changes
         */
        private void saveFile(string fileName)
        {
            string textContent = textPad.InnerText;
            string folderPath = Server.MapPath("~\\MyFiles\\");

            bool uploadFile = true;

            string[] files = Directory.GetFiles(Server.MapPath(@"MyFiles\"), "*.txt");

            //Test if file already exists on the server's directory

            foreach (string str in files)
            {
                string tempStr = str.Substring(str.LastIndexOf("\\") + 1);

                if ((tempStr == fileName) || (tempStr == fileName + ".txt"))
                {
                    uploadFile = false;
                }
            }

            if (fileName.IndexOf(".txt") == -1 && (fileName != "")) //check if extension was included or not
            {
                fileName += ".txt";
            }

            using (StreamWriter myFile = new StreamWriter(folderPath + fileName))
            {
                string printTextFile = textContent.Replace("\n", "\r\n");
                myFile.WriteLine(printTextFile);             

                if(uploadFile)
                {
                    dirListBox.Items.Add(fileName);
                }
            }
        }


        /*	
         Function Name: openButton_Click
         return: Void
         Parameters: object sender, EventArgs e
         Purpose: When you are opening a file in a folder
        */
        protected void openButton_Click(object sender, EventArgs e)
        {
            string openFileName = dirListBox.Text;
            string folderPath = Server.MapPath("~\\MyFiles\\");
            
            try
            {
                openErrorMsg.InnerText = "";
                if (openFileName.IndexOf(".txt") == -1 && (openFileName != "")) //check if extension was included or not
                {
                    openFileName += ".txt"; 
                }
                textPad.InnerText = openFileName;

                using (StreamReader readFile = new StreamReader(folderPath + openFileName))
                {
                    saveErrorMsg.InnerText = ""; //Clear the error message 
                    openErrorMsg.InnerText = ""; //Clear the error message 

                    string grabInput;
                    ViewState["mySaveFile"] = openFileName; //Must specify new file name to SaveAs
                    textPad.InnerText = "";

                    while ((grabInput = readFile.ReadLine()) != null)
                    {
                        textPad.InnerText += grabInput + "\n";
                    }
                }
            }
            catch(Exception exp)
            {
                //Could not open the file
                openErrorMsg.InnerText = "*File not found!\n";
                saveErrorMsg.InnerText = ""; //Clear the error message 

            }
        }

        /*	
      Function Name: fillListBox
      return: Void
      Parameters: object sender, EventArgs e
      Purpose: Filling the list box wiht the files
     */

        private void fillListBox()
        {
            string[] files = Directory.GetFiles(Server.MapPath(@"MyFiles\"), "*.txt");

            foreach(string str in files)
            {
                dirListBox.Items.Add(str.Substring(str.LastIndexOf("\\") + 1)); 
            }
        }
        /*	
        Function Name: fillListBox
        return: Void
        Parameters: object sender, EventArgs e
        Purpose: Updating the box with the files
        */
        protected void UpdateBox_Click(object sender, EventArgs e)
        {
            dirListBox.Items.Clear();
            fillListBox();
        }


    }
}