#include "stdafx.h"
#include "Bird.h"

Bird::Bird()
{
	srand(unsigned(time(NULL)));
	toggle = true;
	screenHeight = 423;	
	screenWidth = 1009;
	//setting the bird velocity
	birdVelocity = 0;
	//set up where it will first start the bird 
	orgion = SetUpReferencePoints(screenHeight);
	//Bird start postion for now in terms of 
	xBirdPos = 0;
	//bird starting height for now
	yBirdPos = orgion;
	birdFallingMode = false;
	SetUpBirdMovement();
	birdHitBoxX = xBirdPos +( xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
	pointOfNoReturn = screenHeight * .85;
}


void Bird::SetUpBirdMovement()
{
	highPotentialHeight = 0;
	lowPotentialHeight = 0;
	//highest Poistion bird will elevate to
	highPotentialHeight += orgion *.83;
	//lowest point bird will go to
	lowPotentialHeight = orgion + (orgion - highPotentialHeight);
	//speed of the movement 
	birdVelocity = (rand() % (20 - 4)) + 4;

}

void Bird::MoveBird()
{
	xBirdPos += 12;

	if (xBirdPos > screenWidth)
	{
		orgion = SetUpReferencePoints(screenHeight);
		xBirdPos = 0;
		yBirdPos = orgion;
		SetUpBirdMovement();
		Sleep(1000);
	}
	else
	{
		if (toggle == false)
		{
			yBirdPos += birdVelocity;
			if (yBirdPos > lowPotentialHeight)
			{
				toggle = !toggle;
			}
		}
		else
		{
			yBirdPos -= birdVelocity;
			if (yBirdPos <= highPotentialHeight)
			{
				toggle = !toggle;
			}
		}
	}
	birdHitBoxX = xBirdPos + (xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
}


int Bird::SetUpReferencePoints(int screenSizeHeight)
{
	//total size subtrack 25 percent of the screen of possible position of the bird to start up from
	int lowestMovement = screenSizeHeight - (screenSizeHeight * .3);
	int highestMovement = screenHeight * .3;
	int startPosition = rand() % (lowestMovement - highestMovement + 1) + highestMovement;
	return startPosition;
}


int Bird::getPointOfNoReturn()
{
	return pointOfNoReturn;
}

void Bird::setPointOfNoReturn(int pointOfDeath)
{
	pointOfNoReturn = pointOfDeath * .85;
}

void Bird::setXBirdPos(int curPost)
{
	xBirdPos = curPost;
}

void Bird::setBirdFalling(bool statusOfFall)
{
	birdFallingMode = statusOfFall;
}

void Bird::birdDieingToDeath()
{
	xBirdPos = xBirdPos + 19;
	yBirdPos = yBirdPos + 19;
}

bool Bird::getBirdFalling()
{
	return birdFallingMode;
}
int Bird::getBirdHitBoxY()
{
	return birdHitBoxY;
}

int Bird::getScreenHeight()
{
	return screenHeight;
}

int Bird::getScreenWidth()
{
	return screenWidth;
}

int Bird::getBirdHitBoxX()
{
	return birdHitBoxX;
}

int Bird::getYBirdPos()
{
	return yBirdPos;
}

int Bird::getXBirdPos()
{
	return xBirdPos;
}

void Bird::setScreenWidth(int screenWidth)
{
	this->screenWidth = screenWidth;
}

void Bird::setScreenHeight(int screenHeight)
{
	setPointOfNoReturn(screenHeight);
	this->screenHeight = screenHeight;
}

