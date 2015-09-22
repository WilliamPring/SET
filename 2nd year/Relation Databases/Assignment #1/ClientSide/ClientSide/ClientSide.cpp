#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#define DEFAULT_PORT "27016"
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
int recvbuflen = DEFAULT_BUFLEN;
int exitProgram = 0;
char recvbuf[DEFAULT_BUFLEN];

bool showMenu = true;

/******************** Function Prototypes ********************/
void generateInformation(char *temp);
void handleInsert(LPVOID lpParam);

string randomDateGenerator(void);
string randomName(void);

/****************************************/

int main(int argc, char* argv[])
{
	int iResult;

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

	// Receive data until the server closes the connection
	printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");

	do {
		memset(recvbuf, 0, strlen(recvbuf));
		iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);

		if (strcmp(recvbuf, "Server Closed") == 0)
		{
			printf("\n>>Server Shutdown\n");
			break;
		}

		if (iResult > 0)
		{
			if (strcmp(recvbuf, "Server Closed") == 0)
			{
				exit(0);
			}

			if (showMenu == true)
			{
				printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
			}
			else
			{
				printf("%s\n", recvbuf);
				showMenu = true;
			}
		}
		else if (iResult == 0)
		{
			printf("Connection closed\n");
		}
	} while (iResult > 0);

	exit(0);

	return 0;
}


DWORD WINAPI typeInfoThread(LPVOID lpParam)
{
	bool loopCondition = true;
	bool menuOption = true;

	int ConnectSocket = (int)lpParam;
	int numberChosen = 0;
	int errorCheck = 0; //Checks for errors when trying to shutdown and close the sockets 

	char buffer[DEFAULT_BUFLEN] = ""; //Used to store user input and to perform error checking 

	while (menuOption)
	{
		fgets(buffer, 1024, stdin);
		*strchr(buffer, '\n') = '\0';
		send(ConnectSocket, buffer, (int)strlen(buffer), 0);
		numberChosen = atoi(buffer);

		switch (numberChosen)
		{
		case 1: //Inserting new member into database, error check
		{
			handleInsert(lpParam);
			break;
		}
		case 2:
		{
			printf("You've chosen 2\n");
			fgets(buffer, 1024, stdin);
			*strchr(buffer, '\n') = '\0';
			send(ConnectSocket, buffer, (int)strlen(buffer), 0);
			break;
		}
		case 3:
		{
			fgets(buffer, 1024, stdin);
			*strchr(buffer, '\n') = '\0';
			send(ConnectSocket, buffer, (int)strlen(buffer), 0);
			showMenu = false;
			//memset(recvbuf, 0, recvbuflen);
			//recv(ConnectSocket, recvbuf, recvbuflen, 0);
			//printf("%s\n", recvbuf);
			break;
		}
		case 4: //Disconnect client
		{
			menuOption = false;
			// shutdown the send half of the connection since no more data will be sent
			send(ConnectSocket, buffer, (int)strlen(buffer), 0);
			errorCheck = shutdown(ConnectSocket, SD_BOTH);
			closesocket(ConnectSocket);
			if (errorCheck == SOCKET_ERROR)
			{
				printf("shutdown failed: %d\n", WSAGetLastError());
				closesocket(ConnectSocket);
				WSACleanup();
				return 1;
			}
			printf("You have officially disconnected, goodbye!\n");
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
void handleInsert(LPVOID lpParam)
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
			int amountOfEntry = 0;
			int i = 0;
			amountOfEntry = atoi(tempAmount.c_str());
			for (i; i < amountOfEntry; i++)
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
	
	printf("Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n");
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
