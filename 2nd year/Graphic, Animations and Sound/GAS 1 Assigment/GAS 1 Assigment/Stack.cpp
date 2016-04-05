/*
* FILE : Stack.cpp
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* has 6 box and its dimension
*/

#include "stdafx.h"
#include "Stack.h"

Stack::Stack(int width, int height)
{
	for (int i = 0; i < 5; i++)
	{
		Box box = Box();
		box.setBoxPosX(width * .9);
		if (listofBox.size() ==0)
		{
			box.setBoxPosY(height * .9);
		}
		else
		{
			int tmp = listofBox.at(i-1).getBoxPosY() - 50;
			box.setBoxPosY(tmp);
		}
		listofBox.push_back(box); 
	}

}

void Stack::Resize(int width, int height)
{

	listofBox.clear(); 
	for (int i = 0; i < 5; i++)
	{
		Box box = Box();
		box.setBoxPosX(width * .9);
		if (listofBox.size() == 0)
		{
			box.setBoxPosY(height * .9);
		}
		else
		{
			box.setBoxHeight(height*0.072);
			box.setBoxWidth(width*0.05);
			int tmp = listofBox.at(i - 1).getBoxPosY() - box.getBoxHeight();
			box.setBoxPosY(tmp);
		}
		listofBox.push_back(box);
	}
}

std::vector<Box>* Stack::getListOfBox()
{
	return &listofBox;
}


Stack::Stack()
{

}