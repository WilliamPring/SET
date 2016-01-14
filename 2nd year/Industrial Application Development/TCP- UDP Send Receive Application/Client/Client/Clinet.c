#ifndef UNICODE
#define UNICODE
#endif

#define WIN32_LEAN_AND_MEAN

#include <winsock2.h>
#include <Ws2tcpip.h>
#include <stdio.h>
#include <stdlib.h>

// Link with ws2_32.lib
#pragma comment(lib, "Ws2_32.lib")

#define DEFAULT_PORT 5001
int main(int argc, char* argv[])
{
	char Buffer[128];
	char *server_name = "localhost";
	unsigned short port = DEFAULT_PORT;
	int retval = 0;
	char * buffer = NULL;
	int totalSize = 0;
	int amountOfPackage = 0;
	unsigned int addr;
	struct sockaddr_in server;
	struct hostent *hp;
	WSADATA wsaData;
	SOCKET  conn_socket;
	char buffer1000[1000] = "";
	char buffer2000[2000] = "";
	char buffer5000[5000] = "";
	char buffer10000[10000] = "";
	if (argc != 4)
	{

	}
	else
	{

		if ((retval = WSAStartup(0x202, &wsaData)) != 0) {
			printf("WSAStartup failed with error\n");
			WSACleanup();
		}
		else
		{
			server.sin_family = AF_INET;
			server.sin_port = htons(port);
			server.sin_addr.s_addr = inet_addr(argv[1]);

			conn_socket = socket(AF_INET, SOCK_STREAM, 0); /* Open a socket */
			if (conn_socket < 0) {
				fprintf(stderr, "Client: Error Opening socket: Error %d\n",
					WSAGetLastError());
				WSACleanup();
				return -1;
			}
			if (connect(conn_socket, (struct sockaddr*)&server, sizeof(server))
				== SOCKET_ERROR) {
				fprintf(stderr, "connect() failed: %d\n", WSAGetLastError());
				WSACleanup();
				return -1;
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
			else if (totalSize == 10000 )
			{
				buffer = buffer1000;

			}
			amountOfPackage = atoi(argv[3]); 
			for (int i = 0; i < amountOfPackage; i++)
			{

				strcpy(buffer, "1");
				retval = send(conn_socket, buffer, totalSize, 0);
				if (retval == SOCKET_ERROR) {
					fprintf(stderr, "send() failed: error %d\n", WSAGetLastError());
					WSACleanup();
					return -1;
				}
				memset(buffer, 0, totalSize); 
			}

			closesocket(conn_socket);
			WSACleanup();

		}
	}




}