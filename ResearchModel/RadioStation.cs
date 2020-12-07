using System;
using System.Windows.Forms;

namespace ResearchModel
{
    public partial class RadioStation : UserControl
    {

        public Label NameLabel
        {
            get => nameLabel;
            set => nameLabel = value;
        }
        
        public double X
        {
            get => coordinates[0];
            set => coordinates[0] = value;
        }

        public double Vx
        {
            get => velocity[0];
            set => velocity[0] = value;
        }

        public double Wx
        {
            get => frequency[0];
            set => frequency[0] = value;
        }

        public double Y
        {
            get => coordinates[1];
            set => coordinates[1] = value;
        }

        public double Vy
        {
            get => velocity[1];
            set => velocity[1] = value;
        }
        public double Wy
        {
            get => frequency[1];
            set => frequency[1] = value;
        }

        public double Z
        {
            get => coordinates[2];
            set => coordinates[2] = value;
        }
        public double Vz
        {
            get => velocity[2];
            set => velocity[2] = value;
        }
        public double Wz
        {
            get => frequency[2];
            set => frequency[2] = value;
        }

        public double[] coordinates;
        public double[] velocity;
        public double[] frequency;

        public RadioStation()
        {
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[3];
            frequency = new double[3];
            X = 0;
            Y = 0;
            Z = 0;
            Vx = 0;
            Vy = 0;
            Vz = 0;
            Wx = 0;
            Wy = 0;
            Wz = 0;
        }

        public RadioStation(double x, double y, double z)
        {
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[3];
            X = x;
            Y = y;
            Z = z;
        }

        public RadioStation(double x, double y, double z, double vx, double vy, double vz)
        {
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[3];
            X = x;
            Y = y;
            Z = z;
            Vx = vx;
            Vy = vy;
            Vz = vz;
        }

        public RadioStation(double x, double y, double z, double vx, double vy, double vz, double wx, double wy, double wz)
        {
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[3];
            X = x;
            Y = y;
            Z = z;
            Vx = vx;
            Vy = vy;
            Vz = vz;
            Wx = wx;
            Wy = wy;
            Wz = wz;
        }

        public void Run()
        {
            X = Convert.ToDouble(xTextBox.Text);
            Y = Convert.ToDouble(yTextBox.Text);
            Z = Convert.ToDouble(zTextBox.Text);
        }

        public static bool operator ==(RadioStation a, RadioStation b)
            => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

        public static bool operator !=(RadioStation a, RadioStation b)
            => a.X != b.X || a.Y != b.Y || a.Z != b.Z;
    }
}
