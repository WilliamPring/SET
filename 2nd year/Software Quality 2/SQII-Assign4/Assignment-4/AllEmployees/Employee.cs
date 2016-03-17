using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Supporting;

namespace AllEmployees
{
    public class Employee
    {
        private String firstName;
        private String lastName;
        private String socialInsuranceNumber;
        private DateTime dateOfBirth;
        public Logging logfile;
        private String employeeType;

        /// <summary>
        /// Constructor that takes in no parameters. Initializes all attributes
        /// to default values.
        /// </summary>
        public Employee()
        {
            logfile = new Logging();
            SetFirstName("");
            SetLastName("");
            SetSocialNumber("000000000");
            SetDateOfBirth(new DateTime(0));
        }

        /// <summary>
        /// Constructor that takes in firstName and lastName as parameters.
        /// Intializes all other attributes to their default values.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        public Employee(String firstName, String lastName)
        {
            logfile = new Logging();
            SetFirstName("");
            SetLastName("");
            SetSocialNumber("000000000");
            SetDateOfBirth(new DateTime(0));
        }
        
        /// <summary>
        /// Constructor that takes all Employee attributes as parameters.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        /// <param name="date">Value for dateOfBirth. String.</param>
        /// <param name="socialInsNum">Value for socialinsuranceNumber. String.</param>
        public Employee(String firstName, String lastName, String date, String socialInsNum)
        {
            logfile = new Logging();
            SetFirstName(firstName);
            SetLastName(lastName);
            SetSocialNumber(socialInsNum);
            SetDateOfBirth(date);
        }

        /// <summary>
        /// Accessor for firstName.
        /// </summary>
        /// <returns>The value of firstName. String.</returns>
        public String GetFirstName()
        {
            return this.firstName;
        }

        /// <summary>
        /// Accessor for lastName.
        /// </summary>
        /// <returns>The value of lastName. String.</returns>
        public String GetLastName()
        {
            return this.lastName;
        }

        /// <summary>
        /// Accessor for socialInsuranceNumber.
        /// </summary>
        /// <returns>The value of socialInsuranceNumber. Int.</returns>
        public String GetSocialNumber()
        {
            return this.socialInsuranceNumber;
        }

        /// <summary>
        /// Accessor for dateOfBirth.
        /// </summary>
        /// <returns>The value of dateOfBirth. DateTime.</returns>
        public DateTime GetDateOfBirth()
        {
            return this.dateOfBirth;
        }

        /// <summary>
        /// Accessor for employeeType.
        /// </summary>
        /// <returns>The value of employeeType. String.</returns>
        public String GetEmployeeType()
        {
            return this.employeeType;
        }

        /// <summary>
        /// Mutator for firstName.
        /// </summary>
        /// <param name="firstName">Value for firstName.</param>
        /// <returns>A boolean value indicating whether or not the paramater was valid.</returns>
        public bool SetFirstName(String firstName)
        {
            if (this.employeeType != "CT")
            {
                // check the validity of the string
                if (CheckString(firstName) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', firstName);
                    return false;
                }
                else
                {
                    this.firstName = firstName;
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', firstName);
                }
            }
                return true;
        }

        /// <summary>
        /// Mutator for lastName.
        /// </summary>
        /// <param name="lastName">Value for lastName.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetLastName(String lastName)
        {
            // check the validity of the string
            if( CheckString(lastName) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', lastName);
                return false;
            }
            else
            {
                this.lastName = lastName;
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', lastName);
            }
            return true;
        }

        /// <summary>
        /// Mutator for employeeType.
        /// </summary>
        /// <param name="employeeType">Value for employeeType.</param>
        /// <returns>A boolean value indicating wether or not the parameter was valid.</returns>
        public bool SetEmployeeType(String employeeType)
        {
            this.employeeType = employeeType;
            return true;
        }

        /// <summary>
        /// Mutator for dateOfBirth.
        /// </summary>
        /// <param name="date">Value for dateOfBirth.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfBirth(String date)
        {
            try
            {
                if(date != "0")
                {
                    if (CheckDate(DateTime.Parse(date), new DateTime(0), DateTime.Now) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                        return false;
                    }
                    else
                    {
                        this.dateOfBirth = DateTime.Parse(date);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date);
                    }
                    return true;
                }
                else
                {
                    if (CheckDate(new DateTime(0), new DateTime(0), DateTime.Now) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', (new DateTime(0)).ToShortDateString());
                        return false;
                    }
                    else
                    {
                        this.dateOfBirth = new DateTime(0);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', (new DateTime(0)).ToShortDateString());
                    }
                    return true;
                }
            }
            catch
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                return false;
            }
        }

        /// <summary>
        /// Overloaded Mutator for dateOfBirth.
        /// </summary>
        /// <param name="date">Value for dateOfBirth.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfBirth(DateTime date)
        {
            try
            {
                if (CheckDate(date, new DateTime(0), DateTime.Now) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                    return false;
                }
                else
                {
                    this.dateOfBirth = date;
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date.ToShortDateString());
                }
                return true;
            }
            catch
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                return false;
            }
        }

        /// <summary>
        /// Mutator for socialinsuranceNumber.
        /// </summary>
        /// <param name="socialInsNum">Value for socialInsuranceNumber</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetSocialNumber(String socialInsNum)
        {
            if (CheckSinNumber(socialInsNum) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', socialInsNum);
                return false;
            }
            else
            {
                socialInsNum = socialInsNum.PadLeft(9, '0');
                this.socialInsuranceNumber = socialInsNum;
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', socialInsNum);
            }
            return true;
        }

        /// <summary>
        /// Validates all attributes of the employee class.
        /// </summary>
        /// <returns>A boolean value indicating whether or not all attributes are valid.</returns>
        public virtual bool Validate()
        {
            bool valid = true;
            // check the first name
            if (this.employeeType != "CT")
            {
                if (CheckString(this.firstName) == false || this.firstName == "" || this.firstName == null)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.firstName);
                    valid = false;
                }
                else
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.firstName);
                }
            }
            else
            {
                if (this.firstName != null && firstName != "")
                {
                    if (CheckString(this.firstName) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.firstName);
                        valid = false;
                    }
                    else
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.firstName);
                    }
                }
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.firstName);
            }
            // check the last name
            if (CheckString(this.lastName) == false || this.lastName == "")
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.lastName);
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.lastName);
            }
            // check the sin number
            if (CheckSinNumber(this.socialInsuranceNumber) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.socialInsuranceNumber);
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.socialInsuranceNumber);
            }

            //check dateOfBirth
            if (CheckDate(this.dateOfBirth, new DateTime(0), DateTime.Now) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.dateOfBirth.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.dateOfBirth.ToShortDateString());
            }

            return valid;
        }


        /// <summary>
        /// Validates a string to ensure all characters are letters.
        /// </summary>
        /// <param name="testString">The string ot be validated.</param>
        /// <returns>A boolean value indicating whether or not the string is valid.</returns>
        public virtual bool CheckString(String testString)
        {
            for(int i = 0; i < testString.Length; i++)
            {
                if (testString[i] != '\'' && testString[i] != '-')
                {
                    if (!Char.IsLetter(testString[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Validates a SIN number. Uses method from http://www.ryerson.ca/JavaScript/lectures/forms/textValidation/sinProject.html
        /// </summary>
        /// <param name="sinNumber">The SIN number to be validated.</param>
        /// <returns>A boolean value indicating whether or not the SIN Number is valid.</returns>
        public virtual bool CheckSinNumber(String sinNumber)
        {
            int[] sinNumbers = new int[9];
            int total = 0;
            int temp = 0;

            sinNumber = sinNumber.Replace(" ", "");

            if (sinNumber.Length <= 9)
            {
                String tempString = sinNumber.PadLeft(9, '0');
                for (int i = 0; i < tempString.Length; i++)
                {
                    sinNumbers[i] = (int)Char.GetNumericValue(tempString[i]);
                }

                sinNumbers[1] = sinNumbers[1] * 2;
                sinNumbers[3] = sinNumbers[3] * 2;
                sinNumbers[5] = sinNumbers[5] * 2;
                sinNumbers[7] = sinNumbers[7] * 2;

                total += (sinNumbers[1] / 10);
                total += (sinNumbers[1] % 10);
                total += (sinNumbers[3] / 10);
                total += (sinNumbers[3] % 10);
                total += (sinNumbers[5] / 10);
                total += (sinNumbers[5] % 10);
                total += (sinNumbers[7] / 10);
                total += (sinNumbers[7] % 10);

                total += sinNumbers[0] + sinNumbers[2] + sinNumbers[4] + sinNumbers[6];

                if (total % 10 == 0)
                {
                    if (sinNumbers[8] == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    temp = ((total / 10) + 1) * 10;
                    total = temp - total;
                    if (total == sinNumbers[8])
                    {
                        return true;
                    }
                }
            }

            return false;

        }

       /// <summary>
       /// Validates a date. Ensuring it falls between the specified minimum and maximum date.
       /// </summary>
       /// <param name="testDate">The date to be validated.</param>
       /// <param name="minDate">The minimum allowed value for the date to be valid.</param>
       /// <param name="maxDate">The maximum allowed value for the date to be valid.</param>
       /// <returns>A boolean value indicating wether or not the date is valid.</returns>
        public bool CheckDate(DateTime testDate, DateTime minDate, DateTime maxDate)
        {
            if (testDate == new DateTime(0))
            {
                return true;
            }
            if (maxDate > minDate)
            {
                if (testDate < minDate || testDate > maxDate)
                {
                    return false;
                }
            }
            else
            {
                if (testDate < minDate)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Output all of the Employee attribute values to the console.
        /// </summary>
        public virtual void Details()
        {
            String outSin = "";
            switch (this.employeeType)
            {
                case "FT":
                    Console.WriteLine("Employee Type: Full Time");
                    break;
                case "PT":
                    Console.WriteLine("Employee Type: Part Time");
                    break;
                case "CT":
                    Console.WriteLine("Employee Type: Contract");
                    break;
                case "SN":
                    Console.WriteLine("Employee Type: Seasonal");
                    break;
            }
            if (this.employeeType == "CT")
            {
                Console.WriteLine("Business Name: {0}", this.lastName);
            }
            else
            {
                Console.WriteLine("Last Name: {0}", this.lastName);
                Console.WriteLine("First Name: {0}", this.firstName);
            }
            try
            {
                if (this.employeeType != "CT")
                {
                    outSin = this.socialInsuranceNumber.Insert(3, " ");
                    outSin = outSin.Insert(7, " ");
                }
                else
                {
                    outSin = this.socialInsuranceNumber.Insert(5, " ");
                }
            }
            catch
            {
            }
            Console.WriteLine("SIN #: {0}", outSin);
            if (this.dateOfBirth == new DateTime(0))
            {
                Console.WriteLine("Date of Birth: N/A");
            }
            else
            {
                Console.WriteLine("Date of Birth: {0}", this.dateOfBirth.Year + "-" + this.dateOfBirth.Month + "-" + this.dateOfBirth.Day);
            }
            /***TODO: Add Logging Operation***/
        }
    }
}
