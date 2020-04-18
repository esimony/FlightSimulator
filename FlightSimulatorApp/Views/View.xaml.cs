using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using System;
using System.Windows;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        private MyTelnetClient telnetClient;
        private MySimulatorModel mySimulator;
        private JoystickViewModel joystickViewModel;
        private DashboardViewModel dashboardViewModel;
        private MapViewModel mapViewModel;
        private ErrorViewModel errorViewModel;

        public View(string port, string ip)
        {
            int tempPort;
            // Connect settings  - client and server.
            telnetClient = new MyTelnetClient();
            Application.Current.Properties["model"] = new MySimulatorModel(telnetClient);
            mySimulator = (MySimulatorModel)Application.Current.Properties["model"];

            // Define all controls in the view window.
            joystickViewModel = new JoystickViewModel(mySimulator);
            dashboardViewModel = new DashboardViewModel(mySimulator);
            errorViewModel = new ErrorViewModel(mySimulator);
            mapViewModel = new MapViewModel(mySimulator);
            // Checks the reseve port.
            try
            {
                tempPort = int.Parse(port);
            }
            catch
            {
                Console.WriteLine("Error - worng formt of port");
                tempPort = 0;
            }
            if (mySimulator.connect(ip, tempPort) == 0)
                mySimulator.start();

            InitializeComponent();
            Joystick.DataContext = joystickViewModel;
            dash.DataContext = dashboardViewModel;
            error.DataContext = errorViewModel;
            map.DataContext = mapViewModel;
            disconnect.DataContext = mapViewModel;
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            // Check if server is connect.
            if (mySimulator.Connect)
            {
                telnetClient.disconnect();
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}