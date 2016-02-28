#pragma once
#define HitBoxDimension 44
class Bird 
{
public:
	Bird(); 
	void setScreenHeight(int screenHeight);
	int SetUpReferencePoints(int screenHeight);
	void SetUpBirdMovement(); 
	int getBirdHitBoxY();
	int getBirdHitBoxX();
	int getYBirdPos();
	int getXBirdPos();
	void playFallingSound();
	bool getBirdFalling(); 
	void setBirdFalling(bool statusOfFall);
	void MoveBird();
	int birdHitBoxX;
	int birdHitBoxY;
	~Bird();
private:
	int screenHeight;
	int screenWidth;
	int birdVelocity;
	bool birdFallingMode;
	int xBirdPos;
	int yBirdPos;
	int orgion;
	int highPotentialHeight;
	int lowPotentialHeight;
	bool toggle;
};