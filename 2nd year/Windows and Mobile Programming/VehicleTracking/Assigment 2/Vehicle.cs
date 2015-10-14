/*
File name: Vehicle.cs
Project: WinProg Assignment 2
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: The file contains the parent class (vehicle) which branches off into 2 child classes. It provides the basic characteristics
describing the given qualities that must be assigned for any vehicle and it's sub-classes. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    /*
        Name: Vehicle
        Purpose: Defines and declares the specifics and functionality of what an abstract vehicle class would normally contain 

        Data Members :  sManufacturer - A string type that specifies the manufacturer of the product 
                        sModel - A string type specifiying the type of model of the product 
                        nPurchaseDate - A string type specifying the purchase in which the product was purchased 
                        nModelYear - An int type specifying the year model of the product 
                        dCurrentOdometer - A double type specifing the odometer reading of the product(vehicle)
                        dSizeOfEngine - A double type specifying the size of the product's engine
                        dPurchasePrice - A double type specifying the price of the product, calculated through depreciation and a rate
        Type: Abstract - So that this is the base class for other sub-classes, and so that an object cannot be instantiated from this class 
    */
    abstract class Vehicle
    {
        /***********  Data Members  **********/
        protected string sManufacturer;
        protected string sModel;
        protected string nPurchaseDate;
        protected int nModelYear;
        protected int yearPurchased;
        protected double dCurrentOdometer;
        protected double dSizeOfEngine;
        protected double dPuchasePrice;

        /***********  Constructor  **********/
        /*
            Default Constructor for initializing the data members at first instantiation
        */
        public Vehicle()
        {
            sManufacturer = "";
            sModel = "";
            nModelYear = 0;
            dPuchasePrice = 0;
            nPurchaseDate = "";
            dCurrentOdometer = 0.0;
            dSizeOfEngine = 0.0;
        }

        /*
            Overloaded constructor to initialize all members through the arguments inserted
        */
        public Vehicle(string sManufacturer, string sModel, int nModelYear, double dPuchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, int dPurchaseYear)
        {
            this.sManufacturer = sManufacturer;
            this.sModel = sModel;
            this.nModelYear = nModelYear;
            this.dPuchasePrice = dPuchasePrice;
            this.nPurchaseDate = nPurchaseDate;
            this.dCurrentOdometer = dCurrentOdometer;
            this.dSizeOfEngine = dSizeOfEngine;
            this.yearPurchased = dPurchaseYear;
        }

        //This constructor is minus the dPurchaseYear variable 

        public Vehicle(string sManufacturer, string sModel, int nModelYear, double dPuchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine)
        {
            this.sManufacturer = sManufacturer;
            this.sModel = sModel;
            this.nModelYear = nModelYear;
            this.dPuchasePrice = dPuchasePrice;
            this.nPurchaseDate = nPurchaseDate;
            this.dCurrentOdometer = dCurrentOdometer;
            this.dSizeOfEngine = dSizeOfEngine;
        }


        /***********  Properties  **********/
        public int YearPurchased
        {
            get
            {
                return yearPurchased;
            }
        }

        public string Model
        {
            get
            {
                return sModel;
            }
        }
        public double PurchasePrice
        {
            get
            {
                return dPuchasePrice;
            }
        }
        public double SizeOfEngine
        {
            get
            {
                return dSizeOfEngine;
            }
        }
        public double CurrentOdometer
        {
            get
            {
                return dCurrentOdometer;
            }

            set
            {
                if (value >= 0)
                {
                    dCurrentOdometer = value;
                }
            }
        }
        public string PurchaseDate
        {
            get
            {
                return nPurchaseDate;
            }
        }
        public int ModelYear
        {
            get
            {
                return nModelYear;
            }
        }
        public string Manufacturer
        {
            get
            {
                return sManufacturer;
            }
        }

        /*****************  Abstract Methods *****************/

        abstract public string print(); //Modified in the base classes, used to print out the data members specific to it's class
        abstract public void calculateValue(double originalCost, int time); //Modified in the base classes, used to calculat price specific to it's class rules


        /*****************  End of Class *****************/
    }
}
