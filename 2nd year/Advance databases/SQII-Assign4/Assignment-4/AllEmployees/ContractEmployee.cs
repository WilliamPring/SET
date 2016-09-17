using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Supporting;

namespace AllEmployees
{
    public class ContractEmployee : Employee
    {
        private DateTime contractStartDate;
        private DateTime contractStopDate;
        private float fixedContractAmount;

        /// <summary>
        /// Constructor that takes no parameters.
        /// </summary>
        public ContractEmployee()
            :base()
        {
            SetContractStartDate("0");
            SetContractStopDate("0");
            SetFixedContractAmount(0);
            SetEmployeeType("CT");
        }

        /// <summary>
        /// Constructor that takes firstName and lastName as parameters.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        public ContractEmployee(String firstName, String lastName)
            :base (firstName, lastName)
        {
            SetContractStartDate("0");
            SetContractStopDate("0");
            SetFixedContractAmount(0);
            SetEmployeeType("CT");
        }

        /// <summary>
        /// Constructor that takes all ContractEmployee attributes as parameters.
        /// </summary>
        /// <param name="contractStartDate">Value for contractStartDate. String.</param>
        /// <param name="contractStopDate">Value for contractStopDate. String.</param>
        /// <param name="fixedContractAmount">Value for fixedContractAmount. Float.</param>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        /// <param name="date">Value for dateOfBirth. String.</param>
        /// <param name="socialInsNum">Value for socialinsuranceNumber. String.</param>
        public ContractEmployee(String contractStartDate, String contractStopDate, float fixedContractAmount, String firstName, String lastName, String date, String socialInsNum) 
            : base(firstName, lastName, date, socialInsNum)
        {
            SetContractStartDate(contractStartDate);
            SetContractStopDate(contractStopDate);
            SetFixedContractAmount(fixedContractAmount);
            SetEmployeeType("CT");
        }

        /// <summary>
        /// Accessor for contractStartDate.
        /// </summary>
        /// <returns>The value of contractStartDate.</returns>
        public DateTime GetContractStartDate()
        {
            return this.contractStartDate;
        }

        /// <summary>
        /// Accessor for contractStopDate.
        /// </summary>
        /// <returns>The value of contractStopDate.</returns>
        public DateTime GetContractStopDate()
        {
            return this.contractStartDate;
        }

        /// <summary>
        /// Accessor for fixedContractAmount.
        /// </summary>
        /// <returns>The value of fixedContractAmount.</returns>
        public float GetFixedContractAmount()
        {
            return this.fixedContractAmount;
        }

        /// <summary>
        /// Mutator for contractStartDate.
        /// </summary>
        /// <param name="date">Value for contractStartDate.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetContractStartDate(String date)
        {
            try
            {
                if(date != "0")
                {
                    if (CheckDate(DateTime.Parse(date), base.GetDateOfBirth(), this.contractStopDate) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                        return false;
                    }
                    else
                    {
                        this.contractStartDate = DateTime.Parse(date);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date);
                    }
                    return true;
                }
                else
                {
                    if (CheckDate(new DateTime(0), base.GetDateOfBirth(), this.contractStopDate) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', (new DateTime(0)).ToShortDateString());
                        return false;
                    }
                    else
                    {
                        this.contractStartDate = new DateTime(0);
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
        /// Overloaded Mutator for contractStartDate.
        /// </summary>
        /// <param name="date">Value for contractStartDate.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetContractStartDate(DateTime date)
        {
            try
            {
                if (CheckDate(date, base.GetDateOfBirth(), this.contractStopDate) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                    return false;
                }
                else
                {
                    this.contractStartDate = date;
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
        /// Mutator for contractStopDate.
        /// </summary>
        /// <param name="date">Value for contractStopDate.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetContractStopDate(String date)
        {
            try
            {
                if(date != "0")
                {
                    if (CheckDate(DateTime.Parse(date), this.contractStartDate, new DateTime(0)) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date);
                        return false;
                    }
                    else
                    {
                        this.contractStopDate = DateTime.Parse(date);
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', date);
                    }
                    return true;
                }
                else
                {
                    if (CheckDate(new DateTime(0), this.contractStartDate, new DateTime(0)) == false)
                    {
                        logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', (new DateTime(0)).ToShortDateString());
                        return false;
                    }
                    else
                    {
                        this.contractStopDate = new DateTime(0);
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
        /// Overloaded Mutator for contractStopDate.
        /// </summary>
        /// <param name="date">Value for contractStopDate.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetContractStopDate(DateTime date)
        {
            try
            {
                if (CheckDate(date, this.contractStartDate, new DateTime(0)) == false)
                {
                    logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', date.ToShortDateString());
                    return false;
                }
                else
                {
                    this.contractStopDate = date;
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
        /// Mutator for fixedContractAmount.
        /// </summary>
        /// <param name="fA">Value for fixedContractAmount.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetFixedContractAmount(float fA)
        {
            if (CheckFixedContractAmount(fA) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', fA.ToString());
                return false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', fA.ToString());
                this.fixedContractAmount = fA;
            }
            return true;
        }

        /// <summary>
        /// Validates a business name to ensure all characters are letters spaces or periods.
        /// </summary>
        /// <param name="testString">The string ot be validated.</param>
        /// <returns>A boolean value indicating whether or not the string is valid.</returns>
        public override bool CheckString(String testString)
        {
            try
            {
                testString = testString.Replace(".", "");
                testString = testString.Replace(" ", "");
                if (testString == "")
                {
                    return false;
                }
                for (int i = 0; i < testString.Length; i++)
                {
                    if (testString[i] != '\'' && testString[i] != '-')
                    {
                        if (!Char.IsLetter(testString[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates a fixedContractAmount to ensure it it not a negative value.
        /// </summary>
        /// <param name="fA">The fixedContractAmount to be validated.</param>
        /// <returns>A boolean value indicating wether or not the fixedContractAmount is valid.</returns>
        public bool CheckFixedContractAmount(float fA)
        {
            if (fA < 0)
            {
                return false;
            }
            return true;
        }

        public override bool CheckSinNumber(string sinNumber)
        {
            String tempString = sinNumber.Replace(" ", "");
            tempString = tempString.PadLeft(9, '0');

            if (tempString != "000000000")
            {
                if (tempString[0] != GetDateOfBirth().ToShortDateString()[GetDateOfBirth().ToShortDateString().Length - 2] || tempString[1] != GetDateOfBirth().ToShortDateString()[GetDateOfBirth().ToShortDateString().Length - 1])
                {
                    return false;
                }
            }
            if (!base.CheckSinNumber(sinNumber))
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

            if (CheckDate(this.contractStartDate, base.GetDateOfBirth(), this.contractStopDate) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.contractStartDate.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.contractStartDate.ToShortDateString());
            }

            if (CheckDate(this.contractStopDate, this.contractStartDate, new DateTime(0)) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.contractStopDate.ToShortDateString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.contractStopDate.ToShortDateString());
            }

            if (CheckFixedContractAmount(this.fixedContractAmount) == false || fixedContractAmount == 0)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', this.fixedContractAmount.ToString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', this.fixedContractAmount.ToString());
            }
            valid = base.Validate();

            return valid;
        }

        /// <summary>
        /// Output all of the ContractEmployee attribute values to the console.
        /// </summary>
        public override void Details()
        {
            base.Details();
            if (this.contractStartDate == new DateTime(0))
            {
                Console.WriteLine("Contract Start Date: N/A");
            }
            else
            {
                Console.WriteLine("Contract Start Date: {0}", this.contractStartDate.Year + "-" + this.contractStartDate.Month + "-" + this.contractStartDate.Day);
            }
            if (this.contractStopDate == new DateTime(0))
            {
                Console.WriteLine("Contract Stop Date: N/A");
            }
            else
            {
                Console.WriteLine("Contract Stop Date: {0}", this.contractStopDate.Year + "-" + this.contractStopDate.Month + "-" + this.contractStopDate.Day);
            }
            Console.WriteLine("Fixed Contract Amount: {0}", this.fixedContractAmount);
        }
    }
}
