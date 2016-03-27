
// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "GAS 1 Assigment.h"
#include "ChildView.h"
#include "Bird.h"
#include "mmsystem.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif
HCURSOR   slingShot;
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
Image* slingshot2;
Bird bird;
RECT screenRectSize;
float spin = 0.0;
bool explo;
CChildView::CChildView()
{
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
	deadBird = Gdiplus::Image::FromFile(L"res//dead.gif");
	myCur = LoadCursorFromFile(L"res//fus.cur");
	logoDontSueMe = Gdiplus::Image::FromFile(L"res//exp.png");

	int counter =0;
}

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

//on left button down
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
void CChildView::OnTimer(UINT_PTR nIDEvent)
{
	if (bird.getBirdFalling() == false)
	{
		bird.MoveBird();
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


	BOOL CChildView::OnSetCursor(CWnd* pWnd, UINT nHitTest, UINT message)
	{
		if (m_ChangeCursor)
		{
			SetCursor(myCur);
			return TRUE;
		}
		return CChildView::OnSetCursor(pWnd, nHitTest, message);
	}

BOOL CChildView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;
}


void CChildView::OnPaint()
{
	if (timer == 0)
	{
		SetTimer(1, 130, NULL);
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
		bird.SetUpReferencePoints(yHeight, xWidth);
		statusRefFirstTime = false;
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
	drawGraphics.DrawImage(displayNewForeground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttrForeground);
	

	if (bird.getBirdFalling()==true)
	{
		drawGraphics.TranslateTransform(bird.getXBirdPos(), bird.getYBirdPos(), MatrixOrderAppend);
		drawGraphics.RotateTransform(spin+=5);
		drawGraphics.DrawImage(deadBird, -15, -10, (int)(bird.getScreenWidth() * 0.08), (int)(bird.getScreenHeight() * 0.08));
		drawGraphics.ResetTransform();
	}
	else
	{
		bird.changeFlyPos();
		drawGraphics.DrawImage(gifOfPiggy[bird.getBirdFlyPos()], bird.getXBirdPos(), bird.getYBirdPos(), (int)(xWidth*0.08), (int)(yHeight*0.08));
	}
	if (explo == true)
	{
		drawGraphics.DrawImage(logoDontSueMe, 0, 0, (int)(bird.getScreenWidth()), (int)(bird.getScreenHeight()));
		explo = false;
	}

}


