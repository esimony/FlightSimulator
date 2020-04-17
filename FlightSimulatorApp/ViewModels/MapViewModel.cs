using FlightSimulatorApp.Models;
using System;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModels
{
    class MapViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        // Constructor.
        public MapViewModel(ISimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // The properties implementation.
        public string VM_Location
        {
            get
            {
                return model.Location;
            }
        }

        public bool Connect
        {
            get
            {
                return model.Connect;
            }
        }
    }
}
