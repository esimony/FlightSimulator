﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    class MySimulatorModel : ISimulatorModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;

        private double throttle = 0;
        private double ailrone = 0;
        private double rudder = 0;
        private double elevator = 0;
        private double heading;
        private double verticalSpeed;
        private double groundSpeed;
        private double airSpeed;
        private double GPSaltitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double latitude;
        private double longitude;
        private Location location;

        String message;

        public event PropertyChangedEventHandler PropertyChanged;

        public MySimulatorModel (ITelnetClient telnetClient) {
            this.telnetClient = telnetClient;
            stop = false;
        }

        public void connect(string ip, int port) {
            telnetClient.connect(ip, port);
        }

        public void disconnect() {
            stop = true;
            telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate () {
                while (!stop)
                {
                    try
                    {
                        telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Heading = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            VerticalSpeed = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            GroundSpeed = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            AirSpeed = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            GPSAltitude = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Roll = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Pitch = Double.Parse(telnetClient.read());
                        telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            AltimeterAlt = Double.Parse(telnetClient.read());
                        telnetClient.write("get /position/latitude-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Latitude = Double.Parse(telnetClient.read());
                        telnetClient.write("get /position/longitude-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Longitude = Double.Parse(telnetClient.read());
                    }
                    catch
                    {
                        Console.WriteLine("Could not get Heading-Deg value");
                    }
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }


        // the properties implementation

        public double Heading
        {
            get { return heading; }
            set
            {
                if (heading != value)
                {
                    heading = value;
                    NotifyPropertyChanged("Heading");
                }
            }
        }

        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                if (verticalSpeed != value)
                {
                    verticalSpeed = value;
                    NotifyPropertyChanged("VerticalSpeed");
                }
            }
        }

        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                if (groundSpeed != value)
                {
                    groundSpeed = value;
                    NotifyPropertyChanged("GroundSpeed");
                }
            }
        }

        public double AirSpeed
        {
            get { return airSpeed; }
            set
            {
                if (airSpeed != value)
                {
                    airSpeed = value;
                    NotifyPropertyChanged("AirSpeed");
                }
            }
        }

        public double GPSAltitude
        {
            get { return GPSaltitude; }
            set
            {
                if (GPSaltitude != value)
                {
                    GPSaltitude = value;
                    NotifyPropertyChanged("GPSAltitude");
                }
            }
        }

        public double Roll
        {
            get { return roll; }
            set
            {
                if (roll != value)
                {
                    roll = value;
                    NotifyPropertyChanged("Roll");
                }
            }
        }

        public double Pitch
        {
            get { return pitch; }
            set
            {
                if (pitch != value)
                {
                    pitch = value;
                    NotifyPropertyChanged("Pitch");
                }
            }
        }

        public double AltimeterAlt
        {
            get { return altimeter; }
            set
            {
                if (altimeter != value)
                {
                    altimeter = value;
                    NotifyPropertyChanged("AltimeterAlt");
                }
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

        public Location Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    NotifyPropertyChanged("Location");
                }
            }
        }

        public double Throttle
        {
            get { return throttle; }
            set
            {
                if (throttle != value)
                {
                    throttle = value;
                    NotifyPropertyChanged("Throttle");
                }
            }
        }

        public double Aileron
        {
            get { return ailrone; }
            set
            {
                if (ailrone != value)
                {
                    ailrone = value;
                    NotifyPropertyChanged("Aileron");
                }
            }
        }

        public double Rudder
        {
            get { return rudder; }
            set
            {
                if (rudder != value)
                {
                    rudder = value;
                    NotifyPropertyChanged("Rudder");
                }
            }
        }

        public double Elevator
        {
            get { return elevator; }
            set
            {
                if (elevator != value)
                {
                    elevator = value;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
