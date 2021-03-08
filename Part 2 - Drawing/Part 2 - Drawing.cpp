// Part 2 - Drawing.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Part 2 - Drawing.h"

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
    LoadStringW(hInstance, IDC_PART2DRAWING, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_PART2DRAWING));

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

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PART2DRAWING));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_PART2DRAWING);
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
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

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
    switch (message)
    {
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    
    /* DRAWING TEXT */
    
    // The simplest way to draw a text:
    //case WM_PAINT:
    //    {
    //        PAINTSTRUCT ps;
    //        HDC hdc = BeginPaint(hWnd, &ps);  // handle to device context (DC)
    //        TCHAR s[] = _T("Hello World!");  // define text
    //        RECT rc;
    //        GetWindowRect(hWnd, &rc); // gets window area (the entire window incl. menu bar, not just client area) - this is wrong!! CLIENT AREA SHOULD BE USED FOR DRAWING
    //        TextOut(hdc, (rc.right - rc.left) / 2, (rc.bottom - rc.top) / 2, s, (int)_tcslen(s)); // draw text s with length (int)_tcslen(s) starting at center of client area
    //        EndPaint(hWnd, &ps);
    //    }
    //    break;
    
    // More options are available when the DrawText() function is used :
    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-drawtext
    //case WM_PAINT:
    //    {
    //        PAINTSTRUCT ps;
    //        HDC hdc = BeginPaint(hWnd, &ps);
    //        TCHAR s[] = _T("Hello World!");  // define text
    //        RECT rc;
    //        GetClientRect(hWnd, &rc); // gets only the client area of the window
    //        DrawText(hdc, s, (int)_tcslen(s), &rc, DT_CENTER | DT_VCENTER | DT_SINGLELINE); // this time we can REALLY center the text using flags
    //        EndPaint(hWnd, &ps);
    //    }
    //    break;
    
    
    /* ACTUALLY DRAWING STUFF */
    
    // Using pens
    // The HPEN is a handle to a pen and represents a pen in drawing using GDI
    //case WM_PAINT:
    //{
    //    //� GDI always uses current objects(one pen, one brush, one font, etc.) to draw, the object can be
    //    //selected as current on the device context(HDC) using the SelectObject function.
    //    //� The colour contains 3 values: for the red, green, and blue.The RGB macro can be used to set
    //    //specic colour, its parameters are integer values from the range[0..255])
    //    //� VERY IMPORTANT : All created GDI objects must be destroyed.The pen created using the
    //    //CreatePen function must be destroyed using the DeleteObject.
    //    //� VERY IMPORTANT : Objects selected as current on the device context cannot be destroyed(so
    //    //  always remember the old objectand restore it before calling the DeleteObject function)
    //    
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    HPEN pen = CreatePen(PS_SOLID, 5, RGB(255, 0, 0)); // define new pen to be used
    //    HPEN oldpen = (HPEN)SelectObject(hdc, pen); // (start using defined pen, store oldpen)
    //    MoveToEx(hdc, 0, 0, NULL); // move to (0,0)
    //    LineTo(hdc, 100, 100);  // draw line to (100,100)
    //    SelectObject(hdc, oldpen); // switch to oldpen
    //    DeleteObject(pen); // delete pen
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    
    // Using brushes
    // Brushes are similar to pens, they are used when something must be filled, e.g. a rectangle:
    //case WM_PAINT:
    //{
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    HPEN pen = CreatePen(PS_DOT, 1, RGB(255, 0, 0));
    //    HPEN oldpen = (HPEN)SelectObject(hdc, pen);
    //    HBRUSH brush = CreateSolidBrush(RGB(0, 128, 0));
    //    HBRUSH oldbrush = (HBRUSH)SelectObject(hdc, brush);
    //    Rectangle(hdc, 20, 20, 120, 120);
    //    SelectObject(hdc, oldpen);
    //    DeleteObject(pen);
    //    SelectObject(hdc, oldbrush);
    //    DeleteObject(brush);
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    
    /* USING FONTS */
    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hWnd, &ps);
        TCHAR s[] = _T("Hello World!");
        HFONT font = CreateFont(
            -MulDiv(24, GetDeviceCaps(hdc, LOGPIXELSY), 72),    // height
            0,                                                  // Width
            0,                                                  // Escapament
            0,                                                  // Orientation
            FW_BOLD,                                            // Weight
            false,                                              // Italic (false == FALSE  == 0)
            FALSE,                                              // Underline
            0,                                                  // StrikeOut
            EASTEUROPE_CHARSET,                                 // CharSet
            OUT_DEFAULT_PRECIS,                                 // OutPrecision
            CLIP_DEFAULT_PRECIS,                                // ClipPrecision
            DEFAULT_QUALITY,                                    // Quality
            DEFAULT_PITCH | FF_SWISS,                           // PitchAndFamily
            _T("Verdana"));                                     // Facename
        
        HFONT oldfont = (HFONT)SelectObject(hdc, font);
        RECT rc;
        GetClientRect(hWnd, &rc);
        
        DrawText(hdc, s, (int)_tcslen(s), &rc, DT_CENTER | DT_VCENTER | DT_SINGLELINE);

        SelectObject(hdc, oldfont);
        DeleteObject(font);
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
