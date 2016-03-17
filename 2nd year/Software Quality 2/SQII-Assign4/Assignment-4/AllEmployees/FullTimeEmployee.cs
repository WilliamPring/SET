using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Supporting;

namespace AllEmployees
{
    public class FullTimeEmployee : Employee
    {
        private DateTime dateOfHire;
        private DateTime dateOfTermination;
        private float salary;

        /// <summary>
        /// Constructor that takes no parameters.
        /// </summary>
        public FullTimeEmployee()
            :base()
        {
            SetDateOfHire(new DateTime(0));
            SetDateOfTermination(new DateTime(0));
            SetSalary(0);
            SetEmployeeType("FT");
        }

        /// <summary>
        /// Constructor that takes firstName and lastName as parameters.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        public FullTimeEmployee(String firstName, String lastName)
            :base (firstName, lastName)
        {
            SetDateOfHire("0");
            SetDateOfTermination("0");
            SetSalary(0);
            SetEmployeeType("FT");
        }

        /// <summary>
        /// Constructor that takes all FullTimeEmployee attributes as parameters.
        /// </summary>
        /// <param name="dateOfHire">Value for dateOfHire. String.</param>
        /// <param name="dateOfTermination">Value for dateOfTermination. String.</param>
        /// <param name="salary">Value for salary. Float.</param>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        /// <param name="date">Value for dateOfBirth. String.</param>
        /// <param name="socialInsNum">Value for socialinsuranceNumber. String.</param>
        public FullTimeEmployee(String dateOfHire, String dateOfTermination, float salary, String firstName, String lastName, String date, String socialInsNum) 
            : base(firstName, lastName, date, socialInsNum)
        {
            SetDateOfHire(dateOfHire);
            SetDateOfTermination(dateOfTermination);
            SetSalary(salary);
            SetEmployeeType("FT");
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
        /// Accessor for salary.
        /// </summary>
        /// <returns>The value of salary.</returns>
        public float GetSalary()
        {
            return this.salary;
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
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
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
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
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
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                return false;
            }
        }

        /// <summary>
        /// Overloaded Mutator for dateOfTermination.
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
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                return false;
            }
        }

        /// <summary>
        /// Mutator for salary.
        /// </summary>
        /// <param name="sal">Value for salary.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetSalary(float sal)
        {
            if (CheckSalary(sal) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', sal.ToString());
                return false;
            }
            else
            {
                this.salary = sal;
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', sal.ToString());
            }
            return true;
        }

        /// <summary>
        /// Validates a salary to ensure it it not a negative value.
        /// </summary>
        /// <param name="sal">The salary to be validated.</param>
        /// <returns>A boolean value indicating wether or not the salary is valid.</returns>
        public bool CheckSalary(float sal)
        {
            if (sal < 0)
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

            if (CheckDate(this.dateOfTermination, this.dateOfHire, DateTime.Now) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.dateOfTermination.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.dateOfTermination.ToShortDateString());
            }

            if (CheckSalary(salary) == false || salary == 0)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', salary.ToString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', salary.ToString());
            }
            valid = base.Validate();

            return valid;
        }

        /// <summary>
        /// Output all of the FullTimeEmployee attribute values to the console.
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
            Console.WriteLine("Salary: {0}", this.salary);
            /***TODO: Add Logging Operation***/
        }

    }
}
