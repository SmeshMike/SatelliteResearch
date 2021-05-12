using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using static ResearchModel.Extensions;
using System.Windows.Forms;

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
            dmEarthWithMap = 6,
            ddEarthWithMap = 7,
            sumEarthWithMap = 8,
        }

        const double w0 = 6000000000;
        const double c = 300000000;
        const double rE = 63781370;

        private static StreamWriter myStreamWriter;

        public static RadioStation tmpSource;
        public static RadioStation SearcherStation1 { get; set; }

        public static RadioStation SearcherStation2 { get; set; }

        public static RadioStation SearcherStation3 { get; set; }

        public static RadioStation SearcherStation4 { get; set; }

        public static RadioStation NewSource { get; set; }

        public static RadioStation TrueSource { get; set; }

        public static Process MathProcess { get; set; }

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

        public static double GetSimpleSourceDifference()
        {

            var tmp = Math.Sqrt((TrueSource.X - NewSource.X)* (TrueSource.X - NewSource.X) + (TrueSource.Y - NewSource.Y) * (TrueSource.Y - NewSource.Y) + (TrueSource.Z - NewSource.Z) * (TrueSource.Z - NewSource.Z));
            return tmp;
        }

        public static double GetSourceDifference()
        {
            var longtitudeTrue = TrueSource.X == 0 ? (TrueSource.Y > 0 ? 90 : -90) : (Math.Atan(TrueSource.Y / TrueSource.X) / Math.PI * 180);
            var latitudeTrue = TrueSource.Y * TrueSource.Y + TrueSource.X * TrueSource.X == 0 ? (TrueSource.Z > 0 ? 90 : -90) : (Math.Atan(TrueSource.Z / Math.Sqrt(TrueSource.Y * TrueSource.Y + TrueSource.X * TrueSource.X)) / Math.PI * 180);
            var longtitudeNew = NewSource.X == 0 ? (NewSource.Y > 0 ? 90 : -90) : (Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180);
            var latitudeNew= NewSource.Y * NewSource.Y + NewSource.X * NewSource.X == 0 ? (NewSource.Z > 0 ? 90 : -90) : (Math.Atan(NewSource.Z / Math.Sqrt(NewSource.Y * NewSource.Y + NewSource.X * NewSource.X)) / Math.PI * 180);

            double r = 63781370;
            var d = Math.Acos(Math.Round((TrueSource.X*NewSource.X+ TrueSource.Y * NewSource.Y+ TrueSource.Z * NewSource.Z)/(Math.Sqrt(TrueSource.X* TrueSource.X + TrueSource.Y * TrueSource.Y+ TrueSource.Z * TrueSource.Z)* Math.Sqrt(NewSource.X * NewSource.X + NewSource.Y * NewSource.Y + NewSource.Z * NewSource.Z)),15));
            var k = Math.Round((TrueSource.X * NewSource.X + TrueSource.Y * NewSource.Y + TrueSource.Z * NewSource.Z) / (r * r), 15);
            var kk = Math.Acos(k);
            //var d = Math.Acos(Math.Round((TrueSource.X * NewSource.X + TrueSource.Y * NewSource.Y + TrueSource.Z * NewSource.Z) / (r*r),15));
            var tmp = d * 63781370;
            //var tmp = Math.Sqrt(Math.Pow(NewSource.X - TrueSource.X, 2) + Math.Pow(NewSource.Y - TrueSource.Y, 2) + Math.Pow(NewSource.Z - TrueSource.Z, 2));
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
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) - Dw13 / W1, 2);
            var tmp3 = Math.Pow((V(1, source) - V(4, source)) / (c + V(1, source)) - Dw14 / W1, 2);
            return tmp1 + tmp2 + tmp3;
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

        private static double DmEarthWithMapF(RadioStation source)
        {

            var tmp1 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt13, 2);
            var tmp3 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation3.X - source.X), 2) + Math.Pow((SearcherStation3.Y - source.Y), 2) + Math.Pow((SearcherStation3.Z - source.Z), 2)) - Dt23, 2);

            var r = RadiusChange(source, MathProcess);

            var tmp4 = Math.Pow(r - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);

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


        private static double DdEarthWithMapF(RadioStation source)
        {
            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow((V(1, source) - V(3, source)) / (c + V(1, source)) - Dw13 / W1, 2);
            var tmp3 = Math.Pow((V(2, source) - V(3, source)) / (c + V(2, source)) - Dw23 / W2, 2);
            var r = RadiusChange(source, MathProcess);
            var tmp4 = Math.Pow(r - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1 + tmp2 /*+ tmp3*/ + tmp4;
        }

        private static double SumEarthF(RadioStation source)
        {

            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);
            var tmp3 = Math.Pow(rE - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1 + tmp2 + tmp3;
        }

        private static double SumEarthWithMapF(RadioStation source)
        {

            var tmp1 = Math.Pow((V(1, source) - V(2, source)) / (c + V(1, source)) - Dw12 / W1, 2);
            var tmp2 = Math.Pow(Math.Sqrt(Math.Pow((SearcherStation1.X - source.X), 2) + Math.Pow((SearcherStation1.Y - source.Y), 2) + Math.Pow((SearcherStation1.Z - source.Z), 2))
                                - Math.Sqrt(Math.Pow((SearcherStation2.X - source.X), 2) + Math.Pow((SearcherStation2.Y - source.Y), 2) + Math.Pow((SearcherStation2.Z - source.Z), 2)) - Dt12, 2);

            var r = RadiusChange(source, MathProcess);
            var tmp3 = Math.Pow(r - Math.Sqrt(source.X * source.X + source.Y * source.Y + source.Z * source.Z), 2);
            return tmp1+tmp2+tmp3;
        }

        public static void AddInaccuracy(RadioStation rs, RadioStation tmpRs, Random rand, int iter)
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
                case (FunctionType)6:
                    function = DmEarthWithMapF;
                    Dt12 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2));
                    Dt13 = Math.Sqrt(Math.Pow((SearcherStation1.X - TrueSource.X), 2) + Math.Pow((SearcherStation1.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation1.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    Dt23 = Math.Sqrt(Math.Pow((SearcherStation2.X - TrueSource.X), 2) + Math.Pow((SearcherStation2.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation2.Z - TrueSource.Z), 2)) -
                           Math.Sqrt(Math.Pow((SearcherStation3.X - TrueSource.X), 2) + Math.Pow((SearcherStation3.Y - TrueSource.Y), 2) + Math.Pow((SearcherStation3.Z - TrueSource.Z), 2));
                    break;
                case (FunctionType)7:
                    function = DdEarthWithMapF;
                    Dw12 = (V(1, TrueSource) - V(2, TrueSource)) * w0 / c;
                    Dw13 = (V(1, TrueSource) - V(3, TrueSource)) * w0 / c;
                    Dw23 = (V(2, TrueSource) - V(3, TrueSource)) * w0 / c;
                    W1 = (1 + (V(1, TrueSource)) / c) * w0;
                    W2 = (1 + (V(2, TrueSource)) / c) * w0;
                    break;
                case (FunctionType)8:
                    function = SumEarthWithMapF;
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

        public static bool HookJeeves(double delta, double minDelta, double denominator, F function)
        {
            return ExecuteWithTimeLimit(TimeSpan.FromSeconds(8), () =>
            {
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
            });
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
            int jCount = 1000;
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
            int iCount = 100;
            int jCount = 1000;
            RadioStation tmp1 = new RadioStation();
            tmp1.Run(SearcherStation1);
            RadioStation tmp2 = new RadioStation();
            tmp2.Run(SearcherStation2);
            RadioStation tmp3 = new RadioStation();
            tmp3.Run(SearcherStation3);
            RadioStation tmp4 = new RadioStation();
            tmp4.Run(SearcherStation4);

            double newSourceX = TrueSource.X - 5000;
            double newSourceY = TrueSource.Y - 5000;
            double newSourceZ = TrueSource.Z - 5000;

            
            pb.Maximum = iCount;
            Random rand = new Random();
            tmpSource = new RadioStation();
            for (int i = 0; i < iCount; ++i)
            {

                for (int j = 0; j < jCount; j++)
                {
                    NewSource.X = newSourceX;
                    NewSource.Y = newSourceY;
                    NewSource.Z = newSourceZ;
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

        public static void DrawClearMap(double leftFi, double rightFi, double upperTeta, double bottomTeta, int brightness, double longtitude, double latitude, double backColor,  F function)
        {
            Bitmap startMap = new Bitmap("../../../../WorldMap.jpg");
            Bitmap clearHeat = new Bitmap(startMap.Width, startMap.Height, PixelFormat.Format32bppArgb);
            
            byte iIntense;
            RadioStation rs = new RadioStation();
            List<double> clearResults = new List<double>();
            const double r = 63781370;
            tmpSource = new RadioStation();
            HookJeeves(1024, 0.015625, 2, function);
            var trueLongtitude = NewSource.X == 0 ? (NewSource.Y > 0 ? 90 : -90) : (NewSource.X > 0 ? Math.Round(Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : (Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180 > 0 ? Math.Round(-90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : Math.Round(90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7)));
            var trueLatitude = NewSource.Y * NewSource.Y + NewSource.X * NewSource.X == 0 ? (NewSource.Z > 0 ? 90 : -90) : (Math.Round(Math.Atan(NewSource.Z / Math.Sqrt(NewSource.Y * NewSource.Y + NewSource.X * NewSource.X)) / Math.PI * 180, 7));

            var i = 0;
            int iter = 0;
            int methodX = 0, methodY = 0;
            for (double fi = -180; fi <= 180; fi += (double)360 / startMap.Width)
            {
                for (double teta = 90; teta >= -90; teta -= (double)180 / startMap.Height)
                {
                    rs.X = r * Math.Cos(Math.PI * teta / 180) * Math.Cos(Math.PI * fi / 180);
                    rs.Y = r * Math.Cos(Math.PI * teta / 180) * Math.Sin(Math.PI * fi / 180);
                    rs.Z = r * Math.Sin(Math.PI * teta / 180);
                    if (trueLongtitude < fi + (double) 360 / startMap.Width && trueLongtitude > fi - (double) 360 / startMap.Width && trueLatitude < teta + (double) 180 / startMap.Height &&
                        trueLatitude > teta - (double) 180 / startMap.Height)
                    {
                        iter = i;
                    }
                    var t = function(rs);
                    i++;
                    if ((fi <= rightFi && fi >= leftFi) && (teta <= upperTeta && teta >= bottomTeta))
                        clearResults.Add(t);
                    else
                        clearResults.Add(backColor);
                }
            }


            var clearMax = clearResults.Max();
            var clearMin = clearResults.Min();

            i = 0;
            int mapX = 0, mapY = 0;

            for (int x = 0; x < startMap.Width; x++)
            {
                for (int y = 0; y < startMap.Height; y++)
                {
                    iIntense = Convert.ToByte(255 * Math.Tanh(brightness * clearResults[i] / clearMax));
                    var color = Color.FromArgb(255 - iIntense, Convert.ToByte(255-iIntense), Convert.ToByte(0), Convert.ToByte(0));
                    if (clearMin == clearResults[i])
                    {
                        mapX = x;
                        mapY = y;
                    }

                    if (iter == i)
                    {
                        methodX = x;
                        methodY = y;
                    }

                    clearHeat.SetPixel(x, y, color);
                    i++;
                }
            }

            var sourceX = Convert.ToInt32((double)(startMap.Width / (double)360) * longtitude + startMap.Width / 2);
            var sourceY = Convert.ToInt32(startMap.Height / 2 - (double)(startMap.Height / (double)180) * latitude);

            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)180 / 255.0F;

            using (Bitmap image1 = startMap)
            {
                using (Bitmap image2 = clearHeat)
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

                using (var g = Graphics.FromImage(image1))
                {
                    using (Brush bruh = new SolidBrush(Color.DarkBlue))
                    {
                        g.FillEllipse(bruh, sourceX-6, sourceY-6, 12, 12);
                    }
                    
                    using (Brush bruh = new SolidBrush(Color.ForestGreen))
                    {
                        g.FillEllipse(bruh, methodX - 4, methodY - 4, 8, 8);
                    }
                    using (Brush bruh = new SolidBrush(Color.Yellow))
                    {
                        g.FillEllipse(bruh, mapX - 2, mapY - 2, 4, 4);
                    }
                }

                image1.Save("../../../../Heat.jpg", ImageFormat.Jpeg);
            }
        }

        public static void DrawErroredMap(int errorAbs, double leftFi, double rightFi, double upperTeta, double bottomTeta, int brightness, double longtitude, double latitude, double backColor, F function)
        {
            Random rand = new Random();
            RadioStation tmp1 = new RadioStation();
            tmp1.Run(SearcherStation1);
            RadioStation tmp2 = new RadioStation();
            tmp2.Run(SearcherStation2);
            RadioStation tmp3 = new RadioStation();
            tmp3.Run(SearcherStation3);
            RadioStation tmp4 = new RadioStation();
            tmp4.Run(SearcherStation4);
            Bitmap startMap = new Bitmap("../../../../WorldMap.jpg");
            Bitmap clearHeat = new Bitmap(startMap.Width, startMap.Height, PixelFormat.Format32bppArgb);
            Bitmap erroredHeat = new Bitmap(startMap.Width, startMap.Height, PixelFormat.Format32bppArgb);
            Bitmap sumHeat = new Bitmap(startMap.Width, startMap.Height, PixelFormat.Format32bppArgb);
            byte iIntense;
            RadioStation rs = new RadioStation();
            List<double> clearResults = new List<double>();
            List<double> erroredResults = new List<double>();
            const double r = 63781370;
            tmpSource = new RadioStation();
            HookJeeves(1024, 0.015625, 2, function);
            var trueLongtitude = NewSource.X == 0 ? (NewSource.Y > 0 ? 90 : -90) : (NewSource.X > 0 ? Math.Round(Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : (Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180 > 0 ? Math.Round(-90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : Math.Round(90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7)));
            var trueLatitude = NewSource.Y * NewSource.Y + NewSource.X * NewSource.X == 0 ? (NewSource.Z > 0 ? 90 : -90) : (Math.Round(Math.Atan(NewSource.Z / Math.Sqrt(NewSource.Y * NewSource.Y + NewSource.X * NewSource.X)) / Math.PI * 180, 7));

            var i = 0;
            int clearIter = 0, erroredIter=0;
            int clearMethodX = 0, clearMethodY = 0, erroredMethodX =0, erroredMethodY = 0;

            for (double fi = -180; fi <= 180; fi += (double)360 / startMap.Width)
            {
                for (double teta = 90; teta >= -90; teta -= (double)180 / startMap.Height)
                {
                    rs.X = r * Math.Cos(Math.PI * teta / 180) * Math.Cos(Math.PI * fi / 180);
                    rs.Y = r * Math.Cos(Math.PI * teta / 180) * Math.Sin(Math.PI * fi / 180);
                    rs.Z = r * Math.Sin(Math.PI * teta / 180);

                    if (trueLongtitude < fi + (double)360 / startMap.Width && trueLongtitude > fi - (double)360 / startMap.Width && trueLatitude < teta + (double)180 / startMap.Height &&
                        trueLatitude > teta - (double)180 / startMap.Height)
                    {
                        clearIter = i;
                    }
                    var t = function(rs);
                    if ((fi <= rightFi && fi >= leftFi) && (teta <= upperTeta && teta >= bottomTeta))
                        clearResults.Add(t);
                    else
                        clearResults.Add(backColor);

                    i++;
                }
            }
            AddInaccuracy(SearcherStation1, tmp1, rand, errorAbs);
            AddInaccuracy(SearcherStation2, tmp2, rand, errorAbs);
            AddInaccuracy(SearcherStation3, tmp3, rand, errorAbs);
            AddInaccuracy(SearcherStation4, tmp4, rand, errorAbs);
            HookJeeves(1024, 0.015625, 2, function);
            var erroredLongtitude = NewSource.X == 0 ? (NewSource.Y > 0 ? 90 : -90) : (NewSource.X > 0 ? Math.Round(Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : (Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180 > 0 ? Math.Round(-90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7) : Math.Round(90 - Math.Atan(NewSource.Y / NewSource.X) / Math.PI * 180, 7)));
            var erroredLatitude = NewSource.Y * NewSource.Y + NewSource.X * NewSource.X == 0 ? (NewSource.Z > 0 ? 90 : -90) : (Math.Round(Math.Atan(NewSource.Z / Math.Sqrt(NewSource.Y * NewSource.Y + NewSource.X * NewSource.X)) / Math.PI * 180, 7));
            i = 0;
            for (double fi = -180; fi <= 180; fi += (double)360 / startMap.Width)
            {
                for (double teta = 90; teta >= -90; teta -= (double)180 / startMap.Height)
                {
                    rs.X = r * Math.Cos(Math.PI * teta / 180) * Math.Cos(Math.PI * fi / 180);
                    rs.Y = r * Math.Cos(Math.PI * teta / 180) * Math.Sin(Math.PI * fi / 180);
                    rs.Z = r * Math.Sin(Math.PI * teta / 180);


                    if (erroredLongtitude < fi + (double)360 / startMap.Width && erroredLongtitude > fi - (double)360 / startMap.Width && erroredLatitude < teta + (double)180 / startMap.Height &&
                        erroredLatitude > teta - (double)180 / startMap.Height)
                    {
                        erroredIter = i;
                    }
                    i++;

                    var t = function(rs);
                    if ((fi <= rightFi && fi >= leftFi) && (teta <= upperTeta && teta >= bottomTeta))
                        erroredResults.Add(t);
                    else
                        erroredResults.Add(backColor);

                }
            }

            Array.Copy(tmp1.coordinates, SearcherStation1.coordinates, 3);
            Array.Copy(tmp2.coordinates, SearcherStation2.coordinates, 3);
            Array.Copy(tmp3.coordinates, SearcherStation3.coordinates, 3);
            Array.Copy(tmp4.coordinates, SearcherStation4.coordinates, 3);

            var clearMax = clearResults.Max();
            var clearMin = clearResults.Min();
            var erroredMax = erroredResults.Max();
            var erroredrMin = erroredResults.Min();
            i = 0;
            int clearMapX = 0, clearMapY = 0, erroredMapX = 0, erroredMapY = 0;

            for (int x = 0; x < startMap.Width; x++)
            {
                for (int y = 0; y < startMap.Height; y++)
                {

                    if (clearIter == i)
                    {
                        clearMethodX = x;
                        clearMethodY = y;
                    }
                    if (erroredIter == i)
                    {
                        erroredMethodX = x;
                        erroredMethodY = y;
                    }

                    iIntense = Convert.ToByte(255 * Math.Tanh(brightness * clearResults[i] / clearMax));

                    var color = Color.FromArgb(255 - iIntense, Convert.ToByte(255 - iIntense), Convert.ToByte(0), Convert.ToByte(0));
                    //var color = Color.FromArgb(255, Convert.ToByte(255), Convert.ToByte(iIntense), Convert.ToByte(iIntense));
                    if (clearMin == clearResults[i])
                    {
                        clearMapX = x;
                        clearMapY = y;
                    }

                    clearHeat.SetPixel(x, y, color);

                    iIntense = Convert.ToByte(255 * Math.Tanh(brightness * erroredResults[i] / erroredMax));
                    color = Color.FromArgb(255-iIntense, Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255 - iIntense));
                    if (erroredrMin == erroredResults[i])
                    {
                        erroredMapX = x;
                        erroredMapY = y;
                    }

                    erroredHeat.SetPixel(x, y, color);
                    i++;
                }
            }

            var sourceX = Convert.ToInt32((double)(startMap.Width / (double)360) * longtitude + startMap.Width / 2);
            var sourceY = Convert.ToInt32(startMap.Height / 2 - (double)(startMap.Height / (double)180) * latitude);

            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)127 / 255.0F;

            using (Bitmap image1 = clearHeat)
            {
                using (Bitmap image2 = startMap)
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

                Rectangle cloneRect = new Rectangle(0, 0, image1.Width, image1.Height);
                PixelFormat format = image1.PixelFormat;
                sumHeat = image1.Clone(cloneRect, format);
            }

            alphaNorm = (float)180 / 255.0F;
            using (Bitmap image1 = erroredHeat)
            {
                using (Bitmap image2 = sumHeat)
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

                using (var g = Graphics.FromImage(image1))
                {
                    using (Brush bruh = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(bruh, sourceX - 6, sourceY - 6, 12, 12);
                    }

                    using (Brush bruh = new SolidBrush(Color.Green))
                    {
                        g.FillEllipse(bruh, clearMethodX - 4, clearMethodY - 4, 8, 8);
                    }
                    using (Brush bruh = new SolidBrush(Color.Chocolate))
                    {
                        g.FillEllipse(bruh, erroredMethodX - 4, erroredMethodY - 4, 8, 8);
                    }
                    using (Brush bruh = new SolidBrush(Color.Yellow))
                    {
                        g.FillEllipse(bruh, clearMapX - 2, clearMapY - 2, 4, 4);
                    }
                    using (Brush bruh = new SolidBrush(Color.Aqua))
                    {
                        g.FillEllipse(bruh, erroredMapX - 2, erroredMapY - 2, 4, 4);
                    }
                }


                image1.Save("../../../../Heat.jpg", ImageFormat.Jpeg);
            }
        }

        private static int RadiusChange(RadioStation source, Process myProcess)
        {
            int picWidth = 43200;
            int picLength = 21600;

            myStreamWriter = MathProcess.StandardInput;

            var lon = source.X == 0 ? (source.Y > 0 ? 90 : -90) : (source.X > 0 ? Math.Round(Math.Atan(source.Y / source.X) / Math.PI * 180, 7) : (Math.Atan(source.Y / source.X) / Math.PI * 180 > 0 ? Math.Round(-90 - Math.Atan(source.Y / source.X) / Math.PI * 180, 7) : Math.Round(90 - Math.Atan(source.Y / source.X) / Math.PI * 180, 7)));
            var lat = source.Y * source.Y + source.X * source.X == 0 ? (source.Z > 0 ? 90 : -90) : (Math.Round(Math.Atan(source.Z / Math.Sqrt(source.Y * source.Y + source.X * source.X)) / Math.PI * 180, 7));

            int picLon = 0;
            if (Math.Abs(lon) < 0.0001)
                picLon = picWidth / 2;
            else
            {
                picLon = lon > 0 ? Convert.ToInt32(lon / 90 * picWidth / 2 + picWidth / 2) : Convert.ToInt32(lon / 90 * picWidth / 2);
            }

            int picLat = 0;
            if (Math.Abs(lat) < 0.0001)
                picLat = picLength / 2;
            else
            {
                picLat = lat > 0 ? Convert.ToInt32(lat / 180 * picLength / 2 + picLength / 2) : Convert.ToInt32(lat / 90 * picLength / 2);
            }

            if (picLat < 0)
                picLat = 0;
            if (picLat > picLength - 1)
                picLat = picLength - 1;

            if (picLon < 0)
                picLon = 0;
            if (picLon > picWidth - 1)
                picLon = picWidth - 1;


            string inputText = $"{picLat} {picLon}";
            if (lat == picLength / 2)
                lat = 0;
            else
                lat = lat < picLength / 2 ? (lat - picLength / 2) / (picLength / 2) * 90 : -(lat - picLength / 2) / (picLength / 2) * 90;
            if (lon == picWidth / 2)
                lon = 0;
            else
                lon = lon < picWidth / 2 ? (lon - picWidth / 2) / (picWidth / 2) * 90 : -(lon - picWidth / 2) / (picWidth / 2) * 90;

            myStreamWriter.WriteLine(inputText);

            var result = Convert.ToInt32(MathProcess.StandardOutput.ReadLine());

            return Convert.ToInt32(rE+result);
        }

    }
}

