using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using UIEngine.API;
using UIEngine.Helper.Enum;
using UIEngine.Helper.Interface;
using UIEngine.Memory.Helper;

namespace UIEngine.Memory
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    public class UIMemory : IMemory
    {
        /// <summary>
        /// Хандл процесса
        /// </summary>
        private readonly IntPtr ProcessHandle;

        /// <summary>
        /// Коллекция адресов модулей
        /// </summary>
        private readonly Dictionary<string, int> Modules;

        /// <summary>
        /// Получение хандла
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        public UIMemory(string ProcessName, ProcessAccess ProcessAccess) => ProcessHandle = KernelAPI.OpenProcess((uint)ProcessAccess, false, Process.GetProcessesByName(ProcessName)[0].Id);

        /// <summary>
        /// Получение хандла с получением загруженных модулей процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <param name="Modules">Колекция модулей</param>
        public UIMemory(string ProcessName, ref Dictionary<string, int> Modules, ProcessAccess ProcessAccess)
        {
            ProcessHandle = KernelAPI.OpenProcess((uint)ProcessAccess, false, Process.GetProcessesByName(ProcessName)[0].Id);
            this.Modules = Modules;

            foreach (ProcessModule UIModule in Process.GetProcessesByName(ProcessName)[0].Modules)
            {
                Modules.Add(UIModule.ModuleName, (int)UIModule.BaseAddress);
            }
        }

        /// <summary>
        /// Дескриптор, закрывающий Хандл
        /// </summary>
        ~UIMemory() => KernelAPI.CloseHandle(ProcessHandle);

        /// <summary>
        /// Чтение байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessSize">Размер процесса</param>
        /// <returns></returns>
        private byte[] ReadBytes(IntPtr ProcessOffset, uint ProcessSize)
        {
            byte[] LpBuffer = new byte[(int)(IntPtr)ProcessSize];
            KernelAPI.ReadProcessMemory(ProcessHandle, ProcessOffset, LpBuffer, ProcessSize, 0U);

            return LpBuffer;
        }

        /// <summary>
        /// Запись байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessBytes">Байты процесса</param>
        /// <returns></returns>
        private bool WriteBytes(IntPtr ProcessOffset, byte[] ProcessBytes) => KernelAPI.WriteProcessMemory(ProcessHandle, ProcessOffset, ProcessBytes, (uint)ProcessBytes.Length, 0U);

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