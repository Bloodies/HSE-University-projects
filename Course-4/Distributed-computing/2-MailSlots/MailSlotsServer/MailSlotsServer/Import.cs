using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;

namespace DIS
{
    class Import
    {
        //�������� ���������
        [DllImport("kernel32.dll")]
        public static extern int CreateMailslot(string lpName,            //������, ���������� ��� ������ Mailslot
                                            int nMaxMessageSize,          //������������ ������ ���������
                                            int lReadTimeout,             //����� �������� ��� ������
                                            int securityAttributes);     //����� ��������� ������

        [DllImport("kernel32.dll")]
        public static extern bool GetMailslotInfo(int hMailslot,
            int lpMaxMessageSize, ref int lpNextSize, ref int lpMessageCount,
            int lpReadTimeout);

        //�������� ���������
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CreateFile(string lpFileName,                                  //������ � ������ ������  
                                               Types.EFileAccess dwDesiredAccess,               //����� �������
                                               Types.EFileShare dwShareMode,                    //����� ����������� �������������
                                               int lpSecurityAttributes,                        //���������� ������
                                               Types.ECreationDisposition dwCreationDisposition,//��������� ��������
                                               int dwFlagsAndAttributes,                        //�������� �����
                                               int hTemplateFile);                              //������������� ����� � ����������


        //������ ������ � �����
        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(int hFile,                //��������� ���������� ������  
                                     byte[] lpBuffer,                 //����� ������, ������ �� �������� ����� �������� � �����
                                     uint nNumberOfBytesToWrite,      //������ ������
                                     ref uint lpNumberOfBytesWritten, //����� ����, ������������� ���������� � �����
                                     int lpOverlapped);               //������� �� ������ ������

        //������ ������ �� ������
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(int hFile,                 //��������� ���������� ������
                                    byte[] lpBuffer,              //����� ������, ���� ����� ��������� ������ �� ������
                                    uint nNumberOfBytesToRead,    //������ ������
                                    ref uint lpNumberOfBytesRead, //���������� ������������� ����������� ���� �� ������
                                    int lpOverlapped);         //������� �� ������ ������

        //�������, ������� ���������, ��� ������ ������������� ���������� � ��������
        [DllImport("kernel32.dll")]
        public static extern byte FlushFileBuffers(int hPipe);

        //�������� handle 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(int hObject);
    }
}
