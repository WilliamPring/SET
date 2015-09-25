/*
Filename cA5_proto.h
Project: Assignment 5
Programer: William Pring
Date: November 26, 2014
Discription: The purpose of this proto includes all of my prototypes that I use in my program 
*/



//Prototypes
#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#pragma warning(disable: 4996)




/*
* Function: getFileLength()
* Description: Will get the file size
* Parameters: const char* argv
* Return Values: the file sizes or retruns -1
*/

int getFileLength(const char*argv);

