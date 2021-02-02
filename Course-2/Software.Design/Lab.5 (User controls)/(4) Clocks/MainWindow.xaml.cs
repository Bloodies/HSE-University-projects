using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Lab._5__User_controls_._4__Clocks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Timer timer = new Timer(100);
            timer.Elapsed += obn;
            timer.Start();
        }
        private void obn(object sender, ElapsedEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() => clockPerm.TimeSpan = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Ekaterinburg Standard Time")));
                Dispatcher.Invoke(() => clockMoscow.TimeSpan = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")));
                Dispatcher.Invoke(() => clockNewYork.TimeSpan = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")));
            }
            catch { }
        }
    }
}
