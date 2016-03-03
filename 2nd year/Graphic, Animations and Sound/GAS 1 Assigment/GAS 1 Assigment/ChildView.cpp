
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

ULONG_PTR gdiplusToken;
int timer;
Bitmap* bmpBackground;
Bitmap* bmpForeground;
Bitmap* bmpMidground;
Bitmap* displayNewMidground;
Bitmap* displayNewForeground;
Bitmap* displayNewBackground; 
Image* slingshot1;
Image* reptile;
Image* slingshot2;
Bird bird;
RECT screenRectSize;
float rotation = 0.0f;

CChildView::CChildView()
{
	GdiplusStartupInput gdiplusStartupInput;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
	bmpBackground = (Bitmap*)Image::FromFile(L"res//Background.bmp");
	bmpForeground = (Bitmap*)Image::FromFile(L"res//Foreground.bmp");
	bmpMidground = (Bitmap*)Image::FromFile(L"res//Midground.bmp");
	slingshot1 = Gdiplus::Image::FromFile(L"res//slingshot1.png");
	reptile = Gdiplus::Image::FromFile(L"res//reptile.png");
	slingshot2 = Gdiplus::Image::FromFile(L"res//slingshot2.png");
}

CChildView::~CChildView()
{
	delete bmpBackground;
	delete bmpForeground;
	delete bmpMidground;
	delete slingshot1;
	delete reptile;
	delete slingshot2;
	GdiplusShutdown(gdiplusToken); 
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	//left button click	
	ON_WM_LBUTTONDOWN()
	ON_WM_PAINT()
	//timer
	ON_WM_TIMER()
	//doublebuffering
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

//on left button down
void CChildView::OnLButtonDown(UINT nFlags, CPoint point)
{
	int x = point.x;
	int y = point.y; 
	int xCompPlus = bird.getXBirdPos() + ((bird.getScreenWidth()* 0.04));
	int yCompPlus = bird.getYBirdPos() + ((bird.getScreenWidth()* 0.03));
	if (bird.getBirdFalling() == false)
	{
		PlaySound(L"res//slingshotsound.wav", NULL, SND_FILENAME| SND_ASYNC);
		if (((x >= bird.getXBirdPos()) && (x <= xCompPlus)) && (y <= yCompPlus)&& (y >= bird.getYBirdPos()))
		{
			PlaySound(L"res//SPLAT_Sound_Effects.wav", NULL, SND_FILENAME | SND_ASYNC);
			bird.setBirdFalling(true);
		}
	}
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
		}
	}
	this->Invalidate();
}




// CChildView message handlers

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



BOOL CChildView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;
}


void CChildView::OnPaint()
{
	if (timer == 0)
	{
		SetTimer(1, 120, NULL);
	}
	CPaintDC dc(this); // device context for painting
	CMemDC mDC((CDC&)dc, this);
	GetWindowRect(&screenRectSize);
	int xWidth = screenRectSize.right - screenRectSize.left;
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	bird.setScreenHeight(yHeight);
	bird.setScreenWidth(xWidth);
	Graphics drawGraphics(mDC.GetDC());
	Graphics drawBird(mDC.GetDC());
	ImageAttributes ImgAttr;
	//display it
	displayNewBackground = (Bitmap*)bmpBackground->GetThumbnailImage(xWidth, yHeight);
	displayNewForeground = (Bitmap*)bmpForeground->GetThumbnailImage(xWidth, yHeight);
	displayNewMidground = (Bitmap*)bmpMidground->GetThumbnailImage(xWidth, yHeight);


	ImageAttributes imgAttMiddleground;
	ImageAttributes imgAttrForeground;
	imgAttMiddleground.SetColorKey(Color(0, 200, 0), Color(255, 255, 255));
	imgAttrForeground.SetColorKey(Color(0, 120, 0), Color(255, 255, 255));
	
	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(displayNewMidground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttMiddleground);
	drawGraphics.DrawImage(displayNewForeground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttrForeground);
	drawGraphics.DrawImage(slingshot1, 10, yHeight - 140, (int)(xWidth*0.06), (int)(yHeight*0.1));
	drawBird.DrawImage(reptile, bird.getXBirdPos(), bird.getYBirdPos(), (int)(xWidth*0.06), (int)(yHeight*0.06));
	drawGraphics.DrawImage(slingshot2, 10, yHeight - 140, (int)(xWidth*0.06), (int)(yHeight*0.1));
	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
}


