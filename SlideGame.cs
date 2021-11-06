using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Slide
{
    class SlideGame
    {
        private FrmGame m_MainWindow;
        private Panel m_GameSurface;
        private TextBox m_GuessLocation;

        public List<SlideBox> m_lstSlideBoxes = new List<SlideBox>();

        public int m_iGameSize = 9;
        public int m_iGameWidth = 3;
        public int m_iGameHeight = 3;
        public int m_iOpenSpot = 0;
        public bool m_bWon = false;
        private bool m_bDraggingBox = false;

        public enum WinningCondition
        {
            Slides = 0,
            Place = 1
        }

        int m_iBackgroundImageIndex = -1;

        string m_strImagesPath = "";
        List<(String Place, String Name, String Filepath)> m_lstImageInfo = new List<(string Place, String Name, string Filepath)>();

        public SlideGame(Panel GameSurface, TextBox GuessLocation)
        {
            m_GameSurface = GameSurface;
            m_GuessLocation = GuessLocation;
            m_MainWindow = (FrmGame)GameSurface.Parent;
        }

        public void LoadImageInformation()
        {
            StreamReader srImagesData;

            if (m_strImagesPath == "")
            {
                m_strImagesPath = AppContext.BaseDirectory + "\\pictures\\";
            }

            srImagesData = File.OpenText(m_strImagesPath + "images.xml");

            easyXMLReader easyXML = new easyXMLReader(srImagesData);

            string strNodeName = easyXML.ReadToNextNode();
            string strLocation = "";
            string strName = "";
            string strFile = "";

            while (strNodeName != "")
            {
                if (strNodeName == "location")
                {
                    strLocation = easyXML.ReadNodeValue();
                }

                if (strNodeName == "name")
                {
                    strName = easyXML.ReadNodeValue();
                }

                if (strNodeName == "filename")
                {
                    strFile = easyXML.ReadNodeValue();
                }

                if (strLocation.Length > 0 && strFile.Length > 0)
                {
                    m_lstImageInfo.Add((strLocation, strName, (m_strImagesPath + strFile)));
                    strLocation = "";
                    strFile = "";
                }

                strNodeName = easyXML.ReadToNextNode();
            }

            easyXML.Close();
            srImagesData.Close();

        }

        Image LoadNextImage()
        {
            Image backgroundImage = null;

            backgroundImage = Bitmap.FromFile(m_lstImageInfo[m_iBackgroundImageIndex].Filepath);

            return backgroundImage;
        }

         void ClearBackgroundImage()
        {
            m_GameSurface.BackgroundImage = null;
            m_GameSurface.BackColor = Color.Black;
        }

        bool LoadBackgroundImage()
        {
            m_iBackgroundImageIndex++;

            if (m_iBackgroundImageIndex >= m_lstImageInfo.Count())
            {
                m_iBackgroundImageIndex = 0;
            }

            m_GameSurface.BackgroundImageLayout = ImageLayout.Stretch;
            m_GameSurface.BackgroundImage = LoadNextImage();
            m_GameSurface.Tag = m_lstImageInfo[m_iBackgroundImageIndex].Place;

            return true;
        }

        private void lblWin_Click(object o, EventArgs e)
        {
            if (null == o)
            {
                return;
            }

            ((Label)o).Text = ((Label)o).Text == "" ? ((Label)o).Tag.ToString() : "";
        }
        void CreateWinMessage(string WinMsg)
        {
            Label lblWin = new Label();
            lblWin.Tag = WinMsg;
            lblWin.Text = WinMsg;
            Font fWin = new Font(FontFamily.GenericSansSerif, 48, FontStyle.Bold);
            lblWin.ForeColor = Color.FromArgb(255, 255, 64);
            lblWin.BackColor = Color.Transparent;
            lblWin.Font = fWin;
            lblWin.TextAlign = ContentAlignment.TopCenter;
            lblWin.Top = 0;
            lblWin.Left = 0;
            lblWin.Width = m_GameSurface.Width;
            lblWin.Height = m_GameSurface.Height;
            lblWin.Anchor = (AnchorStyles)15;
            lblWin.Visible = true;
            lblWin.Parent = m_GameSurface;
            lblWin.Click += lblWin_Click;
        }

        public void StartGame()
        {
            GameSettings gameSettings = new GameSettings();

            //Form specific code (bah, the need to do this is ... bah)
            gameSettings.Left = (m_MainWindow.Left + m_MainWindow.Width / 2) - (gameSettings.Width / 2);
            gameSettings.Top = (m_MainWindow.Top + m_MainWindow.Height / 2) - (gameSettings.Height / 2);
            gameSettings.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            gameSettings.ShowDialog();

            //Whoops, didn't want to start a new game quite yet
            if (false == gameSettings.StartNewGame)
            {
                gameSettings.Close();
                gameSettings = null;
                return;
            }

            m_bWon = false;
            m_bDraggingBox = false;

            ClearBackgroundImage();

            m_GuessLocation.Text = "[Location]";
            m_GuessLocation.ForeColor = SystemColors.InactiveCaption;

            m_iGameHeight = gameSettings.Rows;
            m_iGameWidth = gameSettings.Columns;
            m_iGameSize = m_iGameHeight * m_iGameWidth;

            gameSettings.Close();
            gameSettings = null;

            if (m_GameSurface.Controls.Count > 0)
            {
                for (int i = (m_GameSurface.Controls.Count - 1); i >= 0; i--)
                {
                    m_GameSurface.Controls[i].Dispose();
                }
            }

            if (m_lstSlideBoxes.Count > 0)
            {
                for (int i = (m_lstSlideBoxes.Count - 1); i >= 0; i--)
                {
                    m_lstSlideBoxes[i].Dispose();
                    m_lstSlideBoxes.RemoveAt(i);
                }

                m_lstSlideBoxes.Clear();
            }

            GC.Collect();

            DateTime dtStart = DateTime.Now;
            for (int i = 1; i < m_iGameSize; i++)
            {
                CreateNewSlideBox(m_iGameWidth, m_iGameHeight, m_GameSurface);
            }
            DateTime dtStop = DateTime.Now;
            TimeSpan tsElapsed = dtStop - dtStart;

            System.Diagnostics.Debug.WriteLine(
                String.Format("Random Selection between 0 and {0} and box setup took: {1}ms",
                m_iGameSize, tsElapsed.TotalMilliseconds));


            m_iOpenSpot = GetOpenSpot(m_iGameSize);

            UpdateSlidePieces();

            LoadBackgroundImage();
        }

        private List<int> GetUnusedPositions(int GameSize)
        {
            List<int> Avail = new List<int>(GameSize);

            for (int i = 0; i < GameSize; i++)
            {
                Avail.Add(i);
            }

            foreach (Label label in m_lstSlideBoxes)
            {
                Avail[(int)(label.Tag)] = -1;
            }

            for (int i = Avail.Count - 1; i >= 0; i--)
            {
                if (Avail[i] == -1)
                {
                    Avail.RemoveAt(i);
                }
            }

            return Avail;
        }

        public int GetOpenSpot(int GameSize)
        {
            List<int> AvailableSpots = GetUnusedPositions(GameSize);

            if (AvailableSpots.Count > 1)
            {
                throw new Exception("There are too many spots open");
            }

            return AvailableSpots[0];
        }


        private bool IsIntUsed(int IsUsed, int GameSize)
        {
#if DEBUG
            List<int> Pos = new List<int>(GameSize);
            List<int> Avail = GetUnusedPositions(GameSize);

            foreach (Label label in m_lstSlideBoxes)
            {
                Pos.Add((int)label.Tag);
            }

            System.Diagnostics.Debug.Write(String.Format("Checking For: {0} ", IsUsed));

            System.Diagnostics.Debug.Write("Available: ");
            foreach (int i in Avail)
            {
                if (i != -1)
                {
                    System.Diagnostics.Debug.Write(String.Format("{0} ", i));
                }
            }

            System.Diagnostics.Debug.Write(" Used: ");
            foreach (int i in Pos)
            {
                System.Diagnostics.Debug.Write(String.Format("{0} ", i));
            }

            System.Diagnostics.Debug.Write("\n");
#endif


            foreach (Label label in m_lstSlideBoxes)
            {
                if (IsUsed == (int)label.Tag)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CreateNewSlideBox(int Cols, int Rows, Control Parent)
        {
            int GameSize = Cols * Rows;
            SlideBox slideBox = new SlideBox(Parent, Cols, Rows, -1);

            Random rnd = new Random();
            int iPos = rnd.Next(GameSize);
            while (IsIntUsed(iPos, GameSize))
            {
                iPos = rnd.Next(GameSize);
            }
            slideBox.Position = iPos;

            Font ctlFont = new Font(FontFamily.GenericSerif, 36, FontStyle.Bold);

            slideBox.Visible = true;
            slideBox.AutoSize = false;
            slideBox.BackColor = Color.DarkSeaGreen;
            slideBox.Font = ctlFont;
            slideBox.Width = Parent.Width / Cols;
            slideBox.Height = Parent.Height / Rows;
            slideBox.Name = "SlideBox" + m_lstSlideBoxes.Count.ToString();
            slideBox.Text = (m_lstSlideBoxes.Count + 1).ToString();
            slideBox.TextAlign = ContentAlignment.MiddleCenter;
            slideBox.Tag = iPos;
            slideBox.Margin = new Padding(0);
            slideBox.FlatStyle = FlatStyle.Flat;
            slideBox.BorderStyle = BorderStyle.FixedSingle;
            slideBox.AllowDrop = true;

            //SlideBox.Invalidate();
            //SlideBox.Refresh();

            slideBox.MouseUp += new MouseEventHandler(Box_MouseUpEvent);
            slideBox.MouseDown += new MouseEventHandler(Box_MouseDownEvent);
            slideBox.MouseMove += new MouseEventHandler(Box_MouseMoveEvent);
            slideBox.MouseEnter += new EventHandler(Box_MouseEnterEvent);
            slideBox.MouseLeave += new EventHandler(Box_MouseLeaveEvent);

            m_lstSlideBoxes.Add(slideBox);

            return true;
        }


        public bool CanPieceMove(int iPos)
        {
            if ((iPos == m_iOpenSpot + 1) && (iPos / m_iGameWidth == m_iOpenSpot / m_iGameWidth))
            {
                return true;
            }

            if ((iPos == m_iOpenSpot - 1) && (iPos / m_iGameWidth == m_iOpenSpot / m_iGameWidth))
            {
                return true;
            }

            if ((iPos == m_iOpenSpot - m_iGameWidth) || (iPos == m_iOpenSpot + m_iGameWidth))
            {
                return true;
            }

            return false;
        }


        public void MoveSlidePiece(int iPos)
        {
            int iSlideBox = 0;

            for (int i = 0; i < m_lstSlideBoxes.Count; i++)
            {
                if (m_lstSlideBoxes[i].Position == iPos)
                {
                    iSlideBox = i;
                    break;
                }
            }

            if (!CanPieceMove(iPos))
            {
                return;
            }

            m_lstSlideBoxes[iSlideBox].Position = m_iOpenSpot;
            m_iOpenSpot = iPos;

            UpdateSlidePieces();

            CheckForWin(WinningCondition.Slides);
        }

        public void Resize()
        {
            m_lstSlideBoxes.ForEach((SlideBox box) =>
            {
                box.ResizeBox();
            });
        }

        public void UpdateSlidePieces()
        {
            m_lstSlideBoxes.ForEach((SlideBox box) =>
            {
                box.SetPositionFromIndex();
            });
        }

        public void CheckForWin(WinningCondition CheckForWinBy)
        {
            //no game is running
            if (m_iBackgroundImageIndex == -1)
                return;

            string strWinMessage = "";
            string strPlaceName = m_lstImageInfo[m_iBackgroundImageIndex].Name;

            if (m_bWon == true)
                return;

            if (WinningCondition.Place == CheckForWinBy)
            {
                List<string> placeNames = m_lstImageInfo[m_iBackgroundImageIndex].Place.Split(',').ToList();
                string guessedPlaceName = m_GuessLocation.Text.ToLower().Trim().Replace(" ", "");
                if (placeNames.IndexOf(guessedPlaceName) > -1)
                {
                    strWinMessage = "Good Guess!\nYou Won!!!";
                    m_bWon = true;
                }
                else
                {
                    return;
                }
            }
            else if (WinningCondition.Slides == CheckForWinBy)
            {
                for (int iSlideBox = 0; iSlideBox < m_lstSlideBoxes.Count; iSlideBox++)
                {
                    if ((m_lstSlideBoxes[iSlideBox].Position + 1).ToString() != m_lstSlideBoxes[iSlideBox].Text)
                    {
                        return;
                    }
                }
                strWinMessage = "Nice Moves!\nYou Won!!!";
                m_bWon = true;
            }

            //Add the place name to the winning message dialog
            if ( m_bWon )
            {
                strWinMessage += "\n\nThis is a picture of:\n" + strPlaceName;
            }

            m_lstSlideBoxes.ForEach((SlideBox box) =>
            {
                box.Visible = false;
            });

            CreateWinMessage(strWinMessage);
        }

        public void Box_MouseDownEvent(object o, MouseEventArgs e)
        {
            SlideBox box = (SlideBox)o;
            if (!box.DragEnabled)
            {
                box.DragStartTimer = new Timer();
                box.DragStartTimer.Interval = 250;
                box.DragStartTimer.Tick += new EventHandler((object o, EventArgs e) =>
                {
                    if( Form.MouseButtons == MouseButtons.Left )
                    {
                        box.DragEnabled = true;
                        m_bDraggingBox = true;
                        box.BringToFront();
                    }
                    box.DragStartTimer.Dispose();
                    Bitmap bmpCursor = new Bitmap(box.Width, box.Height);
                    box.DrawToBitmap(bmpCursor, box.ClientRectangle);
                    //Image icursor = bmpCursor;
                    //CursorConverter cursorFromBmp = new CursorConverter();
                    //System.Drawing.ImageConverter imgConvert = new ImageConverter();
                    //var rtType = Type.GetType(Cursor.Current.Handle;
                    //CursorConverter cc = new CursorConverter();
                    
                    //bmpCursor.Save("C:\\usb\\test.cur",
                    //object cursor = cursorFromBmp.ConvertFrom(icursor);
                    //m_MainWindow.Cursor = (Cursor)cursor;
                });
                box.DragStartTimer.Start();
            }
        }

        SlideBox SlideBoxMouseIsOver(SlideBox HoverBox)
        {
            SlideBox IntersectedBox = null;
            Point ptMouse = Form.MousePosition;
            Rectangle rctMouse = new Rectangle(ptMouse.X - 1, ptMouse.Y - 1, 1, 1);

            m_lstSlideBoxes.ForEach((SlideBox box) =>
            {
                Rectangle rctScreenCoords = box.RectangleToScreen(box.ClientRectangle);
                if (rctScreenCoords.IntersectsWith(rctMouse) && HoverBox.Text != box.Text)
                {
                    IntersectedBox = box;
                    return;
                }
            });

            return IntersectedBox;
        }

        void SwapBoxPositions(SlideBox boxFrom, SlideBox boxTo)
        {
            if (null == boxFrom || null == boxTo)
                return;

            int iFromPos = boxFrom.Position;
            boxFrom.Position = boxTo.Position;
            boxTo.Position = iFromPos;
            m_bDraggingBox = false;

            UpdateSlidePieces();
        }

        bool IsSlideBoxOverOpenSpot()
        {
            Rectangle rctOpenSpot = Rectangle.Empty;

            //(int x, int y, int width, int height) rect;

            rctOpenSpot.X = (m_iOpenSpot % m_iGameWidth) * (m_GameSurface.Width / m_iGameWidth);
            rctOpenSpot.Y = (m_iOpenSpot / m_iGameWidth) * (m_GameSurface.Height / m_iGameHeight);
            rctOpenSpot.Width = (m_GameSurface.Width / m_iGameWidth);
            rctOpenSpot.Height = (m_GameSurface.Height / m_iGameHeight);

            Point ptMouse = Form.MousePosition;
            Point ptClient = m_GameSurface.PointToClient(ptMouse);
            Rectangle rctMouse = new Rectangle(ptClient, new Size(2, 2));

            if (rctOpenSpot.IntersectsWith(rctMouse))
                return true;

            return false;
        }

        public void Box_MouseUpEvent(object o, MouseEventArgs e)
        {
            SlideBox box = (SlideBox)o;
            if (box.DragEnabled)
            {
                box.DragEnabled = false;
                SlideBox swapBox = null;

                swapBox = SlideBoxMouseIsOver(box);

                if (swapBox != null)
                {
                    SwapBoxPositions(box, swapBox);
                    CheckForWin(WinningCondition.Slides);
                }
                else if (IsSlideBoxOverOpenSpot())
                {
                    if (CanPieceMove(box.Position))
                        MoveSlidePiece(box.Position);
                }
                
                box.SetPositionFromIndex();
                m_bDraggingBox = false;

                return;
            }

            MoveSlidePiece(box.Position);
        }

        public void Box_MouseMoveEvent(object o, MouseEventArgs e)
        {
            SlideBox box = (SlideBox)o;
            if (MouseButtons.Left == e.Button && true == box.DragEnabled)
            {
                Point ptMouse = FrmGame.MousePosition;
                Point ptDragPoint = m_GameSurface.PointToClient(ptMouse);
                box.Top = ptDragPoint.Y - (box.Height / 2);
                box.Left = ptDragPoint.X - (box.Width / 2);
                //System.Diagnostics.Debug.WriteLine("Drag Event: Mouse Coordinates {0},{1}, Box Coordinates {2},{3}", ptDragPoint.X, ptDragPoint.Y, box.Left, box.Top);
            }
        }

        public void Box_MouseEnterEvent(object o, EventArgs e)
        {
            SlideBox box = (SlideBox)o;
            //if ( m_bDraggingBox && !box.DragEnabled )
            //{
            //    box.BackColor = Color.FromArgb(255, 127, 127);
            //}
        }

        public void Box_MouseLeaveEvent(object o, EventArgs e)
        {
            SlideBox box = (SlideBox)o;
            //if ( box.BackColor == Color.FromArgb(255,127,127) )
            //{
            //    box.BackColor = Color.DarkSeaGreen;
            //}
        }

    }
}
