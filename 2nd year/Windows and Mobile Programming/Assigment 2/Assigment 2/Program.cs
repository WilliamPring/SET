using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            container newVehiclesInfo = new container();
            View startProgram = new View();
            //Begin UI for the user, insert the container class to store the vehicles 
            startProgram.getUserInput(newVehiclesInfo);
        }
    }
}
