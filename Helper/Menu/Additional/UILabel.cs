using System.Drawing;
using System.Windows.Forms;
using UIEngine.Helper.Enum;

namespace UIEngine.Helper.Menu.Additional
{
    public class UILabel:Label
    {
        #region Params
        private Color CLine = Color.Purple;
        private DockLine DLine = Enum.DockLine.None;
        private int WLine = 1;
        #endregion

        public virtual Color ColorLine
        {
            get => CLine;
            set => CLine = value;
        }

        public virtual DockLine DockLine
        {
            get => DLine;
            set => DLine = value;
        }

        public virtual int WidthLine
        {
            get => WLine;
            set => WLine = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            switch(DockLine)
            {
                case DockLine.Bottom:
                    e.Graphics.DrawLine(new Pen(ColorLine, WidthLine), Right, Right/Left, 0, Right/Left);
                    break;
                case DockLine.Left:
                    e.Graphics.DrawLine(new Pen(ColorLine, WidthLine), 0, Right, 0, 0);
                    break;
                case DockLine.Top:
                    e.Graphics.DrawLine(new Pen(ColorLine, WidthLine), Right, 0, 0, 0);
                    break;
            }
        }
    }
}