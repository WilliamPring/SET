#include <string>
#include <sstream>
#include <fstream> 


#include <algorithm>
#include <string>
#include <stdio.h>
using namespace std;
bool checkInputIfNum(string num);
bool checkForDashW(string CheckW);
bool readingFileFunction(string ReadFile, string num, bool statusOfW);
#include <iostream>
#include <fstream> 

int main(int argc, char*argv[])
{
	string readFile = "";
	bool statusOfNumConversion = 0;
	bool checkReturn = true;
	int i = 1;
	if (argc < 1)
	{
		printf("Error\n");
	}
	else
	{
		do
		{

			string input = argv[1];
			if (input == "-w")
			{
				checkReturn = readingFileFunction(argv[i + 2], argv[2], false);
			}
			else if (argc == 1)
			{
				checkReturn = regularWraping(argv[1]); 
			}
			else
			{
				checkReturn = checkForDashW(argv[i]);
				if (checkReturn == true)
				{
					checkReturn = readingFileFunction(argv[1 + i], argv[1], true);				
				}
				else
				{
					break;
				}
			}
			input = "";
			i++;
		} while (i < argc);
	}
}


bool readingFileFunction(string ReadFile, string num, bool statusOfW)
{
	bool resultOfInterger = false;
	int lineCount = 0;
	char character = 0;
	string finalMessage = "";
	int number = 0;
	string praseLine = "";
	if (statusOfW == true)
	{
		num.erase(1, 1);
		num.erase(0, 1);
	}
	resultOfInterger = checkInputIfNum(num);
	if (resultOfInterger == true)
	{
		number = atoi(num.c_str());
		ifstream ifs(ReadFile, ifstream::in);
		if (ifs.good())
		{
			while (ifs.good())
			{
				character = ifs.get();
				if (lineCount == number)
				{
					printf("%s\n", praseLine.c_str());
					praseLine = "";
					lineCount = 0;
					praseLine += character;
				}
				else
				{
					praseLine += character;
				}
				lineCount++;
			}
		}
	}
	return resultOfInterger;
}




bool checkForDashW(string CheckW)
{
	bool status = false;
	if ((CheckW.at(0) == '-') && (CheckW.at(1) == 'w'))
	{
		CheckW.erase(1, 1);
		CheckW.erase(0, 1);
		status = checkInputIfNum(CheckW);
	}
	return status;
}


bool checkInputIfNum(string num)
{
	bool status = true;
	char buffer[121] = "";
	int number = 0;
	int length = 0;
	if (num.length() < 121)
	{
		number = atoi(num.c_str());
		sprintf(buffer, "%d", number);
		if (strcmp(buffer, num.c_str())==0)
		{
			status = true;
		}
	}
	else
	{
		printf("ERROR!\n");
		status = false;
	}
	return status;
}



