/*
* Filename:	Header.c
* Project:  Assignment 2 -MakeFile
* By:		William Pring
* Date:		Feb 11, 2015
* Description:	Function contain the header file of a ll the include, defines and prototypes
*/

//defines
#define RANGE 11	
#define BUFFER_SIZE 12	
#define TO_LARGE 1	
#define TO_SMALL -1
#define TO_SAME 0
#define FALSE 0


//include
#include <stdio.h> 
#include <time.h>
#include <ctype.h>
#include <stdlib.h>
#include <string.h>

//prototype
int getRandomNumber(void);
int checkRandomNumber(int random, int userNumber);
int checkUserInput(char userInput[]);
int getRandomNumber(void);


