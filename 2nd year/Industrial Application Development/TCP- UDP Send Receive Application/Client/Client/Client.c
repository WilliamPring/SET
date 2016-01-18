/*
File name: Client.cs
Project: 
By: William Pring and Denys Polituk
Date: 1/16/2016
Description: Client that will connect to the server and will have a choice in the command line to input the server IP adress,
Amount of bytes and the amount of time to send
*/



#define DEFAULT_PORT 5001
#define WIN32_LEAN_AND_MEAN
//Execute when in Windows
#ifdef _WIN32
	#include <winsock2.h>
	#include <Ws2tcpip.h>
	#pragma comment(lib, "Ws2_32.lib")
//Execute When in Linux
#else
	#include <string.h> 
	#include <sys/types.h>
	#include <sys/socket.h>
	#include <netinet/in.h>
	#include <netdb.h>
	#include <unistd.h>
#endif

#include <stdio.h>
#include <stdlib.h>




int main(int argc, char* argv[])
{
	unsigned short port = DEFAULT_PORT;
	int retval = 0;
	char * buffer = NULL;
	int totalSize = 0;
	int amountOfPackage = 0;
	unsigned int addr;
	struct sockaddr_in server;
//Socekts and WSADATA Linux uses an INT for the Sockets unlike Windows 
#ifdef _WIN32
		//Windows Sockets
	WSADATA wsaData;
	SOCKET  conn_socket;
#else
	int  conn_socket =0;
#endif
	
	char buffer1000[1000] = "";
	char buffer2000[2000] = "";
	char buffer5000[5000] = "";
	char buffer10000[10000] = "";
	if (argc != 4)
	{
		printf("Not correct amount of Agruments example 127.0.0.1 1000 1000\n");
	}
	else
	{
		//Windows Windows Sockets implementation
#ifdef _WIN32
		if ((retval = WSAStartup(0x202, &wsaData)) != 0) {
			printf("WSAStartup failed with error\n");
			WSACleanup();
		}
		else
#endif
		{
			//provide the correct server information
			server.sin_family = AF_INET;
			server.sin_port = htons(port);
			server.sin_addr.s_addr = inet_addr(argv[1]);

			conn_socket = socket(AF_INET, SOCK_STREAM, 0); /* Open a socket */
			//check for -1
			if (conn_socket < 0) {
				printf("Client: Error Opening socket: Error\n");
#ifdef _WIN32				
				//Clean sockets
				WSACleanup();
#endif
			}
			else
			{
				if (connect(conn_socket, (struct sockaddr*)&server, sizeof(server)) < 0) {
					printf("connect() failed: \n");
#ifdef _WIN32									
					WSACleanup();
#endif			
				}

				// cook up a string to send
				//
				totalSize = atoi(argv[2]);

				if (totalSize == 1000)
				{
					buffer = buffer1000;
				}
				//2000
				else if (totalSize == 2000)
				{
					buffer = buffer2000;

				}
				//5000
				else if (totalSize == 5000)
				{
					buffer = buffer5000;

				}
				//10000
				else if (totalSize == 10000)
				{
					buffer = buffer1000;

				}
				//amount of packages
				amountOfPackage = atoi(argv[3]);
				if ((amountOfPackage == 0) || (amountOfPackage > 1000000))
				{
					printf("Number is invalid number\n");
				}
				else
				{
					int i = 0;
					//loop base on the thrid agrument
					for (; i < amountOfPackage; i++)
					{
						sprintf(buffer, "%d", i);
						retval = send(conn_socket, buffer, totalSize, 0);
						//check to see if it was sent sucessfully
						if (retval < 0) {
							printf("send() failed: error %d\n", i);
#ifdef _WIN32					
							WSACleanup();
#endif					
							break;

						}
						memset(buffer, 0, totalSize);
					}
#ifdef _WIN32
					closesocket(conn_socket);
					WSACleanup();
#else
					close(conn_socket);
#endif
				}
			}
		}
	}
}
