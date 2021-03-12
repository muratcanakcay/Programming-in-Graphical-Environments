// FlappyBird.cpp : Defines the entry point for the application.
//

#include "pch.h"
#include "framework.h"
#include "FlappyBird.h"

#define MAX_LOADSTRING 100
#define WINDOW_WIDTH 600
#define WINDOW_HEIGHT 400
#define BALL_SIZE 15
#define WALL_SIZE 10
#define ScreenX GetSystemMetrics(SM_CXSCREEN)
#define ScreenY GetSystemMetrics(SM_CYSCREEN)
#define GAP_HEIGHT 80

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
HWND hWnd;
HWND hWndBall;

struct Wall_t {
	HWND hUp;
	HWND hDown;
	int x;
	bool missed;
};

Wall_t walls[WALL_SIZE];
int wall_count = 0;
int flappyX;
int flappyY;
float flappyVelY = 5;
int points = 0;
const int wallWidth = 30;

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
ATOM				MyRegisterBallClass(HINSTANCE hInstance);
ATOM				MyRegisterWallClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK    WndProc2(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
void				UpdateFlappy();
void				UpdateWalls();
void				updateTitle();
void				AddWall();

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
    LoadStringW(hInstance, IDC_FLAPPYBIRD, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);
	MyRegisterBallClass(hInstance);
	MyRegisterWallClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_FLAPPYBIRD));

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
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRD));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(CreateSolidBrush(RGB(102, 204, 255)));
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_FLAPPYBIRD);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

ATOM MyRegisterBallClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;
	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW; 
	wcex.lpfnWndProc = WndProc2;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRD));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(CreateSolidBrush(RGB(204, 0, 0)));
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDC_FLAPPYBIRD);
	wcex.lpszClassName = L"BallClass";
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));
	return RegisterClassExW(&wcex);
}

ATOM MyRegisterWallClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc2;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRD));
	wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(CreateSolidBrush(RGB(0, 0, 255)));
	wcex.lpszMenuName = MAKEINTRESOURCEW(IDI_FLAPPYBIRD);
	wcex.lpszClassName = L"WallClass";
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
   int x = (ScreenX - WINDOW_WIDTH) / 2;
   int y = (ScreenY - WINDOW_HEIGHT) / 2;

   hWnd = CreateWindowW(szWindowClass, szTitle, WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX,
      x, y, WINDOW_WIDTH, WINDOW_HEIGHT, nullptr, nullptr, hInstance, nullptr);
   SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

   //Set WS_EX_LAYERED on this window (Make window transparent)
   SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED);
   SetLayeredWindowAttributes(hWnd, 0, (255 * 80) / 100, LWA_ALPHA);  //80% alpha
   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   if (!hWnd)
   {
      return FALSE;
   }
   
   hWndBall = CreateWindowW(L"BallClass", L"Ball", WS_CHILD,
	   WINDOW_WIDTH / 5, WINDOW_HEIGHT / 3, BALL_SIZE, BALL_SIZE, hWnd, NULL, hInstance, NULL);
   SetWindowRgn(hWndBall, CreateEllipticRgn(0, 0, BALL_SIZE, BALL_SIZE), true);
   ShowWindow(hWndBall, nCmdShow);
   UpdateWindow(hWndBall);

   for (int i = 0; i < WALL_SIZE; i++)
   {
	   walls[i].hUp = CreateWindow(L"WallClass", L"Wall", WS_CHILD,
		   10, 10, BALL_SIZE, BALL_SIZE, hWnd, NULL, hInstance, NULL);

	   walls[i].hDown = CreateWindow(L"WallClass", L"Wall", WS_CHILD, 
		   10, 10, BALL_SIZE, BALL_SIZE, hWnd, NULL, hInstance, NULL);
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
	RECT rc;
	static HCURSOR cursor = NULL;

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
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            // TODO: Add any drawing code that uses hdc here...
            EndPaint(hWnd, &ps);
        }
        break;

	case WM_CREATE:
		{
			SetTimer(hWnd, 1, 50, NULL);  //20Hz
			//SetTimer(hWnd, 2, 200, NULL);  //5Hz
			GetClientRect(hWnd, &rc);
			flappyX = (rc.right - rc.left) / 4;
			flappyY = (rc.bottom - rc.top) / 2;
			MoveWindow(hWndBall, flappyX, flappyY, BALL_SIZE, BALL_SIZE, TRUE);
			cursor = LoadCursor(NULL, IDC_HAND);
		}
		break;
		
	case WM_TIMER:
		if (wParam == 1) {
			//UpdateAll();
			UpdateFlappy();
			UpdateWalls();
		}
		break;
	case WM_LBUTTONDOWN:
		flappyVelY = 6;
		break;

	case WM_SETCURSOR:
		SetCursor(cursor);
		return TRUE;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

LRESULT CALLBACK WndProc2(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		//case WM_PAINT:
		//{
		//	PAINTSTRUCT ps;
		//	HDC hdc = BeginPaint(hWnd, &ps);

		//	EndPaint(hWnd, &ps);
		//}
		//break;
	case WM_NCHITTEST:
		return HTTRANSPARENT;
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


void AddWall()
{
	RECT rc;
	if (wall_count < WALL_SIZE)
	{
		GetClientRect(hWnd, &rc);
		int windowHeight = rc.bottom - rc.top;
		int windowWidth = rc.right - rc.left;
		int r = rand() % (windowHeight - 20 - GAP_HEIGHT) + 10;

		MoveWindow(walls[wall_count].hUp, windowWidth, 0, wallWidth, r, TRUE);
		MoveWindow(walls[wall_count].hDown, windowWidth, r + GAP_HEIGHT,
			wallWidth, windowHeight - r - GAP_HEIGHT, TRUE);
		walls[wall_count].x = windowWidth;
		walls[wall_count].missed = false;

		ShowWindow(walls[wall_count].hUp, 10);
		UpdateWindow(walls[wall_count].hUp);
		ShowWindow(walls[wall_count].hDown, 10);
		UpdateWindow(walls[wall_count].hDown);
		wall_count++;
	}
}

void ResetAll()
{
	for (int i = 0; i < wall_count; i++)
	{
		ShowWindow(walls[i].hUp, SW_HIDE);
		ShowWindow(walls[i].hDown, SW_HIDE);
		walls[i].missed = true;
	}
	wall_count = 0;
}

void UpdateWalls()
{
	static int counter = 50;
	if (counter-- == 0) {
		AddWall();
		counter = 50;
	}

	RECT rc;
	//GetWindowRect(hMainWnd, &rcMain);
	for (int i = 0; i < wall_count; i++)
	{
		walls[i].x -= 4;
		GetWindowRect(walls[i].hUp, &rc);
		MapWindowPoints(HWND_DESKTOP, GetParent(walls[i].hUp), (LPPOINT)&rc, 2);

		MoveWindow(walls[i].hUp, walls[i].x, rc.top, rc.right - rc.left, rc.bottom - rc.top, TRUE);

		GetWindowRect(walls[i].hDown, &rc);
		MapWindowPoints(HWND_DESKTOP, GetParent(walls[i].hDown), (LPPOINT)&rc, 2);
		MoveWindow(walls[i].hDown, walls[i].x, rc.top, rc.right - rc.left, rc.bottom - rc.top, TRUE);

		//InvalidateRect(walls[i].hUp, NULL, TRUE);
		//InvalidateRect(walls[i].hDown, NULL, TRUE);
		UpdateWindow(walls[i].hUp);
		UpdateWindow(walls[i].hDown);

		if (walls[i].x + wallWidth < flappyX)
		{ //add points
			if (walls[i].missed == false)
			{
				walls[i].missed = true;
				points++;
				updateTitle();
			}
		}
		//remove walls
		if (walls[i].x < -(rc.bottom - rc.top))
		{
			Wall_t tmp = walls[i];
			walls[i] = walls[wall_count - 1];
			walls[wall_count - 1] = tmp;
			ShowWindow(walls[wall_count - 1].hUp, SW_HIDE);
			ShowWindow(walls[wall_count - 1].hDown, SW_HIDE);
			wall_count--;
		}
	}
	InvalidateRect(hWnd, NULL, TRUE);
	//InvalidateRect(hMainWnd, NULL, FALSE);
	//UpdateWindow(hMainWnd);
}

void UpdateFlappy() 
{
	flappyY += -flappyVelY;
	if (flappyVelY > -7) flappyVelY--;
	if (flappyY < 0) {
		flappyY = 0;
		flappyVelY = 0;
	}
	else
		MoveWindow(hWndBall, flappyX, flappyY, BALL_SIZE, BALL_SIZE, TRUE);

	if (flappyY > WINDOW_HEIGHT)
	{
		flappyY = 40;
		points = 0;
		ResetAll();
		updateTitle();
	}
}

void updateTitle()
{
	const int bufSize = 30;
	wchar_t buf[bufSize];
	_stprintf_s(buf, bufSize, _T("FlappyWindow Points: %d"), points);
	SetWindowText(hWnd, buf);
}


//void UpdateAll()
//{
//	UpdateFlappy();
//	UpdateWalls();
//
//	/*if (checkCollisions())
//	{
//		points = 0;
//		flappyY = 40;
//		ResetAll();
//		updateTitle();
//	}*/
//}