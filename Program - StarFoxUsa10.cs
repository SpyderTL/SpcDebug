using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpcDebug
{
	class ReadStarFoxUsa10
	{
		static void Main(string[] args)
		{
			var functions = new List<Function>();
			var branches = new Queue<long>();
			var labels = new List<long>();

			Arguments.Load(args);

			var apu = new AudioProcessingUnit();

			//var data = File.ReadAllBytes(Arguments.Path);

			var data = File.ReadAllBytes(@"C:\Users\joshu\source\Workspaces\StarFoxBrowser\StarFoxBrowser\Resources\StarFoxUsa10.bin");

			Array.Copy(data, 0x0c0004, apu.Ram.Data, 0x3ee8, 24);
			Array.Copy(data, 0x0c0020, apu.Ram.Data, 0x0400, 10239);
			Array.Copy(data, 0x0c2823, apu.Ram.Data, 0x3e20, 155);

			using var stream = new MemoryStream(apu.Ram.Data);
			var reader = new SpcReader { Stream = stream };

			branches.Enqueue(0x0400);
			labels.Add(0x0400);

			branches.Enqueue(0x09c9);
			labels.Add(0x09c9);

			branches.Enqueue(0x09d9);
			labels.Add(0x09d9);

			branches.Enqueue(0x09ff);
			labels.Add(0x09ff);

			branches.Enqueue(0x0a11);
			labels.Add(0x0a11);

			branches.Enqueue(0x0a14);
			labels.Add(0x0a14);

			branches.Enqueue(0x0a23);
			labels.Add(0x0a23);

			branches.Enqueue(0x0a2f);
			labels.Add(0x0a2f);

			branches.Enqueue(0x0a60);
			labels.Add(0x0a60);

			branches.Enqueue(0x0a69);
			labels.Add(0x0a69);

			branches.Enqueue(0x0a91);
			labels.Add(0x0a91);

			branches.Enqueue(0x09c9);
			labels.Add(0x09c9);

			branches.Enqueue(0x0a32);
			labels.Add(0x0a32);

			branches.Enqueue(0x0a36);
			labels.Add(0x0a36);

			branches.Enqueue(0x0a59);
			labels.Add(0x0a59);

			branches.Enqueue(0x0a82);
			labels.Add(0x0a82);

			branches.Enqueue(0x0ab4);
			labels.Add(0x0ab4);

			branches.Enqueue(0x0aeb);
			labels.Add(0x0aeb);

			branches.Enqueue(0x0af2);
			labels.Add(0x0af2);

			branches.Enqueue(0x0aca);
			labels.Add(0x0aca);

			branches.Enqueue(0x0b5d);
			labels.Add(0x0b5d);

			branches.Enqueue(0x0b5a);
			labels.Add(0x0b5a);

			while (branches.Any())
			{
				var offset = branches.Dequeue();

				if (!functions.Any(x => x.Entry <= offset && x.Exit >= offset))
				{
					stream.Position = offset;

					var function = Read(reader);

					functions.Add(function);

					foreach (var branch in function.Branches.OrderBy(x => x))
					{
						if (!functions.Any(y => y.Entry <= branch && y.Exit >= branch) &&
							!branches.Contains(branch))
							branches.Enqueue(branch);

						if (!labels.Contains(branch))
							labels.Add(branch);
					}
				}
			}

			functions = functions.OrderBy(x => x.Entry).ToList();

			foreach(var function in functions.OrderBy(x => x.Entry))
			{
				stream.Position = function.Entry;

				while ((stream.Position <= function.Exit) && reader.Read())
				{
					if (labels.Contains(reader.Offset))
					{
						Console.WriteLine();
						Console.WriteLine(reader.Offset.ToString("X4") + ":");
					}

					switch (Spc.OpCodes[reader.OpCode].Type)
					{
						case Spc.InstructionType.DirectToDirect:
						case Spc.InstructionType.ImmediateToDirect:
							Console.WriteLine(reader.Offset.ToString("X4") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (reader.Value.Value & 0xff).ToString("X2") + ", " + ((reader.Value.Value >> 8) & 0xff).ToString("X2"));
							//Console.WriteLine("\t" + Spc.OpCodes[reader.OpCode].Name + " " + (reader.Value.Value & 0xff).ToString("X2") + ", " + ((reader.Value.Value >> 8) & 0xff).ToString("X2"));
							//Console.WriteLine("\t" + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (reader.Value.Value & 0xff).ToString("X2") + ", " + ((reader.Value.Value >> 8) & 0xff).ToString("X2"));
							break;

						case Spc.InstructionType.Relative:
							Console.WriteLine(reader.Offset.ToString("X4") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (stream.Position + (sbyte)reader.Value.Value).ToString("X4"));
							//Console.WriteLine("\t" + Spc.OpCodes[reader.OpCode].Name + " " + (stream.Position + reader.Value.Value).ToString("X4"));
							//Console.WriteLine("\t" + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + " " + (stream.Position + (sbyte)reader.Value.Value).ToString("X4"));
							break;

						default:
							Console.WriteLine(reader.Offset.ToString("X4") + " " + Spc.OpCodes[reader.OpCode].Name + (reader.Value.HasValue ? " " + reader.Value.Value.ToString("X4") : string.Empty));
							//Console.WriteLine("\t" + Spc.OpCodes[reader.OpCode].Name + (reader.Value.HasValue ? " " + reader.Value.Value.ToString("X4") : string.Empty));
							//Console.WriteLine("\t" + reader.OpCode.ToString("X2") + " " + Spc.OpCodes[reader.OpCode].Name + (reader.Value.HasValue ? " " + reader.Value.Value.ToString("X4") : string.Empty));
							break;
					}
				}
			}
		}

		private static Function Read(SpcReader reader)
		{
			var entry = reader.Stream.Position;
			var exit = 0L;
			var branches = new List<long>();

			while ((exit == 0L) && reader.Read())
			{
				switch (reader.OpCode)
				{
					case 0x1F:
					case 0x6F:
					case 0x7F:
						exit = reader.Offset;
						break;

					case 0x2F:
						branches.Add(reader.Value.Value + reader.Stream.Position);
						exit = reader.Offset;
						break;

					case 0x5F:
						branches.Add(reader.Value.Value);
						exit = reader.Offset;
						break;

					case 0x3F:
						branches.Add(reader.Value.Value);
						break;

					case 0x10:
					case 0x30:
					case 0x50:
					case 0x6e:
					case 0x70:
					case 0x90:
					case 0xb0:
					case 0xd0:
					case 0xf0:
					case 0xfe:
						branches.Add(reader.Value.Value + reader.Stream.Position);
						break;

					case 0x03:
					case 0x13:
					case 0x23:
					case 0x33:
					case 0x43:
					case 0x53:
					case 0x63:
					case 0x73:
					case 0x83:
					case 0x93:
					case 0xa3:
					case 0xb3:
					case 0xc3:
					case 0xd3:
					case 0xe3:
					case 0xf3:
						//branches.Add((sbyte)(reader.Value.Value & 0xff) + reader.Stream.Position);
						branches.Add((sbyte)((reader.Value.Value >> 8) & 0xff) + reader.Stream.Position);

						break;
				}
			}

			return new Function
			{
				Entry = entry,
				Exit = exit,
				Branches = branches.ToArray()
			};
		}

		private class Function
		{
			public long Entry;
			public long Exit;
			public long[] Branches;
		}
	}
}
