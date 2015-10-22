using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codewar
{
    class Program
    {
        static void Main(string[] args)
        {
            int w = 0;
            bool status1 = false;
            bool status = false; 
            long num = 451999277;
            long num1 = 41177722899;
            for (int i = 0; i <= 9; i++)
            {
                var count = num.ToString().Count(c => c == i);
                if (count == 3)
                {
                    status = true;
                }
            }
            if (status == true)
            {
                for (int i = 0; i <= 9; i++)
                {
                    var count = num1.ToString().Count(c => c == i);
                    if (count == 2)
                    {
                        status1 = true;
                    }
                }
            }
            if (status1 == true)
            {
                w = 1;
            }
            else
            {
                w = 0; 
            }

        }
    }
}
