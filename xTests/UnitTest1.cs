using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using ResearchModel;
using SatelliteResearch;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xTests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output = new TestOutputHelper();

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        RadioStation searcherStation1 = new RadioStation();
        RadioStation searcherStation2 = new RadioStation();
        RadioStation searcherStation3 = new RadioStation();
        RadioStation searcherStation4 = new RadioStation();
        static RadioStation newSource = new RadioStation();

        int dt12 = 3;
        int dt23 = 3;
        int dt34 = 3;


        public  void HookJeeves(int delta, int minDelta, int denominator)
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
                    newSource.X = 2 * newSource.X - tmpSource.X;
                    newSource.Y = 2 * newSource.Y - tmpSource.Y;
                    newSource.Z = 2 * newSource.Z - tmpSource.Z;
                    var f2 = F(newSource);
                    if (f2 >= f1)
                    {
                        newSource.X = (newSource.X + tmpSource.X) / 2;
                        newSource.Y = (newSource.Y + tmpSource.Y) / 2;
                        newSource.Z = (newSource.Z + tmpSource.Z) / 2;
                    }

                    Array.Copy(newSource.coordinates, tmpSource.coordinates, 3);
                }
            }

        }

        void CheckNeighbourPoints(int delta)
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

        double F(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.X - source.X), 2) + Math.Pow((searcherStation1.Y - source.Y), 2) + Math.Pow((searcherStation1.Z - source.Z), 2))
                            - Math.Sqrt(Math.Pow((searcherStation2.X - source.X), 2) + Math.Pow((searcherStation2.Y - source.Y), 2) +
                                        Math.Pow((searcherStation2.Z - source.Z), 2)) - dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation2.X - source.X), 2) + Math.Pow((searcherStation2.Y - source.Y), 2) + Math.Pow((searcherStation2.Z - source.Z), 2))
                              - Math.Sqrt(
                                  Math.Pow((searcherStation3.X - source.X), 2) + Math.Pow((searcherStation3.Y - source.Y), 2) + Math.Pow((searcherStation3.Z - source.Z), 2)) -
                              dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation3.X - source.X), 2) + Math.Pow((searcherStation3.Y - source.Y), 2) + Math.Pow((searcherStation3.Z - source.Z), 2))
                              - Math.Sqrt(
                                  Math.Pow((searcherStation4.X - source.X), 2) + Math.Pow((searcherStation4.Y - source.Y), 2) + Math.Pow((searcherStation4.Z - source.Z), 2)) -
                              dt34, 2);
        }

        [Theory]
        [InlineData(15)]
        public void SpeedTestMethod1(int delta)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var i = 0; i < 1000000; i++)
            {

                double tmpF = F(newSource);
                var f = tmpF;
                newSource.coordinates[0] += delta;
                f = F(newSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    newSource.coordinates[0] -= 2 * delta;
                    f = F(newSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        newSource.coordinates[0] += delta;
                }

                newSource.coordinates[1] += delta;
                f = F(newSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    newSource.coordinates[1] -= 2 * delta;
                    f = F(newSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        newSource.coordinates[1] += delta;
                }

                newSource.coordinates[2] += delta;
                f = F(newSource);
                if (tmpF > f)
                    tmpF = f;
                else
                {
                    newSource.coordinates[2] -= 2 * delta;
                    f = F(newSource);
                    if (tmpF > f)
                        tmpF = f;
                    else
                        newSource.coordinates[2] += delta;
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            output.WriteLine("{0}", ts.TotalMilliseconds / 1000);
        }

        [Theory]
        [InlineData(15)]
        public void SpeedTestMethod2(int delta)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var j = 0; j < 1000000; j++)
            {
                var tmpF = F(newSource);
                var f = tmpF;
                for (int i = 0; i < 3; i++)
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
                }
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            output.WriteLine("{0}", ts.TotalMilliseconds / 1000);
        }

        [Theory]
        [InlineData(262144)]
        public void SpeedTestMethod3(int delta)
        {
            Stopwatch sp = new Stopwatch();
            int x, y, z;
            List<TimeSpan> tsL = new List<TimeSpan>();

            long tmp;

            for (int i = 2; i <= delta; i *= 2)
            {

                tmp = 20000000;
                Random rand = new Random();
                x = rand.Next(Convert.ToInt32(tmp));
                searcherStation1.X = x;
                tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
                y = rand.Next(Convert.ToInt32(tmp));
                searcherStation1.Y = y;
                z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
                searcherStation1.Z = z;

                x = rand.Next(Convert.ToInt32(tmp));
                searcherStation2.X = x;
                tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
                y = rand.Next(Convert.ToInt32(tmp));
                searcherStation2.Y = y;
                z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
                searcherStation2.Z = z;

                x = rand.Next(Convert.ToInt32(tmp));
                searcherStation3.X = x;
                tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
                y = rand.Next(Convert.ToInt32(tmp));
                searcherStation3.Y = y;
                z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
                searcherStation3.Z = z;

                x = rand.Next(Convert.ToInt32(tmp));
                searcherStation4.X = x;
                tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
                y = rand.Next(Convert.ToInt32(tmp));
                searcherStation4.Y = y;
                z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
                searcherStation4.Z = z;

                tmp = 6370000;
                x = rand.Next(Convert.ToInt32(tmp));
                newSource.X = x;
                tmp = Convert.ToInt64(Math.Sqrt(tmp * tmp - x * x));
                y = rand.Next(Convert.ToInt32(tmp));
                newSource.Y = y;
                z = Convert.ToInt32(Math.Sqrt(tmp * tmp - y * y));
                newSource.Z = z;
                sp.Restart();

                for (int j = 0; j < 10; j++)
                {
                    HookJeeves(delta, 1, 2);

                    newSource.xTextBox.Text = newSource.X.ToString();
                    newSource.yTextBox.Text = newSource.Y.ToString();
                    newSource.zTextBox.Text = newSource.Z.ToString();
                }

                tsL.Add(sp.Elapsed/10);

            }

            for(var i =0; i < tsL.Count; ++i)
            {
                output.WriteLine("{0}: {1}/n", Math.Pow(2, i+1), tsL[i].TotalMilliseconds);
            }
        }
    }
}
