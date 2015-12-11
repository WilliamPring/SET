/*
File name: Player.cs
Project: Windows 10 universal Application
By: William Pring and Naween Mehanmal
Date: 
Description: This is the class for the players
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameApp
{

    /*
   Name: class Player
      Purpose: Elements and information that our player will have
      */
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
