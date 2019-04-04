using System;
using System.Collections.Generic;
using System.Diagnostics;
using UIEngine.API;
using UIEngine.Helper.Enum;

namespace UIEngine.Memory.Helper
{
    public class HMemory
    {
        /// <summary>
        /// Хандл процесса
        /// </summary>
        protected IntPtr ProcessHandle;

        /// <summary>
        /// Коллекция адресов модулей
        /// </summary>
        protected Dictionary<string, int> Modules;

        /// <summary>
        /// Получение хандла
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        public HMemory(string ProcessName, ProcessAccess ProcessAccess) => ProcessHandle = KernelAPI.OpenProcess((uint)ProcessAccess, false, Process.GetProcessesByName(ProcessName)[0].Id);

        /// <summary>
        /// Получение хандла с получением загруженных модулей процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <param name="Modules">Колекция модулей</param>
        public HMemory(string ProcessName, ref Dictionary<string, int> Modules, ProcessAccess ProcessAccess)
        {
            ProcessHandle = KernelAPI.OpenProcess((uint)ProcessAccess, false, Process.GetProcessesByName(ProcessName)[0].Id);
            this.Modules = Modules;

            foreach (ProcessModule UIModule in Process.GetProcessesByName(ProcessName)[0].Modules)
            {
                Modules.Add(UIModule.ModuleName, (int)UIModule.BaseAddress);
            }
        }      

        /// <summary>
        /// Чтение байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessSize">Размер процесса</param>
        /// <returns></returns>
        protected byte[] ReadBytes(IntPtr ProcessOffset, uint ProcessSize)
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
        protected bool WriteBytes(IntPtr ProcessOffset, byte[] ProcessBytes) => KernelAPI.WriteProcessMemory(ProcessHandle, ProcessOffset, ProcessBytes, (uint)ProcessBytes.Length, 0U);
    }
}