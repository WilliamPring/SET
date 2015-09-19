/*
* file Name: Music.cpp
* By: William Pring
* Date: March 31 2015
* Description: These are the function and methods of the Music class
*/


#include "Music.h"




/*
* Function: GetTitle()
* Description: This is a getter that will get the title
* Parameters: Nothing
* Returns: string
*/
string Music::GetTitle()
{
	return title;
}


/*
* Function: GetArtist()
* Description: This is a getter that will get the artist
* Parameters: Nothing
* Returns: string
*/

string Music::GetArtist()
{
	return artist;
}



/*
* Function: Music()
* Description: This is a defult contsructor that will set everything
* Parameters: Nothing
* Returns: Nothing
*/

Music::Music()
{
	title = "";
	artist = "";
}


/*
* Function: Music()
* Description: This is a contsructor that will take a string then parse it into diffrent parts if there a problem that
* that dose not me the requirement set the strings to nothign
* Parameters: Nothing
* Returns: Nothing
*/

Music::Music(string fullCurrentSongName)
{
	size_t found = 0;
	size_t foundTitle = 0;
	foundTitle = fullCurrentSongName.rfind(".");
	found = fullCurrentSongName.find('-');
	//check to see if there is a "-" and a "."
	if ((found == string::npos) || (foundTitle == string::npos))
	{
		artist = "";
		title = "";
	}
	else
	{
		//parsing the substrings
		artist = artist.assign(fullCurrentSongName, 0, found);
		title = title.assign(fullCurrentSongName, found + 1, (foundTitle - found) - 1);
	}
}



/*
* Function: operator<()
* Description: This is a overloading operator will overload the less then character "<"
* Parameters: Nothing
* Returns: Nothing
*/


bool Music::operator<(const Music& song) const
{
	bool status = true;
	if (this->title < song.title)
	{
		status = true;
	}
	else
	{
		if (this->title == song.title)
		{
			if (this->artist < song.artist)
			{
				status = true;
			}
			else
			{
				status = false;
			}
		}
		else
		{
			status = false;
		}
	}
	return status;
}


