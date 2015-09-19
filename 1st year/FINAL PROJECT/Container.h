#ifndef __CONTAINER_H__
#define __CONTAINER_H__
#include <vector>
#include <string>
#include <sstream>
#include <fstream> 
#include <iostream>
#include <vector>
#include <random>
#include "Champions.h"
#include <stdio.h>
#include <conio.h>

#include <algorithm>
#define NUMBER 11


using namespace std; 

class Container
{


private:
	static string arrangeMember;
	vector<Champions> championContainer;
	static 	ifstream  dbaseFileInput;

public:
	string deleteAChampion();
	string deleteDatabase();
	string saveDataBase();
	//Functions required to insert, sort and display everything found in the vector container, which contains objects of the Champion class 	
	string createUserLOLChampion(string name, string MainRole, string SubRole, string AD, string AP, string DEF, string DIFF, string IP, string RP);
	string sortLOLChampion(string stats);
	string sortAmount(string memType); 
	string displayContainer();
	string convertInt(int number);
	//Functions required to handle all functionalities of the system database
	string displayDataBase();
	string deleteContainer();
	static bool OpenDBaseFile_ASCII_READ(string whichFile);


	static void CloseDBase_READ(void); 

	string readEntireFile(string fileName); 

	string assignChampionValues(int pipeNumber, string value, Champions& newChampion); 
	//Functions required to create random Champions using random generator functions
	string randomSubRoleCreater(void);
	string randomMainRoleCreater(void);
	string createChampions(string champNum);
	string randomNameCreator(void);
	int randomNumCreator(void);
	int randomIPCreator(void);
	int randomRPCreator(void); 

	//

	string GetArrangeMember(void); 


	//Create spaces for the data members

	string createNameSpaces(string& Name);
	string createSpaces(string& Type);




};



#endif