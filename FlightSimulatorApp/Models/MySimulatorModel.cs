using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp.Models
{
    class MySimulatorModel : ISimulatorModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;

        private double throttle;
        private double ailrone;
        private double rudder;
        private double elevator;
        private double heading;
        private double verticalSpeed;
        private double groundSpeed;
        private double airSpeed;
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double latitude;
        private double longitude;
        private Location location;

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
                String message;

                while (!stop)
                {
                    try
                    {
                        telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Heading = Double.Parse(message);

                        telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            VerticalSpeed = Double.Parse(message);

                        telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            GroundSpeed = Double.Parse(message);

                        telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            AirSpeed = Double.Parse(message);

                        telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Altitude = Double.Parse(message);

                        telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Roll = Double.Parse(message);

                        telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Pitch = Double.Parse(message);

                        telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Altimeter = Double.Parse(message);

                        telnetClient.write("get /position/latitude-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Latitude = Double.Parse(message);

                        telnetClient.write("get /position/longitude-deg\n");
                        message = telnetClient.read();
                        if (!message.Contains("ERR"))
                            Longitude = Double.Parse(message);

                        Thread.Sleep(250);// read the data in 4Hz
                    }
                    catch
                    {
                        Console.WriteLine("Could not get Heading-Deg value");
                    }
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

        public double Altitude
        {
            get { return altitude; }
            set
            {
                if (altitude != value)
                {
                    altitude = value;
                    NotifyPropertyChanged("Altitude");
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

        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                if (altimeter != value)
                {
                    altimeter = value;
                    NotifyPropertyChanged("Altimeter");
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
                if (value > 1)
                {
                    throttle = 1;
                    NotifyPropertyChanged("Throttle");
                }
                else if (value < -1)
                {
                    throttle = -1;
                    NotifyPropertyChanged("Throttle");
                }
                else if (throttle != value)
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
                if (value > 1)
                {
                    ailrone = 1;
                    NotifyPropertyChanged("Aileron");
                }
                else if (value < -1)
                {
                    ailrone = -1;
                    NotifyPropertyChanged("Aileron");
                }
                else if (ailrone != value)
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
                if (value > 1)
                {
                    rudder = 1;
                    NotifyPropertyChanged("Rudder");
                }
                else if (value < -1)
                {
                    rudder = -1;
                    NotifyPropertyChanged("Rudder");
                }
                else if (rudder != value)
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
                if (value > 1)
                {
                    elevator = 1;
                    NotifyPropertyChanged("Elevator");
                }
                else if (value < -1)
                {
                    elevator = -1;
                    NotifyPropertyChanged("Elevator");
                }
                else if (elevator != value)
                {
                    elevator = value;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
