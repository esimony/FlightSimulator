using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Views;
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
            InitializeComponent();


        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            View v = new View(port.Text, ip.Text);
            v.Show();
            this.Close();
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            View v = new View("5402", "127.0.0.1");
            v.Show();
            this.Close();
        }
    }
}
