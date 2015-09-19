/*
* file Name: dsA3
* By: William Pring
* Date: March 31 2015
* Description: This main will add the .mp3 or \\.mp3 depending on the argument then pass it the 
fucnction that will put it inside the containers
*/


#include "header.h"
#include "Music.h"



int main(int argc, char *argv[])
{
	list <Music>::iterator iter;
	list <Music> music;

	char dir[MAX_BUFFER_SIZE] = "";
	int i = 1;
	int counter = 0;
	int length = 0;

	if (argc < MIN_ARGUMENT)
	{
		printf("Error: Usage: dsA1 <directory...>\n");
	}
	else
	{
		//will loop through all the agrument 
		do
		{
			//get the length of the argv[i]
			length = strlen(argv[i]);
			//check the last postion in the code to see if there is a backslash the reason you minus one is because it an array
			//and an array start from one
			if (argv[i][length - THELASTCHARACTER] == '\\')
			{
				//if it dose have a slash all you need to do it put the copy and put it into the dir
				strcpy(dir, argv[i]);
				//after putting the directory in memory append the *.mp3 to it 
				strcat(dir, "*.mp3");
				findFiles(dir, music);
				//then increment by 1 to increase the loop counter
				i++;
			}
			//if not it means there is directory then put th //*.mp3
			else
			{
				strcpy(dir, argv[i]);
				//if the directory dose not contain a // then append a slash with a *.mp3
				strcat(dir, "\\*.mp3");
				//get the head and return it back so it will not get deleat it
				 findFiles(dir, music);
				//then increment by 1 to increase the loop counter
				i++;
			}

		} while (i < argc);
		//loop until end display the artist and the title
		for (iter = music.begin(); iter != music.end(); iter++)
		{
			if ((counter % 20 == 0) && (counter != 0))
			{
				printf("Press any key to continue \n");
				counter = 0;
				_getch();
			}
			printf("Artist: %s		", (*iter).GetArtist().c_str());
			printf("Title: %s\n", (*iter).GetTitle().c_str());
			counter++;
		}
		music.clear();
	}
	return 0;
}