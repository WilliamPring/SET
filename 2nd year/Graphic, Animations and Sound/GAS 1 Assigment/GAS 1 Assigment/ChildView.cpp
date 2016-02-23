
// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "GAS 1 Assigment.h"
#include "ChildView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

ULONG_PTR gdiplusToken;
// CChildView

int timer;
int x;
int y;
int orgion;
int maxValue;
int minValue;
int orginMiddle;
Bitmap* bmpBackground;
Bitmap* bmpForeground;
Bitmap* bmpMidground;
Image* slingshot1;
Image* reptile;
Image* slingshot2;

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
	orgion =0;
	maxValue=0;
	minValue=0;

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
	ON_WM_PAINT()
	ON_WM_TIMER()
	//doublebuffering
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

void CChildView::OnTimer(UINT_PTR nIDEvent)
{
	x += 25;
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
		SetTimer(1, 1000 / 70, NULL);
		//bitmap backgroud imaage
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
	//for the draw method
	//display it
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


	drawGraphics.DrawImage(slingshot1, 10, yHeight - 140, (int)(xWidth*0.06), (int)(yHeight*0.1));
	if (40 + x > xWidth)
	{
		x = 0;
	}
	drawGraphics.DrawImage(reptile, x, yHeight - 140, (int)(xWidth*0.06), (int)(yHeight*0.06));
	drawGraphics.DrawImage(slingshot2, 10, yHeight - 140, (int)(xWidth*0.06), (int)(yHeight*0.1));
	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
}


