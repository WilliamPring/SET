///<File Header>
///This code has been borrowed (with permission) from Dan Morrison, Dave Wagler and Matt Sindall [March 2010]
///
/// This is the program which actually runs our EMS database system.
/// To initiate the program, all this program needs to do is call the UIMenu function DisplayMenu.
/// From then on, all functions which are needed are used in the Supporting class
/// and will be called inside the other classes
///</File Header>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supporting;


namespace EMS
{
    class EMS
    {
        static void Main(string[] args)
        {
            UIMenu menu = new UIMenu();
            while (true)
            {
                if (menu.DisplayMenu()) //run program by starting thse UI menu
                {
                    Console.WriteLine("Press Enter To Return to Main Menu.\n");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
