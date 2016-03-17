using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Supporting
{
    public class FileIO
    {
        /// <summary>
        /// Opens or creates a file based on the filename and permissions passed in to the function.
        /// </summary>
        /// <param name="fileName">The name of the file to be opened.</param>
        /// <param name="permissions">The permissions to be given to the opened file.</param>
        /// <returns>If successful FileStream pointing to the opened file. If unsuccessful, returns null.</returns>
        public FileStream Openfile(String fileName, Char permissions)
        {
            try
            {
                String path = "DBase";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                fileName = "DBase\\" + fileName;

                FileInfo f = new FileInfo(fileName);
                FileAccess a = new FileAccess();
                FileMode m = FileMode.OpenOrCreate;
                switch (permissions)
                {
                    case 'R':
                        a = FileAccess.Read;
                        break;
                    case 'W':
                        a = FileAccess.Write;
                        m = FileMode.Create;
                        break;
                    case 'A':
                        m = FileMode.Append;
                        a = FileAccess.Write;
                        break;
                    default:
                        a = FileAccess.Read;
                        break;
                }
                FileStream s = f.Open(m, a);
                return s;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Closes the FileStream passed into the function.
        /// </summary>
        /// <param name="s">The FileStream to be closed.</param>
        /// <returns>Returns a boolean value indicating success or failure.</returns>
        public Boolean CloseFile(FileStream s)
        {
            try
            {
                s.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Writes the database information to a file.
        /// </summary>
        /// <param name="s">The filestream to write the data to.</param>
        /// <param name="data">The data to be written to the file.</param>
        /// <param name="l">A char flag indicating where the data is to be written. ('S' from Start, 'A' for append.)</param>
        /// <returns>A boolean value indicating success or failure.</returns>
        public Boolean WriteToFile(FileStream s, String data)
        {
            try
            {
                UnicodeEncoding uniEncoding = new UnicodeEncoding();
                
                s.Write(uniEncoding.GetBytes(data), 0, uniEncoding.GetByteCount(data));
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads in data from a FileStream and returns a string containing that data.
        /// </summary>
        /// <param name="s">The FileStream to read from.</param>
        /// <returns>String containing the data read in from the file.</returns>
        public String ReadFromFile(FileStream s)
        {
            try
            {
                // Read the source file into a byte array.
                byte[] bytes = new byte[s.Length];
                int numBytesToRead = (int)s.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = s.Read(bytes, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                    {
                        break;
                    }

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                return enc.GetString(bytes);
            }
            catch
            {
                return null;
            }
        }
    }
}
