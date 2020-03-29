﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface ISimulatorModel :INotifyPropertyChanged
    {
        // Connection to the simulator
        void connect(string ip, int port);
        void disconnect();
        void start();
        void setThrottle(double val);
        void setAileron(double val);
        void setRudder(double val);
        void setElevator(double val);
        double Heading { get; set; }
        double VerticalSpeed { get; set; }
        double GroundSpeed { get; set; }
        double AirSpeed { get; set; }
        double GPSAltitude { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double AltimeterAlt { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
