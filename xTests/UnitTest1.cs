using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
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
        RadioStation newSource = new RadioStation();

        int dt12 = 3;
        int dt23 = 3;
        int dt34 = 3;

        double F(RadioStation source)
        {
            return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                            - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2)) - dt23, 2)
                   + Math.Pow(Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                              - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2)) - dt34, 2);
        }
        [Theory]
        [InlineData(15)]
        public void TestMethod1(int delta)
        {
            
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

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

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            output.WriteLine("{0}", ts.TotalMilliseconds);
        }
        [Theory]
        [InlineData(15)]
        public void TestMethod2(int delta)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

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
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;

            output.WriteLine("{0}", ts.TotalMilliseconds);
        }
    }
}
