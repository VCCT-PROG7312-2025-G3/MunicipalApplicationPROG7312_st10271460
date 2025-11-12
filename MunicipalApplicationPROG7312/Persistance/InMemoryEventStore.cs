using System;
using System.Collections.Generic;
using MunicipalApplicationPROG7312.Domain;

namespace MunicipalApplicationPROG7312.Persistance
{
    /// <summary>
    /// Simple in-memory data source; you can swap this later for JSON/DB
    /// without changing UI by keeping the IEventStore interface.
    /// </summary>
    public sealed class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<int, LocalEvent> _events = new Dictionary<int, LocalEvent>();
        private int _nextId = 1;

        public InMemoryEventStore()
        {
            Seed();
        }

        public IEnumerable<LocalEvent> All() => _events.Values;
        public LocalEvent? GetById(int id) => _events.TryGetValue(id, out var e) ? e : null;

        public void Add(LocalEvent e)
        {
            e.Id = _nextId++;
            _events[e.Id] = e;
        }

        private void Seed()
        {
            // ------- Utilities -------
            Add(new LocalEvent
            {
                Title = "Planned Water Outage - Ward 61",
                Description = "Maintenance on main line. Affected streets listed on city site.",
                Category = EventCategory.Utilities,
                Start = DateTime.Today.AddDays(1).AddHours(9),
                End = DateTime.Today.AddDays(1).AddHours(16),
                Location = "Ward 61",
                IsAnnouncement = true,
                Urgency = 2,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "water", "maintenance", "outage", "ward61" }
            });

            Add(new LocalEvent
            {
                Title = "Electricity Maintenance – Ocean View Substation",
                Description = "Planned power interruption while teams replace switchgear.",
                Category = EventCategory.Utilities,
                Start = DateTime.Today.AddDays(4).AddHours(8),
                End = DateTime.Today.AddDays(4).AddHours(12),
                Location = "Ocean View",
                IsAnnouncement = true,
                Urgency = 2,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "electricity", "maintenance", "substation", "oceanview" }
            });

            Add(new LocalEvent
            {
                Title = "Burst Pipe Repair – Hout Bay Valley",
                Description = "Emergency crew on site. Low pressure expected in surrounding streets.",
                Category = EventCategory.Utilities,
                Start = DateTime.Today.AddHours(2),
                End = DateTime.Today.AddHours(6),
                Location = "Hout Bay",
                IsAnnouncement = true,
                Urgency = 3,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "water", "burstpipe", "emergency", "houtbay" }
            });

            // ------- Traffic -------
            Add(new LocalEvent
            {
                Title = "Road Closure: Main Rd 09:00–13:00",
                Description = "Parade route via Church St. Use alternative routes.",
                Category = EventCategory.Traffic,
                Start = DateTime.Today.AddDays(2).AddHours(9),
                End = DateTime.Today.AddDays(2).AddHours(13),
                Location = "Main Rd",
                IsAnnouncement = true,
                Urgency = 1,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "traffic", "closure", "parade" }
            });

            Add(new LocalEvent
            {
                Title = "Temporary Stop/Go – Sir Lowry’s Pass Village Rd",
                Description = "Single-lane traffic for resurfacing. Expect 10–15 min delays.",
                Category = EventCategory.Traffic,
                Start = DateTime.Today.AddDays(3).AddHours(7),
                End = DateTime.Today.AddDays(3).AddHours(17),
                Location = "Sir Lowry’s Pass Village",
                IsAnnouncement = true,
                Urgency = 1,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "traffic", "stopgo", "resurfacing", "delay" }
            });

            Add(new LocalEvent
            {
                Title = "Signal Fault – Koeberg Rd / Plattekloof Intersection",
                Description = "Treat as a four-way stop. Technicians dispatched.",
                Category = EventCategory.Traffic,
                Start = DateTime.Today.AddHours(1),
                End = DateTime.Today.AddHours(5),
                Location = "Milnerton",
                IsAnnouncement = true,
                Urgency = 2,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "traffic", "robots", "fault", "milnerton" }
            });

            // ------- Roads (maintenance & defects) -------
            Add(new LocalEvent
            {
                Title = "Pothole Blitz – Gugulethu NY 108",
                Description = "Roads team repairing multiple defects along corridor.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(1).AddHours(8),
                End = DateTime.Today.AddDays(1).AddHours(15),
                Location = "Gugulethu",
                IsAnnouncement = false,
                Urgency = 1,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "roads", "potholes", "maintenance", "gugulethu" }
            });

            Add(new LocalEvent
            {
                Title = "Resealing Programme – Voortrekker Rd (Paarl CBD)",
                Description = "Lane closures in sections; businesses remain accessible.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(6).AddHours(7),
                End = DateTime.Today.AddDays(6).AddHours(18),
                Location = "Paarl",
                IsAnnouncement = true,
                Urgency = 1,
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "roads", "reseal", "laneclosure", "paarl" }
            });

            // ------- Health -------
            Add(new LocalEvent
            {
                Title = "Health Screening Bus",
                Description = "Free BP and glucose testing.",
                Category = EventCategory.Health,
                Start = DateTime.Today.AddDays(5).AddHours(10),
                End = DateTime.Today.AddDays(5).AddHours(14),
                Location = "Pelican Park Library",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "clinic", "free", "wellness" }
            });

            Add(new LocalEvent
            {
                Title = "Mobile Clinic – Atlantis Civic Centre",
                Description = "Immunisations and basic check-ups. Bring ID/clinic card.",
                Category = EventCategory.Health,
                Start = DateTime.Today.AddDays(2).AddHours(9),
                End = DateTime.Today.AddDays(2).AddHours(13),
                Location = "Atlantis",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "health", "mobileclinic", "vaccination", "atlantis" }
            });

            Add(new LocalEvent
            {
                Title = "Mental Health Awareness Talk",
                Description = "Community session with local NGO counsellors.",
                Category = EventCategory.Health,
                Start = DateTime.Today.AddDays(7).AddHours(17),
                End = DateTime.Today.AddDays(7).AddHours(18).AddMinutes(30),
                Location = "Macassar Community Hall",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "health", "awareness", "talk", "macassar" }
            });

            // ------- Community -------
            Add(new LocalEvent
            {
                Title = "Community Clean-up: Strandfontein Dunes",
                Description = "Bring gloves. Bags provided. Families welcome.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(3).AddHours(8),
                End = DateTime.Today.AddDays(3).AddHours(11),
                Location = "Strandfontein Pavilion",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "cleanup", "beach", "volunteer" }
            });

            Add(new LocalEvent
            {
                Title = "Neighbourhood Watch Induction",
                Description = "Register and receive safety briefing with SAPS & CPF.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(4).AddHours(18),
                End = DateTime.Today.AddDays(4).AddHours(19).AddMinutes(30),
                Location = "Ocean View Community Centre",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "community", "safety", "watch", "saps" }
            });

            Add(new LocalEvent
            {
                Title = "Tree-Planting Day – Pelican Park",
                Description = "Greening initiative. Bring a hat and water bottle.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(9).AddHours(9),
                End = DateTime.Today.AddDays(9).AddHours(12),
                Location = "Pelican Park Sports Fields",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "environment", "trees", "volunteer", "pelicanpark" }
            });

            Add(new LocalEvent
            {
                Title = "Youth 5-a-side Soccer Tournament",
                Description = "Under-17. Free entry. Teams register online.",
                Category = EventCategory.Community,
                Start = DateTime.Today.AddDays(12).AddHours(10),
                End = DateTime.Today.AddDays(12).AddHours(15),
                Location = "Paarl East Multipurpose Centre",
                Tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "youth", "sport", "soccer", "paarl" }
            });
        }

    }
}
