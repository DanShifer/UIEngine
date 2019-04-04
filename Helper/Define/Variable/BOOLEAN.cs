using System;

namespace UIEngine.Helper.Define.Variable
{
    /// <summary>
    /// Этот тип данных аналогичен bool. Он также имеет два значения – 0(false) или 1(true).
    /// </summary>
    public struct BOOLEAN
    {
        private bool Value;

        public BOOLEAN(bool Value = false) => this.Value = Value;

        public BOOLEAN(int Value = 0) => this.Value = Convert.ToBoolean(Value);

        public static implicit operator BOOLEAN(int Value) => Value > 0 ? new BOOLEAN(true) : new BOOLEAN(false);

        public static implicit operator bool(BOOLEAN BOOLEAN) => BOOLEAN.Value;
    }
}