using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UIEngine.API;

namespace UIEngine.Helper
{
    public static class Funct
    {
        /// <summary>
        /// Проверка активного окна
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <returns></returns>
        public static bool IsActiveWindow(string ProcessName) => Process.GetProcessesByName(ProcessName)[0].MainWindowHandle == UserAPI.GetForegroundWindow() ? true : false;

        /// <summary>
        /// Чтение смещений в файле
        /// </summary>
        /// <param name="Section">Секция</param>
        /// <param name="Key">Ключ</param>
        /// <param name="FileInfo">Имя файла</param>
        /// <returns></returns>
        public static int GetReadOffset(string Section, string Key, FileInfo FileInfo)
        {
            StringBuilder Offset = new StringBuilder(255);

            KernelAPI.GetPrivateProfileString(Section, Key, "", Offset, 255, FileInfo.FullName);

            return int.Parse(Offset.ToString());
        }

        /// <summary>
        /// Рандомное число (Используется в основном для анимации ножей)
        /// </summary>
        /// <param name="nMin"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int Min, int Max) => (new Random().Next() % (Max - Min + 1) + Min);
    }
}