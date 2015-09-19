/*
  FileName:tcpip-server.c
  By: Naween M. and William P.
  Date: March 27, 2014
  Description: This following source file implicates a server to allow multiple clients to connect and chat with each other within their
  own terminals.  
 */

#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <signal.h>
#include <sys/wait.h>
#include <fcntl.h>
#include <errno.h>
#define PORT     5000
#define BUFSIZ   1024
#define SOCKSIZE 30

// used for accepting incoming command and also holding the command's response
char buffer[BUFSIZ] = "";
char IP[BUFSIZ] = ""; 

//store the different socket numbers 
int socketStore[SOCKSIZE] = { 0 }; 
int Number = 0; 
int i = 0; 

int server_socket = 0; 

// global to keep track of the number of connections
static int nClients = 0;
static int nNoConnections = 0;
void *connection_handler(void *socket_desc);  

/* Watch dog timer - to keep informed and watch how long the server goes without a connection */
void alarmHandler(int signal_number)
{
  if(nClients == 0) 
  {
     nNoConnections++;
     // It's been 10 seconds - determine how many 10 second intervals its been without a connection
     printf ("[SERVER WATCH-DOG] : It's been %d interval(s) without a new client connection ...\n", nNoConnections);
     alarm (10);	// reset alarm
  }
  else
  {
     // reset the number of times we've checked with no client connections
     nNoConnections = 0;
  }

  // reactivate signal handler for next time ...

  if((nNoConnections == 3) && (nClients == 0))
  {
     printf("[SERVER WATCH-DOG] : Its been 30 seconds without a new CLIENT ... LEAVING !\n");
     exit(-1);
  }
  signal (signal_number, alarmHandler);
}

/*
 * this signal handler is used to catch the termination
 * of the child. Needed so that we can avoid wasting
 * system resources when "zombie" processes are created
 * upon exit of the child (as the parent could concievably
 * wait for the child to exit)
 */

void WatchTheKids (int n)
{
    /* when this signal is invoked, it means that one of the children of this process (the clients)
       had exited.  So let's decrement the nClients value here ... */
    nClients--;

    /* given that a CLIENT has exited, let's reset the alarm to make us check in 10 seconds */
    alarm (10);	// reset alarm

    /* we received this signal because a CHILD (the CLIENT PROCESSOR) has exited ... we (the SERVER) have no intention
       of doing anything or need anything from that process - so the wait3() function call tells the O/S that we are 
       not waiting on anything from the terminated process ... so free up its resources and get rid of it ...

       ... without this wait3() call ... the system would be littered with ZOMBIES */
    //wait3 (NULL, WNOHANG, NULL);    
    signal (SIGCHLD, WatchTheKids);
}

void signalHandler(int signal_number);


int main (void)
{  
  int                client_socket    = 0;
  int                sizeOfTheStruct  = 0;
  int                client_len       = 0;
  int                closeSock        = 0; 
  int                len              = 0; 
  int                i                = 0;
  int                j                = 0;
  int 				 exitLoop 		  = 0; 

  struct sockaddr_in client_addr;
  struct sockaddr_in server_addr; 

  pthread_t newThread;

  char disconnect[BUFSIZ] = "";

  do
  {
	  signal(SIGINT, signalHandler);      // Call the signal to distribute termination messages to all of the clients from the server

	  printf ("[SERVER] : Obtaining STREAM Socket ...\n");
	  fflush(stdout);

	  if ((server_socket = socket (AF_INET, SOCK_STREAM, 0)) < 0) 
	  {
	    printf ("[SERVER] : Server Socket.getting - FAILED\n");
	    exitLoop = 1;
	    continue;
	  }

	  /*
	   * initialize our server address info for binding purposes
	   */
	  memset (&server_addr, 0, sizeof (server_addr));
	  server_addr.sin_family = AF_INET;
	  server_addr.sin_addr.s_addr = htonl (INADDR_ANY);
	  server_addr.sin_port = htons (PORT);

	  printf ("[SERVER] : Binding socket to server address ...\n");
	  fflush(stdout);

	  if (bind (server_socket, (struct sockaddr *)&server_addr, sizeof (server_addr)) < 0) 
	  {
	    printf ("[SERVER] : Binding of Server Socket - FAILED\n");
	    close (server_socket);
	    perror("bind");
	    exitLoop = 2;
	    continue;
	  }

	  /*
	   * start listening on the socket
	   */
	  printf ("[SERVER] : Begin listening on socket for incoming message ...\n");
	  fflush(stdout);

	  if (listen (server_socket, 5) < 0) 
	  {
	    printf ("[SERVER] : Listen on Socket - FAILED.\n");
	    close (server_socket);
	    exitLoop = 3;
	    continue;
	  }

	  sizeOfTheStruct = sizeof(struct sockaddr_in);

	  while(1)
	  {
			/*
		     * accept a packet from the client
		     */
		    client_len = sizeof (client_addr);
		    if ((client_socket = accept (server_socket,(struct sockaddr *)&client_addr, &client_len)) < 0) 
		    {
		      printf ("[SERVER] : Accept Packet from Client - FAILED\n");
		      close (server_socket);
		      exitLoop = 4;
		      break;
		    }

		    Number++;      

		    for(j = 0; j < Number; j++)
		    {
		      if(socketStore[j] == 0)
		      {
		        socketStore[j] = client_socket;   
		      }      
		    }

		    printf("Server got connection from client %s\n", inet_ntoa(server_addr.sin_addr));

			
		  	if( pthread_create( &newThread , NULL ,  connection_handler , (void*) &client_socket) < 0)
		    {
		        printf("Error in creating thread\n");
		    }	       
	 }

	 if(exitLoop == 4)
	 {
	 	break; 
	 }

  }while(exitLoop == 0);   

 return 0;
}


/*
Function: singalHandler
Description: Function handles when user enterls cntrl-c into the server, the server will then disconnect all of the users and inform that the chat is no longer available
Parameter(s): int signal_number: The signal number that will be used to maintain the signal  
Return: void
*/
void signalHandler(int signal_number)
{
  int msg = 0;  

  for(msg = 0; msg < Number; msg++)
  {
    write(socketStore[msg] , "!quit", strlen("!quit")); 
  } 

  close(server_socket);
}



/*
Function: connection_handler()
Description: This thread is created to prevent messaging blocks and to ensure all messages sent and recieved are instantanious and not pending. 
Parameter(s): void *socket_desc : The socket number being passed in for processing
Return: void * : A pointer to void
*/
void *connection_handler(void *socket_desc)
{
    //Get the socket descriptor
    int sock = *((int*)socket_desc);
    int read_size;
    char client_message[2000]= "";
    int ii = 0;
    int temp = 0; 
     
    memset(client_message, 0, 2000);

    while(1)
    {
      memset(client_message, 0, 2000);
      read(sock , client_message , 2000);

      if (strcmp(client_message, "quit") == 0 )
      {
            //printf("This should quit\n");
            memset(client_message, 0, 2000);
            //printf("ARRAY IND: %d VALUE: %d\n", ii, socketStore[ii]); 
            break;
      }  
	
      for(ii = 0; ii < Number; ii++)
      {        
          //printf("Socket being written to: %d\n\n", socketStore[ii]);           
          //printf("prints to client: %d\n", socketStore[ii]);
          write(socketStore[ii] , client_message , strlen(client_message));   
      }

       memset(client_message, 0, 2000);
    }

    for(temp = 0; temp < Number; temp++)
    {

      if(socketStore[temp] == sock)
      {
        socketStore[temp] = 0;
        Number--;
        close(sock);
        break;  
      }      
    }
    
		memset(client_message, 0, 2000);
       

    if(read_size == 0)
    {
        printf("Client disconnected\n");
        fflush(stdout);
    }
    else if(read_size == -1)
    {
        perror("recv failed");
    }
         
    return 0;
} 
