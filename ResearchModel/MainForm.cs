using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static ResearchModel.MathStuff;
using static ResearchModel.ProcessCoordinates;

namespace ResearchModel
{

    public partial class MainForm : Form
    {

        private double delta, minDelta, denominator;

        private FunctionType type;


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void RefreshButtonClick(object sender, EventArgs e)
        {
            newSource.xTextBox.Text = "0";
            newSource.yTextBox.Text = "0";
            newSource.zTextBox.Text = "0";
            


            var tmp = GetStormCoordinates(4);

            searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
            searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
            searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
            //searcherStation1 = tmp[0];
            //searcherStation1.Run();
            searcherStation1.Run(tmp[0]);

            
            searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
            searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
            searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
            //searcherStation1 = tmp[1];
            //searcherStation2.Run();
            searcherStation2.Run(tmp[1]);

            
            searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
            searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
            searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
            //searcherStation1 = tmp[2]; 
            //searcherStation3.Run();
            searcherStation3.Run(tmp[2]);

            
            searcherStation4.xTextBox.Text = Coordinate[3, 0].ToString();
            searcherStation4.yTextBox.Text = Coordinate[3, 1].ToString();
            searcherStation4.zTextBox.Text = Coordinate[3, 2].ToString();
            //searcherStation1 = tmp[3];
            //searcherStation4.Run();
            searcherStation4.Run(tmp[3]);

            GenerateStormSource();
            
            trueSource.xTextBox.Text = ProcessCoordinates._x.ToString();
            trueSource.yTextBox.Text = ProcessCoordinates._y.ToString();
            trueSource.zTextBox.Text = ProcessCoordinates._z.ToString();
            trueSource.Run();
            //if (newSource.xTextBox.Text == "")
            newSource.xTextBox.Text = (ProcessCoordinates._x - 2500).ToString();
            //if (newSource.yTextBox.Text == "")
            newSource.yTextBox.Text = (ProcessCoordinates._y + 5000).ToString();
            //if (newSource.zTextBox.Text == "")
            newSource.zTextBox.Text = (ProcessCoordinates._z - 1500).ToString();



            //Dt12 = Math.Sqrt(Math.Pow((searcherStation1.X - trueSource.X), 2) + Math.Pow((searcherStation1.Y - trueSource.Y), 2) + Math.Pow((searcherStation1.Z - trueSource.Z), 2))
            //       - Math.Sqrt(Math.Pow((searcherStation2.X - trueSource.X), 2) + Math.Pow((searcherStation2.Y -trueSource.Y), 2) + Math.Pow((searcherStation2.Z - trueSource.Z), 2));
            //Dt23 = Math.Sqrt(Math.Pow((searcherStation2.X - trueSource.X), 2) + Math.Pow((searcherStation2.Y - trueSource.Y), 2) + Math.Pow((searcherStation2.Z - trueSource.Z), 2))
            //       - Math.Sqrt(Math.Pow((searcherStation3.X - trueSource.X), 2) + Math.Pow((searcherStation3.Y - trueSource.Y), 2) + Math.Pow((searcherStation3.Z - trueSource.Z), 2));
            //Dt34 = Math.Sqrt(Math.Pow((searcherStation3.X - trueSource.X), 2) + Math.Pow((searcherStation3.Y - trueSource.Y), 2) + Math.Pow((searcherStation3.Z - trueSource.Z), 2))
            //       - Math.Sqrt(Math.Pow((searcherStation4.X - trueSource.X), 2) + Math.Pow((searcherStation4.Y - trueSource.Y), 2) + Math.Pow((searcherStation4.Z - trueSource.Z), 2));



        }
        private void ddRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (earthRadioButton.Checked && ddRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.ddEarth;
            }
            else if (ddRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                type = FunctionType.ddSpace;
            }
        }

        private void dmRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (earthRadioButton.Checked && dmRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.dmEarth;
            }
            else if (dmRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                type = FunctionType.dmSpace;
            }
        }

        private void sumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (earthRadioButton.Checked && sumRadioButton.Checked)
            {
                searcherStation3.Enabled = false;
                searcherStation4.Enabled = false;
                type = FunctionType.sumEarth;
            }
            else if(sumRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.sumSpace;
            }
        }

        private void spaceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ddRadioButton.Checked && spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                type = FunctionType.ddSpace;
            }
            else if(dmRadioButton.Checked && spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                type = FunctionType.dmSpace;
            }
            else if(spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.sumSpace;
            }
        }

        private void earthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ddRadioButton.Checked  && earthRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.ddEarth;
            }
            else if (dmRadioButton.Checked && earthRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                type = FunctionType.dmEarth;
            }
            else if (earthRadioButton.Checked)
            {
                searcherStation3.Enabled = false;
                searcherStation4.Enabled = false;
                type = FunctionType.sumEarth;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeStormSatellites();
            SearcherStation1 = searcherStation1;
            SearcherStation2 = searcherStation2;
            SearcherStation3 = searcherStation3;
            SearcherStation4 = searcherStation4;
            NewSource = newSource;
            TrueSource = trueSource;
            searcherStation1.NameLabel.Text = "Спутник 1";
            searcherStation2.NameLabel.Text = "Спутник 2";
            searcherStation3.NameLabel.Text = "Спутник 3";
            searcherStation4.NameLabel.Text = "Спутник 4";
            trueSource.NameLabel.Text = "Источник";
            newSource.NameLabel.Text = "Предполагаемые координаты";
            progressBar.Visible = true;
            progressBar.Minimum = 0;
            progressBar.Maximum = 200;
            progressBar.Value = 1;
            progressBar.Step = 1;

            stepTextBox.Text = "262144";
            minStepTextBox.Text = "0,015625";
            denominatorTextBox.Text = "64";

            RefreshButtonClick(null, EventArgs.Empty);
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            newSource.Run();
            delta = Convert.ToDouble(stepTextBox.Text.Replace('.', ','));
            minDelta = Convert.ToDouble(minStepTextBox.Text);
            denominator = Convert.ToDouble(denominatorTextBox.Text);
            while (!HookJeeves(delta, minDelta, denominator, type))
            {
                RefreshButtonClick(null, EventArgs.Empty);
            }

            newSource.xTextBox.Text = Convert.ToInt32(newSource.X).ToString();
            newSource.yTextBox.Text = Convert.ToInt32(newSource.Y).ToString();
            newSource.zTextBox.Text = Convert.ToInt32(newSource.Z).ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
            errorLabel.Text = GetSourceDifference().ToString();
        }

        private void DCoordinatesGraphButtonClick(object sender, EventArgs e)
        {
            List<double> points = new List<double>();

            FindSatelliteInaccuracy(delta, minDelta, denominator,type, points, progressBar);
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
            FindDtInaccuracy(delta, minDelta, denominator, type, points, progressBar);
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

