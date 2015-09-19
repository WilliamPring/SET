#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <Windows.h>
//pragma warning 
#pragma warning(disable: 4996)

typedef struct songInfo
{
	char *pArtist;
	char *pTitle;
}songInfo;

//Strut that has a field that is pointed to next and pointed to the theSong struct
typedef struct songList
{
	songInfo theCurrentSong;
	struct songList *next;
}songList;

void findFiles(char* argv, songList* curHashTable[128], songList* curHead);
unsigned long djb2(char *str);
songList *insertArtistAndTitle(char* buffer, songList* insert);
void lookupUserHashTable(songList *bucketInfo, char *input);
