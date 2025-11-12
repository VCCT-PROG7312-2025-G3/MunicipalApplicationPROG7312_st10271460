// UI/ServiceStatusForm.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312.UI
{
    public partial class ServiceStatusForm : Form
    {
        private enum ReqStatus { New, Assigned, InProgress, Resolved }
        private record ServiceRequest(int Id, string Category, string Area, int Severity, DateTime Created, ReqStatus Status);

        private readonly Dictionary<int, ServiceRequest> _byId = new();
        private readonly SortedDictionary<DateTime, List<ServiceRequest>> _byDate = new();
        private readonly HashSet<string> _areas = new(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<ServiceRequest> _history = new();
        private readonly Queue<ServiceRequest> _fifoQueue = new();
        private readonly PriorityQueue<ServiceRequest, int> _priority = new();

        private class BstNode { public ServiceRequest Item; public BstNode? Left; public BstNode? Right; public BstNode(ServiceRequest i) { Item = i; } }
        private BstNode? _root;

        private class Edge { public string A; public string B; public int W; public Edge(string a, string b, int w) { A = a; B = b; W = w; } }
        private readonly Dictionary<string, List<Edge>> _graph = new(StringComparer.OrdinalIgnoreCase);

        public ServiceStatusForm()
        {
            InitializeComponent();
            
        }

       
    }
}
