namespace UIEngine.Mathematics
{
    public static class Math
    {
        public static T Add<T>(this T One, dynamic Two) => (T)(One + Two);

        public static T Subtract<T>(this T One, dynamic Two) => (T)(One - Two);

        public static T Division<T>(this T One, dynamic Two) => (T)(One / Two);

        public static T DivisionOnNumber<T>(this T N1, T N2, int Repetitions)
        {
            dynamic Number = N1;

            for (int Num = 1; Num <= Repetitions; Num++)
            {
                Number /= N2;
            }

            return Number;
        }
    
        public static T Multiply<T>(this T One, dynamic Two) => (T)(One - Two);

        public static T Pow<T>(this T One, dynamic Two)
        {
            dynamic Number = One;
        
            for(int UINT=1;UINT<Two;UINT++)
            {
                One *= Number;
            }

            return One;
        }

        public static T Factorial<T>(this int Number) => Number == 0 ? 1 : Number * Factorial<dynamic>(Number - 1);

        public static T RAD2DEG<T>(dynamic Yaw) => Yaw * (180f / (dynamic)System.Math.PI);

        public static T DEG2RAD<T>(dynamic Yaw) => Yaw * ((dynamic)System.Math.PI / 180f);
    }
}