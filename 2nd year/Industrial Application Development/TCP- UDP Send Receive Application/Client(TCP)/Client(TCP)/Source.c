#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

int __cdecl main(int argc, char **argv)
{
	WSADATA wsaData;
	SOCKET ConnectSocket = INVALID_SOCKET;
	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;
	char *sendbuf = "this is a test";
	char recvbuf[DEFAULT_BUFLEN];
	int iResult;
	int recvbuflen = DEFAULT_BUFLEN;

	// Validate the parameters
	if (argc != 2) {
		printf("usage: %s server-name\n", argv[0]);
		return 1;
	}

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// Resolve the server address and port
	iResult = getaddrinfo(argv[1], DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Attempt to connect to an address until one succeeds
	for (ptr = result; ptr != NULL;ptr = ptr->ai_next) {

		// Create a SOCKET for connecting to server
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
			ptr->ai_protocol);
		if (ConnectSocket == INVALID_SOCKET) {
			printf("socket failed with error: %ld\n", WSAGetLastError());
			WSACleanup();
			return 1;
		}

		// Connect to server.
		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (iResult == SOCKET_ERROR) {
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}
		break;
	}

	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Unable to connect to server!\n");
		WSACleanup();
		return 1;
	}

	int clientSocket;
	char Size1k[1000] = { -1 };
	char NumberBuffer[5] = { -1 };
	char Size2k[2000] = { -1 };
	char Size5k[5000] = { -1 };
	char Size10K[10000] = { -1 };
	char BytesSend[10000] = { 0 }; 
	char PackageSend[10000] = { 0 };
	int userInput = 0;
	int choice = 0;
	int package = 0;
	do
	{
		printf("Pick the bytes you want to send to the server (1 - 4) \n");
		printf("1) 1000 bytes \n2) 2000 bytes \n3) 5000 bytes \n4) 10000 bytes \n");
		fgets(BytesSend, 121, stdin);
		userInput = myIsDigt(BytesSend);
		if (userInput == 1)
		{
			choice = atoi(BytesSend);
			if ((choice >= 1) && (choice <= 4))
			{
				while (1)
				{
					printf("Enter the amount of packages you wish to enter\n");
					fgets(PackageSend, 10000, stdin);
					package = myIsDigt(PackageSend);
					if (package == 1)
					{
						break;
					}
				}
				if (strlen(PackageSend) <= 6)
				{
					package = atoi(PackageSend);
					if (package == 0)
					{
						printf("Package cannot be 0! \n");
						continue;
					}
				}
				else
				{
					printf("Number To large! Enter amount of packages between 1 - 999999\n");
				}
				if (choice == 1)
				{
					int TotalPack = 0;
					TotalPack = atoi(PackageSend);
					iResult = send(ConnectSocket, PackageSend, 10000, 0);
					for (int i = 0; i < TotalPack; i++)
					{
						memset(Size1k, 0, 1000);
						sprintf(NumberBuffer, "%d", i);
						strcat(Size1k, NumberBuffer);	
						send(ConnectSocket, Size1k, 1000, 0);
						Sleep(1);
					}
				}
				else if (choice == 2)
				{
					iResult = send(ConnectSocket, Size2k, package, 0);
					for (int i = 0; i < package; i++)
					{
						memset(Size2k, 0, 2000);
						sprintf(NumberBuffer, "%d", i);
						strcat(Size2k, NumberBuffer);
						send(ConnectSocket, Size2k, 2000, 0);
					}
				}
				else if (choice ==3)
				{
					iResult = send(ConnectSocket, Size5k, package, 0);

					for (int i = 0; i < package; i++)
					{
						memset(Size5k, 0, 5000);
						sprintf(NumberBuffer, "%d", i);
						strcat(Size5k, NumberBuffer);
						send(ConnectSocket, Size5k, 5000, 0);
					}


				}
				else
				{
					iResult = send(ConnectSocket, Size10K, package, 0);

					for (int i = 0; i < package; i++)
					{
						memset(Size10K, 0, 10000);
						sprintf(NumberBuffer, "%d", i);
						strcat(Size10K, NumberBuffer);
						send(ConnectSocket, Size10K, 10000, 0);
					}
				}


			

			}
			else if (choice == 5)
			{
				break;
			}
			else
			{
				printf("Please Enter a number between 1-5\n\n");
			}
		}
		else
		{
			printf("Please Enter a valid number (ie 1-5) \n\n");

		}
	} while (choice != 5);






	//chagne depending on the inital input o
	iResult = send(ConnectSocket, sendbuf, (int)strlen(sendbuf), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	// shutdown the connection since no more data will be sent
	iResult = shutdown(ConnectSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	// cleanup
	closesocket(ConnectSocket);
	WSACleanup();

	return 0;
}





int myIsDigt(char *input)
{
	int returnStatus = 0;
	int checkSize = 0;
	checkSize = strlen(input);
	//check to see if what user put inside is a valid number that might overflow the buffer and int
	//in the future
	if (checkSize > 121)
	{
		returnStatus = 0;
	}
	else
	{
		char checkIfDigit[121] = "";
		//convert string to int already did validation check base on the first if statement
		int number = atoi(input);
		//check to see if it a digit or not by using sprintf and putting a digit input a char buffer
		sprintf(checkIfDigit, "%d", number);
		strcat(checkIfDigit, "\n");
		//if sucessfull return 1 if not return 0
		if (strcmp(checkIfDigit, input) == 0)
		{
			returnStatus = 1;
		}
	}
	return returnStatus;
}