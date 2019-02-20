using UIEngine.Mathematics.Vector;
using System.Windows.Forms;

namespace UIEngine.Mathematics.Helper.Module
{
    public class Visual
    {
        public static bool WorldToScreen(Vector3 from, Vector3 to,Vector3 Position, Form Overlay, float[] ViewMatrix)
        {
            to.X = ViewMatrix[0] * from.X + ViewMatrix[1] * from.Y + ViewMatrix[2] * from.Z + ViewMatrix[3];
            to.Y = ViewMatrix[4] * from.X + ViewMatrix[5] * from.Y + ViewMatrix[6] * from.Z + ViewMatrix[7];

            float World = ViewMatrix[12] * from.X + ViewMatrix[13] * from.Y + ViewMatrix[14] * from.Z + ViewMatrix[15];

            if (World < 0.01f)
            {
                return false;
            }

            to.X *= (1.0f / World);
            to.Y *= (1.0f / World);


            float X = Overlay.Size.Width / 2;
            float Y = Overlay.Size.Height / 2;

            X += 0.5f * to.X * Overlay.Size.Width + 0.5f;
            Y -= 0.5f * to.Y * Overlay.Size.Height + 0.5f;

            Position.X = to.X = X;
            Position.Y = to.Y = Y;

            return true;
        }
    }
}