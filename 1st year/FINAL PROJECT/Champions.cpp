/*
File name:
Project:
By:
Date:
Description:
*/

#include "Champions.h"

int Champions::GetChampAD()
{
	return champAD;
}



string Champions::GetChampName()
{
	return champName;
}



string Champions::ChampMainRole()
{
	return champMainRole;
}



string Champions::ChampSubRole()
{
	return champSubRole;
}



int Champions::GetChampDiff()
{
	return champDIFF;
}



int Champions::GetChampRP()
{
	return champRP;
}



int Champions::GetChampIP()
{
	return champIP;
}



int Champions::GetChampAP()
{
	return champAP;
}



int Champions::GetChampDef()
{
	return champDEF;
}



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
					message += "* Attack Damage (AD) value was incorrect\n";
				}
				else if (i == AP_Test)
				{
					message += "* Ablity Power (AP) value was incorrect\n";
				}
				else if (i == DEF_Test)
				{
					message += "* Defence value was incorrect\n";

				}
				else if (i == DIFF_Test)
				{
					message += "* Difficutly value was incorrect\n";
				}
			}
		}
		else if (i == 4)
		{
			if ((championsStats[i] < 450) || (championsStats[i] > 6300))
			{
				if (i == IP_Test)
				{
					message += "* Influence Points (IP) value was incorrect\n";
				}
			}
		}
		else
		{
			if ((championsStats[i] < 260) || (championsStats[i] > 975))
			{
				message += "* Riot Points (RP) value was incorrect\n";
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
		Message += "* Main Class and Sub Class cannot be the same ";
	}
	if (stricmp(mainClass.c_str(), "N/A") == 0)
	{
		Message += "* Main Class cannot be N/A ";
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

	match = SetChampName(inputName[0]);

	//First validate the name of the new character

	if (match == false)
	{
		message += "* Champions name was not entered correctly\n";
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
				message += "* Champions Class was not entered correctly\n";
			}

			if (status == false && i == 6 && j == 2)
			{
				message += "* Champions Sub-Class was not entered correctly\n";
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
	//intialzing the variables
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
		messageForInt += "NO ERRORS\n";

	}
	messageForChampion = validateName(championString);
	if (messageForChampion == "")
	{
		messageForChampion += "NO ERRORS\n";

	}
	messageCheckSubAndMainClass = validateMainClassSubClass(SubRole, MainRole);
	if (messageCheckSubAndMainClass == "")
	{
		messageCheckSubAndMainClass += "NO ERRORS\n";

	}
	if ((messageForInt == "NO ERRORS\n") && (messageForChampion == "NO ERRORS\n") && (messageCheckSubAndMainClass == "NO ERRORS\n"))
	{
		finalMessage += "You have sucessfully created your new champion!\n";
	}
	else
	{
		finalMessage += "There was a problem when creating your champion...\n";
		finalMessage += "\nERRORS WILL BE LISTED BELOW:\n";
		finalMessage += "\nError For champions Stats:\n";
		finalMessage += messageForInt;
		finalMessage += "\nError For champions Class and Sub Class:\n";
		finalMessage += messageForChampion;
		finalMessage += "\nError For duplication:\n";
		finalMessage += messageCheckSubAndMainClass;
		finalMessage += "\n"; 
	}

	return finalMessage;
}



bool Champions::SetChampAP(int AP)
{
	bool returnFlag = false;

	if ((AP <= 10) && (AP >= 1))
	{
		this->champAP = AP;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampDef(int DEF)
{
	bool returnFlag = false;

	if ((DEF <= 10) && (DEF >= 1))
	{
		this->champDEF = DEF;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampDiff(int Difficulty)
{
	bool returnFlag = false;

	if ((Difficulty <= 10) && (Difficulty >= 1))
	{
		this->champDIFF = Difficulty;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampAD(int AD)
{
	bool returnFlag = false;

	if ((AD <= 10) && (AD >= 1))
	{
		this->champAD = AD;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampIP(int IP)
{

	bool returnFlag = false;

	if ((IP <= 6300) && (IP >= 450))
	{
		this->champIP = IP;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampRP(int RP)
{
	bool returnFlag = false;

	if ((RP <= 975) && (RP >= 260))
	{
		this->champRP = RP;
		returnFlag = true;
	}

	return returnFlag;
}



bool Champions::SetChampMainRole(string mainRole)
{
	bool returnFlag = false;
	int i = 0;

	string type[7] = { "Fighter", "Mage", "Assassin", "Tank", "N/A", "Support", "Marksman" };

	for (int i = 0; i < 7; i++)
	{
		if (mainRole == type[i])
		{
			this->champMainRole = mainRole;
			returnFlag = true;
			break;
		}
	}

	return returnFlag;
}



bool Champions::SetChampSubRole(string subRole)
{

	bool returnFlag = false;
	int i = 0;

	string type[7] = { "Fighter", "Mage", "Assassin", "Tank", "N/A", "Support", "Marksman" };

	for (int i = 0; i < 7; i++)
	{
		if (subRole == type[i])
		{
			this->champSubRole = subRole;
			returnFlag = true;
			break;
		}
	}

	return returnFlag;
}



bool Champions::SetChampName(string name)
{
	bool returnFlag = true;
	bool match = true;
	int  counter = 0; 

	if (name.length() <= 16)
	{
		for (unsigned int i = 0; i < name.length(); i++)
		{
			if (isdigit(name.c_str()[i]))
			{
				returnFlag = false;
				break;
			}

			if (name.c_str()[i] == ' ')
			{
				counter++;
			}

			if (counter > 1)
			{
				returnFlag = false; 
				break;
			}
		}

		if (returnFlag != false)
		{
			this->champName = name; 
		}
	}
	else
	{
		returnFlag = false; 
	}

	return returnFlag;
}





