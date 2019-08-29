using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using UIEngine.API;
using UIEngine.Helper.Define.Variable;
using UIEngine.Memory.Helper;

namespace UIEngine.Helper
{
    public static class Funct
    {
        /// <summary>
        /// Рандомное число (Используется в основном для анимации ножей)
        /// </summary>
        /// <param name="nMin"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int Min, int Max) => (new Random().Next() % (Max - Min + 1) + Min);
    }
}