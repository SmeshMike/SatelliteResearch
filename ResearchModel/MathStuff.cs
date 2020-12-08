﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SatelliteResearch;
using static ResearchModel.ThreadControl;

namespace ResearchModel
{
    public static class MathStuff
    {

        public enum FunctionType : byte
        {
            dmSpace =0,
            ddSpace =1,
            sumSpace =2,
            dmEarth = 3,
            ddEarth = 4,
            sumEarth = 5,
        }

        const long w0 = 6000000000;
        const int c = 300000000;
        const long rE = 6370000;
        public static RadioStation SearcherStation1 { get; set; }

        public static RadioStation SearcherStation2 { get; set; }

        public static RadioStation SearcherStation3 { get; set; }

        public static RadioStation SearcherStation4 { get; set; }

        public static RadioStation NewSource { get; set; }

        public static RadioStation TrueSource { get; set; }

        public static double Dt12 { get; set; }

        public static double Dt23 { get; set; }

        public static double Dt34 { get; set; }

        public static double Dw12 { get; set; }

        public static double Dw13 { get; set; }

        public static double Dw14 { get; set; }
        public static double W1 { get; set; }

        public delegate double F(RadioStation source);

        public static double GetSourceDifference()
        {
            var tmp = Convert.ToInt32(Math.Sqrt(Math.Pow(NewSource.X - TrueSource.X, 2) + Math.Pow(NewSource.Y - TrueSource.Y, 2) + Math.Pow(NewSource.Z - TrueSource.Z, 2)));
            return tmp;
        }

        private static double NormVectX(RadioStation satellite)
        {
            return (TrueSource.X - satellite.X) / Math.Sqrt((TrueSource.X - satellite.X) * (TrueSource.X - satellite.X) + (TrueSource.Y - satellite.Y) * (TrueSource.Y - satellite.Y) +
                                                             (TrueSource.Z - satellite.Z) * (TrueSource.Z - satellite.Z));
        }

        private static double NormVectY(RadioStation satellite)
        {
            return (TrueSource.Y - satellite.Y) / Math.Sqrt((TrueSource.X - satellite.X) * (TrueSource.X - satellite.X) + (TrueSource.Y - satellite.Y) * (TrueSource.Y - satellite.Y) +
                                                            (TrueSource.Z - satellite.Z) * (TrueSource.Z - satellite.Z));
        }

        private static double NormVectZ(RadioStation satellite)
        {
            return (TrueSource.Z - satellite.Z) / Math.Sqrt((TrueSource.X - satellite.X) * (TrueSource.X - satellite.X) + (TrueSource.Y - satellite.Y) * (TrueSource.Y - satellite.Y) +
                                                            (TrueSource.Z - satellite.Z) * (TrueSource.Z - satellite.Z));
        }
        private static double V(int i, RadioStation source)
        {
            var tmp = new RadioStation[4];
            tmp[0] = SearcherStation1;
            tmp[1] = SearcherStation2;
            tmp[2] = SearcherStation3;
            tmp[3] = SearcherStation4;

            return (tmp[i - 1].Vx * (source.X - tmp[i-1].X) + tmp[i - 1].Vy * (source.Y - tmp[i - 1].Y) + tmp[i - 1].Vz * (source.Z - tmp[i - 1].Z))
                   / (Math.Sqrt((source.X - tmp[i - 1].X) * (source.X - tmp[i - 1].X) + (source.Y - tmp[i - 1].Y) * (source.Y - tmp[i - 1].Y) + (source.Z - tmp[i - 1].Z) * (source.Z - tmp[i - 1].Z)));
        }

        private static double DmSpaceF(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                            - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation4.X - source.X), 2) + Math.Pow((SearcherStation4.Y - source.Y), 2) + Math.Pow((SearcherStation4.Z - source.Z), 2)) - Dt34, 2);
        }


        private static double DdSpaceF(RadioStation source)
        {
            return Math.Pow((V(1, source) - V(2, source)) / (c + V(1,source)) - Dw12 / W1, 2)
                   + Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) * Dw13/ W1, 2)
                   + Math.Pow((V(1, source) - V(4, source)) / (c + V(1, source)) * Dw14/ W1, 2);
        }

        private static double SumSpaceF(RadioStation source)
        {
            return Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2)
                   + Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) * Dw13 / W1, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2);
        }

        private static double DmEarthF(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                            - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2)
                   + Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
        }


        private static double DdEarthF(RadioStation source)
        {
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) * Dw13 / W1, 2);
            var tmp3 = Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1 + tmp2 + tmp3;
        }

        private static double SumEarthF(RadioStation source)
        {
            return Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                              - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2)
                   + Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
        }

        private static void AddInaccuracy(RadioStation rs, RadioStation tmpRs, Random rand, int iter)
        {
            rs.X = tmpRs.X + (rand.Next(iter) * 2 - iter);
            rs.Y = tmpRs.Y + (rand.Next(iter) * 2 - iter);
            rs.Z = tmpRs.Z + (rand.Next(iter) * 2 - iter);
        }

        static void CheckNeighbourPoints(double delta, F function)
        {

            var tmpF = function(NewSource);
            double f;
            //Parallel.For(0, 3, (i) =>
            //{
            for(var i =0; i<3;++i)
            { 
                NewSource.coordinates[i] += delta;
                f = function(NewSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    NewSource.coordinates[i] -= 2 * delta;
                    f = function(NewSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        NewSource.coordinates[i] += delta;
                }
            }
            //);
        }

        public static bool HookJeeves(double delta, double minDelta, double denominator, FunctionType type)
        {
            //return ExecuteWithTimeLimit(TimeSpan.FromSeconds(30), () =>
            //        {
            F function;
            switch (type)
            {
                case 0:
                    function = DmSpaceF;
                    break;
                case (FunctionType) 1:
                    function = DdSpaceF;
                    break;
                case (FunctionType) 2:
                    function = SumSpaceF;
                    break;
                case (FunctionType) 3:
                    function = DmEarthF;
                    break;
                case (FunctionType) 4:
                    function = DdEarthF;
                    break;
                case (FunctionType) 5:
                    function = SumEarthF;
                    break;
                default:
                    function = DmSpaceF;
                    break;
            }

            Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                   Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
            Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                   Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
            Dt34 = Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2)) -
                   Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));


            Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
            Dw13 = (V(1, TrueSource) - V(3, TrueSource)) * w0 / c;
            Dw14 = (V(1, TrueSource) - V(4, TrueSource)) * w0 / c;

            W1 = (1 + (V(1, TrueSource) ) / c)*w0;

            //Dw12 = (SearcherStation1.Vx * NormVectX(SearcherStation1) + SearcherStation1.Vy * NormVectY(SearcherStation1) + SearcherStation1.Vz * NormVectZ(SearcherStation1) -
            //        (SearcherStation2.Vx * NormVectX(SearcherStation2) + SearcherStation2.Vy * NormVectY(SearcherStation2) + SearcherStation2.Vz * NormVectZ(SearcherStation2))) * w0 / c;
            //Dw13 = (SearcherStation1.Vx * NormVectX(SearcherStation1) + SearcherStation1.Vy * NormVectY(SearcherStation1) + SearcherStation1.Vz * NormVectZ(SearcherStation1) -
            //        (SearcherStation3.Vx * NormVectX(SearcherStation3) + SearcherStation3.Vy * NormVectY(SearcherStation3) + SearcherStation3.Vz * NormVectZ(SearcherStation3))) * w0 / c;
            //Dw14 = (SearcherStation1.Vx * NormVectX(SearcherStation1) + SearcherStation1.Vy * NormVectY(SearcherStation1) + SearcherStation1.Vz * NormVectZ(SearcherStation1) -
            //        (SearcherStation4.Vx * NormVectX(SearcherStation4) + SearcherStation4.Vy * NormVectY(SearcherStation4) + SearcherStation4.Vz * NormVectZ(SearcherStation4))) * w0 / c;

            //W1 = (1 + (SearcherStation1.X * NormVectX(SearcherStation1) + SearcherStation1.Y * NormVectY(SearcherStation1) + SearcherStation1.Z * NormVectZ(SearcherStation1)) / c);


            var tmpSource = new RadioStation();
            Array.Copy(NewSource.coordinates, tmpSource.coordinates, 3);

            while (delta >= minDelta)
            {
                CheckNeighbourPoints(delta, function);
                if (tmpSource == NewSource)
                    delta /= denominator;
                else
                {
                    var f1 = function(NewSource);
                    NewSource.X = 2 * NewSource.X - tmpSource.X;
                    NewSource.Y = 2 * NewSource.Y - tmpSource.Y;
                    NewSource.Z = 2 * NewSource.Z - tmpSource.Z;
                    var f2 = function(NewSource);
                    if (f2 >= f1)
                    {
                        NewSource.X = (NewSource.X + tmpSource.X) / 2;
                        NewSource.Y = (NewSource.Y + tmpSource.Y) / 2;
                        NewSource.Z = (NewSource.Z + tmpSource.Z) / 2;
                    }

                    Array.Copy(NewSource.coordinates, tmpSource.coordinates, 3);
                }
            }

            return true;
            //}
            //);
        }

        public static void FindDtInaccuracy(double delta, double minDelta, double denominator,FunctionType type, List<double> inaccuracyArr, ProgressBar pb)
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

                    HookJeeves(delta, minDelta, denominator, type);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / 100);
                inaccuracy = 0;

                pb.PerformStep();
                pb.PerformStep();
            }

        }

        public static void FindSatelliteInaccuracy(double delta, double minDelta, double denominator, FunctionType type, List<double> inaccuracyArr, ProgressBar pb)
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

                    HookJeeves(delta, minDelta, denominator, type);
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

