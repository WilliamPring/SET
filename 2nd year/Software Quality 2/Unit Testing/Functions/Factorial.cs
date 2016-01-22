//This code was borrowed from the VS2008 C# Samples and was modified by S. Clarke

// Factorial.cs
// compile with: /target:library
using System; 

// Declares a namespace. You need to package your libraries according
// to their namespace so the .NET runtime can correctly load the classes.
namespace Functions 
{ 
    public static class Factorial 
    { 
        // The "Calc" static method calculates the factorial value for the
        // specified integer passed in:
        //  
        // QUESTION:  I'm not exactly sure about the rules around calculating
        //            a factorial - but I like this recursive algorithm, so
        // I'm going to use it ... anyone know any better ??
        public static int Calc(int i) 
        {
            int answer = i;
            if ((answer <= 0) || (i > 20))
            {
                answer = 0;
            }
            else
            {
                for (int w = 1; w < i; w++)
                {
                    answer = answer * w;
                }
            }
            return answer;

            //  return((i <= 1) ? 1 : (i * Calc(i-1))); 
        }
    }
}

