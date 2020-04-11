using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    interface ISimulatorModel : INotifyPropertyChanged
    {
        // Connection to the simulator
        void connect(string ip, int port);
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
        double Throttle { get; set; }
        double Aileron { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }
    }
}
