/*
* FILE : Stack.h
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* Stack header file which contains a list of boxes
*/



#pragma once
#include "box.h"
#include <vector>
/*
CLASS		: Stack
DESCRIPTION	:
Class to represent an list of box that has resize and a list of a 6 box
*/
class Stack
{
public:
	Stack(int width, int height);
	Stack();
	std::vector<Box>* Stack::getListOfBox();
	void Resize(int width, int height);
private:
	std::vector<Box> listofBox;
};