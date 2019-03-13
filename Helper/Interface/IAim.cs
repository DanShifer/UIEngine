using UIEngine.Mathematics.Vector;

namespace UIEngine.Helper.Interface
{
    public interface IAim
    {
        /// <summary>
        /// Метод, вычисляющий расстояние до цели в градусах
        /// </summary>
        /// <param name="ViewAngel">Угол обзора локального игрока</param>
        /// <param name="Dst">Противник</param>
        /// <returns></returns>
        float GetFov(Vector3 ViewAngel, Vector3 Dst);

        /// <summary>
        /// Получение игрока
        /// </summary>
        /// <returns></returns>
        int GetPlayer(int Index);

        /// <summary>
        /// Высчитывание угла
        /// </summary>
        /// <param name="Src">От локального игрока</param>
        /// <param name="Dst">До противника</param>
        /// <returns></returns>
        Vector3 CalcAngle(Vector3 Src, Vector3 Dst);
    }
}