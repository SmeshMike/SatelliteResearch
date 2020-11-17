using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SatelliteResearch
{
    public partial class RadioStation : UserControl
    {

        public Label NameLabel
        {
            get => nameLabel;
            set => nameLabel = value;
        }
        
        public double x {
            get => coordinates[0];
            set => coordinates[0] = value;
        }

        public double y
        {
            get => coordinates[1];
            set => coordinates[1] = value;
        }
        public double z
        {
            get => coordinates[2];
            set => coordinates[2] = value;
        }

        public double[] coordinates;

        public RadioStation()
        {
            InitializeComponent();
            coordinates = new double[3];
            x = 0;
            y = 0;
            z = 0;
        }

        public void Run()
        {
            x = Convert.ToInt32(xTextBox.Text);
            y = Convert.ToInt32(yTextBox.Text);
            z = Convert.ToInt32(zTextBox.Text);
        }

        public static bool operator ==(RadioStation a, RadioStation b)
            => a.x == b.x && a.y == b.y && a.z == b.z;

        public static bool operator !=(RadioStation a, RadioStation b)
            => a.x != b.x || a.y != b.y || a.z != b.z;
    }
}
