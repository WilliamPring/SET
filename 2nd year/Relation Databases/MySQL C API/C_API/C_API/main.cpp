
/*
File name: main.cpp
Project: 
By: William Pring and Naween Mehanmal 
Date: October 23, 2015
Description: in class lab using c API sending to another computer
*/


#include <stdio.h>
#include <stdlib.h>
#include <my_global.h>
#include <mysql.h>
#include <string>
using namespace std;
#define MAXLOOP 10000


int main(int argc, char * argv[])
{
	MYSQL structDb;				// Allocate the space for the structure
	MYSQL *pdb = &structDb;		// MySQL structure pointer for convenient access to the variable
	string host = "";
	string user = "superuser";
	string password = "somepassword";
	string db = "Historian";
	unsigned int port = 3306;
	string strQuery = "";
	pdb = mysql_init(NULL);
	//check agruments to see if its valid basic validation
	if (argc > 3)
	{
		printf("Error too much arguments/n");
	}
	else if (argc == 1)
	{
		printf("Error no arguments/n");
	}
	else if (argc == 2)
	{
		printf("To little Arguments/n");
	}
	else
	{
		string prefix = argv[1];
		host = argv[2];
		//check connection
		if (mysql_real_connect(pdb, host.c_str(), user.c_str(), password.c_str(), db.c_str(), port, NULL, 0) == NULL)
		{
			printf("Error: %s\n", mysql_error(pdb));
		}
		else
		{
			//loop for 10000 times
			for (int i = 1; i <= MAXLOOP; i++)
			{
				
				strQuery = "INSERT INTO sample (SampleData) VALUES ('";
				//append to strQuery
				strQuery += prefix;
				strQuery += to_string(i);

				strQuery += "');";
				//query to the database
				mysql_query(pdb, strQuery.c_str());
				Sleep(500);
			}
		}
	}





}