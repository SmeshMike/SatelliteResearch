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

namespace SatelliteResearch
{
    public partial class MainForm : Form
    {
        double dt12, dt23, dt34;
        private int delta, minDelta, denominator;

        double F(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                            - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2)) - dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2)) - dt34, 2);
        }

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
        }

        public MainForm()
        {
            InitializeComponent();

            refreshButtonClick(null, EventArgs.Empty);

            newSource.NameLabel.Text = "Предполагаемые координаты";
            newSource.xTextBox.Text = "300";
            newSource.yTextBox.Text = "300";
            newSource.zTextBox.Text = "300";
            stepTextBox.Text = "1024";
            minStepTextBox.Text = "1";
            denominatorTextBox.Text = "2";
            delta = Convert.ToInt32(stepTextBox.Text);
            minDelta = Convert.ToInt32(minStepTextBox.Text);
            denominator = Convert.ToInt32(denominatorTextBox.Text);

            searcherStation1.Run();
            searcherStation2.Run();
            searcherStation3.Run();
            searcherStation4.Run();
            trueSource.Run();
            newSource.Run();
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();

            dt12 = Math.Sqrt(Math.Pow((searcherStation1.x - trueSource.x), 2) + Math.Pow((searcherStation1.y - trueSource.y), 2) + Math.Pow((searcherStation1.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2));
            dt23 = Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2));
            dt34 = Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation4.x - trueSource.x), 2) + Math.Pow((searcherStation4.y - trueSource.y), 2) + Math.Pow((searcherStation4.z - trueSource.z), 2));

            HookJeeves(delta, minDelta, denominator);

            newSource.xTextBox.Text = newSource.x.ToString();
            newSource.yTextBox.Text = newSource.y.ToString();
            newSource.zTextBox.Text = newSource.z.ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";

            
        }

        void CheckNeighbourPoints(int delta)
        {

            double tmpF = F(newSource);
            var f = tmpF;
            int i = 0;
            Parallel.For(0, 3, ctr =>
            {
                newSource.coordinates[i] += delta;
                f = F(newSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    newSource.coordinates[i] -= 2 * delta;
                    f = F(newSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        newSource.coordinates[i] += delta;
                }

                i++;
            });
        }

        void HookJeeves(int delta, int minDelta, int denominator)
        {
            var tmpSource = new RadioStation();
            tmpSource.coordinates = new int[3];
            Array.Copy(newSource.coordinates, tmpSource.coordinates, 3);

            while (delta >= minDelta)
            {
                CheckNeighbourPoints(delta);
                if (tmpSource == newSource)
                    delta /= denominator;
                else
                {
                    var f1 = F(newSource);
                    newSource.x = 2 * newSource.x - tmpSource.x;
                    newSource.y = 2 * newSource.y - tmpSource.y;
                    newSource.z = 2 * newSource.z - tmpSource.z;
                    var f2 = F(newSource);
                    if (f2 >= f1)
                    {
                        newSource.x = (newSource.x + tmpSource.x) / 2;
                        newSource.y = (newSource.y + tmpSource.y) / 2;
                        newSource.z = (newSource.z + tmpSource.z) / 2;
                    }

                    Array.Copy(newSource.coordinates, tmpSource.coordinates, 3);
                }
            }

        }

        private void DCoordinatesGraphButtonClick(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            FindSatelliteInaccuracy(delta, minDelta, denominator, points);
            ChartsForm form;

            form = new ChartsForm();

            form.dtDifference.Series.Clear();
            var series = new Series
            {
                    Name = "",
                    Color = Color.Green,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line
            };
            form.dtDifference.Series.Add(series);
            Parallel.ForEach<double>(points, point => form.dtDifference.Series[0].Points.AddY(point));
            form.dtDifference.Invalidate();
            form.Show();
        }

        private void DtGraphButton(object sender, EventArgs e)
        {
            List<double> points = new List<double>();
            FindDtInaccuracy(delta, minDelta, denominator, points);
            ChartsForm form;
            if (ChartsForm.ActiveForm! == null)
            {
                form = new ChartsForm();
            }
            else
            {
                form = (ChartsForm)ChartsForm.ActiveForm;
            }

            form.dtDifference.Series[0].Points.AddY(points);
            form.Show();
        }

        double GetNewSourceDistanceDifference()
        {
            return Math.Sqrt(Math.Pow(newSource.x - trueSource.x, 2) + Math.Pow(newSource.y - trueSource.y, 2) + Math.Pow(newSource.z - trueSource.z, 2));
        }

        void FindDtInaccuracy(int delta, int minDelta, int denominator, List<double> inaccuracyArr)
        {
            int iter = 0;
            int err;
            double inaccuracy = 0;
            Random rand = new Random();

            var tmpDt12 = dt12;
            var tmpDt23 = dt23;
            var tmpDt34 = dt34;

            Parallel.For(0, 100, function =>
                    {
                        Parallel.For(0, 1000, function =>
                                {
                                    err = rand.Next(iter) * 2 - iter;
                                    dt12 = tmpDt12 + err;
                                    err = rand.Next(iter) * 2 - iter;
                                    dt23 = tmpDt23 + err;
                                    err = rand.Next(iter) * 2 - iter;
                                    dt34 = tmpDt34 + err;

                                    HookJeeves(delta, minDelta, denominator);
                                    inaccuracy += GetNewSourceDistanceDifference();
                                }
                        );
                        inaccuracyArr.Add(inaccuracy/1000);
                        inaccuracy = 0;
                        iter++;
                    }
            );

        }

        void FindSatelliteInaccuracy(int delta, int minDelta, int denominator, List<double> inaccuracyArr)
        {
            int iter = 0;
            double inaccuracy = 0;
            RadioStation tmp1 = searcherStation1;
            RadioStation tmp2 = searcherStation2;
            RadioStation tmp3 = searcherStation3;
            RadioStation tmp4 = searcherStation4;

            Random rand = new Random();
            Parallel.For(0, 5, function =>
                    {
                        Parallel.For(0, 1, function =>
                                {
                                    AddInaccuracy(searcherStation1, tmp1, rand, iter);
                                    AddInaccuracy(searcherStation2, tmp2, rand, iter);
                                    AddInaccuracy(searcherStation3, tmp3, rand, iter);
                                    AddInaccuracy(searcherStation4, tmp4, rand, iter);

                                    HookJeeves(delta, minDelta, denominator);
                                    inaccuracy += GetNewSourceDistanceDifference();
                                }
                        );
                        inaccuracyArr.Add(inaccuracy / 1000);
                        inaccuracy = 0;
                        iter++;
                    }
            );
        }

        void AddInaccuracy(RadioStation rs, RadioStation tmpRs, Random rand, int iter)
        {
            rs.x = tmpRs.x + (rand.Next(iter) * 2 - iter);
            rs.y = tmpRs.y + (rand.Next(iter) * 2 - iter);
            rs.z = tmpRs.z + (rand.Next(iter) * 2 - iter);
        }


    }
}

