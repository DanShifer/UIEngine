using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UIEngine.API;
using UIEngine.Helper.Define.Helper;
using UIEngine.Helper.Define.Variable;
using UIEngine.Helper.Enum;
using UIEngine.Memory.Helper;

namespace UIEngine.Memory
{
    public sealed class OldMemory : HMemory
    {
        /// <summary>
        /// Получение хандла
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        public OldMemory(string ProcessName, ProcessAccess ProcessAccess) : base(ProcessName, ProcessAccess) { }

        /// <summary>
        /// Получение хандла с получением загруженных модулей процесса
        /// </summary>
        /// <param name="ProcessName">Имя процесса</param>
        /// <param name="Modules">Колекция модулей</param>
        public OldMemory(string ProcessName, out Dictionary<string, HANDLE> Modules, ProcessAccess ProcessAccess) : base(ProcessName, out Modules, ProcessAccess) { }

        /// <summary>
        /// Дескриптор, закрывающий Хандл
        /// </summary>
        ~OldMemory() => KernelAPI.CloseHandle(ProcessHandle);

        #region Read
        /// <summary>
        /// Конвертирует прочитанные байты в Boolean
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public bool ReadBoolean(HANDLE Address) => BitConverter.ToBoolean(ReadBytes(Address, sizeof(bool)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в BOOLEAN
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public bool ReadBOOLEAN(HANDLE Address) => VariableConverter.ToBOOLEAN(ReadBytes(Address, (uint)Marshal.SizeOf(typeof(BOOLEAN))));

        /// <summary>
        /// Конвертирует прочитанные байты в Double
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public double ReadDouble(HANDLE Address) => BitConverter.ToDouble(ReadBytes(Address, sizeof(double)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в Single
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public float ReadFloat(HANDLE Address) => BitConverter.ToSingle(ReadBytes(Address, sizeof(float)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в Int16
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public short ReadShort(HANDLE Address) => BitConverter.ToInt16(ReadBytes(Address, sizeof(short)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в Int32
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public int ReadInteger32(HANDLE Address) => BitConverter.ToInt32(ReadBytes(Address, sizeof(int)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в DWORD
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public DWORD ReadDWORD(HANDLE Address) => VariableConverter.ToDWORD(ReadBytes(Address, (uint)Marshal.SizeOf(typeof(DWORD))));

        /// <summary>
        /// Конвертирует прочитанные байты в Int64
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public long ReadLong(HANDLE Address) => BitConverter.ToInt64(ReadBytes(Address, sizeof(long)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в UInt32
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public uint ReadUint(HANDLE Address) => BitConverter.ToUInt32(ReadBytes(Address, sizeof(uint)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в UInt16
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public ushort ReadUShort(HANDLE Address) => BitConverter.ToUInt16(ReadBytes(Address, sizeof(ushort)), 0);

        /// <summary>
        /// Конвертирует прочитанные байты в UInt64
        /// </summary>
        /// <param name="Address">Адрес функции</param>
        /// <returns></returns>
        public ulong ReadULong(HANDLE Address) => BitConverter.ToUInt64(ReadBytes(Address, sizeof(ulong)), 0);
        #endregion

        #region Write
        /// <summary>
        /// Записывает конвертированное число
        /// </summary>
        /// <param name="Address">Адресс функции</param>
        /// <param name="Value">Значение</param>
        /// <returns></returns>
        public bool WriteInteger(HANDLE Address, int Value) => WriteBytes(Address, BitConverter.GetBytes(Value));

        /// <summary>
        /// Записывает конвертированное число
        /// </summary>
        /// <param name="Address">Адресс функции</param>
        /// <param name="Value">Значение</param>
        /// <returns></returns>
        public bool WriteDWORD(HANDLE Address, DWORD Value) => WriteBytes(Address, BitConverter.GetBytes(Value));

        /// <summary>
        /// Записывает конвертированное число
        /// </summary>
        /// <param name="Address">Адресс функции</param>
        /// <param name="Value">Значение</param>
        /// <returns></returns>
        public bool WriteShort(HANDLE Address, Int16 Value) => WriteBytes(Address, BitConverter.GetBytes(Value));
        #endregion
    }
}