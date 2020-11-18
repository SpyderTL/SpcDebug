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

				case Spc.InstructionType.AbsolutePlusX:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.AbsolutePlusY:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.Direct:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.Relative:
					Value = (sbyte)Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectBitwise:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectBitwiseRelative:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.DirectBitwiseImmediate:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.DirectPlusXPointer:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectPointerPlusY:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectToDirect:
					Value = Stream.ReadByte() | (Stream.ReadByte() << 8);
					break;

				case Spc.InstructionType.DirectPlusX:
					Value = Stream.ReadByte();
					break;

				case Spc.InstructionType.DirectPlusY:
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
