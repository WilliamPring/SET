/*
* file Name: header.h
* By: William Pring
* Date: March 31 2015
* Description: This containes all the prototypes of the program the includes and the defines
*/

#ifndef __HEADER_H__
#define __HEADER_H__
//include 
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <windows.h>
#include <list>
#include <iostream>
#include <string>
#include "Music.h"





//pragma warning 
#pragma warning(disable: 4996)
#define MAX_BUFFER_SIZE 512
#define MIN_ARGUMENT 2
#define THELASTCHARACTER 1
#define MIN_CONTAINER_SIZES 0

using namespace std;

//prototype
void findFiles(char* argv, list <string> &newMusic);
void parseInfo(char *input);
void findFiles(char* argv, list <Music> &newMusic);

#endif	