
// Unhappy Flying ReptilesView.cpp : implementation of the CUnhappyFlyingReptilesView class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Unhappy Flying Reptiles.h"
#endif

#include "Unhappy Flying ReptilesDoc.h"
#include "Unhappy Flying ReptilesView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CUnhappyFlyingReptilesView

IMPLEMENT_DYNCREATE(CUnhappyFlyingReptilesView, CView)

BEGIN_MESSAGE_MAP(CUnhappyFlyingReptilesView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CUnhappyFlyingReptilesView::OnFilePrintPreview)
	ON_WM_CONTEXTMENU()
	ON_WM_RBUTTONUP()
END_MESSAGE_MAP()

// CUnhappyFlyingReptilesView construction/destruction

CUnhappyFlyingReptilesView::CUnhappyFlyingReptilesView()
{
	// TODO: add construction code here

}

CUnhappyFlyingReptilesView::~CUnhappyFlyingReptilesView()
{
}

BOOL CUnhappyFlyingReptilesView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CUnhappyFlyingReptilesView drawing

void CUnhappyFlyingReptilesView::OnDraw(CDC* /*pDC*/)
{
	CUnhappyFlyingReptilesDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here
}


// CUnhappyFlyingReptilesView printing


void CUnhappyFlyingReptilesView::OnFilePrintPreview()
{
#ifndef SHARED_HANDLERS
	AFXPrintPreview(this);
#endif
}

BOOL CUnhappyFlyingReptilesView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CUnhappyFlyingReptilesView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CUnhappyFlyingReptilesView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

void CUnhappyFlyingReptilesView::OnRButtonUp(UINT /* nFlags */, CPoint point)
{
	ClientToScreen(&point);
	OnContextMenu(this, point);
}

void CUnhappyFlyingReptilesView::OnContextMenu(CWnd* /* pWnd */, CPoint point)
{
#ifndef SHARED_HANDLERS
	theApp.GetContextMenuManager()->ShowPopupMenu(IDR_POPUP_EDIT, point.x, point.y, this, TRUE);
#endif
}


// CUnhappyFlyingReptilesView diagnostics

#ifdef _DEBUG
void CUnhappyFlyingReptilesView::AssertValid() const
{
	CView::AssertValid();
}

void CUnhappyFlyingReptilesView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CUnhappyFlyingReptilesDoc* CUnhappyFlyingReptilesView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CUnhappyFlyingReptilesDoc)));
	return (CUnhappyFlyingReptilesDoc*)m_pDocument;
}
#endif //_DEBUG


// CUnhappyFlyingReptilesView message handlers
