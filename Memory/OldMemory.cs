using System;
using System.Collections.Generic;
using System.Text;
using UIEngine.API;
using UIEngine.Helper.Enum;
using UIEngine.Memory.Helper;

namespace UIEngine.Memory
{
    class OldMemory:HMemory
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
        public OldMemory(string ProcessName, ref Dictionary<string, int> Modules, ProcessAccess ProcessAccess) : base(ProcessName, ref Modules, ProcessAccess) { }

        /// <summary>
        /// Дескриптор, закрывающий Хандл
        /// </summary>
        ~OldMemory() => KernelAPI.CloseHandle(ProcessHandle);

        #region Read
        /// <summary>
        /// Convert Read Byte in Boolean
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public bool ReadBoolean(int Address) => Convert.ToBoolean(ReadBytes((IntPtr)Address, sizeof(bool)));

        /// <summary>
        /// Convert Read Byte in Decimal
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public decimal ReadDecimal(int Address) => Convert.ToDecimal(ReadBytes((IntPtr)Address, sizeof(decimal)));

        /// <summary>
        /// Convert Read Byte in Double
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public double ReadDouble(int Address) => Convert.ToDouble(ReadBytes((IntPtr)Address, sizeof(double)));

        /// <summary>
        /// Convert Read Byte in Single
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public float ReadFloat(int Address) => Convert.ToSingle(ReadBytes((IntPtr)Address, sizeof(float)));

        /// <summary>
        /// Convert Read Byte in Int16
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public short ReadShort(int Address) => Convert.ToInt16(ReadBytes((IntPtr)Address, sizeof(short)));

        /// <summary>
        /// Convert Read Byte in Int32(DWORD)
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public int ReadInteger32(int Address) => Convert.ToInt32(ReadBytes((IntPtr)Address, sizeof(int)));

        /// <summary>
        /// Convert Read Byte in Int64
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public long ReadLong(int Address) => Convert.ToInt64(ReadBytes((IntPtr)Address, sizeof(long)));

        /// <summary>
        /// Convert Read Byte in SByte
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public sbyte ReadSByte(int Address) => Convert.ToSByte(ReadBytes((IntPtr)Address, sizeof(sbyte)));

        /// <summary>
        /// Convert Read Byte in UInt32
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public uint ReadUint(int Address) => Convert.ToUInt32(ReadBytes((IntPtr)Address, sizeof(uint)));

        /// <summary>
        /// Convert Read Byte in UInt16
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public ushort ReadUShort(int Address) => Convert.ToUInt16(ReadBytes((IntPtr)Address, sizeof(ushort)));

        /// <summary>
        /// Convert Read Byte in UInt64
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public ulong ReadULong(int Address) => Convert.ToUInt64(ReadBytes((IntPtr)Address, sizeof(ulong)));
        #endregion

        #region Write
        /// <summary>
        /// Write Type in Function(Address)
        /// </summary>
        /// <param name="Address">Address function</param>
        /// <returns></returns>
        public bool WriteTypeEnum<T>(int Address,T Value) => WriteBytes((IntPtr)Address,Encoding.UTF8.GetBytes(Value.ToString()));
        #endregion
    }
}