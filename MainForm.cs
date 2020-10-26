using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatelliteResearch
{
    public partial class MainForm : Form
    {
        
        double dt12, dt23, dt34;
        double F(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12
                   + Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2)) - dt23
                   + Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2)) - dt34, 2);
        }

        public MainForm()
        {
            InitializeComponent();

            searcherStation1.NameLabel.Text = "Спутник 1";
            searcherStation1.xTextBox.Text = "20";
            searcherStation1.yTextBox.Text = "20";
            searcherStation1.zTextBox.Text = "20";
            searcherStation2.NameLabel.Text = "Спутник 2";
            searcherStation2.xTextBox.Text = "20";
            searcherStation2.yTextBox.Text = "25";
            searcherStation2.zTextBox.Text = "20";
            searcherStation3.NameLabel.Text = "Спутник 3";
            searcherStation3.xTextBox.Text = "20";
            searcherStation3.yTextBox.Text = "20";
            searcherStation3.zTextBox.Text = "25";
            searcherStation4.NameLabel.Text = "Спутник 4";
            searcherStation4.xTextBox.Text = "30";
            searcherStation4.yTextBox.Text = "30";
            searcherStation4.zTextBox.Text = "20";
            source.NameLabel.Text = "Источник";
            source.xTextBox.Text = "10";
            source.yTextBox.Text = "10";
            source.zTextBox.Text = "10";
            newSource.NameLabel.Text = "Предполагаемые координаты";
            newSource.xTextBox.Text = "30";
            newSource.yTextBox.Text = "30";
            newSource.zTextBox.Text = "30";
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            searcherStation1.Run();
            searcherStation2.Run();
            searcherStation3.Run();
            searcherStation4.Run();
            source.Run();
            newSource.Run();

            var step = Convert.ToInt32(stepTextBox.Text);
            var minStep = Convert.ToInt32(minStepTextBox.Text);
            var denominator = Convert.ToInt32(denominatorTextBox.Text);

            dt12 = Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2));
            dt23 = Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2));
            dt34 = Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                   - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2));

            HookJeeves(step, minStep, denominator);

            newSource.xTextBox.Text = newSource.x.ToString();
            newSource.yTextBox.Text = newSource.y.ToString();
            newSource.zTextBox.Text = newSource.z.ToString();
        }

        void CheckNeighbourPoints(int delta)
        {
            int i = 0;
            double tmpF = F(newSource);
            var f = tmpF;
            Parallel.For(0, 3, ctr=>
            {
                newSource.coordinates[i] += delta;
                f = F(newSource);
                if (tmpF >f)
                    tmpF =f;
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

        void HookJeeves( int delta, int minDelta, int denominator)
        {
            var tmpSource = new RadioStation();
            tmpSource.coordinates = new int[3];
            Array.Copy(newSource.coordinates, tmpSource.coordinates,  3);
            
            while (delta>=minDelta)
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
    }
}
