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

        public static implicit operator DWORD(int Value) => Value < 0 ? new DWORD() : new DWORD((UInt32)Value);

        public static explicit operator IntPtr(DWORD DWORD) => (IntPtr)DWORD.Value;

        public static implicit operator UInt32(DWORD DWORD) => DWORD.Value;

        public static implicit operator int(DWORD DWORD) => (int)DWORD.Value;

        public static implicit operator long(DWORD DWORD) => DWORD.Value;

        public static explicit operator short(DWORD DWORD) => (short)DWORD.Value;

        public static DWORD operator +(DWORD DWORD, UInt32 Value) => DWORD.Value + Value;

        public static DWORD operator -(DWORD DWORD, UInt32 Value) => DWORD.Value - Value;

        public static DWORD operator *(DWORD DWORD, UInt32 Value) => DWORD.Value * Value;

        public static DWORD operator /(DWORD DWORD, UInt32 Value) => DWORD.Value / Value;

        public static bool operator ==(DWORD DWORD, UInt32 Value) => DWORD.Value == Value;

        public static bool operator !=(DWORD DWORD, UInt32 Value) => DWORD.Value != Value;

        public override bool Equals(Object Object) => Object is DWORD dWORD && Value == dWORD.Value;

        public override int GetHashCode() => -1937169414 + Value.GetHashCode();
    }
}