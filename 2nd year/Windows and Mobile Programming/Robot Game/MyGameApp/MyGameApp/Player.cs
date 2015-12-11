using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameApp
{
    class Player
    {
        string UserName;
        int Time;

        /** Constructor **/

        public Player() { }

        public Player(string UserName, int Time)
        {
            this.UserName = UserName;
            this.Time = Time;
        }

        /** Properties **/

        public string GetUserName
        {
            get
            {
                return UserName;
            }

        }

        public int GetTime
        {
            get
            {
                return Time;
            }
        }
    }
}
