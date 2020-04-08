using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Models;

// TODO: IMPLEMENT THIS!!

namespace FlightSimulatorApp.ViewModels
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        private double throttle;
        private double aileron;
        private double rudder;
        private double elevator;

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

        public double VM_Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                model.Throttle = value;
                //string msg = "set /controls/engines/current-engine/throttle " + value.ToString() + "\n";
                //model.sendSetRequest(msg, "Throttle");
                NotifyPropertyChanged("VM_Throttle");
            }
        }

        public double VM_Airelon
        {
            get { return aileron; }
            set
            {
                aileron = value;
                model.Aileron = value;
                //string msg = "set /controls/flight/aileron " + value.ToString() + "\n";
                //model.sendSetRequest(msg, "Aileron");
                NotifyPropertyChanged("VM_Airelon");
            }
        }

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                model.Rudder = value;
                //string msg = "set /controls/flight/rudder " + value.ToString() + "\n";
                //model.sendSetRequest(msg, "Rudder");
                NotifyPropertyChanged("VM_Rudder");
            }
        }

        public double VM_Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                model.Elevator = value;
                //string msg = "set /controls/flight/elevator " + value.ToString() + "\n";
                //model.sendSetRequest(msg, "Elevator");
                NotifyPropertyChanged("VM_Elevator");
            }
        }
    }
}
