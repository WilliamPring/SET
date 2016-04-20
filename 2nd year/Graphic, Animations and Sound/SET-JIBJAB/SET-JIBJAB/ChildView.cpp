/*
* FILE : ChildView.cpp
* PROJECT : SET-JIBJAB
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION : The Child View class that will be for the view 
*/

#include "stdafx.h"
#include "SET-JIBJAB.h"
#include "ChildView.h"
#include <GdiPlusInit.h>
#include "mmsystem.h"
#include "head.h"
#include <ctime> 
#include <vector>
#include "avi_utils.h"
using namespace Gdiplus;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#define two 2
ULONG_PTR gdiplusToken;
Bitmap* bmpBackground;
Bitmap* head;
Bitmap* head1;
Bitmap* orgHead;
Bitmap* orgHead1;
Bitmap* displayNewBackground;
RECT screenRectSize;
int timer;
int x;
int y;
bool isVideoDone;
int randLeftOrRight;
bool leftRightBool;
bool init;
int counter;
Head myHead;
Head myHead1;
int rotation;
bool status;
int  angle;
int sizeX;
int sizeY;
HAVI avi;

BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_WM_TIMER()
	ON_WM_SIZE()
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()
/*
* NAME : OnTimer
* PURPOSE : Timer class event that will execute base on your SetTimer
*/
void CChildView::OnTimer(UINT_PTR nIDEvent)
{
	int resizeInfo = 0;
	//moving left or 
	counter++;
	if (counter % (30 * 5) == 0)
	{
		myHead.changeDirAndSpeed();

	}
	if (counter % (30 * 3) == 0)
	{
		randLeftOrRight = (rand() % (3 - 1)) + 1;
	}


		bool status = CheckCollision();
		if (status == true)
		{
			myHead.changeDirAndSpeed();
		}

		if (counter > (500)) {
			AddingMusicFinish();
			KillTimer(1);
		}
		else {
			myHead.move();
			myHead1.move();
			this->Invalidate();
		}


}

/*
* NAME : AddingMusicFinish
* PURPOSE : adding music the your AVI file and close the AVI
*/
void CChildView::AddingMusicFinish() {
	isVideoDone = true;
	AddAviWav(avi, "music.wav", SND_FILENAME);
	CloseAvi(avi);
}


/*
* NAME : AddingMusicFinish
* PURPOSE : Checking collison for the two faces
*/
bool CChildView::CheckCollision()
{
	bool retStatus = false;
	if (myHead.getX()  < (myHead1.getX() + sizeX) && (myHead.getX() + sizeX) > myHead1.getX())
	{
		if (myHead.getY() < (myHead1.getY() + sizeY) && (sizeY + myHead.getY()) > myHead1.getY())
		{
			myHead.changeDirAndSpeed();
			retStatus = true; //Collided with box
		}
	}

	return retStatus;
}


/*
* NAME : CChildView
* PURPOSE : Constructor for the ChildView
*/
CChildView::CChildView()
{
	srand(time(NULL));
	init = true;
	GdiplusStartupInput gdiplusStartupInput;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
	bmpBackground = (Bitmap*)Image::FromFile(L"res//background.bmp",0);
	orgHead = (Bitmap*)Image::FromFile(L"res//1.bmp", 0);	
	orgHead1 = (Bitmap*)Image::FromFile(L"res//1.bmp", 0);
	randLeftOrRight = 0;
	timer = 0;
	x = 0;
	angle = 0;
	leftRightBool = false;
	y = 0;
	status = NULL;
	counter = 0;
	avi = CreateAvi("test.avi", int(1000 / 30), NULL);
	isVideoDone = false;
}


/*
* NAME : CChildView
* PURPOSE : destructor for the ChildView
*/
CChildView::~CChildView()
{	
	delete orgHead;
	delete orgHead1;
	delete head;
	delete head1;
	delete bmpBackground;
	GdiplusShutdown(gdiplusToken);
}
/*
* NAME : OnEraseBkgnd
* PURPOSE : doublebuffering
*/
BOOL CChildView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;
}
/*
* NAME : OnSize
* PURPOSE : Anytime the screen changes size this function will be called
*/
void CChildView::OnSize(UINT nType, int x, int y)
{
	RECT screenSize;
	GetWindowRect(&screenSize);
	int xWidth = screenSize.right - screenSize.left;
	int yHeight = screenSize.bottom - screenSize.top;

	if (xWidth > 0 && yHeight > 0)
	{
		delete head;
		delete head1;
		sizeX = xWidth * 0.2;
		sizeY = yHeight * 0.2;
		head = (Bitmap*)orgHead->GetThumbnailImage(sizeX, sizeY);
		head1 = (Bitmap*)orgHead1->GetThumbnailImage(sizeX, sizeY);
	}

	if (init)
	{
		SetTimer(1, 1000 / 30, NULL);
		myHead.initProject(xWidth, yHeight);
		myHead1.initProject(xWidth, yHeight);
		init = false;
	}
	
	delete displayNewBackground;
	displayNewBackground = (Bitmap*)bmpBackground->GetThumbnailImage(xWidth, yHeight);
}

/*
* NAME : PreCreateWindow
* PURPOSE : Set the cursor and the screen 
*/
BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}





/*
* NAME : OnPaint
* PURPOSE : OnPaint is drawing on the screen
*/
void CChildView::OnPaint() 
{
	CPaintDC dc(this); 
	CMemDC mDC((CDC&)dc, this);
	GetWindowRect(&screenRectSize);
	//set the screen size
	int xWidth = screenRectSize.right - screenRectSize.left;
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	int faceX = 0;
	int faceY = 0;
	Bitmap destination(xWidth, yHeight);
	myHead.setScreenHeight(yHeight);
	myHead.setScreenWidth(xWidth);
	myHead1.setScreenHeight(yHeight);
	myHead1.setScreenWidth(xWidth);
	Gdiplus::Graphics drawGraphics(mDC.GetDC());

	Gdiplus::Graphics* imgGraphicsDrawingToVideo = Gdiplus::Graphics::FromImage(&destination);
	imgGraphicsDrawingToVideo->DrawImage(bmpBackground, 0, 0);

	faceX = xWidth * .2;
	faceY = yHeight * .2;
	ImageAttributes imgAttMiddleground;
	imgAttMiddleground.SetColorKey(Color(0, 175, 0), Color(251, 255, 251));
	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	//roating of the face
	if (randLeftOrRight == 1)
	{
	
		imgGraphicsDrawingToVideo->TranslateTransform(myHead.getX() + (sizeX / 2), myHead.getY() + (sizeY / 2), MatrixOrderAppend);
		imgGraphicsDrawingToVideo->RotateTransform(angle);
		imgGraphicsDrawingToVideo->DrawImage(head, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		imgGraphicsDrawingToVideo->ResetTransform();
		//second head
		imgGraphicsDrawingToVideo->TranslateTransform(myHead1.getX() + (sizeX / 2), myHead1.getY() + (sizeY / 2), MatrixOrderAppend);
		imgGraphicsDrawingToVideo->RotateTransform(angle);
		imgGraphicsDrawingToVideo->DrawImage(head1, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		imgGraphicsDrawingToVideo->ResetTransform();


		//first head
		drawGraphics.TranslateTransform(myHead.getX() + (sizeX / 2), myHead.getY() + (sizeY / 2), MatrixOrderAppend);
		drawGraphics.RotateTransform(angle);
		drawGraphics.DrawImage(head, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		drawGraphics.ResetTransform();
		//second head
		drawGraphics.TranslateTransform(myHead1.getX() + (sizeX / 2), myHead1.getY() + (sizeY / 2), MatrixOrderAppend);
		drawGraphics.RotateTransform(angle);
		drawGraphics.DrawImage(head1, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		drawGraphics.ResetTransform();
		if ((angle == -10) && (leftRightBool == true))
		{
			randLeftOrRight = 4;
			angle = 0;
			leftRightBool = false;
		}
		if (angle <= -360)
		{
			angle = 0;
			leftRightBool = true;
		}
		else
		{
			angle -= 10;

		}
	}
	//rotate left
	else if (randLeftOrRight == 2)
	{
		//first head
		//first head
		imgGraphicsDrawingToVideo->TranslateTransform(myHead.getX() + (sizeX / 2), myHead.getY() + (sizeY / 2), MatrixOrderAppend);
		imgGraphicsDrawingToVideo->RotateTransform(angle);
		imgGraphicsDrawingToVideo->DrawImage(head, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		imgGraphicsDrawingToVideo->ResetTransform();
		//second head
		imgGraphicsDrawingToVideo->TranslateTransform(myHead1.getX() + (sizeX / 2), myHead1.getY() + (sizeY / 2), MatrixOrderAppend);
		imgGraphicsDrawingToVideo->RotateTransform(angle);
		imgGraphicsDrawingToVideo->DrawImage(head1, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		imgGraphicsDrawingToVideo->ResetTransform();



		//first head
		drawGraphics.TranslateTransform(myHead.getX() + (sizeX / 2), myHead.getY() + (sizeY / 2), MatrixOrderAppend);
		drawGraphics.RotateTransform(angle);
		drawGraphics.DrawImage(head, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		drawGraphics.ResetTransform();
		//second head
		drawGraphics.TranslateTransform(myHead1.getX() + (sizeX / 2), myHead1.getY() + (sizeY / 2), MatrixOrderAppend);
		drawGraphics.RotateTransform(angle);
		drawGraphics.DrawImage(head1, Gdiplus::RectF(-1 * (sizeX / 2), -1 * (sizeY / 2), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		drawGraphics.ResetTransform();
		if (angle >= 360)
		{
			angle = 0;
			randLeftOrRight = 4;
		}
		else
		{
			angle += 10;
		}
	}
	else
	{

		
		imgGraphicsDrawingToVideo->DrawImage(head, Gdiplus::RectF(myHead.getX(), myHead.getY(), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		imgGraphicsDrawingToVideo->DrawImage(head1, Gdiplus::RectF(myHead1.getX(), myHead1.getY(), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		
		drawGraphics.DrawImage(head, Gdiplus::RectF(myHead.getX(), myHead.getY(), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
		drawGraphics.DrawImage(head1, Gdiplus::RectF(myHead1.getX(), myHead1.getY(), sizeX, sizeY), 0, 0, sizeX, sizeY, UnitPixel, &imgAttMiddleground);
	}


	if (!isVideoDone)
	{
		HBITMAP tempHBit;
		destination.GetHBITMAP(Color::Black, &tempHBit);
		AddAviFrame(avi, tempHBit);

		DeleteObject(tempHBit);
		DeleteObject(&destination);
	}
	DeleteObject(&drawGraphics);
	delete imgGraphicsDrawingToVideo;
	
}

