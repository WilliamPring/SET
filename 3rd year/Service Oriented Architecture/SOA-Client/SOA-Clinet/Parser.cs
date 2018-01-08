using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_Clinet
{
    class Parser
    {
        //public Parser()
        //{
        //}

        static private string ReplaceASCIISegments(string sMessage)
        {
            // Remove the delimiters -> Intentionally not doing it in regex 
            // Might come across character set issues
            sMessage = sMessage.Replace(((char)11).ToString(), string.Empty); // BOM
            sMessage = sMessage.Replace(((char)13).ToString(), string.Empty); // EOS
            sMessage = sMessage.Replace(((char)28).ToString(), string.Empty); // EOM

            return sMessage;
        }

        static public bool ParseOutputTeamRegister(string sMessage, SocketConnector sConnObj)
        {
            bool retStatus = false;
            sMessage = ReplaceASCIISegments(sMessage);
            // Split the string '|'
            string[] segments = sMessage.Split('|');

            if (segments.Length > 1)
            {
                if (segments[1] == "OK")
                {
                    sConnObj.RootConfigObj.clientSetting.TeamId = segments[2]; //Team id index
                    retStatus = true;
                }
                else if (segments[1] == "NOT-OK")
                {
                    Console.WriteLine("Registry did not accept the command");
                }
            }

            return retStatus;
        }

        static public bool ParseOutputResultsServiceMessage(string sMessage, SocketConnector sConnObj)
        {
            
            bool retStatus = true;
            string[] lines = sMessage.Split('\r');
            foreach (string line in lines)
            {
                int i = 0;
                if (line.Contains("RSP"))
                {
             
                    string[] temp = line.Split('|');
                    foreach (QueryServiceResponse sr in sConnObj.qsr)
                    {
                        if(sr.name == temp[2])
                        {
                            sConnObj.qsr[i].value = temp[4];
                            i = 0;
                            break;
                        }
                        i++;
                    }
                }
            }


            return retStatus;
        }

        static public bool ParseOutputQueryServiceMessage(string sMessage, SocketConnector sConnObj)
        {
            bool retStatus = true;
            string[] lines = sMessage.Split('\r');

            foreach (string line in lines)
            {
                if (line.Contains("ARG"))
                {
                    string[] temp1 = line.Split('|');
                    sConnObj.qsr.Add(new QueryServiceResponse(temp1[0], temp1[1], temp1[2], temp1[3], temp1[4]));
                }
                else if (line.Contains("RSP"))
                {
                    string[] temp = line.Split('|');
                    sConnObj.qsr.Add(new QueryServiceResponse(temp[0], temp[1], temp[2],temp[3]));
                }
                else if (line.Contains("SRV")) {
                    string[] temp = line.Split('|');
                    sConnObj.RootConfigObj.ServiceToConsume.ServiceName = temp[2];
                    sConnObj.RootConfigObj.ServiceToConsume.argumentNumbers = temp[4];
                }
                else if (line.Contains("MCH"))
                {
                    string[] temp = line.Split('|');
                    sConnObj.RootConfigObj.ServiceToConsume.ipaddress = temp[1];
                    sConnObj.RootConfigObj.ServiceToConsume.port = temp[2];
                }
            }



            return retStatus;
        }

    }
}
