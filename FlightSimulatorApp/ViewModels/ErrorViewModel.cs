using FlightSimulatorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class ErrorViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

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

        public string VM_ConnectionError
        {
            get { return model.ConnectionError; }
            set
            {
                model.ConnectionError = value;
            }
        }

        public string VM_TimeoutError
        {
            get { return model.TimeoutError; }
            set
            {
                model.TimeoutError = value;
            }
        }
    }
}
