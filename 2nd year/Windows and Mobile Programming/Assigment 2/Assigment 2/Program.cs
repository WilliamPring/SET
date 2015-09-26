/*
File name: Program.cs
Project: WinProg Assignment 2
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: The start of the menu driven client interacting with a database (text file). You can store Automobiles, Small Trucks or Motorcycle
into a record file, you view members from the file, modify certain characteristics and altogether delete them. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{

    class Program
    {
        static void Main(string[] args)
        {
            container newVehiclesInfo = new container();
            View startProgram = new View();

            //Begin UI for the user, insert the container class to store the vehicles 
            startProgram.getUserInput(newVehiclesInfo);

        }
    }
}
