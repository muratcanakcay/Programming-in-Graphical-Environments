// FRUITNINJAH.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "FruitNinjaH.h"

#define MAX_LOADSTRING 100
#define ScreenX GetSystemMetrics(SM_CXSCREEN)
#define ScreenY GetSystemMetrics(SM_CYSCREEN)

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
int nWindowWidth;                               // main windows height
int nWindowHeight;                              // main window width
POINT WindowPos;                                // main window position        
int nBoardHeight;                               // board height
int nBoardWidth;                                // board width 
int pbarheight = 20;                            // progress bar height
static HCURSOR cursor = NULL;

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
//ATOM MyRegisterBlackSquareClass(HINSTANCE hInstance);
//ATOM MyRegisterWhiteSquareClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
//LRESULT CALLBACK WndProcSq(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
void InitBoardDimensions();
void SetWindowPosition();
void DrawBoard(HWND hWnd, HDC hdc);
DWORD CheckItem(UINT hItem, HMENU hmenu);
void InitializeGame(HWND hWnd);
void ChangeBoardSize(HWND hWnd, int wmId);


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
    LoadStringW(hInstance, IDC_FRUITNINJAH, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);
   /* MyRegisterWhiteSquareClass(hInstance);
    MyRegisterBlackSquareClass(hInstance);*/

    // Perform application initialization:
    if (!InitInstance(hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_FRUITNINJAH));

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

    return (int)msg.wParam;
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

    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FRUITNINJA));
    wcex.hCursor = LoadCursor(hInstance, MAKEINTRESOURCE(IDC_KNIFE));
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_FRUITNINJAH);
    wcex.lpszClassName = szWindowClass;
    wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_FRUITNINJA));

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
    
    InitBoardDimensions();
    SetWindowPosition();

    HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPED | WS_SYSMENU | WS_MAXIMIZEBOX,
        WindowPos.x, WindowPos.y, nWindowWidth, nWindowHeight, nullptr, nullptr, hInstance, nullptr);

    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
    SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED);
    SetLayeredWindowAttributes(hWnd, 0, (255 * 100) / 100, LWA_ALPHA);

    if (!hWnd)
    {
        return FALSE;
    }

    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);

    return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    // FOR DEBUGGING
    const int bufSize = 256;
    TCHAR buf[bufSize];
    
    switch (message)
    {
        case WM_CREATE:
        {
            InitializeGame(hWnd);

        } break;

        case WM_TIMER: // Make window transparent (TODO: perhaps add second timer to smooth it)
        {
            if (wParam == 7)
            {
                SetLayeredWindowAttributes(hWnd, 0, (255 * 20) / 100, LWA_ALPHA);
                UpdateWindow(hWnd);
            }
        } break;

        case WM_NCMOUSEMOVE:
        {
            SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA); // Remove transparency 
            SetTimer(hWnd, 7, 3000, NULL); // Reset transparancy timer        
        } break;

        case WM_MOUSEMOVE:
        {
            SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA); // Remove transparency 
            SetTimer(hWnd, 7, 3000, NULL); // Reset transparancy timer        
        } break;

        case WM_WINDOWPOSCHANGING: // Prevent repositioning
        {
            int x = (ScreenX - nWindowWidth) / 2;
            int y = (ScreenY - nWindowHeight) / 2;
            ((WINDOWPOS*)lParam)->x = x;
            ((WINDOWPOS*)lParam)->y = y;
        } break;    

        case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
                case IDM_ABOUT:
                {    
                    DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                } break;
                case ID_BOARD_SMALL:
                case ID_BOARD_MEDIUM:
                case ID_BOARD_BIG:
                {
                    ChangeBoardSize(hWnd, wmId);
                } break;
        

                case IDM_EXIT:
                {  
                    DestroyWindow(hWnd);
                } break;
                default:
                    return DefWindowProc(hWnd, message, wParam, lParam);
            }
        } break;
    
        case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            DrawBoard(hWnd, hdc);
            EndPaint(hWnd, &ps);
        } break;

        case WM_DESTROY:
        {
            PostQuitMessage(0);
        } break;
    
        default:
            return DefWindowProc(hWnd, message, wParam, lParam);
    }

    return 0;
}

LRESULT CALLBACK WndProcSq(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {

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


void DrawBoard(HWND hWnd, HDC hdc)
{
    HBRUSH wbrush = (HBRUSH)GetStockObject(BLACK_BRUSH);
    HBRUSH bbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH oldbrush = (HBRUSH)SelectObject(hdc, bbrush);

    RECT rc;
    GetClientRect(hWnd, &rc);
    int nColumns = rc.right / 50;
    int nRows = rc.bottom / 50;

    for (int r = 0; r < nRows; r++)
    {
        for (int c = 0; c < nColumns; c++)
        {
            if ((c + r) % 2 == 0) SelectObject(hdc, bbrush);
            else SelectObject(hdc, wbrush);

            Rectangle(hdc, rc.left + c * 50, rc.top + r * 50, rc.left + (c + 1) * 50, rc.top + (r + 1) * 50);
        }
    }

    SelectObject(hdc, oldbrush);
    DeleteObject(wbrush);
    DeleteObject(bbrush);
}

DWORD CheckItem(UINT hItem, HMENU hmenu)
{
    //First uncheck all
    CheckMenuItem(hmenu, ID_BOARD_SMALL, MF_BYCOMMAND | MF_UNCHECKED);
    CheckMenuItem(hmenu, ID_BOARD_MEDIUM, MF_BYCOMMAND | MF_UNCHECKED);
    CheckMenuItem(hmenu, ID_BOARD_BIG, MF_BYCOMMAND | MF_UNCHECKED);
    //then check the hItem
    return CheckMenuItem(hmenu, hItem, MF_BYCOMMAND | MF_CHECKED);
}

void InitializeGame(HWND hWnd)
{
    switch (nBoardHeight)
    {
        case 300:
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            break;
        case 500:
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            break;
        case 600:
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            break;
    }

    SetTimer(hWnd, 7, 3000, NULL); // Transparancy timer
}

void ChangeBoardSize(HWND hWnd, int wmId)
{
    switch (wmId)
    {
        case ID_BOARD_SMALL:
        {
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            nBoardHeight = 300;
            nBoardWidth = 400;
        }break;

        case ID_BOARD_MEDIUM:
        {
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            nBoardHeight = 500;
            nBoardWidth = 600;
        }break;

        case ID_BOARD_BIG:
        {
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            nBoardHeight = 600;
            nBoardWidth = 800;
        }break;
    }

    SetWindowPosition();
    MoveWindow(hWnd, WindowPos.x, WindowPos.y, nWindowWidth, nWindowHeight, TRUE);

    // NewGame();
}

void InitBoardDimensions()
{
    // initializa board dimensions
    if (false) {} // TODO: read last board dimensions from .ini file (if it exists)
    else
    {
        nBoardHeight = 300;
        nBoardWidth = 400;
    }
}

void SetWindowPosition()
{
    // Set the Size and Position of the main window
    RECT rc;
    rc.top = rc.left = 0;
    rc.bottom = nBoardHeight;
    rc.right = nBoardWidth;

    AdjustWindowRectEx(&rc, WS_BORDER | WS_CAPTION, true, 0);
    
    nWindowWidth = rc.right - rc.left;
    nWindowHeight = rc.bottom - rc.top;
    WindowPos = { (ScreenX - nWindowWidth) / 2 , (ScreenY - nWindowHeight) / 2 };
}