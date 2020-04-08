using FlightSimulatorApp.Models;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MyTelnetClient telnetClient = new MyTelnetClient();

            Application.Current.Properties["model"] = new MySimulatorModel(telnetClient);
            MySimulatorModel mySimulator = (MySimulatorModel)Application.Current.Properties["model"];

            telnetClient.connect("127.0.0.1", 5402);
            mySimulator.start();

            InitializeComponent();
        }
    }
}
