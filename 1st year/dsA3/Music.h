/*
* file Name: header.h
* By: William Pring
* Date: March 31 2015
* Description: This containes all the prototypes for my Music class the includes and the defines
*/

#ifndef __MUSIC_H__
#define __MUSIC_H__

#include <string>
#include <Windows.h>
#include <list>
#include <vector>
#include <cctype>
using namespace std;


/*
NAME	:	AmfmRadio
PURPOSE :	The purpose of this class is to parse the song name to title and artist
*/

class Music
{
private:
	string title;
	string artist;

public:
	Music(string fullCurrentSongName);
	Music();
	string GetTitle();
	string GetArtist();
	bool operator<(const Music& song) const;
};
#endif