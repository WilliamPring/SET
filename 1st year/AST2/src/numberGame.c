#include "..\include\header.h"

int main()
{

int guess = 0;	//the number guessed by the user
int number = 0;	//the number randomly generated
int programRunning = YES;	//Is the program Running?
int validInput = YES;		//Is the input valid?
int result = 0;			//was the guess correct?

number = createNumber();	//Create a random number
while (programRunning == YES)
{
	guess = getNum();		//Ask user to guess the number

//If the user gives incorrect input, try again
	if (guess == FAIL)
	{
	printf("That is not a valid answer!");
	validInput = NO;
	}
//If the user gives correct input, move forward
	if(validInput = YES)
	{
	result = checkInput(guess, number);	//Check the result of user's guess
	giveOutput(result);
		
		//If the user guesses the correct number, close the program
		if(result == SUCCEED)
		{
		programRunning = NO;
		}//endif
	}
	else
	{
	programRunning = YES;
	}//endif

}//End While
return 0;
}