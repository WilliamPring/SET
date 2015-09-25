/*
Filename fileUtilties.c
Project: Assignment 5
Programer: William Pring
Date: November 26, 2014
Discription: The purpose of this program would be to get the file sizes and retrun if it there is an error
it would return a -1 and display an error message
*/


#include "cA5_proto.h"


int getFileLength(const char*argv)
{
	int size = 0;
	//Declre a filedata of WIN32
	WIN32_FIND_DATA filedata = { 0 };
	//Find the first file location
	HANDLE h = FindFirstFile(argv, &filedata);
	//if
	if (h != INVALID_HANDLE_VALUE  )
	{
		//this will get the sizes of the filedata
		size =  filedata.nFileSizeLow;
	}
	else
	{
		//display an error when FindFirstFile run into an error 
		printf("Error message");
		// returns a -1
		size = -1;
	}
	FindClose(h);
	return size;
}