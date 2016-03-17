using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Supporting;

public enum Season
{
    Winter,
    Spring,
    Summer,
    Fall
};

namespace AllEmployees
{
    public class SeasonalEmployee : Employee
    {
        private Season season;
        private float piecePay;

        /// <summary>
        /// Constructor that takes no parameters.
        /// </summary>
        public SeasonalEmployee()
            :base()
        {
            SetSeason("Winter");
            SetPiecePay(0);
            SetEmployeeType("SN");
        }

        /// <summary>
        /// Constructor that takes firstName and lastName as parameters.
        /// </summary>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        public SeasonalEmployee(String firstName, String lastName)
            :base (firstName, lastName)
        {
            SetSeason("Winter");
            SetPiecePay(0);
            SetEmployeeType("SN");
        }

        /// <summary>
        /// Constructor that takes all SeasonalEmployee attributes as parameters.
        /// </summary>
        /// <param name="season">Value for season. String.</param>
        /// <param name="piecePay">Value for piecePay. Float.</param>
        /// <param name="firstName">Value for firstName. String.</param>
        /// <param name="lastName">Value for lastName. String.</param>
        /// <param name="date">Value for dateOfBirth. String.</param>
        /// <param name="socialInsNum">Value for socialinsuranceNumber. Integer.</param>
        public SeasonalEmployee(String season, float piecePay, String firstName, String lastName, String date, String socialInsNum) 
            : base(firstName, lastName, date, socialInsNum)
        {
            SetSeason(season);
            SetPiecePay(piecePay);
            SetEmployeeType("SN");
        }

        /// <summary>
        /// Accessor for season.
        /// </summary>
        /// <returns>The value of season.</returns>
        public Season GetSeason()
        {
            return this.season;
        }

        /// <summary>
        /// Accessor for piecePay.
        /// </summary>
        /// <returns>The value of piecePay.</returns>
        public float GetPiecePay()
        {
            return this.piecePay;
        }

        /// <summary>
        /// Mutator for season.
        /// </summary>
        /// <param name="season">Value for season.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetSeason(String season)
        {
            if (CheckSeason(season) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', season);
                return false;
            }
            else
            {
                season = season.ToLower();
                switch (season)
                {
                    case "winter":
                        this.season = Season.Winter;
                        break;
                    case "spring":
                        this.season = Season.Spring;
                        break;
                    case "summer":
                        this.season = Season.Summer;
                        break;
                    case "fall":
                        this.season = Season.Fall;
                        break;
                }
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', season);
            }
            return true;
        }

        /// <summary>
        /// Mutator for piecePay.
        /// </summary>
        /// <param name="pP">Value for piecePay.</param>
        /// <returns>A boolean value indicating whether or not the parameter was valid.</returns>
        public bool SetPiecePay(float pP)
        {
            if (CheckPiecePay(pP) == false)
            {

                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'F', piecePay.ToString());
                return false;
            }
            else
            {
                this.piecePay = pP;
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'T', 'S', piecePay.ToString());
            }
            return true;
        }

        public bool CheckSeason(String season)
        {
            season = season.ToLower();
            if (season != "winter" && season != "spring" && season != "summer" && season != "fall")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates a piecePay to ensure it it not a negative value.
        /// </summary>
        /// <param name="pP">The piecePay to be validated.</param>
        /// <returns>A boolean value indicating wether or not the fixedContractAmount is valid.</returns>
        public bool CheckPiecePay(float pP)
        {
            if (pP < 0)
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

            if (CheckSeason(Enum.GetName(typeof(Season), this.season)) == false)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', Enum.GetName(typeof(Season),this.season));
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', Enum.GetName(typeof(Season), this.season));
            }

            if (CheckPiecePay(this.piecePay) == false || piecePay == 0)
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'F', piecePay.ToString());
                valid = false;
            }
            else
            {
                logfile.Log(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name, 'V', 'S', piecePay.ToString());
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
            Console.WriteLine("Season: {0}", this.season);
            Console.WriteLine("Piece Pay: {0}", this.piecePay);
            /***TODO: Add Logging Operation***/
        }
    }
}
