#include "framework.h"
#include "FruitNinjaH.h"
#include <list>
#include <ctime>
#include <map>
#include <string>
#include <fstream>

#define MAX_LOADSTRING 100
#define SMALL_HEIGHT 300
#define SMALL_WIDTH 400
#define MEDIUM_HEIGHT 500
#define MEDIUM_WIDTH 600
#define BIG_WIDTH 800
#define BIG_HEIGHT 600
#define PROGRESS_BAR_HEIGHT 20
#define SQUARE_SIZE 50
#define GAME_DURATION 1          // (seconds)
#define REFRESH_RATE 50           // (Hz)
#define BALL_SIZE_L 60
#define BALL_SIZE_M 30
#define BALL_SIZE_S 12
#define SMALL 0
#define MEDIUM 1
#define BIG 2
#define TRANSPARENCY_TIMER 7
#define REFRESH_TIMER 9
#define SPAWN_TIMER 10
#define SPAWN_RATE 1              // (seconds between ball spawns)
#define ScreenX GetSystemMetrics(SM_CXSCREEN)
#define ScreenY GetSystemMetrics(SM_CYSCREEN)

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

struct Ball_t {
    POINT position;
    INT dx;
    double dy;
    COLORREF color;
    INT size;
};

std::list<Ball_t> balls;                        // list of balls
INT nWindowWidth;                               // main windows height
INT nWindowHeight;                              // main window width
POINT WindowPos;                                // main window position        
INT nBoardHeight;                               // board height
INT nBoardWidth;                                // board width 
INT nBoardSize;                                 // board size
INT nGameScore;                                 // game score
INT nGameTicks = 0;                             // game ticks
BOOL MouseTracking = FALSE;
BOOL GameRunning = FALSE;
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

ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, INT);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);

VOID InitBoardDimensions();
VOID SetWindowPosition();
VOID DrawBoard(HWND hWnd, HDC offDC);
VOID DrawScore(HWND hWnd, HDC offDC);
VOID DrawProgressBar(HWND hWnd, HDC offDC);
VOID DrawBalls(HWND hWnd, HDC offDC);
VOID DrawEndScreen(HWND hWnd, HDC hdc, HDC offDC);
VOID DrawEndScore(HWND hWnd, HDC offDC);
VOID StartNewGame(HWND hWnd);
DWORD CheckItem(UINT hItem, HMENU hmenu);
VOID InitializeGame(HWND hWnd);
VOID ChangeBoardSize(HWND hWnd, INT wmId);
VOID SpawnBall(INT ballSize, POINT pos, COLORREF color);
VOID TrackMouse(HWND hWnd);
VOID EndGame(HWND hWnd);
VOID SaveSizeToFile();
VOID CheckCollisions(HWND hWnd);

INT APIENTRY wWinMain(_In_ HINSTANCE hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR    lpCmdLine,
    _In_ INT       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_FRUITNINJAH, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

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

BOOL InitInstance(HINSTANCE hInstance, INT nCmdShow)
{
    hInst = hInstance; // Store instance handle in our global variable

    InitBoardDimensions();
    SetWindowPosition();

    HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPED | WS_SYSMENU,
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
            
            InitializeGame(hWnd);
            StartNewGame(hWnd);
        } break;
        case WM_TIMER:
        {
            if (wParam == REFRESH_TIMER)                                // refresh timer
            {
                RECT rc;
                GetClientRect(hWnd, &rc);
                InvalidateRect(hWnd, &rc, FALSE);

                if (++nGameTicks == GAME_DURATION * REFRESH_RATE)
                    EndGame(hWnd);
            }
            else if (wParam == TRANSPARENCY_TIMER)                  // transparency timer (TODO: perhaps add second timer to smooth it)
            {
                SetLayeredWindowAttributes(hWnd, 0, (255 * 20) / 100, LWA_ALPHA);
            }
            else if (wParam == SPAWN_TIMER)                         // ball spawn timer
            {
                SpawnBall(BALL_SIZE_L, POINT{ 0, 0 }, RGB(0, 0, 0));
            }
        } break;
        case WM_NCMOUSEMOVE:
        case WM_MOUSEMOVE:
        {
            CheckCollisions(hWnd); 
            
            if (!MouseTracking)
                TrackMouse(hWnd);
        } break;
        case WM_MOUSELEAVE:
        {
            SetTimer(hWnd, 7, 3000, NULL);                       // Reset transparancy timer
            MouseTracking = false;
        } break;
        case WM_WINDOWPOSCHANGING: // Prevent repositioning
        {
            INT x = (ScreenX - nWindowWidth) / 2;
            INT y = (ScreenY - nWindowHeight) / 2;
            ((WINDOWPOS*)lParam)->x = x;
            ((WINDOWPOS*)lParam)->y = y;
        } break;    
        case WM_COMMAND:
        {
            INT wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
                case ID_GAME_NEWGAME:
                {
                    ChangeBoardSize(hWnd, nBoardSize);
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
            
            DrawBoard(hWnd, offDC);
            DrawScore(hWnd, offDC);
            DrawBalls(hWnd, offDC);
            DrawProgressBar(hWnd, offDC);
            BitBlt(hdc, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT, offDC, 0, 0, SRCCOPY);
            
            if (!GameRunning)
            {
                DrawEndScore(hWnd, offDC);
                BitBlt(hdc, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT, offDC, 0, 0, SRCCOPY);
                DrawEndScreen(hWnd, hdc, offDC);
            }

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

            SaveSizeToFile();
            PostQuitMessage(0);
        } break;    
        default:
            return DefWindowProc(hWnd, message, wParam, lParam);
    }

    return 0;
}

VOID DrawBoard(HWND hWnd, HDC offDC)
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
VOID DrawScore(HWND hWnd, HDC offDC)
{
    COLORREF oldcolor = SetTextColor(offDC, RGB(255, 0, 0));
    HFONT font = CreateFont(
                -MulDiv(24, GetDeviceCaps(offDC, LOGPIXELSY), 72),  // Height
                0,                                                  // Width
                0,                                                  // Escapament
                0,                                                  // Orientation
                FW_BOLD,                                            // Weight
                FALSE,                                              // Italic 
                FALSE,                                              // Underline
                FALSE,                                              // StrikeOut
                EASTEUROPE_CHARSET,                                 // CharSet
                OUT_DEFAULT_PRECIS,                                 // OutPrecision
                CLIP_DEFAULT_PRECIS,                                // ClipPrecision
                DEFAULT_QUALITY,                                    // Quality
                DEFAULT_PITCH | FF_SWISS,                           // PitchAndFamily
                _T("Verdana"));                                     // Facename
            
    HFONT oldfont = (HFONT)SelectObject(offDC, font);

    TCHAR s[256];
    _stprintf_s(s, 256, _T("%d"), nGameScore);
    
    RECT rc;
    GetClientRect(hWnd, &rc);
    rc.top += 5;
    rc.right -= 3;

    SetBkMode(offDC, TRANSPARENT);
    DrawText(offDC, s, (int)_tcslen(s), &rc, DT_TOP | DT_RIGHT);    
    SetBkMode(offDC, OPAQUE);

    SetTextColor(offDC, oldcolor);
    SelectObject(offDC, oldfont);
    DeleteObject(font);
}
VOID DrawProgressBar(HWND hWnd, HDC offDC)
{
    HBRUSH wbrush = (HBRUSH)GetStockObject(WHITE_BRUSH);
    HBRUSH gbrush = CreateSolidBrush(RGB(11, 25, 220));    
    HPEN wpen = (HPEN)GetStockObject(WHITE_PEN);
    HPEN gpen = CreatePen(PS_SOLID, 1, RGB(11, 25, 110));
    
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
VOID DrawBalls(HWND hWnd, HDC offDC)
{
    //iterate through the ball list
    std::list<Ball_t>::iterator it = balls.begin();
    while(it != balls.end())
    {
        // remove balls that fall below the bottom
        if ((*it).position.y > nBoardHeight + 50)
        {
            it = balls.erase(it);
            continue;
        }

        HBRUSH bbrush = CreateSolidBrush(it->color);
        HPEN bpen = CreatePen(PS_SOLID, 1, it->color);

        HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, bbrush);
        HPEN oldpen = (HPEN)SelectObject(offDC, bpen);
        
        // update position of ball
        it->position.x += it->dx;
        it->position.y = (INT)(it->position.y + it->dy);
        
        // draw ball
        Ellipse(offDC
            /*left*/,   it->position.x                     
            /*top*/,    it->position.y                     
            /*right*/,  it->position.x + it->size         
            /*bottom*/, it->position.y + it->size         
        );

        // gravity
        it->dy += 0.15;

        SelectObject(offDC, oldbrush);
        SelectObject(offDC, oldpen);
        DeleteObject(bbrush);
        DeleteObject(bpen);

        it++;
    }
}
VOID DrawEndScreen(HWND hWnd, HDC hdc, HDC offDC)
{
    HBRUSH gbrush = CreateSolidBrush(RGB(0, 0, 255));
    HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, gbrush);

    BLENDFUNCTION bf;
    bf.AlphaFormat = 0;
    bf.BlendOp = AC_SRC_OVER;
    bf.SourceConstantAlpha = 130;
    bf.BlendFlags = 0;

    Rectangle(offDC, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT);
    GdiAlphaBlend(hdc, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT, offDC, 0, 0, nBoardWidth, nBoardHeight + PROGRESS_BAR_HEIGHT, bf);

    SelectObject(offDC, oldbrush);
    DeleteObject(gbrush);

    SetLayeredWindowAttributes(hWnd, 0, (255 * 20) / 100, LWA_ALPHA);
}
VOID DrawEndScore(HWND hWnd, HDC offDC)
{
    COLORREF oldcolor = SetTextColor(offDC, RGB(255, 0, 0));
    INT fontsize = nBoardSize == SMALL ? 8 : nBoardSize == MEDIUM ? 12 : 16;
    
    HFONT font = CreateFont(
        -MulDiv(fontsize, GetDeviceCaps(offDC, LOGPIXELSY), 36),    // Height
        0,                                                  // Width
        0,                                                  // Escapament
        0,                                                  // Orientation
        FW_BOLD,                                            // Weight
        FALSE,                                              // Italic 
        FALSE,                                              // Underline
        FALSE,                                              // StrikeOut
        EASTEUROPE_CHARSET,                                 // CharSet
        OUT_DEFAULT_PRECIS,                                 // OutPrecision
        CLIP_DEFAULT_PRECIS,                                // ClipPrecision
        DEFAULT_QUALITY,                                    // Quality
        DEFAULT_PITCH | FF_SWISS,                           // PitchAndFamily
        _T("Verdana"));                                     // Facename

    HFONT oldfont = (HFONT)SelectObject(offDC, font);

    TCHAR s[256];
    _stprintf_s(s, 256, _T("YOU SCORED %d POINTS!"), nGameScore);

    RECT rc;
    GetClientRect(hWnd, &rc);
    rc.top -= 100;

    SetBkMode(offDC, TRANSPARENT);

    DrawText(offDC, s, (int)_tcslen(s), &rc, DT_SINGLELINE | DT_CENTER | DT_VCENTER);

    SetBkMode(offDC, OPAQUE);

    SetTextColor(offDC, oldcolor);
    SelectObject(offDC, oldfont);
    DeleteObject(font);
}
VOID SpawnBall(INT ballSize, POINT pos, COLORREF color)
{
    Ball_t ball;
    
    if (ballSize == BALL_SIZE_L)
    {
        ball.position = { rand() % nBoardWidth, nBoardHeight };
        ball.dx = (ball.position.x < nBoardWidth / 2 ? 1 : -1) * ((rand() % 4) + 1);
        ball.dy = (nBoardSize == SMALL ? -0.7 : -1) * ((double)(rand() % 8) + 7);   
        ball.color = colorSet[rand() % 6];
        ball.size = ballSize;
    }
    else
    {
        ball.position = { pos.x, pos.y };
        ball.dx = (rand() % 3 - 1) * ((rand() % 4) + 1);
        ball.dy = ((double)(rand() % 3) - 1) * ((double)(rand() % 4) + 1);   
        ball.color = color;
        ball.size = ballSize;
    }

    balls.push_back(ball);    
}
VOID CheckCollisions(HWND hWnd)
{
    BOOL spawn = false;
    COLORREF color;
    POINT pos;
    INT size;
    
    POINT cursorPos;
    GetCursorPos(&cursorPos);
    ScreenToClient(hWnd, &cursorPos);

    std::list<Ball_t>::iterator it = balls.begin();
    while (it != balls.end())
    {
        // check if cursor is cutting the ball
        INT r = (*it).size / 2;
        INT dx = (cursorPos.x - (*it).position.x - r);
        INT dy = (cursorPos.y - (*it).position.y - r);
        INT d = (dx * dx) + (dy * dy);

        if (d <= (r * r))
        {
            // update score (1pt for Large balls / 2pt for Medium balls / 4pt for Small balls)
            nGameScore += (*it).size == BALL_SIZE_L ? 1 : (*it).size == BALL_SIZE_M ? 2 : 4;
            
            // new ball properties
            color = (*it).color;
            pos = (*it).position;
            size = (*it).size == BALL_SIZE_L ? BALL_SIZE_M : BALL_SIZE_S;
            
            // if ball was L or M set spawn flag
            if ((*it).size != BALL_SIZE_S) spawn = TRUE;
            
            // erase the ball
            balls.erase(it);
            
            break;
        }

        it++;
    }

    if (spawn)
    {
        //spawn new ball(s)
        INT spawnCount = rand() % 3 + 4;
        
        while (spawnCount--)
            SpawnBall(size, pos, color);
    }

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
    switch (nBoardSize)
    {
        case SMALL:
            CheckItem(ID_BOARD_SMALL, GetMenu(hWnd));
            break;
        case MEDIUM:
            CheckItem(ID_BOARD_MEDIUM, GetMenu(hWnd));
            break;
        case BIG:
            CheckItem(ID_BOARD_BIG, GetMenu(hWnd));
            break;
    }
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
    
    BOOL nofile = TRUE;
    
    std::ifstream infile("FruitNinja.ini", std::ios::beg);
    if(infile.is_open())
    {
        std::string line;
        std::getline(infile, line);        
        
        if (line.compare(std::string{ "[GAME]" }) == 0)
        {
            std::getline(infile, line);

            if (line.compare(0, 5, std::string("SIZE=")) == 0)
            {
                if (line[5] == '2')
                {
                    nBoardHeight = BIG_HEIGHT;
                    nBoardWidth = BIG_WIDTH;
                    nBoardSize = BIG;
                    nofile = FALSE;
                }
                else if (line[5] == '1')
                {
                    nBoardHeight = MEDIUM_HEIGHT;
                    nBoardWidth = MEDIUM_WIDTH;
                    nBoardSize = MEDIUM;
                    nofile = FALSE;
                }
            }
        }
        
        infile.close();
    }
        
    if (nofile)
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
    nGameScore = 0;
    SetTimer(hWnd, TRANSPARENCY_TIMER, 3000, NULL);             // Transparancy timer
    SetTimer(hWnd, REFRESH_TIMER, 1000 / REFRESH_RATE, NULL);   // Game timer - 200 Hz => 5ms => Game over at nGameTicks = 6000    
    SetTimer(hWnd, SPAWN_TIMER, SPAWN_RATE * 1000, NULL);       // Ball spawn timer
    std::srand((unsigned int)std::time(nullptr));               
    GameRunning = TRUE;
}
VOID EndGame(HWND hWnd)
{
    KillTimer(hWnd, SPAWN_TIMER);
    KillTimer(hWnd, REFRESH_TIMER);
    SetLayeredWindowAttributes(hWnd, RGB(0, 255, 0), (255 * 20) / 100, LWA_ALPHA);
    GameRunning = FALSE;
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
VOID SaveSizeToFile() 
{
    std::ofstream outfile("FruitNinja.ini", std::ios::trunc);
    if (outfile.is_open())
    {
        outfile << "[GAME]\n";
        outfile << "SIZE=" << nBoardSize;
        outfile.close();
    }
}
