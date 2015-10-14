/*
File name: Automobile.cs
Project: WinProg Assignment 2
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: The file contains the child class Automobile which inherits from the parent class Vehicle. 
The purpose is to add a bit more functionality and more data members specific to a typical automobile. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    /*
        Name: Automobile 
        Purpose: Defines and declares the specifics and functionality of what an derived Automobile class would contain 

        Data Members :  nNumOfDoor - An int type indicating the number of doors the Automobile contains 
                        aFuel - A string type describing the type of fuel the Automobile consumes 
                     
        Type: Protected Inheritance, the members are inherited from the parent class and are only relevant to the parent and child class
    */
    class Automobile : Vehicle
    {
        /******************** Data Members ********************/
        private int nNumOfDoor;
        private string aFuel;

        /******************** Constructor ********************/
        /*
            The overloaded constructor also inherits the constructor found in the parent class to immediately assign the data members correctly
        */
        public Automobile(string sManufacturer, string sModel, int nModelYear, double dInitalPurchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, int nNumOfDoor, string aFuel, int dPurchaseYear)
            : base(sManufacturer, sModel, nModelYear, dInitalPurchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine, dPurchaseYear)
        {
            this.nNumOfDoor = nNumOfDoor;
            this.aFuel = aFuel;
        }

        public Automobile(string sManufacturer, string sModel, int nModelYear, double dInitalPurchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, int nNumOfDoor, string aFuel)
           : base(sManufacturer, sModel, nModelYear, dInitalPurchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine)
        {
            this.nNumOfDoor = nNumOfDoor;
            this.aFuel = aFuel;
        }

        /******************** Properties ********************/
        public int NumberOfDoors
        {
            get
            {
                return nNumOfDoor;
            }
        }

        public string TypeOfFuel
        {
            get
            {
                return aFuel;
            }
        }

        /******************** Overrided Methods ********************/
        /*
            Method: calculateValue()
            Keyword Type: Override - It defines the original abstract function declared in the parent class (Vehicle)
            Description: The method calculates the current price of the Automobile from using the variables of depreciation value and the rate.
            Parameter(s): double originalCost - The initial cost of the Automobile when it was first released 
                          int time - The year of the model 
            Return: Nothing
        */
        public override void calculateValue(double originalCost, int time)
        {
            //Formula: V = C(1-rate)^t  ... C is original cost, V is the value or price 
            double rate = 1 - 0.20;
            time = 2015 - time;

            if (dCurrentOdometer <= 20000)
            {
                //Formula: V = C(1-rate)^t  ... C is original cost, V is the value or price 

                for (int i = 0; i < time - 1; i++)
                {
                    rate = rate * 0.8;
                }

                dPuchasePrice = originalCost * rate;
                dPuchasePrice = Math.Round(dPuchasePrice, 2);
            }
            else
            {
                for (int i = 0; i < time - 1; i++)
                {
                    rate = rate * 0.8;
                }

                dPuchasePrice = (originalCost * rate) + (0.10 * dCurrentOdometer);
                dPuchasePrice = Math.Round(dPuchasePrice, 2);
            }
        }


        /*
            Method: print()
            Keyword Type: Override - It defines the original abstract function declared in the parent class (Vehicle)
            Description: To store what to write to the console on a string so that it can be passed from the business logic layer to the UI layer
            Parameter(s): None
            Return: Returns the string containing what must be ouputted on the console screen, pretty much the values of all the data members of a specific object
        */
        public override string print()
        {
            //throw new NotImplementedException
            string retStringValue = "Manufacturer " + sManufacturer + "\nModel: " + sModel + "\nYear Model: " + nModelYear + "\nPurchase Price: $" + dPuchasePrice +
"\nPurchase Date: " + nPurchaseDate + "\nOdometer: " + dCurrentOdometer + "\nSize of Engine " + dSizeOfEngine + "\nNumber of doors: " + nNumOfDoor + "\nFuel type: " + aFuel + "\n********************\n";

            return retStringValue;
        }

        /****************************************************/
    }
}
