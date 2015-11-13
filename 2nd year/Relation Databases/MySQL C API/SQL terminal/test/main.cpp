// TestMySQL.cpp 
// Date: 11 Nov 2008
// Description: Sample program to demonstrate the basic functions of access to MySQL databases.
// Original author: Carlo Sgro


#include <stdio.h>
#include <stdlib.h>
#include <my_global.h>
#include <mysql.h>


void showResults(MYSQL *pdb);

int main(int argc, char* argv[])
{
	MYSQL structDb;				// Allocate the space for the structure
	MYSQL *pdb = &structDb;		// MySQL structure pointer for convenient access to the variable
	char *host = "localhost";	// ip address of host
	char *user = "superuser";		// user name
	char *password = "somepassword";	// password
	char *db = "Historian";		// database name
	unsigned int port = 3306;	// You may also use 0, which uses the default anyway (3306)

	static char strQuery[300] = ""; // query string
	host = argv[1];
	// Initialize MySQL structure
	pdb = mysql_init(NULL);

	// Connect to the database
	if (mysql_real_connect(pdb, host, user, password, db, port, NULL, 0) == NULL)
	{
		printf("Error: %s\n", mysql_error(pdb));
		return 1;
	}

	// Prompt for a query or 'q' to exit
	printf("---\nEnter new query (q to quit):\n");
	fgets(strQuery, sizeof(strQuery), stdin);

	while (strQuery[0] != 'q' && strQuery[0] != 'Q')
	{
		// submit Query
		if (mysql_query(pdb, strQuery) == 0)
		{
			printf("Query succeeded: %s\n", strQuery);
			showResults(pdb);
		}
		else
		{
			printf("Query failed: %s\n", mysql_error(pdb));
		}


		// Prompt again...
		printf("---\nEnter new query (q to quit):\n");
		fgets(strQuery, sizeof(strQuery), stdin);
	}
	mysql_close(pdb);
	printf("Program ended...\n");
	return 0;
}

// Function: showResults()
// Parameter: MYSQL *pdb: a pointer to an initialized and connected database 
// Return: Nothing
// Description: This function shows the result from a MySQL query. 
//
void showResults(MYSQL *pdb)
{
	unsigned int numFields = 0;					// number of fields in result
	my_ulonglong numRows = 0;					// number of rows returned
	unsigned int i = 0;							// counter
	MYSQL_RES *res = mysql_store_result(pdb);	// result of query


	if (res == NULL)							// Check for a result
	{
		printf("No result\n");
	}
	else
	{
		MYSQL_ROW row;
		// display the number of rows returned
		numRows = mysql_num_rows(res);
		printf("Number of rows returned: %d\n", numRows);

		// print the result of the query
		numFields = mysql_num_fields(res);
		while ((row = mysql_fetch_row(res)))
		{
			for (i = 0; i < numFields; i++)
			{
				if (row[i] != NULL)
				{
					if (i>0) printf(", ");
					printf("%s", row[i]);
				}
			}
			printf("\n");
		}

		// always free the results
		mysql_free_result(res);
	}
}