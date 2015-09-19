/*
  FileName:tcpip-client.c
  By: Naween M. and William P.
  Date: March 27, 2014
  Description: The following program is a server based chat program that allows multiple clients to connect and talk amongst each other within the 
  Server based terminal. Clients are able to register themselves and pursue conversation, disconnect from the server and reconnect to continue 
  engaging in conversation. 
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
#include <pthread.h>
#include <ncurses.h>
#define PORT    5000
#define BUFSIZ  1024

int counter =1;
void *connection_handler_read(void *socket_desc); 
char buffer[BUFSIZ] = "";
char IP[BUFSIZ] = ""; 
int my_server_socket;
WINDOW * lowerScreen;
WINDOW * topScreen;

/*
Function: draw_borders()
Description: This function arranges the video screen that will be used to display the clients messages 
Parameter(s): WINDOW *screen: 
Return: void, nothing
*/
void draw_borders(WINDOW *screen)
{
    int x, y, i;
    getmaxyx(screen, y, x);
    // 4 corners
    for (i = 1; i < (x - 1); i++) 
    {
    mvwprintw(screen, 0, i, "-");
    mvwprintw(screen, y - 1, i, "-");
    }
 }



int main (int argc, char *argv[])
{
  pthread_t newThread;
  int len =0;
  int done = 0;
  struct sockaddr_in server_addr;
  struct sockaddr_in name; 
  struct hostent*    host;

  // Check to see if the user entered in the correct IP address

  if (argc < 2) 
  {
    printf("USAGE : tcpipClient <server_name>\n");
  }

  else
  {  		 
	  // Initialize variables that will be used to construct the video screen through the ncurses library

	  int y = 0;
	  int x =0;
	  int sizeOfSubWindow = 5;
	  int exitLoop = 0; 
	  initscr(); //start curse mode
	  getmaxyx(stdscr, y, x);
	  topScreen = newwin( y - sizeOfSubWindow, x,0, 0);
	  lowerScreen = newwin(sizeOfSubWindow, x,y - sizeOfSubWindow, 0);
	  draw_borders(lowerScreen);
	  draw_borders(topScreen);
	  scrollok(topScreen, TRUE);
	  wrefresh(topScreen);
	  wmove(lowerScreen,1,1);
	  wrefresh(lowerScreen);

	  char IPNAME[BUFSIZ] = ""; // Name of the client that will be stored and registered onto the server

	  do
	  {
		  // Receives the name and if their is an error then NULL is set	
		  if ((host = gethostbyname (argv[1])) == NULL) 
		  {
		    printf("[CLIENT] : Host Info Search - FAILED\n");
		    exitLoop = 2;
		    continue;    
		  }
		  
		     wmove(topScreen, 1,1);
		     wprintw (topScreen, "Enter in your name\n");
		     wrefresh(topScreen);
		     
		     wmove(lowerScreen, 1,1);
		     wgetstr(lowerScreen, IPNAME);
		     wrefresh(lowerScreen);
		     wclear(lowerScreen);
		     wrefresh(lowerScreen);
		     draw_borders(lowerScreen);

			  /*
			   * initialize struct to get a socket to host
			   */
			  memset (&server_addr, 0, sizeof (server_addr));
			  server_addr.sin_family = AF_INET;
			  memcpy (&server_addr.sin_addr, host->h_addr, host->h_length);
			  server_addr.sin_port = htons (PORT);

			     /*
			      * get a socket for communications
			      */
			  wmove(topScreen,1,1);
			  wprintw (topScreen, "[CLIENT] : Getting STREAM Socket to talk to SERVER\n");
			  wrefresh(topScreen);
			  //fflush(stdout);
			  //creating a socket
			  if ((my_server_socket = socket (AF_INET, SOCK_STREAM, 0)) < 0) 
			  {
			     wmove(topScreen,1,1);
			     wprintw (topScreen, "[CLIENT] : Getting Client Socket - FAILED\n");
			     wrefresh(topScreen);
			     exitLoop = 3;			     
			     continue; 
			  }

			  
			     /*
			      * attempt a connection to server
			      */
			  wmove(topScreen,1,1);
			  wprintw (topScreen, "[CLIENT] : Connecting to SERVER\n");
			  wrefresh(topScreen);

			  //fflush(stdout);
			  //makes a connection to the socket
			  if (connect (my_server_socket, (struct sockaddr *)&server_addr,sizeof (server_addr)) < 0) 
			  {
			     wmove(topScreen,1,1);
			     wprintw (topScreen,"[CLIENT] : Connect to Server - FAILED\n");
			     wrefresh(topScreen);
			     close (my_server_socket);    
			     exitLoop = 4;
			     continue;
			  }

			  wmove(topScreen,1,1);
			  wprintw(topScreen, "Connected\n");
			  wrefresh(topScreen);


			  if( pthread_create( &newThread , NULL ,  connection_handler_read , (void*) &my_server_socket) < 0)
			  {
			      //printw("Error in creating thread\n");
				  perror("thread");
				  exitLoop = 1;

				  continue; 
			  }
			  

			  //for the infinite loop	
			  done = 1;

			  while(done)
			  {
			    strcat(IP, "[");
			    strcat(IP, IPNAME); //server_addr.sin_addr
			    strcat(IP, "] : "); // inet_ntoa((struct sockaddr_in *)&theIP.theIp_addr->sin_addr


			     /* clear out the contents of buffer (if any) */
			    memset(buffer,0,BUFSIZ);

			     
			     //fflush (stdout);
			     
			     wmove(lowerScreen, 1,0);
			     wgetstr(lowerScreen, buffer);
			     wrefresh(lowerScreen);
			     wclear(lowerScreen);
			     wrefresh(lowerScreen);
			     draw_borders(lowerScreen);    

			     strcat(IP, buffer);


			     if(strcmp(buffer,"!quit") == 0)
			     {
			       done = 0;
			       wmove(lowerScreen,1,1);
			       wprintw(lowerScreen, "Server disconnected\n");  
			       //write(my_server_socket, "quit", 4);
			     } 
			     

			     if(strcmp(buffer,"quit") == 0)
			     {
			       done = 0;

			       //wmove(topScreen,1,1);
			       //wprintw(topScreen, "Server disconnected\n");  
			       //wrefresh(topScreen);
			       write(my_server_socket, "quit", 4);
			       exitLoop = 1; 
			       break; 
			     } 

			     write(my_server_socket, IP, strlen (IP));
			     memset(IP, 0, BUFSIZ);
			  }

				  /*
				   * cleanup
				   */

				  wmove(topScreen,1,1);
				  wprintw(topScreen, "[CLIENT] : I'm outta here !\n");
				  wrefresh(topScreen);
				  wrefresh(lowerScreen);

				  draw_borders(lowerScreen);
				  draw_borders(topScreen);

				 

				  delwin(topScreen);
				  delwin(lowerScreen);
				  endwin();

	  }while(exitLoop == 0); 

  } 

  return 0;
  
}


/*
Function: connection_handler_read()
Description: This is another thread created to maintain messages incoming from the server instantly and without any delays the messages will be printed onto the terminal 
Parameter(s): void *socket_desc: A pointer to void, in this case the arguement that will be passed onto here is the socket number
Return:
*/
void *connection_handler_read(void *socket_desc)
{
     int sock = my_server_socket; //*(int*)socket_desc;
     char newnew[1000] = "";

     // read (sock, newnew, sizeof (newnew));
     while(1)
     {
      memset(newnew, 0, 1000);
    //attempts to write nbytes 
    //read nbyte bytes from the file associated with the open file descriptor
         read (my_server_socket, newnew,  sizeof(newnew));
       //  printf("Got the newnew\n");
         if (strcmp(newnew, "quit")==0)
         {
          //close(sock);
          memset(newnew, 0, 1000);
          break;
         }
         
         wmove(topScreen,counter,0);
         wprintw(topScreen, "%s\n", newnew);          
         wrefresh(topScreen);
         counter++;                      
      }

   return 0;
} 


