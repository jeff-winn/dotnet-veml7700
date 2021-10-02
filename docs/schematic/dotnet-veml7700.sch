EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 1
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
Text GLabel 5650 2200 3    50   Input ~ 0
3V3
$Comp
L dotnet-veml7700:Adafruit_VEML7700 U1
U 1 1 6158B1BC
P 6050 1600
F 0 "U1" H 6628 1563 50  0000 L CNN
F 1 "Adafruit VEML7700 Lux Sensor" H 6628 1472 50  0000 L CNN
F 2 "" H 6000 1600 50  0001 C CNN
F 3 "" H 6000 1600 50  0001 C CNN
	1    6050 1600
	1    0    0    -1  
$EndComp
Text GLabel 6050 2200 3    50   UnSpc ~ 0
GND
Text GLabel 6250 2200 3    50   Input ~ 0
SCL
Text GLabel 6450 2200 3    50   Input ~ 0
SDA
NoConn ~ 5850 2200
$Comp
L Connector:Raspberry_Pi_2_3 J1
U 1 1 6158D9AE
P 2900 3650
F 0 "J1" H 2900 5350 50  0000 C CNN
F 1 "Raspberry Pi 3A+" H 2900 5250 50  0000 C CNN
F 2 "" H 2900 3650 50  0001 C CNN
F 3 "https://www.raspberrypi.org/documentation/hardware/raspberrypi/schematics/rpi_SCH_3bplus_1p0_reduced.pdf" H 2900 3650 50  0001 C CNN
	1    2900 3650
	1    0    0    -1  
$EndComp
Text GLabel 3000 2350 1    50   Output ~ 0
3V3
Text GLabel 3700 3050 2    50   Output ~ 0
SDA
Text GLabel 3700 3150 2    50   Output ~ 0
SCL
Text GLabel 2500 4950 3    50   UnSpc ~ 0
GND
$EndSCHEMATC
