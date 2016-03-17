using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Supporting
{
    public class Logging
    {
        FileIO logfile;

        public Logging()
        {
            logfile = new FileIO();
        }

        /// <summary>
        /// Logs an event passed into the function to a logfile.
        /// </summary>
        /// <param name="name">Name of the class the log event is being passed from.</param>
        /// <param name="logData">Log data.</param>
        /// <returns>Boolean value indicating success or failure.</returns>
        public Boolean Log(String name, Char evnt, Char result, String data)
        {
            String filename;
            FileStream log;
            String logevent;

            logevent = "[" + DateTime.Now.ToString() + "] " + name + " :Event: ";

            switch (evnt)
            {
                case 'T':
                    logevent = logevent + "Set: " + data;
                    break;
                case 'M':
                    logevent = logevent + "Modify: " + data;
                    break;
                case 'V':
                    logevent = logevent + "Verify: " + data;
                    break;
                case 'A':
                    logevent = logevent + "Add: " + data;
                    break;
                case 'R':
                    logevent = logevent + "Remove: " + data;
                    break;
                case 'S':
                    logevent = logevent + "Save: " + data;
                    break;
                case 'L':
                    logevent = logevent + "Load: " + data;
                    break;
                case 'F':
                    logevent = logevent + "Find: " + data;
                    break;
                default:
                    return false;
            }

            logevent = logevent + " :Result: ";

            switch (result)
            {
                case 'S':
                    logevent = logevent + "Successful.\r\n";
                    break;
                case 'F':
                    logevent = logevent + "Failed.\r\n";
                    break;
                default:
                    return false;
            }

            filename = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".log";
            log = logfile.Openfile(filename, 'A');
            logfile.WriteToFile(log, logevent);
            logfile.CloseFile(log);

            return true;
        }
    }
}
