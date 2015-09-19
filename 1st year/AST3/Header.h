/*
* file name: Header.h
* By: William Pring
* Date: Febuary 17, 2015
* Description: This is the header file with the include, define and with the function prototype
*/



#include <stdio.h>
#define MAX_ROWS 24
#define MAX_COLS 40
#define SECOND_LAST_ELEMENT 920
#include <string.h>
#pragma warning(disable : 4996)
#define SEMI_MAX_ROW 23
#define SEMI_MAX_COL 39

void VideoSim(char video[MAX_ROWS][MAX_COLS]);
void clearScreen(char video[24][40]);
void setCursorPosition(int *theCurRow, int *theCurCol, int settingNewRow, int settingNewCol);
void outputString(char *string, int currentRow, int currentColumn, char video[MAX_ROWS][MAX_COLS]);
void scrollScreen(char video[MAX_ROWS][MAX_COLS]);
void insertShape(char shapeOfX[3][3], char video[MAX_ROWS][MAX_COLS], int curRow, int curCol);
