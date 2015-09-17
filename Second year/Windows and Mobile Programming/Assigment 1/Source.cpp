/*
* file Name: Source.cpp
* By: William Pring
* Date: Sept 9, 2015
* Description: This project purpose will to introduce us to using win32 api's. Allowing us to use functions such as 
* send message and create button functions
*/


#include <windows.h>
#include <windowsx.h>
#define IDC_MAIN_BUTTON		101		// Button identifier
#define IDC_MAIN_LISTBOX	102		// Edit box identifier
#define IDC_RIGHT_LISTBOX 103	
HWND hListBox;
HWND hListBoxLeft;
HWND hWndButton = NULL;

//prototype
LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst, LPSTR lpCmdLine, int nShowCmd)
{
	WNDCLASSEX wClass;
	ZeroMemory(&wClass, sizeof(WNDCLASSEX));
	wClass.cbClsExtra = NULL;
	wClass.cbSize = sizeof(WNDCLASSEX);
	wClass.cbWndExtra = NULL;
	wClass.hbrBackground = (HBRUSH)COLOR_WINDOW;
	wClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	wClass.hIcon = NULL;
	wClass.hIconSm = NULL;
	wClass.hInstance = hInst;
	wClass.lpfnWndProc = (WNDPROC)WinProc;
	wClass.lpszClassName = "Window Class";
	wClass.lpszMenuName = NULL;
	wClass.style = CS_HREDRAW | CS_VREDRAW;

	if (!RegisterClassEx(&wClass))
	{
		int nResult = GetLastError();
		MessageBox(NULL,
			"Window class creation failed\r\n",
			"Window Class Failed",
			MB_ICONERROR);
	}

	HWND hWnd = CreateWindowEx(NULL,
		"Window Class",
		"Windows application",
		WS_OVERLAPPEDWINDOW,
		//postion on the screen
		200,
		//postion on screen
		200,
		//size x
		640,
		//size y
		480,
		NULL,
		NULL,
		hInst,
		NULL);

	if (!hWnd)
	{
		int nResult = GetLastError();

		MessageBox(NULL,
			"Window creation failed\r\n",
			"Window Creation Failed",
			MB_ICONERROR);
	}

	ShowWindow(hWnd, nShowCmd);

	MSG msg;
	ZeroMemory(&msg, sizeof(MSG));

	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return 0;
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:
	{
		//creating the windows with the uniquie id and name
		//assigning the sizes
		hListBox = CreateWindowEx(WS_EX_CLIENTEDGE,
			"LISTBOX",
			"",
			WS_CHILD | WS_VISIBLE | LBS_NOTIFY,
			400,
			100,
			200,
			100,
			hWnd,
			(HMENU)IDC_RIGHT_LISTBOX,
			GetModuleHandle(NULL),
			NULL);

		hListBoxLeft = CreateWindowEx(WS_EX_CLIENTEDGE,
			"LISTBOX",
			"",
			WS_CHILD | WS_VISIBLE | LBS_NOTIFY,
			10,
			100,
			200,
			100,
			hWnd,
			(HMENU)IDC_MAIN_LISTBOX,
			GetModuleHandle(NULL),
			NULL);

		HGDIOBJ hfDefault = GetStockObject(DEFAULT_GUI_FONT);
		//set the font of the list boxs
		SendMessage(hListBoxLeft, WM_SETFONT, (WPARAM)hfDefault, MAKELPARAM(FALSE, 0));
		SendMessage(hListBox, WM_SETFONT, (WPARAM)hfDefault, MAKELPARAM(FALSE, 0));

		//add the names into the left window
		SendMessage(hListBoxLeft, LB_ADDSTRING, NULL, (LPARAM)"John Smith");
		SendMessage(hListBoxLeft, LB_ADDSTRING, NULL, (LPARAM)"Mark Ryan");
		SendMessage(hListBoxLeft,LB_ADDSTRING,NULL, (LPARAM)"Jerry Hayes");
		SendMessage(hListBoxLeft,LB_ADDSTRING,NULL,(LPARAM)"Anthony Hodgins");
		SendMessage(hListBoxLeft, LB_ADDSTRING, NULL, (LPARAM)"Bart Simpson");


		// Create a push button
		hWndButton = CreateWindowEx(NULL,
			"BUTTON",
			"Move",
			WS_TABSTOP | WS_VISIBLE |
			WS_CHILD | BS_DEFPUSHBUTTON,
			250,
			130,
			100,
			24,
			hWnd,
			(HMENU)IDC_MAIN_BUTTON,
			GetModuleHandle(NULL),
			NULL);
		//set font
		SendMessage(hWndButton, WM_SETFONT, (WPARAM)hfDefault, MAKELPARAM(FALSE, 0));
		//disable the button  at the start
		Button_Enable(hWndButton, FALSE);
	}
	break;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
			case IDC_MAIN_LISTBOX:
			{
				switch (HIWORD(wParam))
				{
					case LBN_SELCHANGE:
					{
						//enable the button 
						Button_Enable(hWndButton, TRUE);
						break;
					}
				}
			}
			break;
			case IDC_MAIN_BUTTON:
			{
				char buffer[256] ="";
				int indexInList = (int)SendMessage(hListBoxLeft, LB_GETCURSEL, (WPARAM)0, (LPARAM)0);
				// If there was something selected, then we get it (using LB_GETTEXT) and
				// display it with the the MessageBox method.
				if (indexInList != LB_ERR)
				{
					//reads
					SendMessage(hListBoxLeft, LB_GETTEXT, indexInList, reinterpret_cast<LPARAM>(buffer));
					SendMessage(hListBox, LB_ADDSTRING, NULL, (LPARAM)buffer);
					SendMessage(hListBoxLeft, LB_DELETESTRING, indexInList, reinterpret_cast<LPARAM>(buffer));
					//disable the button when item gets moved to the right box
					Button_Enable(hWndButton, FALSE);
				}
			}
			break;
		}
		break;

	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	break;
	}

	return DefWindowProc(hWnd, msg, wParam, lParam);
}
