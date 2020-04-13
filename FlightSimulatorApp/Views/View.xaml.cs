using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        private MyTelnetClient telnetClient;
        private MapViewModel mvm;
        public View(string port, string ip)
        {
            telnetClient = new MyTelnetClient();

            Application.Current.Properties["model"] = new MySimulatorModel(telnetClient);
            MySimulatorModel mySimulator = (MySimulatorModel)Application.Current.Properties["model"];
            JoystickViewModel jvm = new JoystickViewModel(mySimulator);
            DashboardViewModel dvm = new DashboardViewModel(mySimulator);
            ErrorViewModel evm = new ErrorViewModel(mySimulator);
            mvm = new MapViewModel((MySimulatorModel)Application.Current.Properties["model"]);
            if (mySimulator.connect(ip, int.Parse(port)) == 0)
                mySimulator.start();


            InitializeComponent();
            Joystick.DataContext = jvm;
            dash.DataContext = dvm;
            error.DataContext = evm;
            map.DataContext = mvm;
            disconnect.DataContext = mvm;
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (mvm.Connect)
            {
                telnetClient.disconnect();
            }
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
