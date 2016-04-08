/*
* FILE : bird.h
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* Header file for the bird class which represent a reptile
*/

#pragma once
#define HitBoxDimension 44


/*
CLASS		: Bird
DESCRIPTION	:
Class represent the bird that can move, resize, die, reset post
*/

class Bird 
{
public:
	Bird(); 
	void setScreenHeight(int screenHeight);
	void SetUpReferencePoints(int screenHeight, int screenWidth);
	void SetUpBirdMovement();
	int getScreenHeight();
	int getScreenWidth();
	int getBirdHitBoxY();
	void setScreenWidth(int screenWidth);
	int getBirdHitBoxX();
	int getYBirdPos();
	int getXBirdPos();
	void setXBirdPos(int curPost);
	bool leftOrRight;
	bool getBirdFalling(); 
	void setBirdFalling(bool statusOfFall);
	void MoveBird();
	void birdDieingToDeath();
	int getPointOfNoReturn();
	int getBirdFlyPos();
	void changeFlyPos(); 
	void setPointOfNoReturn(int pointOfDeath);
private:
	int birdHitBoxX;
	int screenHeight;
	int screenWidth;
	int disappearBird;
	int birdVelocity;
	bool birdFallingMode;
	int xBirdPos;
	int birdFlyPos;
	int yBirdPos;
	int orgion;
	int pointOfNoReturn;
	int birdHitBoxY;
	bool toggleLeftOrRight;
};