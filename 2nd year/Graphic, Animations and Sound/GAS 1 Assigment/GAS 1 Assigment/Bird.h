#pragma once
#define HitBoxDimension 44
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
	bool toggle;
};