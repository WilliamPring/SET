using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    class Motorcycle: Vehicle
    {
        /******************** Data Members ********************/
        private string sTypeOfMotocycle;


        /******************** Constructor ********************/
        public Motorcycle(string type, string sManufacturer, string sModel, int nModelYear, double dPuchasePrice, string nPurchaseDate, double dCurrentOdometer, double dSizeOfEngine, string sTypeOfMotocycle)
            : base(sManufacturer, sModel, nModelYear, dPuchasePrice, nPurchaseDate, dCurrentOdometer, dSizeOfEngine)
        {
            this.sTypeOfMotocycle = sTypeOfMotocycle;
        }


        /******************** Overrided Methods ********************/
        public override void calculateValue(double originalCost, int time)
        {
            //Formula: V = C(1-rate)^t  ... C is original cost, V is the value or price 
            double rate = 1 - 0.15;

            time = 2015 - time;

            for (int i = 0; i < time - 1; i++)
            {
                rate *= rate;
            }
            
            dPuchasePrice = originalCost * rate;
            Math.Round(dPuchasePrice, 2);
        }      

        public override string print()
        {
            // throw new NotImplementedException();
            string retStringValue = "Manufacturer " + sManufacturer + "\nModel: " + sModel + "\nYear Model: " + nModelYear + "\nPurchase Price: $" + dPuchasePrice +
"\nPurchase Date: " + nPurchaseDate + "\nOdometer: " + dCurrentOdometer + "\nSize of Engine " + dSizeOfEngine + "\nType: " + sTypeOfMotocycle + "\n********************\n";

            return retStringValue; 
        }

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
