﻿using System.Collections.Generic;
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
        protected Dictionary<string, HANDLE> Modules = null;
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
        public HMemory(string ProcessName, out Dictionary<string, HANDLE> Modules, ProcessAccess ProcessAccess)
        {
            this.ProcessName = ProcessName;
            this.ProcessAccess = ProcessAccess;

            ProcessHandle = KernelAPI.OpenProcess(ProcessAccess, false, (DWORD)Process.GetProcessesByName(ProcessName)[0].Id);

            Modules = GetProcessModule();
        }

        #region Process
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
        public static Process GetProcess(string ProcessName, int Index = 0) => Process.GetProcessesByName(ProcessName)[Index];

        /// <summary>
        /// Является ли процесс запущенным
        /// </summary>
        public BOOLEAN IsProcessActive() => Process.GetProcessesByName(ProcessName).Length > 0 ? true : false;

        /// <summary>
        /// Является ли процесс запущенным
        /// </summary>
        public static BOOLEAN IsProcessActive(string ProcessName) => Process.GetProcessesByName(ProcessName).Length > 0 ? true : false;

        /// <summary>
        /// Получение модулей процесса и добавление их в коллекцию
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <returns></returns>
        public Dictionary<string, HANDLE> GetProcessModule()
        {
            if (Modules == null)
            {
                this.Modules = new Dictionary<string, HANDLE>();

                foreach (ProcessModule UIModule in GetProcess().Modules)
                {
                    this.Modules.Add(UIModule.ModuleName, UIModule.BaseAddress);
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
        public HANDLE GetProcessModuleAddress(string Module) => this.Modules == null ? GetProcessModule()[Module] : this.Modules[Module];
        #endregion

        #region Control Memory
        /// <summary>
        /// Чтение байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessSize">Размер процесса</param>
        /// <returns></returns>
        protected byte[] ReadBytes(HANDLE ProcessOffset, DWORD ProcessSize)
        {
            byte[] LpBuffer = new byte[ProcessSize];
            KernelAPI.ReadProcessMemory(ProcessHandle, ProcessOffset, LpBuffer, ProcessSize, 0);

            return LpBuffer;
        }

        /// <summary>
        /// Запись байтов
        /// </summary>
        /// <param name="ProcessOffset">Смещение</param>
        /// <param name="ProcessBytes">Байты процесса</param>
        /// <returns></returns>
        protected bool WriteBytes(HANDLE ProcessOffset, byte[] ProcessBytes) => KernelAPI.WriteProcessMemory(ProcessHandle, ProcessOffset, ProcessBytes, ProcessBytes.Length, 0);
        #endregion
    }
}