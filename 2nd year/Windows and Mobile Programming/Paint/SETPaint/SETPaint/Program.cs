/*
File name: Program.cs
Project: Assigment 3 SETPaint
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: You will be able to draw lines, Rectangle and Eclipse change the colour of the line and have
the option to fill in the shape.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    /*
       Name: Program 
       Purpose: Start of the program
       Data Members :  None
       Type: None
   */
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
            Application.Run(new FrmSetPaint());
        }
    }
}
