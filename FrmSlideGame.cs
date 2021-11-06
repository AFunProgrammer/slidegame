using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;

namespace Slide
{
    public partial class FrmGame : Form
    {
        SlideGame slideGame;

        //[DLLImport("user32.dll", EntryPoint = "MapWindowPoints", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true]
        //private extern int MapWindowPoints(HandleRef From, HandleRef To, MarshalByRefObject Point[], MarshalByValueComponent Int32);
        //int MapWindowPoints(HWND hWndFrom, HWND hWndTo, LPPOINT point, int cPoints); //User32.dll

        public FrmGame()
        {
            InitializeComponent();
            slideGame = new SlideGame(pnlSlideGame, txtGuessLocation);
        }

        private void frmSlide_Load(object sender, EventArgs e)
        {
            slideGame.LoadImageInformation();
            this.Show();
            slideGame.StartGame();
        }

        void RestartGame_Click(object sender, System.EventArgs e)
        {
            slideGame.StartGame();
        }

        private void frmSlide_Resize(object sender, EventArgs e)
        {
            slideGame.Resize();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            slideGame.StartGame();
        }

        private void btnGuessLocation_Click(object sender, EventArgs e)
        {
            slideGame.CheckForWin(SlideGame.WinningCondition.Place);
        }

        private void txtGuessLocation_Enter(object sender, EventArgs e)
        {
            if ((txtGuessLocation.Text.ToLower().LastIndexOf("location") > -1 ) ||
               (txtGuessLocation.ForeColor == SystemColors.InactiveCaption))
            {
                txtGuessLocation.Text = "";
            }
            txtGuessLocation.ForeColor = SystemColors.ControlText;
        }

        private void txtGuessLocation_Leave(object sender, EventArgs e)
        {
            if (txtGuessLocation.Text.Trim() == "")
            {
                txtGuessLocation.Text = "[Location]";
                txtGuessLocation.ForeColor = SystemColors.InactiveCaption;
            }
        }

        private void txtGuessLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if ((txtGuessLocation.Text.ToLower().LastIndexOf("location") > -1) ||
               (txtGuessLocation.ForeColor == SystemColors.InactiveCaption))
            {
                txtGuessLocation.Text = "";
                txtGuessLocation.ForeColor = SystemColors.ControlText;
            }
        }

        private void txtGuessLocation_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtGuessLocation.Text.Trim() == "")
            {
                txtGuessLocation.Text = "[Location]";
                txtGuessLocation.ForeColor = SystemColors.InactiveCaption;
            }

            if (e.KeyCode == Keys.Enter)
            {
                btnGuessLocation.Focus();
            }
        }
    }

    /// <summary>
    /// AnchorSetting - Settings to anchor a control to a specific region of a parent control
    /// (this is How The AnchorStyles enum Should Be -> if designed by a thoughtful engineer
    ///  or in a round table discussion)
    /// </summary>

    enum AnchorSettings
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8,
        All = 15,
        TopLeft = 5,
        TopRight = 9,
        BottomLeft = 6,
        BottomRight = 10,
        Width = 12,
        Height = 3
    }

}
