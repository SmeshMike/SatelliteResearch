using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace ResearchModel
{
    public  class HeatMapDrawer
    {
        public struct HeatPoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public byte Intensity { get; set; }

            public HeatPoint(int iX, int iY, byte bIntensity)
            {
                X = iX;
                Y = iY;
                Intensity = bIntensity;
            }
        }
        private List<HeatPoint> heatPoints = new List<HeatPoint>();

        public Bitmap CreateIntensityMask(Bitmap bSurface, List<HeatPoint> aHeatPoints)
        {
            // Traverse heat point data and draw masks for each heat point
            var graphics = Graphics.FromImage(bSurface);
            foreach (HeatPoint DataPoint in aHeatPoints)
            {
                Color color = Color.FromArgb(255, 255, Convert.ToByte(255 - DataPoint.Intensity), Convert.ToByte(255 - DataPoint.Intensity));
                // Render current heat point on draw surface
                bSurface.SetPixel(DataPoint.X, DataPoint.Y, color);
            }
            return bSurface;
        }
    }
}
