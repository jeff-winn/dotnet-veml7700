namespace WebApp.Infrastructure.Primitives {
    public interface II2cBus {
        II2cDevice CreateDevice(int deviceAddress);
    }    
}