
// GAS 1 Assigment.h : main header file for the GAS 1 Assigment application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CGAS1AssigmentApp:
// See GAS 1 Assigment.cpp for the implementation of this class
//

class CGAS1AssigmentApp : public CWinApp
{
public:
	CGAS1AssigmentApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CGAS1AssigmentApp theApp;
