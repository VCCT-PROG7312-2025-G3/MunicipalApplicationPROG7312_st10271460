// UI/ReportIssueForm.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace MunicipalApplicationPROG7312.UI
{
    public partial class ReportIssueForm : Form
    {
        // Store full attachment paths; ListBox shows filenames only
        private readonly LinkedList<string> _attachments = new();

        // Red inline error icons next to invalid fields
        private readonly ErrorProvider _errors = new ErrorProvider()
        {
            BlinkStyle = ErrorBlinkStyle.NeverBlink
        };

        // Progress UI (bind existing designer controls if present; otherwise create)
        private ProgressBar _prg;
        private Label _lblProgress;

        // Required checkpoints: Location, Category, Description, Consent
        private const int REQUIRED_TOTAL = 4;

        public ReportIssueForm()
        {
            InitializeComponent();
            this.UseGlobalSettings();
            GlobalUiSettings.WireLanguageRefresh(this);

            // ErrorProvider operates on this form
            _errors.ContainerControl = this;

            // Live validation events
            txtLocation.TextChanged += OnFieldChanged;           // validate on typing
            cmbCategory.SelectedIndexChanged += OnFieldChanged;  // validate on selection
            txtDescription.TextChanged += OnFieldChanged;        // validate on typing

            // Consent is a CheckedListBox in your Designer
            chkConsent.ItemCheck += Consent_ItemCheck;           // revalidate when user ticks/unticks

            // Attachments
            btnAttach.Click += BtnAttach_Click;                  // add files
            btnRemoveSelected.Click += BtnRemoveSelected_Click;  // multi-delete
            lstAttachments.SelectionMode = SelectionMode.MultiExtended;

            // Actions
            btnSubmit.Click += BtnSubmit_Click;                  // submit
            btnBack.Click += (s, e) => Close();                  // back to MainForm
            btnSettings.Click += BtnSettings_Click;              // open Settings

            // Bind/create progress UI and sync initial state
            BindProgressUi();
            UpdateValidationAndProgress();
        }

        // =============== Attachments ===============

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
                    _attachments.AddLast(file);                         // keep path
                    lstAttachments.Items.Add(Path.GetFileName(file));    // show name
                }
            }
        }

        private void BtnRemoveSelected_Click(object? sender, EventArgs e)
        {
            if (lstAttachments.SelectedIndices.Count == 0) return;

            // Remove items bottom-up so indices don’t shift
            var indices = lstAttachments.SelectedIndices.Cast<int>()
                           .OrderByDescending(i => i).ToList();

            foreach (int idx in indices)
            {
                // Mirror removal in LinkedList by position
                var node = _attachments.First;
                for (int i = 0; i < idx && node != null; i++) node = node.Next;
                if (node != null) _attachments.Remove(node);

                lstAttachments.Items.RemoveAt(idx);
            }
        }

        // =============== Submit / Navigation ===============

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            UpdateValidationAndProgress(); // final pass

            if (CompletedCount() < REQUIRED_TOTAL)
            {
                MessageBox.Show(
                    "Please complete Location, Category, Description and POPIA consent.",
                    "Missing info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                FocusFirstInvalid();
                return;
            }

            MessageBox.Show("Issue submitted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset form
            cmbCategory.SelectedIndex = -1;
            txtLocation.Clear();
            txtDescription.Clear();

            // Uncheck everything in consent list
            for (int i = 0; i < chkConsent.Items.Count; i++)
                chkConsent.SetItemChecked(i, false);

            lstAttachments.Items.Clear();
            _attachments.Clear();

            UpdateValidationAndProgress();
        }

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            using var f = new SettingsForm();
            f.ShowDialog(this); // modal
        }

        // =============== Validation + Progress ===============

        private void OnFieldChanged(object? sender, EventArgs e) => UpdateValidationAndProgress();

        private void Consent_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            // ItemCheck fires before the CheckedItems collection updates; defer update
            BeginInvoke(new Action(UpdateValidationAndProgress));
        }

        private void UpdateValidationAndProgress()
        {
            ValidateLocation();
            ValidateCategory();
            ValidateDescription();
            ValidateConsent();

            int completed = CompletedCount();
            _prg.Maximum = REQUIRED_TOTAL;
            _prg.Value = Math.Min(completed, _prg.Maximum);
            btnSubmit.Enabled = (completed == REQUIRED_TOTAL);

            _lblProgress.Text = completed switch
            {
                0 => "Start by adding location",
                < REQUIRED_TOTAL => $"{completed}/{REQUIRED_TOTAL} completed",
                _ => "All required fields completed"
            };
        }

        private void ValidateLocation()
        {
            bool ok = !string.IsNullOrWhiteSpace(txtLocation.Text);
            _errors.SetError(txtLocation, ok ? "" : "Location is required");
        }

        private void ValidateCategory()
        {
            bool ok = cmbCategory.SelectedIndex >= 0;
            _errors.SetError(cmbCategory, ok ? "" : "Select a category");
        }

        private void ValidateDescription()
        {
            bool ok = !string.IsNullOrWhiteSpace(txtDescription.Text);
            _errors.SetError(txtDescription, ok ? "" : "Add a brief description");
        }

        private void ValidateConsent()
        {
            bool ok = chkConsent.CheckedItems.Count > 0; // CheckedListBox only
            _errors.SetError(chkConsent, ok ? "" : "Consent is required");
        }

        private int CompletedCount()
        {
            int count = 0;
            if (string.IsNullOrWhiteSpace(_errors.GetError(txtLocation))) count++;
            if (string.IsNullOrWhiteSpace(_errors.GetError(cmbCategory))) count++;
            if (string.IsNullOrWhiteSpace(_errors.GetError(txtDescription))) count++;
            if (string.IsNullOrWhiteSpace(_errors.GetError(chkConsent))) count++;
            return count;
        }

        private void FocusFirstInvalid()
        {
            if (!string.IsNullOrWhiteSpace(_errors.GetError(txtLocation))) { txtLocation.Focus(); return; }
            if (!string.IsNullOrWhiteSpace(_errors.GetError(cmbCategory))) { cmbCategory.Focus(); cmbCategory.DroppedDown = true; return; }
            if (!string.IsNullOrWhiteSpace(_errors.GetError(txtDescription))) { txtDescription.Focus(); return; }
            if (!string.IsNullOrWhiteSpace(_errors.GetError(chkConsent))) { chkConsent.Focus(); return; }
        }

        // =============== Progress UI plumbing ===============

        private void BindProgressUi()
        {
            // If you already have prgReport/lblProgress in Designer, they’ll be used.
            _prg = prgReport ?? CreateProgressBar();
            _lblProgress = lblProgress ?? CreateProgressLabel();
        }

        private ProgressBar CreateProgressBar()
        {
            var bar = new ProgressBar
            {
                Name = "prgReport",
                Height = 12,
                Style = ProgressBarStyle.Continuous
            };

            // Place under attachments list; fallback to form if needed
            Control host = lstAttachments ?? (Control)this;
            bar.Width = host.Width;
            bar.Left = host.Left;
            bar.Top = host.Bottom + 6;

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

            lbl.Left = _prg.Left;
            lbl.Top = _prg.Bottom + 6;

            (_prg.Parent ?? this).Controls.Add(lbl);
            lbl.BringToFront();
            return lbl;
        }
    }
}
