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

        private double throttle;
        private double ailrone;
        private double rudder;
        private double elevator;
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

        // INotifyPropertyChanged implementation:
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
                    telnetClient.write("get heading-deg");
                    Heading = Double.Parse(telnetClient.read());
                    telnetClient.write("get Vertical-Speed");
                    VerticalSpeed = Double.Parse(telnetClient.read());
                    telnetClient.write("get Ground-Speed");
                    GroundSpeed = Double.Parse(telnetClient.read());
                    telnetClient.write("get Air-Speed");
                    AirSpeed = Double.Parse(telnetClient.read());
                    telnetClient.write("get GPS-Altitude");
                    GPSAltitude = Double.Parse(telnetClient.read());
                    telnetClient.write("get Roll");
                    Roll = Double.Parse(telnetClient.read());
                    telnetClient.write("get Pitch");
                    Pitch = Double.Parse(telnetClient.read());
                    telnetClient.write("get Altimeter-Alt");
                    AltimeterAlt = Double.Parse(telnetClient.read());
                    telnetClient.write("get Latitude");
                    Latitude = Double.Parse(telnetClient.read());
                    telnetClient.write("get Longitude");
                    Longitude = Double.Parse(telnetClient.read());
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
                heading = value;
                NotifyPropertyChanged("Heading");
            }
        }

        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }

        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }

        public double AirSpeed
        {
            get { return airSpeed; }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }

        public double GPSAltitude
        {
            get { return GPSaltitude; }
            set
            {
                GPSaltitude = value;
                NotifyPropertyChanged("GPSAltitude");
            }
        }

        public double Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }

        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        public double AltimeterAlt
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("AltimeterAlt");
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                Latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        // TODO: IMPLEMENT THIS!
        public void setThrottle(double val) { }
        public void setAileron(double val) { }
        public void setRudder(double val) { }
        public void setElevator(double val) { }

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
