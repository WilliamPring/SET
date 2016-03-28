#include "stdafx.h"
#include "box.h"

int Box::getBoxPosY()
{
	return boxPosY;
}

void Box::setBoxPosY(int curBoxPosY)
{
	boxPosY = curBoxPosY;

}

void Box::setBoxPosX(int curBoxPosX)
{
	boxPosX = curBoxPosX;
}

int Box::getBoxPosX()
{
	return boxPosY;
}

int Box::getBoxWidth()
{
	return boxWidth;
}

void Box::setBoxWidth(int newBoxWidth)
{
	boxWidth = newBoxWidth;
}

int Box::getBoxHeight()
{
	return boxHeight;
}

void Box::setBoxHeight(int newBoxHeight)
{
	boxHeight = newBoxHeight;
}
