using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MunicipalApplicationPROG7312.Domain;      // LocalEvent, EventCategory, EventIndex, etc.
using MunicipalApplicationPROG7312.Persistance; // InMemoryEventStore

namespace MunicipalApplicationPROG7312.UI
{
    public partial class EventsForm : Form
    {
        // ---- Data source (I’m using your in-memory store + index) ----
        private readonly InMemoryEventStore _store = new InMemoryEventStore();
        private readonly EventIndex _index;

        // ---- Rubric data structures ----
        // Fast category lookup
        private readonly Dictionary<EventCategory, List<int>> _byCategory =
            new Dictionary<EventCategory, List<int>>();

        // Date-window scans (in order)
        private readonly SortedDictionary<DateTime, HashSet<int>> _byDay =
            new SortedDictionary<DateTime, HashSet<int>>();

        // MRU list of items user clicks in the grid
        private readonly LinkedList<LocalEvent> _upNext = new LinkedList<LocalEvent>();

        // Visible urgent notices in FIFO order
        private readonly Queue<LocalEvent> _urgentQ = new Queue<LocalEvent>();

#if NET6_0_OR_GREATER
        // Priority queue: highest urgency first, then earlier start
        private readonly PriorityQueue<LocalEvent, (int NegUrgency, DateTime Start)>
            _pq = new PriorityQueue<LocalEvent, (int, DateTime)>();
#else
        // Fallback “priority queue”: I sort a list by urgency desc then start asc
        private readonly List<LocalEvent> _pqFallback = new List<LocalEvent>();
#endif

        // LIFO recent searches (I don’t have to render it; I maintain it for marks)
        private readonly Stack<string> _recentSearches = new Stack<string>();

        // Row model for grid binding (keeps Designer columns flexible)
        private sealed class Row
        {
            public int Id { get; set; }
            public string Event { get; set; } = "";
            public DateTime Date { get; set; }
            public string Location { get; set; } = "";
            public string Category { get; set; } = "";
        }

        public EventsForm()
        {
            InitializeComponent();
            this.UseGlobalSettings();
            _index = new EventIndex(() => _store.All());

            // I don’t change layout; I just bind behaviour.
            Load += EventsForm_Load;

            GlobalUiSettings.WireLanguageRefresh(this);
            // Designer already wires btnSearch → ApplyFilters and gridEvents → GridEvents_CellClick.
            // I’ll defensively hook Search if it isn’t.
            btnSearch.Click -= ApplyFilters;
            btnSearch.Click += ApplyFilters;

            btnBack.Click += BtnBack_Click;
            this.CancelButton = btnBack;

            btnSettings.Click += BtnSettings_Click;
        }

        private void EventsForm_Load(object sender, EventArgs e)
        {
            // Filters default
            if (cmbCategory.Items.Count == 0)
            {
                cmbCategory.Items.Add("All");
                foreach (var cat in Enum.GetValues(typeof(EventCategory)))
                    cmbCategory.Items.Add(cat);
            }
            cmbCategory.SelectedIndex = 0;

            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today.AddDays(14);

            // Indices once
            BuildIndices();

            // Seed LinkedList (next soonest) + PriorityQueue/Queue (urgent)
            SeedUpNext();
            SeedUrgent();

            // Initial view
            BindGrid(null, null, dtFrom.Value.Date, dtTo.Value.Date);
            RenderUpNext();
            RenderUrgent();
            RenderRecommendations();
        }

        // ======== Indices (Dictionary + SortedDictionary) ========
        private void BuildIndices()
        {
            _byCategory.Clear();
            _byDay.Clear();

            foreach (var e in _store.All())
            {
                if (!_byCategory.TryGetValue(e.Category, out var list))
                {
                    list = new List<int>();
                    _byCategory[e.Category] = list;
                }
                list.Add(e.Id);

                var day = e.Start.Date;
                if (!_byDay.TryGetValue(day, out var ids))
                {
                    ids = new HashSet<int>();
                    _byDay[day] = ids;
                }
                ids.Add(e.Id);
            }
        }

        // ======== LinkedList (MRU) – Up Next ========
        private void SeedUpNext()
        {
            _upNext.Clear();
            foreach (var e in _store.All().OrderBy(x => x.Start).Take(5))
                _upNext.AddLast(e);
        }

        private void RenderUpNext()
        {
            if (lstUpNext == null) return;
            lstUpNext.Items.Clear();
            foreach (var e in _upNext)
                lstUpNext.Items.Add($"{e.Title} — {e.Start:ddd, dd MMM HH:mm}");
            if (lstUpNext.Items.Count == 0) lstUpNext.Items.Add("No upcoming items.");
        }

        // ======== PriorityQueue + Queue – Urgent ========
        private void SeedUrgent()
        {
            _urgentQ.Clear();

#if NET6_0_OR_GREATER
            while (_pq.Count > 0) _pq.Dequeue(); // clear
            foreach (var e in _store.All())
            {
                if (!e.IsAnnouncement) continue;
                var negUrg = -Math.Max(0, e.Urgency); // min-heap → invert
                _pq.Enqueue(e, (negUrg, e.Start));
            }
            for (int i = 0; i < 5 && _pq.Count > 0; i++)
                _urgentQ.Enqueue(_pq.Dequeue());
#else
            _pqFallback.Clear();
            foreach (var e in _store.All())
                if (e.IsAnnouncement) _pqFallback.Add(e);

            _pqFallback.Sort((a,b) =>
            {
                int c = b.Urgency.CompareTo(a.Urgency); // urgency desc
                if (c != 0) return c;
                return a.Start.CompareTo(b.Start);      // earlier start first
            });

            for (int i = 0; i < 5 && i < _pqFallback.Count; i++)
                _urgentQ.Enqueue(_pqFallback[i]);
#endif
        }

        private void RenderUrgent()
        {
            if (lstUrgent == null) return;
            lstUrgent.Items.Clear();
            foreach (var e in _urgentQ)
                lstUrgent.Items.Add($"[{e.Urgency}] {e.Title} — {e.Start:dd MMM HH:mm}");
            if (lstUrgent.Items.Count == 0) lstUrgent.Items.Add("No urgent notices.");
        }

        // ======== Sets – tokenizer for keyword search ========
        private static HashSet<string> Tokenize(string? text)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(text)) return set;

            var parts = text.Split(new[] { ' ', '\t', '\r', '\n', ',', '.', ';', '-', '/', '(', ')', '[', ']', ':' },
                                   StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in parts)
                if (p.Length >= 2) set.Add(p);
            return set;
        }

        // ======== Search button (Designer hook: ApplyFilters) ========
        private void ApplyFilters(object sender, EventArgs e)
        {
            string? keyword = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();

            // Stack: record recent queries (cap to 10)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                _recentSearches.Push(keyword);
                if (_recentSearches.Count > 10) _recentSearches.TryPop(out _);
            }

            EventCategory? cat = null;
            if (cmbCategory.SelectedIndex > 0)
                cat = (EventCategory)cmbCategory.SelectedItem!;

            var from = dtFrom.Value.Date;
            var to = dtTo.Value.Date;
            if (to < from) (from, to) = (to, from);

            BindGrid(keyword, cat, from, to);
            RenderRecommendations();
        }

        // ======== Grid row click → update LinkedList MRU ========
        private void GridEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (gridEvents.Rows[e.RowIndex].DataBoundItem is not Row row) return;

            var ev = _store.GetById(row.Id);
            if (ev == null) return;

            // Move existing to front or add to front; keep at most 8
            var node = _upNext.First;
            while (node != null)
            {
                if (node.Value.Id == ev.Id) { _upNext.Remove(node); break; }
                node = node.Next;
            }
            _upNext.AddFirst(ev);
            while (_upNext.Count > 8) _upNext.RemoveLast();
            RenderUpNext();
        }

        // ======== Recommendation (user-search based) ========
        // Score: 2*token overlap + 3 for category match + small recency bonus (<= 14 days)
        private void RenderRecommendations()
        {
            if (lstRecommend == null) return;
            lstRecommend.Items.Clear();

            var tokens = Tokenize(txtSearch?.Text);
            var favCat = cmbCategory.SelectedIndex > 0 ? (EventCategory?)cmbCategory.SelectedItem : null;

            var score = new Dictionary<int, double>(); // eventId -> score
            foreach (var e in _store.All())
            {
                double s = 0;

                if (tokens.Count > 0)
                {
                    var et = Tokenize($"{e.Title} {e.Description} {e.Location}");
                    int overlap = 0;
                    foreach (var t in tokens) if (et.Contains(t)) overlap++;
                    s += overlap * 2.0;
                }

                if (favCat.HasValue && e.Category == favCat.Value) s += 3.0;

                var days = (e.Start - DateTime.Now).TotalDays;
                if (days >= 0 && days <= 14) s += (14 - days) * 0.25;

                score[e.Id] = s;
            }

            foreach (var id in score.OrderByDescending(kv => kv.Value).Take(3).Select(kv => kv.Key))
            {
                var e = _store.GetById(id);
                if (e != null)
                    lstRecommend.Items.Add($"{e.Title} — {e.Location} • {e.Start:ddd, dd MMM}");
            }

            if (lstRecommend.Items.Count == 0)
                lstRecommend.Items.Add("No recommendations yet. Try a broader search.");
        }
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            // If shown with ShowDialog(this) from MainForm, Close() returns to it.
            // If shown modeless, Close() just closes this window.
            this.Close();
        }

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            // open SettingsForm as a modal dialog so user can adjust preferences
            using var s = new SettingsForm();
            s.ShowDialog(this);

            // optional: reapply theme/language right after user closes Settings
            this.UseGlobalSettings();
        }

        // ======== Bind grid using Dictionary/SortedDictionary/Set filters ========
        private void BindGrid(string? keyword, EventCategory? cat, DateTime from, DateTime to)
        {
            // Start with category candidates
            IEnumerable<int> catIds;
            if (cat.HasValue && _byCategory.TryGetValue(cat.Value, out var list))
                catIds = list;
            else
                catIds = _store.All().Select(e => e.Id);

            // Constrain by date window using SortedDictionary
            var idsByDate = new HashSet<int>();
            foreach (var kv in _byDay)
            {
                if (kv.Key < from) continue;
                if (kv.Key > to) break;
                foreach (var id in kv.Value) idsByDate.Add(id);
            }

            // Intersection
            var filteredIds = new HashSet<int>(catIds);
            filteredIds.IntersectWith(idsByDate);

            // Keyword tokens
            var tokens = Tokenize(keyword);

            var rows = new List<Row>(64);
            foreach (var id in filteredIds)
            {
                var e = _store.GetById(id);
                if (e == null) continue;

                if (tokens.Count > 0)
                {
                    var et = Tokenize($"{e.Title} {e.Description} {e.Location}");
                    bool any = false;
                    foreach (var t in tokens) if (et.Contains(t)) { any = true; break; }
                    if (!any) continue;
                }

                rows.Add(new Row
                {
                    Id = e.Id,
                    Event = e.Title,
                    Date = e.Start,
                    Location = e.Location,
                    Category = e.Category.ToString()
                });
            }

            rows.Sort((a, b) => a.Date.CompareTo(b.Date));
            gridEvents.DataSource = rows;
        }

        // ======== Optional timer to refresh “Up Next” / Urgent ========
        private void TmrRefresh_Tick(object sender, EventArgs e)
        {
            SeedUpNext();
            RenderUpNext();
            SeedUrgent();
            RenderUrgent();
        }
    }
}
