/*
* FILE : box.cpp
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* box object
*/


#include "stdafx.h"
#include "box.h"


Box::Box()
{
	boxPosX =0;
	boxPosY=0;
	boxWidth=50;
	boxHeight = 50;
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
