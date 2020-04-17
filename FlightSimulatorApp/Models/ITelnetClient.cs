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
        int connect(string ip, int port);
        void write(string command);
        // Read is blocking call.
        string read(string command);
        void disconnect();
        bool getIsConnect();
    }
}
