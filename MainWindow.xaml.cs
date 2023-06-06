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
using System.Diagnostics;

namespace _8thMeet
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        // 時鐘
        List<string> hours = new List<string>();
        List<string> minutes = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();
        string strSelectTime = "";
        DispatcherTimer timerAlert = new DispatcherTimer();
        //碼表
        List<string> StopWatchLog = new List<string>();
        DispatcherTimer timerStopWatch = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            //時鐘
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
            //碼表
            timerStopWatch.Interval = TimeSpan.FromMilliseconds(1);
            timerStopWatch.Tick += new EventHandler(timerStopWatch_tick);
        }
        //時鐘
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
        //碼表
        private void timerStopWatch_tick(object sender, EventArgs e)
        {
            TxtStopWatch.Text = sw.Elapsed.ToString("hh':'mm':'ss':'fff");
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

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();             
            timerStopWatch.Start(); 
        }


        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            timerStopWatch.Stop();
            TxtStopWatch.Text = "00:00:00:000";
            TxtStopWatchLog.Text = "";
            StopWatchLog.Clear();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                TxtStopWatchLog.Text = "";
                if (StopWatchLog.Count == 50)
                    StopWatchLog.RemoveAt(0);
                StopWatchLog.Add(TxtStopWatch.Text);
                int i = 1;
                while (i <= StopWatchLog.Count)
                {
                    TxtStopWatchLog.Text += String.Format("第 {0} 筆紀錄：{1}", i.ToString(), StopWatchLog[StopWatchLog.Count - 1] + "\n");
                    i++;
                }
                sw.Restart();
            }
            else
            {
                sw.Reset();
                TxtStopWatch.Text = "00:00:00:000";
            }
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();
            timerStopWatch.Stop();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            TxtStopWatchLog.Text = "";
            if (StopWatchLog.Count == 50)
                StopWatchLog.RemoveAt(0);
            StopWatchLog.Add(TxtStopWatch.Text);

            int i = 1;
            while (i <= StopWatchLog.Count)
            {
                TxtStopWatchLog.Text += String.Format("第 {0} 筆紀錄：{1}", i.ToString(), StopWatchLog[StopWatchLog.Count - 1] + "\n");
                i++;
            }
        }
    }
}
