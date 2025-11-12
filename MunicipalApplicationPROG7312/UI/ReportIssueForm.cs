using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing; // for layout if we create controls programmatically

namespace MunicipalApplicationPROG7312.UI
{
    public partial class ReportIssueForm : Form
    {
        // stores attachment paths
        private readonly LinkedList<string> _attachments = new();

        // error provider shows the red icon like in your screenshot
        private readonly ErrorProvider _errors = new ErrorProvider() { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        // progress UI (we'll bind to existing controls if present; otherwise we create them)
        private ProgressBar _prg;
        private Label _lblProgress;

        // how many required fields we track
        private const int REQUIRED_TOTAL = 4; // Location, Category, Description, Consent

        public ReportIssueForm()
        {
            InitializeComponent();                                 // builds designer UI
            _errors.ContainerControl = this;                       // attach error provider to the form

            // Hook change events so validation/progress react instantly
            txtLocation.TextChanged += OnFieldChanged;             // re-validate when location changes
            cmbCategory.SelectedIndexChanged += OnFieldChanged;    // re-validate when category changes
            txtDescription.TextChanged += OnFieldChanged;          // re-validate when description changes
            chkConsent.ItemCheck += Consent_ItemCheck;             // re-validate when consent is ticked/unticked

            // Attach buttons (in case not wired in designer)
            btnAttach.Click += BtnAttach_Click;                    // open file dialog and add attachments
            btnSubmit.Click += BtnSubmit_Click;                    // validate+submit

            // Bind or create progress UI
            BindProgressUi();                                      // find existing ProgressBar/Label or create them
            UpdateValidationAndProgress();                         // initial state (e.g., "Start by adding location")
        }

        // -------- Attachments ----------
        private void BtnAttach_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Attach media",
                Filter = "Images or documents|*.png;*.jpg;*.jpeg;*.pdf;*.docx;*.txt|All files|*.*",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    _attachments.AddLast(file);                    // keep full path
                    lstAttachments.Items.Add(Path.GetFileName(file)); // show short name
                }
            }
        }

        // -------- Submit ----------
        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            UpdateValidationAndProgress();                         // final validation pass

            if (CompletedCount() < REQUIRED_TOTAL)
            {
                MessageBox.Show("Please complete Location, Category, Description and POPIA consent.",
                                "Missing info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Issue submitted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset form
            cmbCategory.SelectedIndex = -1;                        // reset selection
            txtLocation.Clear();                                   // reset location
            txtDescription.Clear();                                // reset description
            lstAttachments.Items.Clear();                          // clear list
            _attachments.Clear();                                  // clear store
            if (chkConsent.Items.Count > 0)                        // uncheck consent if present
                chkConsent.SetItemChecked(0, false);

            UpdateValidationAndProgress();                         // reset progress/errors
        }

        // -------- Live validation & progress ----------
        private void OnFieldChanged(object? sender, EventArgs e)
        {
            UpdateValidationAndProgress();                         // single entry point for all field changes
        }

        private void Consent_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            // ItemCheck fires before the CheckedItems updates; defer update to after the state changes
            BeginInvoke(new Action(UpdateValidationAndProgress));
        }

        private void UpdateValidationAndProgress()
        {
            // validate each field and set/clear inline error
            ValidateLocation();
            ValidateCategory();
            ValidateDescription();
            ValidateConsent();

            // compute progress
            var completed = CompletedCount();
            _prg.Maximum = REQUIRED_TOTAL;                         // total required checkpoints
            _prg.Value = Math.Min(completed, _prg.Maximum);        // guard against overflow
            _lblProgress.Text = completed switch
            {
                0 => "Start by adding location",
                < REQUIRED_TOTAL => $"{completed}/{REQUIRED_TOTAL} completed",
                _ => "All required fields completed"
            };
        }

        private void ValidateLocation()
        {
            var ok = !string.IsNullOrWhiteSpace(txtLocation.Text); // location must not be empty
            _errors.SetError(txtLocation, ok ? "" : "Location is required");
        }

        private void ValidateCategory()
        {
            var ok = cmbCategory.SelectedIndex >= 0;               // must choose a category
            _errors.SetError(cmbCategory, ok ? "" : "Select a category");
        }

        private void ValidateDescription()
        {
            var ok = !string.IsNullOrWhiteSpace(txtDescription.Text); // description must not be empty
            _errors.SetError(txtDescription, ok ? "" : "Add a brief description");
        }

        private void ValidateConsent()
        {
            var ok = chkConsent.CheckedItems.Count > 0;            // must tick POPIA consent
            _errors.SetError(chkConsent, ok ? "" : "Consent is required");
        }

        private int CompletedCount()
        {
            int count = 0;
            if (string.IsNullOrWhiteSpace(_errors.GetError(txtLocation))) count++;      // location valid
            if (string.IsNullOrWhiteSpace(_errors.GetError(cmbCategory))) count++;      // category valid
            if (string.IsNullOrWhiteSpace(_errors.GetError(txtDescription))) count++;   // description valid
            if (string.IsNullOrWhiteSpace(_errors.GetError(chkConsent))) count++;       // consent valid
            return count;
        }

        // -------- Progress UI plumbing ----------
        private void BindProgressUi()
        {
           
            _prg = prgReport ?? CreateProgressBar();               
            _lblProgress = lblProgress ?? CreateProgressLabel();   
        }

        private ProgressBar CreateProgressBar()
        {
            // create a slim bar under the attachments box (like your screenshot)
            var bar = new ProgressBar
            {
                Name = "prgReport",
                Height = 12,
                Style = ProgressBarStyle.Continuous
            };

            Control host = lstAttachments as Control ?? (Control)this;
            bar.Width = host.Width;
            bar.Left = host.Left;
            bar.Top = host.Bottom + 6;


            // add to same parent so it aligns nicely
            (host.Parent ?? this).Controls.Add(bar);
            bar.BringToFront();
            return bar;
        }

        private Label CreateProgressLabel()
        {
            var lbl = new Label
            {
                Name = "lblProgress",
                AutoSize = true,
                Text = "Start by adding location"
            };

            // sit just under the progress bar
            lbl.Left = _prg.Left;
            lbl.Top = _prg.Bottom + 6;

            (_prg.Parent ?? this).Controls.Add(lbl);
            lbl.BringToFront();
            return lbl;
        }
    }
}
