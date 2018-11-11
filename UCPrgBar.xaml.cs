using Delay;
using System;
using System.ComponentModel;
using System.IO.Pipes;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SortingStatus
{
    /// <summary>
    /// Interaction logic for UCPrgBar.xaml
    /// </summary>
    public partial class UCPrgBar : UserControl
    {
        public BackgroundWorker backgroundWorker = new BackgroundWorker();
        public ComponentResolution cr = new ComponentResolution();
        WndProperties wp;
        public static bool topMost = false;

        public UCPrgBar()
        {
            InitializeComponent();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            DataContext = cr;    
        }
        public void SetWndDataContext(WndProperties wp)
        {
            this.wp = wp;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private  void DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                double pass, general;
                string[] progress;
                
                string progressInfo = ReceiveSingleMessageFromClient();
                string[] info = progressInfo.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);//"value:(x,y) -> 50/80:(500,200)"
                //progress info
                progress = info[0].Split(new[] { '/', '\\' });
                pass = Convert.ToInt16(progress[0]);
                general = Convert.ToInt16(progress[1]);

                //location info
                string[] location = info[1].Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                wp.X = Convert.ToInt16(location[0]);
                wp.Y = Convert.ToInt16(location[1]);
                cr.PbValue = (int)((pass / general) * 100);
                backgroundWorker.ReportProgress(cr.PbValue);
                cr.Progress = pass > general ? $"? {pass}/{general} ?" : $"{pass}/{general}";
                MinimizeToTray.SetToolTip(cr.Progress);
                ShowMainWindow();
            }
            while (cr.PbValue != 100);

            if (cr.Beep)
            {
                Console.Beep();
            }
            Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
       
        private static string ReceiveSingleMessageFromClient()
        {
            string message = "";
            try
            {
                string namedPipe = System.Configuration.ConfigurationManager.AppSettings["NamedPipe"];
                using (NamedPipeServerStream namedPipeServer = new NamedPipeServerStream(namedPipe, PipeDirection.InOut,
                                                                                       1, PipeTransmissionMode.Message))
                {
                    namedPipeServer.WaitForConnection();
                    StringBuilder messageBuilder = new StringBuilder();
                    string messageChunk = string.Empty;
                    byte[] messageBuffer = new byte[10];
                    do
                    {
                        namedPipeServer.Read(messageBuffer, 0, messageBuffer.Length);
                        messageChunk = Encoding.UTF8.GetString(messageBuffer);
                        messageBuilder.Append(messageChunk);
                        messageBuffer = new byte[messageBuffer.Length];
                    }
                    while (!namedPipeServer.IsMessageComplete);
                    message = messageBuilder.ToString();
                    System.Diagnostics.Trace.WriteLine(message);
                }
            }
            catch(Exception e)
            {
                message = "";
            }
            return message;
        }

        private void lblMinimize_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
            {
                ///MainWindow.main.Visibility = Visibility.Hidden;
                MainWindow.main.ShowInTaskbar = true;
                MainWindow.main.WindowState = WindowState.Minimized;
                MainWindow.main.ShowInTaskbar = false;
            }));
        }
        private void ShowMainWindow()
        {
            MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
            {
               // MainWindow.main.Visibility = Visibility.Visible;
                MainWindow.main.WindowState = WindowState.Normal;
               
            }));
        }

        private void cbTopMost_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
            {
                MainWindow.main.Topmost = true;
                topMost = true;
            }));
        }

        private void cbTopMost_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Topmost = false;
            topMost = false;
        }
    }
}
