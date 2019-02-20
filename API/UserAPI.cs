using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UIEngine.API
{
    public class UserAPI
    {
        /// <summary>
        /// Проверка нажатой клавиши
        /// </summary>
        /// <param name="vKey">Клавиша</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Keys vKey);

        /// <summary>
        /// ID Активного окна
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}