using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SortingStatus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public  WndProperties wp = new WndProperties();
        public static MainWindow main;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = wp;
                main = this;
                ucPrgBar.SetWndDataContext(wp);
                ucPrgBar.backgroundWorker.RunWorkerAsync();
            }
            catch(Exception e)
            {
                Trace.WriteLine($"Error while running progress bar. e = {e}");
            }
        }

        private void wndProgress_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
