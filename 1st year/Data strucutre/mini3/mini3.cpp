/*
Project: DS Mini-Assignment #3
File: mini3.cpp
Programer: William Pring
Date: March-19-2015
description: User enter words and place them in the vector and user is allow to serach after user clicks a "."
*/


#include <vector>
#include <iostream>
#include <string>
#include <algorithm> 
using namespace std;


int main(void)
{
	//set <string> words;
	vector <string> words;
	int track = 0;
	bool bExit = false;
	string userInput = "";
	//loop until bExit dose not equal false
	while (bExit == false)
	{
		printf("enter word\n");
		//get user input
		getline(cin, userInput);
		//end loop when user click '.'
		if (userInput == ".")
		{
			bExit = true;
			continue;
		}
		//check for space
		if (userInput.find(' ') != userInput.npos)
		{
			printf("found space\n");
			track = 1;
			break;
		}
		if (track == 1)
		{
			continue;
		}
		words.push_back(userInput);
	}
	//sort all of them
	sort(words.begin(), words.end());

	bExit = false;
	//loop until bExit!=false
	while (bExit == false)
	{
		printf("enter word to search\n");
		getline(cin, userInput);
		//exit the loop until user click .
		if (userInput == ".")
		{
			bExit = true;
			continue;
		}
		//serach for users inputs
		if (binary_search(words.begin(), words.end(), userInput))
		{
			printf("Success!\n");
		}
		else {
			printf("Not there!\n");
		}
	}
	return 0;
}
