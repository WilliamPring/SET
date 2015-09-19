/*
File name:
Project:
By:
Date:
Description:
*/

#include "Champions.h"

string Champions::validateStat(int championsStats[6])
{
	bool status = true;
	string message = "";
	for (int i = 0; i < 6; i++)
	{
		if ((i >= 0) && (i < 4))
		{
			if ((championsStats[i] > 10) || (championsStats[i] <= 0))
			{
				if (i == AD_Test)
				{
					message += "Attack Damage value was not Correct\n";
				}
				else if (i == AP_Test)
				{
					message += "Ablity Power value was not Correct\n";
				}
				else if (i == DEF_Test)
				{
					message += "DEFENCE value was not Correct\n";

				}
				else if (i == DIFF_Test)
				{
					message += "Difficutly value was not Correct\n";
				}
			}
		}
		else if (i == 4)
		{
			if ((championsStats[i] < 450) || (championsStats[i] > 6300))
			{
				if (i == IP_Test)
				{
					message += "Influence Points value was not Correct\n";
				}
			}
		}
		else
		{
			if ((championsStats[i] < 260) || (championsStats[i] > 975))
			{
				message += "Riot Points value was not Correct\n";
			}
		}
	}

	return message;
}

string Champions::validateMainClassSubClass(string subClass, string mainClass)
{
	string Message = "";
	bool statusFlag = false;
	if (stricmp(subClass.c_str(), mainClass.c_str()) == 0)
	{
		Message += "Main Class and Sub Class cannot be the same, ";
	}
	if (stricmp(mainClass.c_str(), "N/A") == 0)
	{
		Message += "Main Class Cannot be N/A, ";
	}
	return Message;
}





string Champions::validateName(string inputName[3])
{
	bool status = false;
	bool match = true;
	int i = 0;
	int j = 0;
	int counter = 0;
	string message = "";
	string type[7] = { "Fighter", "Mage", "Assassin", "Tank", "N/A", "Support", "Marksman" };

	regex exp("([a-zA-Z]+\s[a-zA-z]+|[a-zA-Z]+)");

	match = regex_match(inputName[i], exp);

	//First validate the name of the new character

	if (match == false)
	{
		message += "Champions name was not entered correctly\n";
	}

	//Now validate the Class of the new character 

	for (int j = 1; j < 3; j++)
	{
		for (int i = 0; i < 7; i++) //First validate the Class of the new character, then the Sub-Class of the new character
		{
			if (stricmp(inputName[j].c_str(), type[i].c_str()) == 0)
			{
				status = true;
				counter++;
				break;
			}

			if (status == false && i == 6 && j == 1)
			{
				message += "Champions Class was not entered correctly\n";
			}

			if (status == false && i == 6 && j == 2)
			{
				message += "Champions Sub-Class was not entered correctly\n";
			}
		}
	}

	return message;
}


Champions::Champions()
{
	champAD = 0;
	champAP = 0;
	champDEF = 0;
	champDIFF = 0;
	champIP = 0;
	champRP = 0;
	champName = "";
	champMainRole = "";
	champSubRole = "";
}

Champions::Champions(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP)
{
	champAD = atoi(AD.c_str());
	champAP = atoi(AP.c_str());
	champDEF = atoi(DEF.c_str());
	champDIFF = atoi(DIFF.c_str());
	champIP = atoi(IP.c_str());
	champRP = atoi(RP.c_str());
	champName = name;
	champMainRole = MainRole;
	champSubRole = SubRole;
}


string Champions::Champion(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP)
{
	//variables
	string finalMessage = "";
	string messageForInt = "";
	string messageForChampion = "";
	string messageCheckSubAndMainClass = "";
	string total = "";
	string championString[3] = { "" };
	int champStats[6] = { 0 };
	//intialzing the varibles
	//String values for the new character being created 
	champName = name;
	champMainRole = MainRole;
	champSubRole = SubRole;
	championString[0] = name;
	championString[1] = MainRole;
	championString[2] = SubRole;
	//Integer values for the new character being created 
	champAD = atoi(AD.c_str());
	champAP = atoi(AP.c_str());
	champDEF = atoi(DEF.c_str());
	champDIFF = atoi(DIFF.c_str());
	champIP = atoi(IP.c_str());
	champRP = atoi(RP.c_str());
	champStats[0] = champAD;
	champStats[1] = champAP;
	champStats[2] = champDEF;
	champStats[3] = champDIFF;
	champStats[4] = champIP;
	champStats[5] = champRP;

	//Now check the return message for the different validations 
	messageForInt = validateStat(champStats);
	if (messageForInt == "")
	{
		messageForInt += "NO ERRORS";

	}
	messageForChampion = validateName(championString);
	if (messageForChampion == "")
	{
		messageForChampion += "NO ERRORS";

	}
	messageCheckSubAndMainClass = validateMainClassSubClass(SubRole, MainRole);
	if (messageCheckSubAndMainClass == "")
	{
		messageCheckSubAndMainClass += "NO ERRORS";

	}
	if ((messageForInt == "NO ERRORS") && (messageForChampion == "NO ERRORS") && (messageCheckSubAndMainClass == "NO ERRORS"))
	{
		finalMessage += "You have sucessfully created your new champion!\n";
	}
	else
	{
		finalMessage += "There was a problem when creating you champion...\n";
		finalMessage += "\nERRORS WILL BE LISTED BELOW: \n";
		finalMessage += "\nError For champions Stats: \n";
		finalMessage += messageForInt;
		finalMessage += "\nError For champions Class and Sub Class: \n";
		finalMessage += messageForChampion;
		finalMessage += "\nError For duplication or non validClass input: \n";
		finalMessage += messageCheckSubAndMainClass;
	}

	return finalMessage;
}


void Champions::SetAllPurposeStats(string stats)
{
	allPurposeStats = stats;
}

bool Champions::operator<(const Champions& type) const
{
	bool status = false;
	if (allPurposeStats == "AD")
	{
		if (this->champAD < type.champAD)
		{
			status = true;
		}
	}
	else if (allPurposeStats == "AP")
	{
		if (this->champAP < type.champAP)
		{
			status = true;
		}
	}
	else if (allPurposeStats == "DEF")
	{
		if (this->champDEF < type.champDEF)
		{
			status = true;
		}
	}
	else if (allPurposeStats == "DIFF")
	{
		if (this->champDIFF < type.champDIFF)
		{
			status = true;
		}
	}
	else if (allPurposeStats == "IP")
	{
		if (this->champIP < type.champIP)
		{
			status = true;
		}
	}
	else
	{
		if (this->champRP < type.champRP)
		{
			status = true;
		}
	}

	return status; 
}




