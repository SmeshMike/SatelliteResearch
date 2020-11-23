using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SatelliteResearch;
using ResearchModel;
using static ResearchModel.ThreadControl;

namespace SatelliteResearch
{
    public static class MathStuff
    {
        private static RadioStation searcherStation1, searcherStation2, searcherStation3, searcherStation4, newSource, trueSource;
        private static double[,] coef;
        private static double[,] coordinate;

        public static double[,] Coordinate
        {
            get => coordinate;
            set => coordinate = value;
        }

        public static RadioStation SearcherStation1
        {
            get => searcherStation1;
            set => searcherStation1 = value;
        }
        public static RadioStation SearcherStation2
        {
            get => searcherStation2;
            set => searcherStation2 = value;
        }
        public static RadioStation SearcherStation3
        {
            get => searcherStation3;
            set => searcherStation3 = value;
        }

        

        public static RadioStation SearcherStation4
        {
            get => searcherStation4;
            set => searcherStation4 = value;
        }

        public static RadioStation NewSource
        {
            get => newSource;
            set => newSource = value;
        }
        public static RadioStation TrueSource
        {
            get => trueSource;
            set => trueSource = value;
        }

        public static double Dt12
        {
            get => dt12;
            set => dt12 = value;
        }

        public static double Dt23
        {
            get => dt23;
            set => dt23 = value;
        }

        public static double Dt34
        {
            get => dt34;
            set => dt34 = value;
        }

        static double dt12, dt23, dt34;



        static double F(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                            - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2)) - dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2)) - dt34, 2);
        }



        static void CheckNeighbourPoints(double delta)
        {

            var tmpF = F(newSource);
            var f = tmpF;
            Parallel.For(0, 3, (i) =>
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
            });
        }

        public static bool HookJeeves(double delta, double minDelta, double denominator)
        {
            return ExecuteWithTimeLimit(TimeSpan.FromSeconds(1), () =>
                    {
                        var tmpSource = new RadioStation();
                        tmpSource.coordinates = new double[3];
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
            );
        }

        public static double GetNewSourceDistanceDifference()
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow(newSource.x - trueSource.x, 2) + Math.Pow(newSource.y - trueSource.y, 2) + Math.Pow(newSource.z - trueSource.z, 2)));
        }

        public static void FindDtInaccuracy(double delta, double minDelta, double denominator, List<double> inaccuracyArr, ProgressBar pb)
        {
            int err = 0;
            pb.Value = 0;
            double inaccuracy = 0;

            Random rand = new Random();

            var tmpDt12 = dt12;
            var tmpDt23 = dt23;
            var tmpDt34 = dt34;

            for (int i = 0; i < 200; i+=2)
            {
                for (int j = 0; j < 100; j++)
                {
                    err = rand.Next(i) * 2 - i;
                    dt12 = tmpDt12 + err;
                    err = rand.Next(i) * 2 - i;
                    dt23 = tmpDt23 + err;
                    err = rand.Next(i) * 2 - i;
                    dt34 = tmpDt34 + err;

                    HookJeeves(delta, minDelta, denominator);
                    inaccuracy += GetNewSourceDistanceDifference();
                }
                
                inaccuracyArr.Add(inaccuracy / 100);
                inaccuracy = 0;

                pb.PerformStep();
                pb.PerformStep();
            }

        }

        public static void FindSatelliteInaccuracy(double delta, double minDelta, double denominator, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value=0;
            double inaccuracy = 0;
            RadioStation tmp1 = searcherStation1;
            RadioStation tmp2 = searcherStation2;
            RadioStation tmp3 = searcherStation3;
            RadioStation tmp4 = searcherStation4;

            Random rand = new Random();
            for (int i = 0; i < 200; i+=2)
            {
                
                for (int j = 0; j < 100; j++)
                {
                    AddInaccuracy(searcherStation1, tmp1, rand, i);
                    AddInaccuracy(searcherStation2, tmp2, rand, i);
                    AddInaccuracy(searcherStation3, tmp3, rand, i);
                    AddInaccuracy(searcherStation4, tmp4, rand, i);

                    HookJeeves(delta, minDelta, denominator);
                    inaccuracy += GetNewSourceDistanceDifference();
                }
                inaccuracyArr.Add(inaccuracy / 100);
                inaccuracy = 0;

                pb.PerformStep();
                pb.PerformStep();
            }
        }

        static void AddInaccuracy(RadioStation rs, RadioStation tmpRs, Random rand, int iter)
        {
            rs.x = tmpRs.x + (rand.Next(iter) * 2 - iter);
            rs.y = tmpRs.y + (rand.Next(iter) * 2 - iter);
            rs.z = tmpRs.z + (rand.Next(iter) * 2 - iter);
        }

        static void CreatePlane()
        {
            coef = new double[3,3];
            coef[0, 0] = -Math.Sin(Math.PI / 180 * 155);
            coef[0, 1] = 0;
            coef[0, 2] = Math.Cos(Math.PI / 180 * 155);
            coef[1, 0] = -Math.Sin(Math.PI / 180 * 155) * Math.Cos(Math.PI / 180 * 120);
            coef[1, 1] = -Math.Sin(Math.PI / 180 * 155) * Math.Sin(Math.PI / 180 * 120);
            coef[1, 2] = Math.Cos(Math.PI / 180 * 120);
            coef[2, 0] = -Math.Sin(Math.PI / 180 * 155) * Math.Cos(Math.PI / 180 * 240);
            coef[2, 1] = -Math.Sin(Math.PI / 180 * 155) * Math.Sin(Math.PI / 180 * 240);
            coef[2, 2] = Math.Cos(Math.PI / 180 * 240);
        }

        public static void GenerateDots(short n)
        {
            CreatePlane();
            coordinate = new double[n,3];

            long r = 25420000;

            Random random = new Random();

            var x = random.Next(Convert.ToInt32(r)) * 2 - r;
            coordinate[0, 0] = x;
            var y = (long)Math.Sqrt(r * r - x * x);
            coordinate[0, 1] = y;
            var z = Convert.ToInt64((-coef[0, 0] * x - coef[0, 1] * y) / coef[0, 2]);
            coordinate[0, 2] = z;

            
            for (int i = 1; i < n; ++i)
            {
                do
                {
                    x = random.Next(Convert.ToInt32(r)) * 2 - r;
                    y = (long) Math.Sqrt(r * r - x * x);
                    z = Convert.ToInt64((-coef[i-1, 0] * x - coef[i - 1, 1] * y) / coef[i - 1, 2]);

                } while (Math.Sqrt((coordinate[i - 1, 0] - x) * (coordinate[i - 1, 0] - x))< 1000000 || Math.Sqrt((coordinate[i - 1, 1] - y) * (coordinate[i - 1, 1] - y)) < 1000000 || Math.Sqrt((coordinate[i - 1, 2] - z) * (coordinate[i - 1, 2] - z)) < 1000000);

                coordinate[i, 0] = x;
                coordinate[i, 1] = y;
                coordinate[i, 2] = z;
            }



        }
    }
}
