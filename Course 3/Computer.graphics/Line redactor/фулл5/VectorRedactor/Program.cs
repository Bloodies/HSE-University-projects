using System;
using System.Windows.Forms;
using VectorRedactor.UI;

namespace VectorRedactor
{
    internal static class Program
    {
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowForm());
        }
    }
}