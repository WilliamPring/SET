
#include <stdio.h>
#include <string.h>
#include <iostream>
#include <string>
#include <regex>
#pragma warning(disable:4996)

using namespace std;
int main()
{
	int year = 0;
	int month = 0;
	int day = 0;
	char userInput[1024] = "";
	string sendingString = "";
	string firstName = "";
	string lastName = "";
	bool answer = false;
	string dateOfBirth = "";
	regex integer("\b\w{1,5}\b");

	while (1)
	{

		getline(cin, firstName);



		answer = regex_match(firstName, integer);


	}
}