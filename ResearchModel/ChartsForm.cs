using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ResearchModel
{
    public partial class ChartsForm : Form
    {
        public Chart dtDifference;
        public ChartsForm()
        {
            InitializeComponent();
            dtDifference.Location = new Point(5, 5);
            dtDifference.Size = new Size(790, 445);
        }

        public void CreateGraphic(List<double> points)
        {
            
        }
    }
}
