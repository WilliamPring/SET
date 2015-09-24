using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assigment_2
{
    class View
    {
        public void deleteMotocycle(string ManufacturerInfo, string ModelInfo, int ModelYearInfo, double dInitalPurchasePrice,
                                  string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, string sTypeOfMotocycle, container trackNewVehicle)
        {

        }


        /******************** Create Automobile Method  ********************/

        public void createAutomobile(string ManufacturerInfo, string ModelInfo, int ModelYearInfo, double dInitalPurchasePrice,
                                string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, int nNumOfDoor, string aFuel, container trackNewVehicle)
        {
            string type = "A";
            bool status = true;
            bool infoStatus = true;
            bool fuelStatus = false; 
            int tempNum = 0;
            string sEnterInfo = "";
            string[] fuelTypes = { "diesel", "Diesel", "electric", "Electric", "gas", "Gas" };
            double minPriceCheck = 0; 

            Console.Clear();

            //Obtain the intial purchase price of the automobile (double)
            while (status)
            {
                System.Console.WriteLine("What is the initial price?");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                    minPriceCheck = Convert.ToDouble(sEnterInfo);//Convert the string to an int and assign to variable

                    if (minPriceCheck < 500)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid price (Min is $500). Try again!");
                        continue;
                    }

                    dInitalPurchasePrice = minPriceCheck;
                    break;
                }
            }

            vehicleRelatedQuestions(ref ManufacturerInfo,
                                    ref ModelInfo,
                                    ref ModelYearInfo,
                                    ref dInitalPurchasePrice,
                                    ref nPurchaseDateInfo,
                                    ref dCurrentOdometerInfo,
                                    ref dSizeOfEngineInfo);

            //Ask questons unrelated to a basic vehicle

            //Ask for the number of doors on the automobile
            while (status)
            {
                System.Console.WriteLine("How many doors does the automobile have? (2 or 4)");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckNum(ref sEnterInfo);

                if (infoStatus == true)
                {
                    tempNum = Convert.ToInt32(sEnterInfo);
                    if (tempNum == 2 || tempNum == 4)
                    {
                        nNumOfDoor = tempNum;
                        break;
                    }
                    Console.Clear(); 
                    Console.WriteLine("Invalid input! You can only choose between 2 and 4 door automobiles.\nPlease try again.");        
                }
            }

            //Ask for the type of fuel the automobile uses
            while (status)
            {
                System.Console.WriteLine("What type of fuel does your automobile consume? (\"Diesel\", \"Electric\", \"Gas\")");
                sEnterInfo = Console.ReadLine();

                foreach(string fuel in fuelTypes)
                {
                    if(sEnterInfo == fuel)
                    {
                        status = false;
                        fuelStatus = true; 
                        aFuel = sEnterInfo;
                        break;
                    }
                }  
                
                if(fuelStatus == false)
                {
                    Console.Clear(); 
                    Console.WriteLine("Incorrect fuel type! Try again please.");
                    status = true;
                }           
            }

            //Add Automobile to the container
            trackNewVehicle.addAutomobileToContainer(ManufacturerInfo,
                                                    ModelInfo,
                                                    ModelYearInfo,
                                                    dInitalPurchasePrice,
                                                    nPurchaseDateInfo,
                                                    dCurrentOdometerInfo,
                                                    dSizeOfEngineInfo,
                                                    nNumOfDoor,
                                                    aFuel);
        }


        /******************** End of Automobile Method  ********************/


        /******************** Create Small Truck Method  ********************/
        public void createSmallTruck(string ManufacturerInfo, string ModelInfo, int ModelYearInfo, double dInitalPurchasePrice,
                                string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, double cargoCapacity, double towingCapacity, container trackNewVehicle)
        {
            string type = "ST";

            bool status = true;
            bool infoStatus = true;
            string sEnterInfo = "";
            double minPriceCheck = 0; 

            Console.Clear();
            vehicleRelatedQuestions(ref ManufacturerInfo,
                                    ref ModelInfo,
                                    ref ModelYearInfo,
                                    ref dInitalPurchasePrice,
                                    ref nPurchaseDateInfo,
                                    ref dCurrentOdometerInfo,
                                    ref dSizeOfEngineInfo);

            //Ask questons unrelated to a basic vehicle

            //Obtain the intial purchase price of the small truck (double)
            while (status)
            {
                System.Console.WriteLine("What is the initial price?");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                    minPriceCheck = Convert.ToDouble(sEnterInfo);//Convert the string to an int and assign to variable

                    if (minPriceCheck < 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid price (Min is $0). Try again!");
                        continue;
                    }

                    dInitalPurchasePrice = minPriceCheck;
                    break;
                }
            }

            //Obtain the cargo capacity 
            while (status)
            {
                System.Console.WriteLine("What is the cargo capacity (lbs)? (Numeric Value)");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo); 

                if (infoStatus == true)
                {
                    cargoCapacity = Convert.ToDouble(sEnterInfo);
                    break;
                }
            }

            //Obtain the towing capacity 
            while (status)
            {
                System.Console.WriteLine("What is the towing capacity (lbs)? (Numeric Value)");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                    towingCapacity = Convert.ToDouble(sEnterInfo);
                    break;
                }
            }

            //Add the new motorcycle created into the container
            trackNewVehicle.addSmallTruckToContainer(ManufacturerInfo,
                                                     ModelInfo,
                                                     ModelYearInfo,
                                                     dInitalPurchasePrice,
                                                     nPurchaseDateInfo, 
                                                     dCurrentOdometerInfo,
                                                     dSizeOfEngineInfo,
                                                     cargoCapacity,
                                                     towingCapacity);
        }
        /******************** End of Small Truck Method  ********************/



        /******************** Create Motorcycle Method  ********************/
        public void createMotorcycle(string ManufacturerInfo, string ModelInfo, int ModelYearInfo, double dInitalPurchasePrice,
                                string nPurchaseDateInfo, double dCurrentOdometerInfo, double dSizeOfEngineInfo, string sTypeOfMotocycle, container trackNewVehicle)
        {
            string type = "M";
            bool status = true;
            bool infoStatus = true;
            double minPriceCheck = 0;  
            string sEnterInfo = "";

            Console.Clear(); 

            //Ask the question unrelated to a regular vehicle
            System.Console.WriteLine("What type of motorcycle is it?");
            sTypeOfMotocycle = Console.ReadLine();

            //Obtain the intial purchase price of the motorcycle (double)
            while (status)
            {
                System.Console.WriteLine("What is the initial price?");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                  minPriceCheck = Convert.ToDouble(sEnterInfo);//Convert the string to an int and assign to variable
                  
                  if(minPriceCheck < 1500)
                  {
                        Console.Clear();
                        Console.WriteLine("Invalid price (Min is $1500). Try again!");
                        continue; 
                  }

                   dInitalPurchasePrice = minPriceCheck;
                   break;
                }               
            }

            vehicleRelatedQuestions(ref ManufacturerInfo,
                                    ref ModelInfo,
                                    ref ModelYearInfo,
                                    ref dInitalPurchasePrice,
                                    ref nPurchaseDateInfo,
                                    ref dCurrentOdometerInfo,
                                    ref dSizeOfEngineInfo);         

            //Add the new motorcycle created into the container
            trackNewVehicle.addMotocycleToContainer
                                    (type, ManufacturerInfo,
                                    ModelInfo,
                                    ModelYearInfo,
                                    dInitalPurchasePrice,
                                    nPurchaseDateInfo,
                                    dCurrentOdometerInfo,
                                    dSizeOfEngineInfo,
                                    sTypeOfMotocycle);
        }

        /******************** End of Motorcycle Method  ********************/


        /******************** Related Methods  ********************/

        public void vehicleRelatedQuestions(ref string ManufacturerInfo, ref string ModelInfo, ref int ModelYearInfo, ref double dInitalPurchasePrice, 
                                 ref string nPurchaseDateInfo, ref double dCurrentOdometerInfo, ref double dSizeOfEngineInfo)
        {
            bool status = true;
            bool infoStatus = true; 
            string sEnterInfo = "";

            int year = 0;
            int month = 0;
            int day = 0;
            int checkModelYear = 0; 

            System.Console.WriteLine("Who manufactored it?");
            ManufacturerInfo = Console.ReadLine();

            System.Console.WriteLine("What model is it?");
            ModelInfo = Console.ReadLine();

            //Obtain the year of the model
            while (status)
            {
                System.Console.WriteLine("What year is the model?");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckNum(ref sEnterInfo);

                if (infoStatus == true)
                {
                    checkModelYear = Convert.ToInt32(sEnterInfo); //Convert the string to an int and assign to variable

                    if(checkModelYear > 2015 || checkModelYear < 1990)
                    {
                        Console.Clear();
                        Console.WriteLine("Year unacceptable, try again!");
                        continue;
                    }
                    ModelYearInfo = checkModelYear; 
                    break;
                }
            }       

            //Enter in the intial purchase date (int)
            while (status)
            {
                Console.WriteLine("Enter the date purchased.");

                Console.WriteLine("What year was it purchased? (Numeric Value)");
                sEnterInfo = Console.ReadLine();
                infoStatus = errorCheckNum(ref sEnterInfo);
                if (infoStatus == true)
                {
                    year = Convert.ToInt32(sEnterInfo);

                    if(year > 2015 || year < 1990 || year < ModelYearInfo)
                    {
                        Console.Clear();
                        Console.WriteLine("Year unacceptable, try again!");
                        continue; 
                    }
                    nPurchaseDateInfo += sEnterInfo + "-";                   
                }
                else
                {
                    continue; 
                }


                Console.WriteLine("What month was it purchased? (Numeric Value)");
                sEnterInfo = Console.ReadLine();
                infoStatus = errorCheckNum(ref sEnterInfo);
                if (infoStatus == true)
                {
                    month = Convert.ToInt32(sEnterInfo);

                    if (month < 1 || month > 12)
                    {
                        Console.Clear();
                        Console.WriteLine("Month unacceptable, try again!");
                        continue;
                    }
                    nPurchaseDateInfo += sEnterInfo + "-";
                }
                else
                {
                    continue; 
                }

                Console.WriteLine("What day was it purchased? (Numeric Value)");
                sEnterInfo = Console.ReadLine();
                infoStatus = errorCheckNum(ref sEnterInfo);
                if (infoStatus == true)
                {
                    day = Convert.ToInt32(sEnterInfo);

                    if (day < 1 || day > 31)
                    {
                        Console.Clear();
                        Console.WriteLine("Day unacceptable, try again!");
                        continue;
                    }
                    nPurchaseDateInfo += sEnterInfo;
                    break;
                }
                else
                {
                    continue; 
                }  
            }

            //Obtain the odemeter info about the motorcycle (double)
            while (status)
            {
                System.Console.WriteLine("What is the odometer reading? (Numeric Value)");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                    dCurrentOdometerInfo = Convert.ToDouble(sEnterInfo);
                    break;
                }
            }

            //Obtain the size of the engine (double)
            while (status)
            {
                System.Console.WriteLine("What is the size of the engine? (Numeric Value)");
                sEnterInfo = Console.ReadLine();

                infoStatus = errorCheckDouble(ref sEnterInfo);

                if (infoStatus == true)
                {
                    dSizeOfEngineInfo = Convert.ToDouble(sEnterInfo);
                    break;
                }
            }

        }


        /*

        */
        public bool errorCheckNum(ref string number)
        {
            bool retValue = true;

            if (Regex.IsMatch(number, "^\\d+$") == false) //Check to see if it's a number only
            {
                Console.Clear();
                System.Console.WriteLine("Invalid input! Try again");
                retValue = false; 
            }
            return retValue; 
        }


        /*

        */
        public bool errorCheckDouble(ref string number)
        {
            bool retValue = true;
            double amount;
            //Check to see if the parsing worked 
            retValue = Double.TryParse(number, out amount);

            if (retValue == false)
            {
                // double here
                Console.Clear(); 
                System.Console.WriteLine("Invalid input! Try again");
            }
            return retValue;
        }


        /*

       */
        public void menu()
        {
            System.Console.WriteLine("1. ADD MOTORCYCLE");
            System.Console.WriteLine("2. ADD SMALL TRUCK");
            System.Console.WriteLine("3. ADD AUTOMOBILE");
            System.Console.WriteLine("4. MODIFY ODOMETER");
            System.Console.WriteLine("5. LIST MODEL");
            System.Console.WriteLine("6. LIST TYPE");
            System.Console.WriteLine("7. LIST ALL");
            System.Console.WriteLine("8. DELETE");
            System.Console.WriteLine("9. EXIT");
        }

        /******************** End of Related Methods  ********************/


        public void getUserInput(container trackNewVehicle)
        {
            trackNewVehicle.readDataBase(); //Read information from the database         
            
            /* Variables related to basic vehicle class (applies to all 3 sub-classes) */
            string sManufacturerInfo = "";
            string sModelInfo = "";
            string sTypeOfMotocycle = "";
            string nPurchaseDateInfo = "";

            int nModelYearInfo = 0;

            double dInitalPurchasePrice = 0;
            double dCurrentOdometerInfo = 0.0;
            double dSizeOfEngineInfo = 0.0;

            /* Variables related to small truck class */
            double cargoSize = 0;
            double towingSize = 0;

            /* Variables related to automobile class */
            int nNumOfDoor = 0;
            string aFuel = "";


            /* Count of sub-class objects */
            int nMotoCount = 0;
            int nAutoCount = 0;
            int nSmallTruckCount = 0;

            /* Other Variables */
            List<Vehicle> temp = new List<Vehicle>();

            string sMenuOption = "";

            int nChoice = 0;

            bool bStatusOFLoop = true;
            bool menuStatus = true;

            //Start of menu screen loop

            while (bStatusOFLoop)
            {
                menu(); 

                //User chooses an option 
                sMenuOption = Console.ReadLine();                
                menuStatus = errorCheckNum(ref sMenuOption);

                if (menuStatus == true)
                {
                    //convert string to int
                    nChoice = Convert.ToInt32(sMenuOption);

                    //Check which chained if and else match the number inputted 
                    if (nChoice == 1)
                    {
                        nMotoCount = trackNewVehicle.GetMotocycleCount;
                        if (nMotoCount < 10)
                        {
                            createMotorcycle(
                                   sManufacturerInfo,
                                   sModelInfo,
                                   nModelYearInfo,
                                   dInitalPurchasePrice,
                                   nPurchaseDateInfo,
                                   dCurrentOdometerInfo,
                                   dSizeOfEngineInfo,
                                   sTypeOfMotocycle, 
                                   trackNewVehicle);
                            Console.Clear();
                            Console.WriteLine("Motorcycle created successfully");
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("You cannot add any more!");
                            continue;
                        }

                    }
                    else if (nChoice == 2)
                    {
                        //Add to small truck count 
                        nSmallTruckCount = trackNewVehicle.GetSmallTruckCount; 

                        if(nSmallTruckCount < 10)
                        {
                            createSmallTruck(sManufacturerInfo,
                                sModelInfo,
                                nModelYearInfo,
                                dInitalPurchasePrice,
                                nPurchaseDateInfo,
                                dCurrentOdometerInfo,
                                dSizeOfEngineInfo,
                                cargoSize,
                                towingSize,
                                trackNewVehicle);
                            Console.Clear();
                            Console.WriteLine("Small Truck created successfully");
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("You cannot add any more!");
                            continue;
                        }          
                    }
                    else if (nChoice == 3)
                    {
                        //Add to small truck count 
                        nSmallTruckCount = trackNewVehicle.GetAutoMobileCount;

                        if (nSmallTruckCount < 10)
                        {
                            createAutomobile(sManufacturerInfo,
                                sModelInfo,
                                nModelYearInfo,
                                dInitalPurchasePrice,
                                nPurchaseDateInfo,
                                dCurrentOdometerInfo,
                                dSizeOfEngineInfo,
                                nNumOfDoor,
                                aFuel,
                                trackNewVehicle);
                            Console.Clear();
                            Console.WriteLine("Automobile created successfully");
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("You cannot add any more!");
                            continue;
                        }
                    }
                    else if (nChoice == 4)
                    {

                    }
                    else if (nChoice == 5)
                    {
                        nAutoCount = trackNewVehicle.AmountOfVehicle;
                        if (nAutoCount == 0)
                        {
                            System.Console.WriteLine("List cannot be empty");
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("Type the model you wish to search for");
                            sMenuOption = Console.ReadLine();
                            temp = trackNewVehicle.ModelTracking(sMenuOption);
                            foreach (Vehicle v in temp)
                            {
                                if (v.GetType().ToString() == "Assigment_2.Small_Truck")
                                {
                                    Small_Truck ST = (Small_Truck)v;
                                    System.Console.WriteLine("Manufacturer: " + ST.Manufacturer);
                                    System.Console.WriteLine("Model: " +  ST.Model);
                                    System.Console.WriteLine("Model Year:" + ST.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + ST.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + ST.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + ST.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine " + ST.SizeOfEngine);
                                    System.Console.WriteLine("Cargo Capacity " + ST.CargoCapacity);
                                    System.Console.WriteLine("Towing Capacity " + ST.TowingCapacity);

                                    System.Console.WriteLine("*******************************");

                                }
                                else if (v.GetType().ToString() == "Assigment_2.Motorcycle")
                                {
                                    Motorcycle Moto = (Motorcycle)v;


                                    System.Console.WriteLine("Manufacturer: " + Moto.Manufacturer);
                                    System.Console.WriteLine("Model: " + Moto.Model);
                                    System.Console.WriteLine("Model Year:" + Moto.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + Moto.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + Moto.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + Moto.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine:" + Moto.SizeOfEngine);
                                    System.Console.WriteLine("Motocycle type: " + Moto.TypeOfMotocycle);
                                    System.Console.WriteLine("*******************************");
                                }
                                else if (v.GetType().ToString() == "Assigment_2.Automobile")
                                {
                                    Automobile AU = (Automobile)v;

                                    System.Console.WriteLine("Manufacturer: " + AU.Manufacturer);
                                    System.Console.WriteLine("Model: " + AU.Model);
                                    System.Console.WriteLine("Model Year:" + AU.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + AU.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + AU.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + AU.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine: " + AU.SizeOfEngine);
                                    System.Console.WriteLine("Number of doors: " + AU.NumberOfDoors);
                                    System.Console.WriteLine("Type of fuel ", AU.TypeOfFuel);
                                    System.Console.WriteLine("*******************************");

                                }
                            }
                        }

                    }
                    else if (nChoice == 6)
                    {
                        System.Console.WriteLine("Input the vehicle type you wish to search for.");
                        System.Console.WriteLine("1. Motocycle");
                        System.Console.WriteLine("2. Automobile");
                        System.Console.WriteLine("3. Small Truck");
                        System.Console.WriteLine("4. Main Menu");
                        sMenuOption = Console.ReadLine();
                        if ((sMenuOption == "1") || (sMenuOption == "2") || (sMenuOption == "3"))
                        {
                            temp = trackNewVehicle.TypeTracking(sMenuOption);
                            foreach (Vehicle v in temp)
                            {
                                if (v.GetType().ToString() == "Assigment_2.Small_Truck")
                                {
                                    Small_Truck ST = (Small_Truck)v;
                                    System.Console.WriteLine("Manufacturer: " + ST.Manufacturer);
                                    System.Console.WriteLine("Model: " + ST.Model);
                                    System.Console.WriteLine("Model Year:" + ST.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + ST.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + ST.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + ST.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine " + ST.SizeOfEngine);
                                    System.Console.WriteLine("Cargo Capacity " + ST.CargoCapacity);
                                    System.Console.WriteLine("Towing Capacity " + ST.TowingCapacity);
                                    System.Console.WriteLine("*******************************");


                                }
                                else if (v.GetType().ToString() == "Assigment_2.Motorcycle")
                                {
                                    Motorcycle Moto = (Motorcycle)v;


                                    System.Console.WriteLine("Manufacturer: " + Moto.Manufacturer);
                                    System.Console.WriteLine("Model: " + Moto.Model);
                                    System.Console.WriteLine("Model Year:" + Moto.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + Moto.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + Moto.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + Moto.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine:" + Moto.SizeOfEngine);
                                    System.Console.WriteLine("Motocycle type: " + Moto.TypeOfMotocycle);
                                    System.Console.WriteLine("*******************************");

                                }
                                else if (v.GetType().ToString() == "Assigment_2.Automobile")
                                {
                                    Automobile AU = (Automobile)v;

                                    System.Console.WriteLine("Manufacturer: " + AU.Manufacturer);
                                    System.Console.WriteLine("Model: " + AU.Model);
                                    System.Console.WriteLine("Model Year:" + AU.ModelYear);
                                    System.Console.WriteLine("Purchase Price:" + AU.PurchasePrice);
                                    System.Console.WriteLine("Purchase Date: " + AU.PurchaseDate);
                                    System.Console.WriteLine("Current Odometer: " + AU.CurrentOdometer);
                                    System.Console.WriteLine("Size of Engine: " + AU.SizeOfEngine);
                                    System.Console.WriteLine("Number of doors: " + AU.NumberOfDoors);
                                    System.Console.WriteLine("Type of fuel ", AU.TypeOfFuel);
                                    System.Console.WriteLine("*******************************");

                                }
                            }
                        }
                        else if (sMenuOption == "4")
                        {
                            continue;
                        }
                        else
                        {
                            System.Console.WriteLine("Input not valid!");
                            continue;
                        }

                    }
                    else if (nChoice == 7)
                    {
                        //nAutoCount = trackNewVehicle.GetSmallTruckCount;
                        Console.Clear(); 
                        trackNewVehicle.listingAllContent();                      
                    }
                    else if (nChoice == 8)
                    {
                        while (true)
                        {
                            System.Console.WriteLine("What do you want to delete?");
                            System.Console.WriteLine("1. Motocycle");
                            System.Console.WriteLine("2. Automobile");
                            System.Console.WriteLine("3. Small Truck");
                            System.Console.WriteLine("4. Main Menu");
                            sMenuOption = Console.ReadLine();
                            if (sMenuOption == "1")
                            {

                            }
                            else if (sMenuOption == "2")
                            {

                            }
                            else if (sMenuOption == "3")
                            {

                            }
                            else if (sMenuOption == "4")
                            {

                            }
                            else
                            {
                                System.Console.WriteLine("Not valid");
                                continue;
                            }
                        }


                    }
                    else if (nChoice == 9)
                    {
                        trackNewVehicle.SaveToDatabase(trackNewVehicle);
                        break;
                    }

                    else
                    { }
                }                                                
            }
        }
    }
}




            
