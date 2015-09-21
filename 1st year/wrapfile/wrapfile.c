#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <ctype.h>
#include <Windows.h>
#pragma warning(disable:4996)
#define maxInputChar 100
int getFileLength(const char* aru);
int getNumericArgument(char *argument[]);


int checkCondition(char *argument[], int totalArugument)
{
	int status = 0;
	int numberLength = 0;
	if (argument[1][0] != '-')
	{
		if ((totalArugument == 2) || (totalArugument > 2))
		{
			status = 1;
		}
		else
		{
			status = 0;
		}
	}
	else if (((strcmp("-w", argument[1]) == 0) && (isdigit(numberLength = *argument[2]) != 0)))
	{
		if ((totalArugument == 1) || (totalArugument == 2) || (argument[3] == NULL))
		{
			status = 0;
		}
		else
		{
			status = 2;
		}
	}
	else if ((argument[1][0] == '-') && (argument[1][1] == 'w') && (isdigit(numberLength = argument[1][2])))
	{
		if (argument[2] == NULL)
		{
			status = 0;
		}
		else
		{
			status = 3;
			printf("%d", status);
		}
	}
	else
	{
		status = 0;
	}

	return status;
}




int main(int argc, char *argv[])
{
	//Variables
	FILE *ifp = NULL;
	char wrapNumber[20] = "";
	char fileData[maxInputChar] = "";
	int lengthofthenumber = 0;
	char newlength[7] = "";
	int counter = 2;
	char*findNull = "";
	int counter3 = 3;
	int sizesOfFile = 0;
	int counterForFile = 1;
	int counterFor3 = 4;
	char *memoryLocation = NULL;
	int fileLength = 0;
	int status = 0;
	int number = 0;
	int sizeOfFile = 0;
	int returnValues = 0;

	returnValues = checkCondition(argv, argc);
	printf("%d\n", returnValues);
	if (returnValues == 1)
	{
		while (counter <= argc)
		{
			// open file base on the counter - 1 because counter starts at 2 in this case 
			ifp = fopen(argv[counter - 1], "r");
			if (ifp == NULL)
			{
				printf("Cant open the file");
				break;
			}
			// loops until feof reaches pass the end of file
			while (!feof(ifp))
			{
				//stores the content of ifp into the content of filedata
				fgets(fileData, 40, ifp);
				printf("%s\n", fileData);
			}
			//counter needs to be incremented
			counter++;
			//close the file
			fclose(ifp);
		}

	}

	// this else if will work if argv has -w and if argv is a number
	else if (returnValues == 2)
	{
		//copy the agrument into a string
		strcpy(wrapNumber, argv[2]);
		//convert the stirng into a number
		number = atoi(wrapNumber);
		//check to see if the number is valid

		//while the counterFor3 less then the total amount of agrument
		while (counterFor3 <= argc)
		{
			//open file for reading
			ifp = fopen(argv[1], "r");
			//if there is a problem
			if (ifp == NULL)
			{
				printf("Cant open the file");
				break;
			}
			else
			{

				while (!feof(ifp))
				{
					fgets(fileData, number + 1, ifp);
					//check to see if there is a next line carriage and if there is put a null terminator  
					if (findNull = strchr(fileData, '\n'))*findNull = 0;
					{
						printf("%s\n", fileData);
					}
				}
				fclose(ifp);
				counterFor3++;
			}
		}
	}
	//check if the first and the second element the the first agrument contains a '-' and 'w' and check if the next element next to 
	// w is a number 
	else if (returnValues == 3)
	{
		int numbericAgrument = 0;
		numbericAgrument = getNumericArgument(argv);
		ifp = fopen(argv[counter3 - 1], "r");
		if (ifp == NULL)
		{
			printf("Cant open the file");
		}
		else
		{
			while (!feof(ifp))
			{
				fgets(fileData, numbericAgrument + 1, ifp);
				//check to see if there is a next line carriage and if there is put a null terminator  
				if (findNull = strchr(fileData, '\n'))*findNull = 0;
				{
					printf("%s\n", fileData);
				}
			}
			fclose(ifp);
		}
	}
	else if (returnValues == 0)
	{
		printf("ERROR");
	}
}



int getNumericArgument(char *argument[])
{
	int number = 0;
	char wrapNumber[10] = "";
	char newlength[10] = "";
	int lengthofthenumber = 0;
	// this will loop and append from element 2 and onwords to a string
	for (int j = 2, w = 1; j < 8; j++)
	{
		//this check if the argv is a number if it is then the value will be save
		if (isdigit(argument[w][j]) != 0)
		{
			//this is appedning the numbers to a string
			strcat(wrapNumber, &(argument[w][j]));
		}
		//if it not a string it will will execute code
		else
		{
			//break out of the program
			break;
		}
	}
	// get the length of the string
	lengthofthenumber = strlen(wrapNumber);
	//if the string is less then 2 then convert the string to an interger
	if (lengthofthenumber < 2)
	{
		number = atoi(wrapNumber);
	}
	// if it not then loop 
	else
	{
		//this will start from agrv[1][2] so you will only get the numbers and ignore the "-" , "w" 
		for (int k = 2; k < lengthofthenumber; k++)
		{
			//appends the the argv into a string
			strcat(newlength, &(argument[1][k]));
			//convert the string into an interger
			number = atoi(newlength);
		}
	}
	return number;
}
