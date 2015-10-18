using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int vowelCount = 0;
            int numberCount = 0;
            string name = "world";
            //name = str.ToLower();
            // Your code here
            foreach (char c in name)
            {
                if ((c == 'a') || (c == 'e') || (c == 'i') || (c == 'o') || (c == 'u'))
                {
                    name = name.Remove(vowelCount, 1);
                    string ss = (vowelCount+1).ToString();
                    name= name.Insert(vowelCount, ss);
                }
                vowelCount++;
                numberCount++;
            }

            int sdf = 0;

        }

        private static string ToString(int vowelCount)
        {
            throw new NotImplementedException();
        }
    }
}
