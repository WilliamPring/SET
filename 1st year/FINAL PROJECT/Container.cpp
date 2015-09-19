/*
File name:
Project:
By:
Date:
Description:
*/

#include "Container.h"

string Container::arrangeMember;


string Container::deleteAChampion()
{
	bool stats = true;
	vector <Champions>::iterator iter;
	string userInput = "";
	string contentDisplay = "";

	if (contentDisplay == "")
	{

		while (1)
		{
			if (stats != true)
			{
				break;
			}
			getline(cin, userInput);
			if (championContainer.size() < 1)
			{
				contentDisplay += "There was a problem when sorting your champion...\n";
				contentDisplay += "\nERRORS WILL BE LISTED BELOW: \n";
				contentDisplay += "Cannot display when there is no champions!\n";
			}
			else
			{
				for (iter = championContainer.begin(); iter != championContainer.end(); iter++)
				{
					if (stricmp((*iter).GetChampName().c_str(), userInput.c_str()) == 0)
					{
						championContainer.erase(iter);
						contentDisplay += "Sucessfully deleted champion";
						stats = false;
						break;
					}
				}

				if (stats == true)
				{
					contentDisplay += "Champion not found";
					break;
				}
			}
		}

	}
	else
	{
		int i = 0; 
	}

	return contentDisplay;
}



string Container::deleteContainer()
{
	string message = "";
	if (championContainer.size() == 0)
	{
		message += "You can not delete a champion that does not exist yet!!\n";
	}
	else
	{
		championContainer.erase(championContainer.begin(), championContainer.end());
		message += "You have sucesfully deleted all of your champions locally!!\n";
	}
	return message;
}



string Container::createUserLOLChampion(string nameChamp, string MainRoleChamp, string SubRoleChamp, string ADChamp, string APChamp, string DEFChamp, string DIFFChamp, string IPChamp, string RPChamp)
{
	string statusMessage = "";
	Champions rules;

	statusMessage = rules.Champion(nameChamp, MainRoleChamp, SubRoleChamp ,ADChamp, APChamp, DEFChamp, DIFFChamp, IPChamp, RPChamp);
	if (statusMessage == "You have sucessfully created your new champion!\n")
	{
		championContainer.push_back(rules);
	}
	
	return statusMessage;
}



string Container::GetArrangeMember(void)
{
	return arrangeMember; 
}


string Container::saveDataBase()
{
	fstream myfile;
	vector <Champions>::iterator iter;

	myfile.open("williampri.txt", std::ofstream::out | std::ofstream::trunc);
	
	for (iter = championContainer.begin(); iter != championContainer.end(); iter++)
	{
		myfile << (*iter).GetChampName() << "|";
		myfile << (*iter).ChampMainRole() << "|";
		myfile << (*iter).ChampSubRole() << "|";
		myfile << (*iter).GetChampAD() << "|";
		myfile << (*iter).GetChampAP() << "|";
		myfile << (*iter).GetChampDef() << "|";
		myfile << (*iter).GetChampDiff() << "|";
		myfile << (*iter).GetChampIP() << "|";
		myfile << (*iter).GetChampRP() << "|\n";
	}
	dbaseFileInput.close();
	return "Success\n";
}



bool championsCompare(Champions &lhs, Champions &rhs)
{
	bool resultStatus = false;
	Container champsSort;
	if (champsSort.GetArrangeMember() == "AD")
	{
		if (lhs.GetChampAD() < rhs.GetChampAD())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampAD() == rhs.GetChampAD())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "AP")
	{
		if (lhs.GetChampAP() < rhs.GetChampAP())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampAP() == rhs.GetChampAP())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "DEF")
	{
		if (lhs.GetChampDef() < rhs.GetChampDef())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampDef() == rhs.GetChampDef())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "Diff")
	{
		if (lhs.GetChampDiff() < rhs.GetChampDiff())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampDiff() == rhs.GetChampDiff())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "DEF")
	{
		if (lhs.GetChampDef() < rhs.GetChampDef())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampDef() == rhs.GetChampDef())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "IP")
	{
		if (lhs.GetChampIP() < rhs.GetChampIP())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampIP() == rhs.GetChampIP())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else if (champsSort.GetArrangeMember() == "RP")
	{
		if (lhs.GetChampRP() < rhs.GetChampRP())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampRP() == rhs.GetChampRP())
			{
				if (lhs.GetChampName() < rhs.GetChampName())
				{
					resultStatus = true;
				}
			}
		}
	}
	else
	{
		if (lhs.GetChampName() < rhs.GetChampName())
		{
			resultStatus = true;
		}
		else
		{
			if (lhs.GetChampName() == rhs.GetChampName())
			{
				if (lhs.GetChampIP() < rhs.GetChampIP())
				{
					resultStatus = true;
				}
			}
		}
	}


	return resultStatus;
}



string Container::sortAmount(string memType)
{
	string message = ""; 

	if (championContainer.size() == 0)
	{
		message += "There is nothing to sort, you have no champions!\n";
	}
	else if (championContainer.size() == 1)
	{
		message += "There is only 1 champion, nothing else that can sorted!\n";
	}
	else
	{
		sort(championContainer.begin(), championContainer.end(), championsCompare);
		message = "Success! Sorted Champions.\n";
	}

	return message;
}



string Container::displayContainer()
{
	string contentDisplay = "";
	int i = 0;
	int counter = 0;
	string statsConversion = "";
	vector <Champions>::iterator iter;
	bool flag = true; 
	char character = 0;
	if (championContainer.size() < 1)
	{
		contentDisplay += "There was a problem when sorting your champion...\n";
		contentDisplay += "\nERRORS WILL BE LISTED BELOW: \n";
		contentDisplay += "Cannot display when there is no champions!\n";
	}
	else
	{
		for (iter = championContainer.begin(); iter != championContainer.end(); iter++)
		{
			printf("\n****************|CHAMPION|******************\n");
			printf("*   Name\t      : ");
			contentDisplay = createNameSpaces((*iter).GetChampName());
			printf("%s", contentDisplay.c_str());

			printf("   *\n*   Main Role\t      : ");
			contentDisplay = createSpaces((*iter).ChampMainRole());
			printf("%s", contentDisplay.c_str());

			printf("\t   *\n*   Sub Role\t      : ");
			contentDisplay = createSpaces((*iter).ChampSubRole());
			printf("%s", contentDisplay.c_str());
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampAD());
			printf("\t   *\n*   Attack Damage     : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampAP());
			printf("\t\t   *\n*   Attack Power      : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampDef());
			printf("\t\t   *\n*   Attack Defense    : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampDiff());
			printf("\t\t   *\n*   Attack Difficulty : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampRP());
			printf("\t\t   *\n*   Riot Points       : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			//convert int to a string
			statsConversion = convertInt((*iter).GetChampIP());
			printf("\t\t   *\n*   Influence Points  : ");
			printf("%s", statsConversion.c_str());
			statsConversion = "";
			printf("\t\t   *\n");
			printf("********************************************\n");
			if (counter % 2)
			{
				character = _getch();
			}
			counter++;
		}
	}
	return contentDisplay;
}



string Container::convertInt(int number)
{
	stringstream ss;
	ss << number;
	return ss.str();
}



string Container::sortLOLChampion(string stats)
{
	string returnMessage = "";
	string fianlMessage = "";
	if ((stats == "AD") || (stats == "AP") || (stats == "DEF") || (stats == "DIFF") || (stats == "IP") || (stats == "RP") || (stats =="ChampName"))
	{
		arrangeMember = stats;
		returnMessage = sortAmount(stats);
	}	
	else
	{
		returnMessage += "Did not match the corresponding stats\n";
	}
	if (returnMessage == "")
	{
		fianlMessage += "There was a problem when sorting your champion...\n";
		fianlMessage += "\nERRORS WILL BE LISTED BELOW: \n";
		fianlMessage += returnMessage;
	}
	else
	{
		fianlMessage += returnMessage;
	}

	return fianlMessage;
}



string Container::createChampions(string champNum)
{
	int championAmount = 0;
	int i = 0; 
	bool letterCondition = false;
	string message = "";
	Champions fighter;
	string number = "";
	championAmount = atoi(champNum.c_str());

	srand((unsigned)time(NULL));

	for (unsigned int i = 0; i < champNum.length(); i++)
	{
		if (isdigit(champNum.c_str()[i]) == 0)
		{
			letterCondition = true;
		}
	}	

	if (championAmount == 0)
	{
		message += "There was a problem when creating your champions...\n";
		message += "\nERRORS WILL BE LISTED BELOW: \n";
		message += "You cannot specify 0 when creating a champion!\n";
	}
	else if (championAmount < 0)
	{
		message += "There was a problem when creating your champions...\n";
		message += "\nERRORS WILL BE LISTED BELOW: \n";
		message += "You cannot specify a negative champion!\n";
	}
	else if (letterCondition == true)
	{
		message += "Letters are not allowed\n";
	}
	else
	{
		for (int i = 0; i < championAmount; i++)
		{

			fighter.SetChampAD(randomNumCreator());
			fighter.SetChampAP(randomNumCreator());
			fighter.SetChampDef(randomNumCreator());
			fighter.SetChampDiff(randomNumCreator());
			fighter.SetChampIP(randomIPCreator());
			fighter.SetChampRP(randomRPCreator());
			fighter.SetChampMainRole(randomMainRoleCreater());
			fighter.SetChampSubRole(randomSubRoleCreater());
			fighter.SetChampName(randomNameCreator());

			//Push into the vector
			championContainer.push_back(fighter);
		}
		message += "You have sucessfully created ";
		number = convertInt(championAmount);
		message += number;
		message += " new champions!\n";
	}
	return message;
}



int Container::randomNumCreator(void)
{
	int randomNumber = 0;
	time_t t = 0;
	randomNumber = rand() % (10 - 1) + 1;

	return randomNumber;	
}



int Container::randomIPCreator(void)
{
	int randomNumber = 0;

	time_t t = 0;

	randomNumber = rand() % (6301 - 450) + 450;
	return randomNumber;
}



int Container::randomRPCreator(void)
{
	int randomNumber = 0;

	randomNumber = rand() % (976 - 260) + 260;

	return randomNumber;
}



string Container::randomNameCreator(void)
{
	static const char alphanum[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "abcdefghijklmnopqrstuvwxyz";
	char str[121] = "";
	//srand(time(0));

	int letters = rand() % (123 - 65) + 65;
	
	//srand(time(0));
	
	int len = rand() % (15 - 7) + 7; 

	for (int i = 0; i < len; i++) {
		str[i] = alphanum[rand() % (sizeof(alphanum)-1)];
	}

	str[len] = 0;

	string result = str; 

	return result;	
}



string Container::randomMainRoleCreater(void)
{
	int randomNumber = 0;
	string retunvale = "";
	time_t t = 0;

	//srand((unsigned)time(&t));

	randomNumber = 1 + rand() % 5;

	string type[6] = { "Fighter", "Mage", "Assassin", "Tank", "Support", "Marksman" };

	retunvale = type[randomNumber];
	return retunvale;
}



string Container::randomSubRoleCreater(void)
{
	int randomNumber = 0;
	string retunvale = "";
	time_t t = 0;

	randomNumber = 1 + rand() % 6;

	string type[7] = { "Fighter", "Mage", "Assassin", "Tank","N/A" , "Support", "Marksman" };

	retunvale = type[randomNumber];
	return retunvale;
}



void Container::CloseDBase_READ(void)
{
	dbaseFileInput.close();
}



string Container::deleteDatabase()
{
	string message = "";
	ifstream ifs("williampri.txt", std::ofstream::out | std::ofstream::trunc);
	if (!ifs)
	{
		message = "Cannot open file\n";
	}
	else
	{
		message += "deleted database sucessfully\n";
	}
	
	ifs.close();
	return message;
}



string Container::readEntireFile(string fileName)
{
	int pipeCounter = 0;
	string errorMessage = "";
	int totalError = 0;
	bool bytesToRead = true; 
	ifstream ifs(fileName, ifstream::in);
	char character =0;
	int errorCount = 0;
	bool result = true;
	string errorCountForChampionContainer = "";
	string championThatPass = "";
	string reasonForError = "";
	int loopCounter = 0;
	Champions dBaseChampion; 
	string parseLine = "";
	if (ifs.good())
	{
		while (ifs.good())
		{
			character = ifs.get();
			if (character == '|')
			{
				pipeCounter++;
				errorMessage = assignChampionValues(pipeCounter, parseLine, dBaseChampion);
				if ((errorMessage == "Error in reading from database\n") && (result == true))
				{
					errorCount++;
					totalError = errorCount;
					result = false;
					errorCount = 0;
				}
				parseLine = "";
			}
			else if (character == '\n')
			{
				if ((pipeCounter == 9) && (errorCount == 0))
				{
					loopCounter++;
					championContainer.push_back(dBaseChampion);
					pipeCounter = 0;
					result = true;
				}
				else if ((pipeCounter == 0) || (pipeCounter ==1)|| (pipeCounter == 2) || (pipeCounter == 3) || (pipeCounter == 4) ||
					(pipeCounter == 5) || (pipeCounter == 6) || (pipeCounter == 7) || (pipeCounter == 8))
				{
					errorCount++;
					totalError = errorCount;
					errorCount = 0;
					pipeCounter = 0;
					result = true;
					parseLine = "";
				}
				else 
				{
					totalError++;
					totalError = errorCount;
					totalError = 0;
					pipeCounter = 0;
					result = true;
					parseLine = "";
				}
			}
			else
			{
				parseLine += character;
			}
		}
	}
	if ((pipeCounter == 0) && (errorCount == 0) && (loopCounter ==0))
	{
		reasonForError += "Database is empty did not add anything in container\n";
	}
	else if (totalError == 0)
	{
		reasonForError += "Sucessful in putting Champions in the database!\n";
		reasonForError += errorMessage;
	}
	else
	{
		reasonForError = +"There was an error that occured when loading the champion into the database\n";
		championThatPass = convertInt(loopCounter);
		reasonForError += "The amount of Champion that was sucesfully loaded into the data base was: ";
		reasonForError += championThatPass;
		reasonForError += "\n";
		reasonForError += "The amount of champion that was not sucessful in loading from the database was: \n";
		errorCountForChampionContainer = convertInt(totalError);
		reasonForError += "Ran into ";
		reasonForError += errorCountForChampionContainer;
		reasonForError += " Numbers of errors\n";

	}
	ifs.close();
	return reasonForError;
}



string Container::assignChampionValues(int pipeNumber, string value, Champions& newChampion)
{
	string validatingMessage = "";
	bool errorMessage = false; 
	int numberValue = 0; 
	if (pipeNumber == 1)
	{
		errorMessage = newChampion.SetChampName(value); 
	}
	else if (pipeNumber == 2)
	{
		errorMessage = newChampion.SetChampMainRole(value); 
	}
	else if (pipeNumber == 3)
	{
		errorMessage = newChampion.SetChampSubRole(value);
	}
	else if (pipeNumber == 4)
	{
		numberValue = atoi(value.c_str()); 
		errorMessage = newChampion.SetChampAD(numberValue);
	}
	else if (pipeNumber == 5)
	{
		numberValue = atoi(value.c_str());
		errorMessage = newChampion.SetChampDef(numberValue); 
	}
	else if (pipeNumber == 6)
	{
		numberValue = atoi(value.c_str());
		errorMessage = newChampion.SetChampAP(numberValue);
	}
	else if (pipeNumber == 7)
	{
		numberValue = atoi(value.c_str());
		errorMessage = newChampion.SetChampDiff(numberValue);

	}
	else if (pipeNumber == 8)
	{
		numberValue = atoi(value.c_str());
		errorMessage = newChampion.SetChampIP(numberValue); 
	}
	else if (pipeNumber == 9)
	{
		numberValue = atoi(value.c_str());
		errorMessage = newChampion.SetChampRP(numberValue); 
	}

	if (errorMessage == false)
	{
		validatingMessage += "Error in reading from database\n";
	}

	return validatingMessage; 
}



string Container::createNameSpaces(string& Name)
{
	string retString = ""; 

	if (Name.length() == 15)
	{
		Name += " ";
	}
	else if (Name.length() == 14)
	{
		Name += "  ";
	}
	else if (Name.length() == 13)
	{
		Name += "   ";
	}
	else if (Name.length() == 12)
	{
		Name += "    ";
	}
	else if (Name.length() == 11)
	{
		Name += "     ";
	}
	else if (Name.length() == 10)
	{
		Name += "      ";
	}
	else if (Name.length() == 9)
	{
		Name += "       ";
	}
	else if (Name.length() == 8)
	{
		Name += "        ";
	}
	else if (Name.length() == 7)
	{
		Name += "         ";
	}
	else if (Name.length() == 6)
	{
		Name += "          ";
	}
	else if (Name.length() == 5)
	{
		Name += "           ";
	}
	else if (Name.length() == 4)
	{
		Name += "            ";
	}
	else if (Name.length() == 3)
	{
		Name += "             ";
	}
	else if (Name.length() == 2)
	{
		Name += "              ";
	}
	else if (Name.length() == 1)
	{
		Name += "               ";
	}
	else if (Name.length() == 0)
	{
		Name += "                ";
	}
	retString += Name; 

	return retString; 
}




string Container::createSpaces(string& Type)
{
	string retString = ""; 
	
	if (Type == "N/A")
	{
		Type += "\t";
	}
	else if ((Type == "Mage") || (Type == "Tank"))
	{
		Type += "    ";
	}
	else if ((Type == "Fighter") || (Type == "Support"))
	{
		Type += " ";
	}

	retString += Type;

	return retString;
}



ifstream Container::dbaseFileInput;



