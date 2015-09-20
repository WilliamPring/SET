#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#define DEFAULT_PORT "27015"
#define DEFAULT_BUFLEN 1024

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <string.h>
#include <iostream>
#include <vector>
#include <string>
#include <time.h>
#pragma comment(lib, "Ws2_32.lib")
#pragma warning(disable:4996)

using namespace std; 
string randomDateGenerator();
DWORD WINAPI typeInfoThread(LPVOID lpParam);
HANDLE newThread;
int recvbuflen = DEFAULT_BUFLEN;
int exitProgram = 0; 
char recvbuf[DEFAULT_BUFLEN];
bool menuOption = false; 

//Function Prototypes
char* userInput(void);
bool CheckLetter(char* answer);
bool birthdate(char* answer);



string randomName()
{
	string result = "";
	static const char alphanum[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "abcdefghijklmnopqrstuvwxyz";
	char str[121] = "";
	//srand(time(0));

	int letters = rand() % (123 - 65) + 65;

	//srand(time(0));

	int len = rand() % (15 - 7) + 7;

	for (int i = 0; i < len; i++) {
		str[i] = alphanum[rand() % (sizeof(alphanum) - 1)];
	}

	str[len] = 0;

	result = str;

	return result;
}

		




string randomDateGenerator()
{
	string totalDate = "";
	string sMonth = "";
	string sDay = "";
	string sYear = "";
	srand(time(NULL));
	int month = 0;
	int day = 0;
	int year = 0;
	month = rand() % 12 + 1;
	day = rand() % 31 + 1;
	year = rand() % (2015 - 1915) +1915;
	sDay = to_string(day);
	sYear = to_string(year);
	sMonth = to_string(month);
	totalDate += (sYear) + ("/" + sMonth) + ("/" + sDay);

	return totalDate;


}


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
	
	do {
		memset(recvbuf, 0, strlen(recvbuf));
		iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);

		if (strcmp(recvbuf, "Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n") == 0)
		{
			menuOption = true; 
		}

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

			printf("%s\n", recvbuf);

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
	bool condition = true; 
	int ConnectSocket = (int)lpParam; 
	int numberChosen = 0;	
	int errorCheck = 0; //Checks for errors when trying to shutdown and close the sockets 
	char buffer[1024] = ""; //Used to store user input and to perform error checking 
	char officialString[1024] = ""; 
	char* answer = ""; 
	char* serverMenu = "Welcome to Database Server\n\n1-INSERT\n2-UPDATE\n3-FIND\n4-DISCONNECT\n";
	string fullData = "";
	string firstName = "";
	string lastName = "";

	while (condition)
	{
		fgets(buffer, 1024, stdin);
		*strchr(buffer, '\n') = '\0';
		send(ConnectSocket, buffer, (int)strlen(buffer), 0);
		numberChosen = atoi(buffer);

		if (menuOption == true)
		{
			switch (numberChosen)
			{
				case 1: //Inserting new member into database, error check
				{
					string tempAmount = "";
					printf("How much data do you want to enter in the database");
					getline(cin, tempAmount);

					send(ConnectSocket, tempBufferData, strlen(tempBufferData), 0);

					int amountOfEntry = 0;
					amountOfEntry = atoi(tempAmount.c_str());
					for (int i = 0; i < amountOfEntry; i++)
					{
						string sNewEntryData = "";
						string date = "";
						string firstName = "";
						string lastName = "";
						date = randomDateGenerator();
						firstName = randomName();
						lastName = randomName();
						sNewEntryData += (firstName + "|") + (lastName + "|") + (date + "|") + "\n";
						char tempBufferData[50] = "";
						strcpy(tempBufferData,sNewEntryData.c_str());
						send(ConnectSocket, tempBufferData, strlen(tempBufferData), 0);
					}
					break;
				}
				case 2:
				{
					printf("You've chosen 2");
					break;
				}
				case 3:
				{
					printf("You've chosen 3");
					break;
				}
				case 4: //Disconnect client
				{
					condition = false;
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
char* userInput(void)
{
	char buffer[1024] = { 0 };
	fgets(buffer, 1024, stdin);
	*strchr(buffer, '\n') = '\0';
	return buffer;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
bool CheckLetter(char* answer)
{
	string input = "";
	bool error = true;
	bool inputStatus = true;
	bool retry = false;
	bool status = true;
	char* buffer = ""; 

	while (status)
	{
		if (retry == true)
		{
			memset(buffer, 0, strlen(buffer));
			fgets(buffer, 1024, stdin);
			*strchr(buffer, '\n') = '\0';
			retry = false;
		}
		//check to see if the length of the string is greater then 20
		if (strlen(buffer) > 20)
		{
			//if it is then you will be prompt with a question if you wish to continue or not
			printf("inappropriate ammount of characters\n");
			printf("Do you wish to try again n (no) or click anybutton for yes\n");
			getline(cin, input);
			//if the peroson dose not wish to continue then it will end the loop
			if (input == "n")
			{
				status = false;
				error = false;
				continue;
			}
			else
			{
				//if the person wish to try again there will be an if statement that will allow them because 
				//the retry bool will change
				status = true;
				retry = true;
				continue;
			}
		}
		//if everything is correct such as the lenght of the string it will check to see if the 
		//the string is a number or not
		for (unsigned int i = 0; i < strlen(buffer); i++)
		{
			//check to see if the letter is a digit or not
			if (isdigit(buffer[i]) != 0)
			{
				//if it si then promt them with a message asking them do they wish to try again
				printf("That is not a letter \n");
				printf("Do you wish to try again? \n");
				getline(cin, input);
				//if not the program will end
				if (input == "n")
				{
					error = false;
					inputStatus = true;
					error = false;
					break;
				}
				else
				{
					memset(buffer, 0, strlen(buffer));
					fgets(buffer, 1024, stdin);
					*strchr(buffer, '\n') = '\0';
					inputStatus = false;
					break;
				}
			}
			inputStatus = true;
		}
		if (inputStatus == true)
		{
			break;
		}
	}
	return error;
}



/*
Function:
Description:
Parameter(s):
Return:
*/
bool birthdate(char* answer)
{
	string input = "";
	char tempBuffer[121] = "";
	char buffer[1024] = { 0 };
	bool returnStatus = true;
	string totalStringBirthdate = "";
	totalStringBirthdate = answer;
	string month = "";
	string day = "";
	int nMonth = 0;
	int nDay = 0;
	int nYear = 0;
	bool status = true;
	bool inputStatus = false;
	string year = "";
	while (status)
	{
		if (inputStatus == true)
		{
			memset(buffer, 0, strlen(buffer));
			fgets(buffer, 1024, stdin);
			*strchr(buffer, '\n') = '\0';
			inputStatus = false;
		}
		if ((totalStringBirthdate.c_str()[4] == '/') && (totalStringBirthdate.c_str()[7] == '/') && (totalStringBirthdate.length() == 10))
		{
			year = totalStringBirthdate.substr(0, 4);
			month = totalStringBirthdate.substr(5, 2);
			day = totalStringBirthdate.substr(9, 2);
			//removing the first /
			totalStringBirthdate.erase(4, 1);
			//removing the second /
			totalStringBirthdate.erase(6, 1);
			strcpy(tempBuffer, totalStringBirthdate.c_str());
			for (unsigned int i = 0; i < strlen(tempBuffer); i++)
			{
				//check to see if the letter is a digit or not
				if (isalpha(tempBuffer[i]) != 0)
				{
					//if it si then promt them with a message asking them do they wish to try again
					printf("That is not a number \n");
					printf("Do you wish to try again? click n for no or click any button to try again\n");
					getline(cin, input);
					//if not the program will end
					if (input == "n")
					{
						break;
					}
					else
					{
						inputStatus = true;
						break;
					}
				}
			}
			if (inputStatus == false)
			{
				nYear = atoi(year.c_str());
				nDay = atoi(day.c_str());
				nMonth = atoi(month.c_str());

				if (((nYear <= 2015) && (nYear >= 1915)) && ((nMonth >= 1) && (nMonth <= 12)) && ((nDay >= 1) && (nDay <= 31)))
				{
					break;
				}
				else
				{
					printf("That was the wrong format. click n (no) or click any button to try again ");
					getline(cin, input);
					//if not the program will end
					if (input == "n")
					{
						break;
					}
					else
					{
						inputStatus = true;
						continue;
					}
				}
			}
		}
		else
		{
			printf("That was the wrong format. click n (no) or click any button to try again ");
			getline(cin, input);
			//if not the program will end
			if (input == "n")
			{
				break;
			}
			else
			{
				inputStatus = true;
				continue;
			}

		}
	}
	return returnStatus;
}