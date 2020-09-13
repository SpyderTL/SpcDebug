using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpcDebug
{
	public class SpcReader
	{
		public Stream Stream;
		public long Offset;
		public int OpCode;
		public int? Value;

		public bool Read()
		{
			Offset = Stream.Position;

			var value = Stream.ReadByte();

			if (value == -1)
				return false;

			OpCode = value;

			switch (Spc.OpCodes[OpCode].Type)
			{
				case Spc.InstructionType.Absolute:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.AbsoluteBitwise:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.AbsoluteXIndex:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.AbsoluteYIndex:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.Direct:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.Relative:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectBitwise:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectBitwiseRelative:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.DirectPlusXIndexPointer:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectPointerPlusYIndex:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectToDirect:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.DirectPlusXIndex:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectPlusYIndex:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.Immediate:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.ImmediateToDirect:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				default:
					Value = null;
					break;
			}

			return true;
		}
	}
}
