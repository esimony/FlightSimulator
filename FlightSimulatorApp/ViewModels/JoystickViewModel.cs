using FlightSimulatorApp.Models;
using System;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModels
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        // Constructor.
        public JoystickViewModel(ISimulatorModel model)
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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // The properties implementation.
        public double VM_Throttle
        {
            set
            {
                model.Throttle = value;
            }
        }

        public double VM_Aileron
        {
            set
            {
                model.Aileron = value;
            }
        }

        public double VM_Rudder
        {
            set
            {
                model.Rudder = value;
                NotifyPropertyChanged("VM_Rudder");
            }
        }

        public double VM_Elevator
        {
            set
            {
                model.Elevator = value;
            }
        }
    }
}
