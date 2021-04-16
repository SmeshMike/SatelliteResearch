import rasterio
import sys

#path = 'D:\\VS Pojects\\SatelliteResearch\\1dot6gb.tif'
path = 'C:\\Repository\\SatelliteResearch\\1dot6gb.tif'
dataset = rasterio.open(path)
band = dataset.read(1)

string = input()
while string!='q':
    y,x  = string.split(' ')
    print(band[int(y), int(x)])
    string = input()
