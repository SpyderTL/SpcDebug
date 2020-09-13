using System.Collections.Generic;

namespace SpcDebug
{
	public class Bus
	{
		public List<BusDevice> Devices = new List<BusDevice>();

		public long Address;
		public long Data;
		public byte Width;
		public bool Write;
	}
}