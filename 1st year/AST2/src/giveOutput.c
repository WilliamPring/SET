#include "..\include\header.h"

//Function: void giveOutput(int result)
//Parameters: int result
//Return: void
//Purpose: Use to tell the user the result of their guess.
void giveOutput(int result)
{
if (result == SUCCEED)
	{
	printf("You guessed correctly!\n");
	}
else if (result == LESSER)
	{
	printf("You guessed too low!\n");
	}
else if (result == GREATER)
	{
	printf("You guessed too high\n");
	}
}