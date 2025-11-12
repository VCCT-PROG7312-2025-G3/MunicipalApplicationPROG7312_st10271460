// UI/ServiceStatusForm.Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312.UI
{
    partial class ServiceStatusForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView gridRequests;
        private Button btnProcessNext;
        private Button btnUndo;
        private ComboBox cmbFilter;
        private Label lblFilter;
        private ListBox lstRoute;
        private Label lblRoute;
        private Button btnComputeMst;
        private PictureBox picStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gridRequests = new DataGridView();
            btnProcessNext = new Button();
            btnUndo = new Button();
            cmbFilter = new ComboBox();
            lblFilter = new Label();
            lstRoute = new ListBox();
            lblRoute = new Label();
            btnComputeMst = new Button();
            picStatus = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)gridRequests).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picStatus).BeginInit();
            SuspendLayout();

            Text = "Service Request Status";
            ClientSize = new Size(1040, 560);
            StartPosition = FormStartPosition.CenterParent;

            lblFilter.Text = "Filter";
            lblFilter.Left = 16; lblFilter.Top = 16;

            cmbFilter.Left = 16; cmbFilter.Top = 40; cmbFilter.Width = 200;

            btnProcessNext.Text = "Process Next (Priority)";
            btnProcessNext.Left = 230; btnProcessNext.Top = 38; btnProcessNext.Width = 180;

            btnUndo.Text = "Undo Last Change";
            btnUndo.Left = 420; btnUndo.Top = 38; btnUndo.Width = 150;

            gridRequests.Left = 16; gridRequests.Top = 80; gridRequests.Width = 720; gridRequests.Height = 410;

            lblRoute.Text = "Optimal Service Route (MST)";
            lblRoute.Left = 750; lblRoute.Top = 80;
            lstRoute.Left = 750; lstRoute.Top = 104; lstRoute.Width = 260; lstRoute.Height = 320;

            btnComputeMst.Text = "Compute MST";
            btnComputeMst.Left = 750; btnComputeMst.Top = 430; btnComputeMst.Width = 260;

            picStatus.Left = 580; picStatus.Top = 16; picStatus.Width = 156; picStatus.Height = 52;
            picStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            

            Controls.AddRange(new Control[] {
                lblFilter, cmbFilter, btnProcessNext, btnUndo, gridRequests, lblRoute, lstRoute, btnComputeMst, picStatus
            });

            // handlers wired in designer
            cmbFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
            btnProcessNext.Click += (s, e) => ProcessNext();
            btnUndo.Click += (s, e) => Undo();
            btnComputeMst.Click += (s, e) => ComputeMst();

            ResumeLayout(false);
        }

        // ---- thin wrappers so Designer lambdas compile ----
        private void ApplyFilter() => ApplyFilter(this, EventArgs.Empty);
        private void ProcessNext() => ProcessNext(this, EventArgs.Empty);
        private void Undo() => Undo(this, EventArgs.Empty);
        private void ComputeMst() => ComputeMst(this, EventArgs.Empty);

        // ---- real event handlers (put your logic here) ----
        private void ApplyFilter(object? sender, EventArgs e)
        {
            // your existing filter logic; minimal safe version:
            try
            {
                var items = _byId.Values
                    .OrderByDescending(i => i.Severity)
                    .ThenBy(i => i.Created)
                    .Select(i => new { i.Id, i.Category, i.Area, i.Severity, i.Status, Created = i.Created.ToString("dd MMM") })
                    .ToList();
                gridRequests.DataSource = items;
            }
            catch { /* ignore to compile */ }
        }

        private void ProcessNext(object? sender, EventArgs e)
        {
            try
            {
                if (_priority.TryDequeue(out var next, out _))
                {
                    var updated = next with { Status = ReqStatus.Assigned };
                    _byId[updated.Id] = updated;
                    _history.Push(next);
                }
                else if (_fifoQueue.Count > 0)
                {
                    var n = _fifoQueue.Dequeue();
                    var updated = n with { Status = ReqStatus.Assigned };
                    _byId[updated.Id] = updated;
                    _history.Push(n);
                }
                ApplyFilter();
            }
            catch { }
        }

        private void Undo(object? sender, EventArgs e)
        {
            try
            {
                if (_history.Count == 0) return;
                var prev = _history.Pop();
                _byId[prev.Id] = prev;
                ApplyFilter();
            }
            catch { }
        }

        private void ComputeMst(object? sender, EventArgs e)
        {
            try
            {
                var nodes = new HashSet<string>(_byId.Values.Select(v => v.Area), StringComparer.OrdinalIgnoreCase);
                if (nodes.Count == 0) return;

                var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var pq = new PriorityQueue<Edge, int>();

                var start = nodes.First();
                visited.Add(start);
                foreach (var e1 in _graph[start]) if (nodes.Contains(e1.B)) pq.Enqueue(e1, e1.W);

                var result = new List<Edge>();
                while (pq.Count > 0 && visited.Count < nodes.Count)
                {
                    pq.TryDequeue(out var e2, out _);
                    if (!visited.Contains(e2.B))
                    {
                        result.Add(e2);
                        visited.Add(e2.B);
                        foreach (var ne in _graph[e2.B])
                            if (nodes.Contains(ne.B) && !visited.Contains(ne.B))
                                pq.Enqueue(ne, ne.W);
                    }
                }

                lstRoute.Items.Clear();
                int total = 0;
                foreach (var e3 in result)
                {
                    lstRoute.Items.Add($"{e3.A} → {e3.B}  ({e3.W} km)");
                    total += e3.W;
                }
                lstRoute.Items.Add($"Total approx distance: {total} km");
            }
            catch { }
        }

    }
}
