// UI/ReportIssueForm.Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312.UI
{
    partial class ReportIssueForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblDescription;
        private RichTextBox txtDescription;
        private Button btnAttach;
        private Button btnSubmit;
        private CheckedListBox chkConsent;
        private ListBox lstAttachments;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblDescription = new Label();
            txtDescription = new RichTextBox();
            btnAttach = new Button();
            btnSubmit = new Button();
            chkConsent = new CheckedListBox();
            lstAttachments = new ListBox();
            btnBack = new Button();
            grpBox = new GroupBox();
            lblProgress = new Label();
            btnRemoveSelected = new Button();
            prgReport = new ProgressBar();
            panel1 = new Panel();
            btnSettings = new Button();
            lblReportIssues = new Label();
            grpBox.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblCategory
            // 
            lblCategory.Location = new Point(12, 141);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(100, 23);
            lblCategory.TabIndex = 0;
            lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            cmbCategory.Items.AddRange(new object[] { "Water", "Electricity", "Roads", "Waste", "Parks" });
            cmbCategory.Location = new Point(12, 167);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(377, 23);
            cmbCategory.TabIndex = 1;
            // 
            // lblLocation
            // 
            lblLocation.Location = new Point(12, 78);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(100, 23);
            lblLocation.TabIndex = 2;
            lblLocation.Text = "Location";
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(12, 104);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(377, 23);
            txtLocation.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(12, 202);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 228);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(377, 154);
            txtDescription.TabIndex = 5;
            txtDescription.Text = "";
            // 
            // btnAttach
            // 
            btnAttach.Location = new Point(17, 22);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new Size(150, 34);
            btnAttach.TabIndex = 6;
            btnAttach.Text = "Add Attachment";
            btnAttach.Click += BtnAttach_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(16, 451);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(140, 36);
            btnSubmit.TabIndex = 9;
            btnSubmit.Text = "Submit Issue";
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // chkConsent
            // 
            chkConsent.Items.AddRange(new object[] { "I consent to processing of my data for service delivery (POPIA)." });
            chkConsent.Location = new Point(16, 402);
            chkConsent.Name = "chkConsent";
            chkConsent.Size = new Size(377, 22);
            chkConsent.TabIndex = 8;
            // 
            // lstAttachments
            // 
            lstAttachments.ItemHeight = 15;
            lstAttachments.Location = new Point(17, 63);
            lstAttachments.Name = "lstAttachments";
            lstAttachments.Size = new Size(325, 154);
            lstAttachments.TabIndex = 7;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.Location = new Point(185, 451);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(140, 36);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // grpBox
            // 
            grpBox.BackColor = SystemColors.ButtonFace;
            grpBox.Controls.Add(lblProgress);
            grpBox.Controls.Add(btnRemoveSelected);
            grpBox.Controls.Add(prgReport);
            grpBox.Controls.Add(lstAttachments);
            grpBox.Controls.Add(btnAttach);
            grpBox.Location = new Point(415, 104);
            grpBox.Name = "grpBox";
            grpBox.Size = new Size(362, 413);
            grpBox.TabIndex = 11;
            grpBox.TabStop = false;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(17, 330);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(52, 15);
            lblProgress.TabIndex = 10;
            lblProgress.Text = "Progress";
            // 
            // btnRemoveSelected
            // 
            btnRemoveSelected.Location = new Point(192, 22);
            btnRemoveSelected.Name = "btnRemoveSelected";
            btnRemoveSelected.Size = new Size(150, 34);
            btnRemoveSelected.TabIndex = 9;
            btnRemoveSelected.Text = "Remove Selected";
            btnRemoveSelected.UseVisualStyleBackColor = true;
            // 
            // prgReport
            // 
            prgReport.Location = new Point(17, 358);
            prgReport.Name = "prgReport";
            prgReport.Size = new Size(325, 23);
            prgReport.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(btnSettings);
            panel1.Controls.Add(lblReportIssues);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(780, 70);
            panel1.TabIndex = 12;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(682, 19);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 23);
            btnSettings.TabIndex = 1;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            // 
            // lblReportIssues
            // 
            lblReportIssues.AutoSize = true;
            lblReportIssues.Font = new Font("Segoe UI", 16F);
            lblReportIssues.Location = new Point(20, 19);
            lblReportIssues.Name = "lblReportIssues";
            lblReportIssues.Size = new Size(141, 30);
            lblReportIssues.TabIndex = 0;
            lblReportIssues.Text = "Report Issues";
            // 
            // ReportIssueForm
            // 
            ClientSize = new Size(780, 540);
            Controls.Add(panel1);
            Controls.Add(grpBox);
            Controls.Add(btnBack);
            Controls.Add(lblCategory);
            Controls.Add(cmbCategory);
            Controls.Add(lblLocation);
            Controls.Add(txtLocation);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(chkConsent);
            Controls.Add(btnSubmit);
            Name = "ReportIssueForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Report Issue";
            grpBox.ResumeLayout(false);
            grpBox.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnBack;
        private GroupBox grpBox;
        private ProgressBar prgReport;
        private Panel panel1;
        private Label lblReportIssues;
        private Button btnRemoveSelected;
        private Button btnSettings;
        private Label lblProgress;
    }
}
