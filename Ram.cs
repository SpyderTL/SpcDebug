namespace SpcDebug
{
	public class Ram : Device
	{
		public byte[] Data;

		public void Tick()
		{
			var address = Bus.Bus.Address - Bus.BusOffset + Bus.DeviceOffset;

			if (address >= 0 &&
				address < Data.LongLength - Bus.Bus.Width)
			{
				if (Bus.Bus.Write)
				{
					Data[address] = (byte)(Bus.Bus.Data & 0xff);

					if(Bus.Bus.Width > 1)
						Data[address] = (byte)((Bus.Bus.Data >> 8) & 0xff);

					if (Bus.Bus.Width > 2)
						Data[address] = (byte)((Bus.Bus.Data >> 16) & 0xff);

					if (Bus.Bus.Width > 3)
						Data[address] = (byte)((Bus.Bus.Data >> 24) & 0xff);

					if (Bus.Bus.Width > 4)
						Data[address] = (byte)((Bus.Bus.Data >> 32) & 0xff);

					if (Bus.Bus.Width > 5)
						Data[address] = (byte)((Bus.Bus.Data >> 40) & 0xff);

					if (Bus.Bus.Width > 6)
						Data[address] = (byte)((Bus.Bus.Data >> 48) & 0xff);

					if (Bus.Bus.Width > 7)
						Data[address] = (byte)((Bus.Bus.Data >> 56) & 0xff);
				}
				else
				{
					Bus.Bus.Data = Data[address];

					if (Bus.Bus.Width > 1)
						Bus.Bus.Data |= (long)Data[address + 1] << 8;

					if (Bus.Bus.Width > 2)
						Bus.Bus.Data |= (long)Data[address + 2] << 16;

					if (Bus.Bus.Width > 3)
						Bus.Bus.Data |= (long)Data[address + 3] << 24;

					if (Bus.Bus.Width > 4)
						Bus.Bus.Data |= (long)Data[address + 4] << 32;

					if (Bus.Bus.Width > 5)
						Bus.Bus.Data |= (long)Data[address + 5] << 40;

					if (Bus.Bus.Width > 6)
						Bus.Bus.Data |= (long)Data[address + 6] << 48;

					if (Bus.Bus.Width > 7)
						Bus.Bus.Data |= (long)Data[address + 7] << 56;
				}
			}
		}
	}
}