#include <stdio.h>
#include <ctype.h>
#include <stdlib.h>
#include <windows.h>

#pragma warning(disable: 4996)



typedef struct
{
	char *pArtist;
	char *pTitle;
}songInfo;

//Strut that has a field that is pointed to next and pointed to the theSong struct

typedef struct nodeList
{
	songInfo theCurrentSong;
	struct nodeList *next;
	struct nodeList *prev;
} nodeList;



void findFiles(char* argv);
void parseInfo(char *input);
nodeList * insertNewSong(char* fileName, nodeList **head, nodeList **tail);
void freeTheList(nodeList *head);


void freeAll(nodeList *head)
{
	nodeList *curr = NULL;
	nodeList* next = NULL;

	curr = head;

	// traverse the list, being careful to not access freed blocks
	while (curr != NULL)
	{
		// keep a pointer to the next block so we can go there after it's freed
		next = curr->next;
		free(curr);
		curr = next;
	}

}



void showList(nodeList *head)
{
	nodeList *item = NULL;

	item = head;

	while (item != NULL)
	{
		printf("%s %s\n", item->theCurrentSong.pTitle, item->theCurrentSong.pArtist);
		item = item->next;
	}
}





int main(int argc, char *argv[])
{
	char dir[1000] = "";
	char * backSlashZero = "\0";
	int i = 1;
	int status = 0;
	char* serachForSlash = NULL;
	if (argc < 2)
	{
		printf("Error: Usage: dsA1 <directory...>");
	}
	else
	{
		//will loop through all the agrument 
		do
		{
			//if it pointing to a /0 then put the .mp3
			if (strcmp(&argv[i][(sizeof(argv[i]))], "\\") == 0)
			{
				strncpy(dir, argv[i], sizeof(dir));
				strcat(dir, "*");
				findFiles(dir);
				i++;
			}
			//if not it means there is directory then put th //*.mp3
			else
			{

				strncpy(dir, argv[i], sizeof(dir));
				strcat(dir, "\\*");
				findFiles(dir);
				i++;
			}

		} while (i < argc);
	}
	return 0;
}




void findFiles(char* argv)
{
	nodeList *head = NULL;
	nodeList * tail = NULL;
	int statusCondition = 0;
	WIN32_FIND_DATA FindingFileData = { 0 };
	HANDLE hFind = INVALID_HANDLE_VALUE;

	hFind = FindFirstFile(argv, &FindingFileData);

	if (hFind == INVALID_HANDLE_VALUE)
	{
		printf("Error");
	}
	else
	{
		do
		{
			//what ever that return true it will equal false This test the directory
			if (!(FindingFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
			{
				head = insertNewSong(FindingFileData.cFileName, &head, &tail);
			}
		} while (FindNextFile(hFind, &FindingFileData));
		showList(head);
		freeAll(head);
	}

	FindClose(hFind);

}




nodeList * insertNewSong(char* fileName, nodeList **head, nodeList **tail)
{
	int status = 1;
	nodeList * newBlockOfMemory = NULL;
	nodeList *current = NULL;
	nodeList *newNode = NULL;
	nodeList *beforeElement = NULL;
	nodeList *afterElement = NULL;
	char* pointer = NULL;

	newBlockOfMemory = (nodeList*)malloc(sizeof(nodeList));
	//check to see if new block is equal to NULL if it is return status -
	if (newBlockOfMemory == NULL)
	{
		printf("No memory");
		status = 0;
	}
	else
	{
		//if new block dose not equal to NULL continue on the program
		//these sequence of the next few lines are to parse the information to get the artist and the title of the song
		//the pointer to the first occerance of when the string has a -
		char* title = strchr(fileName, '-');
		//if it dose not equal to null then it will replace the - with the \0
		if (title != NULL)
		{
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
		newBlockOfMemory->theCurrentSong.pArtist = (char*)malloc(sizeof(fileName) + 1);
		if (newBlockOfMemory == NULL)
		{
			printf("Memory issue");
			status = 0;
		}
		else
		{
			//allocate memory for the title
			newBlockOfMemory->theCurrentSong.pTitle = (char*)malloc(sizeof(title) + 1);
			//check to see if the malloc return 0
			if (newBlockOfMemory->theCurrentSong.pArtist == NULL)
			{
				printf("Memory issue");
				status = 0;
			}
			else
			{

				//copy the artist 
				strcpy(newBlockOfMemory->theCurrentSong.pArtist, fileName);
				strcpy(newBlockOfMemory->theCurrentSong.pTitle, title);
				//intial values to be set to NULL
				newBlockOfMemory->prev = newBlockOfMemory->next = NULL;
				//if head is equal to NULL this happen only at the start of the list
				if (*head == NULL)
				{
					//set the head pointer and the tail pointer to the new block of memory you allocate
					*head = *tail = newBlockOfMemory;
					status = 0;
				}
				else if (strcmp((*head)->theCurrentSong.pTitle, newBlockOfMemory->theCurrentSong.pTitle) == 0)
				{
					if ((strcmp((*head)->theCurrentSong.pArtist, newBlockOfMemory->theCurrentSong.pArtist) >= 0))
					{	//assign the newblock of memory to the location head is pointing to
						newBlockOfMemory->next = *head;
						//set head previous to the new block 
						(*head)->prev = newBlockOfMemory;
						//then set the head pointer to the new block so the new block will now have head 
						*head = newBlockOfMemory;
						//you set this equal to zero because they dont want to enter the loop
						status = 0;
					}
				}
				//if your new title is greater then your then your old title
				else if (strcmp((*head)->theCurrentSong.pTitle, newBlockOfMemory->theCurrentSong.pTitle) > 0)
				{
					//assign the newblock of memory to the location head is pointing to
					newBlockOfMemory->next = *head;
					//set head previous to the new block 
					(*head)->prev = newBlockOfMemory;
					//then set the head pointer to the new block so the new block will now have head 
					*head = newBlockOfMemory;
					status = 0;
				}

			}
		}
	}

	if (status == 1)
	{
		//the beforeElement and afterElement are just being set up so it knows where it located at
		beforeElement = *head;
		afterElement = (*head)->next;
		//loop until it knows where to put it
		while (afterElement != NULL)
		{
			//if your new title is greater then your then your old title then break out because you are in the right postion 
			if (strcmp(afterElement->theCurrentSong.pArtist, newBlockOfMemory->theCurrentSong.pArtist) > 0)
			{
				break;
			}
			//if they are equal with the new and old tile then go inside the else if statement this is for the speical case
			else if (strcmp(afterElement->theCurrentSong.pTitle, newBlockOfMemory->theCurrentSong.pTitle) == 0)
			{
				//if they are equal or if your new artist is greater then the old artist then break this is for the special case
				if (strcmp(afterElement->theCurrentSong.pArtist, newBlockOfMemory->theCurrentSong.pArtist) >= 0)
				{

					break;
				}
			}
			//set before element to the next 
			beforeElement = afterElement;
			//set the afterElment to point after what it was pointing at
			afterElement = afterElement->next;
		}
		//this will set the newBlockOfMemory previous to equal to the head of what it was it 
		newBlockOfMemory->prev = beforeElement;
		//newBlockOfMemory next is pointing the next item of which your head was pointing to
		newBlockOfMemory->next = afterElement;
		//set the beforeNext which is pointing to the head and connect it with the newBlock of memory
		beforeElement->next = newBlockOfMemory;
		//this will check for if we at in NUll basical this is checking for if we are at the end
		if (afterElement == NULL)
		{
			*tail = newBlockOfMemory;
		}
		else
		{
			afterElement->prev = newBlockOfMemory;
		}
	}

	return *head;
}







