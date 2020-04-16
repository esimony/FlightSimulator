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
using System.Windows.Media.Animation;
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
        public delegate void EmptyJoystickEventHandler(Joystick sender);
        public event EmptyJoystickEventHandler Released;
        public event EmptyJoystickEventHandler Captured;
        private Point startPoint = new Point();

        public double Elevator
        {
            get
            {
                return Convert.ToDouble(GetValue(ElevatorProperty));
            }
            set
            {
                SetValue(ElevatorProperty, value);
            }
        }

        public static readonly DependencyProperty ElevatorProperty = DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        public double Rudder
        {
            get
            {
                return Convert.ToDouble(GetValue(RudderProperty));
            }
            set
            {
                SetValue(RudderProperty, value);
            }
        }

        public static readonly DependencyProperty RudderProperty = DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);
        private readonly Storyboard centerknob;
        public Joystick()
        {
            InitializeComponent();
            centerknob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Rudder = 0;
            Elevator = 0;
            Released?.Invoke(this);
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                startPoint = e.GetPosition(this);
                Captured?.Invoke(this);
                Knob.CaptureMouse();
                centerknob.Stop();
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Knob.IsMouseCaptured)
                return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - startPoint.X;
                double y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt((x * x) + (y * y)) < Base.Height / 2.5)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    Elevator = -(y / (Base.Height / 2 - KnobBase.Width / 2));
                    Rudder = x / (Base.Width / 2 - KnobBase.Width / 2);
                    if (Rudder > 1)
                    {
                        Rudder = 1;
                    }
                    if (Rudder < -1)
                    {
                        Rudder = -1;
                    }
                    if (Elevator > 1)
                    {
                        Elevator = 1;
                    }
                    if (Elevator < -1)
                    {
                        Elevator = -1;
                    }
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // knobPosition.X = 0;
            //knobPosition.Y = 0;
            Knob.ReleaseMouseCapture();
            centerknob.Begin();
            // Rudder = 0;
            //  Elevator = 0;
        }
    }
}