using System;
using MunicipalApplicationPROG7312.Models;

namespace MunicipalApplicationPROG7312.Persistence
{
    // Small singleton-like store. Persist to disk later if you want.
    public static class SettingsStore
    {
        // What the whole app currently uses
        public static AppSettings Current { get; private set; } = new AppSettings();

        // Everyone can subscribe to this to refresh themselves
        public static event EventHandler? Changed;

        // Swap in a new snapshot and tell everyone
        public static void Update(AppSettings next)
        {
            if (next == null) return;
            Current = next;
            Changed?.Invoke(null, EventArgs.Empty);
        }

        // Convenience when just tweaking one property
        public static void Mutate(Action<AppSettings> change)
        {
            var s = new AppSettings
            {
                LanguageCode = Current.LanguageCode,
                Theme = Current.Theme,
                BaseFontSize = Current.BaseFontSize
            };
            change?.Invoke(s);
            Update(s);
        }
    }
}
