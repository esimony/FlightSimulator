using FlightSimulatorApp.Models;
using System;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModels
{
    class ErrorViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        // Constructor.
        public ErrorViewModel(ISimulatorModel model)
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
        public string VM_Error
        {
            get { return model.Error; }
            set
            {
                model.Error = value;
            }
        }
    }
}
