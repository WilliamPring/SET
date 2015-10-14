/*
File name: container.cs
Project: WinProg Assignment 2
By: William Pring and Naween Mehanmal
Date: September 25, 2015
Description: The maintains the database layer for the user, anything involving the record file is found on here,
and values are retrieved and sent to the record file. 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    /*
       Name: container 
       Purpose: Defines and declares the specifics and functionality of what database class should contain, like insertion, deletion and listing of records

       Data Members :  nMotocycle - The number of Motorcycles in the record file
                       nSmallTruck - The number of Small_Truck in the record file
                       nAutoMobile - The number of Automobiles in the record file 
    */
    class container
    {
        /***********  Data Members  **********/
        private int nMotocycle;
        private int nSmallTruck;
        private int nAutoMobile;
        private List<Vehicle> newVehicles = new List<Vehicle>();

        /*
          Method: listingAllContent()
          Description: The purpose of this method is to take all of the objects and store it in a list template to insert into the database after
          Parameter(s): None

          Return: List<Vehicle> - Contains all of the records and is returned for printing in the UI layer  
        */
        public List<Vehicle> listingAllContent()
        {
            List<Vehicle> temp = new List<Vehicle>();

            foreach (Vehicle v in newVehicles)
            {
                temp.Add(v);
            }
            return temp;
        }


        /*
         Method: modifyOdometerContent()
         Description: The purpose of this method is to take all of the objects and store it in a list template to insert into the database after
         Parameter(s): int inputNumber - The specific record member to change 
                       int newOdometerValue - The new odometer value for the object 

         Return: string indicating the odometer value of the object
        */
        public string modifyOdometerCotent(int inputNumber, int newOdometerValue)
        {
            string retMessage = "";
            int sizeOfList = newVehicles.Count;
            int matchID = 1;

            if (inputNumber <= sizeOfList)
            {
                foreach (Vehicle v in newVehicles)
                {
                    if (matchID == inputNumber)
                    {
                        v.CurrentOdometer = newOdometerValue;
                        retMessage = "Modification was successful";
                        break;
                    }
                    matchID++;
                    retMessage = "Modification was unsuccessful!";
                }
            }
            else
            {
                retMessage = "Not enough members in the record! Record contains " + Convert.ToInt32(sizeOfList) + " members";
            }

            return retMessage;
        }

        /**********************************************************************************/

        /*
        Method: deleteContent()
        Description: The purpose of this method is to delete a specific record member from the file 
        Parameter(s): int inputNumber - The specific record member to delete 

        Return: string indicating if the member was successfully deleted or not
       */
        public string deleteContent(int inputNumber)
        {
            int matchID = 1;
            int sizeOfList = newVehicles.Count;
            string retMessage = "";

            if (inputNumber <= sizeOfList)
            {
                foreach (Vehicle v in newVehicles)
                {
                    if (matchID == inputNumber)
                    {
                        retMessage = "Deletion was successful";
                        newVehicles.Remove(v);
                        if (v.GetType().ToString() == "Assigment_2.Small_Truck")
                        {
                            nSmallTruck--;
                        }
                        else if (v.GetType().ToString() == "Assigment_2.Motorcycle")
                        {
                            nMotocycle--;
                        }
                        else if (v.GetType().ToString() == "Assigment_2.Automobile")
                        {
                            nAutoMobile--;
                        }
                        break;
                    }
                    matchID++;
                    retMessage = "Deletion was unsuccessful!";
                }
            }
            else
            {
                retMessage = "Not enough members in the record! Record contains " + Convert.ToInt32(sizeOfList) + " members";
            }

            //Console.WriteLine("SMALL TRUCK: " + nSmallTruck + "\nMOTORCYCLE: " + nMotocycle + "\nAUTOMOBILE: " + nAutoMobile);
            return retMessage;
        }


        /*
            Method: saveToDatabase()
            Description: The purpose of this method is to take all of the members from a list and write it to a text file to save permanently 
            Parameter(s): container AddNewVehicleToDatabase - Save the member in the List template and write it to a text file 

            Return: Nothing 
        */
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
                        else if (v.GetType().ToString() == "Assigment_2.Motorcycle")
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



        /*
        Method: readDataBase()
        Description: The purpose of this method is to read from a textfile and store the record member strings into a List template 
        Parameter(s): None

        Return: Nothing
       */
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
                            if (nMotocycle > 10)
                            {
                                continue;
                            }
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
                            if (nSmallTruck > 10)
                            {
                                continue;
                            }
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
                            else if (counter == 9)
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
                            if (nAutoMobile > 10)
                            {
                                continue;
                            }
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


        /*
        Method: TypeTracking()
        Description: The purpose of this method is to take all of the objects and store it in a list template to insert into the database after
        Parameter(s): string ModelToTrack - Find a specific member in the record database, based on the year of the model 

        Return: List<Vehicle>, returns the entire template so that it can be loaded onto the UI layer for outputting information 
       */
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



        /*
        Method: deleteContent()
        Description: The purpose of this method is to track a specific record member based on the information the user inputted 
        Parameter(s): string ModelToTrack - Track a specific record in the database 

        Return: List<Vehicle>, returns the entire template so that it can be loaded onto the UI layer for outputting information 
       */
        public List<Vehicle> ModelTracking(string ModelToTrack)
        {
            List<Vehicle> temp = new List<Vehicle>();
            foreach (Vehicle v in newVehicles)
            {
                if (v.ModelYear.ToString() == ModelToTrack)
                {
                    temp.Add(v);
                }
            }

            return temp;
        }

        /***********  Constructor  **********/
        /*
            Initialize database data members
        */
        public container()
        {
            nMotocycle = 0;
            nSmallTruck = 0;
            nAutoMobile = 0;
        }

        /***********  Properties  **********/

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

        /***********  Create New Object  **********/

        public void addMotocycleToContainer(string type, string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, string sTypeOfMotocycle, int theYearOfPurchase)
        {
            Motorcycle tempMotocycle = new Motorcycle(type, sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, sTypeOfMotocycle, theYearOfPurchase);
            tempMotocycle.calculateValue(tempMotocycle.PurchasePrice, tempMotocycle.YearPurchased);
            newVehicles.Add(tempMotocycle);
            nMotocycle++;
        }
        public void addSmallTruckToContainer(string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, double dCargoCapacity, double dTowingCapacity, int theYearOfPurchase)
        {
            Small_Truck tempSmallTruck = new Small_Truck(sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, dCargoCapacity, dTowingCapacity, theYearOfPurchase);
            tempSmallTruck.calculateValue(tempSmallTruck.PurchasePrice, tempSmallTruck.YearPurchased);
            newVehicles.Add(tempSmallTruck);
            nSmallTruck++;
        }
        public void addAutomobileToContainer(string sManufacturerInfo, string sModelInfo, int nModelYearInfo, double dInitalPurchasePrice, string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, int nNumOfDoor, string aFuel, int theYearOfPurchase)
        {
            Automobile tempAutomobile = new Automobile(sManufacturerInfo, sModelInfo, nModelYearInfo, dInitalPurchasePrice, nPurchaseDateInfo, dCurrentOdometerInfo, dSizeOfEngineInfo, nNumOfDoor, aFuel, theYearOfPurchase);
            tempAutomobile.calculateValue(tempAutomobile.PurchasePrice, tempAutomobile.YearPurchased);
            newVehicles.Add(tempAutomobile);
            nAutoMobile++;
        }
    }
}
