#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
#include <windows.h>
#include <math.h>
const int N = 15;
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	HWND hMainWnd;
	TCHAR szClassName[] = _T("MyClass");
	MSG msg;
	WNDCLASSEX wc;
	wc.cbSize = sizeof(wc);
	wc.style = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc = WndProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = CreateSolidBrush(RGB(0, 0, 255));
	wc.lpszMenuName = NULL;
	wc.lpszClassName = szClassName;
	wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

	if (!RegisterClassEx(&wc)) {
		MessageBox(NULL, _T("Cannot register class"), _T("Error"), MB_OK);
		return 0;
	}

	hMainWnd = CreateWindow(szClassName, _T("FirstApplication"), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0, 320, 240, NULL, NULL, hInstance, NULL);

	if (!hMainWnd) {
		MessageBox(NULL, _T("Cannot create main window"), _T("Error"), MB_OK);
		return 0;
	}

	ShowWindow(hMainWnd, nCmdShow);

	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}

int mas[N * N][2];
int c = 0;

int search(int a, int b) {
	for (int i = 0; i < c; i++) {
		if (mas[i][0] == a && mas[i][1] == b) return i;
	}
	return -1;
}
void deleteAt(int index) {
	for (int i = index + 1; i <= c; i++) {
		mas[i - 1][0] = mas[i][0];
		mas[i - 1][1] = mas[i][1];
	}
	c--;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	HDC hDC;
	HBRUSH hBrush;
	HPEN hPen;
	PAINTSTRUCT ps;
	RECT rect;
	POINT pt;
	switch (uMsg)
	{
	case WM_PAINT:
	{
		GetClientRect(hWnd, &rect);
		hDC = BeginPaint(hWnd, &ps);
		hPen = CreatePen(PS_SOLID, 1, RGB(255, 0, 0));
		SelectObject(hDC, hPen);

		int startY = rect.bottom / N;
		for (int i = 1; i < N; i++) {
			MoveToEx(hDC, 0, i * startY, &pt);
			LineTo(hDC, rect.right, i * startY);
		}
		int startX = rect.right / N;
		for (int i = 1; i < N; i++) {
			MoveToEx(hDC, i * startX, 0, &pt);
			LineTo(hDC, i * startX, rect.bottom);
		}

		DeleteObject(hPen);
		hBrush = CreateSolidBrush(RGB(255, 250, 250));
		hPen = CreatePen(PS_SOLID, 1, RGB(255, 250, 250));
		SelectObject(hDC, hBrush);
		SelectObject(hDC, hPen);
		int minSide = startX < startY ? startX : startY;
		if (c != 0) {
			for (int i = 0; i < c; i++) {
				//Ellipse(hDC, mas[i][0] * startX + 2, mas[i][1] * startY + 2, (mas[i][0] * startX) + minSide - 2, (mas[i][1] * startY) + minSide - 2);
				Ellipse(hDC, (mas[i][0] * startX) + (startX / 2 - minSide / 2) + 2, mas[i][1] * startY + (startY / 2 - minSide / 2) + 2, (mas[i][0] * startX) + (startX / 2 + minSide / 2) - 2, (mas[i][1] * startY) + (startY / 2 + minSide / 2) - 2);
			}
		}
		EndPaint(hWnd, &ps);
		DeleteObject(hPen);
		DeleteObject(hBrush);
		break;
	}
	case WM_LBUTTONDOWN:
	{
		GetClientRect(hWnd, &rect);
		POINTS pnts = MAKEPOINTS(lParam);
		int a = pnts.x / (rect.right / N);
		int b = pnts.y / (rect.bottom / N);
		int index = search(a, b);
		if (index == -1 && a < N && b < N && c < N * N) {
			mas[c][0] = a;
			mas[c][1] = b;
			c++;
			InvalidateRect(NULL, NULL, TRUE);
		}
		else if (index > -1) {
			deleteAt(index);
			InvalidateRect(NULL, NULL, TRUE);
		}
		break;
	}
	case WM_KEYUP: {
		switch (wParam)
		{
		case VK_ESCAPE:
			DestroyWindow(hWnd);
			return 0;
		case VK_RETURN:
			SetClassLong(hWnd, GCL_HBRBACKGROUND, (LONG)CreateSolidBrush(RGB(rand() % 256, rand() % 256, rand() % 256)));
			InvalidateRect(NULL, NULL, TRUE);
			return 0;
		case 0x51:
			if (GetKeyState(VK_LCONTROL) < 0) {
				DestroyWindow(hWnd);
			}
			return 0;
		case 0X43:
			if (GetKeyState(VK_SHIFT) < 0) {
				STARTUPINFO stInfo;
				PROCESS_INFORMATION procInfo;
				ZeroMemory(&stInfo, sizeof(STARTUPINFO));
				CreateProcess(_T("C:\\Windows\\Notepad.exe"), NULL, NULL, NULL, FALSE, 0, NULL, NULL, &stInfo, &procInfo);
			}
			return 0;
		}
		break;
	}
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
	}
	return 0;
}