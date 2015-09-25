/*
Filename cA5
Project: Assignment 5
Programer: William Pring
Date: November 26, 2014
Discription: The purpose of this program is to take a binary file copy then
paste to a file call "content.txt" and prints the ASII values into it.
*/




#include "cA5_proto.h"



int main(int argc, char *argv[])
{
	// variables
	unsigned char *memoryLocation = 0;
	unsigned char *incrementMemoryLocation = 0;
	const int maxFileSizes = 2147483647;
	int newLine = 9;
	int countingNumber = 1;
	int sizeOfFile = 0;
	const sizeOfChar = 1;
	FILE *ifp = NULL;
	FILE *ofp = NULL;
	int wrapFileLoop = 1;

	if (argc != 2)
	{
		printf("ERROR!");
	}
	else
	{
		// open the file
			ifp = fopen(argv[1], "rb");
			// cannot not open file if file open fails
			if (ifp == NULL)
			{
				printf("Cannot open file");
				wrapFileLoop = 0;
			}
			// if open file was sucessful 
			else
			{
				//this get the sizes of the file
				sizeOfFile = getFileLength(argv[1]);
				//this will check if sizeOfFile Retruns -1 and check to see if th esizeOfFile is greater or equal to 1
				if ((sizeOfFile != -1) && (sizeOfFile >= 1))
				{
					// if the file size is less then or equal to the max size it will create a dynamic menmory location
					if (sizeOfFile <= maxFileSizes)
					{
						// create a dyanmic menmory location
						memoryLocation = (char*)malloc(sizeOfFile);
						//by doing this will allow me to clear memoryLocation without an error
						incrementMemoryLocation = memoryLocation;
						// reading a file and storing it into the dyanmic menmory location
						fread(memoryLocation, sizeOfFile, 1, ifp);
						//check to see if the file close or not
						//open the output.txt in write mode
						ofp = fopen("contents.txt", "w");
						//if the file opening has an error a message will be display
						if (ofp == NULL)
						{
							printf("ERROR");
						}
						else
						{
							if (fprintf(ofp, "%03d ", *(incrementMemoryLocation++), 1) < 0)
							{
								printf("ERROR");
							}
							else
							{
								//this go though the whole file user inputed
								for (int i = 1; i <= sizeOfFile; i++)
								{
									//this will print the asii values in the binary file 
									fprintf(ofp, "%03d", *(incrementMemoryLocation++), 1);
									//increment number count so it will equal to newLine 
									countingNumber++;
									// if counting number equal to newLine then go to the new line
									if (countingNumber == newLine)
									{
										// this will allow it wrap to the next line 
										fprintf(ofp, " %03d\n", *(incrementMemoryLocation++), 1);
										//this will allow it to wrap every 10 characters 
										newLine = newLine + 9;
									}
									else
									{
										// this will print a space so can seperate the diffrent binary numbers
										fprintf(ofp, " ");
									}
								}
								//this will put a new line at the end of the file
								fprintf(ofp, "\n");
								if (((fclose(ifp)) && (fclose(ofp))) != EOF)
								{
									free(memoryLocation);
								}
								else
								{
									printf("file close ERROR ");
								}
							}
						}
					}
				}
			}
		}
		return 0;
	}


					