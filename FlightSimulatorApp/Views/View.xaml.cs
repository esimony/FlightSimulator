using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
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
            // Connect settings  - client and server.
            telnetClient = new MyTelnetClient();
            Application.Current.Properties["model"] = new MySimulatorModel(telnetClient);
            mySimulator = (MySimulatorModel)Application.Current.Properties["model"];

            // Define all controls in the view window.
            joystickViewModel = new JoystickViewModel(mySimulator);
            dashboardViewModel = new DashboardViewModel(mySimulator);
            errorViewModel = new ErrorViewModel(mySimulator);
            mapViewModel = new MapViewModel((MySimulatorModel)Application.Current.Properties["model"]);
            if (mySimulator.connect(ip, int.Parse(port)) == 0)
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
            if (mapViewModel.Connect)
            {
                telnetClient.disconnect();
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

/*
 // maybe delete this!!!!
 // maybe we can change to this kod
 using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
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
            // Connect settings  - client and server.
            telnetClient = new MyTelnetClient();
            Application.Current.Properties["model"] = new MySimulatorModel(telnetClient);
            mySimulator = (MySimulatorModel)Application.Current.Properties["model"];

            // Define all controls in the view window.
            joystickViewModel = new JoystickViewModel(mySimulator);
            dashboardViewModel = new DashboardViewModel(mySimulator);
            errorViewModel = new ErrorViewModel(mySimulator);
            mapViewModel = new MapViewModel(mySimulator);
            if (mySimulator.connect(ip, int.Parse(port)) == 0)
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

 */
