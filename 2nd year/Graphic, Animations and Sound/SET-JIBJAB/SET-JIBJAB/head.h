/*
* FILE : head.h
* PROJECT : SET-JIBJAB
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION : The head class the represent the head object
*/



#pragma once
#include <time.h>      
#include <stdio.h>
#include <stdlib.h>     


/*
CLASS		: Head
DESCRIPTION	:
Class that represent the head class
*/

class Head
{
public:
	Head(int ScreenHeight, int ScreenWidth);
	void setScreenHeight(int newHeight);
	void setScreenWidth(int newWidth); 
	void initProject(int height, int width);
	Head();
	void move();
	int getX();
	int getY();
	void changeDirAndSpeed();
	void setResize(bool info);
	void setFaceHeight(int newFaceHeight);
	void setFaceWidth(int faceWidth);
	int getFaceHeight();
	int getFaceWidth();
private:
	int x;
	int y;
	int velX;
	int velY;
	int screenHeight;
	int screenWidth;
	bool resize;
	int faceWidth;
	int faceHeight;

};