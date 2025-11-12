using System;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312.UI
{
    /// <summary>
    /// Central place to broadcast language/theme changes to all open forms.
    /// </summary>
    public static class GlobalUiSettings
    {
        // Other code can subscribe to this to know when language (or theme) changes.
        public static event EventHandler? LanguageChanged;

        // Call this from SettingsForm after user picks a new language.
        public static void RaiseLanguageChanged()
        {
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Wire a form so that it:
        /// 1) Applies current global settings now.
        /// 2) Re-applies settings whenever the language changes.
        /// 3) Unsubscribes automatically when the form closes.
        /// </summary>
        public static void WireLanguageRefresh(Form form)
        {
            // 1) Apply current theme/language immediately
            form.UseGlobalSettings();

            // 2) Subscribe to change event
            void OnLanguageChanged(object? sender, EventArgs e)
            {
                // Skip if form already disposed
                if (form.IsDisposed) return;

                // Re-run your existing extension that updates all text/colors
                form.UseGlobalSettings();
            }

            LanguageChanged += OnLanguageChanged;

            // 3) Unsubscribe when this form closes so we don't leak handlers
            form.FormClosed += (_, __) => LanguageChanged -= OnLanguageChanged;
        }
    }
}
