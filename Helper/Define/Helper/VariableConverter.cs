using System;
using UIEngine.Helper.Define.Variable;

namespace UIEngine.Helper.Define.Helper
{
    /// <summary>
    /// Преобразует массив байтов в кастомные типы данных
    /// </summary>
    internal class VariableConverter
    {
        /// <summary>
        /// Конвертирует массив байтов в тип DWORD
        /// </summary>
        /// <param name="Buffer">Массив байтов</param>
        /// <returns></returns>
        public static DWORD ToDWORD(byte[] Buffer) => new DWORD(BitConverter.ToUInt32(Buffer, 0));

        /// <summary>
        /// Конвертирует массив байтов в тип BOOLEAN
        /// </summary>
        /// <param name="Buffer">Массив байтов</param>
        /// <returns></returns>
        public static BOOLEAN ToBOOLEAN(byte[] Buffer) => new BOOLEAN(BitConverter.ToBoolean(Buffer, 0));
    }
}