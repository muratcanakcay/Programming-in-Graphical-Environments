// Part 2 - Drawing.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Part 2 - Drawing.h"
#define MAX_LOADSTRING 100

// These are added during the implementation of the lab task
# include <list>
using namespace std;
static list <POINT> pointsList;

static HDC offDC = NULL;
static HBITMAP offOldBitmap = NULL;
static HBITMAP offBitmap = NULL;

// ----------------------------------------------------------



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
    //    //· GDI always uses current objects(one pen, one brush, one font, etc.) to draw, the object can be
    //    //selected as current on the device context(HDC) using the SelectObject function.
    //    //· The colour contains 3 values: for the red, green, and blue.The RGB macro can be used to set
    //    //specic colour, its parameters are integer values from the range[0..255])
    //    //· VERY IMPORTANT : All created GDI objects must be destroyed.The pen created using the
    //    //CreatePen function must be destroyed using the DeleteObject.
    //    //· VERY IMPORTANT : Objects selected as current on the device context cannot be destroyed(so
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
    //------------------------------------------------------------------------------------------------------------------
    
    
    
    
    
    
    /* USING FONTS */
    
    //case WM_PAINT:
    //{
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    TCHAR s[] = _T("Hello World!");
    //    HFONT font = CreateFont(
    //        -MulDiv(24, GetDeviceCaps(hdc, LOGPIXELSY), 72),    // height
    //        0,                                                  // Width
    //        0,                                                  // Escapament
    //        0,                                                  // Orientation
    //        FW_BOLD,                                            // Weight
    //        true,                                              // Italic (false == FALSE  == 0)
    //        FALSE,                                              // Underline
    //        0,                                                  // StrikeOut
    //        EASTEUROPE_CHARSET,                                 // CharSet
    //        OUT_DEFAULT_PRECIS,                                 // OutPrecision
    //        CLIP_DEFAULT_PRECIS,                                // ClipPrecision
    //        DEFAULT_QUALITY,                                    // Quality
    //        DEFAULT_PITCH | FF_SWISS,                           // PitchAndFamily
    //        _T("Verdana"));                                     // Facename
    //    
    //    HFONT oldfont = (HFONT)SelectObject(hdc, font);
    //    RECT rc;
    //    GetClientRect(hWnd, &rc);
    //    
    //    DrawText(hdc, s, (int)_tcslen(s), &rc, DT_CENTER | DT_VCENTER | DT_SINGLELINE);

    //    SelectObject(hdc, oldfont);
    //    DeleteObject(font);
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    //------------------------------------------------------------------------------------------------------------------



    /* USING BITMAPS */
    // Add a bitmap to resources (right click on the project in the Solution Explorer window in Visual
    // Studio, Add / Resource, and choose Bitmap in the dialog box)
    // * Draw something on the bitmap (there is a simple image editor built in Visual Studio)
    // * Save the bitmap(note, that a.bmp file is created)
    // * Check an identier of the bitmap in the Resource View window(it should be IDB_BITMAP1 by default)
    
    // · There is no function for drawing a bitmap on a device context, another device context must be  used
    // · To create a memory device context use the CreateCompatibleDC function.Each created
    // device context must be destroyed using the DeleteDC function
    // · The BitBlt function copies a bitmap from one device context to another one, the StretchBlt
    // function allows to resize the bitmap
    
    //case WM_PAINT:
    //{
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    TCHAR s[] = _T("Hello World!");
    //    
    //    HBITMAP bitmap = LoadBitmap(hInst, MAKEINTRESOURCE(IDB_BITMAP1));
    //    HDC memDC = CreateCompatibleDC(hdc);
    //    HBITMAP oldbitmap = (HBITMAP)SelectObject(memDC, bitmap);
    //    BitBlt(hdc, 0, 0, 48, 48, memDC, 0, 0, SRCCOPY);
    //    StretchBlt(hdc, 200, 100, 200, 100, memDC, 0, 0, 48, 48, SRCCOPY);
    //    SelectObject(hdc, oldbitmap);
    //    DeleteObject(bitmap);
    //    DeleteDC(memDC);
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    //------------------------------------------------------------------------------------------------------------------




    /* DRAWING IN RESPONSE TO OTHER MESSAGES THAN MS_PAINT */
    // * The BeginPaint() and EndPaint() functions can be used only in response for the WM_PAINT !!!!!
    // In all other cases the GetDC() and ReleaseDC() functions must be used !!!!!
    // * When the window is refreshed (e.g.after changing its size), only the code for the WM_PAINT
    // message is called, this is the reason why the content is cleared
    
    //case WM_LBUTTONDOWN:
    //{
    //    HDC hdc = GetDC(hWnd);
    //    HBRUSH brush = CreateSolidBrush(RGB(0, 0, 200));
    //    HBRUSH oldbrush = (HBRUSH)SelectObject(hdc, brush);

    //    short x = (short)LOWORD(lParam);
    //    short y = (short)HIWORD(lParam);
    //    const int rad = 50;

    //    Ellipse(hdc, x - rad, y - rad, x + rad, y + rad);
    //    SelectObject(hdc, oldbrush);
    //    DeleteObject(brush);
    //    ReleaseDC(hWnd, hdc);
    //}
    //break;
    //------------------------------------------------------------------------------------------------------------------






    /* INVALIDATING AND UPDATING THE WINDOW */
    //  A better solution of previous example(with storing clicked points in the list)
    //  The following are added to the top of this file. The static pointsList will be used to store the clicked points.
    //-----  #include <list>
    //-----  using namespace std;
    //-----  static list <POINT> pointsList;

    //case WM_LBUTTONDOWN:
    //{
    //    POINT pt;
    //    pt.x = (short)LOWORD(lParam);
    //    pt.y = (short)HIWORD(lParam);
    //    pointsList.push_back(pt);
    //    InvalidateRect(hWnd, NULL, TRUE);
    //    //  *The InvalidateRect function sets the specied rectangle(or full client area when the NULL value
    //    //  is used) as a region that must be redrawnand inserts the WM_PAINT message to the message queue
    //    //  * To force the window to redraw as soon as possible, call the UpdateWindow functions immediately
    //    //  after calling the InvalidateRect function
    //}
    //break;
    
    //case WM_PAINT:
    //{
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    HBRUSH brush = CreateSolidBrush(RGB(0, 0, 160));
    //    HBRUSH oldbrush = (HBRUSH)SelectObject(hdc, brush); // QUESTION: when "&brush" is passed instead of "brush", the circles are not filled. why????
    //    
    //    list<POINT>::const_iterator iter = pointsList.begin();
    //    while (iter != pointsList.end())
    //    {
    //        POINT pt = *iter;
    //        const int rad = 50;
    //        Ellipse(hdc, pt.x - rad, pt.y - rad, pt.x + rad, pt.y + rad);
    //        iter++;
    //    }

    //    SelectObject(hdc, oldbrush);
    //    DeleteObject(brush);
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    //------------------------------------------------------------------------------------------------------------------



    /* FLICKER FREE DRAWING */
    // The following implementation flickers when resizing window (may be not noticable)
    //case WM_PAINT:
    //{
    //    PAINTSTRUCT ps;
    //    HDC hdc = BeginPaint(hWnd, &ps);
    //    RECT rc;
    //    GetClientRect(hWnd, &rc);
    //    HBRUSH oldbrush = (HBRUSH )SelectObject(hdc, (HBRUSH)GetStockObject(GRAY_BRUSH));
    //    
    //    Rectangle(hdc, 0, 0, rc.right, rc.bottom);
    //    SelectObject(hdc, (HBRUSH)GetStockObject(BLACK_BRUSH));
    //    const int margin = 50;
    //    Rectangle(hdc, margin, margin, rc.right - margin, rc.bottom - margin);
    //    SelectObject(hdc, oldbrush);
    //    EndPaint(hWnd, &ps);
    //}
    //break;
    
    // There are two reasons of flckering, the rst one is the default background of the window.To disable
    // drawing the background, set the NULL value for the background brush or use the WM_ERASEBKGND message :
    //case WM_ERASEBKGND:    
    //    return 1;

    // The second reason of flickering is drawing figures one after another(firstly the grey rectangle is drawn
    // and secondly the black one).For two rectangle it is easy to modify the code to avoid such covering, but
    // in general the only way to avoid flickering is off screen drawing(i.e.drawing using a memory bitmap) :

    // these static variables are added to the top of the page: 
    // static HDC offDC = NULL;
    // static HBITMAP offOldBitmap = NULL;
    // static HBITMAP offBitmap = NULL;

    case WM_CREATE:
    {
        HDC hdc = GetDC(hWnd);
        offDC = CreateCompatibleDC(hdc);
        ReleaseDC(hWnd, hdc);
    }
    break;

    case WM_SIZE:
    {
        int clientWidth = LOWORD(lParam);
        int clientHeight = HIWORD(lParam);
        HDC hdc = GetDC(hWnd);
        if (offOldBitmap != NULL)
            SelectObject(offDC, offOldBitmap);
        if (offBitmap != -NULL)
            DeleteObject(offBitmap);

        offBitmap = CreateCompatibleBitmap(hdc, clientWidth, clientHeight);
        offOldBitmap = (HBITMAP)SelectObject(offDC, offBitmap);
        ReleaseDC(hWnd, hdc);
    }
    break;

    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hWnd, &ps);
        RECT rc;
        GetClientRect(hWnd, &rc);
        HBRUSH oldbrush = (HBRUSH)SelectObject(offDC, (HBRUSH)GetStockObject(GRAY_BRUSH));
        Rectangle(offDC, 0, 0, rc.right, rc.bottom);
        SelectObject(offDC, (HBRUSH)GetStockObject(BLACK_BRUSH));
        const int margin = 50;
        Rectangle(offDC, margin, margin, rc.right - margin, rc.bottom - margin);
        SelectObject(offDC, oldbrush);
        BitBlt(hdc, 0, 0, rc.right, rc.bottom, offDC, 0, 0, SRCCOPY);
        EndPaint(hWnd, &ps);
    }
    break;

    case WM_ERASEBKGND:
        return 1;

    case WM_DESTROY: // you must comment out the WM_DESTROY definition below to avoid duplicate!!
    {
        if (offOldBitmap != NULL)
            SelectObject(offDC, offOldBitmap);
        if(offDC != NULL)
            DeleteDC(offDC);
        if (offBitmap != NULL)
            DeleteObject(offBitmap);

        PostQuitMessage(0);
        break;
    }
    
    //  *Explanations:
    //    · In response for the WM_CREATE message, a memory device context is created (it has no
    //    dependency on the size of the window, so there is no need to recreate it during responding for
    //    the WM_SIZE message)
    //    · In response for the WM_SIZE message, a memory bitmap is created (so whenever the window
    //    changes its size, the bitmap is recreated - that's why resizing of the window is much slower for
    //    the above code)
    //    (a) If such slow resizing is unacceptable, a bitmap with the biggest possible size (use desktop's
    //    size) should be created in response for the WM_CREATE message
    //    · In the WM_PAINT message everything is painted on the memory device context and at the
    //    end copied to the screen device context using the BitBlt function
    //  * This solution is not perfect - there is no response for changing the image depth of the display(in
    //  bits per pixel).The WM_DISPLAYCHANGE message should be used.
    //------------------------------------------------------------------------------------------------------------------



    // This WM_DESTROY must be uncommented if WM_DESTROY in the last example is commented out.

    //case WM_DESTROY:
    //    PostQuitMessage(0);
    //    break;
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
