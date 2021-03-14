// FRUITNINJAH.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "FruitNinjaH.h"

#define MAX_LOADSTRING 100
#define SMALL_HEIGHT 300
#define SMALL_WIDTH 400
#define MEDIUM_HEIGHT 500
#define MEDIUM_WIDTH 600
#define BIG_WIDTH 800
#define BIG_HEIGHT 600
#define SQUARE_SIZE 50
#define GAME_DURATION 30  // (seconds)
#define REFRESH_RATE 50  // (miliseconds)
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
int nProgressBarHeight = 20;                    // progress bar height
static int nGameTicks = 0;                             // game ticks

// FOR DEBUGGING
const int bufSize = 256;
TCHAR buf[bufSize];

static HCURSOR cursor = NULL;
static HDC offDC = NULL;
static HBITMAP offOldBitmap = NULL;
static HBITMAP offBitmap = NULL;

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
void DrawBoard(HWND hWnd, HDC hdc, HDC offDC);
void DrawProgressBar(HWND hWnd, HDC hdc, HDC offDC);
void StartNewGame(HWND hWnd);
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
    
    
    switch (message)
    {
        case WM_CREATE:
        {
            HDC hdc = GetDC(hWnd);
            offDC = CreateCompatibleDC(hdc);
            ReleaseDC(hWnd, hdc); 
            
            // set the board dimensions
            // start the timers
            InitializeGame(hWnd);

        } break;

        case WM_TIMER: 
        {
            if (wParam == 7)           // transparency timer (TODO: perhaps add second timer to smooth it)
            {
            SetLayeredWindowAttributes(hWnd, 0, (255 * 20) / 100, LWA_ALPHA);
            UpdateWindow(hWnd);
            }
            else if (wParam == 9)                // game timer
            {
                nGameTicks++;
                //if (nGameTicks == GAME_DURATION * 1000 / REFRESH_RATE) {}; // TODO: game over
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
        //case WM_WINDOWPOSCHANGING: // Prevent repositioning
        //{
        //    int x = (ScreenX - nWindowWidth) / 2;
        //    int y = (ScreenY - nWindowHeight) / 2;
        //    ((WINDOWPOS*)lParam)->x = x;
        //    ((WINDOWPOS*)lParam)->y = y;
        //} break;    
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
            
            // TODO: separate bitmaps for all stuff?
            {
                int clientWidth = nBoardWidth;
                int clientHeight = nBoardHeight + nProgressBarHeight;
                //HDC hdc = GetDC(hWnd);
                if (offOldBitmap != NULL)
                    SelectObject(offDC, offOldBitmap);
                if (offBitmap != -NULL)
                    DeleteObject(offBitmap);

                offBitmap = CreateCompatibleBitmap(hdc, clientWidth, clientHeight);
                offOldBitmap = (HBITMAP)SelectObject(offDC, offBitmap);
                //ReleaseDC(hWnd, hdc);
            }
            
            
            DrawBoard(hWnd, hdc, offDC);
            DrawProgressBar(hWnd, hdc, offDC);
            
            EndPaint(hWnd, &ps);
        } break;

        case WM_ERASEBKGND:
            return 1;

        case WM_DESTROY:
        {
            if (offOldBitmap != NULL)
                SelectObject(offDC, offOldBitmap);
            if (offDC != NULL)
                DeleteDC(offDC);
            if (offBitmap != NULL)
                DeleteObject(offBitmap); 
            
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


void DrawBoard(HWND hWnd, HDC hdc, HDC offDC)
{
    HBRUSH bbrush = (HBRUSH)GetStockObject(BLACK_BRUSH);
    HBRUSH wbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, bbrush);
    
    int nColumns = nBoardWidth / SQUARE_SIZE;
    int nRows = nBoardHeight / SQUARE_SIZE;

    for (int r = 0; r < nRows; r++)
    {
        for (int c = 0; c < nColumns; c++)
        {
            if ((c + r) % 2 == 0) SelectObject(offDC, bbrush);
            else SelectObject(offDC, wbrush);

            Rectangle(offDC, c * SQUARE_SIZE, r * SQUARE_SIZE, (c + 1) * SQUARE_SIZE, (r + 1) * SQUARE_SIZE);
        }
    }

    BitBlt(hdc, 0, 0, nBoardWidth, nBoardHeight, offDC, 0, 0, SRCCOPY);
    SelectObject(offDC, oldbrush);
}
void DrawProgressBar(HWND hWnd, HDC hdc, HDC offDC)
{
    HBRUSH wbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH gbrush = CreateSolidBrush(RGB(11, 184, 17));    
    
    HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, wbrush);
    //Rectangle(offDC, 0, nBoardHeight, nBoardWidth/2, nBoardHeight + nProgressBarHeight);
    
    static int counter = 0;

    counter++;

    /*_stprintf_s(buf, bufSize, _T("%d"), nGameTicks);
    OutputDebugString(buf);
    SetWindowText(hWnd, buf);*/   
    
    //stretchblt
    
    Rectangle(offDC, (nBoardWidth * counter) / (GAME_DURATION * 1000 / REFRESH_RATE), nBoardHeight, nBoardWidth, nBoardHeight + nProgressBarHeight);
    
    SelectObject(offDC, gbrush);
    Rectangle(offDC, 0, nBoardHeight, (nBoardWidth * nGameTicks) / (GAME_DURATION * 1000 / REFRESH_RATE), nBoardHeight + nProgressBarHeight);
    
    BitBlt(hdc, 0, 0, nBoardWidth, nBoardHeight+nProgressBarHeight, offDC, 0, 0, SRCCOPY); 
    SelectObject(offDC, oldbrush);
    DeleteObject(gbrush);
    
    const RECT rc = { 0, nBoardHeight, nBoardWidth, nBoardHeight + nProgressBarHeight };
    InvalidateRect(hWnd, &rc, FALSE);
    UpdateWindow(hWnd);
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
    int rows = nBoardHeight / SQUARE_SIZE;
    switch (rows)
    {
        case 6:
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            break;
        case 10:
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            break;
        case 12:
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            break;
    }

    StartNewGame(hWnd);
}
void ChangeBoardSize(HWND hWnd, int wmId)
{
    switch (wmId)
    {
        case ID_BOARD_SMALL:
        {
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            nBoardHeight = SMALL_HEIGHT;
            nBoardWidth = SMALL_WIDTH;
        }break;

        case ID_BOARD_MEDIUM:
        {
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            nBoardHeight = MEDIUM_HEIGHT;
            nBoardWidth = MEDIUM_WIDTH;
        }break;

        case ID_BOARD_BIG:
        {
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            nBoardHeight = BIG_HEIGHT;
            nBoardWidth = BIG_WIDTH;
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
        nBoardHeight = SMALL_HEIGHT;
        nBoardWidth = SMALL_WIDTH;
    }
}
void SetWindowPosition()
{
    // Set the Size and Position of the main window
    RECT rc;
    rc.top = rc.left = 0;
    rc.bottom = nBoardHeight + nProgressBarHeight;
    rc.right = nBoardWidth;

    AdjustWindowRectEx(&rc, WS_BORDER | WS_CAPTION, true, 0);
    
    nWindowWidth = rc.right - rc.left;
    nWindowHeight = rc.bottom - rc.top;
    WindowPos = { (ScreenX - nWindowWidth) / 2 , (ScreenY - nWindowHeight) / 2 };
}
void StartNewGame(HWND hWnd)
{
    SetTimer(hWnd, 7, 3000, NULL);              // Transparancy timer
    SetTimer(hWnd, 9, REFRESH_RATE, NULL);      // Game timer - 20Hz - Game over at nGameTicks = 600    
}