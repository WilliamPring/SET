/*
* FILE : head.cpp
* PROJECT : SET-JIBJAB
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION : The head class the represent the head object
*/



#include "stdafx.h"
#include "head.h"


/*
* NAME : setScreenHeight
* PURPOSE : Set the new screen height
*/
void Head::setScreenHeight(int newHeight)
{
	screenHeight = newHeight;
}

/*
* NAME : setScreenWidth
* PURPOSE : Set the new screen width
*/

void Head::setScreenWidth(int newWidth)
{
	screenWidth = newWidth;
}

/*
* NAME : Head
* PURPOSE : head contsrutor
*/
Head::Head()
{
	x = 0;
	y = 0;
	velX = 0;
	velY = 0;
	screenHeight =0;
	screenWidth =0;
	resize = NULL;
}
/*
* NAME : initProject
* PURPOSE : the starting position for the head
*/
void Head::initProject(int height, int width)
{
	//starting position
	x = rand() % ((width - 200) - 200 + 1) + 200;
	y = rand() % ((height - 200) - 200 + 1) + 200;
	velX = (rand() % 9 + 1) - 5;
	velY = (rand() % 9 + 1) - 5;
}

/*
* NAME : getX
* PURPOSE : getting the head positionX
*/
int Head::getX()
{
	return x;
}

/*
* NAME : getY
* PURPOSE : getting the position of Y
*/
int Head::getY()
{
	return y;
}


/*
* NAME : setResize
* PURPOSE : getting the resize bool
*/
void Head::setResize(bool info)
{
	resize = info;
}

/*
* NAME : changeDirAndSpeed
* PURPOSE : change the direction and speed of the head base on randomization
*/
void Head::changeDirAndSpeed()
{
	int random = (rand() % (4 - 1)) + 1;
	if (random == 1)
	{
		velX = (rand() % 10);
		velY = (rand() % 9 + 1) - 5;
		
	}
	else if (random == 2)
	{
		velX = (rand() % 10)*-1;
		velY = (rand() % 9 + 1) - 5;
	}
	else if (random == 3)
	{
		velY = (rand() % 10)*-1;
		velX = (rand() % 9 + 1) - 5;
	}
	else
	{
		velY = (rand() % 10);
		velX= (rand() % 9 + 1) - 5;
	}
	x += velX;
	y += velY;
}

/*
* NAME : getFaceHeight
* PURPOSE : get the face Height
*/
int Head::getFaceHeight()
{
	return faceHeight;
}
/*
* NAME : getFaceWidth
* PURPOSE : get the the face Width
*/
int Head::getFaceWidth()
{
	return faceWidth;
}

/*
* NAME : setFaceHeight
* PURPOSE : set the face height
*/
void Head::setFaceHeight(int newFaceHeight)
{
	faceHeight = screenHeight;
}


/*
* NAME : setFaceWidth
* PURPOSE : set the face width
*/
void Head::setFaceWidth(int newFaceWidth)
{
	faceWidth = newFaceWidth;
}

/*
* NAME : move
* PURPOSE : move function to move the face
*/
void Head::move()
{
	if (x + (screenWidth * .2)> screenWidth)
	{
		velX = (rand() % 10)*-1;
		velY = (rand() % 9 + 1) - 5;
	
	}
	else if (x < 0)
	{
		velX = (rand() % 10);
		velY = (rand() % 9 + 1) - 5;
	}


	if (y + (screenHeight * .2)> screenHeight)
	{
		velY = (rand() % 10)*-1;
		velX = (rand() % 9 + 1) - 5;
		
	}
	else if (y < 0)
	{
		velY = (rand() % 10);
		velX= (rand() % 9 + 1) - 5;
		
	}
	x += velX;
	y += velY;

}