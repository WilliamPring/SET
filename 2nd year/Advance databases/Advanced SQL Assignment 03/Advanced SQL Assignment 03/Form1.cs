﻿using System;
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
    public partial class Form1 : Form
    {
        string sourceConnectionString = "";
        string destinationConnectionString = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            string tableSrc = "";
            string tableReading = "";
            string errorMessageTotal = "Error List:\n\n";
            if (sourceTable.TextLength ==0) 
            {
                errorMessageTotal += "Error Missing Parameter For Source Table\n";
            }
            if (destinationTable.TextLength == 0)
            {
                errorMessageTotal += "Error Missing Parameter For Destination Table\n";
            }
            if ((sourceConnectionString == null) || (destinationConnectionString == null))
            {
                 errorMessageTotal += "Connection String Error\n";
            }
            if (errorMessageTotal != "Error List\n")
            {
                System.Windows.Forms.MessageBox.Show(errorMessageTotal);
            }
            else
            {
                tableReading = sourceTable.Text;
                tableSrc = destinationTable.Text;
                ConnectionClass connection = new ConnectionClass(sourceConnectionString, destinationConnectionString, tableReading, tableSrc);
                bool status = connection.Connect();
                if(status == false)
                {
                    status = connection.GetInformation();
                    if (status == false)
                    {
                        connection.SetInformation();
                    }
                    else
                    {
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


        private void ConnectionStringSourceBtn_Click(object sender, EventArgs e)
        {
            object _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            _con = _link.PromptNew();
            if (_con == null)
            {
                //Do Something later
            }
            else
            {
                sourceConnectionString = ((ADODB.Connection)_con).ConnectionString;
            }
        }

        private void ConnectionStringDestinationBtn_Click(object sender, EventArgs e)
        {
            object _con = null;
            MSDASC.DataLinks _link = new MSDASC.DataLinks();
            _con = _link.PromptNew();
            if (_con == null)
            {
                //Do Something later
            }
            else
            {
                destinationConnectionString = ((ADODB.Connection)_con).ConnectionString;
            }
        }
    }
}