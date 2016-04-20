
// SET-JIBJAB.h : main header file for the SET-JIBJAB application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CSETJIBJABApp:
// See SET-JIBJAB.cpp for the implementation of this class
//

class CSETJIBJABApp : public CWinApp
{
public:
	CSETJIBJABApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CSETJIBJABApp theApp;
