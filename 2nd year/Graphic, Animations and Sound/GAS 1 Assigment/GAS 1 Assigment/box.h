/*
* FILE : box.h
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* Heading box
*/

#pragma once

/*
CLASS		: Box
DESCRIPTION	:
Class to represent an object of the box on the screen. The Box can move and can reset it's position
*/
class Box
{
public:
	int getBoxPosY();
	void setBoxPosY(int curBoxPosY);
	void setBoxPosX(int curBoxPosX);
	int getBoxPosX(); 
	int getBoxWidth();
	void setBoxWidth(int newBoxWidth);
	Box();
	int getBoxHeight();
	void setBoxHeight(int newBoxHeight); 
	bool getBoxHit();
	void setBoxHit(bool status);
	int getBoxVelX();
	void setBoxVelX();
	int getBoxVelY();
	int getBoxMovementX();
	int getBoxMovementY();
	void setBoxVelY();
private:
	int boxMovementY;
	int boxMovementX;
	int boxPosX;
	int boxPosY;
	int boxWidth;
	int boxHeight;
	bool hit;
	int boxXVel;
	int boxYVel;
};