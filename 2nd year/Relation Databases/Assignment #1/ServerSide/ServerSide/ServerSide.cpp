#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#define DEFAULT_PORT "27015"

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <vector>
#include <iostream>
#include <fstream>


#pragma comment(lib, "Ws2_32.lib")

using namespace std;

DWORD WINAPI readAndWriteThread(LPVOID lpParam);
DWORD WINAPI closeServer(LPVOID lpParam);

//The steps to create a server, create a socket, bind the socket, have the socket listen for connections, connect, read/write to client connected

//Global variables to ease complexity and allow for variables to be used in different threads 
int recvbuflen = 512;
int dBaseID = 1;
int test = 20;

char recvbuf[512] = "";
SOCKET ClientSocket; //Temporary socket variable to accept connections from clients 
SOCKET ListenSocket = INVALID_SOCKET;
HANDLE newThread; //Handle for new thread creation 
HANDLE closeSocketsThread;
HANDLE openMutex;

vector<int> socketPool;
//vector<int>::iterator iter; 

void handleFind(LPVOID lpParam);
void handleUpdate(LPVOID lpParam);

int main()
{
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
	bool  leaveLoop = false;
	DWORD dwWaitResult;
	int loopCounter = 0;
	ofstream file;
	dwWaitResult = WaitForSingleObject(openMutex, INFINITE);

	switch (dwWaitResult)
	{
	case WAIT_OBJECT_0:	// TODO: Perform task
	{
		//send(ClientSocket, serverMenu, (int)strlen(serverMenu), 0);

		do
		{
			recv(ClientSocket, recvbuf, recvbuflen, 0);
			numberChosen = atoi(recvbuf);
			memset(recvbuf, 0, strlen(recvbuf));

			switch (numberChosen)
			{
			case 1:
			{
				//function for inserting into file
				//recv(ClientSocket, recvbuf, recvbuflen, 0);
				//loopCounter = atoi(recvbuf);
				memset(recvbuf, 0, strlen(recvbuf));
				file.open("database.txt", ios::app);

				while (1)
				{
					recv(ClientSocket, recvbuf, recvbuflen, 0);
					if (strcmp(recvbuf, "End") == 0)
					{
						break;
					}
					file << dBaseID << "|" << recvbuf;
					dBaseID++;
					memset(recvbuf, 0, strlen(recvbuf));
				}
				file.close();
				break;
			}
			case 2:
			{
				//function for updating the file 			
				handleUpdate(lpParam);
				break;
			}
			case 3:
			{
				//function for finding a member in the file 
				handleFind(lpParam);
				break;
			}
			case 4:
			{
				printf("Client Number [%d] Disconnected", ClientSocket);
				send(ClientSocket, "Disconnected from server", (int)strlen("Disconnected from server"), 0);
				leaveLoop = true;
				break;
			}
			}
		} while (leaveLoop != true);

		ReleaseMutex(openMutex);
		break;
	}
	case WAIT_TIMEOUT:
	{
		send(ClientSocket, busyServer, (int)strlen(busyServer), 0);
		printf("Thread %d: wait timed out\n", GetCurrentThreadId());
		break;
	}
	default:
	{
		send(ClientSocket, busyServer, (int)strlen(busyServer), 0);
		break;
	}
	}

	printf("\nExited LOOP successfully\n");

	return 0;
}



//Create another thread so that the application can be asynchronous to read and write responses 

DWORD WINAPI closeServer(LPVOID lpParam)
{
	char buffer[1024] = { 0 };

	while (1)
	{
		fgets(buffer, 1024, stdin);
		*strchr(buffer, '\n') = '\0';

		if (strcmp(buffer, "close") == 0)
		{
			printf("Closing Server Now!\n>>SERVER CLOSED\n");
			for (vector<int>::iterator iter = socketPool.begin(); iter != socketPool.end(); ++iter)
			{
				send(*iter, "Server Closed", (int)strlen("Server Closed"), 0);
				shutdown(*iter, SD_BOTH);
				closesocket(*iter);
			}
			exit(0);
			break;
		}
	}
	return 0;
}



/****************************************************/

/*
Function:
Description:
Parameter(s):
Return:
*/
void handleUpdate(LPVOID lpParam)
{
	int socketNum = (int)lpParam;
	int loopCount = 0;
	bool updateString = false;
	fstream inputFile;

	string lineToRead = "";
	string idObtained = "";

	memset(recvbuf, 0, strlen(recvbuf));
	recv(ClientSocket, recvbuf, recvbuflen, 0);
	string findID = recvbuf;
	int enteredID = atoi(findID.c_str());

	inputFile.open("database.txt", ios::in | ios::out);
	inputFile.clear();
	//inputFile.seekg(0, ios::beg);

	int byteCounter = 0;

	if (enteredID <= dBaseID)
	{
		if (inputFile.good())
		{
			while (getline(inputFile, lineToRead, '\n'))
			{
				loopCount = lineToRead.length();

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

				if (updateString == true)
				{
					//update the new string 
					inputFile.seekp(byteCounter);
					// 10|denys|denys|1995-03-03|\n
					inputFile << findID + "|DENYS|WILLY|1995-01-05|";
					break;
				}

				byteCounter += lineToRead.length() + 2;
			}



			/*
			for (int i = 0; i < enteredID; i++)
			{
			getline(inputFile, lineToRead, '\n');
			}

			loopCount = lineToRead.length();

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

			if (updateString == true)
			{
			//Update the string
			cout << lineToRead << endl;
			lineToRead = findID + "|TEST|BRO|1995/05/03";
			cout << lineToRead << endl;

			inputFile << lineToRead;
			inputFile.close();

			}  */
			//send(socketNum, overall.c_str(), (int)strlen(overall.c_str()), 0);
		}
		else
		{
			cout << "File unable to be opened" << endl;
		}
	}
	else
	{
		send(socketNum, "Not enough member ID in the database\n", (int)strlen("Not enough member ID in the database\n"), 0);
	}

	inputFile.close();
	return;
}



/****************************************************/

/*
Function:
Description:
Parameter(s):
Return:
*/
void handleFind(LPVOID lpParam)
{
	int socketNum = (int)lpParam;
	int pipeCounter = 0;
	int loopCount = 0;

	bool parseString = false;

	ifstream ifs;

	string lineToRead = "";
	string memberID = "Member ID : ";
	string firstName = "First Name: ";
	string lastName = "Last Name: ";
	string birthDate = "Birth Date: ";
	string overall = "";
	string idObtained = "";

	memset(recvbuf, 0, strlen(recvbuf));
	recv(ClientSocket, recvbuf, recvbuflen, 0);
	string findID = recvbuf;
	int idNumber = atoi(findID.c_str());

	ifs.open("database.txt");

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

					overall = memberID + "\n" + firstName + "\n" + lastName + "\n" + birthDate + "\n";
					break;
				}
			}
			send(socketNum, overall.c_str(), (int)strlen(overall.c_str()), 0);
		}
		else
		{
			cout << "File unable to be opened" << endl;
		}
	}
	else
	{
		send(socketNum, "Not enough member ID in the database\n", (int)strlen("Not enough member ID in the database\n"), 0);
	}

	ifs.close();
	return;
}

/****************************************************/