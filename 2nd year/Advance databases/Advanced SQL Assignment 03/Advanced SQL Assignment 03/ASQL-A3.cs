/*
* Filename:	ASQL-A3.cs
* Project:	ASQL: Assignment 3 - Programming Abstractions
* By:		William Pring
* Date:		March 3, 2016
* Description:	This program will transfer table information to another table
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_SQL_Assignment_03
{
    /// <summary>
    /// ASQL project view
    /// </summary>
    public partial class ASQL : Form
    {
        string sourceConnectionString = "";
        string destinationConnectionString = "";
        public ASQL()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Submit to execute program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {
            string tableSrc = "";
            string tableReading = "";
            string errorMessageTotal = "Error List:\n\n";
            //check parmeter to see if its empty
            if (sourceTable.TextLength ==0) 
            {
                errorMessageTotal += "Error Missing Parameter For Source Table\n";
            }
            //check parmeter to see if its empty
            if (destinationTable.TextLength == 0)
            {
                errorMessageTotal += "Error Missing Parameter For Destination Table\n";
            }
            if ((sourceConnectionString == null) || (destinationConnectionString == null))
            {
                 errorMessageTotal += "Connection String Error\n";
            }
            if (errorMessageTotal != "Error List:\n\n")
            {
                System.Windows.Forms.MessageBox.Show(errorMessageTotal);
            }
            else
            {
                //get the text for the table name
                tableReading = sourceTable.Text;
                //get the text for the table name
                tableSrc = destinationTable.Text;
                //create the ConnectionClass
                ConnectionClass connection = new ConnectionClass(sourceConnectionString, destinationConnectionString, tableReading, tableSrc);
                //Connect for the sorce and dest
                bool status = connection.Connect();
                if(status == false)
                {
                    //getting infromation about the table
                    status = connection.GetInformation();
                    if (status == false)
                    {
                        //inserting it to the new table
                        connection.SetInformation();
                    }
                    else
                    {
                        //error message
                        errorMessageTotal += "Writing Table Error\n";
                        System.Windows.Forms.MessageBox.Show(errorMessageTotal);
                    }
                }
                else
                {
                    errorMessageTotal += "Copying Table Error\n";
                    System.Windows.Forms.MessageBox.Show(errorMessageTotal);
                }
            }
        }

        /// <summary>
        /// ConnectionString builder for source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionStringSourceBtn_Click(object sender, EventArgs e)
        {
            object _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            _con = _link.PromptNew();
            if (_con != null)
            {
                sourceConnectionString = ((ADODB.Connection)_con).ConnectionString;
            }
        }
        /// <summary>
        /// Connection string builder for destination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionStringDestinationBtn_Click(object sender, EventArgs e)
        {
            object _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            _con = _link.PromptNew();
            if (_con != null)
            {
                destinationConnectionString = ((ADODB.Connection)_con).ConnectionString;
            }
        }
    }
}
