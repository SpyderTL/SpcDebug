using System;
using System.IO;

namespace SpcDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			Arguments.Load(args);

			var apu = new AudioProcessingUnit();

			var data = File.ReadAllBytes(Arguments.Path);

			Array.Copy(data, 0, apu.Ram.Data, 0x400, data.Length);

			using var stream = new MemoryStream(apu.Ram.Data);
			var reader = new SpcReader { Stream = stream };

			stream.Position = 0x400;

			while (reader.Read() && stream.Position < 0x400 + data.Length)
			{
				switch(Spc.OpCodes[reader.OpCode].Type)
				{
					case Spc.InstructionType.DirectToDirect:
					case Spc.InstructionType.ImmediateToDirect:
						Console.WriteLine(reader.Offset.ToString("X4") + " " + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (reader.Value.Value & 0xff).ToString("X2") + ", " + ((reader.Value.Value >> 8) & 0xff).ToString("X2"));
						break;

					case Spc.InstructionType.Relative:
						Console.WriteLine(reader.Offset.ToString("X4") + " " + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (stream.Position + (sbyte)reader.Value.Value).ToString("X4"));
						break;

					default:
						Console.WriteLine(reader.Offset.ToString("X4") + " " + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + (reader.Value.HasValue ? " " + reader.Value.Value.ToString("X4") : string.Empty));
						break;
				}

				switch (reader.OpCode)
				{
					case 0x1F:
					case 0x2F:
					case 0x5F:
					case 0x6F:
					case 0x7F:
						Console.WriteLine();
						break;
				}
			}
		}
	}
}
