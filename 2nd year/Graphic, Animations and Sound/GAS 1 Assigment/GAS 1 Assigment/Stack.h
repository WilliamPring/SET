/*
* FILE : Stack.h
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* Stack h
*/



#pragma once
#include "box.h"
#include <vector>
class Stack
{
public:
	Stack(int width, int height);
	Stack();
	std::vector<Box> getListOfBox();
	void Resize(int width, int height);
private:
	std::vector<Box> listofBox;
};