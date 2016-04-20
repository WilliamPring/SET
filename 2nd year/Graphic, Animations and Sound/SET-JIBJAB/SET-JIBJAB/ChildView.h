
#pragma once

/*
* FILE : ChildView.h
* PROJECT : SET-JIBJAB
* PROGRAMMER : William Pring
* FIRST VERSION : 3/28/2016
* DESCRIPTION : The Child View header class that will be for the view 
*/

/*
CLASS		: CChildView
DESCRIPTION	:
Class that represent the CChildView class
*/

class CChildView : public CWnd
{
public:
	CChildView();
	void AddingMusicFinish();
	bool CheckCollision();
public:

	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

public:
	virtual ~CChildView();

protected:
	afx_msg void OnPaint();
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg void OnSize(UINT nType, int x, int y);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	DECLARE_MESSAGE_MAP()
};


