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

            // Wire events here (not in Designer)
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
            // Replace with your actual search action if needed
            MessageBox.Show($"Search: {txtSearch.Text}", "Search");
        }

        private void btnTileEvents_Click_1(object sender, EventArgs e)
        {

        }
    }
}
