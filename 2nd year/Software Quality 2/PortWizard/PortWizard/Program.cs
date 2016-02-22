using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PortWizard
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputFile = "";
            string inputFile = "";
            if ((args.Length == 4) && (args[0] == "-i") && (args[2] == "-o"))
            {
                inputFile = args[1];
                outputFile = args[3];
                bool status = ReadFile(inputFile, args);
                if (status == false)
                {
                    Console.WriteLine("Please provide a existing file");
                }
            }
            else
            {
                Console.WriteLine("Error wrong Format\nExample: -i C_source_file -o C#_output_file");
            }
        }
        public static string ConvertPrintf(string printfToConvert)
        {
            string returnString = "";
            if(printfToConvert.Contains("%") == true)
            {
                string newSubString = "";
                returnString = printfToConvert.Replace("printf", "Console.WriteLine");
                string output = printfToConvert.Split('"', '"')[1];
                string temp = output;
                int count = new Regex(Regex.Escape("%")).Matches(output).Count;
                int w = 1;
                bool status = true;
                for (int i =0; i < count; i++)
                {
                    string subString = ""; 
                    int positionPercent = output.IndexOf("%");
                    int positionDecimal = output.IndexOf("d");
                    if (positionDecimal < 0)
                    {
                        positionDecimal = output.IndexOf("s");
                        status = false;
                    }
                    if (status ==false)
                    {
                        newSubString = "{" + i + "," + output[positionPercent + 0] + "}";
                        newSubString = newSubString.Replace("%", "0");
                    }
                    else
                    {
                        newSubString = "{" + i + "," + output[positionPercent + 1] + "}";
                    }
                    output = output.Insert(positionDecimal+1, newSubString);
                    output = output.Remove(positionPercent, (positionDecimal+ 1)-positionPercent);
                    positionDecimal = 0;
                }
                returnString = returnString.Replace(temp, output);
            }
            else
            {
                returnString = printfToConvert.Replace("printf", "Console.WriteLine");
            }



            return returnString;
        }


        public static string ConvertVarible(string varibleToConvert)
        {
            string newConvertString = "";
            Regex FILE = new Regex(@"FILE..+");
            Regex Char = new Regex(@"char.+");
            if (FILE.IsMatch(varibleToConvert))
            {
                newConvertString = "\tFileStream fp;\n";
            }
            else if (Char.IsMatch(varibleToConvert))
            {
                string newString = varibleToConvert.Replace("char ", "String ");
                int length = varibleToConvert.IndexOf('[');

                newString = varibleToConvert.Substring(5, length - 5);
                newConvertString = "\tstring" + newString + ";\n"; 
            }
            else
            {
                newConvertString = varibleToConvert + '\n';
            }

            return newConvertString;
        }

        public static string setUpProject(string[] args)
        {
            string input = "";
            input = "\nusing System;\nnamespace " + args[3].Remove(args[3].Length-3, 3) + "\n{\n" + "class Program\n" + "{\n";
            return input;
        }
        public static string atoiConvertor(string ReadNewLienInFile)
        {
            string newInput = "";
            ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, @"\t|\n|\r", "");
            ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, "(atoi)|[(^$)];|(atoi)|[(^$)]", "");
            ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, @"\(", "");
            newInput = ReadNewLienInFile.Insert(ReadNewLienInFile.IndexOf('=')+1, "Int32.Parse(")+ ");";
            return newInput;   
        }
        public static string GetsConvertor(string ReadNewLineInFile)
        {
            string newInput ="";

            ReadNewLineInFile = Regex.Replace(ReadNewLineInFile, "(gets)|[(^$)];", "");
            ReadNewLineInFile = Regex.Replace(ReadNewLineInFile, @"\(", "");
            ReadNewLineInFile = Regex.Replace(ReadNewLineInFile, @"\)", "");
            ReadNewLineInFile = Regex.Replace(ReadNewLineInFile, @"\t|\n|\r", ""); //Get rid of tab, newline characters
            newInput += "\t" + ReadNewLineInFile + "= System.Console.ReadLine();";
            return newInput;
        }

        public static string Convertfgets(string readLineInFile)
        {

            readLineInFile= readLineInFile.Replace("fgets", "");
            int startPosition = readLineInFile.IndexOf(',');
            int endPosition = readLineInFile.LastIndexOf(',');
            readLineInFile = readLineInFile.Remove(startPosition, endPosition - startPosition);
            readLineInFile = readLineInFile.Replace(",", "=");
            startPosition = readLineInFile.IndexOf('=')+4;
            readLineInFile = readLineInFile.Insert(startPosition, ".ReadLine()");


            return readLineInFile; 
        }

        public static string ConvertFile(string readLineInFile, string path, string TotalInfo)
        {
            string newString = "";
            char readOrWrite = '0';
            string constructingNewString = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (true)
                    {
                        newString = sr.ReadLine();
                        if (newString.Contains("fopen"))
                        {
                            readOrWrite = newString[newString.Length - 4];
                            string output = newString.Split('(', ')')[1];
                            if (readOrWrite == 'r')
                            {
                                string info = output.Split(',')[0].Trim();
                                string outputForVariable = readLineInFile.Substring(readLineInFile.IndexOf('*') + 1);
                                outputForVariable = outputForVariable.Remove(outputForVariable.Length-1); 
                                newString = "StreamReader " + outputForVariable + "= new StreamReader(" + info+ ");";
                             
                            }
                            else
                            {
                                string info = output.Split(',')[0].Trim();
                                string outputForVariable = readLineInFile.Substring(readLineInFile.IndexOf('*') + 1);
                                outputForVariable = outputForVariable.Remove(outputForVariable.Length - 1);
                                newString = "StreamWriter " + outputForVariable + "= new StreamWriter(" + info + ");";

                            }
                            TotalInfo = TotalInfo.Replace("using System;", "using System;\nusing System.IO;\n");
                            TotalInfo += "\t" + newString + "\n";
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return TotalInfo;
        }
        public static string fprintfFunctionConvert(string readLineInFile)
        {
            string newString = readLineInFile;
            newString = newString.Remove(0, 13); 
            string last = readLineInFile.Remove(0, 9);
            last = last.Split(',')[0].Trim();
            last = "\t"+ last + ".WriteLine(" + newString; 
            return last;
        }
        public static bool ReadFile(string path, string[] args)
        {
            bool statusOfOpenFile = true;
            bool retStatus = true;
            string readLineInFile = "";
            string newOutput = "";
            //check for file comment, check for closing bracets
            //Regex specialCharacter = new Regex(@"(\/?\*|\*?\/|\{|\})");
            Regex specialCharacter = new Regex(@"(\{|\})");
            Regex assignVariable = new Regex(@"int.+|FILE..+|char.+"); 
            Regex forPrintf = new Regex(@"printf");
            Regex fopen = new Regex(@"fopen");
            Regex comment = new Regex(@"\/+\*");
            Regex ifStatement = new Regex(@"if+\(");
            Regex returnInfo = new Regex("return");
            Regex fprintfInfo = new Regex("fprintf");
            Regex gets = new Regex("gets");
            Regex atoi = new Regex("atoi");
            Regex forLoop = new Regex("for");
            char readOrWrite = '0';
            bool status = true;

            try
            {
                statusOfOpenFile = (File.Exists(path)) ? true : false;
                if (statusOfOpenFile == true)
                {
                    System.IO.StreamReader read = new StreamReader(path);
                    while (true)
                    {
                        //read file
                        readLineInFile = read.ReadLine();

                        if (readLineInFile == "")
                        {
                            newOutput += '\n';
                            continue;
                        }
                        if (readLineInFile == null)
                        {
                            break;
                        }
                        //reading comments
                        if (comment.IsMatch(readLineInFile) && (status == true))
                        {
                            status = false;
                            newOutput += readLineInFile + "\n";
                            while (true)
                            {
                                readLineInFile = read.ReadLine();
                                if((readLineInFile == @" */") || (readLineInFile == @"*/"))
                                {
                                    newOutput += readLineInFile + "\n";
                                    break;
                                }
                                newOutput += readLineInFile + "\n";
                            }
                            newOutput += setUpProject(args);
                        }
                        if (readLineInFile.Contains("=="))
                        {
                            newOutput += readLineInFile + "\n";

                        }
                        else if(readLineInFile.Contains("fgets"))
                        {
                            newOutput += Convertfgets(readLineInFile);
                        }
                        //for comment header and brackets
                        else if ((specialCharacter.IsMatch(readLineInFile)) || (readLineInFile == "int main (void)") || (readLineInFile == "int main(void)") || (returnInfo.IsMatch(readLineInFile))|| forLoop.IsMatch(readLineInFile))
                        {
                            if ((readLineInFile == "int main (void)") || (readLineInFile == "int main(void)"))
                            {
                                newOutput += "static void Main(string[] args)\n";
                            }
                            else
                            {
                                newOutput += readLineInFile + '\n';
                            }
                        }       
                        else if(fprintfInfo.IsMatch(readLineInFile))
                        {
                            newOutput += fprintfFunctionConvert(readLineInFile) + "\n";               
                        }
                        //for printf
                        else if(forPrintf.IsMatch(readLineInFile))
                        {
                            newOutput += ConvertPrintf(readLineInFile) + "\n";
                        }
                        else if(atoi.IsMatch(readLineInFile))
                        {
                            newOutput += atoiConvertor(readLineInFile) + "\n";
                        }
                        else if(gets.IsMatch(readLineInFile))
                        {
                            newOutput += GetsConvertor(readLineInFile) + "\n";
                        }
                        else if (readLineInFile.Contains("FILE"))
                        {
                            newOutput = ConvertFile(readLineInFile, path, newOutput) + "\n";
                        }
                        //for variables
                        else if (assignVariable.IsMatch(readLineInFile))
                        {
                            newOutput += ConvertVarible(readLineInFile);
                        }
                        else
                        {
                            if((readLineInFile.Contains("*") == true) || (readLineInFile.Contains("fclose")))
                            {
                                newOutput += readLineInFile + "\n";
                            }
                        }
                    }
                }
            }
            catch (Exception errorFileStatus)
            {
                Console.WriteLine(errorFileStatus.Message);
            }
            newOutput += "}\n}\n";
            return retStatus;
       } 
    }
}
