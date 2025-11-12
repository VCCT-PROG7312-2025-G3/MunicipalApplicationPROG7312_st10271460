// UI/FormExtensions.cs
using System;
using System.Windows.Forms;
using MunicipalApplicationPROG7312.Localization;   // L10n.ApplyTo
using MunicipalApplicationPROG7312.Models;         // AppTheme, etc. (only for clarity here)
using MunicipalApplicationPROG7312.Persistence;    // SettingsStore

namespace MunicipalApplicationPROG7312.UI
{
    // Small helper so every form can “opt-in” to global theme + language + font.
    public static class FormExtensions
    {
        /// <summary>
        /// Call this once in a form’s ctor (after InitializeComponent).
        /// It applies current settings immediately and re-applies whenever settings change.
        /// </summary>
        public static void UseGlobalSettings(this Form form)
        {
            if (form is null || form.IsDisposed) return;

            // 1) Apply the current snapshot right now.
            var s = SettingsStore.Current;
            UiKit.ApplyRuntimeTheme(form, s.Theme, s.BaseFontSize);  // paint colors + fonts
            L10n.ApplyTo(form);                                       // swap text via Tag keys

            // 2) Keep in sync while the form is open.
            void OnChanged(object? _, EventArgs __)
            {
                if (form.IsDisposed) return;
                var cur = SettingsStore.Current;
                UiKit.ApplyRuntimeTheme(form, cur.Theme, cur.BaseFontSize);
                L10n.ApplyTo(form);
                form.Refresh();
            }

            // Subscribe on create; unsubscribe on close to avoid leaks.
            SettingsStore.Changed += OnChanged;
            form.FormClosed += (_, __) => SettingsStore.Changed -= OnChanged;
        }
    }
}
