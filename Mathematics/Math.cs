namespace UIEngine.Mathematics
{
    public static class Math
    {
        public static T Add<T>(this T One, dynamic Two) => (T)(One + Two);

        public static T Subtract<T>(this T One, dynamic Two) => (T)(One - Two);

        public static T Division<T>(this T One, dynamic Two) => (T)(One / Two);

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
    }
}