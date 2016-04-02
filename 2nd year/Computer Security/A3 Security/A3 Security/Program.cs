/*
* FILE : Program.cs
* PROJECT : A3- Steganography
* PROGRAMMER : William and Denys
* FIRST VERSION : 2016-31-03
* DESCRIPTION :
* Extracting images for img file extension and encoding and decoding images
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace A3_Security
{

    class Program
    {
        /// <summary>
        /// Encode the message that you put in the picture
        /// http://www.codeproject.com/Tips/635715/Steganography-Simple-Implementation-in-Csharp
        /// I took his logic and refactor it for the SET standards as well as modify it to my preferences
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        static bool encodeMessage(string filename, string message)
        {
            int encodeIndex = 0;
            int convertedText = 0;
            bool fillWithZeros = false;
            bool finishedWithMessage = false;
            bool hideText = true;

            bool heightLoop = false;
            bool widthLoop = false;

            int R = 0;
            int G = 0;
            int B = 0;
            bool status = true;
            int trailingZeros = 0;
            int pixelElement = 0;
            int messageLength = message.Length;
            //file name
            if (File.Exists(filename))
            {
                Bitmap bitmap = new Bitmap(filename);
                if (message.Length < 1)
                {
                    Console.WriteLine("Message was not valid");
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < bitmap.Height && heightLoop == false; i++)
                        {
   
                            for (int j = 0; j < bitmap.Width && widthLoop == false; j++)
                            {
                                Color pixel = bitmap.GetPixel(j, i); 
                                //Set the pixel for the RGB 
                                R = pixel.R - pixel.R % 2;
                                G = pixel.G - pixel.G % 2;
                                B = pixel.B - pixel.B % 2;

                                // for each pixel, pass through its elements (RGB)
                                for (int RGB = 0; RGB < 3; RGB++)
                                {
                                    // check to see if 8 zeros have been added if so that means everything is finish
                                    if (pixelElement % 8 == 0)
                                    {
                                        if (fillWithZeros && trailingZeros == 8)
                                        {
                                            finishedWithMessage = true;
                                            heightLoop = true;
                                            widthLoop = true;
                                            if ((pixelElement - 1) % 3 < 2)
                                            {
                                                bitmap.SetPixel(j, i, Color.FromArgb(R, G, B));
                                            }
                                            //exit the program
                                            break;
                                        }

                                        //check just in case that all of message has been hidden in the picture if it has not continue with the program and appending it 
                              
                                        if (encodeIndex >= messageLength)
                                        {
                                            fillWithZeros = true;
                                            hideText = false;
                                        }
                                        else
                                        {
                                            convertedText = message[encodeIndex++];
                                        }
                                    }
                                    //Color is red 
                                    if (pixelElement % 3 == 0 && hideText == true)
                                    {
                                        if (hideText)
                                        {
                                            R += convertedText % 2;
                                            convertedText /= 2;
                                        }
                                    }
                                    //Color is Green 
                                    else if (pixelElement % 3 == 1 && hideText == true)
                                    {
                                        G += convertedText % 2;
                                        convertedText /= 2;
                                    }
                                    //Color is Blue
                                    else if (pixelElement % 3 == 2)
                                    {
                                        if (hideText)
                                        {
                                            B += convertedText % 2;
                                            convertedText /= 2;
                                        }
                                        bitmap.SetPixel(j, i, Color.FromArgb(R, G, B)); //Set the new pixel with the encoded text in it
                                    }

                                    //Now check if zero should be appended at the end
                                    if (fillWithZeros)
                                    {
                                        trailingZeros++;
                                    }

                                    pixelElement++;
                                }
                            }

                        }

                        if (finishedWithMessage)
                        {
                            Console.WriteLine("Success!");
                            //removing everything after a .
                            filename = filename.Split('.')[0].Trim();
                            //make the extension .png
                            filename += ".png";
                            Bitmap saveBitmap = new Bitmap(bitmap);
                            saveBitmap.Save(filename, ImageFormat.Png);
                            //Dispose both bitmap dispose thing after you used them
                            bitmap.Dispose();
                            saveBitmap.Dispose();
                        }
                        else
                        {
                            status = false;
                            Console.WriteLine("Unable to encode the current message to the picture");
                        }
                    }
                    catch (Exception exp)
                    {
                        status = false;
                        Console.WriteLine(exp.Message);
                    }
                }
            }
            else
            {
                status = false;
                Console.WriteLine("Could Not Find File!");
            }
          return status;
       }
        /// <summary>
        /// decode the message
        /// http://www.codeproject.com/Tips/635715/Steganography-Simple-Implementation-in-Csharp
        /// I took his logic and refactor it for the SET standards as well as modify it to my preferences
        /// </summary>
        /// <param name="filename"></param>
        static void decodeMessage(string filename)
        {
            string message = "";
            int convertedText = 0;
            int colorIndex = 0;
            bool finishedWithMessage = false;
            bool height = false;
            bool width = false;
            if (File.Exists(filename))
            {
                Bitmap bitmap = new Bitmap(filename);

                try
                {
                    for (int h =0; h < bitmap.Height && height == false; h++)
                    {
                       
                        for (int w = 0; w < bitmap.Width && width == false; w++)
                        {
                            Color pixel = bitmap.GetPixel(w, h);

                            // for each pixel, pass through its elements (RGB)
                            for (int RGB = 0; RGB < 3; RGB++)
                            {

                                if (colorIndex % 3 == 0)
                                {
                                    convertedText = convertedText * 2 + pixel.R % 2; //Reverse the process when first encoding the message
                                }
                                else if (colorIndex % 3 == 1)
                                {
                                    convertedText = convertedText * 2 + pixel.G % 2;
                                }
                                else if (colorIndex % 3 == 2)
                                {
                                    convertedText = convertedText * 2 + pixel.B % 2;
                                }

                                colorIndex++;

                           
                                if (colorIndex % 8 == 0)
                                {
                                    
                                    convertedText = reverseBits(convertedText);

                                    if (convertedText == 0)
                                    {
                                        finishedWithMessage = true;
                                        height = true;
                                        width = true;
                                        break;
                                    }
                                    message += ((char)convertedText).ToString();
                                }
                            }
                        }

                    }

                    if (finishedWithMessage)
                    {
                        bitmap.Dispose();
                        if (message == "")
                        {
                            Console.WriteLine("There is no message to decode");
                        }
                        else
                        {
                            Console.WriteLine("The Message inside the picture was:" + message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Decoding failed");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Could Not Find File!");
            }
        }
        /// <summary>
        /// I did not modify this function at all this was base his reverseBit for the message
        /// http://www.codeproject.com/Tips/635715/Steganography-Simple-Implementation-in-Csharp
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }
        static void Main(string[] args)
        {
            int fileRecovered = 0;
            bool thumbnail = false;
            bool image = false;
            string filename = "";
            bool status = true;
            if (args.Length > 3)
            {
                Console.WriteLine("To many agruments");
            }
            else
            {
                //-e [File Name]
                if (args[0] == "-e")
                {
                    filename = args[1];
                    string encodingMsg = "";
                    encodingMsg = args[2];
                    status = encodeMessage(filename, encodingMsg);
                }
                else if (args[0] == "-d")
                {
                    filename = args[1];
                 
                    decodeMessage(filename);
                }
                else
                {
                    string fileName = args[0];
                    //Check to see if file exist
                    if (File.Exists(fileName))
                    {
                        byte[] fileBytes = File.ReadAllBytes(fileName);
                        BinaryWriter writtenData = new BinaryWriter(Stream.Null);
                        try
                        {
                            for (int i = 0; i <= fileBytes.Length; i++)
                            {
                                if ((thumbnail == true) || (image == true))
                                {
                                    if ((fileBytes[i] == 0xFF) && (fileBytes[i + 1] == 0xD9))
                                    {
                                        if (image == true)
                                        {
                                            writtenData.Close();
                                            image = false;
                                            thumbnail = false;
                                        }
                                        else if (thumbnail == true)
                                        {
                                            writtenData.Write(fileBytes[i]);
                                            writtenData.Write(fileBytes[i + 1]);
                                            thumbnail = false;
                                            image = true;
                                            i++;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        writtenData.Write(fileBytes[i]);
                                        continue;
                                    }

                                }
                                if ((fileBytes[i] == 0xFF) && (fileBytes[i + 1] == 0xD8) && (fileBytes[i + 2] == 0xFF))
                                {
                                    fileRecovered++;
                                    writtenData = new BinaryWriter(new FileStream(fileRecovered.ToString() + ".jpeg", FileMode.Create));
                                    writtenData.Write(fileBytes[i]);
                                    writtenData.Write(fileBytes[i + 1]);
                                    writtenData.Write(fileBytes[i + 2]);
                                    i += 2;
                                    thumbnail = true;
                                    image = false;
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("End of File");
                        }

                    }
                    else
                    {
                        Console.WriteLine("File not found");
                    }
                }
            }
        }
    }
}

