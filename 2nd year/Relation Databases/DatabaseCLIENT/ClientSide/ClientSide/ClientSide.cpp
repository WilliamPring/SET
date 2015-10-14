/*


*/
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#define DEFAULT_PORT "40000"
#define DEFAULT_BUFLEN 1024

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <string.h>
#include <iostream>
#include <vector>
#include <string>
#include <regex>
#include <time.h>

#pragma comment(lib, "Ws2_32.lib")
#pragma warning(disable:4996)

using namespace std;

/******************** Global Variables ********************/
string randomDateGenerator();
DWORD WINAPI typeInfoThread(LPVOID lpParam);
HANDLE newThread;

/******************** Function Prototypes ********************/
void generateInformation(char *temp);
void handleInsertClient(LPVOID lpParam);
void handleUpdateClient(LPVOID lpParam);

bool verifyNumber(string& numberEntered);
bool verifyMenuNumber(string& numberEntered);

string randomDateGenerator(void);
string randomName(void);

/****************************************/

int main(int argc, char* argv[])
{
	int recvbuflen = DEFAULT_BUFLEN;
	int iResult = 0;
	char recvbuf[DEFAULT_BUFLEN] = "";
	char menuScreen[1024] = "Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n";

	WSADATA wsaData;
	SOCKET ConnectSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed: %d\n", iResult);
		return 1;
	}

	// Resolve the server address and port
	iResult = getaddrinfo(argv[1], DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Attempt to connect to the first address returned by
	// the call to getaddrinfo
	ptr = result;

	// Create a SOCKET for connecting to server
	ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
		ptr->ai_protocol);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Error at socket(): %ld\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}

	// Connect to server.
	iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		closesocket(ConnectSocket);
		ConnectSocket = INVALID_SOCKET;
	}

	// Should really try the next address returned by getaddrinfo
	// if the connect call failed
	// But for this simple example we just free the resources
	// returned by getaddrinfo and print an error message

	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Unable to connect to server!\n");
		WSACleanup();
		return 1;
	}

	newThread = CreateThread(NULL, 0, typeInfoThread, (LPVOID)ConnectSocket, 0, NULL);
	printf("%s\n", menuScreen);

	do {
		memset(recvbuf, 0, strlen(recvbuf));
		recv(ConnectSocket, recvbuf, recvbuflen, 0);

		if (strcmp(recvbuf, "Server Closed") == 0)
		{
			printf("\n>>Server Shutdown\n");
			break;
		}

		printf("%s\n", recvbuf);

		if (strcmp(recvbuf, "<<Disconnected from server>>") == 0)
		{
			printf("You have officially disconnected, goodbye!\n");
			break;
		}

		printf("%s\n", menuScreen);
	} while (1);

	exit(0);

	return 0;
}


DWORD WINAPI typeInfoThread(LPVOID lpParam)
{
	bool loopCondition = true;
	bool menuOption = true;
	bool validNumber = true;
	bool validMenuNumber = true;

	int ConnectSocket = (int)lpParam;
	int numberChosen = 0;
	int errorCheck = 0; //Checks for errors when trying to shutdown and close the sockets 

	char buffer[DEFAULT_BUFLEN] = ""; //Used to store user input and to perform error checking 
	int recvbuflen = DEFAULT_BUFLEN;
	char recvbuf[DEFAULT_BUFLEN] = "";

	string numberInput = "";
	string menuNumberInput = "";

	while (menuOption)
	{
		while (1)
		{
			menuNumberInput.clear();
			getline(cin, menuNumberInput);
			validMenuNumber = verifyMenuNumber(menuNumberInput);
			cout << menuNumberInput << endl;

			if (validMenuNumber == true)
			{
				if (menuNumberInput == "4")
				{
					send(ConnectSocket, "|", (int)strlen("|"), 0);
				}

				send(ConnectSocket, menuNumberInput.c_str(), (int)strlen(menuNumberInput.c_str()), 0);
				validMenuNumber = false;
				system("cls");
				break;
			}
		}

		numberChosen = atoi(menuNumberInput.c_str());

		switch (numberChosen)
		{
		case 1:
		{
			//Inserting new member into database, error check
			handleInsertClient(lpParam);
			break;
		}
		case 2:
		{
			system("cls");

			while (1)
			{
				printf("Which member would you like to update?\n");
				numberInput.clear();
				getline(cin, numberInput);
				validNumber = verifyNumber(numberInput);

				if (validNumber == true)
				{
					send(ConnectSocket, numberInput.c_str(), (int)strlen(numberInput.c_str()), 0);
					validNumber = false;
					break;
				}
			}

			handleUpdateClient(lpParam);
			break;
		}
		case 3:
		{
			system("cls");

			while (1)
			{
				printf("Which member would you like to find?\n");
				numberInput.clear();
				getline(cin, numberInput);
				validNumber = verifyNumber(numberInput);

				if (validNumber == true)
				{
					send(ConnectSocket, numberInput.c_str(), (int)strlen(numberInput.c_str()), 0);
					validNumber = false;
					break;
				}
			}
			break;
		}
		case 4:
		{
			//Shutdown the send half of the connection since no more data will be sent
			menuOption = false; //Disconnect client
			send(ConnectSocket, "|", (int)strlen("|"), 0);
			errorCheck = shutdown(ConnectSocket, SD_BOTH);
			closesocket(ConnectSocket);
			if (errorCheck == SOCKET_ERROR)
			{
				printf("shutdown failed: %d\n", WSAGetLastError());
				closesocket(ConnectSocket);
				WSACleanup();
				return 1;
			}
			break;
		}
		default:
		{
			printf("Character unreadable, please try again.\n");
			break;
		}
		}
		memset(buffer, 0, strlen(buffer));
	}

	exit(0);
	return 0;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
bool verifyMenuNumber(string& numberEntered)
{
	int length = numberEntered.length();
	int actualNumber = atoi(numberEntered.c_str());
	bool retValue = true;

	for (int i = 0; i < length; i++)
	{
		if (!isdigit(numberEntered.c_str()[i]))
		{
			system("cls");
			printf("Invalid input! Must be a number only, try again!\n");
			printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
			retValue = false;
			break;
		}
	}

	if ((actualNumber == 0) || (actualNumber > 4))
	{
		system("cls");
		printf("\nInvalid menu option! Must be a number only, try again!\n");
		printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
		retValue = false;
	}

	return retValue;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
bool verifyNumber(string& numberEntered)
{
	int length = numberEntered.length();
	bool retValue = true;

	for (int i = 0; i < length; i++)
	{
		if (!isdigit(numberEntered.c_str()[i]))
		{
			printf("\nInvalid Input! Must be a number only, try again!\n");
			retValue = false;
			break;
		}
	}

	return retValue;
}


/*
Function:
Description:
Parameter(s):
Return:
*/
void handleUpdateClient(LPVOID lpParam)
{
	int socketNum = (int)lpParam;
	int year = 0;
	int month = 0;
	int day = 0;

	bool answerInteger = false;
	bool answerLetter = false;

	char testBuffer[1024] = "";
	string userInput = "";
	string sendingString = "";
	string firstName = "";
	string lastName = "";
	string dateOfBirth = "";
	//regex integer("^\d+$");
	//regex letters("\b\w{1,5}\b");

	system("cls");

	while (1)
	{
		printf("Enter in the first name of the member:\n(String must be within 5): ");
		firstName.clear();
		getline(cin, firstName);

		answerInteger = false;

		if (firstName.length() != 0)
		{
			for (unsigned int i = 0; i < firstName.length(); i++)
			{
				if (isdigit(firstName.at(i)))
				{
					answerInteger = true;
					break;
				}
			}
		}

		if (answerInteger == true || firstName.length() == 0 || firstName.length() > 5)
		{
			system("cls");
			printf("String entered is invalid, please try again..\n");
			continue;
		}

		if (firstName.length() == 1)
		{
			firstName += "    ";
			sendingString += firstName + "|";
		}
		else if (firstName.length() == 2)
		{
			firstName += "   ";
			sendingString += firstName + "|";
		}
		else if (firstName.length() == 3)
		{
			firstName += "  ";
			sendingString += firstName + "|";
		}
		else if (firstName.length() == 4)
		{
			firstName += " ";
			sendingString += firstName + "|";
		}
		else
		{
			sendingString += firstName + "|";
		}

		//system("cls");
		printf("\nGood, now enter the last name of the member:\n(String must be within 5): ");
		lastName.clear();
		getline(cin, lastName);

		answerInteger = false;

		if (lastName.length() != 0)
		{
			for (unsigned int i = 0; i < lastName.length(); i++)
			{
				if (isdigit(lastName.at(i)))
				{
					answerInteger = true;
					break;
				}
			}
		}

		if (answerInteger == true || lastName.length() == 0 || lastName.length() > 5)
		{
			system("cls");
			printf("String entered is invalid, you must start over..\n");
			continue;
		}

		if (lastName.length() == 1)
		{
			lastName += "    ";
			sendingString += lastName + "|";
		}
		else if (lastName.length() == 2)
		{
			lastName += "   ";
			sendingString += lastName + "|";
		}
		else if (lastName.length() == 3)
		{
			lastName += "  ";
			sendingString += lastName + "|";
		}
		else if (lastName.length() == 4)
		{
			lastName += " ";
			sendingString += lastName + "|";
		}
		else
		{
			sendingString += lastName + "|"; //Append to sending string
		}

		break; //Leave the loop, first and last name were good 
	}

	//system("cls");

	while (1)
	{
		//The calender date checking is a bit more explicit
		printf("\nEnter in the year of date of birth:\n");
		getline(cin, userInput);
		year = atoi(userInput.c_str());

		if (!(year >= 1915 && year <= 2015))
		{
			system("cls");
			printf("Year was not acceptable, try again..\n");
			continue;
		}

		dateOfBirth += userInput + '-';
		userInput.clear();

		printf("\nEnter in the month of date of birth:\n");
		getline(cin, userInput);
		month = atoi(userInput.c_str());

		if (!(month <= 12 && month > 0))
		{
			system("cls");
			printf("Month was not acceptable, try again..\n");
			continue;
		}

		if (userInput.length() == 1)
		{
			userInput = "0" + userInput;
		}
		dateOfBirth += userInput + '-';

		userInput.clear();
		printf("\nEnter in the day of date of birth:\n");
		getline(cin, userInput);
		day = atoi(userInput.c_str());

		if (!(day <= 31 && day > 0))
		{
			system("cls");
			printf("Day was not acceptable, try again..\n");
			continue;
		}

		if (userInput.length() == 1)
		{
			userInput = "0" + userInput;
		}
		dateOfBirth += userInput + "|";
		break;
	}

	sendingString = "|" + sendingString + dateOfBirth;

	strcpy(testBuffer, sendingString.c_str());
	send(socketNum, testBuffer, (int)strlen(testBuffer), 0);
	//system("cls");
	//printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
	return;
}





/*
Function:
Description:
Parameter(s):
Return:
*/
void handleInsertClient(LPVOID lpParam)
{
	string fullData = "";
	string firstName = "";
	string lastName = "";
	regex integer("(\\+|-)?[[:digit:]]+");
	string tempAmount = "";
	int socketNum = (int)lpParam;
	char tempBufferData[DEFAULT_BUFLEN] = "";
	bool continueLoop = true;
	while (continueLoop)
	{
		printf("How much data do you wish to enter in the database: ");
		getline(cin, tempAmount);

		if (regex_match(tempAmount, integer))
		{
			//memset(tempBufferData, 0, strlen(tempBufferData));
			//strcpy(tempBufferData, tempAmount.c_str());
			//send(socketNum, tempBufferData, strlen(tempBufferData), 0);
			int amountOfEntry = atoi(tempAmount.c_str());
			for (int i = 0; i < amountOfEntry; i++)
			{
				generateInformation(tempBufferData);
				send(socketNum, tempBufferData, strlen(tempBufferData), 0);
				memset(tempBufferData, 0, strlen(tempBufferData));
			}
			send(socketNum, "End", strlen("End"), 0);
			continueLoop = false;
		}
		else
		{
			printf("That was not a number do want to try again!\n y(yes) or click anything to quit\n");
			getline(cin, tempAmount);
			if (tempAmount != "y")
			{
				continueLoop = false;
			}
		}
	}

	//system("cls");
	//printf("\nWelcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
	return;
}


/*
Function:
Description:
Parameter(s):
Return:
*/
void generateInformation(char *temp)
{
	string sNewEntryData = "";
	string date = "";
	string firstName = "";
	string lastName = "";
	date = randomDateGenerator();
	firstName = randomName();
	lastName = randomName();
	sNewEntryData += (firstName + "|") + (lastName + "|") + (date + "|") + "\n";
	strcpy(temp, sNewEntryData.c_str());
	return;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
string randomDateGenerator(void)
{
	string totalDate = "";
	string sMonth = "";
	string sDay = "";
	string sYear = "";
	int month = 0;
	int day = 0;
	int year = 0;
	month = rand() % 12 + 1;
	day = rand() % 31 + 1;
	year = rand() % (2015 - 1915) + 1915;
	sDay = to_string(day);
	if (sDay.length() == 1)
	{
		sDay = "0" + sDay;
	}
	sYear = to_string(year);
	sMonth = to_string(month);
	if (sMonth.length() == 1)
	{
		sMonth = "0" + sMonth;
	}
	totalDate += (sYear)+("/" + sMonth) + ("/" + sDay);
	return totalDate;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
string randomName(void)
{
	string result = "";
	const char alphanum[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "abcdefghijklmnopqrstuvwxyz";
	char str[DEFAULT_BUFLEN] = "";
	int letters = rand() % (123 - 65) + 65;
	int len = 5;

	for (int i = 0; i < len; i++)
	{
		str[i] = alphanum[rand() % (sizeof(alphanum) - 1)];
	}

	str[len] = 0;
	result = str;
	return result;
}
