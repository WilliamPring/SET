
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
// CChildView

int timer;
Bitmap* bmpBackground;
Bitmap* bmpForeground;
Bitmap* bmpMidground;
Image* slingshot1;
Image* reptile;
Image* slingshot2;
Bird bird;
float rotation = 0.0;
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
	int xComp = bird.getXBirdPos();
	int yComp = bird.getBirdHitBoxY();
	if (bird.getBirdFalling() == false)
	{
		if ((x > xComp) && (x < bird.getBirdHitBoxX()) && (y * 1.21 >= yComp) && (y * 1.09 < bird.getBirdHitBoxY()))
		{
			PlaySound(L"res//SPLAT_Sound_Effects.wav", 0, SND_ASYNC);
			bird.setBirdFalling(true);
		}
		else
		{
			PlaySound(L"res//slingshotsound.wav", NULL, SND_ASYNC);
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
		bird.BirdFallingToDeath();
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
		SetTimer(1, 100, NULL);
	}

	CPaintDC dc(this); // device context for painting
	//Rectangle
	RECT screenRectSize;
	//get dimension for the rectangle
	GetWindowRect(&screenRectSize);
	CMemDC mDC((CDC&)dc, this);
	//get size for the background
	int xWidth = screenRectSize.right - screenRectSize.left;
	//get the size for the height
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	Graphics drawGraphics(mDC.GetDC());
	ImageAttributes ImgAttr;


	Bitmap* displayNewBackground = (Bitmap*)bmpBackground->GetThumbnailImage(xWidth, yHeight);
	Bitmap* displayNewForeground = (Bitmap*)bmpForeground->GetThumbnailImage(xWidth, yHeight);
	Bitmap* displayNewMidground = (Bitmap*)bmpMidground->GetThumbnailImage(xWidth, yHeight);
	Color pixleBackground;
	Color pixleForeground;
	Color pixleMidground;
	displayNewBackground->GetPixel(0, 0, &pixleBackground);
	displayNewForeground->GetPixel(0, 0, &pixleForeground);
	displayNewMidground->GetPixel(0, 0, &pixleMidground);
	ImageAttributes imgAttMiddleground;
	imgAttMiddleground.SetColorKey(pixleMidground, pixleMidground, ColorAdjustTypeBitmap);

	ImageAttributes imgAttForeground;
	imgAttForeground.SetColorKey(pixleForeground, pixleForeground, ColorAdjustTypeBitmap);
	//drawing the background
	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(displayNewMidground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttForeground);
	drawGraphics.DrawImage(displayNewForeground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttMiddleground);
	
	drawGraphics.DrawImage(slingshot1, 10, yHeight - (yHeight *.2), (int)(xWidth*0.06), (int)(yHeight*0.1));
	if (bird.getBirdFalling() == false)
	{
		drawGraphics.DrawImage(reptile, bird.getXBirdPos(), bird.getYBirdPos(), (int)(xWidth*0.06), (int)(yHeight*0.06));
	}
	else
	{

		//Matrix matrix;
		//matrix.Translate(bird.getXBirdPos(), bird.getYBirdPos());
		//matrix.RotateAt(30.0f, PointF(150.0f, 100.0f), MatrixOrderAppend);

		//drawGraphics.SetTransform(&matrix);

		//drawGraphics.RotateTransform(rotation += 5.0f);
		drawGraphics.DrawImage(reptile, bird.getXBirdPos(), bird.getYBirdPos(), (int)(xWidth*0.06), (int)(yHeight*0.06));
	}
	
	drawGraphics.DrawImage(slingshot2, 10, (yHeight - yHeight * .2), (int)(xWidth*0.06), (int)(yHeight*0.1));

	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
}


