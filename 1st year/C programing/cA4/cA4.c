/*
* Filename:	cA4.c
* Project:	Assignment1
* By:		William Pring
* Date:		November 6, 2014
* Description:	This program will display cities menu and user will input what ever city
*               and it will calculate the total and display the total hours and mins it will take
*/



#include<stdio.h>
#define kcities 6
#define ktimeAndLayover 2



//Prototypes
int checkCity(int cityStart, int endCity);
void cityMenu(void);
int getNum(void);
int amountOfTime(int city[][2], int startCity, int endCity);
int getUserStartingCity(int *startCity);
void getUserEndingCity(int *endCity); 




/*
* Function: cityMenu()
* Description: Will display the menu
*Parameters: nothing
* Return Values: nothing
*/



void cityMenu(void)
{
	//the cities
	printf("\nCities\n");
	printf("(#1) Toronto\n");
	printf("(#2) Atlanta \n");
	printf("(#3) Austin \n");
	printf("(#4) Denver \n");
	printf("(#5) Chicago \n");
}



/*
* Function: getNum()
* Description: Will get user input 
*Parameters: nothing
* Return Values: Number
*/



int getNum(void)
{
	/* the array is 121 bytes in size; we'll see in a later lecture how we can improve this code */
	char record[121] = { 0 }; /* record stores the string */
	int number = 0;
	/* use fgets() to get a string from the keyboard */
	fgets(record, 121, stdin);
	/* extract the number from the string; sscanf() returns a number
	* corresponding with the number of items it found in the string */
	if (sscanf_s(record, "%d", &number) != 1)
	{
		/* if the user did not enter a number recognizable by
		* the system, set number to -1 */
		number = -1;
	}
	return number;
}



/*
* Function: amountOfTime()
* Description: Will loop and add the amount of time to a total based on the 
* starting cities and ending cities
*Parameters: the city array with the information, user startCity and user endCity
* Return Values: sum of all of time added together
*/



int amountOfTime(int city[][2], int startCity, int endCity)
{
	int sum = 0;
	// minus 1 because ending cities is 1 less in the array  
	endCity = endCity -1;
	// minus 1 because first cities start from element 0 not element 1 and user wont
	//input 0 for city 1
	startCity = startCity-1;
	
	// this will loop and add the sum + the cities + the layover time
	for (; startCity < endCity; startCity++)
	{
		// this formula is adding the city and adding the layover and adding the time of flight together
		sum = sum + city[startCity][0] + city[startCity][1];
	}
	// you subtract the layover becasue the loop will add an aditional layover
	sum = sum - city[startCity-1][1];
	return sum;
}



/*
* Function: checkCity()
* Description: This checks to see if the inputs a valid or not
* Parameters: int cityStart, int endCity
* Return Values: status
*/



int checkCity(int cityStart, int endCity)
{
	int status = 0;
	// this will check if starting cities is less the end city
	if (cityStart<endCity)
	{
		status = 1;
	}
	else
	{
		status = 0;
	}
	return status;
}



/*
* Function: convertTime()
* Description: Will convert the mins to hours and mins
* Parameters: int totalMins, int *hours, int *mins
* Return Values: nothing
*/



void convertTime(int totalMins,int *hours,int *mins)
{
	//this converts the totalMins to hours and mins 
	*hours = totalMins / 60;
	*mins = totalMins % 60;
}



/*
* Function: getUserStartingCity()
* Description: Will get user starting city and if it is 0
* it will retrun a value of 0 
* Parameters: int *startCity
* Return Values: nothing
*/



int getUserStartingCity(int *startCity)
{
	int status = 1; 

	printf("Enter the starting city: ");
	*startCity = getNum();

	// if user enter 0 then it will retrun a status of 0 which will exit the program
	if (*startCity == 0)
	{
		status = 0;
	}
	else
	{
		status = 1;
	}

	return status; 
}



/*
* Function: getUserStartingCity()
* Description: Will get user ending city
* Return Values: nothing
*/



void getUserEndingCity(int *endCity)
{
	printf("Enter the ending city: ");
// this will get user ending city using the getNum function and store it 
// store in the endCity adress
	*endCity = getNum();
}



int main(void)
{
	// array for all the cities 
	// this includes the time to get to the city and its layover
	int city[kcities][ktimeAndLayover] = {
		
		{ 255, 80 }, 
		{ 238, 46 }, 
		{ 235, 689 },
		{ 134, 53 },  
		{ 207, 0 },  
		{ 0, 0}   

	};

	//Varibles

	int sum = 0;
	int startingCitiesSelect = 0;
	int endingCitiesSelect = 0;
	int choice = 1;
	int time = 0;
	int checkStatus = 0;
	int hours = 0;
	int mins = 0;
	int status = 0; 
	
	do
	{
		
		cityMenu(); 
		
		// calling the getUserCity function and passisng the startingCitiesSelect adress 
		status = getUserStartingCity(&startingCitiesSelect);
		
		// This if statement is checking the status based on what user inputed if user input a 0
		// it will exit out of the do while loop
		if (status == 0)
		{
			choice = 0;
		}
		else
		{
			// this will get user ending cities if user starting city dose not equal to 0
			getUserEndingCity(&endingCitiesSelect);
		}
		
		// this checks to see if what user input for starting city and ending city or valid or not
		checkStatus = checkCity(startingCitiesSelect, endingCitiesSelect);
		
		//check status is a 0 the whole program will run but if it return anything other then a 0 it will display an error 
		if (checkStatus == 1)
		{
			// this function it taking in the array, user starting city and user ending city and will add them together
			sum = amountOfTime(city, startingCitiesSelect, endingCitiesSelect);
			// this will convert the total amount of mins by getting it the hours and mins 
			convertTime(sum, &hours, &mins);
			//this will display the hours and mins but with a semi
			printf("%d:%d or \n%d hours and %d mins\n", hours, mins, hours, mins);
		}
		else 
		{
			// this will display an error if checkStatus dose not equal to 1
			printf("ERROR\n");
		}
	// loops when choice dose not equal 0 
	} while (choice != 0);
	
	return 0;
}