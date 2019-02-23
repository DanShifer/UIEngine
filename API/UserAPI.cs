using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UIEngine.API
{
    public class UserAPI
    {
        /// <summary>
        /// Cинтезирует движение мыши и щелчки кнопок.
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dwData"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData,int dwExtraInfo);

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