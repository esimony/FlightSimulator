using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    class MyTelnetClient : ITelnetClient
    {
        // TCP client
        private TcpClient client;

        public void connect(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
//                Console.WriteLine("Trying to connect to server");
//                client.Connect(ip, port);
                Console.WriteLine("Client is now connected to server");
            }
            catch
            {
                Console.WriteLine("Error - could not connect to server");
            }
        }

        public void disconnect()
        {
            // Closig TCP client
            client.Close();
        }

        public string read()
        {
            // Declare a buffer to get the data bytes
            Byte[] buffer = new byte[1024];
            String dataString = string.Empty;

            // Reading data bytes from client
            int bytes = client.GetStream().Read(buffer, 0, buffer.Length);

            // Translating bytes into ASCII
            dataString = Encoding.ASCII.GetString(buffer, 0, bytes);
            
            // Print recieved data
            Console.WriteLine("Received data: {0}", dataString);

            return dataString;
        }

        public void write(string command)
        {
            // Declare a buffer to get the data bytes
            Byte[] sentData;

            // Translating bytes into ASCII
            sentData = Encoding.ASCII.GetBytes(command);

            // Send data to server
            client.GetStream().Write(sentData, 0, sentData.Length);

            // Print sent data
            Console.WriteLine("Sent data: {0}", command);
        }
    }
}
