using System;

namespace UIEngine.Helper.Define.Variable
{
    /// <summary>
    /// Дескриптор объекта
    /// </summary>
    public struct HANDLE
    {
        private IntPtr Value;

        public HANDLE(IntPtr Value) => this.Value = Value;

        public static implicit operator HANDLE(DWORD Value) => new HANDLE((IntPtr)Value);

        public static implicit operator HANDLE(int Value) => new HANDLE((IntPtr)Value);

        public static implicit operator HANDLE(IntPtr Value) => new HANDLE(Value);

        public static implicit operator IntPtr(HANDLE HANDLE) => HANDLE.Value;

        public static implicit operator int(HANDLE HANDLE) => (int)HANDLE.Value;

        public static unsafe implicit operator HANDLE(byte* Value) => new HANDLE((IntPtr)Value);

        public static HANDLE operator +(HANDLE HANDLE, int Value) => HANDLE.Value + Value;

        public static HANDLE operator -(HANDLE HANDLE, int Value) => HANDLE.Value - Value;
    }
}