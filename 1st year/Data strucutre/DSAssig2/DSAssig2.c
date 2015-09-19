#include "Header.h"


int main(int argc, char *argv[])
{
	int length = 0;
	int i = 1;
	songList* hashTable[128] = { NULL };
	songList* head = NULL;
	char dir[121] = "";
	char userInput[121] = "";

	if (argc != 2)
	{
		printf("No Directory inpputed\n");
	}
	else
	{
		do
		{
			length = strlen(argv[i]);
			strcpy(dir, argv[i]);
			//check the last postion in the code to see if there is a backslash the reason you minus one is because it an array
			//and an array start from one
			if (argv[i][length - 1] == '\\')
			{
				//if it dose have a slash all you need to do it put the copy and put it into the dir
				//after putting the directory in memory append the *.mp3 to it 
				strcat(dir, "*.mp3");
			}
			else
			{
				//if the directory dose not contain a // then append a slash with a *.mp3
				strcat(dir, "\\*.mp3");
			}
			findFiles(dir, hashTable, head);
			i++;
		} while (i < argc);
	}
	fgets(userInput, 121, stdin);
	lookupUserHashTable(hashTable, userInput);
}


void lookupUserHashTable(songList *bucketInfo, char *input)
{
	
	songList *check = NULL;
	int inputNotFound = 1;
	//set the check var into checkBucket
	check = bucketInfo;
	//while the check is not equal to NULL
	while (check != NULL)
	{
		printf("Seraching for %s found : %s\n", input, check->theCurrentSong.pTitle);
		//compare the check userInput to the input
		if (stricmp(check->theCurrentSong.pTitle, input) == 0)
		{
			printf("Sucessful\n");
			inputNotFound = 0;
			break;
		}
		else
		{
			inputNotFound = 1;
		}
		//increment it
		check = check->next;
	}
	if (inputNotFound == 1)
	{
		printf("Could not find: %s \n", input);
	}
}