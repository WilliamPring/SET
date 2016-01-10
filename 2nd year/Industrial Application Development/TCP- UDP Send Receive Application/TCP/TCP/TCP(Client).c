#include "Header.h"


int main()
{
	//WinSock
	WSADATA wsa;
	//Socket
	SOCKET s;
	struct sockaddr_in server;


	//init the use of the Winsock DLL by a process return -1 when there is a problem
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		printf("Failed. Error Code : %d", WSAGetLastError());
	}
	else
	{
		//create a socket 
		if ((s = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
		{
			printf("Could not create socket : %d", WSAGetLastError());
		}
		else
		{
			//convert ip adress to a long
			server.sin_addr.s_addr = inet_addr("127.0.0.1");
			server.sin_family = AF_INET;
			server.sin_port = htons(80);
			//connect to the server
			if (connect(s, (struct sockaddr *)&server, sizeof(server)) < 0)
			{
				puts("connect error");
			}
			else
			{
				int clientSocket;
				char buffer[1024] = { 0 };
				int userInput = 0;
				int choice = 0;
				do
				{
					printf("1) 1000 bytes \n2) 2000 bytes \n3) 5000 bytes \n4) 1000 bytes \n");
					fgets(buffer, 121, stdin);
					userInput = myIsDigt(buffer);
					if (userInput == 1)
					{
						choice = atoi(buffer);
						if ((choice >= 1) && (choice <= 4))
						{

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




			}


		}

	}


	

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
		int returnStatus = 0;
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