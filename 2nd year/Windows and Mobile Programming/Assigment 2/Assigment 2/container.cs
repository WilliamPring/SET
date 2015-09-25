using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    class container
    {

        private int nMotocycle;
        private int nSmallTruck;
        private int nAutoMobile;
        private List <Vehicle> newVehicles = new List<Vehicle> ();

        public List<Vehicle> listingAllContent()
        {
            List<Vehicle> temp = new List<Vehicle>();

            foreach( Vehicle v in newVehicles)
            {
                temp.Add(v);
                v.calculateValue(v.PurchasePrice, v.ModelYear); 
                Console.WriteLine(v.print());
            }

            Console.WriteLine("Press any key to continue back to the menu");
            Console.ReadLine();
            Console.Clear();
            return temp;
        }


        public void SaveToDatabase(container AddNewVehicleToDataBase)
        {
            string dataBase = "database.txt";
            FileStream File = null;
            try
            {
                File = new FileStream(dataBase, FileMode.Create);
                using (StreamWriter stWriteToFile = new StreamWriter(File, Encoding.Default))
                {
                        foreach (Vehicle v in newVehicles)
                        {
                            if (v.GetType().ToString() == "Assigment_2.Small_Truck")
                            {
                                Small_Truck ST = (Small_Truck)v;
                                stWriteToFile.Write("ST|");
                                stWriteToFile.Write(ST.Manufacturer + "|");
                                stWriteToFile.Write(ST.Model + "|");
                                stWriteToFile.Write(ST.ModelYear + "|");
                                stWriteToFile.Write(ST.PurchasePrice + "|");
                                stWriteToFile.Write(ST.PurchaseDate + "|");
                                stWriteToFile.Write(ST.CurrentOdometer + "|");
                                stWriteToFile.Write(ST.SizeOfEngine + "|");
                                stWriteToFile.Write(ST.CargoCapacity + "|");
                                stWriteToFile.WriteLine(ST.TowingCapacity + "|");
                            }
                            else if(v.GetType().ToString() == "Assigment_2.Motorcycle")
                            {
                            Motorcycle Moto = (Motorcycle)v;
                                stWriteToFile.Write("M|");
                                stWriteToFile.Write(Moto.Manufacturer + "|");
                                stWriteToFile.Write(Moto.Model + "|");
                                stWriteToFile.Write(Moto.ModelYear + "|");
                                stWriteToFile.Write(Moto.PurchasePrice + "|");
                                stWriteToFile.Write(Moto.PurchaseDate + "|");
                                stWriteToFile.Write(Moto.CurrentOdometer + "|");
                                stWriteToFile.Write(Moto.SizeOfEngine + "|");
                                stWriteToFile.WriteLine(Moto.TypeOfMotocycle + "|");
                            }
                            else if (v.GetType().ToString() == "Assigment_2.Automobile")
                             {
                                Automobile auto = (Automobile)v;
                                stWriteToFile.Write("A|");
                                stWriteToFile.Write(auto.Manufacturer + "|");
                                stWriteToFile.Write(auto.Model + "|");
                                stWriteToFile.Write(auto.ModelYear + "|");
                                stWriteToFile.Write(auto.PurchasePrice + "|");
                                stWriteToFile.Write(auto.PurchaseDate + "|");
                                stWriteToFile.Write(auto.CurrentOdometer + "|");
                                stWriteToFile.Write(auto.SizeOfEngine + "|");
                                stWriteToFile.Write(auto.NumberOfDoors + "|");
                                stWriteToFile.WriteLine(auto.TypeOfFuel + "|");
                            }
                        }

                    stWriteToFile.Close();
                }


            }
            finally
            {

            }
        }
        public void readDataBase()
        {
            int counter = 0;
            string readLine = "";
            try
            {


                if (File.Exists("database.txt") == false)
                {
                    File.Create("database.txt").Dispose();
                }
                System.IO.StreamReader read = new StreamReader("database.txt");
                while (true)
                {
                    readLine = read.ReadLine();
                    if (readLine == null)
                    {
                        break;
                    }
                    if (readLine[0] == 'M')
                    {
                        if (nMotocycle == 10)
                        {
                            continue;
                        }
                        counter = 0;
                        string manufacturer = "";
                        string Model = "";
                        int ModelYear = 0;
                        double PurchasePrice = 0.0;
                        string PurchaseDate = "";
                        double CurrentOdometer = 0.0;
                        double SizeOfEngine = 0.0;
                        string TypeOfMotocycle = "";
                        string[] information = readLine.Split('|');
                        foreach (string readed in information)
                        {
                            if (counter == 1)
                            {
                                manufacturer = readed;
                            }
                            else if (counter == 2)
                            {
                                Model = readed;
                            }
                            else if (counter == 3)
                            {
                                ModelYear = Convert.ToInt32(readed);
                            }
                            else if (counter == 4)
                            {
                                PurchasePrice = Convert.ToDouble(readed);
                            }
                            else if (counter == 5)
                            {
                                PurchaseDate = readed;
                            }
                            else if (counter == 6)
                            {
                                CurrentOdometer = Convert.ToDouble(readed);
                            }
                            else if (counter == 7)
                            {
                                SizeOfEngine = Convert.ToDouble(readed);
                            }
                            else if (counter == 8)
                            {
                                TypeOfMotocycle = readed;
                            }
                            else { }

                            counter++;
                        }

                        Motorcycle createNewMoto = new Motorcycle("M", manufacturer, Model, ModelYear, PurchasePrice, PurchaseDate, CurrentOdometer,
                            SizeOfEngine, TypeOfMotocycle);
                        
                        newVehicles.Add(createNewMoto);
                        nMotocycle++;
       

                    }
                    else if ((readLine[0] == 'S') && (readLine[1] == 'T'))
                    {
                        if (nSmallTruck == 10)
                        {
                            continue;
                        }
                        counter = 0;
                        string manufacturer = "";
                        string Model = "";
                        int ModelYear = 0;
                        double PurchasePrice = 0.0;
                        string PurchaseDate = "";
                        double CurrentOdometer = 0.0;
                        double cargoSize = 0;
                        double towingCapcity = 0;
                        double SizeOfEngine = 0.0;
                        string[] information = readLine.Split('|');
                        foreach (string readed in information)
                        {
                            if (counter == 1)
                            {
                                manufacturer = readed;
                            }
                            else if (counter == 2)
                            {
                                Model = readed;
                            }
                            else if (counter == 3)
                            {
                                ModelYear = Convert.ToInt32(readed);
                            }
                            else if (counter == 4)
                            {
                                PurchasePrice = Convert.ToDouble(readed);
                            }
                            else if (counter == 5)
                            {
                                PurchaseDate = readed;
                            }
                            else if (counter == 6)
                            {
                                CurrentOdometer = Convert.ToDouble(readed);
                            }
                            else if (counter == 7)
                            {
                                SizeOfEngine = Convert.ToDouble(readed);
                            }
                            else if (counter == 8)
                            {
                                cargoSize = Convert.ToDouble(readed);
                            }
                            else if(counter ==9)
                            {
                                towingCapcity = Convert.ToDouble(readed);
                            }
                            else { }

                            counter++;
                        }
                        Small_Truck createNewMoto = new Small_Truck(manufacturer, Model, ModelYear, PurchasePrice, PurchaseDate, CurrentOdometer,
                            SizeOfEngine, cargoSize, towingCapcity);
                        newVehicles.Add(createNewMoto);
                        nSmallTruck++;
                    }
                    else if (readLine[0] == 'A')
                    {
                        if (nAutoMobile == 10)
                        {
                            continue;
                        }
                        counter = 0;
                        string manufacturer = "";
                        string Model = "";
                        int ModelYear = 0;
                        double PurchasePrice = 0.0;
                        string PurchaseDate = "";
                        double CurrentOdometer = 0.0;
                        int doorNum = 0;
                        string typeOfFuel = "";
                        double SizeOfEngine = 0.0;
                        string[] information = readLine.Split('|');
                        foreach (string readed in information)
                        {
                            if (counter == 1)
                            {
                                manufacturer = readed;
                            }
                            else if (counter == 2)
                            {
                                Model = readed;
                            }
                            else if (counter == 3)
                            {
                                ModelYear = Convert.ToInt32(readed);
                            }
                            else if (counter == 4)
                            {
                                PurchasePrice = Convert.ToDouble(readed);
                            }
                            else if (counter == 5)
                            {
                                PurchaseDate = readed;
                            }
                            else if (counter == 6)
                            {
                                CurrentOdometer = Convert.ToDouble(readed);
                            }
                            else if (counter == 7)
                            {
                                SizeOfEngine = Convert.ToDouble(readed);
                            }
                            else if (counter == 8)
                            {
                                doorNum = Convert.ToInt32(readed);
                            }
                            else if (counter == 9)
                            {
                                typeOfFuel = readed;
                            }
                            else { }

                            counter++;
                        }
                        Automobile temp = new Automobile(manufacturer, Model, ModelYear, PurchasePrice, PurchaseDate, CurrentOdometer,
                            SizeOfEngine, doorNum, typeOfFuel);
                        newVehicles.Add(temp);
                        nAutoMobile++;
                    }
                    else
                    {

                    }
                }


                read.Close();
                // FileStream fs = new FileStream("database.txt", FileMode.OpenOrCreate);


            }
            catch (Exception efs)
            {
                Console.WriteLine(efs.Message);
            }
        }


        public List<Vehicle> TypeTracking(string ModelToTrack)
        {
            List<Vehicle> temp = new List<Vehicle>();
            if (ModelToTrack == "1")
            {
                foreach (Vehicle v in newVehicles)
                {
                    if (v.GetType().ToString() == "Assigment_2.Motorcycle")
                    {
                        temp.Add(v);
                    }
                }
            }
            else if (ModelToTrack == "2")
            {
                foreach (Vehicle v in newVehicles)
                {
                    if (v.GetType().ToString() == "Assigment_2.Automobile")
                    {
                        temp.Add(v);
                    }
                }
            }
            else if (ModelToTrack == "3")
            {
                foreach (Vehicle v in newVehicles)
                {
                    if (v.GetType().ToString() == "Assigment_2.Small_Truck")
                    {
                        temp.Add(v);
                    }
                }
            }



            return temp;

        }
        public List<Vehicle> ModelTracking(string ModelToTrack)
        {
            List<Vehicle> temp = new List<Vehicle>();
            foreach (Vehicle v in newVehicles)
            {
                if(v.Model == ModelToTrack)
                {
                    temp.Add(v);
                }
            }

            return temp;
        }

        public container()
        {
            nMotocycle = 0;
            nSmallTruck = 0;
            nAutoMobile = 0;
        }
        public int GetSmallTruckCount
        {
            get
            {
                return nSmallTruck;
            }
        }

        public int AmountOfVehicle
        {
            get
            {
                return newVehicles.Count;
            }
        }
        public int GetAutoMobileCount
        {
            get
            {
                return nAutoMobile;
            }
        }
        public int GetMotocycleCount
        {
            get
            {
                return nMotocycle;
            }
        }

        public void addMotocycleToContainer(string type, string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, string sTypeOfMotocycle)
        {
            Motorcycle tempMotocycle = new Motorcycle(type, sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, sTypeOfMotocycle);
            newVehicles.Add(tempMotocycle);
            nMotocycle++;
        }
        public void addSmallTruckToContainer(string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, double dCargoCapacity, double dTowingCapacity)
        {
            Small_Truck tempSmallTruck = new Small_Truck(sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, dCargoCapacity, dTowingCapacity);
            newVehicles.Add(tempSmallTruck);
            nSmallTruck++;
        }
        public void addAutomobileToContainer(string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, int nNumOfDoor, string aFuel)
        {
            Automobile tempAutomobile = new Automobile(sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, nNumOfDoor, aFuel);
            newVehicles.Add(tempAutomobile);
            nAutoMobile++;
        }
    }
}
