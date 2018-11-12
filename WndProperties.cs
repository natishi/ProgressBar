using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;


namespace SortingStatus
{
    public class WndProperties : INotifyPropertyChanged
    {
        private double _MonitorWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        private double _MonitorHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

        private double wndWidth;
        private double wndHeight;
        private double ratioWidth;
        private double ratioHeight;
        private string wndBackground;
        private double opacity;
        private string title;

        private string show;
        private  double x;
        private double y;

               
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public WndProperties()
        {
            RatioWidth = Convert.ToDouble(ConfigurationManager.AppSettings["RatioWidth"]);
            RatioHeight = Convert.ToDouble(ConfigurationManager.AppSettings["RatioHeight"]);
            WndBackground = ConfigurationManager.AppSettings["WndBackground"];
            Opacity = Convert.ToDouble(ConfigurationManager.AppSettings["Opacity"]);
            string[] coordination = ConfigurationManager.AppSettings["Location"].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            X = Convert.ToDouble(coordination[0]);
            Y = Convert.ToDouble(coordination[1]);
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

        public double RatioWidth
        {
            get
            {
                return ratioWidth;
            }
            set
            {
                double valid = value <= 0 || value > 1 ? 1 : value;
                ratioWidth = valid;
                WndWidth = _MonitorWidth * ratioWidth;
                Trace.WriteLine($"RatioWidth = {ratioWidth}");
            }
        }

        public double RatioHeight
        {
            get
            {
                return ratioHeight;
            }
            set
            {
                double valid = value <= 0 || value > 1 ? 1 : value;
                ratioHeight = valid;
                WndHeight= _MonitorWidth * ratioHeight;
                Trace.WriteLine($"RatioHeight = {ratioHeight}");
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
        
        public double X
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
                    double valid = value >= _MonitorWidth || value <= 0 ? _MonitorWidth / 2 : value;
                    x = valid;
                    NotifyPropertyChanged("X");
                    Trace.WriteLine($"X = {x}");
                    //Thread.Sleep(20);    
                }
                updlocation = false;
            }
        }

        public double Y
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
                    double valid = value >= _MonitorHeight || value <= 0 ? _MonitorHeight / 2 : value;
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
