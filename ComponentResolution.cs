using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;

namespace SortingStatus
{
    public class  ComponentResolution : INotifyPropertyChanged
    {
        private static double ratio;
        private string pbColor;
        private string progress;
        private int pbValue;
        private string windowHeader;
        private bool beep;
 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ComponentResolution()
        {
            Ratio = Convert.ToDouble(ConfigurationManager.AppSettings["Ratio"]);
            PbColor = ConfigurationManager.AppSettings["PbColor"];
            Progress = $"0/?";
            PbValue = 0;
            WindowHeader = ConfigurationManager.AppSettings["WindowHeader"];
            beep = Convert.ToBoolean(ConfigurationManager.AppSettings["BeepSound"]);
        }

        public  double Ratio
        {
            get
            {
                return ratio;
            }
            set
            {
                double valid = value <= 0 || value > 1 ? 1 : value;
                ratio = valid;
                NotifyPropertyChanged("Ratio");
                Trace.WriteLine($"Ratio = {ratio}");
            }
        }
        public string PbColor
        {
            get
            {
                return pbColor;
            }
            set
            {
                pbColor = value;
                NotifyPropertyChanged("PbColor");
                Trace.WriteLine($"PbColor = {pbColor}");
            }
        }

        public string Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                NotifyPropertyChanged("Progress");
                Trace.WriteLine($"Progress = {progress}");
            }
        }
        public int PbValue
        {
            get
            {
                return pbValue;
            }
            set
            {
                pbValue = value;
                NotifyPropertyChanged("PbValue");
                Trace.WriteLine($"PbValue = {pbValue}");
            }
        }
        public string WindowHeader
        {
            get
            {
                return windowHeader;
            }
            set
            {
                windowHeader = value;
                NotifyPropertyChanged("WindowHeader");
                Trace.WriteLine($"WindowHeader = {windowHeader}");
            }
        }
        public bool Beep
        {
            get
            {
                return beep;
            }
        }
    }
}
