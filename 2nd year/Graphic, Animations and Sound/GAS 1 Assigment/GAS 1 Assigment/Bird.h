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
<<<<<<< HEAD
=======
	void setXBirdPos(int curPost);
>>>>>>> 1ce91a4eff736dd9ab989346dc901bfdbf041fa2
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
<<<<<<< HEAD
	int disappearBird;
	int birdVelocity;
=======
>>>>>>> 1ce91a4eff736dd9ab989346dc901bfdbf041fa2
	bool birdFallingMode;
	int xBirdPos;
	int yBirdPos;
	int orgion;
	int highPotentialHeight;
	int lowPotentialHeight;
	int pointOfNoReturn;
	int birdHitBoxY;
	int birdVelocity;
	bool toggle;
};