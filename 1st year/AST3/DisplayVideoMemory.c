/*
* file name: DisplayVideoMemory.c
* By: William Pring
* Date: Febuary 17, 2015
* Description: This is the header file with the include, define and with the function prototype
*/


#include "Header.h"


/*
* Function: VideoSim
* Description: This will print everything in you array
* Parameters: char video[MAX_ROWS][MAX_COLS]
* Returns: void
*/

void VideoSim(char video[MAX_ROWS][MAX_COLS])
{
	int i = 0;
	int j = 0;

	printf ("Video memory holds:\n");

	printf ("   ");
	for (i = 0; i < MAX_COLS; i++)
	{
		if ((i % 10) == 0)
		{
			printf("%d", i / 10);
		}
		else
		{
			printf(" ");

		}	/* end for */
	}
	printf ("\n");
	printf ("   ");
	for (i = 0; i < MAX_COLS; i++) 
	{
		printf ("%d", i % 10);
	}	/* end for */
	printf ("\n");

	for (i = 0; i < MAX_ROWS; i++) 
	{	// constant that needs definition
		printf ("%02d ", i);
		for (j = 0; j < MAX_COLS; j++) 
		{
			printf ("%c", video[i][j]);	// private data member
		}
		printf ("\n");
	}
	printf ("\n\n");
}
