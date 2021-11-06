using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slide
{
    public partial class GameSettings : Form
    {
        private bool m_bStartNewGame = false;
        public int Columns { get { return int.Parse(cboColumns.Text); } }
        public int Rows { get { return int.Parse(cboRows.Text); } }
        public bool StartNewGame { get { return m_bStartNewGame; } }

        public GameSettings()
        {
            InitializeComponent();
            cboColumns.SelectedIndex = 0;
            cboRows.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            m_bStartNewGame = true;
            this.Hide();
        }

        private void GameSettings_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                e.Handled = true;
            }
        }

        private void GameSettings_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
