namespace MunicipalApplicationPROG7312.UI
{
    partial class EventsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventsForm));
            lblCat = new Label();
            cmbCategory = new ComboBox();
            lblFrom = new Label();
            dtFrom = new DateTimePicker();
            lblTo = new Label();
            dtTo = new DateTimePicker();
            txtSearch = new TextBox();
            btnSearch = new Button();
            panel1 = new Panel();
            lblTitle = new Label();
            picFlag = new PictureBox();
            gridEvents = new DataGridView();
            lblUpNext = new Label();
            lstUpNext = new ListBox();
            lblUrgent = new Label();
            lstUrgent = new ListBox();
            lstRecommend = new ListBox();
            lblReco = new Label();
            btnBack = new Button();
            btnSettings = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridEvents).BeginInit();
            SuspendLayout();
            // 
            // lblCat
            // 
            lblCat.AutoSize = true;
            lblCat.Location = new Point(16, 70);
            lblCat.Name = "lblCat";
            lblCat.Size = new Size(55, 15);
            lblCat.TabIndex = 1;
            lblCat.Text = "Category";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(16, 90);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(160, 23);
            cmbCategory.TabIndex = 2;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(182, 70);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(35, 15);
            lblFrom.TabIndex = 3;
            lblFrom.Text = "From";
            // 
            // dtFrom
            // 
            dtFrom.Location = new Point(182, 90);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(140, 23);
            dtFrom.TabIndex = 4;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(328, 70);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(20, 15);
            lblTo.TabIndex = 5;
            lblTo.Text = "To";
            // 
            // dtTo
            // 
            dtTo.Location = new Point(328, 90);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(140, 23);
            dtTo.TabIndex = 6;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(474, 70);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search Keywords...";
            txtSearch.Size = new Size(180, 23);
            txtSearch.TabIndex = 7;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Location = new Point(660, 70);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 23);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Lavender;
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(picFlag);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 60);
            panel1.TabIndex = 9;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F);
            lblTitle.ForeColor = SystemColors.ActiveCaptionText;
            lblTitle.Location = new Point(16, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(334, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Local Events and Announcements";
            // 
            // picFlag
            // 
            picFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picFlag.BackgroundImageLayout = ImageLayout.None;
            picFlag.Image = (Image)resources.GetObject("picFlag.Image");
            picFlag.Location = new Point(679, 3);
            picFlag.Name = "picFlag";
            picFlag.Size = new Size(100, 50);
            picFlag.SizeMode = PictureBoxSizeMode.StretchImage;
            picFlag.TabIndex = 0;
            picFlag.TabStop = false;
            // 
            // gridEvents
            // 
            gridEvents.AllowUserToAddRows = false;
            gridEvents.AllowUserToDeleteRows = false;
            gridEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEvents.Location = new Point(16, 119);
            gridEvents.Name = "gridEvents";
            gridEvents.ReadOnly = true;
            gridEvents.RowHeadersVisible = false;
            gridEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridEvents.Size = new Size(638, 280);
            gridEvents.TabIndex = 10;
            // 
            // lblUpNext
            // 
            lblUpNext.AutoSize = true;
            lblUpNext.ForeColor = Color.DeepSkyBlue;
            lblUpNext.Location = new Point(668, 98);
            lblUpNext.Name = "lblUpNext";
            lblUpNext.Size = new Size(49, 15);
            lblUpNext.TabIndex = 11;
            lblUpNext.Text = "Up Next";
            // 
            // lstUpNext
            // 
            lstUpNext.FormattingEnabled = true;
            lstUpNext.ItemHeight = 15;
            lstUpNext.Location = new Point(668, 116);
            lstUpNext.Name = "lstUpNext";
            lstUpNext.Size = new Size(130, 94);
            lstUpNext.TabIndex = 12;
            // 
            // lblUrgent
            // 
            lblUrgent.AutoSize = true;
            lblUrgent.ForeColor = Color.DeepSkyBlue;
            lblUrgent.Location = new Point(668, 336);
            lblUrgent.Name = "lblUrgent";
            lblUrgent.Size = new Size(81, 15);
            lblUrgent.TabIndex = 13;
            lblUrgent.Text = "Urgent Queue";
            // 
            // lstUrgent
            // 
            lstUrgent.FormattingEnabled = true;
            lstUrgent.ItemHeight = 15;
            lstUrgent.Location = new Point(668, 354);
            lstUrgent.Name = "lstUrgent";
            lstUrgent.Size = new Size(130, 94);
            lstUrgent.TabIndex = 14;
            // 
            // lstRecommend
            // 
            lstRecommend.FormattingEnabled = true;
            lstRecommend.ItemHeight = 15;
            lstRecommend.Location = new Point(668, 239);
            lstRecommend.Name = "lstRecommend";
            lstRecommend.Size = new Size(130, 94);
            lstRecommend.TabIndex = 15;
            // 
            // lblReco
            // 
            lblReco.AutoSize = true;
            lblReco.ForeColor = Color.DeepSkyBlue;
            lblReco.Location = new Point(668, 221);
            lblReco.Name = "lblReco";
            lblReco.Size = new Size(129, 15);
            lblReco.TabIndex = 16;
            lblReco.Text = "Recommended for you";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(16, 415);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 17;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(97, 415);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 23);
            btnSettings.TabIndex = 18;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            // 
            // EventsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSettings);
            Controls.Add(btnBack);
            Controls.Add(lblReco);
            Controls.Add(lstRecommend);
            Controls.Add(lstUrgent);
            Controls.Add(lblUrgent);
            Controls.Add(lstUpNext);
            Controls.Add(lblUpNext);
            Controls.Add(gridEvents);
            Controls.Add(panel1);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(dtTo);
            Controls.Add(lblTo);
            Controls.Add(dtFrom);
            Controls.Add(lblFrom);
            Controls.Add(cmbCategory);
            Controls.Add(lblCat);
            Name = "EventsForm";
            Text = "EventsForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblCat;
        private ComboBox cmbCategory;
        private Label lblFrom;
        private DateTimePicker dtFrom;
        private Label lblTo;
        private DateTimePicker dtTo;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel panel1;
        private PictureBox picFlag;
        private Label lblTitle;
        private DataGridView gridEvents;
        private Label lblUpNext;
        private ListBox lstUpNext;
        private Label lblUrgent;
        private ListBox lstUrgent;
        private ListBox lstRecommend;
        private Label lblReco;
        private Button btnBack;
        private Button btnSettings;
    }
}