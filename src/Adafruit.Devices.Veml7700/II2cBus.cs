namespace System.Device.I2c
{
    public interface II2cBus {
        II2cDevice CreateDevice(int deviceAddress);
    }    
}