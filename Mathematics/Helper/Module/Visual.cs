using System.Windows.Forms;

using UIEngine.Mathematics.Vector;
using UIEngine.Helper.Define.Variable;

namespace UIEngine.Mathematics.Helper.Module
{
    public class Visual
    {
        public static BOOLEAN WorldToScreen(Vector3 from, Vector3 to, Vector3 Position, Form Overlay, float[] ViewMatrix)
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

        public static Vector2 WorldToScreen(Vector3 Target, float[] ViewMatrix)
        {
            Vector2 WorldToScreenPosition;
            Vector3 To;

            float W = 0.0f;

            To.X = ViewMatrix[0] * Target.X + ViewMatrix[1] * Target.Y + ViewMatrix[2] * Target.Z + ViewMatrix[3];
            To.Y = ViewMatrix[4] * Target.X + ViewMatrix[5] * Target.Y + ViewMatrix[6] * Target.Z + ViewMatrix[7];

            W = ViewMatrix[12] * Target.X + ViewMatrix[13] * Target.Y + ViewMatrix[14] * Target.Z + ViewMatrix[15];

            if (W < 0.01f)
            {
                return new Vector2(0, 0);
            }

            To.X *= (1.0f / W);
            To.Y *= (1.0f / W);

            int Width = Screen.PrimaryScreen.Bounds.Width;
            int Height = Screen.PrimaryScreen.Bounds.Height;

            float X = Width / 2;
            float Y = Height / 2;

            X += 0.5f * To.X * Width + 0.5f;
            Y -= 0.5f * To.Y * Height + 0.5f;

            To.X = X;
            To.Y = Y;

            WorldToScreenPosition.X = To.X;
            WorldToScreenPosition.Y = To.Y;

            return WorldToScreenPosition;
        }
    }
}