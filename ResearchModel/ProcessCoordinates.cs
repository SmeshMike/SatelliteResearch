using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SatelliteResearch;

namespace ResearchModel
{
    public class ProcessCoordinates
    {
        private static double[,] _coef;
        public static double X { get; set; }
        public static double Y { get; set; }
        public static double Z { get; set; }

        public static Process CoordProcess { get; set; }

        //public static double X {get Coordinate[]; }
        private static double[,,] allSatelliteStats;
        public static double[,] Coordinate { get; set; }


        public static void InitializeStormSatellites()
        {
            string[] files = 
            {
                    "Молния 1",
                    "Геостационар парный 1",
                    "Молния 2",
                    "Геостационар парный 2",
                    "Геостационар центральный"
            };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\SatteliteData\\") + "Молния 1.txt";
            var lineCount = File.ReadAllLines(path).Length;
            allSatelliteStats = new double[files.Length,lineCount-7,6];
            for (var i = 0; i < files.Length; ++i)
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\SatteliteData\\") + files[i] + ".txt";
                var line = File.ReadAllLines(path);
                for (int j = 0; j < lineCount-7; j++)
                {
                    
                    var elements = line[j+7].Replace('.', ',').Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var x = Convert.ToDouble(elements[4]) * 1000;
                    allSatelliteStats[i,j, 0] = x;
                    var y = Convert.ToDouble(elements[5]) * 1000;
                    allSatelliteStats[i,j, 1] = y;
                    var z = Convert.ToDouble(elements[6]) * 1000;
                    allSatelliteStats[i,j, 2] = z;
                    var vx = Convert.ToDouble(elements[7]) * 1000;
                    allSatelliteStats[i,j, 3] = vx;
                    var vy = Convert.ToDouble(elements[8]) * 1000;
                    allSatelliteStats[i,j, 4] = vy;
                    var vz = Convert.ToDouble(elements[9]) * 1000;
                    allSatelliteStats[i,j, 5] = vz;
                }
            }
        }

        public static List<RadioStation> GetStormCoordinates(int count)
        {
            List<RadioStation> result = new List<RadioStation>();
            var lineCount = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\SatteliteData\\") + "Молния 1.txt").Length;
            var i = new Random().Next(lineCount+1);
            Coordinate = new double[count,6];
            switch (count)
            {
                case 2:
                    for (var j = 0; j < 3; j += 2)
                    {
                        var x = allSatelliteStats[j, i, 0];
                        var y = allSatelliteStats[j, i, 1];
                        var z = allSatelliteStats[j, i, 2];
                        var vx = allSatelliteStats[j, i, 3];
                        var vy = allSatelliteStats[j, i, 4];
                        var vz = allSatelliteStats[j, i, 5 ];
                        Coordinate[j / 2, 0] = x;
                        Coordinate[j / 2, 1] = y;
                        Coordinate[j / 2, 2] = z;
                        Coordinate[j / 2, 3] = vx;
                        Coordinate[j / 2, 4] = vy;
                        Coordinate[j / 2, 5] = vz;
                        var tmp = new RadioStation(x, y, z, vx, vy, vz);
                        result.Add(tmp);
                    }
                    break;
                case 3:
                    for (var j = 0; j < 5; j+=2)
                    {
                        var x = allSatelliteStats[j, i, 0];
                        var y = allSatelliteStats[j, i, 1];
                        var z = allSatelliteStats[j, i, 2];
                        var vx = allSatelliteStats[j, i, 3];
                        var vy = allSatelliteStats[j , i, 4];
                        var vz = allSatelliteStats[j, i, 5];
                        Coordinate[j / 2, 0] = x;
                        Coordinate[j / 2, 1] = y;
                        Coordinate[j / 2, 2] = z;
                        Coordinate[j / 2, 3] = vx;
                        Coordinate[j / 2, 4] = vy;
                        Coordinate[j / 2, 5] = vz;
                        var tmp = new RadioStation(x, y, z, vx, vy, vz);
                        result.Add(tmp);
                    }
                    break;
                case 4:
                    for (var j = 0; j < 4; ++j)
                    {
                        ///////////////////////////////////////////////
                        var x = allSatelliteStats[j, i, 0];
                        var y = allSatelliteStats[j, i, 1];
                        var z = allSatelliteStats[j, i, 2];
                        var vx = allSatelliteStats[j, i, 3];
                        var vy = allSatelliteStats[j, i, 4];
                        var vz = allSatelliteStats[j, i, 5];
                        Coordinate[j, 0] = x;
                        Coordinate[j, 1] = y;
                        Coordinate[j, 2] = z;
                        Coordinate[j, 3] = vx;
                        Coordinate[j, 4] = vy;
                        Coordinate[j, 5] = vz;
                        var tmp = new RadioStation(x,y,z,vx,vy,vz);
                        result.Add(tmp);
                    }
                    break;
                    
            }

            return result;
        }
        public static void GenerateSource()
        {
            double tmp = 63781370;
            Random rand = new Random();
            Z = rand.NextDouble() * tmp;
            tmp = Math.Sqrt(tmp * tmp - Z * Z);
            Y = rand.NextDouble()* tmp;
            X = Math.Sqrt(tmp * tmp - Y * Y);
        }

        public static void GenerateSourceWithElevationMap()
        {
            Random random = new Random();
            int picWidth = 43200;
            int picLength = 21600;

            var lat = random.Next(21601);
            var lon = random.Next(43201);
            double latD = 0;
            double lonD = 0;


            StreamWriter myStreamWriter = CoordProcess.StandardInput;

            string inputText= $"{lat} {lon}";
            if (lat == picLength / 2)
                latD = 0;
            else
                latD = (double) (picLength / 2 - lat) / (picLength / 2) * 90;
            if (lon == picWidth / 2)
                lonD = 0;
            else
                lonD = (double) (lon - picWidth / 2) / (picWidth / 2) * 180;

            var r = 63781370;
            myStreamWriter.WriteLine(inputText);
            
            var result = Convert.ToInt32(CoordProcess.StandardOutput.ReadLine());
            r += result;

            X = r * Math.Cos(Math.PI * Convert.ToDouble(latD) / 180) * Math.Cos(Math.PI * Convert.ToDouble(lonD) / 180);
            Y = r * Math.Cos(Math.PI * Convert.ToDouble(latD) / 180) * Math.Sin(Math.PI * Convert.ToDouble(lonD) / 180);
            Z = r * Math.Sin(Math.PI * Convert.ToDouble(latD) / 180);

            // Wait for the sort process to write the sorted text lines.
        }

        public static void GenerateGlonassSatellites(short n)
        {
            CreateGlonassPlane();
            Coordinate = new double[n, 6];

            double r = 25420000;

            Random random = new Random();

            var x = random.NextDouble() * 2 * r - r;
            Coordinate[0, 0] = x;
            var y =  Math.Sqrt(r * r - x * x);
            Coordinate[0, 1] = y;
            var z = Convert.ToDouble(((-_coef[0, 0] * x - _coef[0, 1] * y) / _coef[0, 2]));
            Coordinate[0, 2] = z;
            Coordinate[0, 3] = new Random().NextDouble() * 5000;
            Coordinate[0, 4] = new Random().NextDouble() * 5000;
            Coordinate[0, 5] = new Random().NextDouble() * 5000;

            for (int i = 1; i < n; ++i)
            {
                do
                {
                    x = random.NextDouble()*2*r- r;
                    y = Math.Sqrt(r * r - x * x);
                    z = Convert.ToDouble((long) ((-_coef[i - 1, 0] * x - _coef[i - 1, 1] * y) / _coef[i - 1, 2]));

                } while (Math.Sqrt((Coordinate[i - 1, 0] - x) * (Coordinate[i - 1, 0] - x)) < 1000000 || Math.Sqrt((Coordinate[i - 1, 1] - y) * (Coordinate[i - 1, 1] - y)) < 1000000
                                                                                                      || Math.Sqrt((Coordinate[i - 1, 2] - z) * (Coordinate[i - 1, 2] - z)) < 1000000);

                Coordinate[i, 0] = x;
                Coordinate[i, 1] = y;
                Coordinate[i, 2] = z;
                Coordinate[i, 3] = new Random().NextDouble() * 5000;
                Coordinate[i, 4] = new Random().NextDouble() * 5000;
                Coordinate[i, 5] = new Random().NextDouble() * 5000;
            }
        }

        private static void CreateGlonassPlane()
        {
            _coef = new double[3, 3];
            _coef[0, 0] = -Math.Sin(Math.PI / 180 * 155);
            _coef[0, 1] = 0;
            _coef[0, 2] = Math.Cos(Math.PI / 180 * 155);
            _coef[1, 0] = -Math.Sin(Math.PI / 180 * 155) * Math.Cos(Math.PI / 180 * 120);
            _coef[1, 1] = -Math.Sin(Math.PI / 180 * 155) * Math.Sin(Math.PI / 180 * 120);
            _coef[1, 2] = Math.Cos(Math.PI / 180 * 120);
            _coef[2, 0] = -Math.Sin(Math.PI / 180 * 155) * Math.Cos(Math.PI / 180 * 240);
            _coef[2, 1] = -Math.Sin(Math.PI / 180 * 155) * Math.Sin(Math.PI / 180 * 240);
            _coef[2, 2] = Math.Cos(Math.PI / 180 * 240);
        }
    }
}
