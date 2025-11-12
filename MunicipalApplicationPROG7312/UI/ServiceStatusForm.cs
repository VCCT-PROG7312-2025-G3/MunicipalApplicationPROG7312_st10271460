using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312.UI
{
    public partial class ServiceStatusForm : Form, ISearchable
    {
        // --- Domain model ---
        private enum RequestStatus { New, InProgress, Paused, Resolved, Closed }

        private sealed class ServiceRequest
        {
            public int Id { get; init; }
            public string Category { get; init; } = "";
            public int Priority { get; set; }                // 1 = highest
            public RequestStatus Status { get; set; }
            public DateTime SubmittedAt { get; init; }
            public string Location { get; init; } = "";
            public string Eta => Status is RequestStatus.Resolved or RequestStatus.Closed
                ? "—"
                : $"{Math.Max(2, 24 - (int)(DateTime.Now - SubmittedAt).TotalHours)} h";
        }

        // --- Binding + indices (RB tree via SortedDictionary) ---
        private readonly BindingList<ServiceRequest> _items = new();
        private readonly SortedDictionary<int, ServiceRequest> _byId = new();

        // --- Priority queue (heap) + undo stack ---
        private readonly MinHeap<HeapItem> _heap = new();
        private readonly Stack<HeapItem> _undo = new();

        // --- AVL time index: SubmittedAt.Ticks -> Id ---
        private readonly AvlTree<long, int> _submittedIndex = new();

        // --- Basic binary tree + traversals (for demo/recommended default) ---
        private readonly BinaryTree<string> _categoryTree =
            BinaryTree<string>.FromArray(new[] { "Sanitation", "Roads", "Utilities", "Waste", "Other" });

        public ServiceStatusForm()
        {
            InitializeComponent();

            Load += ServiceStatusForm_Load;
            btnProcessNext.Click += btnProcessNext_Click;
            btnUndo.Click += btnUndo_Click;
            btnComputeMst.Click += btnComputeMst_Click;
            btnBack.Click += (_, __) => Close();

            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;

            gridEvents.AutoGenerateColumns = true;
            gridEvents.ReadOnly = true;
            gridEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lstUpNext.SelectionMode = SelectionMode.MultiExtended;
            lstUrgent.SelectionMode = SelectionMode.MultiExtended;
            lstRecommended.SelectionMode = SelectionMode.MultiExtended;
        }

        private void ServiceStatusForm_Load(object? sender, EventArgs e)
        {
            foreach (var r in Seed())
            {
                _items.Add(r);
                _byId[r.Id] = r;
                _heap.Push(new HeapItem(r));
                _submittedIndex.Insert(r.SubmittedAt.Ticks, r.Id);
            }

            gridEvents.DataSource = _items;

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All");
            cmbFilter.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Waste", "Other" });
            foreach (var id in _items.Select(i => $"#{i.Id}")) cmbFilter.Items.Add(id);
            cmbFilter.SelectedIndex = 0;

            RefreshQueues();
        }

        // ---------------- Heap ----------------
        private readonly record struct HeapItem(int Priority, DateTime When, int Id) : IComparable<HeapItem>
        {
            public HeapItem(ServiceRequest r) : this(r.Priority, r.SubmittedAt, r.Id) { }
            public int CompareTo(HeapItem other)
            {
                int byP = Priority.CompareTo(other.Priority);
                return byP != 0 ? byP : When.CompareTo(other.When);
            }
        }

        private sealed class MinHeap<T> where T : IComparable<T>
        {
            private readonly List<T> a = new();
            public int Count => a.Count;
            public void Push(T v) { a.Add(v); Up(a.Count - 1); }
            public T Pop()
            {
                var root = a[0];
                var last = a[^1];
                a.RemoveAt(a.Count - 1);
                if (a.Count > 0) { a[0] = last; Down(0); }
                return root;
            }
            public IEnumerable<T> PeekMany(int k)
            {
                var tmp = new List<T>();
                var res = new List<T>();
                while (a.Count > 0 && res.Count < k) { var x = Pop(); res.Add(x); tmp.Add(x); }
                foreach (var x in tmp) Push(x);
                return res;
            }
            private void Up(int i)
            {
                while (i > 0)
                {
                    int p = (i - 1) / 2;
                    if (a[p].CompareTo(a[i]) <= 0) break;
                    (a[p], a[i]) = (a[i], a[p]);
                    i = p;
                }
            }
            private void Down(int i)
            {
                int n = a.Count;
                while (true)
                {
                    int l = 2 * i + 1, r = l + 1, s = i;
                    if (l < n && a[l].CompareTo(a[s]) < 0) s = l;
                    if (r < n && a[r].CompareTo(a[s]) < 0) s = r;
                    if (s == i) break;
                    (a[s], a[i]) = (a[i], a[s]);
                    i = s;
                }
            }
        }

        // ---------------- AVL ----------------
        private sealed class AvlTree<TKey, TValue> where TKey : IComparable<TKey>
        {
            private sealed class Node
            {
                public TKey Key;
                public TValue Value;
                public Node? L, R;
                public int H = 1;
                public Node(TKey k, TValue v) { Key = k; Value = v; }
            }
            private Node? _root;

            public void Insert(TKey key, TValue value) => _root = Insert(_root, key, value);

            public bool TryGet(TKey key, out TValue value)
            {
                var n = _root;
                while (n != null)
                {
                    int cmp = key.CompareTo(n.Key);
                    if (cmp == 0) { value = n.Value; return true; }
                    n = cmp < 0 ? n.L : n.R;
                }
                value = default!;
                return false;
            }

            private static int H(Node? n) => n?.H ?? 0;
            private static int Bal(Node? n) => n is null ? 0 : H(n.L) - H(n.R);
            private static void Fix(Node n) => n.H = Math.Max(H(n.L), H(n.R)) + 1;

            private static Node RotR(Node y)
            {
                var x = y.L!; var T2 = x.R;
                x.R = y; y.L = T2;
                Fix(y); Fix(x);
                return x;
            }
            private static Node RotL(Node x)
            {
                var y = x.R!; var T2 = y.L;
                y.L = x; x.R = T2;
                Fix(x); Fix(y);
                return y;
            }

            private static Node Insert(Node? n, TKey k, TValue v)
            {
                if (n == null) return new Node(k, v);
                int cmp = k.CompareTo(n.Key);
                if (cmp < 0) n.L = Insert(n.L, k, v);
                else if (cmp > 0) n.R = Insert(n.R, k, v);
                else { n.Value = v; return n; }

                Fix(n);
                int b = Bal(n);
                if (b > 1 && k.CompareTo(n.L!.Key) < 0) return RotR(n);
                if (b < -1 && k.CompareTo(n.R!.Key) > 0) return RotL(n);
                if (b > 1 && k.CompareTo(n.L!.Key) > 0) { n.L = RotL(n.L!); return RotR(n); }
                if (b < -1 && k.CompareTo(n.R!.Key) < 0) { n.R = RotR(n.R!); return RotL(n); }
                return n;
            }
        }

        // ---------------- Basic binary tree + traversals ----------------
        private sealed class BinaryTree<T>
        {
            private sealed class Node { public T V; public Node? L, R; public Node(T v) { V = v; } }
            private Node? _root;

            public static BinaryTree<T> FromArray(T[] a)
            {
                var t = new BinaryTree<T>();
                foreach (var v in a) t.InsertLevelOrder(v);
                return t;
            }
            private void InsertLevelOrder(T v)
            {
                if (_root == null) { _root = new Node(v); return; }
                var q = new Queue<Node>();
                q.Enqueue(_root);
                while (q.Count > 0)
                {
                    var n = q.Dequeue();
                    if (n.L == null) { n.L = new Node(v); return; }
                    if (n.R == null) { n.R = new Node(v); return; }
                    q.Enqueue(n.L); q.Enqueue(n.R);
                }
            }
            public IEnumerable<T> Inorder()
            {
                var st = new Stack<Node>(); var cur = _root;
                while (cur != null || st.Count > 0)
                {
                    while (cur != null) { st.Push(cur); cur = cur.L; }
                    cur = st.Pop(); yield return cur.V; cur = cur.R;
                }
            }
            public IEnumerable<T> Preorder()
            {
                if (_root == null) yield break;
                var st = new Stack<Node>(); st.Push(_root);
                while (st.Count > 0)
                {
                    var n = st.Pop(); yield return n.V;
                    if (n.R != null) st.Push(n.R);
                    if (n.L != null) st.Push(n.L);
                }
            }
            public IEnumerable<T> Postorder()
            {
                if (_root == null) yield break;
                var st = new Stack<(Node n, bool v)>(); st.Push((_root, false));
                while (st.Count > 0)
                {
                    var (n, v) = st.Pop();
                    if (v) { yield return n.V; continue; }
                    st.Push((n, true));
                    if (n.R != null) st.Push((n.R, false));
                    if (n.L != null) st.Push((n.L, false));
                }
            }
        }

        // ---------------- Graph + Prim’s MST ----------------
        private sealed class Graph
        {
            private readonly Dictionary<string, List<(string to, double w)>> _adj = new();
            public void AddEdge(string a, string b, double w)
            {
                if (!_adj.ContainsKey(a)) _adj[a] = new();
                if (!_adj.ContainsKey(b)) _adj[b] = new();
                _adj[a].Add((b, w));
                _adj[b].Add((a, w));
            }

            public IEnumerable<(string a, string b, double w)> Prim(string start)
            {
                var seen = new HashSet<string> { start };
                var pq = new MinHeap<(double w, string a, string b)>();
                foreach (var (to, w) in _adj[start]) pq.Push((w, start, to));
                var res = new List<(string, string, double)>();

                while (pq.Count > 0)
                {
                    var (w, a, b) = pq.Pop();
                    if (seen.Contains(b)) continue;
                    seen.Add(b);
                    res.Add((a, b, w));
                    foreach (var (to, w2) in _adj[b])
                        if (!seen.Contains(to)) pq.Push((w2, b, to));
                }
                return res;
            }
        }

        // ---------------- Actions ----------------
        private void btnProcessNext_Click(object? sender, EventArgs e)
        {
            if (_heap.Count == 0) return;

            var hi = _heap.Pop();
            _undo.Push(hi);
            var req = _byId[hi.Id];
            req.Status = RequestStatus.InProgress;
            gridEvents.Refresh();
            RefreshQueues();
        }

        private void btnUndo_Click(object? sender, EventArgs e)
        {
            if (_undo.Count == 0) return;

            var last = _undo.Pop();
            var req = _byId[last.Id];
            req.Status = RequestStatus.New;
            _heap.Push(last);
            gridEvents.Refresh();
            RefreshQueues();
        }

        private void btnComputeMst_Click(object? sender, EventArgs e)
        {
            var depot = "Depot";
            var g = new Graph();
            var open = _items.Where(i => i.Status is RequestStatus.New or RequestStatus.InProgress).ToList();

            // depot → request
            foreach (var r in open)
                g.AddEdge(depot, $"R{r.Id}", DistFromDepot(r));

            // request ↔ request
            for (int i = 0; i < open.Count; i++)
                for (int j = i + 1; j < open.Count; j++)
                    g.AddEdge($"R{open[i].Id}", $"R{open[j].Id}",
                              DistByCategory(open[i].Category, open[j]));

            var mst = g.Prim(depot);
            lstRecommended.Items.Clear();
            foreach (var (a, b, w) in mst)
                lstRecommended.Items.Add($"{a} → {b}  ({w:F1})");
        }

        // Depot distance (time + priority)
        private static double DistFromDepot(ServiceRequest r) =>
            r.Priority + (DateTime.Now - r.SubmittedAt).TotalHours / 12.0;

        // Category-based pair distance
        private static double DistByCategory(string sourceCategory, ServiceRequest target) =>
            (sourceCategory == target.Category ? 1.0 : 2.5) + target.Priority * 0.1;

        private void cmbFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var text = cmbFilter.Text?.Trim() ?? "";
            if (text.StartsWith("#") && int.TryParse(text[1..], out int id) && _byId.TryGetValue(id, out var target))
            {
                _ = _submittedIndex.TryGet(target.SubmittedAt.Ticks, out _); // AVL demo

                foreach (DataGridViewRow row in gridEvents.Rows)
                {
                    if (row.DataBoundItem is ServiceRequest sr && sr.Id == id)
                    {
                        row.Selected = true;
                        gridEvents.FirstDisplayedScrollingRowIndex = Math.Max(0, row.Index - 2);
                        break;
                    }
                }
                return;
            }

            if (string.Equals(text, "All", StringComparison.OrdinalIgnoreCase))
                gridEvents.DataSource = _items;
            else
                gridEvents.DataSource = new BindingList<ServiceRequest>(
                    _items.Where(i => i.Category.Equals(text, StringComparison.OrdinalIgnoreCase)).ToList());

            gridEvents.Refresh();
            RefreshQueues();
        }

        // ---------------- Helpers ----------------
        private void RefreshQueues()
        {
            lstUpNext.Items.Clear();
            foreach (var h in _heap.PeekMany(5))
            {
                var r = _byId[h.Id];
                lstUpNext.Items.Add($"#{r.Id}  {r.Category}  P{r.Priority}  {r.Status}");
            }

            lstUrgent.Items.Clear();
            foreach (var r in _items.Where(x => x.Priority <= 2)
                                    .OrderBy(x => x.Priority)
                                    .ThenBy(x => x.SubmittedAt))
            {
                lstUrgent.Items.Add($"#{r.Id}  {r.Category}  P{r.Priority}");
            }

            if (lstRecommended.Items.Count == 0)
            {
                foreach (var cat in _categoryTree.Inorder())
                    lstRecommended.Items.Add(cat);
            }
        }

        private IEnumerable<ServiceRequest> Seed()
        {
            var now = DateTime.Now.AddHours(-10);
            return new[]
            {
                new ServiceRequest { Id=101, Category="Sanitation", Priority=1, Status=RequestStatus.New,          SubmittedAt=now.AddHours(-8),  Location="Ward 7"},
                new ServiceRequest { Id=102, Category="Roads",      Priority=3, Status=RequestStatus.New,          SubmittedAt=now.AddHours(-6),  Location="Ward 9"},
                new ServiceRequest { Id=103, Category="Utilities",  Priority=2, Status=RequestStatus.InProgress,   SubmittedAt=now.AddHours(-5),  Location="Ward 3"},
                new ServiceRequest { Id=104, Category="Waste",      Priority=2, Status=RequestStatus.New,          SubmittedAt=now.AddHours(-4),  Location="Ward 2"},
                new ServiceRequest { Id=105, Category="Other",      Priority=4, Status=RequestStatus.New,          SubmittedAt=now.AddHours(-3),  Location="Ward 1"},
                new ServiceRequest { Id=106, Category="Sanitation", Priority=1, Status=RequestStatus.New,          SubmittedAt=now.AddHours(-2),  Location="Ward 12"},
            };
        }

        // ---------------- ISearchable ----------------
        public void ApplySearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return;
            query = query.Trim();

            if (int.TryParse(query.TrimStart('#'), out var id) && _byId.ContainsKey(id))
            {
                cmbFilter.SelectedItem = $"#{id}";
                cmbFilter_SelectedIndexChanged(null, EventArgs.Empty);
                return;
            }

            var categories = new[] { "Sanitation", "Roads", "Utilities", "Waste", "Other" };
            var cat = categories.FirstOrDefault(c => c.Equals(query, StringComparison.OrdinalIgnoreCase));
            cmbFilter.SelectedItem = cat ?? "All";
            cmbFilter_SelectedIndexChanged(null, EventArgs.Empty);
        }
    }
}
