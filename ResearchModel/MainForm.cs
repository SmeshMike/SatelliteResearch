﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NPOI.SS.Formula.Functions;
using static ResearchModel.MathStuff;
using static ResearchModel.ProcessCoordinates;
using static ResearchModel.Extensions;

namespace ResearchModel
{

    public partial class MainForm : Form
    {

        private double delta, minDelta, denominator;

        private FunctionType type;


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        #region RefreshButtonClick
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

            ProcessCoordinates();
            
            newSource.xTextBox.Text = (x-5000).ToString();
            newSource.yTextBox.Text = (y-5000).ToString();
            newSource.zTextBox.Text = (z-5000).ToString();
        }
        #endregion

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
        private void glonassRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtonClick(sender, e);
        }

        private void euclideanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            trueSourceGroupBox.Enabled = false;
            trueSource.Enabled = true;
            ProcessCoordinates();
        }

        private void sphericalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            trueSourceGroupBox.Enabled = true;
            trueSource.Enabled = false;
            ProcessCoordinates();
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

            progressBar.Value = 1;
            progressBar.Step = 1;

            stepTextBox.Text = "1024";
            minStepTextBox.Text = "0,015625";
            denominatorTextBox.Text = "2";

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
            F function = Initialization(type);

            var i = Convert.ToDouble(errorTextBox.Text);
            var err = new Random().NextDouble() * i * 2 - i;
            Dt12 += err;
            err = new Random().NextDouble() * i * 2 - i;
            Dt13 += err;
            err = new Random().NextDouble() * i * 2 - i;
            Dt14 += err;
            err = new Random().NextDouble() * i * 2 - i;
            Dt23  += err;
            err = new Random().NextDouble() * i * 2 - i;
            Dt24 += err;
            err = new Random().NextDouble() * i * 2 - i;
            Dt34 += err;

            tmpSource = new RadioStation();
            while (!HookJeeves(delta, minDelta, denominator, function))
            {
                
                RefreshButtonClick(null, EventArgs.Empty);
            }

            ProcessCoordinates();
            newSource.xTextBox.Text = Convert.ToDouble(newSource.X).ToString();
            newSource.yTextBox.Text = Convert.ToDouble(newSource.Y).ToString();
            newSource.zTextBox.Text = Convert.ToDouble(newSource.Z).ToString();
            longtitudeNewTextBox.Text = newSource.X == 0 ? (newSource.Y > 0 ? 90 : -90).ToString() :( Math.Atan(newSource.Y / newSource.X) / Math.PI * 180).ToString();
            latitudeNewTextBox.Text = newSource.Y * newSource.Y + newSource.X * newSource.X == 0 ? (newSource.Z > 0 ? 90 : -90).ToString() :(Math.Atan(newSource.Z / Math.Sqrt(newSource.Y * newSource.Y + newSource.X * newSource.X)) / Math.PI * 180).ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
            errorLabel.Text = Convert.ToInt32(GetSourceDifference()).ToString();
        }

        
        private void DrawMapButton_Click(object sender, EventArgs e)
        {
            var leftFi = Convert.ToDouble(leftFiTextBox.Text);
            var rightFi = Convert.ToDouble(rightFiTextBox.Text);
            var upperTeta = Convert.ToDouble(upperTetaTextBox.Text);
            var bottomTeta = Convert.ToDouble(bottomTetaTextBox.Text);
            var brightness = Convert.ToInt32(brightnessCoefTextBox.Text);
            Bitmap startMap = new Bitmap("../../../../WorldMap.jpg");
            Bitmap heat = new Bitmap(startMap.Width, startMap.Height, PixelFormat.Format32bppArgb);
            byte iIntense;
            RadioStation rs = new RadioStation();
            List<double> firstResults = new List<double>();
            const double r = 6370000;

            double backColor;
            if (type == FunctionType.dmSpace)
                backColor = 100000000000000000;
            else
                backColor = 0.000000000001;

            F function = Initialization(type);

            for (double fi = -180; fi <= 180 ; fi+=(double)360/3508)
            {
                for (double teta = 90; teta >= -90; teta -= (double)180/2480)
                {
                    rs.X = r * Math.Cos(Math.PI * teta / 180) * Math.Cos(Math.PI * fi / 180);
                    rs.Y = r * Math.Cos(Math.PI * teta / 180) * Math.Sin(Math.PI * fi / 180);
                    rs.Z = r * Math.Sin(Math.PI * teta / 180);

                    var t = function(rs);
                    if((fi<=rightFi&&fi>=leftFi)&&(teta<=upperTeta&&teta>=bottomTeta))
                        firstResults.Add(t);
                    else
                        firstResults.Add(backColor);
                }
            }

            var max = firstResults.Max();
            var min = firstResults.Min();
            var i = 0;
            int tmpX = 0, tmpY = 0;

            for (int x = 0; x < 3508; x ++)
            {
                for (int y = 0; y < 2480; y++)
                {
                    iIntense = Convert.ToByte(250 * Math.Tanh(brightness * firstResults[i] / max));
                    var color = Color.FromArgb(255, Convert.ToByte(255 - iIntense), Convert.ToByte(255 - iIntense), Convert.ToByte(255 - iIntense));
                    if (min == firstResults[i])
                    {
                        tmpX = x;
                        tmpY = y;
                    }

                    heat.SetPixel(x, y, color);
                    i++;
                }
            }


            var longtitude = Convert.ToDouble(longtitudeTrueTextBox.Text);
            var latitude = Convert.ToDouble(latitudeTrueTextBox.Text);
            var lX = Convert.ToInt32((double)(3508 / (double)360) * longtitude + 3508 / 2);
            var lY = Convert.ToInt32(2480 / 2 - (double)(2480 / (double)180) * latitude);




            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)180 / 255.0F;
            using (Bitmap image1 = startMap)
            {
                using (Bitmap image2 = heat)
                {
                    // just change the alpha
                    ColorMatrix matrix = new ColorMatrix(new[]
                    {
                            new[] {1F, 0, 0, 0, 0},
                            new[] {0, 1F, 0, 0, 0},
                            new[] {0, 0, 1F, 0, 0},
                            new[] {0, 0, 0, alphaNorm, 0},
                            new[] {0, 0, 0, 0, 1F}});

                    ImageAttributes imageAttributes = new ImageAttributes();
                    imageAttributes.SetColorMatrix(matrix);

                    using Graphics g = Graphics.FromImage(image1);
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawImage(image2,
                        new Rectangle(0, 0, image1.Width, image1.Height),
                        0,
                        0,
                        image2.Width,
                        image2.Height,
                        GraphicsUnit.Pixel,
                        imageAttributes);
                }

                image1.SetPixel(lX, lY, Color.Red);
                if (lX > 0)
                    image1.SetPixel(lX - 1, lY, Color.Red);
                if(lX < 3507)
                    image1.SetPixel(lX +1, lY, Color.Red);
                if (lY < 2479)
                    image1.SetPixel(lX, lY + 1, Color.Red);
                if (lY > 0)
                    image1.SetPixel(lX, lY - 1, Color.Red);
                if (lX > 1)
                    image1.SetPixel(lX- 2, lY, Color.Red);
                if (lY < 3506)
                    image1.SetPixel(lX + 2, lY, Color.Red);
                if (lY < 2478)
                    image1.SetPixel(lX, lY + 2, Color.Red);
                if (lY > 1)
                    image1.SetPixel(lX, lY - 2, Color.Red);
                if (lX > 1 && lY < 2479)
                    image1.SetPixel(lX - 2, lY+1, Color.Red);
                if (lY > 0 && lX < 3506)
                    image1.SetPixel(lX+ 2, lY-1, Color.Red);
                if (lY < 2478 && lX < 3507)
                    image1.SetPixel(lX+1, lY+ 2, Color.Red);
                if (lY > 1 && lX > 0)
                    image1.SetPixel(lX-1, lY - 2, Color.Red);

                image1.SetPixel(tmpX, tmpY, Color.Green);

                if(tmpX>0)
                    image1.SetPixel(tmpX-1, tmpY, Color.Green);
                if (tmpX < 3507)
                    image1.SetPixel(tmpX+1, tmpY, Color.Green);
                if (tmpY < 2479)
                    image1.SetPixel(tmpX, tmpY+1, Color.Green);
                if (tmpY > 0)
                    image1.SetPixel(tmpX, tmpY-1, Color.Green);
                image1.Save("../../../../Heat.jpg", ImageFormat.Jpeg);
            }
            
        }

        #region SumMethodsRefreshClick
        private void SumMethodsRefreshClick(object sender, EventArgs e)
        {
            if (stormRadioButton.Checked)
            {
                if (type == FunctionType.sumEarth)
                {
                    SearcherStation1.X = 2911.589288 * 1000;
                    SearcherStation1.Y = 21076.211455 * 1000;
                    SearcherStation1.Z = 27428.013975 * 1000;
                    SearcherStation1.Vx = 0.119659 * 1000;
                    SearcherStation1.Vy = 0.133511 * 1000;
                    SearcherStation1.Vz = 2.413740 * 1000;
                    searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                    searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                    searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();



                    SearcherStation2.X = -2817.404310 * 1000;
                    SearcherStation2.Y = -21060.364441 * 1000;
                    SearcherStation2.Z = 27450.014111 * 1000;
                    SearcherStation2.Vx = -0.110239 * 1000;
                    SearcherStation2.Vy = -0.138972 * 1000;
                    SearcherStation2.Vz = 2.409070 * 1000;

                    searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                    searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                    searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                    TrueSource.X = 1986426.176356317;
                    TrueSource.Y = 4004412.6912230053;
                    TrueSource.Z = -4538247.46397313;
                    trueSource.xTextBox.Text = TrueSource.X.ToString();
                    trueSource.yTextBox.Text = TrueSource.Y.ToString();
                    trueSource.zTextBox.Text = TrueSource.Z.ToString();

                    NewSource.X = 1986426.176356317 - 5000;
                    NewSource.Y = 4004412.6912230053 - 5000;
                    NewSource.Z = -4538247.46397313 - 5000;
                    newSource.xTextBox.Text = NewSource.X.ToString();
                    newSource.yTextBox.Text = NewSource.Y.ToString();
                    newSource.zTextBox.Text = NewSource.Z.ToString();

                }
                else if (type == FunctionType.sumSpace)
                {
                    SearcherStation1.X = 3639853.9609999997;
                    SearcherStation1.Y = 8921804.635;
                    SearcherStation1.Z = -2463549.466;
                    SearcherStation1.Vx = -235.661;
                    SearcherStation1.Vy = 6045.283;
                    SearcherStation1.Vz = 4953.17;
                    searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                    searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                    searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();


                    SearcherStation2.X = -3646032.795;
                    SearcherStation2.Y = -8927601.529;
                    SearcherStation2.Z = -2433225.547;
                    SearcherStation2.Vx = 247.96099999999998;
                    SearcherStation2.Vy = -6033.1460000000006;
                    SearcherStation2.Vz = 4965.738;
                    searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                    searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                    searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                    SearcherStation3.X = 42164135.243;
                    SearcherStation3.Y = 13.411;
                    SearcherStation3.Z = -53855.533;
                    SearcherStation3.Vx = -0.005;
                    SearcherStation3.Vy = 0.001;
                    SearcherStation3.Vz = -3.62;
                    searcherStation3.xTextBox.Text = SearcherStation3.X.ToString();
                    searcherStation3.yTextBox.Text = SearcherStation3.Y.ToString();
                    searcherStation3.zTextBox.Text = SearcherStation3.Z.ToString();

                    TrueSource.X = 6034365.9541306281;
                    TrueSource.Y = 1457231.9555445788;
                    TrueSource.Z = 1428216.5659902152;
                    trueSource.xTextBox.Text = TrueSource.X.ToString();
                    trueSource.yTextBox.Text = TrueSource.Y.ToString();
                    trueSource.zTextBox.Text = TrueSource.Z.ToString();

                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;
                    newSource.xTextBox.Text = NewSource.X.ToString();
                    newSource.yTextBox.Text = NewSource.Y.ToString();
                    newSource.zTextBox.Text = NewSource.Z.ToString();
                }
            }
            else
            {
                if (type == FunctionType.sumEarth)
                {
                    SearcherStation1.X = 3050980.433;
                    SearcherStation1.Y = 21166840.116;
                    SearcherStation1.Z = 30014830.297000002;
                    SearcherStation1.Vx = 121.42699999999999;
                    SearcherStation1.Vy = 31.867;
                    SearcherStation1.Vz = 2129.0589999999997;
                    searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                    searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                    searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();


                    SearcherStation2.X = -2946854.585;
                    SearcherStation2.Y = -21158099.944000002;
                    SearcherStation2.Z = 30031391.82;
                    SearcherStation2.Vx = -113.425;
                    SearcherStation2.Vy = -38.827;
                    SearcherStation2.Vz = 2124.197;
                    searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                    searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                    searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                    TrueSource.X = 5826418.0915079154;
                    TrueSource.Y = 2484117.6945833224;
                    TrueSource.Z = 677430.07196854427;
                    trueSource.xTextBox.Text = TrueSource.X.ToString();
                    trueSource.yTextBox.Text = TrueSource.Y.ToString();
                    trueSource.zTextBox.Text = TrueSource.Z.ToString();

                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;
                    newSource.xTextBox.Text = NewSource.X.ToString();
                    newSource.yTextBox.Text = NewSource.Y.ToString();
                    newSource.zTextBox.Text = NewSource.Z.ToString();
                }
                else if (type == FunctionType.sumSpace)
                {
                    SearcherStation1.X = 213004.652;
                    SearcherStation1.Y = 10941934.4259;
                    SearcherStation1.Z = -1154924.830;
                    SearcherStation1.Vx = -2116.552;
                    SearcherStation1.Vy = -4769.517;
                    SearcherStation1.Vz = -5131.828;
                    searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                    searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                    searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();


                    SearcherStation2.X = -210490.857;
                    SearcherStation2.Y = -10938668.62;
                    SearcherStation2.Z = -1185906.305;
                    SearcherStation2.Vx = 2127.344;
                    SearcherStation2.Vy = 4783.659;
                    SearcherStation2.Vz = -5112.496;
                    searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                    searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                    searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                    SearcherStation3.X = 42164147.087;
                    SearcherStation3.Y = 26.495;
                    SearcherStation3.Z = 43607.498;
                    SearcherStation3.Vx = -0.004;
                    SearcherStation3.Vy = -0.001;
                    SearcherStation3.Vz = 4.292;
                    searcherStation3.xTextBox.Text = SearcherStation3.X.ToString();
                    searcherStation3.yTextBox.Text = SearcherStation3.Y.ToString();
                    searcherStation3.zTextBox.Text = SearcherStation3.Z.ToString();

                    TrueSource.X = 4832916.0136173125;
                    TrueSource.Y = 4149599.7557129469;
                    TrueSource.Z = 25390.405833661556;
                    trueSource.xTextBox.Text = TrueSource.X.ToString();
                    trueSource.yTextBox.Text = TrueSource.Y.ToString();
                    trueSource.zTextBox.Text = TrueSource.Z.ToString();

                    NewSource.X = 4832916.0136173125 - 5000;
                    NewSource.Y = 4149599.7557129469 - 5000;
                    NewSource.Z = 25390.405833661556 - 5000;
                    newSource.xTextBox.Text = NewSource.X.ToString();
                    newSource.yTextBox.Text = NewSource.Y.ToString();
                    newSource.zTextBox.Text = NewSource.Z.ToString();
                }
            }
        }
        #endregion

        

        private void DCoordinatesGraphButtonClick(object sender, EventArgs e)
        {
            newSource.Run();
            List<double> points = new List<double>();
            SumMethodsRefreshClick(sender, e);

            F function = Initialization(type);
            FindSatelliteInaccuracy(delta, minDelta, denominator,function, points, progressBar);
            string satSystem = glonassRadioButton.Checked ? "GLONASS" : "Storm";
            SaveToExcel(points, type + satSystem + "Coord");
            var form = new ChartsForm();

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

        private void processCoordButton_Click(object sender, EventArgs e)
        {
            ProcessCoordinates();
        }

        private void DtGraphButton(object sender, EventArgs e)
        {
            SumMethodsRefreshClick(sender, e);

            //newSource.Run();
            List<double> points = new List<double>();
            F function = Initialization(type);
            if (type == FunctionType.ddSpace || type == FunctionType.ddEarth)
                FindDwInaccuracy(delta, minDelta, denominator, function, points, progressBar);
            else if (type == FunctionType.sumEarth || type == FunctionType.sumSpace)
                FindSumInaccuracy(delta, minDelta, denominator, function, points, progressBar);
            else
                FindDtInaccuracy(delta, minDelta, denominator, function, points, progressBar);

            string satSystem = glonassRadioButton.Checked ? "GLONASS" : "Storm";
            SaveToExcel(points, type+ satSystem + "D");
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

        void ProcessCoordinates()
        {
            if (euclideanRadioButton.Checked)
            {
                trueSource.X = Convert.ToDouble(trueSource.xTextBox.Text);
                trueSource.Y = Convert.ToDouble(trueSource.yTextBox.Text);
                trueSource.Z = Convert.ToDouble(trueSource.zTextBox.Text);
                longtitudeTrueTextBox.Text = trueSource.X == 0 ? (trueSource.Y > 0 ? 90 : -90).ToString(): (Math.Atan(trueSource.Y / trueSource.X)/Math.PI*180).ToString();
                latitudeTrueTextBox.Text = trueSource.Y * trueSource.Y + trueSource.X * trueSource.X == 0 ? (trueSource.Z > 0 ? 90 : -90).ToString() : (Math.Atan(trueSource.Z/Math.Sqrt(trueSource.Y * trueSource.Y + trueSource.X * trueSource.X)) / Math.PI * 180).ToString();
                newSource.xTextBox.Text = (x - 5000).ToString();
                newSource.yTextBox.Text = (y - 5000).ToString();
                newSource.zTextBox.Text = (z - 5000).ToString();
            }
            else if (sphericalRadioButton.Checked)
            {
                double r = 6370000;
                trueSource.X = r * Math.Cos(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180) * Math.Cos(Math.PI * Convert.ToDouble(longtitudeTrueTextBox.Text) / 180);
                trueSource.Y = r * Math.Cos(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180) * Math.Sin(Math.PI * Convert.ToDouble(longtitudeTrueTextBox.Text) / 180);
                trueSource.Z = r * Math.Sin(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180);
                trueSource.xTextBox.Text = trueSource.X.ToString();
                trueSource.yTextBox.Text = trueSource.Y.ToString();
                trueSource.zTextBox.Text = trueSource.Z.ToString();
                newSource.xTextBox.Text = (trueSource.X - 5000).ToString();
                newSource.yTextBox.Text = (trueSource.Y - 5000).ToString();
                newSource.zTextBox.Text = (trueSource.Z - 5000).ToString();
            }

        }


    }
}

