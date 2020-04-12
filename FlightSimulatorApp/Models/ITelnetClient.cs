using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    interface ITelnetClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read(string command);  // blocking call
        void disconnect();
        bool getIsConnect();
    }
}
