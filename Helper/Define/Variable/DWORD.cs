using System;

namespace UIEngine.Helper.Define.Variable
{
    /// <summary>
    /// 32-битное беззнаковое целое
    /// </summary>
    public struct DWORD
    {
        private UInt32 Value;

        public DWORD(UInt32 Value = 0) => this.Value = Value;

        public static implicit operator DWORD(UInt32 Value) => new DWORD(Value);

        public static explicit operator DWORD(int Value) => new DWORD((UInt32)Value);

        public static implicit operator IntPtr(DWORD DWORD) => (IntPtr)DWORD.Value;

        public static implicit operator UInt32(DWORD DWORD) => DWORD.Value;

        public static implicit operator int(DWORD DWORD) => (int)DWORD.Value;

        public static implicit operator long(DWORD DWORD) => DWORD.Value;

        public static explicit operator short(DWORD DWORD) => (short)DWORD.Value;
    }
}