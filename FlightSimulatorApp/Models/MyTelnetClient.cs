using System;
using System.Text;
using System.Net.Sockets;

namespace FlightSimulatorApp.Models
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
                Console.WriteLine("Client is now connected to server");
            }
            catch
            {
                Console.WriteLine("Error - could not connect to server");
            }
        }

        public void disconnect()
        {
            try
            {
                // Closig TCP client
                client.Close();
            }
            catch
            {
                Console.WriteLine("Error - could not disconnect from server");
            }
        }

        public string read()
        {
            if (client != null)
            {
                // Declare a buffer to get the data bytes
                Byte[] buffer = new byte[1024];
                String dataString = string.Empty;

                try
                {
                    // Reading data bytes from client
                    int bytes = client.GetStream().Read(buffer, 0, buffer.Length);

                    // Translating bytes into ASCII
                    dataString = Encoding.ASCII.GetString(buffer, 0, bytes);

                    // Print recieved data
                    Console.WriteLine("Received data: {0}", dataString);

                    return dataString;
                }
                catch
                {
                    Console.WriteLine("Error - could not read from server");
                    return "";
                }
            }
            else
            {
                Console.WriteLine("Error - server not connected");
                return "";
            }
        }

        public void write(string command)
        {
            // Declare a buffer to get the data bytes
            Byte[] sentData;

            if (client != null)
            {
                // Translating bytes into ASCII
                sentData = Encoding.ASCII.GetBytes(command);

                // Send data to server
                client.GetStream().Write(sentData, 0, sentData.Length);

                // Print sent data
                Console.WriteLine("Sent data: {0}", command);
            }
            else
            {
                Console.WriteLine("Error - server not connected");
            }
        }
    }
}
