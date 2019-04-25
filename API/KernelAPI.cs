using System;
using System.Runtime.InteropServices;
using System.Text;
using UIEngine.Helper.Define.Variable;
using UIEngine.Helper.Enum;

namespace UIEngine.API
{
    public class KernelAPI
    {
        /// <summary>
        /// Хандл процесса
        /// </summary>
        /// <param name="dwDesiredAccess">Тип доступа к процессу</param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId">Id процесса</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, BOOLEAN bInheritHandle, DWORD dwProcessId);

        /// <summary>
        /// Закрытие хандла процесса
        /// </summary>
        /// <param name="hHandle">Handle Process</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(HANDLE hHandle);

        /// <summary>
        /// Чтение из памяти
        /// </summary>
        /// <param name="hProcess">Handle процесса</param>
        /// <param name="lpBaseAddress">Адрес функции</param>
        /// <param name="lpBuffer">Буфер считанных байтов</param>
        /// <param name="dwSize">Размер типа функции</param>
        /// <param name="lpNumberOfBytesRead">С какого байта начать чтение</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(HANDLE hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, DWORD dwSize, DWORD lpNumberOfBytesRead);

        /// <summary>
        /// Запись в память
        /// </summary>
        /// <param name="hProcess">Handle процесса</param>
        /// <param name="lpBaseAddress">Адрес функции</param>
        /// <param name="lpBuffer">Буфер байтов для записи</param>
        /// <param name="nSize">Размер типа функции</param>
        /// <param name="lpNumberOfBytesWritten">С какого байта начать запись</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(HANDLE hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, DWORD nSize, DWORD lpNumberOfBytesWritten);

        /// <summary>
        /// Чтение из конфига
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpDefault"></param>
        /// <param name="lpReturnedString">Возвращает значение в этой строке</param>
        /// <param name="nSize">Размер считываемых данных</param>
        /// <param name="lpFileName">Имя файла</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, DWORD nSize, string lpFileName);
    }
}