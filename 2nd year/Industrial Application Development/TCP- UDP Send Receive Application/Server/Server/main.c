///*
//#undef UNICODE
//
//#define WIN32_LEAN_AND_MEAN
//
//#include <windows.h>
//#include <winsock2.h>
//#include <ws2tcpip.h>
//#include <stdlib.h>
//#include <stdio.h>
//
//#pragma comment (lib, "Ws2_32.lib")
//
//#define DEFAULT_BUFLEN 10000
//#define DEFAULT_PORT "27015"
//
//int __cdecl main(void)
//{
//	WSADATA wsaData;
//	int iResult;
//
//	SOCKET ListenSocket = INVALID_SOCKET;
//	SOCKET ClientSocket = INVALID_SOCKET;
//
//	struct addrinfo *result = NULL;
//	struct addrinfo hints;
//	int sizeOfPackage = 0;
//	int iSendResult;
//	char recvbuf[DEFAULT_BUFLEN] = "";
//	int recvbuflen = DEFAULT_BUFLEN;
//	int loopContinue = 0;
//	while (loopContinue)
//	{
//		 Initialize Winsock
//		iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
//		if (iResult != 0) {
//			printf("WSAStartup failed with error: %d\n", iResult);
//			loopContinue = 1;
//			continue;
//		}
//
//		ZeroMemory(&hints, sizeof(hints));
//		hints.ai_family = AF_INET;
//		hints.ai_socktype = SOCK_STREAM;
//		hints.ai_protocol = IPPROTO_TCP;
//		hints.ai_flags = AI_PASSIVE;
//
//		 Resolve the server address and port
//		iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);
//		if (iResult != 0) {
//			printf("getaddrinfo failed with error: %d\n", iResult);
//			WSACleanup();
//			loopContinue = 1;
//			continue;
//		}
//
//		 Create a SOCKET for connecting to server
//		ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
//		if (ListenSocket == INVALID_SOCKET) {
//			printf("socket failed with error: %ld\n", WSAGetLastError());
//			freeaddrinfo(result);
//			WSACleanup();
//			loopContinue = 1;
//			continue;
//		}
//
//		 Setup the TCP listening socket
//		iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
//		if (iResult == SOCKET_ERROR) {
//			printf("bind failed with error: %d\n", WSAGetLastError());
//			freeaddrinfo(result);
//			closesocket(ListenSocket);
//			WSACleanup();
//			loopContinue = 1;
//			continue;
//		}
//
//		freeaddrinfo(result);
//
//		iResult = listen(ListenSocket, SOMAXCONN);
//		if (iResult == SOCKET_ERROR) {
//			printf("listen failed with error: %d\n", WSAGetLastError());
//			closesocket(ListenSocket);
//			WSACleanup();
//			loopContinue = 1;
//			continue;
//		}
//
//		 Accept a client socket
//		ClientSocket = accept(ListenSocket, NULL, NULL);
//		if (ClientSocket == INVALID_SOCKET) {
//			printf("accept failed with error: %d\n", WSAGetLastError());
//			closesocket(ListenSocket);
//			WSACleanup();
//			loopContinue = 1;
//			continue;
//		}
//
//		 No longer need server socket
//		closesocket(ListenSocket);
//	}
//
//	if (loopContinue != 1)
//	{
//		int outOfOrderBytes = 0;
//		 Receive until the peer shuts down the connection
//		do {
//			iResult = recv(ClientSocket, recvbuf, sizeOfPackage, 0);
//			if (iResult > 0) {
//				if (recvbuf != "1")
//				{
//					outOfOrderBytes++;
//				}
//				memset(recvbuf, 0, sizeOfPackage);
//			}
//			else if (iResult == 0)
//			{
//				printf("Connection closing...\n");
//				break;
//			}
//			else {
//				printf("recv failed with error: %d\n", WSAGetLastError());
//				closesocket(ClientSocket);
//				WSACleanup();
//				break;
//			}
//
//		} while (loopContinue != 1);
//
//		 shutdown the connection since we're done
//		iResult = shutdown(ClientSocket, SD_SEND);
//		if (iResult == SOCKET_ERROR) {
//			printf("shutdown failed with error: %d\n", WSAGetLastError());
//			closesocket(ClientSocket);
//			WSACleanup();
//			return 1;
//		}
//		closesocket(ClientSocket);
//	}
//	 cleanup
//	WSACleanup();
//
//	return 0;
//}
//*/