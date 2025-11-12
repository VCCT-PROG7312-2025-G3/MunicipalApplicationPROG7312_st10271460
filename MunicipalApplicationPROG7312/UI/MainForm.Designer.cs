namespace MunicipalApplicationPROG7312.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Header = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            tblTiles = new TableLayoutPanel();
            btnTileReports = new Button();
            btnTileEvents = new Button();
            btnTileStatus = new Button();
            pnlSearch = new Panel();
            lblCredit = new Label();
            btnSearch = new Button();
            txtSearch = new TextBox();
            Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tblTiles.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // Header
            // 
            Header.BackColor = Color.CornflowerBlue;
            Header.Controls.Add(pictureBox1);
            Header.Controls.Add(label1);
            Header.Dock = DockStyle.Top;
            Header.Location = new Point(0, 0);
            Header.Name = "Header";
            Header.Size = new Size(800, 84);
            Header.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(675, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(20, 26);
            label1.Name = "label1";
            label1.Size = new Size(252, 30);
            label1.TabIndex = 0;
            label1.Text = "Municipal Services Portal";
            // 
            // tblTiles
            // 
            tblTiles.ColumnCount = 3;
            tblTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblTiles.Controls.Add(btnTileReports, 0, 0);
            tblTiles.Controls.Add(btnTileEvents, 1, 0);
            tblTiles.Controls.Add(btnTileStatus, 2, 0);
            tblTiles.Dock = DockStyle.Top;
            tblTiles.Location = new Point(0, 84);
            tblTiles.Name = "tblTiles";
            tblTiles.RowCount = 1;
            tblTiles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblTiles.Size = new Size(800, 319);
            tblTiles.TabIndex = 1;
            // 
            // btnTileReports
            // 
            btnTileReports.BackColor = Color.Gold;
            btnTileReports.BackgroundImage = (Image)resources.GetObject("btnTileReports.BackgroundImage");
            btnTileReports.BackgroundImageLayout = ImageLayout.Stretch;
            btnTileReports.Dock = DockStyle.Fill;
            btnTileReports.FlatStyle = FlatStyle.Flat;
            btnTileReports.ForeColor = SystemColors.ButtonFace;
            btnTileReports.Location = new Point(3, 3);
            btnTileReports.Name = "btnTileReports";
            btnTileReports.Size = new Size(260, 313);
            btnTileReports.TabIndex = 0;
            btnTileReports.Text = "Report Issue";
            btnTileReports.TextAlign = ContentAlignment.BottomCenter;
            btnTileReports.UseVisualStyleBackColor = false;
            // 
            // btnTileEvents
            // 
            btnTileEvents.BackColor = Color.Green;
            btnTileEvents.BackgroundImage = (Image)resources.GetObject("btnTileEvents.BackgroundImage");
            btnTileEvents.BackgroundImageLayout = ImageLayout.Stretch;
            btnTileEvents.Dock = DockStyle.Fill;
            btnTileEvents.ForeColor = SystemColors.ButtonFace;
            btnTileEvents.Location = new Point(269, 3);
            btnTileEvents.Name = "btnTileEvents";
            btnTileEvents.Size = new Size(260, 313);
            btnTileEvents.TabIndex = 1;
            btnTileEvents.Text = "Local Events";
            btnTileEvents.TextAlign = ContentAlignment.BottomCenter;
            btnTileEvents.UseVisualStyleBackColor = false;
            btnTileEvents.Click += btnTileEvents_Click_1;
            // 
            // btnTileStatus
            // 
            btnTileStatus.BackColor = Color.Blue;
            btnTileStatus.BackgroundImage = (Image)resources.GetObject("btnTileStatus.BackgroundImage");
            btnTileStatus.BackgroundImageLayout = ImageLayout.Stretch;
            btnTileStatus.Dock = DockStyle.Fill;
            btnTileStatus.ForeColor = SystemColors.ButtonFace;
            btnTileStatus.Location = new Point(535, 3);
            btnTileStatus.Name = "btnTileStatus";
            btnTileStatus.Size = new Size(262, 313);
            btnTileStatus.TabIndex = 2;
            btnTileStatus.Text = "Service Request Status";
            btnTileStatus.TextAlign = ContentAlignment.BottomCenter;
            btnTileStatus.UseVisualStyleBackColor = false;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(lblCredit);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 403);
            pnlSearch.Margin = new Padding(24, 18, 24, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(800, 45);
            pnlSearch.TabIndex = 2;
            // 
            // lblCredit
            // 
            lblCredit.AutoSize = true;
            lblCredit.Font = new Font("Segoe UI", 8F);
            lblCredit.ForeColor = SystemColors.ControlDark;
            lblCredit.Location = new Point(0, 17);
            lblCredit.Name = "lblCredit";
            lblCredit.Size = new Size(180, 13);
            lblCredit.TabIndex = 2;
            lblCredit.Text = "Developed by Reuven-Jon Kadalie";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.ActiveCaption;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = SystemColors.ButtonFace;
            btnSearch.Location = new Point(632, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(186, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search";
            txtSearch.Size = new Size(440, 23);
            txtSearch.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlSearch);
            Controls.Add(tblTiles);
            Controls.Add(Header);
            Name = "MainForm";
            Text = "MainForm";
            Header.ResumeLayout(false);
            Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tblTiles.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel Header;
        private PictureBox pictureBox1;
        private Label label1;
        private TableLayoutPanel tblTiles;
        private Button btnTileReports;
        private Button btnTileEvents;
        private Button btnTileStatus;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Label lblCredit;
        private Button btnSearch;
    }
}