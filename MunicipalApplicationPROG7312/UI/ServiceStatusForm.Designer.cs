namespace MunicipalApplicationPROG7312.UI
{
    partial class ServiceStatusForm
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
            lblFIlter = new Label();
            cmbFilter = new ComboBox();
            btnProcessNext = new Button();
            btnUndo = new Button();
            gridEvents = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colPriority = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colSubmittedAt = new DataGridViewTextBoxColumn();
            colEta = new DataGridViewTextBoxColumn();
            lblUpNext = new Label();
            lstUpNext = new ListBox();
            lblReco = new Label();
            lstRecommended = new ListBox();
            lblUrgent = new Label();
            lstUrgent = new ListBox();
            btnComputeMst = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)gridEvents).BeginInit();
            SuspendLayout();
            // 
            // lblFIlter
            // 
            lblFIlter.AutoSize = true;
            lblFIlter.Location = new Point(20, 18);
            lblFIlter.Name = "lblFIlter";
            lblFIlter.Size = new Size(33, 15);
            lblFIlter.TabIndex = 0;
            lblFIlter.Text = "Filter";
            // 
            // cmbFilter
            // 
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(20, 36);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(200, 23);
            cmbFilter.TabIndex = 1;
            // 
            // btnProcessNext
            // 
            btnProcessNext.Location = new Point(232, 34);
            btnProcessNext.Name = "btnProcessNext";
            btnProcessNext.Size = new Size(180, 28);
            btnProcessNext.TabIndex = 2;
            btnProcessNext.Text = "Process Next (Priority)";
            btnProcessNext.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(420, 34);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(160, 28);
            btnUndo.TabIndex = 3;
            btnUndo.Text = "Undo Last Change";
            btnUndo.UseVisualStyleBackColor = true;
            // 
            // gridEvents
            // 
            gridEvents.AllowUserToAddRows = false;
            gridEvents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            gridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEvents.Columns.AddRange(new DataGridViewColumn[] { colId, colCategory, colPriority, colStatus, colSubmittedAt, colEta });
            gridEvents.Location = new Point(20, 68);
            gridEvents.Name = "gridEvents";
            gridEvents.ReadOnly = true;
            gridEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridEvents.Size = new Size(733, 396);
            gridEvents.TabIndex = 4;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 60;
            // 
            // colCategory
            // 
            colCategory.DataPropertyName = "Category";
            colCategory.HeaderText = "Category";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            colCategory.Width = 120;
            // 
            // colPriority
            // 
            colPriority.DataPropertyName = "Priority";
            colPriority.HeaderText = "Priority";
            colPriority.Name = "colPriority";
            colPriority.ReadOnly = true;
            colPriority.Width = 140;
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 110;
            // 
            // colSubmittedAt
            // 
            colSubmittedAt.DataPropertyName = "SubmittedAt";
            colSubmittedAt.HeaderText = "SubmittedAt";
            colSubmittedAt.Name = "colSubmittedAt";
            colSubmittedAt.ReadOnly = true;
            colSubmittedAt.Width = 140;
            // 
            // colEta
            // 
            colEta.DataPropertyName = "Eta";
            colEta.HeaderText = "Eta";
            colEta.Name = "colEta";
            colEta.ReadOnly = true;
            colEta.Width = 120;
            // 
            // lblUpNext
            // 
            lblUpNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUpNext.AutoSize = true;
            lblUpNext.Location = new Point(792, 68);
            lblUpNext.Name = "lblUpNext";
            lblUpNext.Size = new Size(49, 15);
            lblUpNext.TabIndex = 5;
            lblUpNext.Text = "Up Next";
            // 
            // lstUpNext
            // 
            lstUpNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lstUpNext.BorderStyle = BorderStyle.FixedSingle;
            lstUpNext.FormattingEnabled = true;
            lstUpNext.IntegralHeight = false;
            lstUpNext.ItemHeight = 15;
            lstUpNext.Location = new Point(792, 86);
            lstUpNext.Name = "lstUpNext";
            lstUpNext.Size = new Size(232, 150);
            lstUpNext.TabIndex = 7;
            // 
            // lblReco
            // 
            lblReco.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblReco.AutoSize = true;
            lblReco.Location = new Point(792, 239);
            lblReco.Name = "lblReco";
            lblReco.Size = new Size(129, 15);
            lblReco.TabIndex = 8;
            lblReco.Text = "Recommended for you";
            // 
            // lstRecommended
            // 
            lstRecommended.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lstRecommended.BorderStyle = BorderStyle.FixedSingle;
            lstRecommended.FormattingEnabled = true;
            lstRecommended.IntegralHeight = false;
            lstRecommended.ItemHeight = 15;
            lstRecommended.Location = new Point(792, 257);
            lstRecommended.Name = "lstRecommended";
            lstRecommended.Size = new Size(232, 100);
            lstRecommended.TabIndex = 9;
            // 
            // lblUrgent
            // 
            lblUrgent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUrgent.AutoSize = true;
            lblUrgent.Location = new Point(792, 360);
            lblUrgent.Name = "lblUrgent";
            lblUrgent.Size = new Size(81, 15);
            lblUrgent.TabIndex = 10;
            lblUrgent.Text = "Urgent Queue";
            // 
            // lstUrgent
            // 
            lstUrgent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lstUrgent.BorderStyle = BorderStyle.FixedSingle;
            lstUrgent.FormattingEnabled = true;
            lstUrgent.IntegralHeight = false;
            lstUrgent.ItemHeight = 15;
            lstUrgent.Location = new Point(792, 378);
            lstUrgent.Name = "lstUrgent";
            lstUrgent.Size = new Size(232, 120);
            lstUrgent.TabIndex = 11;
            // 
            // btnComputeMst
            // 
            btnComputeMst.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnComputeMst.Location = new Point(792, 526);
            btnComputeMst.Name = "btnComputeMst";
            btnComputeMst.Size = new Size(232, 28);
            btnComputeMst.TabIndex = 12;
            btnComputeMst.Text = "Compute MST";
            btnComputeMst.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.Location = new Point(28, 522);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // ServiceStatusForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1064, 601);
            Controls.Add(btnBack);
            Controls.Add(btnComputeMst);
            Controls.Add(lstUrgent);
            Controls.Add(lblUrgent);
            Controls.Add(lstRecommended);
            Controls.Add(lblReco);
            Controls.Add(lstUpNext);
            Controls.Add(lblUpNext);
            Controls.Add(gridEvents);
            Controls.Add(btnUndo);
            Controls.Add(btnProcessNext);
            Controls.Add(cmbFilter);
            Controls.Add(lblFIlter);
            MinimumSize = new Size(1080, 640);
            Name = "ServiceStatusForm";
            Text = "Service Request Status";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)gridEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFIlter;
        private ComboBox cmbFilter;
        private Button btnProcessNext;
        private Button btnUndo;
        private DataGridView gridEvents;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colPriority;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colSubmittedAt;
        private DataGridViewTextBoxColumn colEta;
        private Label lblUpNext;
        private ListBox lstUpNext;
        private Label lblReco;
        private ListBox lstRecommended;
        private Label lblUrgent;
        private ListBox lstUrgent;
        private Button btnComputeMst;
        private Button btnBack;
    }
}