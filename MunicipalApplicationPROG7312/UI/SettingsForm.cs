using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalApplicationPROG7312;
using MunicipalApplicationPROG7312.Localization;
using MunicipalApplicationPROG7312.Models;
using MunicipalApplicationPROG7312.Persistence;

namespace MunicipalApplicationPROG7312.UI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            // Show the current theme/language/font on load
            this.Load += SettingsForm_Load;

            // Hook clicks (kept simple)
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Make the form itself match the app right now
            this.UseGlobalSettings();

            // Combo items (add once if Designer didn't)
            if (cmbLanguage.Items.Count == 0)
                cmbLanguage.Items.AddRange(new object[] { "English (en)", "Afrikaans (af)", "isiXhosa (xh)", "isiZulu (zu)" });

            if (cmbTheme.Items.Count == 0)
                cmbTheme.Items.AddRange(new object[] { "Light", "Dark" });

            numFontSize.Minimum = 8;
            numFontSize.Maximum = 18;
        }

        private void SettingsForm_Load(object? sender, EventArgs e)
        {
            var s = SettingsStore.Current;

            cmbLanguage.SelectedIndex = codeToIndex(s.LanguageCode);
            cmbTheme.SelectedIndex = s.Theme == AppTheme.Dark ? 1 : 0;
            numFontSize.Value = s.BaseFontSize;
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            var lang = indexToCode(cmbLanguage.SelectedIndex);
            var theme = (cmbTheme.SelectedIndex == 1) ? AppTheme.Dark : AppTheme.Light;
            var size = (int)numFontSize.Value;

            // Flip the language immediately so text keys translate
            L10n.SetLanguage(lang);

            // Push to the store so every open form updates themselves
            SettingsStore.Update(new AppSettings
            {
                LanguageCode = lang,
                Theme = theme,
                BaseFontSize = size
            });

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // UI helper mapping. Keep this boring and explicit.
        private static int codeToIndex(string code) => code switch
        {
            "af" => 1,
            "xh" => 2,
            "zu" => 3,
            _ => 0
        };

        private static string indexToCode(int i) => i switch
        {
            1 => "af",
            2 => "xh",
            3 => "zu",
            _ => "en"
        };


    }
}
