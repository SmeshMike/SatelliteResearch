using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SatelliteResearch;
using static ResearchModel.Extensions;

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

        const double w0 = 6000000000;
        const double c = 300000000;
        const double rE = 6370000;

        public static RadioStation tmpSource;
        public static RadioStation SearcherStation1 { get; set; }

        public static RadioStation SearcherStation2 { get; set; }

        public static RadioStation SearcherStation3 { get; set; }

        public static RadioStation SearcherStation4 { get; set; }

        public static RadioStation NewSource { get; set; }

        public static RadioStation TrueSource { get; set; }

        public static double Dt12 { get; set; }
        public static double Dt13 { get; set; }
        public static double Dt14 { get; set; }

        public static double Dt23 { get; set; }
        public static double Dt24 { get; set; }

        public static double Dt34 { get; set; }

        public static double Dw12 { get; set; }

        public static double Dw13 { get; set; }

        public static double Dw14 { get; set; }
        public static double Dw23 { get; set; }
        public static double Dw24 { get; set; }
        public static double Dw34 { get; set; }
        public static double W1 { get; set; }
        public static double W2 { get; set; }
        public static double W3 { get; set; }

        public delegate double F(RadioStation source);

        public static double GetSourceDifference()
        {
            var tmp = Math.Sqrt(Math.Pow(NewSource.X - TrueSource.X, 2) + Math.Pow(NewSource.Y - TrueSource.Y, 2) + Math.Pow(NewSource.Z - TrueSource.Z, 2));
            return tmp;
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
            var tmp1 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt13, 2);
            var tmp3 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation4.X - source.X), 2) + Math.Pow((SearcherStation4.Y - source.Y), 2) + Math.Pow((SearcherStation4.Z - source.Z), 2)) - Dt14, 2);
            var tmp4 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2);
            var tmp5 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation4.X - source.X), 2) + Math.Pow((SearcherStation4.Y - source.Y), 2) + Math.Pow((SearcherStation4.Z - source.Z), 2)) - Dt24, 2);
            var tmp6 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation4.X - source.X), 2) + Math.Pow((SearcherStation4.Y - source.Y), 2) + Math.Pow((SearcherStation4.Z - source.Z), 2)) - Dt34, 2);

            return tmp1 + tmp2 + tmp3 + tmp4 + tmp5 + tmp6;
        }


        public static double DdSpaceF(RadioStation source)
        {
            var v1 = V(1, source);
            var v2 = V(2, source);
            var v3 = V(3, source);
            var v4 = V(4, source);
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) - Dw13 / W1, 2);
            var tmp3 = Math.Pow((V(1, source) - V(4, source)) / (c + V(1, source)) - Dw14 / W1, 2);
           // var tmp4 = Math.Pow((V(2, source) - V(3, source)) / (c + V(2, source)) - Dw23 / W2, 2);
            //var tmp5 = Math.Pow((V(2, source) - V(3, source)) / (c + V(2, source)) - Dw24 / W2, 2);
            //var tmp6 = Math.Pow((V(3, source) - V(4, source)) / (c + V(3, source)) - Dw34 / W3, 2);
            return tmp1 + tmp2 + tmp3;// + tmp4 + tmp5 + tmp6;  
        }

        private static double SumSpaceF(RadioStation source)
        {
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) - Dw13 / W1, 2);
            var tmp3 = Math.Pow((V(2, source) - V(3, source)) / (c + V(2, source)) - Dw23 / W2, 2);
            var tmp4 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp5 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt13, 2);
            var tmp6 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) -
                                Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2);
            return tmp1 + tmp2 +/* tmp3 +*/ tmp4 + tmp5 /*+ tmp6*/;
        }

        private static double DmEarthF(RadioStation source)
        {
            var tmp1 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt13, 2);
            var tmp3 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2);
            var tmp4 = Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);

            return tmp1 + tmp2 + tmp3 + tmp4;
        }


        private static double DdEarthF(RadioStation source)
        {
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) - Dw13 / W1, 2);
            var tmp3 = Math.Pow((V(2, source) - V(3, source)) / (c + V(2, source)) - Dw23 / W2, 2);
            var tmp4 = Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1 + tmp2 /*+ tmp3*/ + tmp4;
        }

        private static double SumEarthF(RadioStation source)
        {
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp3 = Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1+tmp2+tmp3;
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

        public static F Initialization(FunctionType type)
        {
            F function;
            switch (type)
            {
                case 0:
                    function = DmSpaceF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dt13 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt14 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));
                    Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt24 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));
                    Dt34 = Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));


                    break;
                case (FunctionType)1:
                    function = DdSpaceF;
                    Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
                    Dw13 = (V(1, TrueSource) - V(3, TrueSource)) * w0 / c;
                    Dw14 = (V(1, TrueSource) - V(4, TrueSource)) * w0 / c;
                    Dw23 = (V(2, TrueSource) - V(3, TrueSource)) * w0 / c;
                    Dw24 = (V(2, TrueSource) - V(4, TrueSource)) * w0 / c;
                    Dw34 = (V(3, TrueSource) - V(4, TrueSource)) * w0 / c;
                    W1 = (1 + (V(1, TrueSource)) / c) * w0;
                    W2 = (1 + (V(2, TrueSource)) / c) * w0;
                    W3 = (1 + (V(3, TrueSource)) / c) * w0;
                    break;
                case (FunctionType)2:
                    function = SumSpaceF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dt13 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
                    Dw13 = (V(1, TrueSource) - V(3, TrueSource)) * w0 / c;
                    Dw23 = (V(2, TrueSource) - V(3, TrueSource)) * w0 / c;
                    W1 = (1 + (V(1, TrueSource)) / c) * w0;
                    W2 = (1 + (V(2, TrueSource)) / c) * w0;
                    break;
                case (FunctionType)3:
                    function = DmEarthF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dt13 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    break;
                case (FunctionType)4:
                    function = DdEarthF;

                    Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
                    Dw13 = (V(1, TrueSource) - V(3, TrueSource)) * w0 / c;
                    Dw23 = (V(2, TrueSource) - V(3, TrueSource)) * w0 / c;
                    W1 = (1 + (V(1, TrueSource)) / c) * w0;
                    W2 = (1 + (V(2, TrueSource)) / c) * w0;
                    break;
                case (FunctionType)5:
                    function = SumEarthF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
                    W1 = (1 + (V(1, TrueSource)) / c) * w0;
                    break;
                default:
                    function = DmSpaceF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dt13 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt14 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));
                    Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt24 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));
                    Dt34 = Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation4.X - TrueSource.X), 2) + Math.Pow((SearcherStation4.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation4.Z - TrueSource.Z), 2));
                    break;
            }

            return function;
        }

        public static bool HookJeeves(double delta, double minDelta, double denominator, F function )
        {
            //return ExecuteWithTimeLimit(TimeSpan.FromSeconds(30), () =>
            //        {



            
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

        public static void FindDtInaccuracy(double delta, double minDelta, double denominator,F function, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value = 0;
            double inaccuracy = 0;
            int iCount = 100;
            int jCount = 1000;
            pb.Maximum = iCount;
            Random rand = new Random();

            var tmpDt12 = Dt12;
            var tmpDt13 = Dt13;
            var tmpDt14 = Dt14;
            var tmpDt23 = Dt23;
            var tmpDt24 = Dt24;
            var tmpDt34 = Dt34;
            tmpSource = new RadioStation();

            for (int i = 0; i < iCount; ++i)
            {
                for (int j = 0; j < jCount; j++)
                {
                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;

                    var err = rand.NextDouble()*i * 2 - i;
                    Dt12 = tmpDt12 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt13 = tmpDt13 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt14 = tmpDt14 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt23 = tmpDt23 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt24 = tmpDt24 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt34 = tmpDt34 + err;

                    HookJeeves(delta, minDelta, denominator, function);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / jCount);
                inaccuracy = 0;

                pb.PerformStep();
            }

        }

        public static void FindDwInaccuracy(double delta, double minDelta, double denominator, F function, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value = 0;
            double inaccuracy = 0;
            int iCount = 100;
            int jCount = 100;
            pb.Maximum = iCount;
            Random rand = new Random();

            var tmpDw12 = Dw12;
            var tmpDw13 = Dw13;
            var tmpDw14 = Dw14;
            var tmpDw23 = Dw23;
            var tmpDw24 = Dw24;
            var tmpDw34 = Dw34;

            tmpSource = new RadioStation();
            for (int i = 0; i < iCount; ++i)
            {
                for (int j = 0; j < jCount; j++)
                {
                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;

                    var err = rand.NextDouble() * i * 2 - i;
                    Dw12 = tmpDw12 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw13 = tmpDw13 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw14 = tmpDw14 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw23 = tmpDw23 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw24 = tmpDw24 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw34 = tmpDw34 + err;

                    HookJeeves(delta, minDelta, denominator, function);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / jCount);
                inaccuracy = 0;

                pb.PerformStep();
            }

        }

        public static void FindSumInaccuracy(double delta, double minDelta, double denominator, F function, List<double> inaccuracyArr, ProgressBar pb)
        {
            pb.Value = 0;
            double inaccuracy = 0;
            int iCount = 100;
            int jCount = 1000;
            pb.Maximum = iCount;
            Random rand = new Random();

            var tmpDw12 = Dw12;
            var tmpDw13 = Dw13;
            var tmpDw14 = Dw14;
            var tmpDw23 = Dw23;
            var tmpDw24 = Dw24;
            var tmpDw34 = Dw34;
            var tmpDt12 = Dt12;
            var tmpDt13 = Dt13;
            var tmpDt14 = Dt14;
            var tmpDt23 = Dt23;
            var tmpDt24 = Dt24;
            var tmpDt34 = Dt34;
            tmpSource = new RadioStation();
            for (int i = 0; i < iCount; ++i)
            {
                for (int j = 0; j < jCount; j++)
                {
                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;

                    var err = rand.NextDouble() * i * 2 - i;
                    Dw12 = tmpDw12 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw13 = tmpDw13 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw14 = tmpDw14 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw23 = tmpDw23 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw24 = tmpDw24 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dw34 = tmpDw34 + err;
                    Dt12 = tmpDt12 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt13 = tmpDt13 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt14 = tmpDt14 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt23 = tmpDt23 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt24 = tmpDt24 + err;
                    err = rand.NextDouble() * i * 2 - i;
                    Dt34 = tmpDt34 + err;

                    HookJeeves(delta, minDelta, denominator, function);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / jCount);
                inaccuracy = 0;

                pb.PerformStep();
            }

        }

        public static void FindSatelliteInaccuracy(double delta, double minDelta, double denominator, F function, List<double> inaccuracyArr, ProgressBar pb)
        {

            pb.Value = 0;
            double inaccuracy = 0;
            RadioStation tmp1 = new RadioStation();
            tmp1.Run(SearcherStation1);
            RadioStation tmp2 = new RadioStation();
            tmp2.Run(SearcherStation2);
            RadioStation tmp3 = new RadioStation();
            tmp3.Run(SearcherStation3);
            RadioStation tmp4 = new RadioStation();
            tmp4.Run(SearcherStation4);

            int iCount = 100;
            int jCount = 1000;
            pb.Maximum = iCount;
            Random rand = new Random();
            tmpSource = new RadioStation();
            for (int i = 0; i < iCount; ++i)
            {

                for (int j = 0; j < jCount; j++)
                {
                    NewSource.X = TrueSource.X - 5000;
                    NewSource.Y = TrueSource.Y - 5000;
                    NewSource.Z = TrueSource.Z - 5000;
                    AddInaccuracy(SearcherStation1, tmp1, rand, i);
                    AddInaccuracy(SearcherStation2, tmp2, rand, i);
                    AddInaccuracy(SearcherStation3, tmp3, rand, i);
                    AddInaccuracy(SearcherStation4, tmp4, rand, i);

                    HookJeeves(delta, minDelta, denominator, function);
                    inaccuracy += GetSourceDifference();
                }

                inaccuracyArr.Add(inaccuracy / jCount);
                inaccuracy = 0;

                pb.PerformStep();
            }

        }

    }
}

