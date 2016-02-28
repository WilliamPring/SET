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
	birdHitBoxX = xBirdPos + HitBoxDimension;
	birdHitBoxY = yBirdPos + HitBoxDimension;
}
 
void Bird::setBirdFalling(bool statusOfFall)
{
	birdFallingMode = statusOfFall;
}

void Bird::playFallingSound()
{
	PlaySound(L"CartoonFalling.wav", NULL, SND_ASYNC);
}

bool Bird::getBirdFalling()
{
	return birdFallingMode;
}
int Bird::getBirdHitBoxY()
{
	return birdHitBoxY;
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

void Bird::SetUpBirdMovement()
{
	highPotentialHeight = 0;
	lowPotentialHeight = 0;
	//highest Poistion bird will elevate to
	highPotentialHeight += orgion *.83;
	//lowest point bird will go to
	lowPotentialHeight = orgion + (orgion - highPotentialHeight);
	//speed of the movement 
	birdVelocity = (rand() % (10 - 4)) + 4;

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
	birdHitBoxX = xBirdPos + HitBoxDimension;
	birdHitBoxY = yBirdPos + HitBoxDimension;
}


int Bird::SetUpReferencePoints(int screenSizeHeight)
{
	//total size subtrack 25 percent of the screen of possible position of the bird to start up from
	int lowestMovement = screenSizeHeight - (screenSizeHeight * .3);
	int highestMovement = screenHeight * .3;
	int startPosition = rand() % (lowestMovement - highestMovement + 1) + highestMovement;
	return startPosition;
}

Bird::~Bird()
{

}




void Bird::setScreenHeight(int screenHeight)
{
	this->screenHeight = screenHeight;
}

