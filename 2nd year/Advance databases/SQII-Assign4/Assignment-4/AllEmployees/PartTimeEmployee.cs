using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Supporting;

namespace AllEmployees
{
    public class PartTimeEmployee : Employee
    {
        private DateTime dateOfHire;
        private DateTime dateOfTermination;
        private float hourlyRate;

        /// <summary>
        /// Constructor that takes no parameters.
        /// </summary>
        public PartTimeEmployee()
            :base()
        {
            SetDateOfHire("0");
            SetDateOfTermination("0");
            SetHourlyRate(0);
            SetEmployeeType("PT");
        }

        /// <summary>
        /// Constructor that takes firstName and lastName as parameters.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        public PartTimeEmployee(String firstName, String lastName)
            :base (firstName, lastName)
        {
            SetDateOfHire("0");
            SetDateOfTermination("0");
            SetHourlyRate(0);
            SetEmployeeType("PT");
        }

        /// <summary>
        /// Constructor that takes all PartTimeEmployee attributes as parameters.
        /// </summary>
        /// <param name="dateOfHire">Value for dateOfHire. String.</param>
        /// <param name="dateOfTermination">Value for dateOfTermination. String.</param>
        /// <param name="hourlyRate">Value for hourlyRate. Float.</param>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        /// <param name="date">Value for dateOfBirth. String.</param>
        /// <param name="socialInsNum">Value for socialinsuranceNumber. String.</param>
        public PartTimeEmployee(String dateOfHire, String dateOfTermination, float hourlyRate, String firstName, String lastName, String date, String socialInsNum) 
            : base(firstName, lastName, date, socialInsNum)
        {
            SetDateOfHire(dateOfHire);
            SetDateOfTermination(dateOfTermination);
            SetHourlyRate(hourlyRate);
            SetEmployeeType("PT");
        }

        /// <summary>
        /// Accessor for dateOfHire.
        /// </summary>
        /// <returns>The value of dateOfHire.</returns>
        public DateTime GetDateOfHire()
        {
            return this.dateOfHire;
        }

        /// <summary>
        /// Accessor for dateOfTermination.
        /// </summary>
        /// <returns>The value of dateOfTermination.</returns>
        public DateTime GetDateOfTermination()
        {
            return this.dateOfTermination;
        }

        /// <summary>
        /// Accessor for hourlyRate.
        /// </summary>
        /// <returns>The value of hourlyRate.</returns>
        public float GetHourlyWage()
        {
            return this.hourlyRate;
        }

        /// <summary>
        /// Mutator for dateOfHire.
        /// </summary>
        /// <param name="date">Value for dateOfHire.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfHire(String date)
        {
            try
            {
                if(date != "0")
                {
                    if (CheckDate(DateTime.Parse(date), base.GetDateOfBirth(), this.dateOfTermination) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                        return false;
                    }
                    else
                    {
                        this.dateOfHire = DateTime.Parse(date);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date);
                    }
                    return true;
                    }
                else
                {
                    if (CheckDate(new DateTime(0), base.GetDateOfBirth(), this.dateOfTermination) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', (new DateTime(0)).ToShortDateString());
                        return false;
                    }
                    else
                    {
                        this.dateOfHire = new DateTime(0);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', (new DateTime(0)).ToShortDateString());
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Overloaded Mutator for dateOfHire.
        /// </summary>
        /// <param name="date">Value for dateOfHire.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfHire(DateTime date)
        {
            try
            {
                if (CheckDate(date, base.GetDateOfBirth(), this.dateOfTermination) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                    return false;
                }
                else
                {
                    this.dateOfHire = date;
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date.ToShortDateString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Mutator for dateOfTermination.
        /// </summary>
        /// <param name="date">Value for dateOfTermination.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfTermination(String date)
        {
            try
            {
                if(date != "0")
                {
                    if (CheckDate(DateTime.Parse(date), this.dateOfHire, DateTime.Now) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                        return false;
                    }
                    else
                    {
                        this.dateOfTermination = DateTime.Parse(date);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date);
                    }
                    return true;
                }
                else
                {
                    if (CheckDate(new DateTime(0), this.dateOfHire, DateTime.Now) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', (new DateTime(0)).ToShortDateString());
                        return false;
                    }
                    else
                    {
                        this.dateOfTermination = new DateTime(0);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', (new DateTime(0)).ToShortDateString());
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Mutator for dateOfTermination.
        /// </summary>
        /// <param name="date">Value for dateOfTermination.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetDateOfTermination(DateTime date)
        {
            try
            {
                if (CheckDate(date, this.dateOfHire, DateTime.Now) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                    return false;
                }
                else
                {
                    this.dateOfTermination = date;
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date.ToShortDateString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Mutator for hourlyRate.
        /// </summary>
        /// <param name="hR">Value for hourlyRate.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetHourlyRate(float hR)
        {
            if (CheckHourlyRate(hR) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', hR.ToString());
                return false;
            }
            else
            {
                this.hourlyRate = hR;
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', hR.ToString());
            }
            return true;
        }

        /// <summary>
        /// Validates a salary to ensure it it not a negative value.
        /// </summary>
        /// <param name="hR">The salary to be validated.</param>
        /// <returns>A boolean value indicating wether or not the salary is valid.</returns>
        public bool CheckHourlyRate(float hR)
        {
            if (hR < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate all attributes of the employee type class.
        /// </summary>
        /// <returns>Boolean Value indicating success or failure of validation.</returns>
        public override bool Validate()
        {
            bool valid = true;

            if (CheckDate(this.dateOfHire, base.GetDateOfBirth(), this.dateOfTermination) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.dateOfHire.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.dateOfHire.ToShortDateString());
            }

            /***TODO: Add Logging Operation***/
            if (CheckDate(this.dateOfTermination, this.dateOfHire, DateTime.Now) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.dateOfTermination.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.dateOfTermination.ToShortDateString());
            }

            /***TODO: Add Logging Operation***/
            if (CheckHourlyRate(hourlyRate) == false || hourlyRate == 0)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.hourlyRate.ToString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.hourlyRate.ToString());
            }
            valid = base.Validate();

            return valid;
        }

        /// <summary>
        /// Output all of the PartTimeEmployee attribute values to the console.
        /// </summary>
        public override void Details()
        {
            base.Details();
            if (this.dateOfHire == new DateTime(0))
            {
                Console.WriteLine("Date of Hire: N/A");
            }
            else
            {
                Console.WriteLine("Date of Hire: {0}", this.dateOfHire.Year + "-" + this.dateOfHire.Month + "-" + this.dateOfHire.Day);
            }
            if (this.dateOfTermination == new DateTime(0))
            {
                Console.WriteLine("Date of Termination: N/A");
            }
            else
            {
                Console.WriteLine("Date of Termination: {0}", this.dateOfTermination.Year + "-" + this.dateOfHire.Month + "-" + this.dateOfHire.Day);
            }
            Console.WriteLine("Hourly Rate: {0}", this.hourlyRate);
            /***TODO: Add Logging Operation***/
        }
    }
}
