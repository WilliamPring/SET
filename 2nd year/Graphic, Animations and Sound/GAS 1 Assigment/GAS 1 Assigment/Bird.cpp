/*
* FILE : Bird.cpp
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION : Bird Class that contain supporting function
*/

#include "stdafx.h"
#include "Bird.h"
/*
* NAME : Bird
* PURPOSE : Bird constructor
*/

Bird::Bird()
{
	birdFlyPos = 0;
	srand(unsigned(time(NULL)));
	toggleLeftOrRight = true;
	screenHeight = 0;	
	screenWidth = 0;
	//setting the bird velocity
	birdVelocity = 0;
	//set up where it will first start the bird 
	birdFallingMode = false;
	birdHitBoxX = 0;
	leftOrRight = true;
	birdHitBoxY = 0;
	pointOfNoReturn = 0;
}
/*
* NAME : getBirdHitBoxY
* PURPOSE : get the bird height hit box
*/


int Bird::getBirdHitBoxY()
{
	return birdHitBoxY;
}


/*
* NAME : changeFlyPos
* PURPOSE : get the brid hit box for X
*/

int Bird::getBirdHitBoxX()
{
	return birdHitBoxX;
}


/*
* NAME : changeFlyPos
* PURPOSE : change the sprite position of what the bird will use
*/

void Bird::changeFlyPos()
{
	birdFlyPos++;
	if (birdFlyPos == 4)
	{
		birdFlyPos = 0;
	}
}


/*
* NAME : getBirdFlyPos
* PURPOSE : gettting the bird flying position
*/

int Bird::getBirdFlyPos()
{
	return birdFlyPos;
}

/*
* NAME : SetUpBirdMovement
* PURPOSE : Setting up to cordinate for the bird to move
*/


void Bird::SetUpBirdMovement()
{
	int oldBirdVelocity = birdVelocity;
	int random = rand() % 2;
	
	while (true)
	{
		birdVelocity = rand() % 17 + (-8);
		if (oldBirdVelocity == birdVelocity)
		{
			continue;
		}
		else
		{
			break;
		}
	}
}
/*
* NAME : MoveBird
* PURPOSE : Moving the bird or generating new location for bird to move
*/

void Bird::MoveBird()
{
	SetUpBirdMovement();
	if (birdVelocity < 0)
	{
		xBirdPos += (birdVelocity - (screenWidth*.04));
	}
	else
	{
		xBirdPos += (birdVelocity + (screenWidth*.04));
	}
	yBirdPos -= 20;
	//setting up reference points
	if (xBirdPos > screenWidth-(screenWidth*.05))
	{
		SetUpReferencePoints(screenHeight, screenWidth); 
	}
	if (yBirdPos < (screenHeight * -.09))
	{
		SetUpReferencePoints(screenHeight, screenWidth);
	}
	if (xBirdPos < 0)
	{
		SetUpReferencePoints(screenHeight, screenWidth);
	}
	birdHitBoxX = xBirdPos + (xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
}

/*
* NAME : SetUpReferencePoints
* PURPOSE : Setting up the intail values and movement
*/

void Bird::SetUpReferencePoints(int screenHeight, int screenWidth)
{
	xBirdPos = rand() % ((screenWidth-30)+30);
	yBirdPos = screenHeight* .85;
	SetUpBirdMovement();
	birdHitBoxX = xBirdPos + (xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
	pointOfNoReturn = screenHeight * .88;
}
/*
* NAME : getPointOfNoReturn
* PURPOSE : returning the bird where it should be deleted
*/

int Bird::getPointOfNoReturn()
{
	return pointOfNoReturn;
}
/*
* NAME : setPointOfNoReturn
* PURPOSE : Set up the new screen
*/
void Bird::setPointOfNoReturn(int pointOfDeath)
{
	pointOfNoReturn = pointOfDeath * .88;
}
/*
* NAME : setXBirdPos
* PURPOSE : return x bird pos
*/
void Bird::setXBirdPos(int curPost)
{
	xBirdPos = curPost;
}
/*
* NAME : setBirdFalling
* PURPOSE : Return bird filling mode
*/
void Bird::setBirdFalling(bool statusOfFall)
{
	birdFallingMode = statusOfFall;
}
/*
* NAME : birdDieingToDeath
* PURPOSE : move the bird when it is dieing
*/
void Bird::birdDieingToDeath()
{
	xBirdPos = xBirdPos + 8;
	yBirdPos = yBirdPos + 8;


	if (xBirdPos > screenWidth+ (screenWidth*.04))
	{
		SetUpReferencePoints(screenHeight, screenWidth);
	}

}

/*
* NAME : getBirdFalling
* PURPOSE : Get the status of the bird death 
*/
bool Bird::getBirdFalling()
{
	return birdFallingMode;
}
/*
* NAME : getScreenHeight
* PURPOSE : return the screen size height
*/

int Bird::getScreenHeight()
{
	return screenHeight;
}
/*
* NAME : getScreenHeight
* PURPOSE : return the screen size width
*/

int Bird::getScreenWidth()
{
	return screenWidth;
}

/*
* NAME : getYBirdPos
* PURPOSE : return the birds y pos
*/
int Bird::getYBirdPos()
{
	return yBirdPos;
}
/*
* NAME : getXBirdPos
* PURPOSE : return the birds X pos
*/
int Bird::getXBirdPos()
{
	return xBirdPos;
}

/*
* NAME : setScreenWidth
* PURPOSE : set screen width size
*/
void Bird::setScreenWidth(int screenWidth)
{
	this->screenWidth = screenWidth;
}
/*
* NAME : setScreenHeight
* PURPOSE : set the screen Height
*/
void Bird::setScreenHeight(int screenHeight)
{
	setPointOfNoReturn(screenHeight);
	this->screenHeight = screenHeight;
}

