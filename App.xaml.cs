using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace ProgressBar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutexOneAppInstance;
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            mutexOneAppInstance = new Mutex(false, "ChecksProgressBarBalamuth");
            if (!mutexOneAppInstance.WaitOne(TimeSpan.FromSeconds(0), false))
            {
                Trace.WriteLine("Progress bar already has an instance. [Mutex = ChecksProgressBarBalamuth]");
                Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
            }
        }

    }
}
