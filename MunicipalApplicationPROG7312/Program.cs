using MunicipalApplicationPROG7312;
using MunicipalApplicationPROG7312.UI;
using System;
using System.Windows.Forms;

namespace MunicipalApplicationPROG7312
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();   // High-DPI + defaults for .NET 8
            Application.Run(new MainForm());         // Start on my main menu
        }
    }
}
