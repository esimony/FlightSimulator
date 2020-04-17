using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


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
        // Control point - responsible for changing the state of the joystick.
        private Point startlPoint = new Point();

        private readonly Storyboard centerknob;

        // Constructor.

        public Joystick()
        {
            InitializeComponent();
            centerknob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        // The properties implementation.

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

        // Joystick functionality.

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            // Initialize values when the joystick returns to its initial location.
            Rudder = 0;
            Elevator = 0;
            Released?.Invoke(this);
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Checks if the joystick was pressed. 
            if (e.ChangedButton == MouseButton.Left)
            {
                // Update control point.
                startlPoint = e.GetPosition(this);
                Captured?.Invoke(this);
                Knob.CaptureMouse();
                centerknob.Stop();
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            // Checks if the joystick is moving. 
            if (!Knob.IsMouseCaptured)
                return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - startlPoint.X;
                double y = e.GetPosition(this).Y - startlPoint.Y;
                if (Math.Sqrt((x * x) + (y * y)) < Base.Height / 2.5)
                {
                    double lengtheMax = (Base.Height / 2) - (KnobBase.Width / 2);
                    // Update the values.
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    Elevator = -(y / lengtheMax);
                    Rudder = x / lengtheMax;
                    // Check according to exercise requirements.
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
            // Update that the joystick has been released.
            Knob.ReleaseMouseCapture();
            centerknob.Begin();
        }
    }
}