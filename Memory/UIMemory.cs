using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UIEngine.API;
using UIEngine.Helper.Enum;
using UIEngine.Helper.Interface;
using UIEngine.Memory.Helper;

namespace UIEngine.Memory
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    public class UIMemory :HMemory, IMemory
    {
        /// <summary>
        /// Получение хандла
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        public UIMemory(string ProcessName, ProcessAccess ProcessAccess) : base(ProcessName, ProcessAccess) { }

        /// <summary>
        /// Получение хандла с получением загруженных модулей процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <param name="Modules">Колекция модулей</param>
        public UIMemory(string ProcessName, ref Dictionary<string, int> Modules, ProcessAccess ProcessAccess) : base(ProcessName, ref Modules, ProcessAccess) { }

        /// <summary>
        /// Дескриптор, закрывающий Хандл
        /// </summary>
        ~UIMemory() => KernelAPI.CloseHandle(ProcessHandle);

        /// <summary>
        /// Читает из процесса значение по определенному адресу
        /// </summary>
        /// <typeparam name="T">Тип значения, которое надо прочитать</typeparam>
        /// <param name="Address">Адрес для чтения</param>
        /// <returns></returns>
        public virtual unsafe T Read<T>(int Address, uint Size = 256, string Module = null)
        {
            if (typeof(T) == typeof(string))
            {
                return Module == null ? ReadBytes((IntPtr)Address, Size) : (dynamic)Encoding.UTF8.GetString(ReadBytes((IntPtr)Modules[Module] + Address, Size));
            }
            else
            {
                fixed (byte* Byte = Module == null ? ReadBytes((IntPtr)Address, (uint)Marshal<T>.Size) : ReadBytes((IntPtr)Modules[Module] + Address, (uint)Marshal<T>.Size))
                {
                    return Marshal.PtrToStructure<T>((IntPtr)Byte);
                }
            }
        }

        /// <summary>
        /// Записывает в память значение по определенному адресу
        /// </summary>
        /// <typeparam name="T">Тип значения (необязательно)</typeparam>
        /// <param name="Address">Адрес для записи</param>
        /// <param name="Value">Само значение</param>
        public virtual unsafe void Write<T>(int Address, T Value, string Module = null)
        {
            byte[] Buffer;

            if (typeof(T) == typeof(string))
            {
                Buffer = Encoding.UTF8.GetBytes(Value.ToString());
            }
            else
            {
                Buffer = new byte[Marshal<T>.Size];

                fixed (byte* Byte = Buffer)
                {
                    Marshal.StructureToPtr(Value, (IntPtr)Byte, true);
                }
            }

            WriteBytes((IntPtr)(Module == null ? Address : Modules[Module] + Address), Buffer);
        }
    }
}