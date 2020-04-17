using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    interface ISimulatorModel : INotifyPropertyChanged
    {
        // Connection to the simulator
        int connect(string ip, int port);
        void disconnect();
        void start();

        double Heading { get; set; }
        double VerticalSpeed { get; set; }
        double GroundSpeed { get; set; }
        double AirSpeed { get; set; }
        double Altitude { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double Altimeter { get; set; }
        string Location { get; set; }
        double Throttle { set; }
        double Aileron { set; }
        double Rudder { set; }
        double Elevator { set; }
        string Error { get; set; }
        bool Connect { get; }

    }
}
