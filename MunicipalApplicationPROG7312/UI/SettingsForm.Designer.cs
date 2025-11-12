namespace MunicipalApplicationPROG7312.UI
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            header = new Panel();
            picFlag = new PictureBox();
            lblTitle = new Label();
            pnlSettingsMain = new Panel();
            lblCredit = new Label();
            grpSettings = new GroupBox();
            btnCancel = new Button();
            btnSave = new Button();
            numFontSize = new NumericUpDown();
            lblFontSize = new Label();
            cmbTheme = new ComboBox();
            lblTheme = new Label();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFlag).BeginInit();
            pnlSettingsMain.SuspendLayout();
            grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).BeginInit();
            SuspendLayout();
            // 
            // header
            // 
            header.BackColor = Color.DeepSkyBlue;
            header.Controls.Add(picFlag);
            header.Controls.Add(lblTitle);
            header.Dock = DockStyle.Top;
            header.Location = new Point(0, 0);
            header.Name = "header";
            header.Size = new Size(800, 84);
            header.TabIndex = 0;
            // 
            // picFlag
            // 
            picFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picFlag.Image = (Image)resources.GetObject("picFlag.Image");
            picFlag.Location = new Point(688, 12);
            picFlag.Name = "picFlag";
            picFlag.Size = new Size(100, 50);
            picFlag.SizeMode = PictureBoxSizeMode.StretchImage;
            picFlag.TabIndex = 1;
            picFlag.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.DeepSkyBlue;
            lblTitle.Font = new Font("Segoe UI", 16F);
            lblTitle.ForeColor = SystemColors.ButtonFace;
            lblTitle.Location = new Point(20, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(90, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Settings";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSettingsMain
            // 
            pnlSettingsMain.BackColor = SystemColors.ButtonFace;
            pnlSettingsMain.Controls.Add(lblCredit);
            pnlSettingsMain.Controls.Add(grpSettings);
            pnlSettingsMain.Dock = DockStyle.Fill;
            pnlSettingsMain.Location = new Point(0, 84);
            pnlSettingsMain.Name = "pnlSettingsMain";
            pnlSettingsMain.Padding = new Padding(24);
            pnlSettingsMain.Size = new Size(800, 366);
            pnlSettingsMain.TabIndex = 1;
            // 
            // lblCredit
            // 
            lblCredit.AutoSize = true;
            lblCredit.ForeColor = SystemColors.AppWorkspace;
            lblCredit.Location = new Point(32, 345);
            lblCredit.Name = "lblCredit";
            lblCredit.Size = new Size(185, 15);
            lblCredit.TabIndex = 1;
            lblCredit.Text = "Developed by Reuven-Jon Kadalie";
            // 
            // grpSettings
            // 
            grpSettings.Controls.Add(btnCancel);
            grpSettings.Controls.Add(btnSave);
            grpSettings.Controls.Add(numFontSize);
            grpSettings.Controls.Add(lblFontSize);
            grpSettings.Controls.Add(cmbTheme);
            grpSettings.Controls.Add(lblTheme);
            grpSettings.Controls.Add(cmbLanguage);
            grpSettings.Controls.Add(lblLanguage);
            grpSettings.FlatStyle = FlatStyle.System;
            grpSettings.Font = new Font("Segoe UI", 10F);
            grpSettings.ForeColor = SystemColors.ActiveCaptionText;
            grpSettings.Location = new Point(60, 40);
            grpSettings.Name = "grpSettings";
            grpSettings.Size = new Size(600, 300);
            grpSettings.TabIndex = 0;
            grpSettings.TabStop = false;
            grpSettings.Text = "Application Preferences";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Red;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = SystemColors.ButtonFace;
            btnCancel.Location = new Point(270, 210);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.ActiveCaption;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = SystemColors.ButtonFace;
            btnSave.Location = new Point(150, 210);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // numFontSize
            // 
            numFontSize.Location = new Point(150, 146);
            numFontSize.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numFontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numFontSize.Name = "numFontSize";
            numFontSize.Size = new Size(80, 25);
            numFontSize.TabIndex = 5;
            numFontSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblFontSize
            // 
            lblFontSize.AutoSize = true;
            lblFontSize.Location = new Point(30, 150);
            lblFontSize.Name = "lblFontSize";
            lblFontSize.Size = new Size(64, 19);
            lblFontSize.TabIndex = 4;
            lblFontSize.Text = "Font Size";
            // 
            // cmbTheme
            // 
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.FormattingEnabled = true;
            cmbTheme.Location = new Point(150, 96);
            cmbTheme.Name = "cmbTheme";
            cmbTheme.Size = new Size(250, 25);
            cmbTheme.TabIndex = 3;
            // 
            // lblTheme
            // 
            lblTheme.AutoSize = true;
            lblTheme.Location = new Point(30, 100);
            lblTheme.Name = "lblTheme";
            lblTheme.Size = new Size(50, 19);
            lblTheme.TabIndex = 2;
            lblTheme.Text = "Theme";
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(150, 46);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(250, 25);
            cmbLanguage.TabIndex = 1;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(30, 50);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(69, 19);
            lblLanguage.TabIndex = 0;
            lblLanguage.Text = "Language";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlSettingsMain);
            Controls.Add(header);
            Name = "SettingsForm";
            Text = "SettingsForm";
            header.ResumeLayout(false);
            header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picFlag).EndInit();
            pnlSettingsMain.ResumeLayout(false);
            pnlSettingsMain.PerformLayout();
            grpSettings.ResumeLayout(false);
            grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel header;
        private Label lblTitle;
        private PictureBox picFlag;
        private Panel pnlSettingsMain;
        private GroupBox grpSettings;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
        private NumericUpDown numFontSize;
        private Label lblFontSize;
        private ComboBox cmbTheme;
        private Label lblTheme;
        private Button btnCancel;
        private Button btnSave;
        private Label lblCredit;
    }
}