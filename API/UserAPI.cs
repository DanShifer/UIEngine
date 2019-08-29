using System;
using System.Runtime.InteropServices;
using UIEngine.Helper.Define.Variable;
using UIEngine.Helper.Enum;

namespace UIEngine.API
{
    public class UserAPI
    {
        /// <summary>
        /// Cинтезирует движение мыши и щелчки кнопок.
        /// </summary>
        /// <param name="dwFlags">Событие мыши</param>
        /// <param name="dx">Координата X</param>
        /// <param name="dy">Координата Y</param>
        /// <param name="dwData"></param>
        /// <param name="dwExtraInfo"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEvent dwFlags, int dx, int dy, DWORD dwData, DWORD dwExtraInfo);

        /// <summary>
        /// Проверка нажатой клавиши
        /// </summary>
        /// <param name="vKey">Клавиша</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(KeysCode vKey);

        /// <summary>
        /// ID Активного окна
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}