/*
* FILE : ChildView.h
* PROJECT : Gas Assig 3
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION :
* Child View header file contains functions, variable
*/
#pragma once
#include <windows.h>
#include <gdiplus.h>

/*
CLASS		: CChildView
DESCRIPTION	:
Class represent the drawing of the screen, function of the screen, timer, doube buffering functionality of the program
*/
class CChildView : public CWnd
{
private:
	WCHAR wcScore[50] = { 0 };
	float spin = 0.0;
	bool explo;
	//setting up the font
	int points;
	bool printToScreen;
	bool statusRefFirstTime;
	Color colorTextPoint;
	StringFormat format;
	RectF layoutRect;
public:
	CChildView();

public:
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
// Implementation
public:
	virtual ~CChildView();
private:
	bool m_ChangeCursor;
	// Generated message map functions
protected:
	afx_msg void OnPaint();
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnSize(UINT nType, int x, int y);
	afx_msg BOOL OnSetCursor(CWnd* pWnd, UINT nHitTest, UINT message);

	DECLARE_MESSAGE_MAP()
};

