#include <windows.h>
#include <iostream>

int APIENTRY wWinMain(_In_ HINSTANCE hInst, _In_opt_ HINSTANCE hPrevInst,
	_In_ LPWSTR lpCmdLine, _In_ int nShowCmd)
{
	MessageBoxW(nullptr, L"Hello World!", L"Hi!", MB_OK);
	return 0;
}