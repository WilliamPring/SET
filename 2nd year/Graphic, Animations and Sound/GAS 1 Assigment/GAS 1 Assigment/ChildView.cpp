
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
END_MESSAGE_MAP()



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

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	//rectangle
	RECT screenRectSize;
	GetWindowRect(&screenRectSize);
	//get size
	int xWidth = screenRectSize.right - screenRectSize.left;
	int yHeight = screenRectSize.bottom - screenRectSize.top;
	ImageAttributes ImgAttr;
	//for the draw method
	Graphics drawGraphics(dc.m_hDC);
	//bitmap backgroud imaage
	Bitmap* bmpBackground = (Bitmap*)Image::FromFile(L"res//Background.bmp");
	Bitmap* bmpForeground = (Bitmap*)Image::FromFile(L"res//Foreground.bmp");
	Bitmap* bmpMidground = (Bitmap*)Image::FromFile(L"res//Midground.bmp");	
	//display it
	Bitmap* displayNewBackground = (Bitmap*)bmpBackground->GetThumbnailImage(xWidth, yHeight);
	Bitmap* displayNewForeground = (Bitmap*)bmpForeground->GetThumbnailImage(xWidth, yHeight);
	Bitmap* displayNewMidground = (Bitmap*)bmpMidground->GetThumbnailImage(xWidth, yHeight);
	ImgAttr.SetColorKey(Color(8), Color(15));
	Color backgroundColor;
	bmpBackground->GetPixel(0, 0, &backgroundColor);
	Image img(L"res//Midground.bmp");
	ImageAttributes imgAtt;





	drawGraphics.DrawImage(displayNewBackground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(bmpMidground, 0, 0, xWidth, yHeight);
	drawGraphics.DrawImage(bmpForeground, 0, 0, xWidth, yHeight);

}

