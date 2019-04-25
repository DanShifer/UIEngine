namespace UIEngine.Mathematics
{
    public static class Math
    {
        public static double Pow(this double One, dynamic Two)
        {
            dynamic Number = One;

            for (int UINT = 1; UINT < Two; UINT++)
            {
                One *= Number;
            }

            return One;
        }

        public static int DivisionOnNumber(this int Dividend, int Divider, int Repetitions)
        {
            int Number = Dividend;

            for (int Num = 1; Num <= Repetitions; Num++)
            {
                Number /= Divider;
            }

            return Number;
        }

        public static long Factorial(this long Number) => Number == 0 ? 1 : Number * Factorial(Number - 1);

        public static double RadTwoDegrees(dynamic Yaw) => Yaw * (180f / System.Math.PI);

        public static double DegreesTwoRad(dynamic Yaw) => Yaw * (System.Math.PI / 180f);
    }
}