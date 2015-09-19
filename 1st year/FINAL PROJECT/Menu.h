/*
Filename: Menu.h
Project: Assignment 3
By: Naween Mehanmal
Date: Jan 25, 2015
Description: 
*/

#ifndef __MENU_H__
#define __MENU_H__
#include <stdio.h>
#include <conio.h>
#include <sstream>
#include <iostream>
#include "Container.h"
#include <regex>

#pragma warning(disable: 4996)

using namespace std;
class Menu
{

private:
	int userInput;

public:
	Menu(void);
	string storeContainer(Container &Champions);
	void Menu::RunProgram(Container Champions);
	string deleteContainerOrDatabase(string whichOneToDelete, Container &lolChampion);
	string choosingSort(string userInput, Container &lolChampion, string menuForTheSort);
	bool CheckNumber(string &userInput, char* message);
	bool CheckLetter(string &userInput, char * message);
	bool GetUserInput(string &Name, string &MainRole, string &SubRole, string &AD, string &AP, string &DEF, string &DIFF, string &IP, string &RP);
	bool GetNumberInput(int number);


};

#endif