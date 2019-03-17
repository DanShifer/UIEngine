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
        public bool ReadBoolean(int Address) => Convert.ToBoolean(ReadBytes((IntPtr)Address, sizeof(bool)));

        public decimal ReadDecimal(int Address) => Convert.ToDecimal(ReadBytes((IntPtr)Address, sizeof(decimal)));

        public double ReadDouble(int Address) => Convert.ToDouble(ReadBytes((IntPtr)Address, sizeof(double)));

        public float ReadFloat(int Address) => Convert.ToSingle(ReadBytes((IntPtr)Address, sizeof(float)));

        public short ReadShort(int Address) => Convert.ToInt16(ReadBytes((IntPtr)Address, sizeof(short)));

        public int ReadInteger32(int Address) => Convert.ToInt32(ReadBytes((IntPtr)Address, sizeof(int)));

        public long ReadLong(int Address) => Convert.ToInt64(ReadBytes((IntPtr)Address, sizeof(long)));

        public sbyte ReadSByte(int Address) => Convert.ToSByte(ReadBytes((IntPtr)Address, sizeof(sbyte)));

        public uint ReadUint(int Address) => Convert.ToUInt32(ReadBytes((IntPtr)Address, sizeof(uint)));

        public ushort ReadUShort(int Address) => Convert.ToUInt16(ReadBytes((IntPtr)Address, sizeof(ushort)));

        public ulong ReadULong(int Address) => Convert.ToUInt64(ReadBytes((IntPtr)Address, sizeof(ulong)));
        #endregion

        #region Write
        public bool WriteBoolean<T>(int Address,T Value) => WriteBytes((IntPtr)Address,Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteDecimal<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteDouble<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteFloat<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteShort<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteInteger32<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteLong<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteSByte<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteUint<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteUShort<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));

        public bool WriteULong<T>(int Address, T Value) => WriteBytes((IntPtr)Address, Encoding.UTF8.GetBytes(Value.ToString()));
        #endregion
    }
}