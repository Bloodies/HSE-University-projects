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
        //Создание майлслота
        [DllImport("kernel32.dll")]
        public static extern int CreateMailslot(string lpName,            //строка, содержащая имя канала Mailslot
                                            int nMaxMessageSize,          //максимальный размер сообщения
                                            int lReadTimeout,             //время ожидания для чтения
                                            int securityAttributes);     //адрес структуры защиты

        [DllImport("kernel32.dll")]
        public static extern bool GetMailslotInfo(int hMailslot,
            int lpMaxMessageSize, ref int lpNextSize, ref int lpMessageCount,
            int lpReadTimeout);

        //Открытие майлслота
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CreateFile(string lpFileName,                                  //строка с именем канала  
                                               Types.EFileAccess dwDesiredAccess,               //режим доступа
                                               Types.EFileShare dwShareMode,                    //режим совместного использования
                                               int lpSecurityAttributes,                        //дескриптор защиты
                                               Types.ECreationDisposition dwCreationDisposition,//параметры создания
                                               int dwFlagsAndAttributes,                        //атрибуты файла
                                               int hTemplateFile);                              //идентификатор файла с атрибутами


        //Запись данных в канал
        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(int hFile,                //описатель реализации канала  
                                     byte[] lpBuffer,                 //адрес буфера, данные из которого будут записаны в канал
                                     uint nNumberOfBytesToWrite,      //размер буфера
                                     ref uint lpNumberOfBytesWritten, //число байт, действительно записанных в канал
                                     int lpOverlapped);               //зависит от режима работы

        //Чтение данных из канала
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(int hFile,                 //описатель реализации канала
                                    byte[] lpBuffer,              //адрес буфера, куда будут прочитаны данные из канала
                                    uint nNumberOfBytesToRead,    //размер буфера
                                    ref uint lpNumberOfBytesRead, //количество действительно прочитанных байт из канала
                                    int lpOverlapped);         //зависит от режима работы

        //Функция, которая проверяет, что данные действительно записались в мейлслот
        [DllImport("kernel32.dll")]
        public static extern byte FlushFileBuffers(int hPipe);

        //Закрытие handle 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(int hObject);
    }
}
