using System.Collections.Generic;

namespace SpcDebug
{
	public class ComputerSystem
	{
		public List<Bus> Busses = new List<Bus>();
		public List<Clock> Clocks = new List<Clock>();
		public List<Device> Devices = new List<Device>();

		public List<BusDevice> BusDevices = new List<BusDevice>();
		public List<ClockDevice> ClockDevices = new List<ClockDevice>();
	}
}