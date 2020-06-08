#include <windows.h>
#include <iostream>
#include <process.h>

using namespace std;

void execF();
void execS();
void execT(char str[]);
char* substr(char str[]);

const char wordStr[] = { "C:\\Program Files\\Microsoft Office\\Office16\\WINWORD.EXE" };
const char excelStr[] = { "C:\\Program Files\\Microsoft Office\\Office16\\EXCEL.EXE" };

int main()
{
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 15));
	setlocale(LC_ALL, "Russian");

	char str[100];
	do {
		cout << "Введите команду: ";
		cin.getline(str, 100);

		if (!strcmp(str, "run word")) {
			execF();
		}
		else if (!strcmp(str, "run excel")) {
			execS();
		}
		else if (!strcmp(substr(str), "open")) {
			cout << (str + 5) << endl;
			execT(str);
		}
		else if (!strcmp(str, "exit")) {
			ExitProcess(NO_ERROR);
		}
		else {
			SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 4));
			cout << "Команда не найдена!" << endl;
			SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 15));
		}
	} while (true);
}

void execF() {
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 4));
	switch (WinExec(wordStr, SW_SHOW))
	{
	case 0:
		cout << "ERROR: Системе не хватает памяти или ресурсов." << endl;
		break;
	case ERROR_BAD_FORMAT:
		cout << "ERROR: Исполняемый файл невалидный." << endl;
		break;
	case ERROR_PATH_NOT_FOUND:
		cout << "ERROR: Заданный путь не найден." << endl;
		break;
	case ERROR_FILE_NOT_FOUND:
		cout << "ERROR: Заданный файл не найден." << endl;
		break;
	}
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 15));
}
void execS() {
	STARTUPINFO sinfo;
	PROCESS_INFORMATION pinfo;
	ZeroMemory(&sinfo, sizeof(sinfo));
	if (!CreateProcess(excelStr, NULL, NULL, NULL, FALSE, NULL, NULL, NULL, &sinfo, &pinfo)) {
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 4));
		cout << "ERROR: Ошибка запуска процесса." << endl;
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 15));
	}
	else {
		cout << "ID процесса (_getpid()): " << _getpid() << endl;
		cout << "ID процесса (dwProcessId): " << pinfo.dwProcessId << endl;
		SetPriorityClass(pinfo.hProcess, ABOVE_NORMAL_PRIORITY_CLASS);
		cout << "Версии для котрого предполагается исп.: " << GetProcessVersion(pinfo.dwProcessId) << endl;
		FILETIME time[4];
		SYSTEMTIME systime;
		GetProcessTimes(pinfo.hProcess, &time[0], &time[1], &time[2], &time[3]);
		FileTimeToSystemTime(&time[0], &systime);
		cout << "Время запуска: " << systime.wHour << ":" << systime.wMinute << ":" << systime.wSecond << endl;
	}
}
void execT(char str[]) {
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 4));
	switch ((int)ShellExecute(NULL, "open", (str + 5), NULL, NULL, SW_SHOWNORMAL))
	{
	case ERROR_FILE_NOT_FOUND:
		cout << "ERROR: Файл не найден!" << endl;
		break;
	case ERROR_PATH_NOT_FOUND:
		cout << "ERROR: Путь не найден!" << endl;
		break;
	case ERROR_BAD_FORMAT:
		cout << "ERROR: Ошибка в исполняющемся файле." << endl;
		break;
	}
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (WORD)(0 | 15));
}
char* substr(char str[]) {
	char str2[5];
	for (int i = 0; i < 4; i++) {
		str2[i] = str[i];
	}
	str2[4] = '\0';
	return str2;
}