
#include "Header.h"


void findFiles(char* argv, songList* curHashTable[128], songList* curHead)
{
	int bucketNumber = 0;
	songList * newBlockOfMemoryForHead = NULL;
	songList * newBlockOfMemory = NULL;
	WIN32_FIND_DATA FindingFileData = { 0 };
	HANDLE hFind = INVALID_HANDLE_VALUE;
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
				//allocate newBlockOfMemory
				newBlockOfMemory = (songList*)malloc(sizeof(songList));
				newBlockOfMemory = insertArtistAndTitle(newBlockOfMemory, FindingFileData.cFileName);
				//return head of where is left off 
				bucketNumber = djb2(newBlockOfMemory->theCurrentSong.pTitle);
				newBlockOfMemoryForHead = (songList*)malloc(sizeof(songList));
				newBlockOfMemoryForHead = insertArtistAndTitle(newBlockOfMemoryForHead, FindingFileData.cFileName);
				// pass file name, and the head from your function parmeters
				curHashTable[bucketNumber] = insertNewSong(newBlockOfMemory, &curHashTable[bucketNumber]);
				curHead = insertNewSong(newBlockOfMemoryForHead, &curHead);

			}

		} while (FindNextFile(hFind, &FindingFileData));
	}

	songList* pointer = NULL;
	for (int i = 0; i < 5; i++)
	{
		pointer = curHashTable[i];
		while (pointer != NULL)
		{
			printf("%3d) %-30s %-30s\n", i + 1, pointer->theCurrentSong.pArtist, pointer->theCurrentSong.pTitle);
			pointer = pointer->next;
		}
	}
	songList * hey = curHead;
	while (hey != NULL)
	{
		printf("%s\n", hey->theCurrentSong.pTitle);
		hey = hey->next;
	}
	//close hFind
	FindClose(hFind);
}


songList* insertNewSong(songList* userInput, songList** whichBucket)
{
	int lengthOfArtist = 0;
	songList *beforeElement = NULL;
	songList *afterElement = NULL;
	//allocating memory
	//if head is equal to NULL this happen only at the start of the list
	if (*whichBucket == NULL)
	{
		//set the head pointer and the tail pointer to the new block of memory you allocate
		*whichBucket = userInput;
	}
	else if (stricmp((*whichBucket)->theCurrentSong.pTitle, userInput->theCurrentSong.pTitle) >= 0)
	{
		//assign the newblock of memory to the location head is pointing to
		userInput->next = *whichBucket;
		//set head previous to the new block 
		//then set the head pointer to the new block so the new block will now have head 
		*whichBucket = userInput;
	}
	else
	{
		//the beforeElement and afterElement are just being set up so it knows where it located at
		beforeElement = *whichBucket;
		//set the after to point to the head next
		afterElement = (*whichBucket)->next;
		//loop until it knows where to put it
		while (afterElement != NULL)
		{
			//if your new title is greater then your then your old title then break out because you are in the right postion 
			if (stricmp(afterElement->theCurrentSong.pTitle, userInput->theCurrentSong.pTitle) >= 0)
			{
				break;
			}
			//set before element to the next 
			beforeElement = afterElement;
			//set the afterElment to point after what it was pointing at
			afterElement = afterElement->next;
		}
		//newBlockOfMemory next is pointing the next item of which your head was pointing to
		userInput->next = afterElement;
		//set the beforeNext which is pointing to the head and connect it with the newBlock of memory
		beforeElement->next = userInput;
		//this will check for if we at in NUll basical this is checking for if we are at the end
	}
	return *whichBucket;

}



		


songList *insertArtistAndTitle(char* buffer, songList* insert)
{
	int lengthOfArtist = 0;
	int lengthOfTitle = 0;
	char userInput[81] = "";

	strcpy(userInput, buffer);
	char* title = strchr(userInput, '-');
	//if it dose not equal to null then it will replace the - with the \0
	if (title != NULL)
	{
		//put a \0 at the location where title is pointing to
		*title = '\0';
		//increment it so you will get title without the '-'
		title++;
	}
	//contune to where you left off and seach for a dot
	char* temp = strrchr(title, '.');
	//if it dose not equal to zero continue with the program
	if (temp != NULL)
	{
		//put the \0 at the .
		*temp = '\0';
	}
	//artist
	lengthOfArtist = strlen((userInput)+1);
	//title
	lengthOfTitle = strlen((title)+1);
	insert->theCurrentSong.pArtist = (char*)malloc(lengthOfArtist * sizeof(char));
	insert->theCurrentSong.pTitle = (char*)malloc(lengthOfTitle * sizeof(char));
	if ((insert->theCurrentSong.pArtist != NULL) && (insert->theCurrentSong.pTitle != NULL))
	{
		strcpy(insert->theCurrentSong.pArtist, userInput);
		strcpy(insert->theCurrentSong.pTitle, title);
		insert->next = NULL;
	}
	else
	{
		printf("no Memory\n");
	}

	return insert;
}