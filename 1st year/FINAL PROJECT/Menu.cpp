/*
File name:
Project:
By:
Date:
Description:
*/

#include "Menu.h"

Menu::Menu(void)
{

}


bool Menu::CheckNumber(string &userInput, char* message)
{
	string temp = "";
	string yesOrNo = "";
	temp = userInput;

	int number = 0;

	char checkIfDigit[121] = "";
	char test[121] = "";

	bool status = true;
	bool run = true;
	bool tryAgain = false;

	while (run)
	{
		if (tryAgain == true)
		{
			printf("%s\n", message);
			getline(cin, temp);
			userInput = temp;

			tryAgain = false;
		}

		strcpy(test, temp.c_str());
		number = atoi(temp.c_str());
		sprintf(checkIfDigit, "%d", number);

		if (strcmp(checkIfDigit, test)==0)
		{
			status = true;
			run = false;
			continue;
		}
		else
		{
			printf("What you have enter is not valid! \n");
			printf("Do you wish to try again? y (yes), n (no)\n");
			getline(cin, yesOrNo);
			if (yesOrNo == "y")
			{
				tryAgain = true;
				continue;
			}
			else if (yesOrNo == "n")
			{
				run = false;
				status = false;
				continue;
			}
			else
			{
				printf("Input not valid !\n");
			}
		}
	}

	return status;
}



void Menu::RunProgram(Container lolChampions)
{
	bool status = true;
	bool fileStatus = true; 
	string openingFileMessages = "";
	//Load the database
	openingFileMessages = lolChampions.readEntireFile("williampri.txt");
	printf("%s\n", openingFileMessages.c_str());
	if (fileStatus != false)
	{
		//Read everything from the database file
		
		while (1)
		{
			string key = "";
			string contentInContainer = "";
			string statusMessage = "";
			bool executed = true;
			string champName = "";
			string champMainRole = "";
			string champSubRole = "";
			string champAD = "";
			string champAP = "";
			string champDEF = "";
			string champDIFF = "";
			string champIP = "";
			string chapRP = "";
			string numChampions = "";
			char buffer[121] = "";
			bool checkStatus = true;
			char bufferChoseSort[256] = "";
			printf("Press one of the followng:\n");
			printf("1) Create x amount of champions\n");
			printf("2) Create your own personal champion\n");
			printf("3) Sort your champions\n");
			printf("4) Display your champions\n");
			printf("5) Delete your champions\n");
			printf("6) Save your champions and store in database\n");
			getline(cin, key);
			if (key == "1")
			{
				system("cls");
				printf("Enter in the number of characters you would like to create?\n");
				getline(cin, numChampions);
				checkStatus = CheckNumber(numChampions, buffer);

				if (checkStatus == false)
				{
					continue;
				}
				string retMessage = "";
				retMessage = lolChampions.createChampions(numChampions);
				system("cls");
				printf("%s", retMessage.c_str());
			}
			else if (key == "2")
			{
				system("cls");
				printf("Creating a League of Legends Champions!\n");
				status = GetUserInput(champName, champMainRole, champSubRole, champAD, champAP, champDEF, champDIFF, champIP, chapRP);
				if (status == false)
				{
					system("cls");
					continue;
				}
				else
				{
					statusMessage = lolChampions.createUserLOLChampion(champName, champMainRole, champSubRole, champAD, champAP, champDEF, champDIFF, champIP, chapRP);
					printf("%s\n", statusMessage.c_str());
				}
			}
			else if (key == "3")
			{
				system("cls"); //Clear the screen
				sprintf(bufferChoseSort, "How do you want your Champions to be sorted by: \n 1) Champion name\n 2) Champion stats\n 3) Prices(IP or RP)\n 4) Go back to the main menu\n");
				printf("%s", bufferChoseSort);
				getline(cin, key);
				key = choosingSort(key, lolChampions, bufferChoseSort);
				printf("%s\n", key.c_str());
			}
			else if (key == "4")
			{
				system("cls");
				contentInContainer = lolChampions.displayContainer();
			}
		
			else if (key == "5")
			{
				system("cls"); 
				printf("What do you wish to do?\n");
				printf("1) Delete local Champions that currently is not in data base\n");
				printf("2) Delete the entire data base\n");
				printf("3) Delete a single champion\n");

				getline(cin, key);
				statusMessage = deleteContainerOrDatabase(key, lolChampions);
				printf("%s\n", statusMessage.c_str());

			}
			else if (key == "6")
			{
				statusMessage = storeContainer(lolChampions);
				printf("%s", statusMessage.c_str());
			}
			else
			{
				system("cls");
				printf("You did not enter a valid option, please try again.\n");
			}

		}
	}
}

string Menu::storeContainer(Container &lolChampion)
{
	string statusMessage = "";
	statusMessage = lolChampion.saveDataBase();
	return statusMessage;
}


string Menu::deleteContainerOrDatabase(string whichOneToDelete, Container &lolChampion)
{
	string statusMessage = "";
	while (1)
	{
		if (whichOneToDelete == "1")
		{
			statusMessage = lolChampion.deleteContainer();
			break;
		}
		else if (whichOneToDelete == "2")
		{
			statusMessage = lolChampion.deleteDatabase();
			break;
		}
		else if (whichOneToDelete == "3")
		{
			system("cls");
			printf("Enter the champion's name you wish to delete!\n");
			statusMessage = lolChampion.deleteAChampion();
			break;
		}
		else
		{
			system("cls");
			printf("ERROR you did not have a valid input\n");
			printf("Do you wish to try again?\n");
			printf("y(yes) or any button to quit!\n");
			getline(cin, whichOneToDelete);
			if (whichOneToDelete == "y")
			{
				continue;
			}
			else
			{
				break;
			}
		}
	}

	return statusMessage;
}

string Menu::choosingSort(string userInput, Container &lolChampion, string menuForTheSort)
{
	bool valid = true;
	bool status = true;
	char buffer[256] = "";
	string returnString = "";

	while (status)
	{
		if (userInput == "1")
		{
			system("cls");
			returnString = lolChampion.sortLOLChampion("ChampName");
			break;
		}
		else if (userInput == "2")
		{
			while (1)
			{
				memset(buffer, 0, sizeof(buffer));
				sprintf(buffer, "Press 1: to sort by Attack Damage\nPress 2: to sort by Attack Power\nPress 3: to sort by Defense\nPress 4: to sort by Difficulty\nPress 5: to quit\n");
				system("cls");
				printf("%s", buffer);
				getline(cin, userInput);
				if (userInput == "1")
				{
					system("cls");
					returnString = lolChampion.sortLOLChampion("AD");
					status = false;
					break;
				}
				else if (userInput == "2")
				{
					system("cls");
					returnString = lolChampion.sortLOLChampion("AP");
					status = false;
					break;
				}
				else if (userInput == "3")
				{
					system("cls");
					returnString = lolChampion.sortLOLChampion("DEF");
					status = false;
					break;

				}
				else if (userInput == "4")
				{
					system("cls");
					returnString = lolChampion.sortLOLChampion("DIFF");
					status = false;
					break;

				}
				else if (userInput == "5")
				{
					status = false;
					valid = false;
					system("cls");
					break;
				}
				else
				{
					system("cls");
					printf("Invalid input!\n");
					printf("Do you wish to try again?\ny(yes) or any other character to return to the main menu\n");

					getline(cin, userInput);

					if (userInput == "y")
					{
						system("cls");
						printf("%s", buffer);
						getline(cin, userInput);
						continue;
					}
					else
					{
						status = false;
						valid = false;
						system("cls");
						break;
					}

				}
			}
		}
		else if (userInput == "3")
		{
			memset(buffer, 0, sizeof(buffer));
			system("cls");
			sprintf(buffer, "Press 1: to sort by RP\nPress 2: to sort by IP\n");
			while (1)
			{
				printf("%s", buffer);
				getline(cin, userInput);
				if (userInput == "1")
				{
					returnString = lolChampion.sortLOLChampion("RP");
					status = false;
					break;
				}
				else if (userInput == "2")
				{
					returnString = lolChampion.sortLOLChampion("IP");
					status = false;
					break;
				}
				else
				{
					printf("Not valid input try agan? \n");
					printf("Do you wish to try again? y(yes) or any character to go back to the main menu\n");
					getline(cin, userInput);
					if (userInput == "yes")
					{
						printf("%s", buffer);
						getline(cin, userInput);
						break;;
					}
					else
					{
						status = false;
						valid = false;
						break;
						system("cls");
					}
				}
			}
		}
		else if (userInput == "4")
		{
			status = false;
			valid = false;
			system("cls");
			break;
		}
		else
		{
			system("cls");
			printf("Not a valid input!\n");
			printf("Do you wish to try again?\ny(yes) or press any other character to go back to the main menu\n");
			getline(cin, userInput);
			if (userInput == "y")
			{			
				printf("%s\n", menuForTheSort.c_str());
				getline(cin, userInput);
				//break;
				continue;
			}
			else
			{
				status = false;
				valid = false;
				break;
				system("cls");
			}
		}
	}
	return returnString;	
}








bool Menu::GetUserInput(string &Name, string &MainRole, string &SubRole, string &AD, string &AP, string &DEF, string &DIFF, string &IP, string &RP)
{
	char buffer[121] = "";
	bool status = true;
	bool returnType = true;
	while (status = true)
	{
		printf("Enter the champions Name!\n");
		sprintf(buffer,"Enter the champions Name!\n");
		getline(cin, Name);
		returnType = CheckLetter(Name, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champions Main Role!\n");
		sprintf(buffer,"Enter the champions Main Role!\n");
		printf("Fighter, Mage, Assassin, Tank, Support, Marksman \n");
		getline(cin, MainRole);
		returnType = CheckLetter(MainRole, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champions Main Sub Role!\n");
		printf("Fighter, Mage, Assassin, Tank, Support, Marksman, N/A \n");
		sprintf(buffer,"Enter the champions Main Sub Role!\n");
		getline(cin, SubRole);
		returnType = CheckLetter(SubRole, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champion AD from a scale 1-10!\n");
		sprintf(buffer,"Enter the champion AD from a scale 1-10!\n");
		getline(cin, AD);
		returnType = CheckNumber(AD, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champion AP from a scale 1-10!\n");
		sprintf(buffer,"Enter the champion AP from a scale 1-10!\n");
		getline(cin, AP);
		returnType = CheckNumber(AP, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}

		printf("Enter the champion Defense from a scale 1-10!\n");
		sprintf(buffer,"Enter the champion Defense from a scale 1-10!\n");

		getline(cin, DEF);
		returnType = CheckNumber(DEF, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champion Difficulty from a scale 1-10!\n");
		sprintf(buffer, "Enter the champion Difficulty from a scale 1-10!\n");
		getline(cin, DIFF);
		returnType = CheckNumber(DIFF, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champion IP from a scale 450-6300\n");
		sprintf(buffer, "Enter the champion IP from a scale 450-6300\n");
		getline(cin, IP);
		returnType = CheckNumber(IP, buffer);
		if (returnType == false)
		{
			status = false;
			break;
		}
		printf("Enter the champion RP from a scale 260-975!\n");
		sprintf(buffer, "Enter the champion RP from a scale 260-975!\n");
		getline(cin, RP);
		returnType = CheckNumber(RP, buffer);
		system("cls");
		if (returnType == false)
		{
			status = false;
			break;
		}
		else
		{
			status = true;
			break;
		}
	}

	return status;
}



bool Menu::CheckLetter(string &userInput, char * message)
{
	string tryAgain = "";
	tryAgain = userInput;
	bool status = true;
	bool run = true;
	bool isLetter = true;
	while (run)
	{
		if (isLetter == false)
		{
			getline(cin, tryAgain);
			userInput = tryAgain;
		}
		for (size_t i = 0; i < tryAgain.length(); i++)
		{
			if ((isdigit(tryAgain.c_str()[i])) != 0)
			{
				printf("Not a string do you want to try again?\n");
				printf("y(yes), n(no) \n");
				getline(cin, tryAgain);
				if (tryAgain == "y")
				{
					run = true; 
					printf("%s\n", message);
					getline(cin, tryAgain);
					userInput = tryAgain;
					continue;
				}
				else
				{
					status = false;
					isLetter = false;
					run = false;
				}
			}
		}
		if (isLetter == true)
		{
			run = false;
			status = true;
			continue;
		}
	}
	return status;
}



