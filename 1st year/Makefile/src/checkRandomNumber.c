/*
* Filename:	checkRandomNumber.c
* Project:  Assignment 2 -MakeFile
* By:		William Pring
* Date:		Feb 11, 2015
* Description: Check what user input and compare it with the random number and tell the user if it equal
* smaller or bigger
*/



#include "../inc/Header.h"



/*
* Function: checkRandomNumber()
* Description: Return a status number base on what user inputed
*Parameters: int random, int userNumber
* Return Values: int status
*/


int checkRandomNumber(int random, int userNumber)
{
	int status = 0;
	//check condition base on user input and random generated number
	if (userNumber == random)
	{
		status = 0;
	}
	else if (userNumber > random)
	{
		status = 1;
	}
	else if (userNumber<random)
	{
		status = -1;
	}
	return status;
}
