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


            if (stormRadioButton.Checked && (type == FunctionType.ddSpace || type == FunctionType.dmSpace))
            {
                var tmp = GetStormCoordinates(4);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                searcherStation1.Run(tmp[0]);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                searcherStation2.Run(tmp[1]);


                searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
                searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
                searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
                searcherStation3.Run(tmp[2]);


                searcherStation4.xTextBox.Text = Coordinate[3, 0].ToString();
                searcherStation4.yTextBox.Text = Coordinate[3, 1].ToString();
                searcherStation4.zTextBox.Text = Coordinate[3, 2].ToString();
                searcherStation4.Run(tmp[3]);

            }
            else if (type == FunctionType.ddEarth || type == FunctionType.dmEarth || type == FunctionType.sumSpace)
            {
                var tmp = GetStormCoordinates(3);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                searcherStation1.Run(tmp[0]);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                searcherStation2.Run(tmp[1]);


                searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
                searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
                searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
                searcherStation3.Run(tmp[2]);


                searcherStation4.xTextBox.Text = (0).ToString();
                searcherStation4.yTextBox.Text = (0).ToString();
                searcherStation4.zTextBox.Text = (0).ToString();

            }
            else if (type == FunctionType.sumEarth)
            {
                var tmp = GetStormCoordinates(2);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                searcherStation1.Run(tmp[0]);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                searcherStation2.Run(tmp[1]);


                searcherStation3.xTextBox.Text = (0).ToString();
                searcherStation3.yTextBox.Text = (0).ToString();
                searcherStation3.zTextBox.Text = (0).ToString();


                searcherStation4.xTextBox.Text = (0).ToString();
                searcherStation4.yTextBox.Text = (0).ToString();
                searcherStation4.zTextBox.Text = (0).ToString();

            }
            else
            {
                
                GenerateGlonassSatellites(4);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                var tmp = new RadioStation(Coordinate[0, 0], Coordinate[0, 1], Coordinate[0, 2], Coordinate[0, 3], Coordinate[0, 4], Coordinate[0, 5]);
                searcherStation1.Run(tmp);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                tmp = new RadioStation(Coordinate[1, 0], Coordinate[1, 1], Coordinate[1, 2], Coordinate[1, 3], Coordinate[1, 4], Coordinate[1, 5]);
                searcherStation2.Run(tmp);


                searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
                searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
                searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
                tmp = new RadioStation(Coordinate[2, 0], Coordinate[2, 1], Coordinate[2, 2], Coordinate[2, 3], Coordinate[2, 4], Coordinate[2, 5]);
                searcherStation3.Run(tmp);


                searcherStation4.xTextBox.Text = Coordinate[3, 0].ToString();
                searcherStation4.yTextBox.Text = Coordinate[3, 1].ToString();
                searcherStation4.zTextBox.Text = Coordinate[3, 2].ToString();
                tmp = new RadioStation(Coordinate[3, 0], Coordinate[3, 1], Coordinate[3, 2], Coordinate[3, 3], Coordinate[3, 4], Coordinate[3, 5]);
                searcherStation4.Run(tmp);
            }

            GenerateSource();

            trueSource.xTextBox.Text = x.ToString();
            trueSource.yTextBox.Text = y.ToString();
            trueSource.zTextBox.Text = z.ToString();
            trueSource.Run();
            ////if (newSource.xTextBox.Text == "")
            //newSource.xTextBox.Text = (ProcessCoordinates.x - 2500).ToString();
            ////if (newSource.yTextBox.Text == "")
            //newSource.yTextBox.Text = (ProcessCoordinates.y + 5000).ToString();
            ////if (newSource.zTextBox.Text == "")
            //newSource.zTextBox.Text = (ProcessCoordinates.z - 1500).ToString();
            newSource.xTextBox.Text = (x-5000).ToString();
            newSource.yTextBox.Text = (y-5000).ToString();
            newSource.zTextBox.Text = (z-5000).ToString();
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

            newSource.xTextBox.Text = Convert.ToDouble(newSource.X).ToString();
            newSource.yTextBox.Text = Convert.ToDouble(newSource.Y).ToString();
            newSource.zTextBox.Text = Convert.ToDouble(newSource.Z).ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
            errorLabel.Text = Convert.ToInt32(GetSourceDifference()).ToString();
        }

        private void glonassRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtonClick(sender, e);
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

