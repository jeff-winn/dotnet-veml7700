# dotnet-veml7700
ASP.NET Core Web API to expose the sensor values from an [Adafruit VEML7700 Lux Sensor](https://www.adafruit.com/product/4162).

## Parts
These are not affiliate links, they're only here so I know what parts I used to build the device.
- [Raspberry Pi 3 Model A+](https://www.raspberrypi.com/products/raspberry-pi-3-model-a-plus/)
- [Adafruit VEML7700 Lux Sensor](https://www.adafruit.com/product/4162)
- [Breadboard Jumper Cables 2x4P 60CM Female to Female Dupont Cable](https://www.amazon.com/dp/B08NVKVSSP)
- [Room Sensor Enclosure - Size 2](https://thepihut.com/products/room-sensor-enclosure-size-2-with-pi-3a-mounts) - NOTE: This did require modification to get the device to fit as noted on the enclosure.
- [CanaKit 5V 2.5A Raspberry Pi 3 Power Supply](https://www.amazon.com/dp/B00MARDJZ4)

## Installation instructions
These commands would be ran from the user workstation which has downloaded the Debian package, and then SSH into the Raspberry Pi to initiate installation.
```
scp dotnet-veml7700_<VERSION>_armhf.deb pi@raspberrypi:/home/pi
ssh pi@raspberrypi
sudo dpkg -i dotnet_veml7700_<VERSION>_armhf.deb
```
After installation Swagger specification will be available at: http://raspberrypi:8080/swagger

NOTE: If you wish to change the default port used for the server, you will need to change the ASPNETCORE_URLS environment variable setting held within the _/etc/systemd/system/dotnet-veml7700.service_ file on the device.
```
sudo nano /etc/systemd/system/dotnet-veml7700.service
sudo systemctl daemon-reload
```
