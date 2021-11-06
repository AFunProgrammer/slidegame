
namespace Slide
{
    partial class FrmGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnGuessLocation = new System.Windows.Forms.Button();
            this.txtGuessLocation = new System.Windows.Forms.TextBox();
            this.pnlSlideGame = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNewGame.Location = new System.Drawing.Point(5, 3);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(199, 36);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnGuessLocation
            // 
            this.btnGuessLocation.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGuessLocation.Location = new System.Drawing.Point(210, 3);
            this.btnGuessLocation.Name = "btnGuessLocation";
            this.btnGuessLocation.Size = new System.Drawing.Size(199, 36);
            this.btnGuessLocation.TabIndex = 2;
            this.btnGuessLocation.Text = "Guess Location";
            this.btnGuessLocation.UseVisualStyleBackColor = true;
            this.btnGuessLocation.Click += new System.EventHandler(this.btnGuessLocation_Click);
            // 
            // txtGuessLocation
            // 
            this.txtGuessLocation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGuessLocation.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtGuessLocation.Location = new System.Drawing.Point(415, 8);
            this.txtGuessLocation.Name = "txtGuessLocation";
            this.txtGuessLocation.Size = new System.Drawing.Size(111, 29);
            this.txtGuessLocation.TabIndex = 3;
            this.txtGuessLocation.Text = "[Location]";
            this.txtGuessLocation.Enter += new System.EventHandler(this.txtGuessLocation_Enter);
            this.txtGuessLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGuessLocation_KeyDown);
            this.txtGuessLocation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGuessLocation_KeyUp);
            this.txtGuessLocation.Leave += new System.EventHandler(this.txtGuessLocation_Leave);
            // 
            // pnlSlideGame
            // 
            this.pnlSlideGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSlideGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSlideGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSlideGame.Location = new System.Drawing.Point(3, 43);
            this.pnlSlideGame.Name = "pnlSlideGame";
            this.pnlSlideGame.Size = new System.Drawing.Size(760, 529);
            this.pnlSlideGame.TabIndex = 0;
            // 
            // FrmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 575);
            this.Controls.Add(this.txtGuessLocation);
            this.Controls.Add(this.btnGuessLocation);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.pnlSlideGame);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "FrmGame";
            this.Text = "Slide Game";
            this.Load += new System.EventHandler(this.frmSlide_Load);
            this.Resize += new System.EventHandler(this.frmSlide_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnGuessLocation;
        private System.Windows.Forms.TextBox txtGuessLocation;
        private System.Windows.Forms.Panel pnlSlideGame;
    }
}

