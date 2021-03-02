#define WIN32_LEAN_AND_MEAN
#include <windows.h>
int APIENTRY wWinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPWSTR lpCmdLine, int nShowCmd)
{
	//Register Class
	LPCWSTR clsname = L"My Window Class";
	WNDCLASSEXW wcx{ sizeof(WNDCLASSEXW) };
	wcx.lpfnWndProc = DefWindowProcW;
	wcx.lpszClassName = clsname;
	ATOM clsid = RegisterClassExW(&wcx);
	if (!clsid) return -1;

	//Create and Show Window
		HWND hWnd = CreateWindowExW(0,
		clsname/*or clsid*/, L"Hello World!",
		0, 100, 100, 640, 360, nullptr,
		nullptr, nullptr, nullptr);
	if (!hWnd) return -1;
	ShowWindow(hWnd, nShowCmd);

	//Process Messages
	MSG msg{};
	while (GetMessageW(&msg, nullptr, 0, 0) > 0) 
	{
		DispatchMessageW(&msg);
	}

	//Destroy Window and Exit
	//Note: Currently unreachable!
	DestroyWindow(hWnd);
	return 0;
}