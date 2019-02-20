using UIEngine.Mathematics.Vector;

namespace UIEngine.Mathematics.Helper.Module
{
    public static class Aim
    {
        /// <summary>
        /// Метод, вычисляющий расстояние до цели в градусах
        /// </summary>
        /// <param name="ViewAngel">Угол обзора локального игрока</param>
        /// <param name="Dst">Противник</param>
        /// <returns></returns>
        public static float GetFov(Vector3 ViewAngel, Vector3 Dst) => (float)System.Math.Sqrt(Math.Pow(Dst.X - ViewAngel.X, 2) + Math.Pow(Dst.Y - ViewAngel.Y, 2));

        /// <summary>
        /// Захват угла
        /// </summary>
        /// <param name="Angles">Угол</param>
        /// <returns></returns>
        public static Vector3 ClampAngle(this Vector3 Angles)
        {
            if (Angles.X >= 89.0f)
            {
                Angles.X = 89f;
            }

            if (Angles.X <= -89.0f)
            {
                Angles.X = -89f;
            }

            if (Angles.Y >= 180.0f)
            {
                Angles.Y = 180f;
            }

            if (Angles.Y <= -180.0f)
            {
                Angles.Y = -180f;
            }

            Angles.Z = 0.0f;

            return Angles;
        }

        /// <summary>
        /// Нормализация угла
        /// </summary>
        /// <param name="Angle">Угол</param>
        /// <returns></returns>
        public static Vector3 NormalizeAngle(this Vector3 Angle)
        {
            while (Angle.X >= 89.0f)
            {
                Angle.X -= 180f;
            }

            while (Angle.X <= -89.0f)
            {
                Angle.X += 180f;
            }

            while (Angle.Y >= 180.0f)
            {
                Angle.Y -= 360f;
            }

            while (Angle.Y <= -180.0f)
            {
                Angle.Y += 360f;
            }

            return Angle;
        }

        /// <summary>
        /// Высчитывание угла
        /// </summary>
        /// <param name="Src">От локального игрока</param>
        /// <param name="Dst">До противника</param>
        /// <returns></returns>
        public static Vector3 CalcAngle(this Vector3 Src, Vector3 Dst) => new Vector3()
        {
            X = (float)(System.Math.Atan((Src - Dst).Z / System.Math.Sqrt((Src - Dst).X * (Src - Dst).X + (Src - Dst).Y * (Src - Dst).Y)) * 57.295779513082f),
            Y = (float)(System.Math.Atan2((Src - Dst).Y, (Src - Dst).X) * 57.295779513082f) + 180.0f,
            Z = 0.0f
        };

        /// <summary>
        /// Плавная доводка до нужного угла
        /// </summary>
        /// <param name="Src">От</param>
        /// <param name="Dst">До</param>
        /// <param name="SmoothAmount">Скорость доводки</param>
        /// <returns></returns>
        public static Vector3 SmoothAngle(Vector3 Src, Vector3 Dst, float SmoothAmount) => (Src + NormalizeAngle(Dst - Src) / 100 * SmoothAmount).ClampAngle();
    }
}