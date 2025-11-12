using System.Drawing;
using System.Windows.Forms;
using MunicipalApplicationPROG7312.Models;

namespace MunicipalApplicationPROG7312
{
    // Keep this pragmatic: apply theme + base font to a form and all children.
    public static class UiKit
    {
        public static void ApplyRuntimeTheme(Form form, AppTheme theme, int baseFontSize)
        {
            if (form == null || form.IsDisposed) return;

            // Pick colors per theme. Tweak to taste.
            Color back, fore, panel;
            if (theme == AppTheme.Dark)
            {
                back = Color.FromArgb(28, 30, 34);
                panel = Color.FromArgb(42, 45, 50);
                fore = Color.Gainsboro;
            }
            else
            {
                back = Color.White;
                panel = Color.FromArgb(245, 246, 248);
                fore = Color.Black;
            }

            // We apply once at the form level then walk all children.
            form.BackColor = back;
            form.ForeColor = fore;
            var font = new Font(form.Font.FontFamily, baseFontSize, form.Font.Style);

            void Recurse(Control c)
            {
                // Don’t fight owner drawn controls too hard; just do the basics
                c.Font = font;
                if (c is Panel or GroupBox or TabPage)
                    c.BackColor = panel;
                else if (c is Button)
                {
                    // Buttons read better with panel background
                    c.BackColor = panel;
                    c.ForeColor = fore;
                }
                else
                {
                    c.BackColor = back;
                    c.ForeColor = fore;
                }

                foreach (Control child in c.Controls)
                    Recurse(child);
            }

            Recurse(form);
        }
    }
}
