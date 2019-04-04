using System.Runtime.InteropServices;

namespace UIEngine.Memory.Helper
{
    /// <summary>
    /// Управляет данными из неуправляемого блока памяти
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    internal static class Marshal<T>
    {
        public static readonly int Size;

        static Marshal()
        {
            if (typeof(T) == typeof(bool))
            {
                Size = 0x1;
            }
            else if (typeof(T).IsEnum)
            {
                Size = Marshal.SizeOf(typeof(T).GetEnumUnderlyingType());
            }
            else
            {
                Size = Marshal.SizeOf(typeof(T));
            }
        }
    }
}