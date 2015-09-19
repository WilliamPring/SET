/*
* Filename:	checkUserInput.c
* Project:  Assignment 2 -MakeFile
* By:		William Pring
* Date:		Feb 11, 2015
* Description:	Check to see if user inputed a valid input
*/


#include "../inc/Header.h"


/*
* Function: checkUserInput()
* Description: Check user input and if it not valid return 1
*Parameters: char userInput[]
* Return Values: No return type (Void)
*/


int checkUserInput(char userInput[])
{
	int loopCounter = 0;
	int lengthOfString = 0;
	int status = 1;
	lengthOfString = strlen(userInput);
	//will loop base on the string length to see if it valid or not
	while (loopCounter< lengthOfString)
	{
		if (isdigit(userInput[loopCounter]) == FALSE)
		{
			status = 0;
		}
		loopCounter++;
	}
	return status;
}
