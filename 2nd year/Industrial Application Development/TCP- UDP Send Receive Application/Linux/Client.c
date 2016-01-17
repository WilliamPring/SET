#define DEFAULT_PORT 5001
#define WIN32_LEAN_AND_MEAN
#ifdef _WIN32
	#include <winsock2.h>
	#include <Ws2tcpip.h>
	#pragma comment(lib, "Ws2_32.lib")
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


	char Buffer[128];
	unsigned short port = DEFAULT_PORT;
	int retval = 0;
	char * buffer = NULL;
	int totalSize = 0;
	int amountOfPackage = 0;
	unsigned int addr;
	struct sockaddr_in server;
	struct hostent *hp;

#ifdef _WIN32
		
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

	}
	else
	{
#ifdef _WIN32
		if ((retval = WSAStartup(0x202, &wsaData)) != 0) {
			printf("WSAStartup failed with error\n");
			WSACleanup();
		}
		else
#endif
		{
			server.sin_family = AF_INET;
			server.sin_port = htons(port);
			server.sin_addr.s_addr = inet_addr(argv[1]);

			conn_socket = socket(AF_INET, SOCK_STREAM, 0); /* Open a socket */
			if (conn_socket < 0) {
				printf("Client: Error Opening socket: Error\n");
#ifdef _WIN32						
				WSACleanup();
#endif
				return -1;
			}
			if (connect(conn_socket, (struct sockaddr*)&server, sizeof(server))< 0) {
				printf("connect() failed: \n");
	#ifdef _WIN32									
WSACleanup();
	#endif			
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
				buffer = buffer10000;

			}
			amountOfPackage = atoi(argv[3]); 
			int i = 0;
			for (; i < amountOfPackage; i++)
			{

				sprintf(buffer, "%d", i);
				retval = send(conn_socket, buffer, totalSize, 0);
				if (retval < 0) {
					printf("send() failed: error %d\n", i);
				#ifdef _WIN32					
					WSACleanup();
				#endif					
				//return -1;
				break;
				
				}
				


				memset(buffer, 0, totalSize); 
			}
			printf("hi %d\n", i);
#ifdef _WIN32
			closesocket(conn_socket);
			WSACleanup();
#else
close(conn_socket);
#endif
		}
	}




}
