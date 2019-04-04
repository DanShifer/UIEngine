using System;
using System.Collections.Generic;
using System.Diagnostics;
using UIEngine.API;
using UIEngine.Helper.Define.Variable;
using UIEngine.Helper.Enum;

namespace UIEngine.Memory.Helper
{
    public abstract class HMemory
    {
        #region Params
        /// <summary>
        /// Имя процесса
        /// </summary>
        private string ProcessName;

        /// <summary>
        /// Хандл процесса
        /// </summary>
        protected HANDLE ProcessHandle;

        /// <summary>
        /// Права в процессе
        /// </summary>
        private ProcessAccess ProcessAccess;

        /// <summary>
        /// Коллекция адресов модулей
        /// </summary>
        protected Dictionary<string, int> Modules = null;
        #endregion

        /// <summary>
        /// Получение хандла
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        public HMemory(string ProcessName, ProcessAccess ProcessAccess)
        {
            this.ProcessName = ProcessName;
            this.ProcessAccess = ProcessAccess;

            ProcessHandle = KernelAPI.OpenProcess(ProcessAccess, false, (DWORD)Process.GetProcessesByName(ProcessName)[0].Id);
        }

        /// <summary>
        /// Получение хандла с получением загруженных модулей процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <param name="Modules">Колекция модулей</param>
        public HMemory(string ProcessName, out Dictionary<string, int> Modules, ProcessAccess ProcessAccess)
        {
            this.ProcessName = ProcessName;
            this.ProcessAccess = ProcessAccess;

            ProcessHandle = KernelAPI.OpenProcess(ProcessAccess, false, (DWORD)Process.GetProcessesByName(ProcessName)[0].Id);

            Modules = GetProcessModule();
        }

        #region Process
        /// <summary>
        /// Является ли процесс запущенным
        /// </summary>
        public bool IsActiveProcess => Process.GetProcessesByName(ProcessName).Length > 0 ? true : false;

        /// <summary>
        /// Получение процесса
        /// </summary>
        /// <param name="Index">Индекс ресурса процесса</param>
        /// <returns></returns>
        public Process GetProcess(int Index = 0) => Process.GetProcessesByName(ProcessName)[Index];

        /// <summary>
        /// Получение процесса
        /// </summary>
        /// <param name="ProcessName">Имя  процесса</param>
        /// <param name="Index">Индекс ресурса процесса</param>
        /// <returns></returns>
        public static Process GetProcess(string ProcessName,int Index = 0) => Process.GetProcessesByName(ProcessName)[Index];

        /// <summary>
        /// Получение модулей процесса и добавление их в коллекцию
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <returns></returns>
        public Dictionary<string,int> GetProcessModule()
        {
            if (Modules == null)
            {
                this.Modules = new Dictionary<string, int>();

                foreach (ProcessModule UIModule in Process.GetProcessesByName(ProcessName)[0].Modules)
                {
                    this.Modules.Add(UIModule.ModuleName, (int)UIModule.BaseAddress);
                }

                return this.Modules;
            }
            else
            {
                return this.Modules;
            }
        }

        /// <summary>
        /// Получение адреса модуля
        /// </summary>
        /// <param name="Module">Имя модуля</param>
        /// <returns></returns>
        public int GetProcessModuleAddress(string Module)
        {
            if (this.Modules == null)
            {
                return GetProcessModule()[Module];
            }
            else
            {
                return this.Modules[Module];
            }
        }
        #endregion

        #region Control Memory
        /// <summary>
        /// Чтение байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessSize">Размер процесса</param>
        /// <returns></returns>
        protected byte[] ReadBytes(IntPtr ProcessOffset, DWORD ProcessSize)
        {
            byte[] LpBuffer = new byte[(int)(IntPtr)ProcessSize];
            KernelAPI.ReadProcessMemory(ProcessHandle, ProcessOffset, LpBuffer, ProcessSize, 0);

            return LpBuffer;
        }

        /// <summary>
        /// Запись байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessBytes">Байты процесса</param>
        /// <returns></returns>
        protected bool WriteBytes(IntPtr ProcessOffset, byte[] ProcessBytes) => KernelAPI.WriteProcessMemory(ProcessHandle, ProcessOffset, ProcessBytes, (DWORD)ProcessBytes.Length, 0);
        #endregion
    }
}