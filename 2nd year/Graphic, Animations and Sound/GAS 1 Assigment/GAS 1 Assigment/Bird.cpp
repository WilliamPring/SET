#include "stdafx.h"
#include "Bird.h"

Bird::Bird()
{
	birdFlyPos = 0;
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
	horizontalDir = true;
	//bird starting height for now
	yBirdPos = orgion;
	birdFallingMode = false;
	SetUpBirdMovement();
	birdHitBoxX = xBirdPos +( xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
	pointOfNoReturn = screenHeight * .85;
}


int Bird::getBirdHitBoxY()
{
	return birdHitBoxY;
}

int Bird::getBirdHitBoxX()
{
	return birdHitBoxX;
}

void Bird::changeFlyPos()
{
	birdFlyPos++;
	if (birdFlyPos == 4)
	{
		birdFlyPos = 0;
	}
}
int Bird::getBirdFlyPos()
{
	return birdFlyPos;
}

void Bird::BirdFallingToDeath()
{
	disappearBird = screenHeight - (screenHeight * .82);
	xBirdPos += 12;
	yBirdPos += 12;

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
	birdVelocity = (rand() % (15 - 4)) + 4;

}

void Bird::MoveBird()
{
	//	xBirdPos += rand() % 17 + (-8);

	xBirdPos += 12;
	//setting up reference points
	if (xBirdPos > screenWidth)
	{
		orgion = SetUpReferencePoints(screenHeight);
		xBirdPos = 0;
		yBirdPos = orgion;
		SetUpBirdMovement();
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
	xBirdPos = xBirdPos + 8;
	yBirdPos = yBirdPos + 8;
}

bool Bird::getBirdFalling()
{
	return birdFallingMode;
}


int Bird::getScreenHeight()
{
	return screenHeight;
}

int Bird::getScreenWidth()
{
	return screenWidth;
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

