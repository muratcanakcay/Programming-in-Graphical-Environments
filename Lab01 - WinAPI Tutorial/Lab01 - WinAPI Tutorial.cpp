// Lab01 - WinAPI Tutorial.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Lab01 - WinAPI Tutorial.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

// Forward declarations of functions added later for lab task 01 
void GetTextInfoForMouseMsg(WPARAM wparam, LPARAM lparam, const TCHAR* msgName, TCHAR* buf, int bufSize);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LAB01WINAPITUTORIAL, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LAB01WINAPITUTORIAL));

    MSG msg;

    // Main message loop:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS; //  "| CS_DBLCLKS" added afterwards for doubleclick messages
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LAB01WINAPITUTORIAL));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_ACTIVECAPTION+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_LAB01WINAPITUTORIAL);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, 50, 30, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   //// QUESTION ---------------------- I can change title with 103 but not with IDS_NEW_TITLE ???
   LoadStringW(hInstance, 103, szTitle, MAX_LOADSTRING); 
   SetWindowText(hWnd, L"new text lololo"); // works
   SetWindowText(hWnd, szTitle); // works. can use LoadStringW(hInstance, 103, szTitle, MAX_LOADSTRING); to load a new text into szTitle from "103" which is defined in string table
   //--------

   // Move & resize 
   MoveWindow(hWnd, 200, 200, 500, 300, true);
   
   // Make the window semitransparent
   // Set WS_EX_LAYERED on this window
   // QUESTION ---------- what's exactly happening in set and get?????
   // SetWindowLongA(hWnd, GWL_EXSTYLE, GetWindowLongA(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED); // <---- careful with paranthesis!! 
   // Make it 50% alpha
   //SetLayeredWindowAttributes(hWnd, 0, (255 * 50) / 100, LWA_ALPHA);  // <---- careful with paranthesis!!  255 * (50 / 100) = 0 !!! 
   
   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   // Create 9 identical windows positioned in 3 rows and 3 columns
   //int size = 250;
   //for (int i = 0; i < 3; i++) {
   //    for (int j = 0; j < 3; j++) {
   //        hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW | WS_VISIBLE,
   //            i * size, j * size, size, size, NULL, NULL, hInstance, NULL);
   //    }
   //}


   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE: Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    const int bufSize = 256;
    TCHAR buf[bufSize];    
    
    switch (message)
    {
    // Getting notification about changing the size of the window:
    case WM_SIZE:
        {
            // get the size of the client area
            int clientWidth = LOWORD(lParam);
            int clientHeight = HIWORD(lParam);
            // get the size of the window
            RECT rc;
            GetWindowRect(hWnd, &rc);
            // modify the caption of the window

            
            // QUESTION 
            // Unicode is supported at the system level since Windows 2000 and it is really good idea to use it
            // (it is much easier to create multilingual applications)!
            // doe shit mean we'd better use the explicit usage??? or the macros??? 
            
            // explicit unicode and wide char usage:
            wchar_t s[256];
            swprintf_s(s, 256,
            L" Window 's size : %d x %d Client area 's size : %d x %d ", rc.right - rc.left, rc.bottom - rc.top, clientWidth, clientHeight);
            SetWindowText(hWnd, s);
            
            // unicode independent:
            //TCHAR s[256]; 
            //_stprintf_s(s, 256, _T(" Window 's size : %d x %d Client area 's size : %d x %d ğüçöş"), 
            //            rc.right - rc.left, rc.bottom - rc.top, clientWidth, clientHeight);
            //SetWindowText(hWnd, s);
        }
        break;
    // ----------------------------


    // Setting the maximum and minimum possible size of the window
    //case WM_GETMINMAXINFO:
    //    {
    //         MINMAXINFO* minMaxInfo = (MINMAXINFO*)lParam;
    //         minMaxInfo->ptMaxSize.x = minMaxInfo->ptMaxTrackSize.x = 500;
    //         minMaxInfo->ptMaxSize.y = minMaxInfo->ptMaxTrackSize.y = 200;             
    //    }
    //    break;
    // ----------------------------


    // Forcing the window to be square
    //case WM_SIZING:
    //    {
    //         RECT* rc = (RECT*)lParam;   // get the rect pointer from lParam

    //         if (wParam == WMSZ_BOTTOM  // if size is changed from top or bottom border
    //            || wParam == WMSZ_BOTTOMLEFT
    //            || wParam == WMSZ_BOTTOMRIGHT
    //            || wParam == WMSZ_TOP
    //            || wParam == WMSZ_TOPLEFT
    //            || wParam == WMSZ_TOPRIGHT)
    //         {
    //             rc->right = rc->left + (rc->bottom - rc->top);
    //         }
    //         else  // if size is changed from left or right border
    //         {
    //             rc->bottom = rc->top + (rc->right - rc->left);
    //         }
    //    }
    //    break;
    // ---------------------------- 


    // Creating and using a timer
    //case WM_CREATE:
    //    // the WM_CREATE message is sent to the window procedure only once (when the
    //    // CreateWindow or CreateWindowEx function is called); it is the first message when the handle
    //    // of the window(HWND) is available and it is great place to do some initialization of the window
    //    // (the window is already created but not visible)

    //    SetTimer(hWnd, 7, 250, NULL);
    //    break;

    //case WM_TIMER:
    //    // the following code squares and centers the window then starts growing and shrinking it as the WM_TIMER timer message arrives
    //    if (wParam == 7) // check timer id
    //    {
    //        const int maxSize = 600;
    //        const int minSize = 400;
    //        static int stepSize = 10;

    //        RECT rc;
    //        // get the center of the work area of the system
    //        SystemParametersInfo(SPI_GETWORKAREA, 0, &rc, 0);
    //        int centerX = (rc.left + rc.right + 1) / 2;
    //        int centerY = (rc.top + rc.bottom + 1) / 2;
    //        // get the current size of the window
    //        GetWindowRect(hWnd, &rc);
    //        int currentSize = max(rc.right - rc.left, rc.bottom - rc.top);
    //        // modify size of the window
    //        currentSize += stepSize;
    //        if (currentSize > maxSize)
    //            stepSize = -abs(stepSize);
    //        else if (currentSize <= minSize)
    //            stepSize = abs(stepSize);

    //        MoveWindow(hWnd, centerX - currentSize / 2, centerY - currentSize / 2, currentSize, currentSize, true);
    //    }
    //    break;
    // --------------------------


    // Using mouse messages
    // for this step GetTextInfoForMouseMessage() function is added below (and declared at the top of the file) - check it out

    // The WM_xBUTTONDBLCLK message is sent when the user use the double click.To get the
    // notication, the CS_DBLCLKS style must be applied to WNDCLASSEX::style in calling the
    // RegisterClassEx function :
    // wcex.style = CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS;
    
    case WM_LBUTTONDOWN:
        GetTextInfoForMouseMsg(wParam, lParam, _T("LBUTTONDOWN"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_LBUTTONUP:
        GetTextInfoForMouseMsg(wParam, lParam, _T("LBUTTONUP"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_MBUTTONDOWN:
        GetTextInfoForMouseMsg(wParam, lParam, _T("MBUTTONDOWN"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_MBUTTONUP:
        GetTextInfoForMouseMsg(wParam, lParam, _T("MBUTTONUP"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_RBUTTONDOWN:
        GetTextInfoForMouseMsg(wParam, lParam, _T("RBUTTONDOWN"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_RBUTTONUP:
        GetTextInfoForMouseMsg(wParam, lParam, _T("RBUTTONUP"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_LBUTTONDBLCLK:
        GetTextInfoForMouseMsg(wParam, lParam, _T("LBUTTONDBLCLICK"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_MBUTTONDBLCLK:
        GetTextInfoForMouseMsg(wParam, lParam, _T("MBUTTONDBLCLICK"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    case WM_RBUTTONDBLCLK:
        GetTextInfoForMouseMsg(wParam, lParam, _T("RBUTTONDBLCLICK"), buf, bufSize);
        SetWindowText(hWnd, buf);
        break;
    // ----------------------------



    
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);

            // Move the window to the right in response for clicking on any menu item
            RECT rc;
            GetWindowRect(hWnd, &rc);
            OffsetRect(&rc, 20, 0);
            MoveWindow(hWnd, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, true);


            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case ID_DANCE_SHOWYOURMOVES:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_DANCE), hWnd, About);
                break;            
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            // TODO: Add any drawing code that uses hdc here...
            EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}


void GetTextInfoForMouseMsg(WPARAM wParam, LPARAM lParam, const TCHAR* msgName, TCHAR* buf, int bufSize)
{
    // get the size of the client area
    short x = (short)LOWORD(lParam);
    short y = (short)HIWORD(lParam);
    
    //prepare message string
    _stprintf_s(buf, bufSize, _T("%s x: %d, y: %d, vk:"), msgName, x, y);
    
    // add relevant info to the message string
    if ((wParam == MK_LBUTTON) != 0)
        _tcscat_s(buf, bufSize, _T(" LEFT"));
    if ((wParam == MK_MBUTTON) != 0)
        _tcscat_s(buf, bufSize, _T(" MIDDLE"));
    if ((wParam == MK_RBUTTON) != 0)
        _tcscat_s(buf, bufSize, _T(" RIGHT"));
}