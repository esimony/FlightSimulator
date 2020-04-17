using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorApp.Models
{
    class MySimulatorModel : ISimulatorModel
    {
        private ITelnetClient telnetClient;
        volatile bool stop;
        public Mutex mutex = new Mutex();
        private bool changed;

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
        private string location;

        private string error;

        public MySimulatorModel (ITelnetClient telnetClient) {
            this.telnetClient = telnetClient;
            stop = false;
            error = "";
        }

        public int connect(string ip, int port) {
            // Try to connect to server.
            if (telnetClient.connect(ip, port) != 0)
            {
                Error = "Could not connect to server \nclick disconnect and try again";
                return -1;
            }
            return 0;
        }

        public void disconnect() {
            // Disconnect from server.
            stop = true;
            telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate () {
                String message = String.Empty;

                while (!stop)
                {
                    // Read all values from server.
                    // If the value is not valid - print a message. else, convert it to double.
                    try
                    {
                        message = telnetClient.read("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Heading value is err";
                            Thread.Sleep(250);
                        }
                        else
                            Heading = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/gps/indicated-vertical-speed\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Vertical Speed value is err";
                            Thread.Sleep(250);
                        }
                        else
                            VerticalSpeed = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Ground Speed value is err";
                            Thread.Sleep(250);
                        }
                        else
                            GroundSpeed = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Air Speed value is err";
                            Thread.Sleep(250);
                        }
                        else
                            AirSpeed = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/gps/indicated-altitude-ft\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Altitude value is err";
                            Thread.Sleep(250);
                        }
                        else
                            Altitude = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Roll value is err";
                            Thread.Sleep(250);
                        }
                        else
                            Roll = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Pitch value is err";
                            Thread.Sleep(250);
                        }
                        else
                            Pitch = Double.Parse(message);

                        message = telnetClient.read("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Altimeter value is err";
                            Thread.Sleep(250);
                        }
                        else
                            Altimeter = Double.Parse(message);

                        message = telnetClient.read("get /position/latitude-deg\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Latitude value is err";
                            Thread.Sleep(250);
                        }
                        else
                        {
                            latitude = Double.Parse(message);
                            // If value is bigger then the limit - make the latitude be equal to limit.
                            if (latitude >= 90)
                            {
                                latitude = 90;
                                Error = "Plane has gotten to limit";
                            }
                            // If value is smaller then the limit - make the latitude be equal to limit.
                            else if (latitude <= -90)
                            {
                                latitude = -90;
                                Error = "Plane has gotten to limit";
                            }
                        }

                        message = telnetClient.read("get /position/longitude-deg\n");
                        if (message.Contains("ERR"))
                        {
                            Error = "Longitude value is err";
                            Thread.Sleep(250);
                        }
                        else
                        {
                            longitude = Double.Parse(message);
                            // If value is bigger then the limit - make the longitude be equal to limit.
                            if (longitude >= 180)
                            {
                                longitude = 180;
                                Error = "Plane has gotten to limit";
                            }
                            // If value is smaller then the limit - make the longitude be equal to limit.
                            else if (longitude <= -180)
                            {
                                longitude = -180;
                                Error = "Plane has gotten to limit";
                            }
                        }
                        Location = Convert.ToString(latitude + "," + longitude);

                        // Read the data in 4Hz.
                        Thread.Sleep(250);
                    }
                    catch (SocketException)
                    {
                        stop = true;
                        Error = "Reading timeout";
                    }
                    catch (FormatException f)
                    {
                        // Connection to server lost.
                        if (message == "READFAILURE")
                        {
                            Error = "Connection to server lost \nclick disconnect and try again";
                            stop = true;
                        }
                        // Server did not sent values for more then 10 seconds.
                        else if (message == "TIMEOUT")
                        {
                            Error = "Read timeout";
                            stop = true;
                        }
                        else if (message == "GENERALFAILURE")
                        {
                            Error = "Socket failure occured";
                            stop = true;
                        }
                        // Value got from server is not a number or "err", so print "Bad format" to the error board.
                        else if (f.ToString().Contains("not in a correct format"))
                        {
                            Error = "Bad format";
                        }
                    }
                    catch (Exception)
                    {
                        Error = "General failure occured";
                        stop = true;
                    }
                }
            }).Start();
        }


        // Define properties implementation.

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

        public string Location
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
            set
            {
                changed = false;
                // Check values limits.
                if (value > 1)
                {
                    throttle = 1;
                }
                else if (value < 0)
                {
                    throttle = 0;
                }
                else if (throttle != value)
                {
                    throttle = value;
                    changed = true;
                }
                string command = "set /controls/engines/current-engine/throttle " + throttle.ToString() + "\n";
                if (changed)
                {
                    this.telnetClient.write(command);
                }
            }
        }

        public double Aileron
        {
            set
            {
                changed = false;
                // Check values limits.
                if (value > 1)
                {
                    ailrone = 1;
                }
                else if (value < -1)
                {
                    ailrone = -1;
                }
                else if (ailrone != value)
                {
                    ailrone = value;
                    changed = true;
                }
                string command = "set /controls/flight/aileron " + ailrone.ToString() + "\n";
                if (changed)
                {
                    this.telnetClient.write(command);
                }
            }
        }

        public double Rudder
        {
            set
            {
                // Check values limits.
                if (value > 1)
                {
                    rudder = 1;
                }
                else if (value < -1)
                {
                    rudder = -1;
                }
                else if (rudder != value)
                {
                    rudder = value;
                }
                string command = "set /controls/flight/rudder " + rudder.ToString() + "\n";
                this.telnetClient.write(command);
            }
        }

        public double Elevator
        {
            set
            {
                // Check values limits.
                if (value > 1)
                {
                    elevator = 1;
                }
                else if (value < -1)
                {
                    elevator = -1;
                }
                else if (elevator != value)
                {
                    elevator = value;
                }
                string command = "set /controls/flight/elevator " + elevator.ToString() + "\n";
                this.telnetClient.write(command);
            }
        }

        public string Error
        {
            get { return error; }
            set
            {
                if (error != value)
                {
                    error = value;
                    NotifyPropertyChanged("Error");
                }
            }
        }

        public bool Connect
        {
            get
            {
                return telnetClient.getIsConnect();
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
