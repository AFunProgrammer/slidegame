using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Slide
{
    public class SlideBox : Label
    {
        private int m_iCols = -1;
        private int m_iRows = -1;
        //private int m_iGameSize = -1;
        private int m_iPosition = -1;
        private bool m_bDragEnabled = false;
        private Timer m_DragStart;

        public int Cols { get { return m_iCols; } }
        public int Rows { get { return m_iRows; } }
        public int Position { get { return m_iPosition; } set { m_iPosition = value; } }
        public bool DragEnabled { get { return m_bDragEnabled; } set { m_bDragEnabled = value; } }
        public ref Timer DragStartTimer { get { return ref m_DragStart; } }

        public SlideBox(Control parent, int Cols, int Rows, int Position)
        {
            Parent = parent;
            m_iCols = Cols;
            m_iRows = Rows;
            m_iPosition = Position;
            m_DragStart = null;
        }

        public void ResizeBox()
        {
            (int x, int y) TopLeft;

            Width = Parent.Width / m_iCols;
            Height = Parent.Height / m_iRows;

            TopLeft.x = (m_iPosition % m_iCols) * (Parent.Width / m_iCols);
            TopLeft.y = (m_iPosition / m_iCols) * (Parent.Height / m_iRows);

            Top = TopLeft.y;
            Left = TopLeft.x;

#if DEBUG && DEBUG_UI
                System.Diagnostics.Debug.WriteLine(String.Format("Position: {0} Top: {1} Left: {2}", m_iPosition, TopLeft.y, TopLeft.x));
#endif
        }

        public void SetPositionFromIndex()
        {
            (int x, int y) TopLeft;

            TopLeft.x = (m_iPosition % m_iCols) * (Parent.Width / m_iCols);
            TopLeft.y = (m_iPosition / m_iCols) * (Parent.Height / m_iRows);

            Top = TopLeft.y;
            Left = TopLeft.x;
        }
    }
}
