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
using System.Windows.Threading;

namespace _8thMeet
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> hours = new List<string>();
        List<string> minutes = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();
        string strSelectTime = "";
        DispatcherTimer timerAlert = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i <= 23; i++)
                hours.Add(string.Format("{0:00}", i));
            for (int i = 0; i <= 59; i++)
                minutes.Add(string.Format("{0:00}", i));
            CmbHour.ItemsSource = hours;
            CmbMin.ItemsSource = minutes;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_tick);
            timer.Start();
            timerAlert.Interval = TimeSpan.FromSeconds(1);
            timerAlert.Tick += new EventHandler(TimerAlert_tick);
            MeSound.LoadedBehavior = MediaState.Stop;
        }
        private void Timer_tick(object sender, EventArgs e)
        {
            TxtTime.Text = DateTime.Now.ToString("HH:mm:ss");  
            TxtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TxtWeekDay.Text = DateTime.Now.ToString("dddd");
        }
        private void TimerAlert_tick(object sender, EventArgs e)
        {
            if (strSelectTime == DateTime.Now.ToString("HH:mm"))
            {
                MeSound.LoadedBehavior = MediaState.Play;
                timerAlert.Stop();
            }
        }
        private void BtnSetAlert_Click(object sender, RoutedEventArgs e)
        {
            timerAlert.Start();
            BtnSetAlert.IsEnabled = false;
            BtnCancelAlert.IsEnabled = true;
            strSelectTime = CmbHour.SelectedItem + ":" + CmbMin.SelectedItem;
        }

        private void BtnCancelAlert_Click(object sender, RoutedEventArgs e)
        {
            MeSound.LoadedBehavior = MediaState.Stop;
            timerAlert.Stop();
            BtnSetAlert.IsEnabled = true;
            BtnCancelAlert.IsEnabled = false;
        }
        private void meSound_MediaEnded(object sender, RoutedEventArgs e)
        {
            MeSound.Position = new TimeSpan(0, 0, 1);
            MeSound.LoadedBehavior = MediaState.Play;
        }
    }
}
