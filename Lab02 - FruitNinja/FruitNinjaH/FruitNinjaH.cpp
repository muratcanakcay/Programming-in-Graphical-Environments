// FRUITNINJAH.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "FruitNinjaH.h"
#include <list>
#include <ctime>
#include <map>
#include <string>

#define MAX_LOADSTRING 100
#define SMALL_HEIGHT 300
#define SMALL_WIDTH 400
#define MEDIUM_HEIGHT 500
#define MEDIUM_WIDTH 600
#define BIG_WIDTH 800
#define BIG_HEIGHT 600
#define PROGRESS_BAR_HEIGHT 20
#define SQUARE_SIZE 50
#define GAME_DURATION 30         // (seconds)
#define REFRESH_RATE 200        // (Hz)
#define BALL_SIZE_L 60
#define BALL_SIZE_M 30
#define BALL_SIZE_S 12
#define SMALL 0
#define MEDIUM 1
#define BIG 2
#define TRANSPARENCY_TIMER 7
#define GAME_TIMER 9
#define SPAWN_TIMER 10
#define SPAWN_RATE 1            // (seconds between ball spawns)
#define ScreenX GetSystemMetrics(SM_CXSCREEN)
#define ScreenY GetSystemMetrics(SM_CYSCREEN)

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

struct Ball_t {
    POINT position;
    INT dx;
    INT dy;
    COLORREF color;
    INT spawnTime;
    INT size;
};

std::list<Ball_t> balls;                        // list of balls
INT nWindowWidth;                               // main windows height
INT nWindowHeight;                              // main window width
POINT WindowPos;                                // main window position        
INT nBoardHeight;                               // board height
INT nBoardWidth;                                // board width 
INT nBoardSize;
static INT nGameTicks = 0;                             // game ticks
BOOL MouseTracking = FALSE;
std::map<int, COLORREF> colorSet { 
    {0, RGB(250, 0, 200)},
    {1, RGB(200, 250, 0)},
    {2, RGB(0, 0, 255)},
    {3, RGB(60, 200, 25)},
    {4, RGB(255, 0, 0)},
    {5, RGB(255, 0, 255)}
};

static HCURSOR cursor = NULL;
static HDC offDC = NULL;
static HBITMAP offOldBitmap = NULL;
 HBITMAP offBitmap = NULL;

// FOR DEBUGGING
CONST INT bufSize = 256;
TCHAR buf[bufSize];

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
//ATOM MyRegisterBlackSquareClass(HINSTANCE hInstance);
//ATOM MyRegisterWhiteSquareClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, INT);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
//LRESULT CALLBACK WndProcSq(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
VOID InitBoardDimensions();
VOID SetWindowPosition();
VOID DrawBoard(HWND hWnd, HDC hdc, HDC offDC);
VOID DrawProgressBar(HWND hWnd, HDC hdc, HDC offDC);
VOID StartNewGame(HWND hWnd);
DWORD CheckItem(UINT hItem, HMENU hmenu);
VOID InitializeGame(HWND hWnd);
VOID ChangeBoardSize(HWND hWnd, INT wmId);
VOID DrawBalls(HWND hWnd, HDC hdc, HDC offDC);
VOID SpawnBall(INT ballSize);
VOID TrackMouse(HWND hWnd);


INT APIENTRY wWinMain(_In_ HINSTANCE hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR    lpCmdLine,
    _In_ INT       nCmdShow)
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

    return (INT)msg.wParam;
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
//   FUNCTION: InitInstance(HINSTANCE, INT)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, INT nCmdShow)
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
            if (wParam == GAME_TIMER)                                // game timer
            {
                nGameTicks++;
                const RECT rc = { 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT };
                InvalidateRect(hWnd, &rc, FALSE);


                if (nGameTicks == GAME_DURATION * 1000 / REFRESH_RATE) {}; // TODO: game over

            }
            else if (wParam == TRANSPARENCY_TIMER)                  // transparency timer (TODO: perhaps add second timer to smooth it)
            {
                SetLayeredWindowAttributes(hWnd, 0, (255 * 20) / 100, LWA_ALPHA);
            }
            else if (wParam == SPAWN_TIMER)                         // ball spawn timer
            {
                SpawnBall(BALL_SIZE_L);
            }
        } break;
        case WM_NCMOUSEMOVE:
        case WM_MOUSEMOVE:
        {
            if (!MouseTracking)
                TrackMouse(hWnd);
        } break;
        case WM_MOUSELEAVE:
        {
            SetTimer(hWnd, 7, 3000, NULL);                       // Reset transparancy timer
            MouseTracking = false;
        } break;
        //case WM_WINDOWPOSCHANGING: // Prevent repositioning
        //{
        //    INT x = (ScreenX - nWindowWidth) / 2;
        //    INT y = (ScreenY - nWindowHeight) / 2;
        //    ((WINDOWPOS*)lParam)->x = x;
        //    ((WINDOWPOS*)lParam)->y = y;
        //} break;    
        case WM_COMMAND:
        {
            INT wmId = LOWORD(wParam);
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
            
            // bitmap
            {
                INT clientWidth = nBoardWidth;
                INT clientHeight = nBoardHeight + PROGRESS_BAR_HEIGHT;
                
                if (offOldBitmap != NULL)
                    SelectObject(offDC, offOldBitmap);
                if (offBitmap != -NULL)
                    DeleteObject(offBitmap);

                offBitmap = CreateCompatibleBitmap(hdc, clientWidth, clientHeight);
                offOldBitmap = (HBITMAP)SelectObject(offDC, offBitmap);
            }
            
            DrawBoard(hWnd, hdc, offDC);
            DrawBalls(hWnd, hdc, offDC);
            DrawProgressBar(hWnd, hdc, offDC);
            
            BitBlt(hdc, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT, offDC, 0, 0, SRCCOPY);
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

// TODO: REMOVE THIS IF UNUSED
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


VOID DrawBoard(HWND hWnd, HDC hdc, HDC offDC)
{
    HBRUSH bbrush = (HBRUSH)GetStockObject(BLACK_BRUSH);
    HBRUSH wbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, bbrush);
    
    INT nColumns = nBoardWidth / SQUARE_SIZE;
    INT nRows = nBoardHeight / SQUARE_SIZE;

    for (INT r = 0; r < nRows; r++)
    {
        for (INT c = 0; c < nColumns; c++)
        {
            if ((c + r) % 2 == 0) SelectObject(offDC, bbrush);
            else SelectObject(offDC, wbrush);

            Rectangle(offDC, c * SQUARE_SIZE, r * SQUARE_SIZE, (c + 1) * SQUARE_SIZE, (r + 1) * SQUARE_SIZE);
        }
    }

    SelectObject(offDC, oldbrush);
}
VOID DrawProgressBar(HWND hWnd, HDC hdc, HDC offDC)
{
    HBRUSH wbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH gbrush = CreateSolidBrush(RGB(11, 184, 17));    
    HPEN wpen = (HPEN)GetStockObject(WHITE_PEN);
    HPEN gpen = CreatePen(PS_SOLID, 1, RGB(11, 184, 17));
    
    HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, wbrush);
    HPEN oldpen = (HPEN)SelectObject(offDC, wpen);
    
    Rectangle(offDC, (nBoardWidth * nGameTicks) / (GAME_DURATION * REFRESH_RATE), nBoardHeight-1, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT);
    
    SelectObject(offDC, gbrush);
    SelectObject(offDC, gpen);
    Rectangle(offDC, 0, nBoardHeight-1, (nBoardWidth * nGameTicks) / (GAME_DURATION * REFRESH_RATE), nBoardHeight + PROGRESS_BAR_HEIGHT);
    
    SelectObject(offDC, oldbrush);
    SelectObject(offDC, oldpen);
    DeleteObject(gbrush);
    DeleteObject(gpen);
}
VOID DrawBalls(HWND hWnd, HDC hdc, HDC offDC)
{
    std::list<Ball_t>::iterator it = balls.begin();
    while(it != balls.end())
    {
        Ball_t ball = *it;

        HBRUSH bbrush = CreateSolidBrush(ball.color);
        HPEN bpen = CreatePen(PS_SOLID, 1, ball.color);

        HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, bbrush);
        HPEN oldpen = (HPEN)SelectObject(offDC, bpen);

        INT ballLife = nGameTicks - ball.spawnTime;

        Ellipse(offDC
            /*left*/, ball.position.x + (INT)(ball.dx * ballLife / 500)
            /*top*/, ball.position.y + (INT)(ball.dy * ballLife / 500)
            /*right*/, ball.position.x + ball.size + (INT)(ball.dx * ballLife / 500)
            /*bottom*/, ball.position.y + ball.size + (INT)(ball.dy * ballLife / 500)
        );

        // gravity
        it->dy = ((ball.dy * 100) + ballLife) / 100;

        SelectObject(offDC, oldbrush);
        SelectObject(offDC, oldpen);
        DeleteObject(bbrush);
        DeleteObject(bpen);

        it++;
    }
}
VOID SpawnBall(INT ballSize)
{
    Ball_t ball;
    ball.position = { rand() % nBoardWidth, nBoardHeight };
    ball.dx = (ball.position.x < nBoardWidth / 2 ? 1 : -1) * (rand() % 60 + 100);
    ball.dy = -(rand() % 100 + 100) * (nBoardHeight / 50);
    ball.color = colorSet[rand() % 6];
    ball.spawnTime = nGameTicks;
    ball.size = ballSize;
    
    balls.push_back(ball);    
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
VOID InitializeGame(HWND hWnd)
{
    INT rows = nBoardHeight / SQUARE_SIZE;
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
VOID ChangeBoardSize(HWND hWnd, INT wmId)
{
    switch (wmId)
    {
        case ID_BOARD_SMALL:
        {
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            nBoardHeight = SMALL_HEIGHT;
            nBoardWidth = SMALL_WIDTH;
            nBoardSize = SMALL;
        }break;

        case ID_BOARD_MEDIUM:
        {
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            nBoardHeight = MEDIUM_HEIGHT;
            nBoardWidth = MEDIUM_WIDTH;
            nBoardSize = MEDIUM;
        }break;

        case ID_BOARD_BIG:
        {
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            nBoardHeight = BIG_HEIGHT;
            nBoardWidth = BIG_WIDTH;
            nBoardSize = BIG;
        }break;
    }

    SetWindowPosition();
    MoveWindow(hWnd, WindowPos.x, WindowPos.y, nWindowWidth, nWindowHeight, TRUE);
    

    balls.clear(); // move this to StartNewGame() after implementing SpawnBall()
    StartNewGame(hWnd);
}
VOID InitBoardDimensions()
{
    // initializa board dimensions
    if (false) {} // TODO: read last board dimensions from .ini file (if it exists)
    else
    {
        nBoardHeight = SMALL_HEIGHT;
        nBoardWidth = SMALL_WIDTH;
        nBoardSize = SMALL;
    }
}
VOID SetWindowPosition()
{
    // Set the Size and Position of the main window according to board size
    RECT rc;
    rc.top = rc.left = 0;
    rc.bottom = nBoardHeight + PROGRESS_BAR_HEIGHT;
    rc.right = nBoardWidth;

    AdjustWindowRectEx(&rc, WS_BORDER | WS_CAPTION, true, 0);
    
    nWindowWidth = rc.right - rc.left;
    nWindowHeight = rc.bottom - rc.top;
    WindowPos = { (ScreenX - nWindowWidth) / 2 , (ScreenY - nWindowHeight) / 2 };
}
VOID StartNewGame(HWND hWnd)
{
    nGameTicks = 0;
    SetTimer(hWnd, TRANSPARENCY_TIMER, 3000, NULL);             // Transparancy timer
    SetTimer(hWnd, GAME_TIMER, 1000 / REFRESH_RATE, NULL);      // Game timer - 20 Hz => 50ms => Game over at nGameTicks = 600    
    SetTimer(hWnd, SPAWN_TIMER, SPAWN_RATE * 1000, NULL);       // Ball spawn timer
    std::srand((unsigned int)std::time(nullptr));
}

VOID TrackMouse(HWND hWnd)
{
    TRACKMOUSEEVENT tme;
    tme.cbSize = sizeof(TRACKMOUSEEVENT);
    tme.dwFlags = TME_LEAVE; //Type of events to track & trigger.
    tme.hwndTrack = hWnd;
    tme.dwHoverTime = 1;
    TrackMouseEvent(&tme);
    MouseTracking = true;

    SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA); // Remove transparency 
    KillTimer(hWnd, 7);
}