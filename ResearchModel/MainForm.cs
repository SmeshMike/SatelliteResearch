﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ResearchModel;
using static SatelliteResearch.MathStuff;

namespace SatelliteResearch
{

    public partial class MainForm : Form
    {

        private double delta, minDelta, denominator;

        private long x, y, z;

        void DotInitialize()
        {
            
            long tmp = 20000000;
            Random rand = new Random();
            x = rand.Next(Convert.ToInt32(tmp));
            tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - (x * x)));
            y = rand.Next(Convert.ToInt32(tmp));
            z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));

        }

        void SourceInitialize()
        {
            long tmp = 6370000;
            Random rand = new Random();
            x = rand.Next(Convert.ToInt32(tmp));
            tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
            y = rand.Next(Convert.ToInt32(tmp));
            z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SearcherStation1 = searcherStation1;
            SearcherStation2 = searcherStation2;
            SearcherStation3 = searcherStation3;
            SearcherStation4 = searcherStation4;
            NewSource = newSource;
            TrueSource = trueSource;

            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = 200;
            progressBar.Value = 1;
            progressBar.Step = 1;
        }

        private void refreshButtonClick(object sender, EventArgs e)
        {
            newSource.xTextBox.Text = "0";
            newSource.yTextBox.Text = "0";
            newSource.zTextBox.Text = "0";

            GenerateDots(4);

            searcherStation1.NameLabel.Text = "Спутник 1";
            searcherStation1.xTextBox.Text = Coordinate[0,0].ToString();
            searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
            searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
            searcherStation1.Run();

            searcherStation2.NameLabel.Text = "Спутник 2";
            searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
            searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
            searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
            searcherStation2.Run();
            
            searcherStation3.NameLabel.Text = "Спутник 3";
            searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
            searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
            searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
            searcherStation3.Run();

            searcherStation4.NameLabel.Text = "Спутник 4";
            searcherStation4.xTextBox.Text = Coordinate[3, 0].ToString();
            searcherStation4.yTextBox.Text = Coordinate[3, 1].ToString();
            searcherStation4.zTextBox.Text = Coordinate[3, 2].ToString();
            searcherStation4.Run();

            SourceInitialize();
            trueSource.NameLabel.Text = "Источник";
            trueSource.xTextBox.Text = x.ToString();
            trueSource.yTextBox.Text = y.ToString();
            trueSource.zTextBox.Text = z.ToString();

            if (newSource.xTextBox.Text == "")
                newSource.xTextBox.Text = "0";
            if (newSource.yTextBox.Text == "")
                newSource.yTextBox.Text = "0";
            if (newSource.zTextBox.Text == "")
                newSource.zTextBox.Text = "0";

            
            
            
            trueSource.Run();
            newSource.Run();
            
            Dt12 = Math.Sqrt(Math.Pow((searcherStation1.x - trueSource.x), 2) + Math.Pow((searcherStation1.y - trueSource.y), 2) + Math.Pow((searcherStation1.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2));
            Dt23 = Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2));
            Dt34 = Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation4.x - trueSource.x), 2) + Math.Pow((searcherStation4.y - trueSource.y), 2) + Math.Pow((searcherStation4.z - trueSource.z), 2));


            delta = Convert.ToDouble(stepTextBox.Text.Replace('.',','));
            minDelta = Convert.ToDouble(minStepTextBox.Text);
            denominator = Convert.ToDouble(denominatorTextBox.Text);
        }

        public MainForm()
        {
            InitializeComponent();
            newSource.NameLabel.Text = "Предполагаемые координаты";

            stepTextBox.Text = "262144";
            minStepTextBox.Text = "0,015625";
            denominatorTextBox.Text = "64";

            refreshButtonClick(null, EventArgs.Empty);
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();

            HookJeeves(delta, minDelta, denominator);

            newSource.xTextBox.Text = Convert.ToInt32(newSource.x).ToString();
            newSource.yTextBox.Text = Convert.ToInt32(newSource.y).ToString();
            newSource.zTextBox.Text = Convert.ToInt32(newSource.z).ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
            errorLabel.Text = GetNewSourceDistanceDifference().ToString();
        }

        private void DCoordinatesGraphButtonClick(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            FindSatelliteInaccuracy(delta, minDelta, denominator, points);
            ChartsForm form = new ChartsForm();

            form.Show();

            form.dtDifference.Series.Clear();
            var series = new Series
            {
                    Name = "сам щит",
                    Color = Color.Green,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
            };
            form.dtDifference.Series.Add(series);
            for (int i = 0; i < points.Count; i++)
            {
                form.dtDifference.Series[0].Points.AddXY(i*2, points[i]);
            }
            form.Controls.Add(form.dtDifference);
            form.dtDifference.Show();



        }

        private void DtGraphButton(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            FindDtInaccuracy(delta, minDelta, denominator, points, progressBar);
            ChartsForm form = new ChartsForm();

            form.Show();

            form.dtDifference.Series.Clear();
            var series = new Series
            {
                Name = "сам щит",
                Color = Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };
            form.dtDifference.Series.Add(series);
            for (int i = 0; i < points.Count; i++)
            {
                form.dtDifference.Series[0].Points.AddXY(i*2, points[i]);
            }
            form.Controls.Add(form.dtDifference);
            form.dtDifference.Show();
        }



    }
}

