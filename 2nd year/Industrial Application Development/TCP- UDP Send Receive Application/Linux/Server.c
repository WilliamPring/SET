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
#include <sys/time.h>

#endif


#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define BILLION  1000000000L;



 
int main(int argc, char **argv) {
	char Buffer[128];
	char *interface = NULL;
	unsigned short port = DEFAULT_PORT;
	int size = 0;
	int numberToSend = 0;
	int retval;
	char* address = NULL;
	int fromlen;
	char *buffer = ""; 
	char buffer1000[1000] = "";
	char buffer2000[2000] = "";
	char buffer5000[5000] = "";
	double diff = 0.0;
	char buffer10000[10000] = "";
	int socket_type = SOCK_STREAM;
	struct sockaddr_in local, from;
	unsigned long micros = 0;
double millis = 0.0;
#ifdef _WIN32
clock_t start, end;
#else
  struct timeval start, end;

#endif

#ifdef _WIN32
	WSADATA wsaData;
	SOCKET listen_socket, msgsock;
#else
	int listen_socket, msgsock =0;
    	double accum = 0.0;
#endif

	int loopContinue = 1;

	address = argv[1];
	size = atoi(argv[2]);
	numberToSend = atoi(argv[3]);

	if ((numberToSend == 0) || (numberToSend > 1000000))
	{
		printf("Invalid number\n");
	}
	else
	{
		if (size == 1000)
		{
			buffer = buffer1000;
		}
		else if (size == 2000)
		{
			buffer = buffer2000;
		}
		else if (size == 5000)
		{
			buffer = buffer5000;
		}
		else if (size == 10000)
		{
			buffer = buffer10000;
		}
		else
		{
			buffer = buffer10000;
			size = 10000;
		}

		while (loopContinue)
		{
#ifdef _WIN32
			if ((retval = WSAStartup(0x202, &wsaData)) != 0) {
				printf("WSAStartup failed with error\n");
				WSACleanup();
				loopContinue = 0;
				break;
			}
#endif
			local.sin_family = AF_INET;
			local.sin_addr.s_addr = inet_addr(address);
			local.sin_port = htons(port);

			listen_socket = socket(AF_INET, SOCK_STREAM, 0);
			if (listen_socket < 0) {
				printf("socket() failed with error \n");
#ifdef _WIN32			
				WSACleanup();
#endif
				loopContinue = 0;
				break;
			}

			if (bind(listen_socket, (struct sockaddr*)&local, sizeof(local)) < 0) {
				printf("bind() failed with error\n");
#ifdef _WIN32			
				WSACleanup();
#endif			
				loopContinue = 0;
				break;
			}
			if (socket_type != SOCK_DGRAM)
			{
				if (listen(listen_socket, 5) < 0) {
					printf("listen() failed with error\n");
#ifdef _WIN32			
					WSACleanup();
#endif
					loopContinue = 0;
					break;
				}
			}
			break;

		}
		int package = 0;
		int bytesInOrder = 0;
		if (loopContinue != 0)
		{

			fromlen = sizeof(from);


			msgsock = accept(listen_socket, (struct sockaddr*)&from, &fromlen);
			if (msgsock < 0) {
				printf("accept() error \n");
#ifdef _WIN32			
				WSACleanup();
#endif				
			}


			int i = 0;
#ifdef _WIN32
			start = clock();
#else
			gettimeofday(&start, NULL);
#endif
			int convertToInt = 0;
			for (; i < numberToSend; i++)
			{

				retval = recv(msgsock, buffer, size, MSG_WAITALL);

				//printf("%s\n", buffer); 
				if (retval > 0)
				{
					//data lost
					package++;
				}
				convertToInt = atoi(buffer);
				//printf("%d\n", convertToInt);
				if (i == convertToInt)
				{
					//bytes in order
					bytesInOrder++;
				}
				memset(buffer, 0, size);
				if (retval < 0) {
					printf("recv() failed: error\n");
#ifdef _WIN32			
					closesocket(msgsock);
#else
					close(msgsock);
#endif
					continue;
				}
				if (retval == 0) {
					printf("Client closed connection\n");
#ifdef _WIN32			
					closesocket(msgsock);
#else
					close(msgsock);
#endif				
					continue;
				}
				continue;
			}
			printf("%d\n", i);
#ifdef _WIN32			
			end = clock();
#else
			gettimeofday(&end, NULL);
			millis = (double)((end.tv_sec * 1000000 + end.tv_usec)
				- (start.tv_sec * 1000000 + start.tv_usec)) / 1000000;
#endif


#ifdef _WIN32			
			closesocket(msgsock);
#else

			close(msgsock);
#endif
		}
#ifdef _WIN32			
		micros = end - start;
		millis = micros / 1000;
#endif
		printf("Number of Package in order %d\nPackage Recv%d\nTime:%f\n", bytesInOrder, package, millis);
	}
}
