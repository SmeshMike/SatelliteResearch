﻿using System;
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

        public double Y
        {
            get => coordinates[1];
            set => coordinates[1] = value;
        }
        public double Z
        {
            get => coordinates[2];
            set => coordinates[2] = value;
        }
        public double Vx
        {
            get => velocity[0];
            set => velocity[0] = value;
        }
        public double Vy
        {
            get => velocity[1];
            set => velocity[1] = value;
        }
        public double Vz
        {
            get => velocity[2];
            set => velocity[2] = value;
        }

        public double VAbs
        {
            get => velocity[3];
            set => velocity[3] = value;
        }

        public double Wx
        {
            get => frequency[0];
            set => frequency[0] = value;
        }
        public double Wy
        {
            get => frequency[1];
            set => frequency[1] = value;
        }

        public double Wz
        {
            get => frequency[2];
            set => frequency[2] = value;
        }

        public double WAbs
        {
            get => frequency[3];
            set => frequency[3] = value;
        }

        public double[] coordinates;
        public double[] velocity;
        public double[] frequency;

        public RadioStation()
        {
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[4];
            frequency = new double[4];
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
            velocity = new double[4];
            frequency = new double[4];
            X = x;
            Y = y;
            Z = z;
            Vx = 0;
            Vy = 0;
            Vz = 0;
            VAbs = 0;
            Wx = 0;
            Wy = 0;
            Wz = 0;
            WAbs = 0;
        }

        public RadioStation(double x, double y, double z, double vx, double vy, double vz)
        {
            const int c = 300000000;
            const long w0 = 6000000000;
            InitializeComponent();
            coordinates = new double[3];
            velocity = new double[4];
            frequency = new double[4];
            X = x;
            Y = y;
            Z = z;
            Vx = vx;
            Vy = vy;
            Vz = vz;
            VAbs = Math.Sqrt(vx * vx + vy * vy + vz * vz);
            Wx = w0/(1 + vx/c);
            Wy = w0 / (1 + vy / c); ;
            Wz = w0 / (1 + vz / c); ;
            WAbs = w0 / (1 + Math.Sqrt(vx * vx + vy * vy + vz * vz) / c);
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
