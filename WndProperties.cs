using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;


namespace SortingStatus
{
    public class WndProperties : INotifyPropertyChanged
    {
        private int _MonitorWidth = SystemInformation.VirtualScreen.Width;
        private int _MonitorHeight = SystemInformation.VirtualScreen.Height; 

        private double wndWidth;
        private double wndHeight;
        private double ratio;
        private string wndBackground;
        private double opacity;
        private string title;

        private string show;
        private  int x;
        private  int y;

               
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public WndProperties()
        {
            Ratio = Convert.ToDouble(ConfigurationManager.AppSettings["Ratio"]);
            WndBackground = ConfigurationManager.AppSettings["WndBackground"];
            Opacity = Convert.ToDouble(ConfigurationManager.AppSettings["Opacity"]);
            string[] coordination = ConfigurationManager.AppSettings["Location"].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            X = Convert.ToInt16(coordination[0]);
            Y = Convert.ToInt16(coordination[1]);
            Show = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowProgressBar"]) == true ? "Visible" : "Hidden";
            Title = ConfigurationManager.AppSettings["WindowHeader"];
        }

        public double Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                double valid = value <= 0 || value > 1 ? 1 : value;
                opacity = valid;
                NotifyPropertyChanged("Opacity");
                Trace.WriteLine($"Opacity = {opacity}");
            }
        }
        public double WndHeight
        {
            get
            {
                return wndHeight;
            }
            set
            {
                wndHeight = value;
                NotifyPropertyChanged("WndHeight");
                Trace.WriteLine($"WndHeight = {wndHeight}");
            }
        }
        public double WndWidth
        {
            get
            {
                return wndWidth;
            }
            set
            {
                wndWidth = value;
                NotifyPropertyChanged("WndWidth");
                Trace.WriteLine($"WndWidth = {wndWidth}");
            }
        }
        public double Ratio
        {
            get
            {
                return ratio;
            }
            set
            {
                double valid = value <= 0 || value > 1 ? 1 : value;
                ratio = valid;
                WndWidth = _MonitorWidth * ratio;//System.Windows.SystemParameters.PrimaryScreenWidth * ratio;
                WndHeight = _MonitorHeight * ratio;//System.Windows.SystemParameters.PrimaryScreenHeight * ratio;
                NotifyPropertyChanged("Ratio");
                Trace.WriteLine($"Ratio = {ratio}");
            }
        }
        public string WndBackground
        {
            get
            {
                return wndBackground;
            }
            set
            {
                wndBackground = value;
                NotifyPropertyChanged("WndBackground");
                Trace.WriteLine($"WndBackground = {wndBackground}");
            }
        }
        public string Show
        {
            get
            {
                return show;
            }
            set
            {
                show = value;
                NotifyPropertyChanged("Show");
                Trace.WriteLine($"Show = {show}");
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
                Trace.WriteLine($"Title = {title}");
            }
        }
        private volatile bool updlocation = false;
        
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (updlocation == false)
                {
                    updlocation = true;
                    int valid = value >= _MonitorWidth || value <= 0 ? _MonitorWidth / 2 : value;
                    x = valid;
                    NotifyPropertyChanged("X");
                    Trace.WriteLine($"X = {x}");
                    //Thread.Sleep(20);    
                }
                updlocation = false;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (updlocation == false)
                {
                    updlocation = true;
                    int valid = value >= _MonitorHeight || value <= 0 ? _MonitorHeight / 2 : value;
                    y = valid;
                    NotifyPropertyChanged("Y");
                    Trace.WriteLine($"Y = {Y}");
                    //Thread.Sleep(20);
                }
                updlocation = false;
            }
        }
    }
}
