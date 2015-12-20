/*
    FILE            : Logger.cs
    PROJECT         : WMP Final project
    PROGRAMMER      : Denys Politiuk
    FIRST VERSION   : 2015-12-03
    DESCRIPTION     :
        Class that contains logging of the events to the log file
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace CheckersLib
{
    /// <summary>
    /// Logger class that saves given information to the log file
    /// </summary>
    public class Logger
    {
        private const string dirPath = "Log";

        /// <summary>
        /// Logging the event
        /// </summary>
        /// <param name="where">Where the event happened</param>
        /// <param name="details">Details about the event</param>
        public static void LogEvent(string where, string details)
        {
            string message = "";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            string currentTime = DateTime.Now.ToString("HH:mm:ss");

            message = currentDate + " " + currentTime + " [" + where + "] " + details;

            StreamWriter writer = null;

            try
            {
                writer = OpenFile(DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")));
            }
            catch (Exception e)
            {
                writer = OpenFile(DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US")) + "ERROR");
                message = e.Message;
            }

            try
            {
                writer.WriteLine(message);
            }
            catch (Exception e)
            {
                writer.Close();

                writer = new StreamWriter("ERROR.log", true);
                writer.WriteLine(message);
            }

            CloseFile(writer);
        }

        /// <summary>
        /// Opening the file writer
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <returns></returns>
        private static StreamWriter OpenFile(string fileName)
        {
            string path = dirPath + @"\" + fileName + ".log";

            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
            }

            if (File.Exists(path) == false)
            {
                File.Create(path).Dispose();
            }

            StreamWriter write = new StreamWriter(path, true);

            return write;
        }



        /// <summary>
        /// Closing the file writer
        /// </summary>
        /// <param name="writer"></param>
        private static void CloseFile(StreamWriter writer)
        {
            writer.Close();
        }
    }
}
