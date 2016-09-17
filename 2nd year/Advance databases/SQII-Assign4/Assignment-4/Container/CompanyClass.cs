using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using AllEmployees;
using Supporting;


namespace Container
{
    public class TheCompany
    {
        private List<Employee> databaseContainer;
        private FileIO databaseFile;

        private Logging logfile;

        /// <summary>
        /// Constructor used to initialize TheCompany class attributes.
        /// </summary>
        public TheCompany()
        {
            databaseContainer = new List<Employee>();
            databaseFile = new FileIO();
            logfile = new Logging();
            
        }

        /// <summary>
        /// Function Name: AddEmployee.
        /// The purpose of this function is to replicate the adding of an employee to the database.
        /// It can be expanded on in the future to use widespread but is currently only used for testing purposes.
        /// While testing this functional will ensure that the adding to list is succesfful and that validation is
        /// properly being conducted of the data being added to the database.
        /// </summary>
        /// <param name="entry">This parameter is an object passed in that can be any 1 of the 4 types of employees.</param>
        /// <returns></returns>
        public bool AddEmployee(Employee entry)
        {
            // declare local variables
            bool result;
            Employee genericClass = new Employee();

            // switch on the type of employee passed in
            // and properly cast it
            switch (entry.GetEmployeeType())
            {
                case "CT":
                    genericClass = (ContractEmployee)entry;
                    break;
                case "SN":
                    genericClass = (SeasonalEmployee)entry;
                    break;
                case "FT":
                    genericClass = (FullTimeEmployee)entry;
                    break;
                case "PT":
                    genericClass = (PartTimeEmployee)entry;
                    break;

            }

            // attempt to add the entry to the database
            // if the result is false from the validation return false
            // otherwise attempt to add it to the database if there is an
            // error return false
            try
            {
                result = genericClass.Validate();
                if (result != false)
                {
                    databaseContainer.Add(genericClass);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        /// <summary>
        /// Method Name: AddEmployee.
        /// The purpose of this method is to add an employee of any type(PartTimeEmployee,
        /// SeasonalEmployee, FullTimeEmployee, ContractEmployee).
        /// </summary>
        /// <param name="employeeType">This is a string representing which type of employee were adding.</param>
        /// <returns></returns>
        public bool AddEmployee(String employeeType)
        {
            // call the appropriate functions based on the employee type
            //passed in
            switch (employeeType)
            {
                // add a parttime employee
                case "PartTime":
                    AddPartTimeEmployee();
                    break;
                // add a seasonal employee
                case "Seasonal":
                    AddSeasonalEmployee();
                    break;
                // add a fulltime employee
                case "FullTime":
                    AddFullTimeEmployee();
                    break;
                // add a contract employee
                case "Contract":
                    AddContractEmployee();
                    break;
            }
            
            return true;
        }

        /// <summary>
        /// Method Name: AddPartTimeEmployee.
        /// The purpose of this function is to get teh data from the user associated with the
        /// part time employee and store that employee in a database that is sorted by firstName.
        /// </summary>
        /// <returns>Returns True on success or False if the Employee could not be added to the database.</returns>
        private bool AddPartTimeEmployee()
        {
            // initialize a part time employee entry
            PartTimeEmployee entry = new PartTimeEmployee();
            // initialize local variables
            bool result = false;
            float hourlyRate = 0;

            this.SetBaseAttributes(entry);

            // get the employee's hourly wages
            Console.WriteLine("Please enter the employee's hourly wage(ie. 15.00):");
            while (true)
            {
                try
                {
                    hourlyRate = float.Parse(Console.ReadLine().Replace("$", ""));
                    entry.SetHourlyRate(hourlyRate);
                    break;
                }
                catch
                {
                    Console.WriteLine("Please re-enter a valid employee's hourly wage(ie. 15.00):");
                }
            }

            Console.WriteLine("Please enter the date the employee was hired <YYYY-MM-DD>:");
            result = entry.SetDateOfHire(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please enter a valid date in which the employee was hired <YYYY-MM-DD>:");
                result = entry.SetDateOfHire(Console.ReadLine());
            }

            Console.WriteLine("Please enter a date of termination or enter a '0' if the employee is still employed at your company:");
            result = entry.SetDateOfTermination(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please re-enter a valid date of termination or enter a '0' if the employee is still employed at your company:");
                result = entry.SetDateOfTermination(Console.ReadLine());
            }

            // validate all data
            if (entry.Validate() == false)
            {

                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Part-Time Employee");
                Console.WriteLine("There was an error with the employee entered. Please add the employee again.");
                return false;
            }
            // add the employee to the database using the firstName as a key.
            try
            {
                databaseContainer.Add(entry);
            }
            catch
            {
                // display a message to the user
                Console.WriteLine("The employee could not be added to the database.");

                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Part-Time Employee");
                return false;
            }

            logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'S', "Part-Time Employee");

            // display the employee added to the database
            Console.Clear();
            Console.WriteLine("\nThe employee added was:\n");

            entry.Details();

            return true;

        }


        /// <summary>
        /// Method Name: AddSeasonalEmployee.
        /// The purpose of this function is to get the user to enter the data pertaining to a full time employee and
        /// add that into the employee database.
        /// </summary>
        /// <returns>Returns True if succesfull and False otherwise.</returns>
        private bool AddSeasonalEmployee()
        {
            // create a seasonal employee object which will be placed in the database after entering the data
            SeasonalEmployee entry = new SeasonalEmployee();
            //declare local variables
            bool result = false;
            float piecePay = 0;

            this.SetBaseAttributes(entry);

            // get the season in which the employee was employed
            Console.WriteLine("Please enter the season in which the employee was employed:");
            result = entry.SetSeason(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please re-enter a valid season in which the employee was employed:");
                result = entry.SetSeason(Console.ReadLine());
            }

            // get the pay in which the employee received
            Console.WriteLine("Please enter the piecpay which the employee received for their work:");
            while (true)
            {
                try
                {
                    piecePay = float.Parse(Console.ReadLine().Replace("$", ""));
                    entry.SetPiecePay(piecePay);
                    break;
                }
                catch
                {
                    Console.WriteLine("Please re-enter a valid piece in which the employee received for their work:");
                }
            }

            // validate all data entered for the current seasonal employee
            if (entry.Validate() == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Seasonal Employee");
                Console.WriteLine("There was an error with the employee entered. Please add the employee again.");
                return false;
            }
            // add the employee to the database
            try
            {
                databaseContainer.Add(entry);
            }
            catch
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Seasonal Employee");
                // if there was an error indicate that the employee could not be added to the database.
                Console.WriteLine("The employee could not be added to the database.");
                return false;
            }

            Console.Clear();
            Console.WriteLine("\nThe employee added was:\n");
            logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'S', "Seasonal Employee");

            entry.Details();

            // return true on success
            return true;
        }


        /// <summary>
        /// Method Name: AddFullTimeEmployee.
        /// The purpose of thsi function is to get the data associated with a Full Time Employee from the user, and
        /// add it to the database.  The data will be validated before it is added to the database. Any errors
        /// will be indicated to the user.
        /// </summary>
        /// <returns></returns>
        private bool AddFullTimeEmployee()
        {
            // create  a Full Time employee object which will house the following information
            // for this full time employee
            FullTimeEmployee entry = new FullTimeEmployee();
            // declare local variables
            bool result = false;
            float salary = 0;

            this.SetBaseAttributes(entry);

            // get the date the employee was hired
            Console.WriteLine("Please enter the date the employee was hired <YYYY-MM-DD>:");
            result = entry.SetDateOfHire(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please enter a valid date in which the employee was hired <YYYY-MM-DD>:");
                result = entry.SetDateOfHire(Console.ReadLine());
            }

            // get the employee's yearly salary
            Console.WriteLine("Please enter the employee's yearly salary (example 45000.54):");
            while (true)
            {
                
                try
                {
                    salary = float.Parse(Console.ReadLine().Replace("$", ""));
                    if (entry.SetSalary(salary) != false)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please re-enter a valid employee's salary (example 45000.54):");
                    }
                }
                catch
                {
                    Console.WriteLine("Please re-enter a valid employee's salary (example 45000.54):");
                }
            }


            // get the date of termination if the employee was fired
            Console.WriteLine("Please enter a date of termination or enter a '0' if the employee is still employed at your company:");
            result = entry.SetDateOfTermination(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please re-enter a valid date of termination or enter a '0' if the employee is still employed at your company:");
                result = entry.SetDateOfTermination(Console.ReadLine());
            }

            // validate the employee information
            if (entry.Validate() == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Full-Time Employee");
                Console.WriteLine("There was an error with the employee entered. Please add the employee again.");
                return false;
            }
            // add the entry to the database
            try
            {
                databaseContainer.Add(entry);
            }
            catch
            {
                // if there was an error indicate to the user that the employee could not be added to the database
                Console.WriteLine("The employee could not be added to the database.");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Full-Time Employee");
                return false;
            }
            Console.Clear();
            Console.WriteLine("\nThe employee added was:\n");
            logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'S', "Full-Time Employee");

            entry.Details();
            return true;
        }


        /// <summary>
        /// Method Name: AddContractEmployee.
        /// The purpose of this function is to get the data associated with a ContractEmployee from the user
        /// and add this employee to the database.
        /// </summary>
        /// <returns>Returns a value of True if it was succesffull otherwise returns a False boolean value.</returns>
        private bool AddContractEmployee()
        {
            // create a contract employee object which will house the data for the contract employee and 
            // will be added to the database
            ContractEmployee entry = new ContractEmployee();
            // initilize local variables
            bool result = false;
            float contractorsFixedAmount = 0;

            this.SetBaseAttributes(entry);

            // get the start date in which the contractor began work
            Console.WriteLine("Please enter the start date for the contracted employee <YYYY-MM-DD>:");
            result = entry.SetContractStartDate(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please enter a valid date in which the contractor started <YYYY-MM-DD>:");
                result = entry.SetContractStartDate(Console.ReadLine());
            }

            // get the date in which the contractor ended the work
            Console.WriteLine("Please enter the date the contractor ended working for your company <YYYY-MM-DD>:");
            result = entry.SetContractStopDate(Console.ReadLine());
            while (result == false)
            {
                Console.WriteLine("Please re-enter the date the contractor ended working for your company <YYYY-MM-DD>:");
                result = entry.SetContractStopDate(Console.ReadLine());
            }

            // get the contractor's fixed amount of pay
            Console.WriteLine("Please enter the contractor's fixed amount of pay (e.g. 4570.80):");
            while (true)
            {
                try
                {
                    contractorsFixedAmount = float.Parse(Console.ReadLine().Replace("$", ""));
                    entry.SetFixedContractAmount(contractorsFixedAmount);
                    break;
                }
                catch
                {
                    Console.WriteLine("Please re-enter a valid contractor's fixed amount of pay (e.g. 4570.80):");
                }
            }

            // validate all data, print an error if data is incorrect
            if (entry.Validate() == false)
            {
                Console.WriteLine("There was an error with the employee entered. Please add the employee again:");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Contract Employee");
                return false;
            }

            // add the contracted employee to the database
            try
            {
                databaseContainer.Add(entry);
            }
            // if there was an error add the employee to the database, indicate that the contracted employee could
            // not be added to the database.
            catch
            {
                Console.WriteLine("The employee could not be added to the database");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Contract Employee");
                return false;
            }

            Console.Clear();
            Console.WriteLine("\nThe employee added was:\n");
            logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'S', "Part-Time Employee");
            entry.Details();

            return true;
        }

        
        /// <summary>
        /// This method is used to set the first name, last name, date of birth, and SIN attributes, which
        /// is found in every employee class.  This eliminates repeating the code 4 times in the above functions.
        /// </summary>
        /// <param name="entry">An object of type Employee used to set the attributes</param>
        private void SetBaseAttributes(Employee entry)
        {
            // initialize local variables
            bool result = false;
            String businessName = "";
            String lastName = "";
            String firstName = "";
            String temp = "";

            Console.WriteLine("***Employee Management System***\n");

            if (entry.GetEmployeeType() == "CT")
            {
                // get the employee's last name
                Console.WriteLine("Please enter the contractor's business name:");
                businessName = Console.ReadLine();
                temp = businessName;

                temp = temp.Replace(" ", "");
                if (temp == "")
                {
                    result = false;
                }
                else
                {
                    result = entry.SetLastName(businessName);
                }
                
                while (result == false)
                {
                    Console.WriteLine("Please re-enter a valid contractor's business name:");
                    businessName = Console.ReadLine();
                    temp = businessName;

                    temp = temp.Replace(" ", "");
                    if (temp == "")
                    {
                        result = false;
                    }
                    else
                    {
                        result = entry.SetLastName(businessName);
                    }
                
                }

                // get the employee's date of birth
                Console.WriteLine("Please enter the date of the company's incorporation <YYYY-MM-DD>:");
                result = entry.SetDateOfBirth(Console.ReadLine());
                while (result == false)
                {
                    Console.WriteLine("Please re-enter the date of the company's incorporation <YYYY-MM-DD>:");
                    result = entry.SetDateOfBirth(Console.ReadLine());
                }

                // get the employee's social insurance number
                Console.WriteLine("Please enter the Business Number:");
                result = entry.SetSocialNumber(Console.ReadLine().Replace(" ", ""));
                while (result == false)
                {
                    Console.WriteLine("Please re-enter the contractor's Business Number:");
                    result = entry.SetSocialNumber(Console.ReadLine().Replace(" ", ""));

                }

            }
            else
            {
                // get the employee's first name
                Console.WriteLine("Please enter the employee's first name:");
                firstName = Console.ReadLine().Replace(" ", "");
                if (firstName == "")
                {
                    result = false;
                }
                else
                {
                    result = entry.SetFirstName(firstName);
                }

                while (result == false)
                {
                    Console.WriteLine("Please re-enter a valid employee's first name:");
                    firstName = Console.ReadLine().Replace(" ", "");
                    
                    if (firstName == "")
                    {
                        result = false;
                    }
                    else
                    {
                        result = entry.SetFirstName(firstName);
                    }
                }

                // get the employee's last name
                Console.WriteLine("Please enter the employee's last name:");
                lastName = Console.ReadLine().Replace(" ", "");

                if (lastName == "")
                {
                    result = false;
                }
                else
                {
                    result = entry.SetLastName(lastName);
                }

                while (result == false)
                {
                    Console.WriteLine("Please re-enter a valid employee's last name:");
                    lastName = Console.ReadLine().Replace(" ", "");
                    if (lastName == "")
                    {
                        result = false;
                    }
                    else
                    {
                        result = entry.SetLastName(lastName);
                    }

                }

                // get the employee's date of birth
                Console.WriteLine("Please enter the employee's date of birth <YYYY-MM-DD>:");
                result = entry.SetDateOfBirth(Console.ReadLine());
                while (result == false)
                {
                    Console.WriteLine("Please re-enter a valid employee's date of birth <YYYY-MM-DD>:");
                    result = entry.SetDateOfBirth(Console.ReadLine());
                }

                // get the employee's social insurance number
                Console.WriteLine("Please enter the employee's Social Insurance Number:");
                result = entry.SetSocialNumber(Console.ReadLine().Replace(" ", ""));
                while (result == false)
                {
                    Console.WriteLine("Please re-enter a valid employee's Social Insurance Number:");
                    result = entry.SetSocialNumber(Console.ReadLine().Replace(" ", ""));

                }
            }

        }

        /// <summary>
        /// Method Name: DisplayDatabase
        /// The purpose of this method is to display the entire contents of the database sorted by either
        /// first name or last name or SIN which is passed into the filter or received from the user's input.
        /// Sorting Algorithm adapted from : http://www.developerfusion.com/code/5513/sorting-and-searching-using-c-lists/
        /// </summary>
        /// <param name="filter"> This parameter determines how the list is sorted.</param>
        public void DisplayDatabase(String filter)
        {
            String userInput = "";
            int entriesDisplayed = 0;

            Console.WriteLine("***Employee Management System***\n");

            // check to see if a value was passed to the function for the filter
            if (filter == "")
            {
                // if the filter was not passed to this function display a menu giving
                // the user the option to sort the database to their liking
                Console.WriteLine("You can sort the database by the following filters:");
                Console.WriteLine("\t1. Last Name");
                Console.WriteLine("\t2. First Name");
                Console.WriteLine("\t3. Social Insurance Number");
                Console.WriteLine("\t4. Seasonal Employees Only");
                Console.WriteLine("\t5. Contract Employees Only");
                Console.WriteLine("\t6. Part-Time Employees Only");
                Console.WriteLine("\t7. Full-Time Employees Only");

                Console.WriteLine("Please enter your choice:");
                // get the user's input until it is either number 1 to 7
                userInput = Console.ReadLine();
                while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" &&
                    userInput != "5" && userInput != "6" && userInput != "7")
                {
                    Console.WriteLine("Please re-enter a valid choice:");
                    userInput = Console.ReadLine();
                }
            }
            else
            {
                // if the user has passed a value down set the userInput to be the value in filter
                // which can be LAST, FIRST, or SIN
                userInput = filter;
            }

            // switch on the user's input to correctly sort based on their liking
            switch (userInput)
            {

                // if the list is being sorted by lastname
                case "1":
                case "LAST":
                    if (databaseContainer.Count != 0)
                    {
                        Console.WriteLine("Sorted list, by last name:\n");
                        // sort the list by last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2)
                        {
                            return employee1.GetLastName().CompareTo(employee2.GetLastName());
                        });
                        // for each element display it's contents with 3 being displayed at once
                        databaseContainer.ForEach(delegate(Employee entry)
                        {
                            // display the information pertaining to a specific employee
                            entry.Details();
                            Console.WriteLine("\n");
                            // increment the entries displayed counter
                            entriesDisplayed++;
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;

                // if the list is being sorted by the last name
                case "FIRST":
                case "2":
                    if (databaseContainer.Count != 0)
                    {
                        Console.WriteLine("Sorted list, by first name:\n");
                        // sort the list by the last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetFirstName().CompareTo(employee2.GetFirstName()); });
                        // for each entry in the list call the Details method which will output the 
                        // specific data relevant to the type of Employee the entry is (ie. full time, part time etc.)
                        databaseContainer.ForEach(delegate(Employee entry)
                        {
                            // display the information pertaining to a specific employee
                            entry.Details();
                            Console.WriteLine("\n");
                            // increment the entries displayed counter
                            entriesDisplayed++;
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;

                // if the list is being sorted by the SIN 
                case "SIN":
                case "3":
                    if (databaseContainer.Count != 0)
                    {
                        Console.WriteLine("Sorted list, by SIN:\n");
                        // sort the list by sin number
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetSocialNumber().CompareTo(employee2.GetSocialNumber()); });
                        // for each entry in the  list output the information for each specific type of employee
                        databaseContainer.ForEach(delegate(Employee entry)
                        {

                            // display the information pertaining to a specific employee
                            entry.Details();
                            Console.WriteLine("\n");
                            // increment the entries displayed counter
                            entriesDisplayed++;
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;
                case "4":
                    if (databaseContainer.Count != 0)
                    {

                        Console.WriteLine("Showing Seasonal employees only.\n");
                        // sort the list by last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetLastName().CompareTo(employee2.GetLastName()); });
                        // for each entry in the  list output the information the employee is a 
                        // seasonal employee
                        databaseContainer.ForEach(delegate(Employee entry)
                        {

                            if (entry.GetEmployeeType() == "SN")
                            {

                                // display the information pertaining to a specific employee
                                entry.Details();
                                Console.WriteLine("\n");
                                // increment the entries displayed counter
                                entriesDisplayed++;
                            }
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }

                    break;
                case "5":
                    if (databaseContainer.Count != 0)
                    {

                        Console.WriteLine("Showing Contract employees only.\n");
                        // sort the list by last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetLastName().CompareTo(employee2.GetLastName()); });
                        // for each entry in the  list output the information the employee is a 
                        // contract employee
                        databaseContainer.ForEach(delegate(Employee entry)
                        {
                            // if the employee is a contract employee display the details
                            if (entry.GetEmployeeType() == "CT")
                            {

                                // display the information pertaining to a specific employee
                                entry.Details();
                                Console.WriteLine("\n");
                                // increment the entries displayed counter
                                entriesDisplayed++;
                            }
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;
                case "6":
                    if (databaseContainer.Count != 0)
                    {
                        Console.WriteLine("Showing Part-time employees only.\n");
                        // sort the list by last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetLastName().CompareTo(employee2.GetLastName()); });
                        // for each entry in the  list output the information the employee is a 
                        // part time employee
                        databaseContainer.ForEach(delegate(Employee entry)
                        {
                            // if the employee is a part time employee display the details
                            if (entry.GetEmployeeType() == "PT")
                            {

                                // display the information pertaining to a specific employee
                                entry.Details();
                                Console.WriteLine("\n");
                                // increment the entries displayed counter
                                entriesDisplayed++;
                            }
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;
                case "7":
                    if (databaseContainer.Count != 0)
                    {
                        Console.WriteLine("Showing Full-time employees only.\n");
                        // sort the list by last name
                        databaseContainer.Sort(delegate(Employee employee1, Employee employee2) { return employee1.GetLastName().CompareTo(employee2.GetLastName()); });
                        // for each entry in the  list output the information the employee is a 
                        // full time employee
                        databaseContainer.ForEach(delegate(Employee entry)
                        {
                            // if the employee is full time then display the details
                            if (entry.GetEmployeeType() == "FT")
                            {

                                // display the information pertaining to a specific employee
                                entry.Details();
                                Console.WriteLine("\n");
                                // increment the entries displayed counter
                                entriesDisplayed++;
                            }
                            // display 3 employee's information at a time
                            if ((entriesDisplayed % 2) == 0 && entriesDisplayed != 0)
                            {
                                Console.WriteLine("<MORE>");
                                Console.ReadLine();

                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine("The database is empty.\n");
                    }
                    break;
                default:
                    break;

            }

        }

        /// <summary>
        /// Method Name: RemoveEmployee
        /// The purpose of this function is to remove a specific employee from the database
        /// based on their SIN number. It uses the SIN number because it is the only unique
        /// identifier for an employee.
        /// </summary>
        /// <param name="SIN">This is the SIN number of the employee that is going to be deleted.</param>
        /// <returns></returns>
        public bool RemoveEmployee(String SIN)
        {
            //initialize local variables
            int i = 0;
            int foundElement = 0;
            int removeFlag = 0;
            SIN = SIN.Replace(" ", "");

            // find the element based on the employee's SIN number
            for (i = 0; i < databaseContainer.Count; i++)
            {
                // if the sin number is found in the database set the 
                // found element to be that index
                if (databaseContainer[i].GetSocialNumber() == SIN)
                {
                    foundElement = i;
                    removeFlag++;
                }
            }

            if (removeFlag == 0)
            {
                // if no entry was found containg the searched for SIN number
                // than indicate that to the user
                Console.WriteLine("No entries with the SIN number entered were found.");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'R', 'F', "No Employee Found.");
                return false;

            }
            try
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'S', (databaseContainer[foundElement].GetFirstName() + " " + databaseContainer[foundElement].GetLastName()));
                // remove the element found at the index store in foundElement
                databaseContainer.RemoveAt(foundElement);

            }
            catch
            {
                Console.WriteLine("Could not remove employee from database.");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'A', 'F', "Employee");
                return false;
            }



            // return true
            return true;
        }

        /// <summary>
        /// Method Name: FindEmployeeData.
        /// The purpose of this function is to find and desplay Employee Data specific to a first name.
        /// </summary>
        /// <param name="firstName">This is a string that contains the first name of the employee the user wishes to 
        /// find in the database.</param>
        /// <returns></returns>
        public bool FindEmployeeData(String firstName)
        {
            // initialize local variable
            int result = 0;

            // for each element in the database
            // compare the first name to the firstname passed in
            databaseContainer.ForEach(delegate(Employee entry)
            {
                try
                {
                    if (entry.GetEmployeeType() != "CT")
                    {
                        // if the database record's first name matches
                        // the name we are searching for display the details.
                        if (entry.GetFirstName().ToUpper() == firstName.ToUpper())
                        {
                            entry.Details();
                            Console.WriteLine("\n");
                            result++;
                        }
                    }
                    else
                    {
                        if (entry.GetLastName().ToUpper() == firstName.ToUpper())
                        {
                            entry.Details();
                            Console.WriteLine("\n");
                            result++;
                        }
                    }
                }
                catch
                {
                    result = 0;

                }
            });

            // if no result was found indicate that the first name searched for
            // was not found in the database
            if (result == 0)
            {
                Console.WriteLine("First Name or Business Name: {0} could not be found in the database", firstName);
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'F', 'F', firstName);
                return false;
            }
            // return true
            logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'F', 'S', firstName);
            return true;
        }


        /// <summary>
        /// Method Name: UpdateEmployeeData.
        /// The purpose of this method is to allow a user to enter a SIN corresponding to an employee that they wish to update.
        /// A menu will then be presented in the console that will allow the user to update specific attributes found in any
        /// of the four employee types.  The user also has the option of changing all attributes.
        /// </summary>
        /// <returns></returns>
        public bool UpdateEmployeeData()
        {

            // initialize local variables
            int foundElement = 0;
            String SIN = "";
            int returnedResult = 0;
            bool result = false;
            float salary = 0, hourlyRate = 0, piecePay = 0, contractorsFixedAmount = 0;
            int i = 0;
            String input = "";
            String firstName = "";
            String lastName = "";
            String temp = "";

            // create objects of each type of employee to be used depending on what type of employee
            // the user is updating
            Employee baseEmployee = new Employee();
            FullTimeEmployee ftEmployee = new FullTimeEmployee();
            PartTimeEmployee ptEmployee = new PartTimeEmployee();
            ContractEmployee cEmployee = new ContractEmployee();
            SeasonalEmployee sEmployee = new SeasonalEmployee();

            // get the user to enter a SIN number of the employee they would like to update
            Console.WriteLine("Please enter the SIN number of the employee you would like to update:");
            // get the user to enter the sin until 
            SIN = Console.ReadLine().Replace(" ", "") ;

            // reset the returnedResult
            returnedResult = 0;

            // find the element based on the employee's SIN number
            for (i = 0; i < databaseContainer.Count; i++)
            {
                // if the sin number is found in the database set the 
                // found element to be that index
                if (databaseContainer[i].GetSocialNumber() == SIN)
                {
                    foundElement = i;
                    returnedResult++;
                }
            }
            // if the result was 0 then the Employee with the SIN entered from the user
            // was not found in the database - return false
            if (returnedResult == 0)
            {
                Console.WriteLine("Could not find employee with the specified SIN");
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'M', 'F', (databaseContainer[foundElement].GetFirstName() + " " + databaseContainer[foundElement].GetLastName()));
                return false;
            }
            // until the user presses 9 allow them to update the employee with the SIN they 
            // previously entered
            while (input != "9")
            {
                // clear the console and display the menu
                Console.Clear();
                // set the baseEmployee to be the employee found in the database
                baseEmployee = databaseContainer[foundElement];

                Console.WriteLine("Currently Updating : {0} {1}", databaseContainer[foundElement].GetFirstName(), databaseContainer[foundElement].GetLastName());
                Console.WriteLine("Updates Available:");
                Console.WriteLine("\t1. First Name.");
                Console.WriteLine("\t2. Last Name/Business.");
                Console.WriteLine("\t3. SIN.");
                Console.WriteLine("\t4. Date Of Birth.");
                Console.WriteLine("\t5. Date Of Hire, Contract Start Date, Season");
                Console.WriteLine("\t6. Salary, PiecePay, Fixed Contract Amount, Hourly Wage");
                Console.WriteLine("\t7. Date of Termination, Contract End Date");
                Console.WriteLine("\t8. Update All Information");
                Console.WriteLine("\t9. Exit");

                // read the user's menu choice
                input = Console.ReadLine();

                // if the option is 1 to 4 then no casting needs to be done to find out which
                // type of employee it is because those attributes are found in all employees
                if (input == "1" || input == "2" || input == "3" || input == "4")
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'M', 'S', (databaseContainer[foundElement].GetFirstName() + " " + databaseContainer[foundElement].GetLastName()));

                    // switch on the input
                    switch (input)
                    {
                            // modify the first name
                        case "1":
                            if (baseEmployee.GetEmployeeType() != "CT")
                            {
                                // display the current employee's firstname
                                Console.WriteLine("Current employee's first name: {0} \n", baseEmployee.GetFirstName());
                                // get the employee's first name, make the user enter it until 
                                // it is valid
                                Console.WriteLine("Please enter a new first name:");
                                firstName = Console.ReadLine().Replace(" ", "");

                                if (firstName == "")
                                {
                                    result = false;
                                }
                                else
                                {
                                    result = baseEmployee.SetFirstName(firstName);
                                }
                                while (result == false)
                                {
                                    Console.WriteLine("Please re-enter a valid employee's first name:");
                                    firstName = Console.ReadLine().Replace(" ", "");

                                    if (firstName == "")
                                    {
                                        result = false;
                                    }
                                    else
                                    {
                                        result = baseEmployee.SetFirstName(firstName);
                                    }
                                }
                            }
                            break;

                            // modify the last name
                        case "2":
                            if (baseEmployee.GetEmployeeType() != "CT")
                            {
                                // display the current employee's last name
                                Console.WriteLine("Current employee's last name: {0}\n", baseEmployee.GetLastName());
                                // get the employee's last name, make the user enter a last name
                                // until it is a valid string
                                Console.WriteLine("Please enter a new last name:");
                                lastName = Console.ReadLine().Replace(" ", "");

                                if (lastName == "")
                                {
                                    result = false;
                                }
                                else
                                {
                                    result = baseEmployee.SetLastName(lastName);
                                }
                                while (result == false)
                                {
                                    Console.WriteLine("Please re-enter a valid employee's last name:");
                                    lastName = Console.ReadLine().Replace(" ", "");

                                    if (lastName == "")
                                    {
                                        result = false;
                                    }
                                    else
                                    {
                                        result = baseEmployee.SetLastName(lastName);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter a bussiness name:");
                                lastName = Console.ReadLine();
                                temp = lastName.Replace(" ", "");

                                if (temp == "")
                                {
                                    result = false;
                                }
                                else
                                {
                                    result = baseEmployee.SetLastName(lastName);
                                }
                                while (result == false)
                                {
                                    Console.WriteLine("Please re-enter a valid business name:");
                                    lastName = Console.ReadLine();
                                    temp = lastName.Replace(" ", "");

                                    if (temp == "")
                                    {
                                        result = false;
                                    }
                                    else
                                    {
                                        result = baseEmployee.SetLastName(lastName);
                                    }
                                }
                                

                            }
                            break;

                            // modify the employee's social insurance number
                        case "3":
                            // display the current employee's social insurance number
                            Console.WriteLine("Current employee's social insurance number : {0}\n", baseEmployee.GetSocialNumber());
                            // get the employee's social insurance number, make user re-enter the SIN until
                            // it is valid
                            Console.WriteLine("Please enter the employee's Social Insurance Number:");
                            while (result == false)
                            {
                                Console.WriteLine("Please re-enter a valid employee's Social Insurance Number:");
                                result = baseEmployee.SetSocialNumber(Console.ReadLine());
                            }
                            break;

                            // modify the employee's date of birth
                        case "4":
                            // display the current employee's date of birth
                            Console.WriteLine("Current employee's date of birth : {0}\n", baseEmployee.GetDateOfBirth().ToShortDateString());
                            // get the employee's date of birth, make the user re-enter the birth date until 
                            // it is valid
                            Console.WriteLine("Please enter the employee's date of birth <YYYY-MM-DD>:");
                            result = baseEmployee.SetDateOfBirth(Console.ReadLine());
                            while (result == false)
                            {
                                Console.WriteLine("Please re-enter a valid employee's date of birth <YYYY-MM-DD>:");
                                result = baseEmployee.SetDateOfBirth(Console.ReadLine());
                            }
                            break;
                    }
                }
                // if were modifying attributes under the menu options 5 - 8
                    // then the attributes change accordingly to which type of employee being modified
                else
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'M', 'F', (databaseContainer[foundElement].GetFirstName() + " " + databaseContainer[foundElement].GetLastName()));
                    // switch on the employee type
                    switch (databaseContainer[foundElement].GetEmployeeType())
                    {
                            
                            // if it's a full time employee being modified then:
                            //
                            // date of hire is being modifited when option 5 is pressed
                            // yearly salary is being modified when option 6 is pressed
                            // date of termination is being modified when option 7 is pressed
                        case "FT":
                            // cast the base employee as a full time employee
                            ftEmployee = (FullTimeEmployee)baseEmployee;
                            switch (input)
                            {
                                    // update date of hire
                                case "5":
                                    // display the current date of hire and get the user to enter the 
                                    // new date of hire
                                    Console.WriteLine("Current Date of Hire : {0}", ftEmployee.GetDateOfHire().ToShortDateString());
                                    // get the date the employee was hired
                                    Console.WriteLine("Please enter the date the employee was hired");
                                    // get teh new date until it is valid
                                    result = ftEmployee.SetDateOfHire(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please enter a valid date in which the employee was hired");
                                        result = ftEmployee.SetDateOfHire(Console.ReadLine());
                                    }
                                    break;
                                    // update yearly salary
                                case "6":
                                    // display the current salary and get the user to enter a new salary
                                    Console.WriteLine("Current Salary is : {0}", ftEmployee.GetSalary());
                                    // get the employee's yearly salary
                                    Console.WriteLine("Please enter the employee's yearly salary (example 45000.54):");
                                    while (true)
                                    {
                                        try
                                        {
                                            salary = float.Parse(Console.ReadLine());
                                            ftEmployee.SetSalary(salary);
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please re-enter a valid employee's salary (example 45000.54):");
                                        }
                                    }
                                    break;
                                    // update the date of termination
                                case "7":
                                    // display the current salary and get the user to enter a new salary
                                    Console.WriteLine("Current date of Termination is : {0}", ftEmployee.GetDateOfTermination().ToShortDateString());
                                    // get the date of termination if the employee was fired
                                    Console.WriteLine("Please enter a date of termination or enter a '0' if the \nemployee is still employed at your company <YYYY-MM-DD>:");
                                    result = ftEmployee.SetDateOfTermination(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please re-enter a valid date of termination or enter a '0' if the \nemployee is still employed at your company <YYYY-MM-DD>:");
                                        result = ftEmployee.SetDateOfTermination(Console.ReadLine());
                                    }

                                    break;
                                    // update all employee information
                                case "8":
                                    Console.Clear();
                                    Console.WriteLine("Employee's current information:");
                                    ftEmployee.Details();
                                    Console.WriteLine("\n");
                                    // if the user is updating all data for an employee remove the current one
                                    // and get them to update all fields by calling the add function
                                    databaseContainer.RemoveAt(foundElement);
                                    this.AddFullTimeEmployee();
                                    break;

                            }
                            break;
                            // if the employee type is a part time employee then:
                            //
                            // option 5 updates the employee's date of hire
                            // option 6 updates the employee's hourly wage
                            // option 7 updates the employee's date of termination
                            // option 8 updates all fields
                        case "PT":
                            // cast the base employee as a part time employee
                            ptEmployee = (PartTimeEmployee)baseEmployee;

                            // switch on the input number
                            switch (input)
                            {
                                // update the employee's hire date
                                case "5":
                                    // display the current date of hire and get the user to enter the 
                                    // new date of hire
                                    Console.WriteLine("Current Date of Hire : {0}", ptEmployee.GetDateOfHire().ToShortDateString());
                                    
                                    // get the new date from the user
                                    Console.WriteLine("Please enter the date the employee was hired <YYYY-MM-DD>:");
                                    result = ptEmployee.SetDateOfHire(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please enter a valid date in which the employee was hired <YYYY-MM-DD>:");
                                        result = ptEmployee.SetDateOfHire(Console.ReadLine());
                                    }
                                    break;
                                    // update the employee's hourly wages
                                case "6":
                                    // display the current date of hire and get the user to enter the 
                                    // new date of hire
                                    Console.WriteLine("Current employee's hourly wages : {0}", ptEmployee.GetHourlyWage());
                                    // get the employee's hourly wages
                                    Console.WriteLine("Please enter the employee's hourly wage(ie. 15.00):");
                                    while (true)
                                    {
                                        try
                                        {
                                            // attempt to parse if it does not succeed then it will throw an exception
                                            hourlyRate = float.Parse(Console.ReadLine());
                                            ptEmployee.SetHourlyRate(hourlyRate);
                                            break;
                                        }
                                        catch
                                        {
                                            // display the error message to the user
                                            Console.WriteLine("Please re-enter a valid employee's hourly wage(ie. 15.00):");
                                        }
                                    }
                                    break;

                                    // update the employee's date of termination
                                case "7":
                                    // display the current date of hire and get the user to enter the 
                                    // new date of hire
                                    Console.WriteLine("Current Employee's date of termination : {0}", ftEmployee.GetDateOfTermination().ToShortDateString());

                                    // get the new date of termination
                                    Console.WriteLine("Please enter a date of termination or enter a '0' if the \nemployee is still employed at your company <YYYY-MM-DD>:");
                                    result = ptEmployee.SetDateOfTermination(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please re-enter a valid date of termination or enter a '0' if the \nemployee is still employed at your company <YYYY-MM-DD>:");
                                        result = ptEmployee.SetDateOfTermination(Console.ReadLine());
                                    }
                                    break;
                                case "8":
                                    Console.Clear();
                                    Console.WriteLine("Employee's current information:");
                                    ptEmployee.Details();
                                    Console.WriteLine("\n");
                                    // if the user is updating all data for an employee remove the current one
                                    // and get them to update all fields by calling the add function
                                    databaseContainer.RemoveAt(foundElement);
                                    this.AddPartTimeEmployee();
                                    break;
                            }
                            break;

                            // if were modifying a Contract Employee then
                            //
                            // option 5 is modifying the date in which the contract employee started
                            // option 6 is the fixed contract amount
                            // option 7 is modifying the date in which the contract employee ended their work
                        case "CT":
                            
                          
                            // cast the base employee as a contract employee
                            cEmployee = (ContractEmployee)baseEmployee;
                            switch (input)
                            {
                                    // modify the date which the contract employee started
                                case "5":
                                    // display the current date the empployee began
                                    Console.WriteLine("Current contract employee's start date : {0}", cEmployee.GetContractStartDate().ToShortDateString());

                                    // get the start date in which the contractor began work
                                    Console.WriteLine("Please enter the start date for the contracted employee <YYYY-MM-DD>:");
                                    result = cEmployee.SetContractStartDate(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please enter a valid date in which the employee was hired <YYYY-MM-DD>:");
                                        result = cEmployee.SetContractStopDate(Console.ReadLine());
                                    }
                                    break;
                                    // modify the fixed amount pay the contractor received
                                case "6":
                                    // display the current fixed amount pay the contractor received
                                    Console.WriteLine("Current contractor's fixed pay amount : {0}", cEmployee.GetFixedContractAmount());
                                    // get the contractor's fixed amount of pay
                                    Console.WriteLine("Please enter the contractor's fixed amount of pay (e.g. 4570.80):");
                                    while (true)
                                    {
                                        try
                                        {
                                            contractorsFixedAmount = float.Parse(Console.ReadLine());
                                            cEmployee.SetFixedContractAmount(contractorsFixedAmount);
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please re-enter a valid contractor's fixed amount of pay (e.g. 4570.80):");
                                        }
                                    }
                                    break;
                                    // modify the date in which the contractor ended work
                                case "7":
                                    // display the current date the empployee began
                                    Console.WriteLine("Current contract employee's stop date : {0}", cEmployee.GetContractStopDate().ToShortDateString());

                                    // get the date in which the contractor ended the work
                                    Console.WriteLine("Please enter the date the contractor ended \nworking for your company <YYYY-MM-DD>:");
                                    result = cEmployee.SetContractStopDate(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please re-enter the date the contractor \nended working for your company <YYYY-MM-DD>:");
                                        result = cEmployee.SetContractStopDate(Console.ReadLine());
                                    }
                                    break;
                                    // modify all the data in the current contract employee
                                case "8":
                                    Console.Clear();
                                    Console.WriteLine("Employee's current information:");
                                    cEmployee.Details();
                                    Console.WriteLine("\n");
                                    // if the user is updating all data for an employee remove the current one
                                    // and get them to update all fields by calling the add function
                                    databaseContainer.RemoveAt(foundElement);
                                    this.AddContractEmployee();
                                    break;

                            }
                            break;
                            // if we are modifying a seasonal employee
                            //
                            // option 5 modifies the season in which the employee was employed
                            // option 6 modifies the piece pay in which the employee received while employed
                            // option 8 modifies all the attributes
                        case "SN":
                            sEmployee = (SeasonalEmployee)baseEmployee;

                            switch (input)
                            {
                                    // modify the season
                                case "5":
                                    // display the season in which the employee was employed
                                    Console.WriteLine("Current employee's season of employment : {0}", sEmployee.GetSeason());
                                    // get the season in which the employee was employed
                                    Console.WriteLine("Please enter the season in which the employee was employed:");
                                    result = sEmployee.SetSeason(Console.ReadLine());
                                    while (result == false)
                                    {
                                        Console.WriteLine("Please re-enter a valid season in which the employee was employed:");
                                        result = sEmployee.SetSeason(Console.ReadLine());
                                    }
                                    break;
                                    // modify the piece pay
                                case "6":
                                    // display the season in which the employee was employed
                                    Console.WriteLine("Current employee's piece pay : {0}", sEmployee.GetPiecePay());

                                    // get the pay in which the employee received
                                    Console.WriteLine("Please enter the piece pay which the employee received for their work:");
                                    while (true)
                                    {
                                        try
                                        {
                                            piecePay = float.Parse(Console.ReadLine());
                                            sEmployee.SetPiecePay(piecePay);
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please re-enter a valid piece in which the employee received for their work:");
                                        }
                                    }
                                    break;
                                    // modify all attributes
                                case "8":
                                    Console.Clear();
                                    Console.WriteLine("Employee's current information:");
                                    sEmployee.Details();
                                    Console.WriteLine("\n");
                                    // if the user is updating all data for an employee remove the current one
                                    // and get them to update all fields by calling the add function
                                    databaseContainer.RemoveAt(foundElement);
                                    this.AddSeasonalEmployee();
                                    break;
                            }
                            break;
                    }/* End Switch */
                }/* End Else Statement*/
            }/* End While Loop*/


            return true;


        }

        /// <summary>
        /// Method Name: SaveDatabase.
        /// The purpose of this method is to parse the database into an output string to save to a file. This method
        /// ensures that all of the database items it is outputting are valid, else it will not ouptut them. The format
        /// in which a database item is outputted is as such:
        /// (Employee Type)|(Last Name)|(First Name)|(SIN)|(SubField1)|(SubField2)|(SubField3)|
        /// </summary>
        /// <returns></returns>
        public bool SaveDatabase()
        {
            String fileOutput;
            FullTimeEmployee ft = new FullTimeEmployee();
            PartTimeEmployee pt = new PartTimeEmployee();
            ContractEmployee c = new ContractEmployee();
            SeasonalEmployee s = new SeasonalEmployee();

            //Clear the file.
            FileStream db = databaseFile.Openfile("dbase.dtb", 'W');
            databaseFile.WriteToFile(db, "");
            databaseFile.CloseFile(db);

            //Insert a default header comment.
            fileOutput = ";\r\n;EMS Database File\r\n;Save Date: " + DateTime.Now.ToString() + "\r\n;Comments:\r\n;\r\n";

            //For each employee in the database.
            databaseContainer.ForEach(delegate(Employee entry)
            {
                //If the employee is valid.
                if (entry.Validate())
                {
                    //Parse each field for output.
                    fileOutput = fileOutput + entry.GetEmployeeType();
                    fileOutput = fileOutput + "|" + entry.GetLastName();
                    fileOutput = fileOutput + "|" + entry.GetFirstName();
                    fileOutput = fileOutput + "|" + entry.GetSocialNumber();
                    //If the date is 0
                    if (entry.GetDateOfBirth() == new DateTime(0))
                    {
                        //Output N/A
                        fileOutput = fileOutput + "|" + "N/A";
                    }
                    else
                    {
                        fileOutput = fileOutput + "|" + entry.GetDateOfBirth().Year + "-" + entry.GetDateOfBirth().Month + "-" + entry.GetDateOfBirth().Day;
                    }
                    //Get the employee type.
                    switch (entry.GetEmployeeType())
                    {
                        //If the employee is a Full Time employee.
                        case "FT":
                            ft = (FullTimeEmployee)entry;
                            //If the date is 0.
                            if (ft.GetDateOfHire() == new DateTime(0))
                            {
                                //Output N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + ft.GetDateOfHire().Year + "-" + ft.GetDateOfHire().Month + "-" + ft.GetDateOfHire().Day;
                            }
                            //If the date is 0.
                            if (ft.GetDateOfTermination() == new DateTime(0))
                            {
                                //Output N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + ft.GetDateOfTermination().Year + "-" + ft.GetDateOfTermination().Month + "-" + ft.GetDateOfTermination().Day;
                            }
                            fileOutput = fileOutput + "|" + ft.GetSalary();
                            break;
                        //If the employee is a Part Time employee.
                        case "PT":
                            pt = (PartTimeEmployee)entry;
                            //If the date is 0.
                            if (pt.GetDateOfHire() == new DateTime(0))
                            {
                                //Output N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + pt.GetDateOfHire().Year + "-" + pt.GetDateOfHire().Month + "-" + pt.GetDateOfHire().Day;
                            }
                            //If the date is 0.
                            if (pt.GetDateOfTermination() == new DateTime(0))
                            {
                                //Out N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + pt.GetDateOfTermination().Year + "-" + pt.GetDateOfTermination().Month + "-" + pt.GetDateOfTermination().Day;
                            }
                            fileOutput = fileOutput + "|" + pt.GetHourlyWage();
                            break;
                        //If the employee is a Contract Employee.
                        case "CT":
                            c = (ContractEmployee)entry;
                            //If the date is 0.
                            if (c.GetContractStartDate() == new DateTime(0))
                            {
                                //Output N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + c.GetContractStartDate().Year + "-" + c.GetContractStartDate().Month + "-" + c.GetContractStartDate().Day;
                            }
                            //If the date is 0.
                            if (c.GetContractStopDate() == new DateTime(0))
                            {
                                //Output N/A.
                                fileOutput = fileOutput + "|" + "N/A";
                            }
                            else
                            {
                                fileOutput = fileOutput + "|" + c.GetContractStopDate().Year + "-" + c.GetContractStartDate().Month + "-" + c.GetContractStartDate().Day;
                            }
                            fileOutput = fileOutput + "|" + c.GetFixedContractAmount();
                            break;
                        //The the employee is a Seasonal Employee.
                        case "SN":
                            s = (SeasonalEmployee)entry;
                            fileOutput = fileOutput + "|" + Enum.GetName(typeof(Season), (s.GetSeason()));
                            fileOutput = fileOutput + "|" + s.GetPiecePay();
                            break;
                    }
                    fileOutput = fileOutput + "|\r\n";
                }
            });
            //Write the parsed output string to a file.
            db = databaseFile.Openfile("dbase.dtb", 'W');
            databaseFile.WriteToFile(db, fileOutput);
            databaseFile.CloseFile(db);
            return true;
        }

        /// <summary>
        /// Method Name: LoadDatabase.
        /// The purpose of this method is to parse an input string from a file into objects to be added to the database. This method
        /// ensures that all of the database items it is creating are valid, else it will not create them. The format
        /// in which a database item is read in is as such:
        /// (Employee Type)|(Last Name)|(First Name)|(SIN)|(SubField1)|(SubField2)|(SubField3)|
        /// </summary>
        /// <returns>A boolean value of true upon completion.</returns>
        public bool LoadDatabase()
        {
            String fileInput = "";
            FullTimeEmployee ft = new FullTimeEmployee();
            PartTimeEmployee pt = new PartTimeEmployee();
            ContractEmployee c = new ContractEmployee();
            SeasonalEmployee s = new SeasonalEmployee();

            String currentType = "";
            String tempSin = "";
            Boolean sinValid = true;

            String[] objects;

            //Read in the data from the database file.
            FileStream db = databaseFile.Openfile("dbase.dtb", 'R');
            fileInput = databaseFile.ReadFromFile(db);
            databaseFile.CloseFile(db);

            //Remove null terminations and carrige returns.
            fileInput = fileInput.Replace("\0", "");
            fileInput = fileInput.Replace("\r", "");

            //Split up the fields for each new line.
            objects = fileInput.Split('\n');

            //Initialize an multi-array for each attribute.
            String[][] attributes = new String[objects.Length][];
            for (int k = 0; k < objects.Length; k++)
            {
                attributes[k] = new String[10];
            }

            //For each object to be entered into the database.
            for (int i = 0; i < objects.Length; i++)
            {
                //Reset the valid SIN flag to true.
                sinValid = true;

                //Split up the object into attributes.
                attributes[i] = objects[i].Split('|');

                //For each attribute in the current object.
                for (int j = 0; j < attributes[i].Length; j++)
                {
                    //If the current attribute is the employee identifier.
                    if (j == 0)
                    {
                        //Check what type of employee the object is.
                        switch (attributes[i][0])
                        {
                            //If the employee is a Full Time employee.
                            case "FT":
                                //If the SIN number is valid.
                                if (ft.CheckSinNumber(attributes[i][3]))
                                {
                                    //Reset the full Time employee object to house the attributes.
                                    ft = new FullTimeEmployee();
                                    //Insert the employee type into the current employee object.
                                    currentType = attributes[i][0];
                                }
                                else
                                {
                                    //Set the valid SIN flag to false.
                                    sinValid = false;
                                    //Break the current object's cycle of the for loop.
                                    j = attributes.Length;
                                }
                                break;
                            //If the employee is a Part Time employee.
                            case "PT":
                                //If the SIN number is valid.
                                if (ft.CheckSinNumber(attributes[i][3]))
                                {
                                    //Reset the Part Time employee object to house the attributes.
                                    pt = new PartTimeEmployee();
                                    //Insert the employee type into the current employee object.
                                    currentType = attributes[i][0];
                                }
                                else
                                {
                                    //Set the valid SIN flag to false.
                                    sinValid = false;
                                    //Break the current object's cycle of the for loop.
                                    j = attributes.Length;
                                }
                                break;
                            //If the employee is a Contract Employee.
                            case "CT":
                                //If the SIN number is valid.
                                if (ft.CheckSinNumber(attributes[i][3]))
                                {
                                    //Reset the Contract employee object to house the attributes.
                                    c = new ContractEmployee();
                                    //Insert the employee type into the current employee object.
                                    currentType = attributes[i][0];
                                }
                                else
                                {
                                    //Set the valid SIN flag to false.
                                    sinValid = false;
                                    //Break the current object's cycle of the for loop.
                                    j = attributes.Length;
                                }
                                break;
                            //If the employee is a Seasonal Employee.
                            case "SN":
                                //If the SIN number is valid.
                                if (ft.CheckSinNumber(attributes[i][3]))
                                {
                                    //Reset the Seasonal Employee object to house the attributes.
                                    s = new SeasonalEmployee();
                                    //Insert the employee type into the current employee object.
                                    currentType = attributes[i][0];
                                }
                                else
                                {
                                    //Set the valid SIN flag to false.
                                    sinValid = false;
                                    //Break the current object's cycle of the for loop.
                                    j = attributes.Length;
                                }
                                break;
                            //Not a valid employee type.
                            default:
                                //Nullify the current employee type.
                                currentType = "";
                                //Break the current object's cycle of the for loop.
                                j = attributes.Length;
                                break;
                        }
                    }
                    //If the current attribute is not the employee identifier.
                    else
                    {
                        //Check the current employee type.
                        switch (currentType)
                        {
                            //If the current employee is a Full Time employee
                            case "FT":
                                //Switch based the the current attribute being read in.
                                switch (j)
                                {
                                    //Set the first name.
                                    case 1:
                                        //Make sure there are no spaces.
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        ft.SetLastName(attributes[i][j]);
                                        break;
                                    //Set the last name.
                                    case 2:
                                        //Make sure there are no spaces.
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        ft.SetFirstName(attributes[i][j]);
                                        break;
                                    //Set the SIN number.
                                    case 3:
                                        if (ft.CheckSinNumber(attributes[i][j]))
                                        {
                                            //Make sure there are no spaces in the SIN number.
                                            attributes[i][j] = attributes[i][j].Replace(" ", "");
                                            ft.SetSocialNumber(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Birth.
                                    case 4:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            ft.SetDateOfBirth(new DateTime(0));
                                        }
                                        else
                                        {
                                            ft.SetDateOfBirth(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Hire.
                                    case 5:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            ft.SetDateOfHire(new DateTime(0));
                                        }
                                        else
                                        {
                                            ft.SetDateOfHire(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Termination.
                                    case 6:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            ft.SetDateOfTermination(new DateTime(0));
                                        }
                                        else
                                        {
                                            ft.SetDateOfTermination(attributes[i][j]);
                                        }
                                        break;
                                    //Set the current Salary.
                                    case 7:
                                        try
                                        {
                                            ft.SetSalary(float.Parse(attributes[i][j]));
                                        }
                                        catch
                                        {
                                        }
                                        break;
                                }
                                break;
                            //If the current employee is a Part Time employee.
                            case "PT":
                                //Switch based on the current attribute.
                                switch (j)
                                {
                                    //Set the first name. 
                                    case 1:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        pt.SetLastName(attributes[i][j]);
                                        break;
                                    //Set the last name.
                                    case 2:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        pt.SetFirstName(attributes[i][j]);
                                        break;
                                    //Set the sin number.
                                    case 3:
                                        if (pt.CheckSinNumber(attributes[i][j]))
                                        {
                                            attributes[i][j] = attributes[i][j].Replace(" ", "");
                                            pt.SetSocialNumber(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Birth.
                                    case 4:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            pt.SetDateOfBirth(new DateTime(0));
                                        }
                                        else
                                        {
                                            pt.SetDateOfBirth(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Hire.
                                    case 5:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            pt.SetDateOfHire(new DateTime(0));
                                        }
                                        else
                                        {
                                            pt.SetDateOfHire(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Termination.
                                    case 6:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            pt.SetDateOfTermination(new DateTime(0));
                                        }
                                        else
                                        {
                                            pt.SetDateOfTermination(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Hourly Rate.
                                    case 7:
                                        try
                                        {
                                            pt.SetHourlyRate(float.Parse(attributes[i][j]));
                                        }
                                        catch
                                        {
                                        }
                                        break;
                                }
                                break;
                            //If the current employee is a Contract Employee.
                            case "CT":
                                switch (j)
                                {
                                    //Set the last name.
                                    case 1:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        c.SetLastName(attributes[i][j]);
                                        break;
                                    //Set the first name.
                                    case 2:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        c.SetFirstName(attributes[i][j]);
                                        break;
                                    //Set the SIN number.
                                    case 3:
                                        tempSin = attributes[i][j];
                                        break;
                                    //Set the Date of Birth.
                                    case 4:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            c.SetDateOfBirth(new DateTime(0));
                                        }
                                        else
                                        {
                                            c.SetDateOfBirth(attributes[i][j]);
                                        }
                                        if (c.CheckSinNumber(tempSin))
                                        {
                                            tempSin = tempSin.Replace(" ", "");
                                            c.SetSocialNumber(tempSin);
                                        }
                                        break;
                                    //Set the Contract Start Date.
                                    case 5:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            c.SetContractStartDate(new DateTime(0));
                                        }
                                        else
                                        {
                                            c.SetContractStartDate(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Contract End Date.
                                    case 6:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            c.SetContractStartDate(new DateTime(0));
                                        }
                                        else
                                        {
                                            c.SetContractStopDate(attributes[i][j]);
                                        }
                                        break;
                                    case 7:
                                        try
                                        {
                                            c.SetFixedContractAmount(float.Parse(attributes[i][j]));
                                        }
                                        catch
                                        {
                                        }
                                        break;
                                }
                                break;
                            //If the current employee is a Seasonal Employee.
                            case "SN":
                                //Switch based on the current attribute.
                                switch (j)
                                {
                                    //Set the last name.
                                    case 1:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        s.SetLastName(attributes[i][j]);
                                        break;
                                    //Set the first name.
                                    case 2:
                                        attributes[i][j] = attributes[i][j].Replace(" ", "");
                                        s.SetFirstName(attributes[i][j]);
                                        break;
                                    //Set the SIN number.
                                    case 3:
                                        if (s.CheckSinNumber(attributes[i][j]))
                                        {
                                            attributes[i][j] = attributes[i][j].Replace(" ", "");
                                            s.SetSocialNumber(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Date of Birth.
                                    case 4:
                                        if (attributes[i][j] == "N/A")
                                        {
                                            s.SetDateOfBirth(new DateTime(0));
                                        }
                                        else
                                        {
                                            s.SetDateOfBirth(attributes[i][j]);
                                        }
                                        break;
                                    //Set the Season.
                                    case 5:
                                        s.SetSeason(attributes[i][j]);
                                        break;
                                    //Set the piece pay.
                                    case 6:
                                        try
                                        {
                                            s.SetPiecePay(float.Parse(attributes[i][j]));
                                        }
                                        catch
                                        {
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
                //If the valid SIN flag was not invalidated.
                if (sinValid)
                {
                    //Check the current employee type.
                    switch (currentType)
                    {
                        //If the current employee is a Full Time Employee.
                        case "FT":
                            //Validate the full time employee.
                            if (ft.Validate())
                            {
                                //Add the employee to the database.
                                databaseContainer.Add(ft);
                            }
                            break;
                        //If the current employee is a Part Time employee.
                        case "PT":
                            //Validate the part tiem employee.
                            if (pt.Validate())
                            {
                                //Add the employee to the database.
                                databaseContainer.Add(pt);
                            }
                            break;
                        //If the current employee is a Contract employee.
                        case "CT":
                            //Validate the COntract employee.
                            if (c.Validate())
                            {
                                //Add the employee to the database.
                                databaseContainer.Add(c);
                            }
                            break;
                        //If the current employee is a Seasonal Employee.
                        case "SN":
                            //Validate the seasonal Employee.
                            if (s.Validate())
                            {
                                //Add the employee to the database.
                                databaseContainer.Add(s);
                            }
                            break;
                    }
                }
            }
            return true;
        }

    }
}
