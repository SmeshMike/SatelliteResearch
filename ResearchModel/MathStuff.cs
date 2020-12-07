using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SatelliteResearch;
using static ResearchModel.ThreadControl;

namespace ResearchModel
{
    public static class MathStuff
    {
        public static RadioStation SearcherStation1 { get; set; }

        public static RadioStation SearcherStation2 { get; set; }

        public static RadioStation SearcherStation3 { get; set; }

        public static RadioStation SearcherStation4 { get; set; }

        public static RadioStation NewSource { get; set; }

        public static RadioStation TrueSource { get; set; }

        public static double Dt12 { get; set; }

        public static double Dt23 { get; set; }

        public static double Dt34 { get; set; }

        public static double GetSourceDifference()
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow(NewSource.X - TrueSource.X, 2) + Math.Pow(NewSource.Y - TrueSource.Y, 2) + Math.Pow(NewSource.Z - TrueSource.Z, 2)));
        }

        private static double DmF(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                            - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation4.X - source.X), 2) + Math.Pow((SearcherStation4.Y - source.Y), 2) + Math.Pow((SearcherStation4.Z - source.Z), 2)) - Dt34, 2);
        }

        private static void AddInaccuracy(RadioStation rs, RadioStation tmpRs, Random rand, int iter)
        {
            rs.X = tmpRs.X + (rand.Next(iter) * 2 - iter);
            rs.Y = tmpRs.Y + (rand.Next(iter) * 2 - iter);
            rs.Z = tmpRs.Z + (rand.Next(iter) * 2 - iter);
        }

        static void CheckNeighbourPoints(double delta)
        {

            var tmpF = DmF(NewSource);
            var f = tmpF;
            Parallel.For(0, 3, (i) =>
            {
                NewSource.coordinates[i] += delta;
                f = DmF(NewSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    NewSource.coordinates[i] -= 2 * delta;
                    f = DmF(NewSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        NewSource.coordinates[i] += delta;
                }
            });
        }

        public static bool HookJeeves(double delta, double minDelta, double denominator)
        {
            return ExecuteWithTimeLimit(TimeSpan.FromSeconds(30), () =>
                    {
                        var tmpSource = new RadioStation();
                        Array.Copy(NewSource.coordinates, tmpSource.coordinates, 3);

                        while (delta >= minDelta)
                        {
                            CheckNeighbourPoints(delta);
                            if (tmpSource == NewSource)
                                delta /= denominator;
                            else
                            {
                                var f1 = DmF(NewSource);
                                NewSource.X = 2 * NewSource.X - tmpSource.X;
                                NewSource.Y = 2 * NewSource.Y - tmpSource.Y;
                                NewSource.Z = 2 * NewSource.Z - tmpSource.Z;
                                var f2 = DmF(NewSource);
                                if (f2 >= f1)
                                {
                                    NewSource.X = (NewSource.X + tmpSource.X) / 2;
                                    NewSource.Y = (NewSource.Y + tmpSource.Y) / 2;
                                    NewSource.Z = (NewSource.Z + tmpSource.Z) / 2;
                                }

                                Array.Copy(NewSource.coordinates, tmpSource.coordinates, 3);
                            }
                        }
                    }
            );
        }



        public static void FindDtInaccuracy(double delta, double minDelta, double denominator, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value = 0;
            double inaccuracy = 0;

            Random rand = new Random();

            var tmpDt12 = Dt12;
            var tmpDt23 = Dt23;
            var tmpDt34 = Dt34;

            for (int i = 0; i < 200; i += 2)
            {
                for (int j = 0; j < 100; j++)
                {
                    var err = rand.Next(i) * 2 - i;
                    Dt12 = tmpDt12 + err;
                    err = rand.Next(i) * 2 - i;
                    Dt23 = tmpDt23 + err;
                    err = rand.Next(i) * 2 - i;
                    Dt34 = tmpDt34 + err;

                    HookJeeves(delta, minDelta, denominator);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / 100);
                inaccuracy = 0;

                pb.PerformStep();
                pb.PerformStep();
            }

        }

        public static void FindSatelliteInaccuracy(double delta, double minDelta, double denominator, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value = 0;
            double inaccuracy = 0;
            RadioStation tmp1 = SearcherStation1;
            RadioStation tmp2 = SearcherStation2;
            RadioStation tmp3 = SearcherStation3;
            RadioStation tmp4 = SearcherStation4;

            Random rand = new Random();
            for (int i = 0; i < 200; i += 2)
            {

                for (int j = 0; j < 100; j++)
                {
                    AddInaccuracy(SearcherStation1, tmp1, rand, i);
                    AddInaccuracy(SearcherStation2, tmp2, rand, i);
                    AddInaccuracy(SearcherStation3, tmp3, rand, i);
                    AddInaccuracy(SearcherStation4, tmp4, rand, i);

                    HookJeeves(delta, minDelta, denominator);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / 100);
                inaccuracy = 0;

                pb.PerformStep();
                pb.PerformStep();
            }
        }

    }
}

