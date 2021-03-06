﻿/*
Project: PortWizard
File: Program.cs
Date: 2/23/2016
By: William Pring and Denys Polituk
Description: Converting C project to C#
*/

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
                string status = ReadFile(inputFile, args);
                if (status =="")
                {
                    Console.WriteLine("Cannot open file");
                }
            }
            else
            {
                Console.WriteLine("Error wrong Format\nExample: -i C_source_file -o C#_output_file");
            }
        }
        /// <summary>
        /// Converting printf Function
        /// </summary>
        /// <param name="printfToConvert"></param>
        /// <returns></returns>
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
          
            if (returnString.Contains("atoi"))
            {
                returnString = atoiConvertor(returnString);
            }
            if (returnString.Contains("strlen"))
            {
                returnString = strlenConvert(returnString);
            }


            return returnString;
        }
        /// <summary>
        /// Converting strlen function to c#
        /// </summary>
        /// <param name="varibleToConvert"></param>
        /// <returns></returns>
        public static string strlenConvert(string varibleToConvert)
        {
            int count = Regex.Matches(varibleToConvert, "strlen.+").Count;
            if (count >= 1)
            {
                varibleToConvert = varibleToConvert.Replace("strlen", "^");
                while (true)
                {
                    count = varibleToConvert.IndexOf('^');
                    for (int i = count; i <= varibleToConvert.Length; i++)
                    {
                        if (varibleToConvert[i] == ')')
                        {
                            varibleToConvert = varibleToConvert.Insert(i + 1, ".Length");
                            varibleToConvert = varibleToConvert.Remove(count, 1);
                            break;
                        }
                    }
                    if (varibleToConvert.Contains("^"))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return varibleToConvert;
        }
        /// <summary>
        /// Converting all varible to C# style
        /// </summary>
        /// <param name="varibleToConvert"></param>
        /// <returns></returns>
        public static string ConvertVarible(string varibleToConvert)
        {
            string newConvertString = "";
            Regex Char = new Regex(@"char.+");
            if (Char.IsMatch(varibleToConvert))
            {
                string newString = varibleToConvert.Replace("char ", "String ");
                int length = varibleToConvert.IndexOf('[');
                newString = varibleToConvert.Substring(5, length - 5);
                newConvertString = "string" + newString + ";\n"; 
                if (varibleToConvert.Contains("="))
                {
                    int index = varibleToConvert.IndexOf("=");
                    varibleToConvert = varibleToConvert.Remove(0, index);
                    index = newConvertString.IndexOf(";");
                    newConvertString = newConvertString.Insert(index, varibleToConvert);
                    index = newConvertString.IndexOf(";");
                    newConvertString = newConvertString.Remove(index, 1);

                }
            }
            else
            {
                newConvertString = varibleToConvert + '\n';
                newConvertString = strlenConvert(newConvertString);
                if(newConvertString.Contains("atoi"))
                {
                    newConvertString = atoiConvertor(newConvertString);
                }

            }
            return newConvertString;
        }
        /// <summary>
        /// Setting up the project
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string setUpProject(string[] args)
        {
            string input = "";
            input = "\nusing System;\nnamespace " + args[3].Remove(args[3].Length-3, 3) + "\n{\n" + "class Program\n" + "{\n";
            return input;
        }
        /// <summary>
        /// Atoi Convertor to c# Function
        /// </summary>
        /// <param name="ReadNewLienInFile"></param>
        /// <returns></returns>
        public static string atoiConvertor(string ReadNewLienInFile)
        {
            string newInput = "";
            if (ReadNewLienInFile.Contains("Console.WriteLine"))
            {
                ReadNewLienInFile = ReadNewLienInFile.Replace("atoi(", "Int32.Parse(");
                newInput = ReadNewLienInFile;
            }
            else
            {
                ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, @"\t|\n|\r", "");
                ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, "(atoi)|[(^$)];|(atoi)|[(^$)]", "");
                ReadNewLienInFile = Regex.Replace(ReadNewLienInFile, @"\(", "");
                newInput = ReadNewLienInFile.Insert(ReadNewLienInFile.IndexOf('=') + 1, "Int32.Parse(") + ");\n";
            }
            return newInput;   
        }
        /// <summary>
        /// Get Convertor to C# Function
        /// </summary>
        /// <param name="ReadNewLineInFile"></param>
        /// <returns></returns>
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
        /// <summary>
        /// fgets Convert to C# Functions
        /// </summary>
        /// <param name="readLineInFile"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Coverting File to C#
        /// </summary>
        /// <param name="readLineInFile"></param>
        /// <param name="path"></param>
        /// <param name="TotalInfo"></param>
        /// <returns></returns>
        public static string ConvertFile(string readLineInFile, string path, string TotalInfo)
        {
            string newString = "";
            char readOrWrite = '0';
            int wordToReplace = 0;
            string replace = "";
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
                                wordToReplace = "StreamReader".Length;
                                replace = "StreamReader";
                            }
                            else
                            {
                                string info = output.Split(',')[0].Trim();
                                string outputForVariable = readLineInFile.Substring(readLineInFile.IndexOf('*') + 1);
                                outputForVariable = outputForVariable.Remove(outputForVariable.Length - 1);
                                newString = "StreamWriter " + outputForVariable + "= new StreamWriter(" + info + ");";
                                wordToReplace = "StreamWriter".Length;
                                replace = "StreamWriter";

                            }
                            string subString = newString;
                            subString = subString.Substring(0, subString.LastIndexOf("="));
                            newString = newString.Remove(0, wordToReplace);
                            replace = subString.Replace(replace, ""); 
                            newString = "try{" + newString + "}catch{" + replace + "= null;" + "}";
                            TotalInfo = TotalInfo.Replace("using System;", "using System;\nusing System.IO;\n");
                            TotalInfo += "\t" + subString + ";" + "\n" + "\t" + newString;
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
        /// <summary>
        /// fprintf to c#
        /// </summary>
        /// <param name="readLineInFile"></param>
        /// <returns></returns>
        public static string fprintfFunctionConvert(string readLineInFile)
        {
            string newString = readLineInFile;
            newString = newString.Remove(0, 13); 
            string last = readLineInFile.Remove(0, 9);
            last = last.Split(',')[0].Trim();
            last = last.Replace("(", ""); 
            last = "\t"+ last + ".WriteLine(" + newString; 
            return last;
        }
        /// <summary>
        /// Convert Fclose to c#
        /// </summary>
        /// <param name="readLineInFile"></param>
        /// <returns></returns>
        public static string ConvertFclose(string readLineInFile)
        {
            readLineInFile = readLineInFile.Replace("fclose", ""); 
            readLineInFile = readLineInFile.Replace(")", ").Close()");
            return readLineInFile;
        }
        /// <summary>
        /// Parsing the file line by line
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string ReadFile(string path, string[] args)
        {
            bool statusOfOpenFile = true;
            string readLineInFile = "";
            string newOutput = "";
            Regex specialCharacter = new Regex(@"(\{|\})");
            Regex assignVariable = new Regex(@"int.+|char.+"); 
            Regex comment = new Regex(@"\/+\*");
            Regex returnInfo = new Regex("return");
            Regex gets = new Regex("gets");
            Regex atoi = new Regex("atoi");
            try
            {
                statusOfOpenFile = (File.Exists(path)) ? true : false;
                if (statusOfOpenFile == true)
                {
                    StreamReader read = new StreamReader(path);
                    while (true)
                    {
                        readLineInFile = read.ReadLine();
                        if (readLineInFile == null)
                        {
                            break;
                        }
                        if (readLineInFile.Contains("NULL"))
                        {
                            readLineInFile = readLineInFile.Replace("NULL", "null");
                        }
                        if (readLineInFile == "")
                        {
                            newOutput += '\n';
                            continue;
                        } 
                        if (readLineInFile.Contains("fgets"))
                        {
                            newOutput += Convertfgets(readLineInFile);
                        }
                        //for comment header and brackets
                        else if ((readLineInFile == "int main (void)") || (readLineInFile == "int main(void)"))
                        {
                            if ((readLineInFile == "int main (void)") || (readLineInFile == "int main(void)"))
                            {
                                newOutput += setUpProject(args);
                                newOutput += "static int Main(string[] args)\n";
                            }
                            else
                            {
                                newOutput += readLineInFile + '\n';
                            }
                        }
                        else if (readLineInFile.Contains("fprintf"))
                        {
                            newOutput += fprintfFunctionConvert(readLineInFile) + "\n";
                        }
                        //for printf
                        else if (readLineInFile.Contains("printf"))
                        {
                            newOutput += ConvertPrintf(readLineInFile) + "\n";
                        }
                        else if (readLineInFile.Contains("FILE"))
                        {
                            newOutput = ConvertFile(readLineInFile, path, newOutput) + "\n";
                        }
                        else if (assignVariable.IsMatch(readLineInFile))
                        {
                            newOutput += "\t" + ConvertVarible(readLineInFile);
                        }
                        else if (readLineInFile.Contains("strlen("))
                        {
                            newOutput += strlenConvert(readLineInFile) + "\n";
                        }
                        else if (atoi.IsMatch(readLineInFile))
                        {
                            newOutput += "\t" + atoiConvertor(readLineInFile) + "\n";
                        }
                        else if (gets.IsMatch(readLineInFile))
                        {
                            newOutput += GetsConvertor(readLineInFile) + "\n";
                        }
                        //for variables     
                        else
                        {
                            if ((readLineInFile.Contains("#") || readLineInFile.Contains("fopen")) || readLineInFile.Contains("fprintf"))
                            {
                                continue;
                           } 
                            else if((readLineInFile.Contains("*") == true) || (readLineInFile.Contains("fclose")) || (readLineInFile.Contains("NULL")))
                            {
                                if (readLineInFile.Contains("."))
                                {
                                     
                                    newOutput += " * " + args[3] + "\n";
                                }
                                else if (readLineInFile.Contains("fclose"))
                                {
                                    newOutput += ConvertFclose(readLineInFile) + "\n";
                                }
                                else if (readLineInFile.Contains("NULL"))
                                {
                                    newOutput += readLineInFile.Replace("NULL", "null") + "\n";
                                }
                                else
                                {
                                    newOutput += readLineInFile + "\n";
                                }
                            }
                            else
                            {
                                newOutput += readLineInFile + "\n";
                            }
                        }
                    }
                }
                newOutput += "}\n}\n";
            }
            catch (Exception errorFileStatus)
            {
                Console.WriteLine(errorFileStatus.Message);
            }
            if (newOutput != "")
            {
                System.IO.File.WriteAllText(args[3], newOutput);
            }

            return newOutput;
       } 
    }
}
