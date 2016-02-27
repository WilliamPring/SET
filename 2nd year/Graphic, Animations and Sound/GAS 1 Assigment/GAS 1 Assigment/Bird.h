#pragma once

class Bird 
{
public:
	Bird(); 
	void setScreenHeight(int screenHeight);
	int SetUpReferencePoints(int screenHeight);
	void SetUpBirdMovement(); 
	int xBirdPos;
	int yBirdPos;

	void MoveBird(); 
	~Bird();
private:
	int screenHeight;
	int screenWidth;
	int birdVelocity;
	int orgion;
	int highPotentialHeight;
	int lowPotentialHeight;
	bool toggle;
	int getXBirdPos();
};