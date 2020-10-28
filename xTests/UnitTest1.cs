using System;
using System.Threading.Tasks;
using SatelliteResearch;
using Xunit;

namespace xTests
{
    public class UnitTest1
    {
        
        public delegate double F(RadioStation source);

        [Theory]
        [InlineData()]
        public void TestMethod1()
        {
            var searcherStation1 = new RadioStation { coordinates = new[] { 300, 216, 250 } };
            var searcherStation2 = new RadioStation { coordinates = new[] { 200, 216, 250 } };
            var searcherStation3 = new RadioStation { coordinates = new[] { 250, 276, 250 } };
            var searcherStation4 = new RadioStation { coordinates = new[] { 284, 27, 222 } };
            var trueSource = new RadioStation { coordinates = new[] { 20, 25, 30 } };




            var dt12 = Math.Sqrt(Math.Pow((searcherStation1.x - trueSource.x), 2) + Math.Pow((searcherStation1.y - trueSource.y), 2) + Math.Pow((searcherStation1.z - trueSource.z), 2))
                       - Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2));

            var dt23 = Math.Sqrt(Math.Pow((searcherStation2.x - trueSource.x), 2) + Math.Pow((searcherStation2.y - trueSource.y), 2) + Math.Pow((searcherStation2.z - trueSource.z), 2))
                       - Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2));

            var dt34 = Math.Sqrt(Math.Pow((searcherStation3.x - trueSource.x), 2) + Math.Pow((searcherStation3.y - trueSource.y), 2) + Math.Pow((searcherStation3.z - trueSource.z), 2))
                       - Math.Sqrt(Math.Pow((searcherStation4.x - trueSource.x), 2) + Math.Pow((searcherStation4.y - trueSource.y), 2) + Math.Pow((searcherStation4.z - trueSource.z), 2));

            double F1(RadioStation source)
            {
                return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                                - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12
                                + Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2))
                                - Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2)) - dt23
                                + Math.Sqrt(Math.Pow((searcherStation3.x - source.x), 2) + Math.Pow((searcherStation3.y - source.y), 2) + Math.Pow((searcherStation3.z - source.z), 2))
                                - Math.Sqrt(Math.Pow((searcherStation4.x - source.x), 2) + Math.Pow((searcherStation4.y - source.y), 2) + Math.Pow((searcherStation4.z - source.z), 2)) - dt34, 2);
            }

            double F2(RadioStation source)
            {
                return Math.Pow(Math.Sqrt(Math.Pow((searcherStation1.x - source.x), 2) + Math.Pow((searcherStation1.y - source.y), 2) + Math.Pow((searcherStation1.z - source.z), 2))
                                - Math.Sqrt(Math.Pow((searcherStation2.x - source.x), 2) + Math.Pow((searcherStation2.y - source.y), 2) + Math.Pow((searcherStation2.z - source.z), 2)) - dt12, 2);
            }

            var newSource = new RadioStation { coordinates = new[] { 15, 25, 30 } };


            void HookJeeves(int delta, int minDelta, int denominator, F f)
            {
                var tmpSource = new RadioStation();
                tmpSource.coordinates = new int[3];
                var  k = f(trueSource);

                Array.Copy(newSource.coordinates, tmpSource.coordinates, 3);

                while (delta >= minDelta)
                {
                    CheckNeighbourPoints(delta, f);
                    if (tmpSource == newSource)
                        delta /= denominator;
                    else
                    {
                        var f1 = f(newSource);
                        newSource.x = 2 * newSource.x - tmpSource.x;
                        newSource.y = 2 * newSource.y - tmpSource.y;
                        newSource.z = 2 * newSource.z - tmpSource.z;
                        var f2 = f(newSource);
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

            void CheckNeighbourPoints(int delta, F f)
            {
                double tmpF1 = f(newSource);
                var tmpF2 = tmpF1;
               for(int i= 0;i< 3; i++)
                {
                    newSource.coordinates[i] += delta;
                    tmpF2 = f(newSource);
                    if (tmpF1 > tmpF2)
                        tmpF1 = tmpF2;
                    else
                    {
                        newSource.coordinates[i] -= 2 * delta;
                        tmpF2 = f(newSource);
                        if (tmpF1 > tmpF2)
                            tmpF1 = tmpF2;
                        else
                            newSource.coordinates[i] += delta;
                    }

                }
            }

            HookJeeves(4, 1, 2, F2);
        }
    }
}
