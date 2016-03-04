/*
* Filename:	Program.cs
* Project:	ASQL: Assignment 3 - Programming Abstractions
* By:		William Pring
* Date:		March 3, 2016
* Description:	This program will transfer table information to another table
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_SQL_Assignment_03
{
    /// <summary>
    /// Start of the program
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ASQL());
        }
    }
}
