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
        // Data stream
        private NetworkStream stream;

        public void connect(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();
                Console.WriteLine("Trying to connect to server");
                client.Connect(ip, port);
                Console.WriteLine("Client is now connected to server");
            }
            catch
            {
                Console.WriteLine("Error - could not connect to server");
            }
        }

        public void disconnect()
        {
            // Closig stream and TCP client
            stream.Close();
            client.Close();
        }

        public string read()
        {
            // Declare a buffer to get the data bytes
            Byte[] buffer = new byte[256];
            String dataString = String.Empty;

            // Reading data bytes from client
            Int32 bytes = stream.Read(buffer, 0, buffer.Length);
            // Translating bytes into ASCII
            dataString = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
            // Print recieved data
            Console.WriteLine("Received data: {0}", dataString);

            return dataString;
        }

        public void write(string command)
        {
            // Translating bytes into ASCII
            Byte[] sentData = System.Text.Encoding.ASCII.GetBytes(command);

            // Send data to server
            this.stream.Write(sentData, 0, sentData.Length);
            // Print sent data
            Console.WriteLine("Sent data: {0}", sentData);
        }
    }
}
