using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    class Small_Truck: Vehicle
    {
        public override void calculateValue(double originalCost, int time)
        {
            //Formula: V = C(1-rate)^t  ... C is original cost, V is the value or price 
            double rate = 1 - 0.20; 

            time = 2015 - time;

            if (dCurrentOdometer <= 25000)
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

                dPuchasePrice = (originalCost * rate) + (0.18 * dCurrentOdometer);
                Math.Round(dPuchasePrice, 2);
            }
        }

        public override string print()
        {
            string retStringValue = "Manufacturer " + sManufacturer + "\nModel: " + sModel + "\nYear Model: " + nModelYear + "\nPurchase Price: $" + dPuchasePrice +
"\nPurchase Date: " + nPurchaseDate + "\nOdometer: " + dCurrentOdometer + "\nSize of Engine " + dSizeOfEngine + "\nCargo Capacity: " + dCargoCapacity + "lbs\nTowing Capacity: " + dTowingCapacity + "lbs\n********************\n";

            return retStringValue; ; 
        }

        /******************** Data Members ********************/
        private double dCargoCapacity;
        private double dTowingCapacity;

        /******************** Constructor ********************/
        public Small_Truck(string sManufacturer, string sModel, int nModelYear, double dInitalPurchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, double dCargoCapacity, double dTowingCapacity)
            : base(sManufacturer, sModel, nModelYear, dInitalPurchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine)
        {
            this.dCargoCapacity = dCargoCapacity;
            this.dTowingCapacity = dTowingCapacity; 
        }

        /******************** Properties ********************/
        public double CargoCapacity
        {
            get
            {
                return dCargoCapacity;
            }
        }

        public double TowingCapacity
        {
            get
            {
                return dTowingCapacity; 
            }
        }
        /****************************************************/
    }
}
