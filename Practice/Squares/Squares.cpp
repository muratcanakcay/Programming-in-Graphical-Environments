// Squares.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Squares.h"

#define MAX_LOADSTRING 100

#define S_SIZE 100
#define S_MARGIN 10
#define N_SQUARES 3


bool tracking = false;

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
ATOM                MyRegisterSquareClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK    WndProcSq(HWND, UINT, WPARAM, LPARAM);
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
    LoadStringW(hInstance, IDC_SQUARES, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);
    MyRegisterSquareClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_SQUARES));

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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_SQUARES));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_SQUARES);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

ATOM MyRegisterSquareClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProcSq;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_SQUARES));
    wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(GetStockObject(BLACK_BRUSH));
    wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_SQUARES);
    wcex.lpszClassName = L"SquareClass";
    wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

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

   RECT rc;
   rc.top = 0;
   rc.bottom = (S_SIZE * N_SQUARES) + (2 * N_SQUARES * S_MARGIN);
   rc.left = 0;
   rc.right = (S_SIZE * N_SQUARES) + (2 * N_SQUARES * S_MARGIN);
   AdjustWindowRectEx(&rc, WS_BORDER | WS_CAPTION, true, 0);

   
   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      500, 200, rc.right - rc.left, rc.bottom-rc.top, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   // Create one child window

   //HWND hWndSquare = CreateWindow(L"SquareClass", L"Square", WS_CHILD,
   //       50, 50, S_SIZE, S_SIZE, hWnd, NULL, hInstance, NULL);
   //ShowWindow(hWndSquare, nCmdShow);
   //UpdateWindow(hWndSquare);
   
   
   // Create child square windows positioned in N_SQUARES rows and columns

   HWND hWndSq[N_SQUARES][N_SQUARES];

   for (int i = 0; i < N_SQUARES; i++) {
       for (int j = 0; j < N_SQUARES; j++) {
           hWndSq[i][j] = CreateWindow(L"SquareClass", L"Square", WS_CHILD,
               ((S_SIZE + 2 * S_MARGIN) * i) + S_MARGIN, ((S_SIZE + 2 * S_MARGIN) * j) + S_MARGIN, S_SIZE, S_SIZE, hWnd, NULL, hInstance, NULL);
       }
   }

   for (int i = 0; i < N_SQUARES; i++) {
       for (int j = 0; j < N_SQUARES; j++) {
           ShowWindow(hWndSq[i][j], nCmdShow);
           UpdateWindow(hWndSq[i][j]);
       }
   }

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
    //case WM_CREATE:
    //{
    //    TRACKMOUSEEVENT me;
    //    me.hwndTrack = hWnd;
    //    me.dwHoverTime = 1000;
    //    TrackMouseEvent(&me);
    //}
    //case WM_MOUSEHOVER:
    //{
    //    SetWindowText(hWnd, L"MOUSE"); 
    //}
    
    
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

LRESULT CALLBACK WndProcSq(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    RECT rc;
    

    switch (message)
    {
    case WM_CREATE:
    {
        
    }
    case WM_MOUSEMOVE:
        {
        if (!tracking)
        {
            TRACKMOUSEEVENT tme;
            tme.cbSize = sizeof(TRACKMOUSEEVENT);
            tme.dwFlags = TME_HOVER | TME_LEAVE; //Type of events to track & trigger.
            tme.hwndTrack = hWnd;
            tme.dwHoverTime = 1;
            TrackMouseEvent(&tme);
            tracking = true;
        }
        }
        break;

    case WM_MOUSEHOVER:
    {
        GetWindowRect(hWnd, &rc);
        MapWindowPoints(HWND_DESKTOP, GetParent(hWnd), (LPPOINT)&rc, 2);
        MoveWindow(hWnd, rc.left - 2, rc.top - 2, rc.right - rc.left + 4, rc.bottom - rc.top + 4, true);
        //InvalidateRect(hWnd, NULL, TRUE);
    }break;

    case WM_MOUSELEAVE:
    {
        GetWindowRect(hWnd, &rc);
        MapWindowPoints(HWND_DESKTOP, GetParent(hWnd), (LPPOINT)&rc, 2);
        MoveWindow(hWnd, rc.left + 2, rc.top + 2, rc.right - rc.left - 4, rc.bottom - rc.top - 4, true);
        //InvalidateRect(hWnd, NULL, TRUE);
        tracking = false;
    }break;

    //case WM_NCHITTEST:
    //    return HTTRANSPARENT;
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
