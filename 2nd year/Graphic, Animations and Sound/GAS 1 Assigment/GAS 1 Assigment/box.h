#pragma once
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


private:
	int boxPosX;
	int boxPosY;
	int boxWidth;
	int boxHeight;
};