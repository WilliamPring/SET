/*
* FILE : ChildView.cpp
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* contains the view of the project and events
*/


#include "stdafx.h"
#include "GAS 1 Assigment.h"
#include "ChildView.h"
#include "Bird.h"
#include "mmsystem.h"
#include "Stack.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

ULONG_PTR gdiplusToken;
int timer;
Bitmap* bmpBackground;
Bitmap* bmpForeground;
Bitmap* bmpMidground;
Bitmap* displayNewMidground;
Bitmap* displayNewForeground;
Bitmap* displayNewBackground;
Image* deadBird;
bool statusRefFirstTime;
Image* gifOfPiggy[4];
Image* logoDontSueMe;
HCURSOR myCur;
Bird bird;
StringFormat format;
RECT screenRectSize;
Stack stack;
Bitmap* onScreenBox;
/*
* NAME : CChildView
* PURPOSE : constructor the inits all of the varible
*/
CChildView::CChildView()
{
	layoutRect = RectF(0.0f, 0.0f, 200.0f, 50.0f);
	colorTextPoint = Color(255, 255, 0, 0);
	wcscat(wcScore, L"Score: 0");
	explo = false;
	statusRefFirstTime = true;
	GdiplusStartupInput gdiplusStartupInput;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
	bmpBackground = (Bitmap*)Image::FromFile(L"res//Background.bmp");
	bmpForeground = (Bitmap*)Image::FromFile(L"res//Foreground.bmp");
	bmpMidground = (Bitmap*)Image::FromFile(L"res//Midground.bmp");
	gifOfPiggy[0] = Gdiplus::Image::FromFile(L"res//p0.gif");
	gifOfPiggy[1] = Gdiplus::Image::FromFile(L"res//p1.gif");
	gifOfPiggy[2] = Gdiplus::Image::FromFile(L"res//p2.gif");
	gifOfPiggy[3] = Gdiplus::Image::FromFile(L"res//p3.gif");
	onScreenBox = (Bitmap*)Image::FromFile(L"res//Box.png");
	deadBird = Gdiplus::Image::FromFile(L"res//dead.gif");
	myCur = LoadCursorFromFile(L"res//fus.cur");
	logoDontSueMe = Gdiplus::Image::FromFile(L"res//exp.png");
	//style of text
	format.SetAlignment(StringAlignmentCenter);
	//color white for text
	printToScreen = false;
	points = 0;
}
/*
* NAME : CChildView
* PURPOSE : Destructor delete all of the resources
*/
CChildView::~CChildView()
{
	delete bmpBackground;
	delete bmpForeground;
	delete bmpMidground;
	delete gifOfPiggy[0];
	delete gifOfPiggy[1];
	delete gifOfPiggy[2];
	delete gifOfPiggy[3];
	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
	GdiplusShutdown(gdiplusToken); 
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_SIZE()
	//left button click	
	ON_WM_LBUTTONDOWN()
	ON_WM_PAINT()
	//timer
	ON_WM_TIMER()
	ON_WM_SETCURSOR()
	//doublebuffering
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

/*
* NAME : OnLButtonDown
* PURPOSE : Left button click
*/
void CChildView::OnLButtonDown(UINT nFlags, CPoint point)
{
	int x = point.x;
	int y = point.y; 
	int xCompPlus = bird.getXBirdPos() + ((bird.getScreenWidth()* 0.08));
	int yCompPlus = bird.getYBirdPos() + ((bird.getScreenWidth()* 0.08));
	if (bird.getBirdFalling() == false)
	{
		if (((x >= bird.getXBirdPos()) && (x <= xCompPlus)) && (y <= yCompPlus)&& (y >= bird.getYBirdPos()))
		{
			points += 100;
			printToScreen = true;
			PlaySound(L"res//SPLAT_Sound_Effects.wav", NULL, SND_ASYNC);
			bird.setBirdFalling(true);
		}
		else
		{
			PlaySound(L"res//slingshotsound.wav", NULL, SND_FILENAME | SND_ASYNC);
		}
	}
	explo = true;
}
/*
* NAME : OnSize
* PURPOSE : Resizing the screen
*/
void CChildView::OnSize(UINT nType, int x, int y)
{
	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
	RECT screenSize;
	GetWindowRect(&screenSize);
	bird.setScreenHeight(screenSize.bottom - screenSize.top);
	bird.setScreenWidth(screenSize.right - screenSize.left);
	//setting  up point for the first time
	displayNewBackground = (Bitmap*)bmpBackground->GetThumbnailImage(bird.getScreenWidth(), bird.getScreenHeight());
	displayNewForeground = (Bitmap*)bmpForeground->GetThumbnailImage(bird.getScreenWidth(), bird.getScreenHeight());
	displayNewMidground = (Bitmap*)bmpMidground->GetThumbnailImage(bird.getScreenWidth(), bird.getScreenHeight());
}

/*
* NAME : OnTimer
* PURPOSE : Timer class event
*/
void CChildView::OnTimer(UINT_PTR nIDEvent)
{
	if (bird.getBirdFalling() == false)
	{
		bool retStatus = true;
		std::vector<Box> myBox = stack.getListOfBox();
		for (int i = 0; i < myBox.size(); i++)
		{
			if ((bird.getXBirdPos() < (myBox[i].getBoxPosX() + myBox[i].getBoxWidth()) && (bird.getXBirdPos() + (bird.getScreenWidth()*0.06)) > myBox[i].getBoxPosX()))
			{
				if (bird.getYBirdPos() < (myBox[i].getBoxPosY() + myBox[i].getBoxHeight()) && (bird.getYBirdPos() + bird.getScreenHeight()*0.06) > myBox[i].getBoxPosY())
				{
					myBox[i].setBoxHit(true);
				}
			}
		}
		if (retStatus)
		{
			bird.MoveBird();
		}
	}
	else
	{
		bird.birdDieingToDeath();
		PlaySound(L"res//CartoonFalling2.wav", 0, SND_NOSTOP|SND_ASYNC| SND_LOOP);
		if (bird.getYBirdPos() > bird.getPointOfNoReturn())
		{
			bird.setBirdFalling(false); 
			int xWidth = screenRectSize.right - screenRectSize.left;
			bird.setXBirdPos(xWidth + 1);
			PlaySound(NULL, 0, SND_ASYNC);
			Sleep(1000);
		}
	}
	this->Invalidate();
}







/*
* NAME : PreCreateWindow
* PURPOSE : pre creating the windows before everything gets created
* create cursor as well
*/
BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle = WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);
	return TRUE;
}

/*
* NAME : OnSetCursor
* PURPOSE : Set the cursor to an cur file
*/
	BOOL CChildView::OnSetCursor(CWnd* pWnd, UINT nHitTest, UINT message)
	{
		if (m_ChangeCursor)
		{
			SetCursor(myCur);
			return TRUE;
		}
		return CChildView::OnSetCursor(pWnd, nHitTest, message);
	}


/*
* NAME : OnEraseBkgnd
* PURPOSE : Double buffering and return true
*/
BOOL CChildView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;
}
/*
* NAME : OnPaint
* PURPOSE : Drawing to the screen to represent the bird moving and hiting boxes
*/

void CChildView::OnPaint()
{
	if (timer == 0)
	{
		SetTimer(1, 220, NULL);
	}

	CPaintDC dc(this); // device context for painting
	CMemDC mDC((CDC&)dc, this);
	GetWindowRect(&screenRectSize);
	//set the screen size
	int xWidth = screenRectSize.right - screenRectSize.left;
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	bird.setScreenHeight(yHeight);
	bird.setScreenWidth(xWidth);

	if (statusRefFirstTime)
	{
		stack = Stack(xWidth, yHeight);
		bird.SetUpReferencePoints(yHeight, xWidth);
	}

	Graphics drawGraphics(mDC.GetDC());
	ImageAttributes imgAttMiddleground;
	ImageAttributes imgAttrForeground;

	//key to remove the green line
	imgAttMiddleground.SetColorKey(Color(0, 200, 0), Color(255, 255, 255));
	imgAttrForeground.SetColorKey(Color(0, 107, 0), Color(255, 255, 255));
	//draw background
	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(displayNewMidground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttMiddleground);


	if (bird.getBirdFalling()==true)
	{
		drawGraphics.TranslateTransform(bird.getXBirdPos(), bird.getYBirdPos(), MatrixOrderAppend);
		if (spin >= 360)
		{
			spin = 0.0;
		}
		drawGraphics.RotateTransform(spin+=5);
		drawGraphics.DrawImage(deadBird, -15, -10, (int)(bird.getScreenWidth() * 0.06), (int)(bird.getScreenHeight() * 0.06));
		drawGraphics.ResetTransform();
	}
	else
	{
		spin = 0.0;
		bird.changeFlyPos();
		drawGraphics.DrawImage(gifOfPiggy[bird.getBirdFlyPos()], bird.getXBirdPos(), bird.getYBirdPos(), (int)(xWidth*0.06), (int)(yHeight*0.06));
	}
	drawGraphics.DrawImage(displayNewForeground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttrForeground);
	

	if (explo == true)
	{
		drawGraphics.DrawImage(logoDontSueMe, 0, 0, (int)(bird.getScreenWidth()), (int)(bird.getScreenHeight()));
		explo = false;
	}
	if (printToScreen == true)
	{
		swprintf(wcScore, L"Score: %d", points);
		printToScreen = false;
	}

	Font myFont(L"Arial", 16);
	SolidBrush color(colorTextPoint);
	drawGraphics.DrawString(
		wcScore,
		-1,
		&myFont,
		layoutRect,
		&format,
		&color);

	if (statusRefFirstTime)
	{
		for (int i = 0; i < 5; i++)
		{
			std::vector<Box> myBox = stack.getListOfBox();
			drawGraphics.DrawImage(onScreenBox, myBox.at(i).getBoxPosX(), myBox.at(i).getBoxPosY(), (int)(xWidth*0.05), (int)(yHeight*0.09));
		}
		statusRefFirstTime = false;
	}
	else
	{
		stack.Resize(xWidth, yHeight);
		std::vector<Box> myBox = stack.getListOfBox();
		for (int i = 0; i < 5; i++)
		{
			drawGraphics.DrawImage(onScreenBox, myBox.at(i).getBoxPosX(), myBox.at(i).getBoxPosY(), (int)(xWidth*0.05), (int)(yHeight*0.09));
		}
	}
}


