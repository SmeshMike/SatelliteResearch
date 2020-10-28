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
        public ChartsForm()
        {
            InitializeComponent();
            Chart dtDifference = new Chart();
            dtDifference.Location = new Point(5,5);
            dtDifference.Size = new Size(895,440);

            Chart coordDifference = new Chart();
            coordDifference.Location = new Point(5, 450);
            coordDifference.Size = new Size(895, 440);
        }
    }
}
