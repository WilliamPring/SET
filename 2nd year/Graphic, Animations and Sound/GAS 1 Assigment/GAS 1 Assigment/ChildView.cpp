
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


CChildView::CChildView()
{
	GdiplusStartupInput gdiplusStartupInput;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
}

CChildView::~CChildView()
{
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
		timer = SetTimer(1, 1000 / 60, NULL);
	}



	CPaintDC dc(this); // device context for painting
	//Rectangle
	RECT screenRectSize;
	//get teh dimension for the rectangle
	GetWindowRect(&screenRectSize);
	CMemDC mDC((CDC&)dc, this);
	//get size for the background
	int xWidth = screenRectSize.right - screenRectSize.left;
	//get the size for the height
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	Graphics drawGraphics(mDC.GetDC());
	//chroma
	ImageAttributes ImgAttr;
	//for the draw method
	//bitmap backgroud imaage
	Bitmap* bmpBackground = (Bitmap*)Image::FromFile(L"res//Background.bmp");
	Bitmap* bmpForeground = (Bitmap*)Image::FromFile(L"res//Foreground.bmp");
	Bitmap* bmpMidground = (Bitmap*)Image::FromFile(L"res//Midground.bmp");	
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

	Image imgFore(L"res//Middleground.bmp");
	ImageAttributes imgAttMiddleground;
	imgAttMiddleground.SetColorKey(pixleMidground, pixleMidground, ColorAdjustTypeBitmap);

	Image imgMiddle(L"res//Foreground.bmp");
	ImageAttributes imgAttForeground;
	imgAttForeground.SetColorKey(pixleForeground, pixleForeground, ColorAdjustTypeBitmap);
	//drawing the background
	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(displayNewMidground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttForeground);
	drawGraphics.DrawImage(displayNewForeground, RectF(0, 0, xWidth, yHeight), 0, 0, xWidth, yHeight, UnitPixel, &imgAttMiddleground);

	//draw the sling shot
	Gdiplus::Image* slingshot1 = Gdiplus::Image::FromFile(L"res//slingshot1.png");
	drawGraphics.DrawImage(slingshot1, 10, yHeight - 150, 100, 100);
	if (40 + x > xWidth)
	{
		x = 0;
	}
	Gdiplus::Image* reptile = Gdiplus::Image::FromFile(L"res//reptile.png");
	drawGraphics.DrawImage(reptile, x, (yHeight - 150), 60, 60);


	Gdiplus::Image* slingshot2 = Gdiplus::Image::FromFile(L"res//slingshot2.png");
	drawGraphics.DrawImage(slingshot2, 10, yHeight - 150, 100, 100);






	delete displayNewBackground;
	delete displayNewForeground;
	delete displayNewMidground;
	delete bmpBackground;
	delete bmpForeground;
	delete bmpMidground;
	delete reptile;
	delete slingshot1;
	delete slingshot2;


}

//{
//	RECT rcClient;
//	HWND hwnd;
//	//::GetClientRect(hwnd, &rcClient);
//	GetWindowRect(&rcClient);
//
//	int left = rcClient.left;
//	int top = rcClient.top;
//	int width = rcClient.right - rcClient.left;
//	int height = rcClient.top - rcClient.bottom;
//	//HDC hdcMem = ::CreateCompatibleDC(hdc);
//	const int nMemDC = ::SaveDC(hdcMem);
//	HBITMAP hBitmap = ::CreateCompatibleBitmap(hdc, width, height);
//	::SelectObject(hdcMem, hBitmap);
//	Graphics graphics(hdcMem);
//	SolidBrush back(Color(255, 255, 255));
//	graphics.FillRectangle(&back, left, top, width, height);
//
//	RECT rcClip;
//	::GetClipBox(hdc, &rcClip);
//	left = rcClip.left;
//	top = rcClip.top;
//	width = rcClip.right - rcClip.left;
//	height = rcClip.bottom - rcClip.top;
//	//::bitbit(hdc, left, top, width, height, hdcMem, left, top, SRCCOPY);
//	::RestoreDC(hdcMem, nMemDC);
//	::DeleteObject(hBitmap);



