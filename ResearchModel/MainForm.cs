using System;
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

        private int delta, minDelta, denominator;

        private long x, y, z;

        void DotInitialize()
        {

            SearcherStation1 = searcherStation1;
            SearcherStation2 = searcherStation2;
            SearcherStation3 = searcherStation3;
            SearcherStation4 = searcherStation4;
            NewSource = newSource;
            TrueSource = trueSource;
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

        }

        private void refreshButtonClick(object sender, EventArgs e)
        {
            DotInitialize();
            searcherStation1.NameLabel.Text = "Спутник 1";
            searcherStation1.xTextBox.Text = x.ToString();
            searcherStation1.yTextBox.Text = y.ToString();
            searcherStation1.zTextBox.Text = z.ToString();

            DotInitialize();
            searcherStation2.NameLabel.Text = "Спутник 2";
            searcherStation2.xTextBox.Text = x.ToString();
            searcherStation2.yTextBox.Text = y.ToString();

            searcherStation2.zTextBox.Text = z.ToString();
            DotInitialize();
            searcherStation3.NameLabel.Text = "Спутник 3";
            searcherStation3.xTextBox.Text = x.ToString();
            searcherStation3.yTextBox.Text = y.ToString();
            searcherStation3.zTextBox.Text = z.ToString();

            DotInitialize();
            searcherStation4.NameLabel.Text = "Спутник 4";
            searcherStation4.xTextBox.Text = x.ToString();
            searcherStation4.yTextBox.Text = y.ToString();
            searcherStation4.zTextBox.Text = z.ToString();

            SourceInitialize();
            trueSource.NameLabel.Text = "Источник";
            trueSource.xTextBox.Text = x.ToString();
            trueSource.yTextBox.Text = y.ToString();
            trueSource.zTextBox.Text = z.ToString();

            if (newSource.xTextBox.Text == "")
                newSource.xTextBox.Text = "300";
            if (newSource.yTextBox.Text == "")
                newSource.yTextBox.Text = "300";
            if (newSource.zTextBox.Text == "")
                newSource.zTextBox.Text = "300";
            
            searcherStation1.Run();
            searcherStation2.Run();
            searcherStation3.Run();
            searcherStation4.Run();
            trueSource.Run();
            newSource.Run();
            
            Dt12 = Math.Sqrt(Math.Pow((searcherStation1.x - trueSource.x), 2) + Math.Pow((searcherStation1.y - trueSource.y), 2) + Math.Pow((searcherStation1.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2));
            Dt23 = Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2));
            Dt34 = Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation4.x - trueSource.x), 2) + Math.Pow((searcherStation4.y - trueSource.y), 2) + Math.Pow((searcherStation4.z - trueSource.z), 2));
        }

        public MainForm()
        {
            InitializeComponent();

            refreshButtonClick(null, EventArgs.Empty);

            newSource.NameLabel.Text = "Предполагаемые координаты";
            newSource.xTextBox.Text = "300";
            newSource.yTextBox.Text = "300";
            newSource.zTextBox.Text = "300";

            stepTextBox.Text = "4";
            minStepTextBox.Text = "1";
            denominatorTextBox.Text = "2";
            delta = Convert.ToInt32(stepTextBox.Text);
            minDelta = Convert.ToInt32(minStepTextBox.Text);
            denominator = Convert.ToInt32(denominatorTextBox.Text);
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();

            HookJeeves(delta, minDelta, denominator);

            newSource.xTextBox.Text = newSource.x.ToString();
            newSource.yTextBox.Text = newSource.y.ToString();
            newSource.zTextBox.Text = newSource.z.ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
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
            Parallel.For(0, points.Count, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (i) =>
            {
                form.dtDifference.Series[0].Points.AddXY(i, points[i]);
            });
            form.Controls.Add(form.dtDifference);
            form.dtDifference.Show();



        }

        private void DtGraphButton(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            FindDtInaccuracy(delta, minDelta, denominator, points);
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
                form.dtDifference.Series[0].Points.AddXY(i, points[i]);
            }
            form.Controls.Add(form.dtDifference);
            form.dtDifference.Show();
        }



    }
}

