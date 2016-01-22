//This code was borrowed from the VS2008 C# Samples and was modified by S. Clarke

// StringBreakout.cs
// compile with: /target:library
using System;
using System.Text.RegularExpressions;

// Declare the same namespace as the one in Factorial.cs. This simply 
// allows types to be added to the same namespace.
namespace Functions 
{
    public static class StringBreakout
    {
        static  int numDigits;
        static  int numAlphas;

        // The NumberOfDigits static method calculates the number of
        // digit characters in the passed string:
        public static int NumberOfDigits(string theString) 
        {
            int count = 0; 
            for ( int i = 0; i < theString.Length; i++ ) 
            {
                if ( Char.IsDigit(theString[i]) ) 
                {
                    count++; 
                }
            }
            // let's hold onto this count
            numDigits = count;

            return count;
        }

        // The NumberOfAlphas static method calculates the number of
        // alphabetic characters in the passed string:
        public static int NumberOfAlphas(string theString)
        {
            int count = 0;
            for (int i = 0; i < theString.Length; i++)
            {
                if (Char.IsLetter(theString[i]))
                {
                    count++;
                }
            }
            // let's hold onto this count
            numAlphas = count;

            return count;
        }

        // The NumberOfOthers static method calculates the number of
        // non-digit / non-alphabetic characters in the passed string:
        //
        // *********************************************************************************
        //  QUESTION : is there a better and more reliable way to determine
        //             the number of "others" ?  Does it matter which order
        //             the NumberOfDigits(), NumberOfAlphas() and NumberOfOthers()
        //             methods are called ?
        // *********************************************************************************
        public static int NumberOfOthers(string theString)
        {
            int count = theString.Length; // start with the length of the string
            numDigits = NumberOfDigits(theString);
            numAlphas = NumberOfAlphas(theString);
            count -= (numAlphas + numDigits);

            return count;
        }

        public static int FindAndExtractDigits(string inpuutString)
        {
            string resultString = "";
            string subString = "";
            int result = 0;
            for (int i = 0; i < inpuutString.Length; i++)
            {
                if (Char.IsDigit(inpuutString[i]))
                {
                    subString += inpuutString[i];
                }
            }

            if (subString != "")
            {
                result = int.Parse(subString);
            }


            return result;
        }

    }
}

