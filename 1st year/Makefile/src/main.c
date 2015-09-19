/*
* Filename:	main.c
* Project:  Assignment 2 -MakeFile
* By:		William Pring
* Date:		Feb 11, 2015
* Description: Contain the main
*/


#include "../inc/Header.h"



int main()
{
	//varibles
	char buffer[BUFFER_SIZE] = "";
	int status = 0;
	int randomNum = 0;
	int theStatusNumber = 0;
	int userInputForNumber = 0;
	//get a random number
	randomNum = getRandomNumber();
	//loop until user get the correct number
	while (1)
	{
		//get user input
		fgets(buffer, sizeof(buffer), stdin);
		//check to see if \n
		if (buffer[0]!='\n')
		{
			//remove the \n
			*strchr(buffer, '\n') = '\0';
			status = checkUserInput(buffer);
			if (status != 0)
			{
				//convert buffer into a number
				userInputForNumber = atoi(buffer);
				//get the status on user input
				theStatusNumber = checkRandomNumber(randomNum, userInputForNumber);
				if (theStatusNumber == TO_SAME)
				{
					printf("You have won!!\n");
					break;
				}
				else if (theStatusNumber == TO_LARGE)
				{
					printf("The number enter is to big try again... \n");
				}
				else if (theStatusNumber == TO_SMALL)
				{
					printf("the number you enter is to small try again \n");
				}
			}
			else
			{
				printf("You did not enter a number... \n");
			}
		}
		else
		{
			printf("You did not enter anything \n");
		}
	}


}
