using System;

namespace WebApp.Exceptions {
    class DeviceNotFoundException : Exception {
        public int DeviceId { get; }

        public DeviceNotFoundException(int deviceId) 
            : base("The device does not exist.") {                
            DeviceId = deviceId;
        }

        public DeviceNotFoundException(int deviceId, Exception innerException) 
            : base("The device does not exist.", innerException) {        
            DeviceId = deviceId;
        }
    }
}