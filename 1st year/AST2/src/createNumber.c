#include "..\include\header.h"

//Function: int createNumber()
//Parameters: void
//Return: int randomNumber
//Purpose: Use to generate a random number between 0 and 9.

int createNumber()
{
int randomNumber = 0;	//variable to store the random number
srand(time(NULL));	//seed rand
randomNumber = rand() % MAX_NUM;	//create random number

return randomNumber;
}
