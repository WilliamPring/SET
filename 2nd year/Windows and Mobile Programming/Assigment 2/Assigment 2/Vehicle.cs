using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    abstract class Vehicle
    {
        /***********  Data Members  **********/
        protected string sManufacturer;
        protected string sModel;
        protected string nPurchaseDate;
        protected int nModelYear;
        protected double dCurrentOdometer;
        protected double dSizeOfEngine;
        protected double dPuchasePrice;

        /***********  Constructor  **********/
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

        abstract public string print();
        abstract public void calculateValue(double originalCost, int time); 


        /*****************  End of Class *****************/
    }
}
