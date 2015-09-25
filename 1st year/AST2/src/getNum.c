#include "..\include\header.h"

//Function: int getNum()
//Parameters: void
//Return: int returnValue
//Purpose: Use to receive input between 0 and 9 from the user and convert the input into a
// numeric value. If the value is not a number, the function returns a fail result.

int getNum()
{
int returnValue = 0;
int number = 0;
char buffer[16] = "";
printf("Guess a number between 0 and 9: ");
fgets(buffer, 16, stdin);

if (strlen(buffer) > 2)
{
returnValue = FAIL;
}
else if (isdigit(buffer[0]) == 0)
	{
	returnValue = FAIL;
	}
else
	{
	number = atoi(buffer);
	returnValue = number;
	}

return returnValue;
}
