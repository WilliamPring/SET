/*
File name: Motorcycle.cs
Project: WinProg Assignment 2
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: The file contains the child class Motorcycle which inherits from the parent class Vehicle. 
The purpose is to add a bit more functionality and more data members specific to a typical motorcycle. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    /*
        Name: Motorcycle 
        Purpose: Defines and declares the specifics and functionality of what an derived Motorcycle class would contain 

        Data Members :  sTypeOfMotorcycle - A string type indicating the type of motorcycle being instantiated  
                     
        Type: Protected Inheritance, the members are inherited from the parent class and are only relevant to the parent and child class
    */
    class Motorcycle : Vehicle
    {
        /******************** Data Members ********************/
        private string sTypeOfMotocycle;


        /******************** Constructor ********************/
        /*
           The overloaded constructor also inherits the constructor found in the parent class to immediately assign the data members correctly
        */
        public Motorcycle(string type, string sManufacturer, string sModel, int nModelYear, double dPuchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, string sTypeOfMotocycle, int dPurchaseYear)
            : base(sManufacturer, sModel, nModelYear, dPuchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine, dPurchaseYear)
        {
            this.sTypeOfMotocycle = sTypeOfMotocycle;
        }

        public Motorcycle(string type, string sManufacturer, string sModel, int nModelYear, double dPuchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, string sTypeOfMotocycle)
          : base(sManufacturer, sModel, nModelYear, dPuchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine)
        {
            this.sTypeOfMotocycle = sTypeOfMotocycle;
        }


        /******************** Overrided Methods ********************/
        /*
            Name: calculateValue()
            Keyword Type: Override - It defines the original abstract function declared in the parent class (Vehicle)
            Description: The method calculates the current price of the Automobile from using the variables of depreciation value and the rate.
            Parameter(s): double originalCost - The initial cost of the Automobile when it was first released 
                          int time - The year of the model 
            Return: Nothing
        */
        public override void calculateValue(double originalCost, int time)
        {
            //Formula: V = C(1-rate)^t  ... C is original cost, V is the value or price 
            double rate = 1 - 0.15;
            time = 2015 - time;

            for (int i = 0; i < time - 1; i++)
            {
                rate = rate * 0.85;
            }
            dPuchasePrice = originalCost * rate;
            dPuchasePrice = Math.Round(dPuchasePrice, 2);
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
            string retStringValue = "Manufacturer " + sManufacturer + "\nModel: " + sModel + "\nYear Model: " + nModelYear + "\nPurchase Price: $" + dPuchasePrice +
"\nPurchase Date: " + nPurchaseDate + "\nOdometer: " + dCurrentOdometer + "\nSize of Engine " + dSizeOfEngine + "\nType: " + sTypeOfMotocycle + "\n********************\n";

            return retStringValue;
        }


        /*****************  Properties *****************/
        public string TypeOfMotocycle
        {
            get
            {
                return sTypeOfMotocycle;
            }
        }

        /*****************  End of Class *****************/
    }
}
