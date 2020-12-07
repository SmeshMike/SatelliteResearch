using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SatelliteResearch;

namespace ResearchModel
{
    public class ProcessCoordinates
    {
        private static double[,] _coef;
        public static long _x, _y, _z;
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
                    var x = Convert.ToInt32(Convert.ToDouble(elements[4]) * 1000);
                    allSatelliteStats[i,j, 0] = x;
                    var y = Convert.ToInt32(Convert.ToDouble(elements[5]) * 1000);
                    allSatelliteStats[i,j, 1] = y;
                    var z = Convert.ToInt32(Convert.ToDouble(elements[6]) * 1000);
                    allSatelliteStats[i,j, 2] = z;
                    var vx = Convert.ToInt32(Convert.ToDouble(elements[7]) * 1000);
                    allSatelliteStats[i,j, 3] = vx;
                    var vy = Convert.ToInt32(Convert.ToDouble(elements[8]) * 1000);
                    allSatelliteStats[i,j, 4] = vy;
                    var vz = Convert.ToInt32(Convert.ToDouble(elements[9]) * 1000);
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
                        var x = allSatelliteStats[j / 2, i, 0];
                        var y = allSatelliteStats[j / 2, i, 1];
                        var z = allSatelliteStats[j / 2, i, 2];
                        var vx = allSatelliteStats[j / 2, i, 3];
                        var vy = allSatelliteStats[j / 2, i, 4];
                        var vz = allSatelliteStats[j / 2, i, 5 ];
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
                        var x = allSatelliteStats[j / 2, i, 0];
                        var y = allSatelliteStats[j / 2, i, 1];
                        var z = allSatelliteStats[j / 2, i, 2];
                        var vx = allSatelliteStats[j / 2, i, 3];
                        var vy = allSatelliteStats[j / 2, i, 4];
                        var vz = allSatelliteStats[j / 2, i, 5];
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
        public static void GenerateStormSource()
        {
            long tmp = 6370000;
            Random rand = new Random();
            _z = rand.Next(4582194, Convert.ToInt32(tmp));
            tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - _z * _z));
            _y = rand.Next(Convert.ToInt32(tmp));
            _x = Convert.ToInt32(Math.Sqrt(tmp * tmp - _y * _y));

        }

        public static void GenerateGlonassSource()
        {
            long tmp = 6370000;
            Random rand = new Random();
            _x = rand.Next(Convert.ToInt32(tmp));
            tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - _x * _x));
            _y = rand.Next(Convert.ToInt32(tmp));
            _z = Convert.ToInt32(Math.Sqrt(tmp * tmp - _y * _y));
        }

        public static void GenerateGlonassSatellites(short n)
        {
            CreateGlonassPlane();
            Coordinate = new double[n, 6];

            long r = 25420000;

            Random random = new Random();

            _x = random.Next(Convert.ToInt32(r)) * 2 - r;
            Coordinate[0, 0] = _x;
            _y = (long) Math.Sqrt(r * r - _x * _x);
            Coordinate[0, 1] = _y;
            _z = Convert.ToInt64((long) ((-_coef[0, 0] * _x - _coef[0, 1] * _y) / _coef[0, 2]));
            Coordinate[0, 2] = _z;


            for (int i = 1; i < n; ++i)
            {
                do
                {
                    _x = random.Next(Convert.ToInt32(r)) * 2 - r;
                    _y = (long) Math.Sqrt(r * r - _x * _x);
                    _z = Convert.ToInt64((long) ((-_coef[i - 1, 0] * _x - _coef[i - 1, 1] * _y) / _coef[i - 1, 2]));

                } while (Math.Sqrt((Coordinate[i - 1, 0] - _x) * (Coordinate[i - 1, 0] - _x)) < 1000000 || Math.Sqrt((Coordinate[i - 1, 1] - _y) * (Coordinate[i - 1, 1] - _y)) < 1000000
                                                                                                      || Math.Sqrt((Coordinate[i - 1, 2] - _z) * (Coordinate[i - 1, 2] - _z)) < 1000000);

                Coordinate[i, 0] = _x;
                Coordinate[i, 1] = _y;
                Coordinate[i, 2] = _z;
                Coordinate[i, 3] = 0;
                Coordinate[i, 4] = 0;
                Coordinate[i, 5] = 0;
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
