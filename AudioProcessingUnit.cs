namespace SpcDebug
{
	public class AudioProcessingUnit : ComputerSystem
	{
		public SpcProcesesor Processor = new SpcProcesesor();
		public Ram Ram = new Ram { Data = new byte[64 * 1024] };
		public Clock Clock = new Clock { Frequency = 1.0 };
		public Bus Bus = new Bus();

		public AudioProcessingUnit()
		{
			Devices.Add(Processor);
			Clocks.Add(Clock);
			Busses.Add(Bus);

			BusDevices.Add(new BusDevice { Bus = Bus, Device = Processor });
			ClockDevices.Add(new ClockDevice { Clock = Clock, Device = Processor });
		}
	}
}