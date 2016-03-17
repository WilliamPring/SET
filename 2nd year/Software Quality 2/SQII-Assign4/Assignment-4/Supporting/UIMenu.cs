///<FileHeader>
/// This is the Supporting Class Library,
///it contains the UIMenu class as well as the FileIO class
///
///</fileHeader>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Container;
using AllEmployees;

namespace Supporting
{
    public class UIMenu
    {
        //variables and instantiations

        //FileIO fileHandlerClass;
        //TheCompany TheCompany;
        //Employee employeeHandler;
        String input;
        TheCompany database;
        FileIO file;

        /// <summary>
        /// Constructor that takes no parameters
        /// </summary>
        public UIMenu()
        {
            database = new TheCompany();
            file = new FileIO();
        }

        /// <summary>
        /// Prints a menu and performs an action depending on the user selection.
        /// Takes no parameters.
        /// Returns true on successful display, or false on exit
        /// </summary>
        public Boolean DisplayMenu()
        {
            //print menu
            Console.WriteLine("***Employee Management System***\n");
            Console.WriteLine("Please choose an option:\n");
            Console.WriteLine("\t1. Add Employee");
            Console.WriteLine("\t2. Remove Employee");
            Console.WriteLine("\t3. Search for Employee");
            Console.WriteLine("\t4. Modify Employee");
            Console.WriteLine("\t5. Display Database");
            Console.WriteLine("\t6. Load a File");
            Console.WriteLine("\t7. Save database to file");
            Console.WriteLine("\t8. Exit Program");

            //read next key press from user
            input = Console.ReadLine();
            Console.Clear();

            switch (input) //switch on ASCII values of entered choice
            {
                //add employee
                case "1":
                    Console.WriteLine("***Employee Management System***\n");
                    Console.WriteLine("Which Kind of Employee would you like to add?\n");
                    Console.WriteLine("\t1. Full-time Employee");
                    Console.WriteLine("\t2. Part-time Employee");
                    Console.WriteLine("\t3. Contract Employee");
                    Console.WriteLine("\t4. Seasonal Employee");

                    input = Console.ReadLine(); //get input from user
                    Console.Clear();

                    switch (input) //comparing against ASCII values (1 is 49, 4 is 52)
                    {
                        case "1":
                            database.AddEmployee("FullTime");
                            break;
                        case "2":
                            database.AddEmployee("PartTime");
                            break;
                        case "3":
                            database.AddEmployee("Contract");
                            break;
                        case "4":
                            database.AddEmployee("Seasonal");
                            break;
                    }
                    break;

                //remove employee
                case "2":
                    Console.WriteLine("***Employee Management System***\n");
                    Console.WriteLine("Enter a Social Insurance Number to remove:\n");
                    input = Console.ReadLine();
                    Console.Clear();
                    database.RemoveEmployee(input);
                    break;

                //search employees
                case "3":
                    Console.WriteLine("***Employee Management System***\n");
                    Console.WriteLine("Enter a name to search for:\n");
                    input = Console.ReadLine();
                    Console.Clear();
                    database.FindEmployeeData(input);
                    break;

                //modify employee
                case "4":
                    Console.WriteLine("***Employee Management System***\n");
                    database.UpdateEmployeeData();
                    break;

                //Display Database.
                case "5":
                    database.DisplayDatabase("");
                    break;

                //load database file
                case "6":
                    database.LoadDatabase();
                    break;

                //save database to file
                case "7":
                    database.SaveDatabase();
                    break;

                //Exit
                case "8":
                    while (input != "y" && input != "n")
                    {
                        Console.WriteLine("***Employee Management System***\n");
                        Console.WriteLine("Would you like to save your changes to the database before exit? (Y/N)");
                        input = Console.ReadLine();
                        input = input.ToLower();
                        switch (input)
                        {
                            case "y":
                                database.SaveDatabase();
                                break;
                            case "n":
                                break;
                        }
                    }
                    return false;
            }
            return true;
           
        }
    }
}
