#pragma once
#define HitBoxDimension 44
class Bird 
{
public:
	Bird(); 
	void setScreenHeight(int screenHeight);
	int SetUpReferencePoints(int screenHeight);
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
	void BirdFallingToDeath();
	void setBirdFalling(bool statusOfFall);
	void MoveBird();
	void birdDieingToDeath();
	int getPointOfNoReturn();
	void setPointOfNoReturn(int pointOfDeath);
private:
	int birdHitBoxX;
	int screenHeight;
	int screenWidth;
	int disappearBird;
	int birdVelocity;
	bool birdFallingMode;
	int xBirdPos;
	int yBirdPos;
	int orgion;
	int highPotentialHeight;
	int lowPotentialHeight;
	int pointOfNoReturn;
	int birdHitBoxY;
	bool toggle;
};