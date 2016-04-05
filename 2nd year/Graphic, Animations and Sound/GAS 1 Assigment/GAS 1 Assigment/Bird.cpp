#include "stdafx.h"
#include "Bird.h"

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

void Bird::MoveBird()
{
	SetUpBirdMovement();
	//if (birdVelocity < 0)
	//{
	//	xBirdPos += (birdVelocity - (screenWidth*.04));
	//}
	//else
	//{
		xBirdPos += (birdVelocity + (screenWidth*.04));
	//}
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


void Bird::SetUpReferencePoints(int screenHeight, int screenWidth)
{
	xBirdPos = rand() % ((screenWidth-30)+30);
	yBirdPos = screenHeight* .85;
	SetUpBirdMovement();
	birdHitBoxX = xBirdPos + (xBirdPos *.1);
	birdHitBoxY = yBirdPos + (yBirdPos*.1);
	pointOfNoReturn = screenHeight * .88;
}


int Bird::getPointOfNoReturn()
{
	return pointOfNoReturn;
}

void Bird::setPointOfNoReturn(int pointOfDeath)
{
	pointOfNoReturn = pointOfDeath * .88;
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


	if (xBirdPos > screenWidth+ (screenWidth*.04))
	{
		SetUpReferencePoints(screenHeight, screenWidth);
	}

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

