/*
* FILE : box.cpp
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* box object representation
*/


#include "stdafx.h"
#include "box.h"

int Box::getBoxVelX()
{
	return boxXVel;
}
void Box::setBoxVelX()
{
	boxXVel = (rand() % 17)*(-1);
}
int Box::getBoxVelY()
{
	return boxYVel;
}
void Box::setBoxVelY()
{
	boxYVel = rand() % 17 + (-8);
}
int Box::getBoxMovementX()
{
	boxPosX -= boxXVel;
	return boxPosX;
}

int Box::getBoxMovementY()
{
	boxPosY -= boxYVel;
	return boxPosY;
}
Box::Box()
{
	boxYVel = 0;
	boxXVel = 0;
	srand(unsigned(time(NULL)));
	hit = false;
	boxPosX =0;
	boxPosY=0;
	boxWidth=50;
	boxHeight = 50;
	boxMovementY = 0;
	boxMovementX = 0;
}
/*
* NAME : getBoxPosY
* PURPOSE : geter for the boxPosY
*/
int Box::getBoxPosY()
{
	return boxPosY;
}
/*
* NAME : setBoxPosY
* PURPOSE: set the boxPosY  
*/
void Box::setBoxPosY(int curBoxPosY)
{
	boxPosY = curBoxPosY;

}


void Box::setBoxHit(bool status)
{
	hit = status; 
}

bool Box::getBoxHit()
{
	return hit;
}

/*
* NAME : setBoxPosX
* PURPOSE: set the boxPosX
*/
void Box::setBoxPosX(int curBoxPosX)
{
	boxPosX = curBoxPosX;
}
/*
* NAME : getBoxPosX
* PURPOSE: get the boxPosX
*/
int Box::getBoxPosX()
{
	return boxPosX;
}
/*
* NAME : getBoxWidth
* PURPOSE: get the boxWidth
*/
int Box::getBoxWidth()
{
	return boxWidth;
}
/*
* NAME : setBoxWidth
* PURPOSE: set the boxWidth
*/
void Box::setBoxWidth(int newBoxWidth)
{
	boxWidth = newBoxWidth;
}
/*
* NAME : getBoxHeight
* PURPOSE: get the boxHeight
*/
int Box::getBoxHeight()
{
	return boxHeight;
}
/*
* NAME : setBoxHeight
* PURPOSE: set the boxHeight
*/
void Box::setBoxHeight(int newBoxHeight)
{
	boxHeight = newBoxHeight;
}
