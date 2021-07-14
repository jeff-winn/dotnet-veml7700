using System.Device.Gpio;

namespace WebApp.Infrastructure.Primitives {
	public class GpioControllerWrapper : IGpioController {
		private readonly GpioController wrappedController;
		
		public GpioControllerWrapper(GpioController wrappedController){
			this.wrappedController = wrappedController;
		}
	}
}