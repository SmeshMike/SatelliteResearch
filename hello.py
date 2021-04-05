import rasterio
import sys
import pathlib

path = 'D:\\VS Pojects\\SatelliteResearch\\1dot6gb.tif'
dataset = rasterio.open(path)
band = dataset.read(1)
x = sys.argv[1]
print(band[int(sys.argv[1]), int(sys.argv[2])])
