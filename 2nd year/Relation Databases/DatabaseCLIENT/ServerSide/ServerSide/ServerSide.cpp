#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#define DEFAULT_PORT "40000"
#define MAX_BUF 1024
#define MAX_RECORD 40000

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <vector>
#include <iostream>
#include <fstream>
#include <cstdio>


#pragma comment(lib, "Ws2_32.lib")

using namespace std;

DWORD WINAPI readAndWriteThread(LPVOID lpParam);
DWORD WINAPI closeServer(LPVOID lpParam);

//The steps to create a server, create a socket, bind the socket, have the socket listen for connections, connect, read/write to client connected

//Global variables to ease complexity and allow for variables to be used in different threads 
int dBaseID = 1;
int test = 20;

HANDLE newThread; //Handle for new thread creation 
HANDLE closeSocketsThread;
HANDLE openMutex;
HANDLE idMutex;


vector<int> socketPool;
//vector<int>::iterator iter; 

void handleFind(string& finalString, char* idNumberString);
bool handleUpdate(string& updateFinalString, char* idNumberString, int& byteCounter);

int main()
{
	SOCKET ClientSocket; //Temporary socket variable to accept connections from clients 
	SOCKET ListenSocket = INVALID_SOCKET;
	WSADATA wsaData;
	int iResult;

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0)
	{
		printf("WSAStartup failed: %d\n", iResult);
		return 1;
	}

	struct addrinfo *result = NULL, *ptr = NULL, hints;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	//So now you have to resolve the local address and port so that it can be used for the server 

	iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);
	if (iResult != 0)
	{
		printf("getaddrinfo failed: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Create a SOCKET for the server to listen for client connections

	printf("Creating Socket...\n");

	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);

	if (ListenSocket == INVALID_SOCKET)
	{
		printf("Error at socket(): %ld\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}

	printf(">>Socket created successfully\n\n");

	//Now bind the socket 

	printf("Binding Socket...\n");

	// Setup the TCP listening socket
	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR)
	{
		printf("bind failed with error: %d\n", WSAGetLastError());
		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}

	printf(">>Socket binded successfully\n\n");

	//When the bind function completes, the address information obtained from getaddrinfo is no longer required
	//So just free the memory

	freeaddrinfo(result);

	//Now listen in on the socket, socket is bounded to an IP Address and port on the system, it's ready to go
	//Server must listen on that IP Address and port for incoming connection requests 

	//SOMAXCONN is a special constant that tells the Winsock provider to allow a maximum and reasonable number of pending 
	//connection into the queue 

	printf("Listening in on request...\n");

	//Create the mutex so that clients can use the server one at a time
	openMutex = CreateMutex(NULL, FALSE, NULL);
	idMutex = CreateMutex(NULL, FALSE, NULL);


	if (openMutex == NULL)
	{
		printf("CreateMutex error: %d\n", GetLastError());
		return 1;
	}

	//Now to accept a connection 

	//Continuous loop to allow multiple client connections 
	while (1)
	{
		if (listen(ListenSocket, SOMAXCONN) == SOCKET_ERROR)
		{
			printf("Listen failed with error: %ld\n", WSAGetLastError());
			closesocket(ListenSocket);
			WSACleanup();
			return 1;
		}

		// Accept a client socket
		ClientSocket = accept(ListenSocket, NULL, NULL);
		if (ClientSocket == INVALID_SOCKET)
		{
			printf("accept failed: %d\n", WSAGetLastError());
			closesocket(ListenSocket);
			WSACleanup();
			return 1;
		}

		socketPool.push_back(ClientSocket);

		newThread = CreateThread(NULL, 0, readAndWriteThread, (LPVOID)ClientSocket, 0, NULL);
		closeSocketsThread = CreateThread(NULL, 0, closeServer, (LPVOID)ClientSocket, 0, NULL);

		printf("Client Number [%d] Connected\n", ClientSocket);
	}

	return 0;
}




DWORD WINAPI readAndWriteThread(LPVOID lpParam)
{
	printf("\n>>Thread created successfully\n");

	int ClientSocket = (int)lpParam;

	char* defaultCase = "Character was unreadable\n\n";
	char* busyServer = "Server is busy at the moment, please wait\n";
	int	  numberChosen = 0;

	bool leaveLoop = false;
	bool leaveMutex = true;
	bool updateRecord = true;

	int loopCounter = 0;
	int byteCounter = 0;

	ofstream file;
	fstream newFile;

	string databaseID = "";
	string finalString = "";
	string updateString = "";

	char recvbuf[MAX_BUF] = "";
	int recvbuflen = MAX_BUF;

	do
	{
		memset(recvbuf, 0, strlen(recvbuf));
		recv(ClientSocket, recvbuf, recvbuflen, 0);

		if (strcmp(recvbuf, "|") == 0)
		{
			printf("Client Number [%d] Disconnected\n", ClientSocket);
			send(ClientSocket, "<<Disconnected from server>>", (int)strlen("<<Disconnected from server>>"), 0);
			break;
		}

		numberChosen = atoi(recvbuf);
		memset(recvbuf, 0, strlen(recvbuf));

		switch (numberChosen)
		{
		case 1:
		{
			//Insert # of records 

			memset(recvbuf, 0, strlen(recvbuf));
			WaitForSingleObject(openMutex, INFINITE);
			file.open("database.txt", ios::app);

			while (1)
			{
				recv(ClientSocket, recvbuf, recvbuflen, 0);

				if (strcmp(recvbuf, "End") == 0)
				{
					//leaveLoop = true; 
					break;
				}

				file << dBaseID << "|" << recvbuf;
				memset(recvbuf, 0, strlen(recvbuf));

				WaitForSingleObject(idMutex, INFINITE);
				if (dBaseID == MAX_RECORD)
				{
					send(ClientSocket, "\n<<Database is full>>\n", (int)strlen("\n<<Database is full>>\n"), 0);
					ReleaseMutex(idMutex);
					updateRecord = false;
					break;
				}

				dBaseID++;
				ReleaseMutex(idMutex);
			}
			file.close();
			ReleaseMutex(openMutex);

			if (updateRecord == true)
			{
				send(ClientSocket, "\n<<Database updated>>\n", (int)strlen("\n<<Database updated>>\n"), 0);
			}
			updateRecord = true;
			break; //Break out of case 1, from parent loop
		}


		case 2:
		{
			//function for updating the file
			memset(recvbuf, 0, strlen(recvbuf));
			recv(ClientSocket, recvbuf, recvbuflen, 0);
			databaseID = recvbuf;
			printf("ID entered: %s\n", recvbuf);

			WaitForSingleObject(openMutex, INFINITE);
			newFile.open("database.txt", ios::in | ios::out);

			updateString.clear();
			updateRecord = handleUpdate(updateString, recvbuf, byteCounter);

			if (updateRecord == true)
			{
				updateRecord = false;
				memset(recvbuf, 0, strlen(recvbuf));
				recv(ClientSocket, recvbuf, recvbuflen, 0);
				printf("%s\n", recvbuf);

				newFile.seekp(byteCounter);
				send(ClientSocket, "\n<<ID was updated>>\n", (int)strlen("\n<<ID was updated>>\n"), 0);
				newFile << databaseID + recvbuf;
				databaseID.clear();
			}
			else
			{
				send(ClientSocket, "\n<<ID could not be updated>>\n", (int)strlen("\n<<ID could not be updated\n>>"), 0);
			}

			newFile.close();

			ReleaseMutex(openMutex); //Open up the file for other clients 				
			break; //break out of case 2 parent loop 
		}
		case 3:
		{
			//function for finding a member in the file 
			memset(recvbuf, 0, strlen(recvbuf));
			recv(ClientSocket, recvbuf, recvbuflen, 0);

			printf("%s\n", recvbuf);

			WaitForSingleObject(openMutex, INFINITE);


			finalString.clear();
			handleFind(finalString, recvbuf);
			send(ClientSocket, finalString.c_str(), (int)strlen(finalString.c_str()), 0);

			ReleaseMutex(openMutex); //Open up the file for other clients 			
			break;
		}
		}
	} while (leaveLoop != true);

	return 0;
}



//Create another thread so that the application can be asynchronous to read and write responses 

/*
Thread: closeServer()
Description:
Parameter(s):
Return:
*/
DWORD WINAPI closeServer(LPVOID lpParam)
{
	string closeProg = "";

	while (1)
	{
		getline(cin, closeProg);
		//check to see if the program wants to shut down all the clients
		if (closeProg == "close")
		{
			printf("Closing Server Now!\n>>SERVER CLOSED\n");
			//seraches through a vector 
			for (vector<int>::iterator iter = socketPool.begin(); iter != socketPool.end(); ++iter)
			{
				//all client will receive this messages
				send(*iter, "Server Closed", (int)strlen("Server Closed"), 0);
				shutdown(*iter, SD_BOTH);
				closesocket(*iter);
			}

			//Delete the database 
			remove("C:\\Users\\Naween\\Desktop\\ServerSide\\ServerSide\\database.txt");
			printf("Deleted database file\n");
			exit(0);
			break;
		}
	}
	return 0;
}



/****************************************************/

/*
Function: handleUpdate()
Description:
Parameter(s):
Return:
*/
bool handleUpdate(string& updateFinalString, char* idNumberString, int& byteCounter)
{
	//int socketNum = (int)lpParam;
	int loopCount = 0;
	bool updateString = false;
	bool retValue = false;
	fstream inputFile;

	char recvbuf[512] = "";
	int recvbuflen = 512;

	string lineToRead = "";
	string idObtained = "";


	string findID = idNumberString;
	int enteredID = atoi(findID.c_str());

	WaitForSingleObject(openMutex, INFINITE);

	inputFile.open("database.txt", ios::in | ios::out);
	inputFile.clear();
 

	if (enteredID <= dBaseID)
	{
		//check the bit to see if it good to open
		if (inputFile.good())
		{
			//if it is get the line until getline fails
			while (getline(inputFile, lineToRead, '\n'))
			{
				//check the lenght of the line that was read
				loopCount = lineToRead.length();
				//loop the length of the line
				for (int i = 0; i < loopCount; i++)
				{
					if (idObtained == findID)
					{
						updateString = true;
						break; // Begin parsing the entire string
					}
					else if (lineToRead[i] == '|')
					{
						idObtained = "";
						break; // Leave loop and try new line
					}

					idObtained += lineToRead[i];
				}

				if (updateString == true) // MIGHTNEED TO PUT THIS OUT IN THE SERVER CASE 
				{

					retValue = true;
					break;
				}

				byteCounter += lineToRead.length() + 2;
			}
		}
		else
		{
			cout << "File unable to be opened" << endl;
		}
	}
	else
	{
		updateFinalString = "Not enough member ID in the database\n";
	}

	inputFile.close();
	ReleaseMutex(openMutex);
	return retValue;
}



/****************************************************/

/*
Function:
Description:
Parameter(s):
Return:
*/
void handleFind(string& finalString, char* idNumberString)
{
	//int socketNum = (int)lpParam;
	int pipeCounter = 0;
	int loopCount = 0;

	char recvbuf[512] = "";
	int recvbuflen = 512;

	bool parseString = false;

	ifstream ifs;

	string lineToRead = "";
	string memberID = "Member ID : ";
	string firstName = "First Name: ";
	string lastName = "Last Name: ";
	string birthDate = "Birth Date: ";
	string overall = "";
	string idObtained = "";

	//memset(recvbuf, 0, strlen(recvbuf));
	//recv(ClientSocket, recvbuf, recvbuflen, 0);
	string findID = idNumberString;
	int idNumber = atoi(findID.c_str());

	//WaitForSingleObject(openMutex, INFINITE);

	ifs.open("database.txt");

	//WaitForSingleObject(idMutexFind, INFINITE);

	if (idNumber <= dBaseID)
	{
		if (ifs.good())
		{
			while (getline(ifs, lineToRead, '\n'))
			{
				loopCount = lineToRead.length();

				for (int i = 0; i < loopCount; i++)
				{
					if (idObtained == findID)
					{
						parseString = true;
						break; // Begin parsing the entire string
					}
					else if (lineToRead[i] == '|')
					{
						idObtained = "";
						break; // Leave loop and try new line
					}

					idObtained += lineToRead[i];
				}

				if (parseString == true)
				{
					cout << lineToRead << endl;

					for (int i = 0; i < loopCount; i++)
					{
						if (lineToRead[i] == '|')
						{
							pipeCounter++;
							continue;
						}

						if (pipeCounter == 0)
						{
							memberID += lineToRead[i];
						}
						else if (pipeCounter == 1)
						{
							firstName += lineToRead[i];
						}
						else if (pipeCounter == 2)
						{
							lastName += lineToRead[i];
						}
						else if (pipeCounter == 3)
						{
							birthDate += lineToRead[i];
						}
					}

					finalString = memberID + "\n" + firstName + "\n" + lastName + "\n" + birthDate + "\n";
					break;
				}
			}
		}
		else
		{
			cout << "File unable to be opened" << endl;
		}
	}
	else
	{
		finalString = "Not enough member ID in the database\n";
	}


	ifs.close();
	return;
}

/****************************************************/