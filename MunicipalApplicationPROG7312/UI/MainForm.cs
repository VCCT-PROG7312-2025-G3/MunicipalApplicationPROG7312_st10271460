using System;
using System.Windows.Forms;
using System.Drawing;  // Bitmap, Image


namespace MunicipalApplicationPROG7312.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

           

            btnTileReports.Click += BtnTileReports_Click;
            btnTileEvents.Click += BtnTileEvents_Click;
            btnTileStatus.Click += BtnTileStatus_Click;
            btnSearch.Click += BtnSearch_Click;
        }

        // --- tile handlers ---
        private void BtnTileReports_Click(object? sender, EventArgs e)
        {
            using var f = new ReportIssueForm();
            f.ShowDialog(this);   // modal; use f.Show() if you want modeless
        }

        private void BtnTileEvents_Click(object? sender, EventArgs e)
        {
            using var f = new EventsForm();
            f.ShowDialog(this);
        }

        private void BtnTileStatus_Click(object? sender, EventArgs e)
        {
            using var f = new ServiceStatusForm();
            f.ShowDialog(this);
        }

        // --- search handler ---
        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            var q = txtSearch.Text?.Trim();
            if (string.IsNullOrWhiteSpace(q)) return;

            // Light intent routing by keywords
            if (q.IndexOf("event", StringComparison.OrdinalIgnoreCase) >= 0 ||
                q.IndexOf("announce", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                using var f = new EventsForm();
                (f as ISearchable)?.ApplySearch(q);   // if EventsForm implements it later
                f.ShowDialog(this);
                return;
            }

            if (q.IndexOf("report", StringComparison.OrdinalIgnoreCase) >= 0 ||
                q.IndexOf("issue", StringComparison.OrdinalIgnoreCase) >= 0 ||
                q.IndexOf("location", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                using var f = new ReportIssueForm();
                (f as ISearchable)?.ApplySearch(q);   // optional in future
                f.ShowDialog(this);
                return;
            }

            // Default: service requests (IDs, categories)
            using (var s = new ServiceStatusForm())
            {
                (s as ISearchable)?.ApplySearch(q);
                s.ShowDialog(this);
            }
        }


        private void btnTileEvents_Click_1(object sender, EventArgs e)
        {

        }
    }
}
