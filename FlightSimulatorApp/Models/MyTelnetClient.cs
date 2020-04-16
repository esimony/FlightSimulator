using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorApp.Models
{
    class MyTelnetClient : ITelnetClient
    {
        // TCP client
        private TcpClient client;
        public Mutex mutex = new Mutex();
        public bool isConnect = false;

        public int connect(string ip, int port)
        {
            int rc = 0;

            try
            {
                client = new TcpClient();
                Console.WriteLine("Client is now connected to server");
                isConnect = true;
            }
            catch
            {
                Console.WriteLine("Error - could not connect to server");
                rc = -1;
            }

            client.SendTimeout = 10000;
            client.ReceiveTimeout = 10000;
            client.Connect(ip, port);
            return rc;
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

        public string read(string command)
        {
            if (client != null)
            {
                try
                {
                    mutex.WaitOne();
                    Byte[] sentData;

                    // Translating bytes into ASCII
                    sentData = Encoding.ASCII.GetBytes(command);

                    // Send data to server
                    client.GetStream().Write(sentData, 0, sentData.Length);

                    // Print sent data
                    // Declare a buffer to get the data bytes
                    Byte[] buffer = new byte[1024];
                    String dataString = string.Empty;

                    // Reading data bytes from client
                    int bytes = client.GetStream().Read(buffer, 0, buffer.Length);

                    // Translating bytes into ASCII
                    dataString = Encoding.ASCII.GetString(buffer, 0, bytes);

                    // Print recieved data
                    Console.WriteLine("Received data: {0}", dataString);
                    mutex.ReleaseMutex();
                    return dataString;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    if (e.ToString().Contains("closed"))
                    {
                        Console.WriteLine("Error - server closed");
                        return "READFAILURE";
                    }
                    else
                    {
                        Console.WriteLine("Error - read timeout");
                        return "TIMEOUT";
                    }
                }
            }

            // Client not connected
            return "NOTCONNECTED";
        }

        public void write(string command)
        {
            if (client != null)
            {
                try
                {
                    mutex.WaitOne();
                    // Declare a buffer to get the data bytes
                    Byte[] sentData;

                    // Translating bytes into ASCII
                    sentData = Encoding.ASCII.GetBytes(command);

                    // Send data to server
                    client.GetStream().Write(sentData, 0, sentData.Length);

                    // Print sent data
                    Console.WriteLine("Sent data: {0}", command);
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    string newData = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                    mutex.ReleaseMutex();
                }
                catch
                {
                    Console.WriteLine("Error - could not write to server");
                }
            }
        }

        public bool getIsConnect()
        {
            return isConnect;
        }
    }
}
