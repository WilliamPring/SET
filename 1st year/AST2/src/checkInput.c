#include "..\include\header.h"
//Function:CheckInput(int guess, int number)
//Parameters: int guess, int number)
//Returns: int result
//Purpose: Use to check if the number guessed by the user is equal to, lesser or greater
//than the random number.
int checkInput(int guess, int number)
{
int result = 0;
if(guess == number)
	{
	result = SUCCEED;
	}
else if(guess > number)
	{
	result = GREATER;
	}
else if(guess < number)
	{
	result = LESSER;
	}

return result;
}
