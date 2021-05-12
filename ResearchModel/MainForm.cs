﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
        private Process myProcess;
        private double _delta, _minDelta, _denominator;

        private FunctionType _type;


        private void MainForm_Load(object sender, EventArgs e)
        {
            myProcess = new Process();
            //myProcess.StartInfo.FileName = @"C:\Users\pravm\AppData\Local\Programs\Python\Python39\python.exe";
            myProcess.StartInfo.FileName = @"C:\Users\Mishanya - PC\AppData\Local\Programs\Python\Python39\python.exe";
            //myProcess.StartInfo.Arguments = "\"C:\\Repository\\SatelliteResearch\\GetElevation.py\"";
            myProcess.StartInfo.Arguments = "\"D:\\VS Pojects\\SatelliteResearch\\GetElevation.py\"";
            myProcess.StartInfo.UseShellExecute = false;// Do not use OS shell
            myProcess.StartInfo.CreateNoWindow = true;  // We don't need new window
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            myProcess.StartInfo.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            myProcess.StartInfo.LoadUserProfile = true;

            myProcess.Start();
            myProcess.StandardInput.WriteLine("1 1");
            myProcess.StandardOutput.ReadLine();
            MathProcess = myProcess;
            CoordProcess = myProcess;
        }

        #region RefreshButtonClick

        private void RefreshButtonClick(object sender, EventArgs e)
        {
            newSource.xTextBox.Text = "0";
            newSource.yTextBox.Text = "0";
            newSource.zTextBox.Text = "0";
            _delta = Convert.ToDouble(stepTextBox.Text.Replace('.', ','));
            _minDelta = Convert.ToDouble(minStepTextBox.Text);
            _denominator = Convert.ToDouble(denominatorTextBox.Text);

            if (stormRadioButton.Checked && (_type == FunctionType.ddSpace || _type == FunctionType.dmSpace))
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
            else if (glonassRadioButton.Checked && (_type == FunctionType.ddSpace || _type == FunctionType.dmSpace))
            {

                GenerateGlonassSatellites(4);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                var tmp = new RadioStation(Coordinate[0, 0], Coordinate[0, 1], Coordinate[0, 2], Coordinate[0, 3], Coordinate[0, 4],
                        Coordinate[0, 5]);
                searcherStation1.Run(tmp);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                tmp = new RadioStation(Coordinate[1, 0], Coordinate[1, 1], Coordinate[1, 2], Coordinate[1, 3], Coordinate[1, 4],
                        Coordinate[1, 5]);
                searcherStation2.Run(tmp);


                searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
                searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
                searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
                tmp = new RadioStation(Coordinate[2, 0], Coordinate[2, 1], Coordinate[2, 2], Coordinate[2, 3], Coordinate[2, 4],
                        Coordinate[2, 5]);
                searcherStation3.Run(tmp);


                searcherStation4.xTextBox.Text = Coordinate[3, 0].ToString();
                searcherStation4.yTextBox.Text = Coordinate[3, 1].ToString();
                searcherStation4.zTextBox.Text = Coordinate[3, 2].ToString();
                tmp = new RadioStation(Coordinate[3, 0], Coordinate[3, 1], Coordinate[3, 2], Coordinate[3, 3], Coordinate[3, 4],
                        Coordinate[3, 5]);
                searcherStation4.Run(tmp);
            }
            else if (stormRadioButton.Checked && (_type == FunctionType.ddEarth || _type == FunctionType.ddEarthWithMap || _type == FunctionType.dmEarth || _type == FunctionType.sumSpace || _type == FunctionType.dmEarthWithMap))
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
            else if (glonassRadioButton.Checked && (_type == FunctionType.ddEarth || _type == FunctionType.ddEarthWithMap || _type == FunctionType.dmEarth || _type == FunctionType.sumSpace || _type==FunctionType.dmEarthWithMap))
            {
                GenerateGlonassSatellites(3);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                var tmp = new RadioStation(Coordinate[0, 0], Coordinate[0, 1], Coordinate[0, 2], Coordinate[0, 3], Coordinate[0, 4],
                        Coordinate[0, 5]);
                searcherStation1.Run(tmp);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                tmp = new RadioStation(Coordinate[1, 0], Coordinate[1, 1], Coordinate[1, 2], Coordinate[1, 3], Coordinate[1, 4],
                        Coordinate[1, 5]);
                searcherStation2.Run(tmp);


                searcherStation3.xTextBox.Text = Coordinate[2, 0].ToString();
                searcherStation3.yTextBox.Text = Coordinate[2, 1].ToString();
                searcherStation3.zTextBox.Text = Coordinate[2, 2].ToString();
                tmp = new RadioStation(Coordinate[2, 0], Coordinate[2, 1], Coordinate[2, 2], Coordinate[2, 3], Coordinate[2, 4],
                        Coordinate[2, 5]);
                searcherStation3.Run(tmp);

                searcherStation4.xTextBox.Text = (0).ToString();
                searcherStation4.yTextBox.Text = (0).ToString();
                searcherStation4.zTextBox.Text = (0).ToString();
            }
            else if (stormRadioButton.Checked && (_type == FunctionType.sumEarth|| _type == FunctionType.sumEarthWithMap))
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
            else if (glonassRadioButton.Checked && (_type == FunctionType.sumEarth || _type == FunctionType.sumEarthWithMap))
            {

                GenerateGlonassSatellites(2);

                searcherStation1.xTextBox.Text = Coordinate[0, 0].ToString();
                searcherStation1.yTextBox.Text = Coordinate[0, 1].ToString();
                searcherStation1.zTextBox.Text = Coordinate[0, 2].ToString();
                var tmp = new RadioStation(Coordinate[0, 0], Coordinate[0, 1], Coordinate[0, 2], Coordinate[0, 3], Coordinate[0, 4],
                        Coordinate[0, 5]);
                searcherStation1.Run(tmp);


                searcherStation2.xTextBox.Text = Coordinate[1, 0].ToString();
                searcherStation2.yTextBox.Text = Coordinate[1, 1].ToString();
                searcherStation2.zTextBox.Text = Coordinate[1, 2].ToString();
                tmp = new RadioStation(Coordinate[1, 0], Coordinate[1, 1], Coordinate[1, 2], Coordinate[1, 3], Coordinate[1, 4],
                        Coordinate[1, 5]);
                searcherStation2.Run(tmp);


                searcherStation3.xTextBox.Text = (0).ToString();
                searcherStation3.yTextBox.Text = (0).ToString();
                searcherStation3.zTextBox.Text = (0).ToString();


                searcherStation4.xTextBox.Text = (0).ToString();
                searcherStation4.yTextBox.Text = (0).ToString();
                searcherStation4.zTextBox.Text = (0).ToString();
            }

            if (AlpesСheckBox.Checked)
                GenerateAlpesSourceWithElevationMap();
            else
            {
                if (ElevationCheckBox.Checked)
                    GenerateSourceWithElevationMap();
                else if(euclideanRadioButton.Checked)
                    GenerateSource();
                else
                {
                    Random rand = new Random();
                    longtitudeTrueTextBox.Text = Math.Round(rand.NextDouble() * 360 - 180, 7).ToString();
                    latitudeTrueTextBox.Text = Math.Round(rand.NextDouble() * 180 - 90, 7).ToString();
                }
            }

            ProcessTextBoxesCoordinates();
        }
        #endregion

        private void ddRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            spaceRadioButton_CheckedChanged(sender, e);
            ElevationCheckBox_CheckedChanged(sender, e);
        }

        private void dmRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            spaceRadioButton_CheckedChanged(sender, e);
            ElevationCheckBox_CheckedChanged(sender, e);
        }

        private void sumRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            spaceRadioButton_CheckedChanged(sender, e);
            ElevationCheckBox_CheckedChanged(sender, e);
        }

        private void spaceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ddRadioButton.Checked && spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                _type = FunctionType.ddSpace;
            }
            if (ddRadioButton.Checked && spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                _type = FunctionType.ddSpace;
            }
            else if(dmRadioButton.Checked && spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = true;
                _type = FunctionType.dmSpace;
            }
            else if(spaceRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                _type = FunctionType.sumSpace;
            }
        }

        private void earthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ddRadioButton.Checked  && earthRadioButton.Checked)
            {
                ElevationCheckBox.Enabled = true;
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                _type = FunctionType.ddEarth;
            }
            else if (dmRadioButton.Checked && earthRadioButton.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                ElevationCheckBox.Enabled = true;
                _type = FunctionType.dmEarth;
            }
            else if (sumRadioButton.Checked && earthRadioButton.Checked)
            {
                searcherStation3.Enabled = false;
                searcherStation4.Enabled = false;
                ElevationCheckBox.Enabled = true;
                
                _type = FunctionType.sumEarth;
            }
            else
            {
                ElevationCheckBox.Checked = false;
                ElevationCheckBox.Enabled = false;
            }


        }

        private void ElevationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ddRadioButton.Checked && earthRadioButton.Checked && ElevationCheckBox.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                _type = FunctionType.ddEarthWithMap;
            }
            else if (dmRadioButton.Checked && earthRadioButton.Checked && ElevationCheckBox.Checked)
            {
                searcherStation3.Enabled = true;
                searcherStation4.Enabled = false;
                _type = FunctionType.dmEarthWithMap;
            }
            else if (earthRadioButton.Checked && ElevationCheckBox.Checked)
            {
                searcherStation3.Enabled = false;
                searcherStation4.Enabled = false;
                _type = FunctionType.sumEarthWithMap;
            }
            else
                earthRadioButton_CheckedChanged(sender, e);

        }

        private void glonassRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtonClick(sender, e);
        }

        private void euclideanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            trueSourceGroupBox.Enabled = false;
            trueSource.Enabled = true;
            ProcessTextBoxesCoordinates();
        }

        private void sphericalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            trueSourceGroupBox.Enabled = true;
            trueSource.Enabled = false;
            ProcessTextBoxesCoordinates();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myProcess.Close();
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

            stepTextBox.Text = "4";
            minStepTextBox.Text = "0,015625";
            denominatorTextBox.Text = "2";

            RefreshButtonClick(null, EventArgs.Empty);
        }

        private void RunButtonClick(object sender, EventArgs e)
        {
            ProcessTextBoxesCoordinates();
            Stopwatch sp = new Stopwatch();
            sp.Start();

            double r = 63781370;

            F function = Initialization(_type);
            anyInfoLabel.Text = _type.ToString();

            var errorAbs = Convert.ToDouble(errorTextBox.Text);
            var err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt12 += err;
            err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt13 += err;
            err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt14 += err;
            err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt23  += err;
            err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt24 += err;
            err = new Random().NextDouble() * errorAbs * 2 - errorAbs;
            Dt34 += err;

            tmpSource = new RadioStation();
            while (!HookJeeves(_delta, _minDelta, _denominator, function))
            {
                
                RefreshButtonClick(null, EventArgs.Empty);
            }

            //ProcessCoordinates();
            newSource.xTextBox.Text = Convert.ToDouble(newSource.X).ToString();
            newSource.yTextBox.Text = Convert.ToDouble(newSource.Y).ToString();
            newSource.zTextBox.Text = Convert.ToDouble(newSource.Z).ToString();
            //longtitudeNewTextBox.Text = newSource.X == 0 ? (newSource.Y > 0 ? 90 : -90).ToString() : (newSource.X > 0 ? Math.Round(Math.Atan(newSource.Y / newSource.X) / Math.PI * 180, 7) : (Math.Atan(newSource.Y / newSource.X) / Math.PI * 180 > 0 ? Math.Round(-90 - Math.Atan(newSource.Y / newSource.X) / Math.PI * 180, 7) : Math.Round(90 - Math.Atan(newSource.Y / newSource.X) / Math.PI * 180, 7))).ToString();
            longtitudeNewTextBox.Text = newSource.X == 0 ? (newSource.Y > 0 ? 90 : -90).ToString() : (newSource.X > 0 ? Math.Round(Math.Atan(newSource.Y / newSource.X) / Math.PI * 180, 7) : (Math.Atan(newSource.Y / newSource.X) / Math.PI * 180 > 0 ? -180 + Math.Atan(newSource.Y / newSource.X) / Math.PI * 180 : 180 +  Math.Atan(newSource.Y / newSource.X) / Math.PI * 180)).ToString();
            latitudeNewTextBox.Text = newSource.Y * newSource.Y + newSource.X * newSource.X == 0 ? (newSource.Z > 0 ? 90 : -90).ToString() : (Math.Round(Math.Atan(newSource.Z / Math.Sqrt(newSource.Y * newSource.Y + newSource.X * newSource.X)) / Math.PI * 180, 7)).ToString();
            //longtitudeNewTextBox.Text = newSource.X == 0 ? (newSource.Y > 0 ? 90 : -90).ToString() :(newSource.X > 0 ? Math.Round(Math.Atan(newSource.Y / newSource.X) / Math.PI * 180,7) : (Math.Round(Math.Atan(newSource.Y / newSource.X) / Math.PI * 180,7)> 0 ? Math.Round(-180 + Math.Atan(newSource.Y / newSource.X) / Math.PI * 180,7) : Math.Round(180 + Math.Atan(newSource.Y / newSource.X) / Math.PI * 180,7)) ).ToString();
            var time = sp.Elapsed;
            timeLabel.Text = $"{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
            errorLabel.Text = Math.Round(GetSourceDifference(),2).ToString();
        }

        
        private void DrawMapButton_Click(object sender, EventArgs e)
        {
            var leftFi = Convert.ToDouble(leftFiTextBox.Text);
            var rightFi = Convert.ToDouble(rightFiTextBox.Text);
            var upperTeta = Convert.ToDouble(upperTetaTextBox.Text);
            var bottomTeta = Convert.ToDouble(bottomTetaTextBox.Text);
            var brightness = Convert.ToInt32(brightnessCoefTextBox.Text);
            

            double backColor;
            if (_type == FunctionType.dmSpace)
                backColor = 100000000000000000;
            else
                backColor = 0.000000000001;

            F function = Initialization(_type);



            var errorAbs = Convert.ToInt32(errorTextBox.Text);


            var longtitude = Convert.ToDouble(longtitudeTrueTextBox.Text);
            var latitude = Convert.ToDouble(latitudeTrueTextBox.Text);


            if (errorAbs == 0)
                DrawClearMap( leftFi, rightFi, upperTeta, bottomTeta, brightness, longtitude, latitude, backColor, function);
            else
                DrawErroredMap(errorAbs, leftFi, rightFi, upperTeta, bottomTeta, brightness, longtitude, latitude, backColor, function);

        }

        #region SumMethodsRefreshClick
        private void SumMethodsRefreshClick(object sender, EventArgs e)
        {

            _delta = Convert.ToDouble(stepTextBox.Text.Replace('.', ','));
            _minDelta = Convert.ToDouble(minStepTextBox.Text);
            _denominator = Convert.ToDouble(denominatorTextBox.Text);
            if (stormRadioButton.Checked)
            {

                SearcherStation1.X = 3445188.207;
                SearcherStation1.Y = 20934458.335;
                SearcherStation1.Z = 36563639.56;
                SearcherStation1.Vx = 74.464;
                SearcherStation1.Vy = -112.460000;
                SearcherStation1.Vz = 1311.173;
                searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();



                SearcherStation2.X = 32299599.621;
                SearcherStation2.Y = -27102542.786000002;
                SearcherStation2.Z = -72388.251;
                SearcherStation2.Vx = 0.002;
                SearcherStation2.Vy = 0.005;
                SearcherStation2.Vz = -0.815;
                searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                SearcherStation3.X = -3320403.6259999997;
                SearcherStation3.Y = -20959212.057;
                SearcherStation3.Z = 36561003.202;
                SearcherStation3.Vx = -71.80799999999;
                SearcherStation3.Vy = 102.488;
                SearcherStation3.Vz = 1306.1229999999998;
                searcherStation3.xTextBox.Text = SearcherStation3.X.ToString();
                searcherStation3.yTextBox.Text = SearcherStation3.Y.ToString();
                searcherStation3.zTextBox.Text = SearcherStation3.Z.ToString();

                SearcherStation4.X = 38213720.761;
                SearcherStation4.Y = 17819336.248;
                SearcherStation4.Z = -1583.459;
                SearcherStation4.Vx = 0.002;
                SearcherStation4.Vy = -0.004;
                SearcherStation4.Vz = -5.34;
                searcherStation4.xTextBox.Text = SearcherStation4.X.ToString();
                searcherStation4.yTextBox.Text = SearcherStation4.Y.ToString();
                searcherStation4.zTextBox.Text = SearcherStation4.Z.ToString();


                GenerateAlpesSourceWithElevationMap();

                TrueSource.X = X;
                TrueSource.Y = Y;
                TrueSource.Z = Z;

                trueSource.xTextBox.Text = TrueSource.X.ToString();
                trueSource.yTextBox.Text = TrueSource.Y.ToString();
                trueSource.zTextBox.Text = TrueSource.Z.ToString();
                trueSource.Run();


                NewSource.X = X - 5000;
                NewSource.Y = Y - 5000;
                NewSource.Z = Z - 5000;
                newSource.xTextBox.Text = NewSource.X.ToString();
                newSource.yTextBox.Text = NewSource.Y.ToString();
                newSource.zTextBox.Text = NewSource.Z.ToString();
                newSource.Run();
            }
            else if (glonassRadioButton.Checked)
            {
                SearcherStation1.X = -5935802.5687354617;
                SearcherStation1.Y = 24717254.051876262;
                SearcherStation1.Z = 2767910.195097459;
                SearcherStation1.Vx = 4730.5250632253128;
                SearcherStation1.Vy = 2143.6350639647035;
                SearcherStation1.Vz = 2947.0247509642622;
                searcherStation1.xTextBox.Text = SearcherStation1.X.ToString();
                searcherStation1.yTextBox.Text = SearcherStation1.Y.ToString();
                searcherStation1.zTextBox.Text = SearcherStation1.Z.ToString();



                SearcherStation2.X = -22012417.926170126;
                SearcherStation2.Y = 12713373.157570107;
                SearcherStation2.Z = 10264559;
                SearcherStation2.Vx = 2688.5168406593225;
                SearcherStation2.Vy = 3268.5073107800017;
                SearcherStation2.Vz = 4955.5059056521886;
                searcherStation2.xTextBox.Text = SearcherStation2.X.ToString();
                searcherStation2.yTextBox.Text = SearcherStation2.Y.ToString();
                searcherStation2.zTextBox.Text = SearcherStation2.Z.ToString();

                SearcherStation3.X = 4678350.1451920457;
                SearcherStation3.Y = 24985784.756916914;
                SearcherStation3.Z = -16312345;
                SearcherStation3.Vx = 1493.7551815499342;
                SearcherStation3.Vy = 4186.350167350075;
                SearcherStation3.Vz = 4911.5723464226221;
                searcherStation3.xTextBox.Text = SearcherStation3.X.ToString();
                searcherStation3.yTextBox.Text = SearcherStation3.Y.ToString();
                searcherStation3.zTextBox.Text = SearcherStation3.Z.ToString();

                SearcherStation4.X = -14485131.6116681;
                SearcherStation4.Y = 20889168.537609473;
                SearcherStation4.Z = 9169112;
                SearcherStation4.Vx = 1356.56143368993;
                SearcherStation4.Vy = 1768.0281688310338;
                SearcherStation4.Vz = 3622.9812650117005;
                searcherStation4.xTextBox.Text = SearcherStation4.X.ToString();
                searcherStation4.yTextBox.Text = SearcherStation4.Y.ToString();
                searcherStation4.zTextBox.Text = SearcherStation4.Z.ToString();


                GenerateAlpesSourceWithElevationMap();

                TrueSource.X = X;
                TrueSource.Y = Y;
                TrueSource.Z = Z;

                trueSource.xTextBox.Text = TrueSource.X.ToString();
                trueSource.yTextBox.Text = TrueSource.Y.ToString();
                trueSource.zTextBox.Text = TrueSource.Z.ToString();
                trueSource.Run();


                NewSource.X = X - 5000;
                NewSource.Y = Y - 5000;
                NewSource.Z = Z - 5000;
                newSource.xTextBox.Text = NewSource.X.ToString();
                newSource.yTextBox.Text = NewSource.Y.ToString();
                newSource.zTextBox.Text = NewSource.Z.ToString();
                newSource.Run();

            }

        }
        #endregion

        

        private void DCoordinatesGraphButtonClick(object sender, EventArgs e)
        {
            newSource.Run();
            List<double> points = new List<double>();
            //SumMethodsRefreshClick(sender, e);

            F function = Initialization(_type);

            FindSatelliteInaccuracy(_delta, _minDelta, _denominator,function, points, progressBar);
            string satSystem = glonassRadioButton.Checked ? "GLONASS" : "Storm";
            SaveToExcel(points, _type+"", satSystem, "Coord");
            var form = new ChartsForm();

            form.Show();
            form.dtDifference.Series.Clear();

            var series = new Series
            {
                    Name = "dCooord\n" + _type +"\n" + satSystem + "",
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

        private void fullResearchButton_Click(object sender, EventArgs e)
        {
            
            anyInfoLabel.Text = "0/18";
            AlpesСheckBox.Checked = true;

            //stormRadioButton.Checked = false;
            //glonassRadioButton.Checked = true;
            //MakeResearchOneGroup(sender, e);

            glonassRadioButton.Checked = false;
            stormRadioButton.Checked = true;
            MakeResearchOneGroup(sender, e);
        }

        private void MakeResearchOneGroup(object sender, EventArgs e)
        {
            ElevationCheckBox.Checked = false;
            earthRadioButton.Checked = false;
            spaceRadioButton.Checked = true;
            MakeResearchOnePlace(sender, e);
            
            spaceRadioButton.Checked = false;
            earthRadioButton.Checked = true;
            MakeResearchOnePlace(sender, e);
            
            ElevationCheckBox.Checked = true;
            MakeResearchOnePlace(sender, e);
            
        }

        private void MakeResearchOnePlace(object sender, EventArgs e)
        {
            dmRadioButton.Checked = true;
            ddRadioButton.Checked = false;
            SumMethodsRefreshClick(sender, e);
            DCoordinatesGraphButtonClick(sender, e);
            DtGraphButton(sender, e);
            var explorationsDone = Convert.ToInt32(anyInfoLabel.Text.Remove(anyInfoLabel.Text.Length-3));
            explorationsDone++;
            anyInfoLabel.Text = $"{explorationsDone}/12";

            dmRadioButton.Checked = false;
            //ddRadioButton.Checked = true;
            //DCoordinatesGraphButtonClick(sender, e);
            //DtGraphButton(sender, e);
            //explorationsDone++;
            //anyInfoLabel.Text = $"{explorationsDone}/18";

            //ddRadioButton.Checked = false;
            sumRadioButton.Checked = true;
            DCoordinatesGraphButtonClick(sender, e);
            DtGraphButton(sender, e);
            explorationsDone++;
            anyInfoLabel.Text = $"{explorationsDone}/12";
        }

        private void DtGraphButton(object sender, EventArgs e)
        {
            //SumMethodsRefreshClick(sender, e);

            //newSource.Run();
            List<double> points = new List<double>();
            F function = Initialization(_type);

            string explType = "";

            if (_type == FunctionType.ddSpace || _type == FunctionType.ddEarth || _type == FunctionType.ddEarthWithMap)
            {
                FindDwInaccuracy(_delta, _minDelta, _denominator, function, points, progressBar);
                explType = "DW";
            }
            else if (_type == FunctionType.dmEarth || _type == FunctionType.dmSpace || _type == FunctionType.dmEarthWithMap)
            {
                FindDtInaccuracy(_delta, _minDelta, _denominator, function, points, progressBar);
                explType = "DT";
            }
            else
            {
                FindSumInaccuracy(_delta, _minDelta, _denominator, function, points, progressBar);
                explType = "DT + DW";
            }

            string satSystem = glonassRadioButton.Checked ? "GLONASS" : "Storm";
            


            SaveToExcel(points, _type + "", satSystem, explType);
            ChartsForm form = new ChartsForm();
            
            form.Show();

            form.dtDifference.Series.Clear();
            var series = new Series
            {
                Name = explType+ "\n" + _type + "\n" + satSystem + "",
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


        void ProcessTextBoxesCoordinates()
        {
            if (euclideanRadioButton.Checked)
            {
                trueSource.X = X;
                trueSource.Y = Y;
                trueSource.Z = Z;
                trueSource.xTextBox.Text = X.ToString();
                trueSource.yTextBox.Text = Y.ToString();
                trueSource.zTextBox.Text = Z.ToString();
                trueSource.Run();

                longtitudeTrueTextBox.Text = trueSource.X == 0 ? (trueSource.Y > 0 ? 90 : -90).ToString() : (trueSource.X > 0 ? Math.Round(Math.Atan(trueSource.Y / trueSource.X) / Math.PI * 180, 7) : (Math.Atan(trueSource.Y / trueSource.X) / Math.PI * 180 > 0 ? -180 + Math.Atan(trueSource.Y / trueSource.X) / Math.PI * 180 : 180 + Math.Atan(trueSource.Y / trueSource.X) / Math.PI * 180)).ToString();
                latitudeTrueTextBox.Text = trueSource.Y * trueSource.Y + trueSource.X * trueSource.X == 0 ? (trueSource.Z > 0 ? 90 : -90).ToString() : Math.Round(Math.Atan(trueSource.Z / Math.Sqrt(trueSource.Y * trueSource.Y + trueSource.X * trueSource.X)) / Math.PI * 180, 7).ToString();
            }
            else
            {
                double r = 63781370;
                X = r * Math.Cos(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180) * Math.Cos(Math.PI * Convert.ToDouble(longtitudeTrueTextBox.Text) / 180);
                Y = r * Math.Cos(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180) * Math.Sin(Math.PI * Convert.ToDouble(longtitudeTrueTextBox.Text) / 180);
                Z = r * Math.Sin(Math.PI * Convert.ToDouble(latitudeTrueTextBox.Text) / 180);
                trueSource.X = X;
                trueSource.Y = Y;
                trueSource.Z = Z;
                //trueSource.xTextBox.Text = X.ToString();
                //trueSource.yTextBox.Text = Y.ToString();
                //trueSource.zTextBox.Text = Z.ToString();
                trueSource.xTextBox.Text = trueSource.X.ToString();
                trueSource.yTextBox.Text = trueSource.Y.ToString();
                trueSource.zTextBox.Text = trueSource.Z.ToString();
                trueSource.Run();
            }


            newSource.xTextBox.Text = (X - 5000).ToString();
            newSource.yTextBox.Text = (Y - 5000).ToString();
            newSource.zTextBox.Text = (Z - 5000).ToString();
            newSource.Run();

        }


    }
}

