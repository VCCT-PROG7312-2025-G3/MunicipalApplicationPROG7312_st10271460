using System;
using System.Drawing;
using System.Windows.Forms;

// I use the same project namespaces you’ve been using elsewhere.
using MunicipalApplicationPROG7312;                          // UiKit (runtime theming)
using MunicipalApplicationPROG7312.Localization;     // L10n
using MunicipalApplicationPROG7312.Models;           // AppSettings, AppTheme
using MunicipalApplicationPROG7312.Persistence;      // SettingsStore

namespace MunicipalApplicationPROG7312.UI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            // I let the Designer build the UI first.
            InitializeComponent();

            // I wire the events here so the Designer keeps rendering fine.
            this.Load += SettingsForm_Load;
            TryHook(btnSave, btnSave_Click);
            TryHook(btnCancel, btnCancel_Click);

            // I prefer runtime theme application here so the preview matches the app.
            ApplyRuntimeTheme();

            // I only add items if the Designer didn’t already add them.
            if (cmbLanguage != null && cmbLanguage.Items.Count == 0)
            {
                cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbLanguage.Items.AddRange(new object[]
                {
                    "English (en)", "Afrikaans (af)", "isiXhosa (xh)", "isiZulu (zu)"
                });
            }

            if (lblTheme != null && cmbTheme.Items.Count == 0)
            {
                cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTheme.Items.AddRange(new object[] { "Light", "Dark" });
            }

            if (numFontSize != null)
            {
                numFontSize.Minimum = 8;
                numFontSize.Maximum = 16;
                if (numFontSize.Value < 8 || numFontSize.Value > 16) numFontSize.Value = 10;
            }

            // I keep the header label text consistent if it exists.
            if (lblTitle != null) lblTitle.Text = "Settings";

            // I keep the credit label readable if you added it.
            if (lblCredit != null) lblCredit.ForeColor = Color.FromArgb(108, 117, 125);
        }

        // --- I populate the controls with the current saved values on load.
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var s = SettingsStore.Current;

            if (cmbLanguage != null)
                cmbLanguage.SelectedIndex = CodeToIndex(s.LanguageCode);

            if (cmbTheme != null)
                cmbTheme.SelectedIndex = s.Theme == AppTheme.Dark ? 1 : 0;

            if (numFontSize != null)
                numFontSize.Value = s.BaseFontSize;
        }

        // --- I save back to SettingsStore and close with OK.
        private void btnSave_Click(object sender, EventArgs e)
        {
            var lang = IndexToCode(cmbLanguage?.SelectedIndex ?? 0);
            var theme = (cmbTheme?.SelectedIndex ?? 0) == 1 ? AppTheme.Dark : AppTheme.Light;
            var size = (int)(numFontSize?.Value ?? 10);

            // I update L10n immediately so the app can reflect it.
            L10n.SetLanguage(lang);

            // I persist the new settings.
            var next = new AppSettings
            {
                LanguageCode = lang,
                Theme = theme,
                BaseFontSize = size
            };
            SettingsStore.Update(next);

            DialogResult = DialogResult.OK;
            Close();
        }

        // --- I cancel without saving.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // --- Helper: runtime theme without touching Designer properties.
        private void ApplyRuntimeTheme()
        {
            var s = SettingsStore.Current;
            UiKit.ApplyRuntimeTheme(this, s.Theme, s.BaseFontSize);
        }

        // --- Helper: language mapping (matches the combo order above).
        private int CodeToIndex(string code)
        {
            switch (code)
            {
                case "af": return 1;
                case "xh": return 2;
                case "zu": return 3;
                default: return 0; // en
            }
        }

        private string IndexToCode(int i)
        {
            switch (i)
            {
                case 1: return "af";
                case 2: return "xh";
                case 3: return "zu";
                default: return "en";
            }
        }

        // --- Helper: safe event hookup to avoid null refs if a control was renamed.
        private void TryHook(Button btn, EventHandler handler)
        {
            if (btn == null) return;
            btn.Click -= handler;
            btn.Click += handler;
        }
    }
}