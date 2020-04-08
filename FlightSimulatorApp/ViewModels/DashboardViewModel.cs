using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

namespace FlightSimulatorApp.ViewModels
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        public DashboardViewModel(ISimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public double VM_Heading
        {
            get { return model.Heading; }
        }

        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }

        public double VM_GroundSpeed
        {
            get { return model.GroundSpeed; }
        }

        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }
        }

        public double VM_Altitude
        {
            get { return model.Altitude; }
        }

        public double VM_Roll
        {
            get { return model.Roll; }
        }

        public double VM_Pitch
        {
            get { return model.Pitch; }
        }

        public double VM_Altimeter
        {
            get { return model.Altimeter; }
        }
    }
}
