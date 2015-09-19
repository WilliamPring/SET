/*
* file name: AST3.c
* By: William Pring
* Date: Febuary 17, 2015
* Description: This program is dealing with video memory
*/




#include "Header.h"
	
int main ()
{
	int status = 0;
	int setNewRow = 23;
	int setNewCol = 32;
	int curRow = 0;
	int curCol = 0;
	char charVideo[MAX_ROWS][MAX_COLS] = { 0 };
	char shapeOfX[3][3] =
	{
		{ '*', ' ', '*' },
		{ ' ', '*', ' ' },
		{ '*', ' ', '*' }
	};



	clearScreen(charVideo);
	setCursorPosition(&curRow, &curCol, setNewRow, setNewCol);
	//test 1
	outputString("Chips", 0, 0, charVideo);
	VideoSim(charVideo);
	//test 2
	outputString("COOKIESeeeeeeeeeeeeeeeeeeeee", setNewRow, setNewCol, charVideo);
	VideoSim(charVideo);
	//test 3
	insertShape(shapeOfX, charVideo, 1, 4);
	VideoSim(charVideo);
	//test 4
	insertShape(shapeOfX, charVideo, 19 , 38);
	VideoSim(charVideo);
	//test 5
	insertShape(shapeOfX, charVideo, 10, 21);
	VideoSim(charVideo);
	//test 6
	outputString("Can we go to........................................................", 15, 12, charVideo);
	VideoSim(charVideo);
	//test 7
	scrollScreen(charVideo);
	VideoSim(charVideo);
	clearScreen(charVideo);
}


/*
* Function: insertShape
* Description: this will copy line by line and put it the vidoe array
* Parameters: char shapeOfX[3][3], char video[MAX_ROWS][MAX_COLS], int curRow, int curCol
* Returns: void
*/


void insertShape(char shapeOfX[3][3], char video[MAX_ROWS][MAX_COLS], int curRow, int curCol)
{
	int x = 0;
	int y = 0;
	int currentRow = curRow;
	int i = 0;
	char *pVideoDes = &shapeOfX[0][0];
	char *pVideoSrc = &video[0][0];
	//loop by 3
	while (x < 3)
	{
		//loop the for each of the rows
		while (y < 3)
		{
			*((*(video + (x + curRow)) + (y + curCol))) = *((*(shapeOfX + x) + y));
			//dose not allow it to go over the vidoe sizes
			if (curCol + y == SEMI_MAX_COL)
			{
				break;
			}
			y++;

		}
		//dose not allow it to go over the video sizes
		if (curRow + x == SEMI_MAX_ROW)
		{
			break;
		}
		y = 0;
		x++;
	}



}




/*
* Function: scrollScreen
* Description: move everything 1 element up
* Parameters: char shapeOfX[3][3], char video[MAX_ROWS][MAX_COLS], int curRow, int curCol
* Returns: void
*/


void scrollScreen(char video[MAX_ROWS][MAX_COLS])
{
	int area = 0;
	int i = 0;
	char *pVideoDes = &video[0][0];
	char *pVideoSrc = &video[1][0];
	//is is less then the area
	while (i < MAX_COLS *MAX_ROWS)
	{
		//920 represent the second last row base on the area
		if (i < SECOND_LAST_ELEMENT)
		{
			*pVideoDes = *pVideoSrc;
			//increment pVideoSrc
			pVideoSrc++;
			//increment pVideoDes
			pVideoDes++;
		}
		//put space on the last element
		else
		{
			//put space
			*pVideoDes = ' ';
			pVideoDes++;
		}
		i++;
	}

}



/*
* Function: outputString
* Description: Pass the video memory and if meet condition will scroll up
* Parameters: char *string, int currentRow, int currentColumn, char video[MAX_ROWS][MAX_COLS]
* Returns: void
*/



void outputString(char *string, int currentRow, int currentColumn, char video[MAX_ROWS][MAX_COLS])
{
	char* x = string;
	int vid_offset = (currentRow * MAX_COLS) + currentColumn;
	char *pVideo = &video[0][0];
	//loop until x is null ter
	while (*x !='\0')
	{
		pVideo[vid_offset] = *x;
		//increment the vid_offset
		vid_offset++;
		if (vid_offset>=(MAX_ROWS*MAX_COLS))
		{
			vid_offset = (MAX_ROWS - 1)*MAX_COLS;
			scrollScreen(video);
		}
		x++;
	}
}



/*
* Function: setCursorPosition
* Description: set the CusorPosition if it meet the range
* Parameters: int *theCurRow, int *theCurCol, int settingNewRow, int settingNewCol
* Returns: void
*/



void setCursorPosition(int *theCurRow, int *theCurCol, int settingNewRow, int settingNewCol)
{
	//check to see if the row is not less then 0 and if it greater then 24
	if ((!(settingNewRow<0)) && (!(settingNewRow >MAX_ROWS)))
	{
		*theCurCol = settingNewCol;
	}

	//check to see if the col is not les then 0 and if it greater then 40

	if ((!(settingNewCol<0)) && (!(settingNewCol >MAX_COLS)))
	{
		*theCurRow = settingNewRow;
	}

}


/*
* Function: clearScreen
* Description: loop until the area until the space
* Parameters: int *theCurRow, int *theCurCol, int settingNewRow, int settingNewCol
* Returns: void
*/



void clearScreen(char video[][MAX_COLS])
{
	int i = 0;
	char *pVideo = &video[0][0];
	//loop area
	for (i = 0; i < (MAX_ROWS * MAX_COLS); i++)
	{
		//put space
		*(pVideo++) = ' ';
	}
}


