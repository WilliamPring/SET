//#define WIN32_LEAN_AND_MEAN
//
//#include <winsock2.h>
//#include <Ws2tcpip.h>
//#include <stdio.h>
//#include <stdlib.h>
//#include <time.h>
//
//// Link with ws2_32.lib
//#pragma comment(lib, "Ws2_32.lib")
//
//#define STRICMP _stricmp
//
//#define DEFAULT_PORT 5001
//
// 
//int main(int argc, char **argv) {
//	char Buffer[128];
//	char *interface = NULL;
//	unsigned short port = DEFAULT_PORT;
//	int size = 0;
//	int numberToSend = 0;
//	int retval;
//	char* address = NULL;
//	int fromlen;
//	char *buffer = ""; 
//	char buffer1000[1000] = "";
//	char buffer2000[2000] = "";
//	char buffer5000[5000] = "";
//	char buffer10000[10000] = "";
//	int socket_type = SOCK_STREAM;
//	struct sockaddr_in local, from;
//	WSADATA wsaData;
//	SOCKET listen_socket, msgsock;
//	int loopContinue = 1;
//
//	address = argv[1];
//	size = atoi(argv[2]);
//	numberToSend = atoi(argv[3]);
//	if (size == 1000)
//	{
//		buffer = buffer1000;
//	}
//	else if (size == 2000)
//	{
//		buffer = buffer2000;
//	}
//	else if (size == 5000)
//	{
//		buffer = buffer5000;
//	}
//	else if (size == 10000)
//	{
//		buffer = buffer10000;
//	}
//	else
//	{
//		buffer = buffer10000;
//		size = 10000;
//	}
//	
//	while (loopContinue)
//	{
//		if ((retval = WSAStartup(0x202, &wsaData)) != 0) {
//			fprintf(stderr, "WSAStartup failed with error %d\n", retval);
//			WSACleanup();
//			loopContinue = 0;
//			break;
//		}
//		local.sin_family = AF_INET;
//		local.sin_addr.s_addr = inet_addr(address);
//		local.sin_port = htons(port);
//
//		listen_socket = socket(AF_INET, SOCK_STREAM, 0); // TCP socket
//
//		if (listen_socket == INVALID_SOCKET) {
//			fprintf(stderr, "socket() failed with error %d\n", WSAGetLastError());
//			WSACleanup();
//			loopContinue = 0;
//			break;
//		}
//
//		if (bind(listen_socket, (struct sockaddr*)&local, sizeof(local))
//			== SOCKET_ERROR) {
//			fprintf(stderr, "bind() failed with error %d\n", WSAGetLastError());
//			WSACleanup();
//			loopContinue = 0;
//			break;
//		}
//		if (socket_type != SOCK_DGRAM) 
//		{
//			if (listen(listen_socket, 5) == SOCKET_ERROR) {
//				fprintf(stderr, "listen() failed with error %d\n", WSAGetLastError());
//				WSACleanup();
//				loopContinue = 0;
//				break;
//			}
//		}
//		break;
//	
//	}
//	int package = 0;
//	int bytesInOrder = 0;
//	if (loopContinue != 0)
//	{
//		
//		fromlen = sizeof(from);
//		
//			msgsock = accept(listen_socket, (struct sockaddr*)&from, &fromlen);
//			if (msgsock == INVALID_SOCKET) {
//				fprintf(stderr, "accept() error %d\n", WSAGetLastError());
//				WSACleanup();
//			}
//			for (int i = 0; i < numberToSend; i++)
//			{
//				if (numberToSend == i - 1)
//				{
//
//				}
//			retval = recv(msgsock, buffer, size, 0);
//			printf("%s", buffer);
//			if (strcmp(Buffer, "1"))
//			{
//				bytesInOrder++;
//			}
//			memset(buffer, 0, size);
//			package++;
//			if (retval == SOCKET_ERROR) {
//				fprintf(stderr, "recv() failed: error %d\n", WSAGetLastError());
//				closesocket(msgsock);
//				continue;
//			}
//			if (retval == 0) {
//				printf("Client closed connection\n");
//				closesocket(msgsock);
//				continue;
//			}
//			if (package == numberToSend)
//			{
//				break;
//			}
//			continue;
//		}
//		closesocket(msgsock);
//	}
//	printf("Number of Package in order %d\nPackage Recv%d", bytesInOrder, package);
//}
