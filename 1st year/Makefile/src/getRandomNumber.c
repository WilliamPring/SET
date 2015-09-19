/*
* Filename:	getRandomNumber.c
* Project:  Assignment 2 -MakeFile
* By:		William Pring
* Date:		Feb 11, 2015
* Description: Generate a random number




/*
* Function: getRandomNumber()
* Description: Get a random number between 0-10
*Parameters: Void
* Return Values: int randNumber
*/

#include "../inc/Header.h"

int getRandomNumber(void)
{
   time_t t;
   int randNumber = 0;
   srand((unsigned) time(&t));
   int status = 0;
   randNumber = 1 + rand() % RANGE;

   return randNumber; 
}







