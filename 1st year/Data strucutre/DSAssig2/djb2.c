/*
* file name: djb2.c
* By: William Pring
* Date: feb 13, 2015
* Description: this will deal with adding things into the bucket and organize it
*/


# include "Header.h"


/*
* Function: hash()
* Description: This function  will return a hash key base on user input
* Parameters: unsigned char *str
* Returns: hash % 11
*/



unsigned long djb2(char *str)
{
	unsigned long hash = 5381;
	int c = 0;
	while ((c = *str++) != '\0')
	{
		//hash mutiple by 32 + the hashbalue then add it with c
		hash = ((hash << 5) + hash) + tolower(c);
	}

	//mod base on the bucket
	return hash %2;
}