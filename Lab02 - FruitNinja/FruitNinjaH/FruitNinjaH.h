#pragma once

#include "resource.h"

#define MAX_LOADSTRING 100
#define SMALL_HEIGHT 300
#define SMALL_WIDTH 400
#define MEDIUM_HEIGHT 500
#define MEDIUM_WIDTH 600
#define BIG_WIDTH 800
#define BIG_HEIGHT 600
#define PROGRESS_BAR_HEIGHT 20
#define SQUARE_SIZE 50
#define GAME_DURATION 30          // (seconds)
#define REFRESH_RATE 50           // (Hz)
#define MAX_TRAIL_SIZE 8          // no. of points  
#define BALL_SIZE_L 60
#define BALL_SIZE_M 30
#define BALL_SIZE_S 12
#define SMALL 0
#define MEDIUM 1
#define BIG 2
#define TRANSPARENCY_TIMER 7
#define REFRESH_TIMER 9
#define SPAWN_TIMER 10
#define TRAIL_RECORD_TIMER 11
#define TRAIL_DELETE_TIMER 12
#define SPAWN_RATE 1              // (seconds between ball spawns)
#define ScreenX GetSystemMetrics(SM_CXSCREEN)
#define ScreenY GetSystemMetrics(SM_CYSCREEN)

VOID InitBoardDimensions();
VOID SetWindowPosition();
VOID DrawBoard(HWND hWnd, HDC offDC);
VOID DrawScore(HWND hWnd, HDC offDC);
VOID DrawProgressBar(HWND hWnd, HDC offDC);
VOID DrawBalls(HWND hWnd, HDC offDC);
VOID DrawEndScreen(HWND hWnd, HDC hdc, HDC offDC);
VOID DrawEndScore(HWND hWnd, HDC offDC);
VOID KeepTrail(HWND hWnd);
VOID DrawTrail(HWND hWnd, HDC offDC);
VOID StartNewGame(HWND hWnd);
DWORD CheckItem(UINT hItem, HMENU hmenu);
VOID InitializeGame(HWND hWnd);
VOID ChangeBoardSize(HWND hWnd, INT wmId);
VOID SpawnBall(INT ballSize, POINT pos, COLORREF color);
VOID TrackMouse(HWND hWnd);
VOID EndGame(HWND hWnd);
VOID SaveSizeToFile();
VOID CheckCollisions(HWND hWnd);
VOID PrepareBitmap(HDC hdc);
POINT GetCursorAcceleration();