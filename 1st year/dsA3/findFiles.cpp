/*
* file Name: findFile.cpp
* By: William Pring
* Date: March 31 2015
* Description: This will call a dynamic create a Music object and store in the containe
*/





#include "header.h"



/*
* Function: findFiles()
* Description: This function find the file and check if it a directory it not then it will pass the name of the file to the
* insert (code from mircosoft)
* Parameters: char* argv, list <Music> &newMusic
* Returns: nothing
*/



void findFiles(char* argv, list <Music> &newMusic)
{
	WIN32_FIND_DATA FindingFileData = { 0 };
	HANDLE hFind = INVALID_HANDLE_VALUE;
	Music *mySongs = NULL;
	size_t number = 0;
	//find the first file
	hFind = FindFirstFile(argv, &FindingFileData);
	//check to see if there was an error when finding the first file
	if (hFind == INVALID_HANDLE_VALUE)
	{
		printf("Error\n");
	}
	else
	{
		do
		{
			//what ever that return true it will equal false This test the directory
			if (!(FindingFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
			{
				mySongs = new Music(FindingFileData.cFileName);
				if (mySongs == NULL)
				{
					printf("Allocation failed \n");
					break;
				}
				//check to see if it return nothing
				if ((mySongs->GetArtist() != "") || (mySongs->GetTitle() != ""))
				{
					//if it did not push it
					newMusic.push_back(*mySongs);
					delete mySongs; 
				}

			}

		} while (FindNextFile(hFind, &FindingFileData));
	}
	//get the container
	number = newMusic.size();
	//if the container it not zero allow it sort
	if (number != MIN_CONTAINER_SIZES)
	{
		newMusic.sort();
	}
	
	//close hFind
	FindClose(hFind);

}
