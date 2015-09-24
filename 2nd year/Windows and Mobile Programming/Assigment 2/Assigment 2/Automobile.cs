using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    class Automobile : Vehicle
    {
        /******************** Data Members ********************/
        private int nNumOfDoor;
        private string aFuel;

        /******************** Constructor ********************/
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
                    rate *= rate;
                }

                dPuchasePrice = originalCost * rate;
                Math.Round(dPuchasePrice, 2);
            }
            else
            {
                for (int i = 0; i < time - 1; i++)
                {
                    rate *= rate;
                }

                dPuchasePrice = (originalCost * rate) + (0.10 * dCurrentOdometer);
                Math.Round(dPuchasePrice, 2);
            }
        }


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
