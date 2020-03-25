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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Point startPoint = new Point();

        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton ==MouseButton.Left)
            {
                startPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - startPoint.X;
                double y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt((x * x) + (y * y)) < Base.Width / 2.5)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}
