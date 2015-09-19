/*
FileName: SETTree.cpp
Project: Assigment 5
By: William Pring
Date: April 3 2015
Description: This will model a tree structure
*/

#include <stdio.h>
#include <string>
#include <Windows.h>
#pragma warning(disable: 4996)
using namespace std;
//prototype
#define SIZE 2048
bool ListDirectoryContents(char *directory, int numSpace);
void createSpace(int numberOfSpace);


int main(int argc, char* argv[])
{
	char directory[SIZE] = "";
	bool status = true;
	bool correctDirectory = true;
	int totalSpaces = 1;
	//if aguement is less then 2
	if (argc < 2)
	{
		printf("Error you need more then 1 arument");
		status = false;
	}
	else
	{
		//dose not allow the directory to be to long
		if (strlen(argv[1]) > SIZE)
		{
			printf("To long of a directory");
		}
		else
		{
			//copy the argv[1] int directory
			strcpy(directory, argv[1]);
			//print it out
			printf("%s\n", argv[1]);
			correctDirectory = ListDirectoryContents(directory, totalSpaces);
			//if the correctDirectory return false
			if (correctDirectory == false)
			{
				printf("Path was not found\n");
				status = false;
			}
		}
	}
}



/*
* Function: ListDirectoryContents()
* Description: recusion base on the files or the folders and display
* Parameters: char *directory, int numSpace
* Returns: bool
*/


bool ListDirectoryContents(char *directory, int numSpace)
{
	WIN32_FIND_DATA fdFile;
	HANDLE hFind = NULL;
	int prevSpace = 0;
	int length = 0;
	bool once = true;
	bool status = true;
	char theCurrentPath[SIZE] = "";
	//check to seee if use but a back slash
	if (directory[length = strlen(directory) - 1] == '\\')
	{
		strcpy(theCurrentPath, directory);
		strcat(theCurrentPath, "*.*");
	}
	else
	{
		strcpy(theCurrentPath, directory);
		//append a dot
		strcat(theCurrentPath, "\\*.*");
	}
	//check to see if the directory is valid
	hFind = FindFirstFile(theCurrentPath, &fdFile);
	if (hFind == INVALID_HANDLE_VALUE)
	{
		status = false;
	}
	else
	{

		do
		{
			if ((strcmp(fdFile.cFileName, ".") != 0) && (strcmp(fdFile.cFileName, "..") != 0))
			{

				sprintf(theCurrentPath, "%s\\%s", directory, fdFile.cFileName);

				//Check to see if it is the File or Folder
				if (fdFile.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
				{
					createSpace(numSpace);
					printf("%s\n", fdFile.cFileName);
					ListDirectoryContents(theCurrentPath, numSpace + 1);
				}
				else
				{
					//this is the file
					createSpace(numSpace);
					printf("%s\n", fdFile.cFileName);
				}
			}
		} while (FindNextFile(hFind, &fdFile));
		FindClose(hFind);
	}
	return status;
}


/*
* Function: createSpace()
* Description: create dashes depend on what level you are on
* Parameters: int numberOfSpace
* Returns: bool
void
*/


void createSpace(int numberOfSpace)
{
	for (int i = 0; i < numberOfSpace; i++)
	{
		printf("--");
	}
}