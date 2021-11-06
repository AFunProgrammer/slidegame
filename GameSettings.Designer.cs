
namespace Slide
{
    partial class GameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlGameSettings = new System.Windows.Forms.Panel();
            this.lblGameSettings = new System.Windows.Forms.Label();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.cboRows = new System.Windows.Forms.ComboBox();
            this.lblRows = new System.Windows.Forms.Label();
            this.lblColumns = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlGameSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGameSettings
            // 
            this.pnlGameSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGameSettings.Controls.Add(this.lblGameSettings);
            this.pnlGameSettings.Controls.Add(this.cboColumns);
            this.pnlGameSettings.Controls.Add(this.cboRows);
            this.pnlGameSettings.Controls.Add(this.lblRows);
            this.pnlGameSettings.Controls.Add(this.lblColumns);
            this.pnlGameSettings.Location = new System.Drawing.Point(12, 12);
            this.pnlGameSettings.Name = "pnlGameSettings";
            this.pnlGameSettings.Size = new System.Drawing.Size(292, 166);
            this.pnlGameSettings.TabIndex = 1;
            // 
            // lblGameSettings
            // 
            this.lblGameSettings.AutoSize = true;
            this.lblGameSettings.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGameSettings.Location = new System.Drawing.Point(35, 9);
            this.lblGameSettings.Name = "lblGameSettings";
            this.lblGameSettings.Size = new System.Drawing.Size(227, 45);
            this.lblGameSettings.TabIndex = 10;
            this.lblGameSettings.Text = "Game Settings";
            // 
            // cboColumns
            // 
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Font = new System.Drawing.Font("Segoe Script", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.cboColumns.FormattingEnabled = true;
            this.cboColumns.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6"});
            this.cboColumns.Location = new System.Drawing.Point(237, 72);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(45, 42);
            this.cboColumns.Sorted = true;
            this.cboColumns.TabIndex = 6;
            // 
            // cboRows
            // 
            this.cboRows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRows.Font = new System.Drawing.Font("Segoe Script", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.cboRows.FormattingEnabled = true;
            this.cboRows.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6"});
            this.cboRows.Location = new System.Drawing.Point(197, 119);
            this.cboRows.Name = "cboRows";
            this.cboRows.Size = new System.Drawing.Size(47, 42);
            this.cboRows.Sorted = true;
            this.cboRows.TabIndex = 8;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRows.Location = new System.Drawing.Point(17, 119);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(180, 30);
            this.lblRows.TabIndex = 9;
            this.lblRows.Text = "Number of Rows:";
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblColumns.Location = new System.Drawing.Point(17, 72);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(214, 30);
            this.lblColumns.TabIndex = 7;
            this.lblColumns.Text = "Number of Columns:";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(96, 184);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(128, 42);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 236);
            this.ControlBox = false;
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pnlGameSettings);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Game Settings";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameSettings_KeyUp);
            this.Leave += new System.EventHandler(this.GameSettings_Leave);
            this.pnlGameSettings.ResumeLayout(false);
            this.pnlGameSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGameSettings;
        private System.Windows.Forms.Label lblGameSettings;
        private System.Windows.Forms.ComboBox cboColumns;
        private System.Windows.Forms.ComboBox cboRows;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.Button btnStart;
    }
}