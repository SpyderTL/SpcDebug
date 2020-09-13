using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SpcDebug
{
	public static class Spc
	{
		public struct OpCode
		{
			public string Name;
			public InstructionType Type;
		}

		public enum InstructionType
		{
			Stack,
			Direct,
			DirectPlusXIndex,
			DirectPlusYIndex,
			Indirect,
			IndirectIncrement,
			DirectToDirect,
			IndirectToIndirect,
			Immediate,
			ImmediateToDirect,
			Relative,
			DirectBitwise,
			DirectBitwiseRelative,
			AbsoluteBitwise,
			Absolute,
			AbsoluteXIndex,
			AbsoluteYIndex,
			DirectPlusXIndexPointer,
			DirectPointerPlusYIndex,
		}

		public static OpCode[] OpCodes = new OpCode[]
		{
			new OpCode { Name = "NoOperation", Type = InstructionType.Stack },
			new OpCode { Name = "CallSystemFunction0", Type = InstructionType.Stack },															// 01
			new OpCode { Name = "SetImmediate8AddressBit0", Type = InstructionType.Immediate },											// 02
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit0Set", Type = InstructionType.ImmediateToDirect },	// 03
			new OpCode { Name = "OrAWithImmediate8Address", Type = InstructionType.Immediate },		// 04
			new OpCode { Name = "OrAWithImmediate16Address", Type = InstructionType.Absolute },		// 05
			new OpCode { Name = "OrAWithXAddress", Type = InstructionType.Absolute },		// 06
			new OpCode { Name = "OrAWithImmediate8PlusXPointer", Type = InstructionType.Immediate },		// 07
			new OpCode { Name = "OrAWithImmediate8", Type = InstructionType.Immediate },		// 08
			new OpCode { Name = "OrImmediate8AddressWithImmediate8Address", Type = InstructionType.Absolute },		// 09
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "ShiftImmediate8AddressLeft", Type = InstructionType.Immediate },		// 0b
			new OpCode { Name = "ShiftImmediate16AddressLeft", Type = InstructionType.Absolute },		// 0c
			new OpCode { Name = "PushFlagsToStack", Type = InstructionType.Stack },																// 0d
			new OpCode { Name = "TestAndSetBits", Type = InstructionType.Absolute },																// 0e
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "BranchToRelative8IfNotNegative", Type = InstructionType.Relative },		// 10
			new OpCode { Name = "CallSystemFunction1", Type = InstructionType.Stack },															// 11
			new OpCode { Name = "ClearImmediate8AddressBit0", Type = InstructionType.Immediate },										// 12
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit0Clear", Type = InstructionType.ImmediateToDirect },	// 13
			new OpCode { Name = "OrAWithImmediate8PlusXAddress", Type = InstructionType.Immediate },		// 14
			new OpCode { Name = "OrAWithImmediate16PlusXAddress", Type = InstructionType.Absolute },		// 15
			new OpCode { Name = "OrAWithImmediate16PlusYAddress", Type = InstructionType.Absolute },		// 16
			new OpCode { Name = "OrAWithImmediate8PointerPlusY", Type = InstructionType.Immediate },		// 17
			new OpCode { Name = "OrImmediate8AddressWithImmediate8", Type = InstructionType.ImmediateToDirect },		// 18
			new OpCode { Name = "OrXAddressWithYAddress", Type = InstructionType.Absolute },		// 19
			new OpCode { Name = "DecrementImmediate8Address16", Type = InstructionType.Direct },		// 1a
			new OpCode { Name = "ShiftImmediate8PlusXAddressLeft", Type = InstructionType.DirectPlusXIndex },		// 1b
			new OpCode { Name = "ShiftALeft", Type = InstructionType.Stack },		// 1c
			new OpCode { Name = "DecrementX", Type = InstructionType.Stack },		// 1d
			new OpCode { Name = "CompareXToImmediate16Address", Type = InstructionType.Absolute },		// 1E
			new OpCode { Name = "JumpToImmediate16Pointer", Type = InstructionType.Absolute },		// 1f
			new OpCode { Name = "ClearDirectPage", Type = InstructionType.Stack },
			new OpCode { Name = "CallSystemFunction2", Type = InstructionType.Stack },															// 21
			new OpCode { Name = "SetImmediate8AddressBit1", Type = InstructionType.Direct },											// 22
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit1Set", Type = InstructionType.ImmediateToDirect },			// 23
			new OpCode { Name = "AndAWithImmediate8Pointer", Type = InstructionType.Direct },		// 24
			new OpCode { Name = "AndAWithImmediate16Address", Type = InstructionType.Absolute },		// 25
			new OpCode { Name = "AndAWithXAddress", Type = InstructionType.Absolute },		// 26
			new OpCode { Name = "AndAWithImmediate8PlusXPointer", Type = InstructionType.Direct },		// 27
			new OpCode { Name = "AndAWithImmediate8", Type = InstructionType.Immediate },		// 28
			new OpCode { Name = "AndImmediate8AddressWithImmediate8Address", Type = InstructionType.ImmediateToDirect },		// 29
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "RollImmediate8AddressLeft", Type = InstructionType.Direct },		// 2b
			new OpCode { Name = "RollImmediate16AddressLeft", Type = InstructionType.Absolute },		// 2c
			new OpCode { Name = "PushAToStack", Type = InstructionType.Stack },																			// 2d
			new OpCode { Name = "BranchIfANotEqualImmediate8Address", Type = InstructionType.Direct },		// 2e
			new OpCode { Name = "JumpToRelative8", Type = InstructionType.Relative },		// 2f
			new OpCode { Name = "BranchToRelative8IfNegative", Type = InstructionType.Relative },		// 30
			new OpCode { Name = "CallSystemFunction3", Type = InstructionType.Stack },															// 31
			new OpCode { Name = "ClearImmediate8AddressBit1", Type = InstructionType.Direct },										// 32
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit1Clear", Type = InstructionType.ImmediateToDirect },			// 33
			new OpCode { Name = "AndAWithImmediate8PlusXPointer", Type = InstructionType.Absolute },		// 34
			new OpCode { Name = "AndAWithImmediate16PlusXAddress", Type = InstructionType.Absolute },		// 35
			new OpCode { Name = "AndAWithImmediate16PlusYAddress", Type = InstructionType.Absolute },		// 36
			new OpCode { Name = "AndAWithImmediate8PointerPlusY", Type = InstructionType.Absolute },		// 37
			new OpCode { Name = "AndImmediate8AddressWithImmediate8", Type = InstructionType.ImmediateToDirect },		// 38
			new OpCode { Name = "AndXAddressWithYAddress", Type = InstructionType.Absolute },		// 39
			new OpCode { Name = "IncrementImmediate8Address16", Type = InstructionType.Direct },		// 3a
			new OpCode { Name = "RollImmediate8PlusXAddressLeft", Type = InstructionType.Absolute },		// 3b
			new OpCode { Name = "RollALeft", Type = InstructionType.Absolute },		// 3c
			new OpCode { Name = "IncrementX", Type = InstructionType.Stack },		// 3d
			new OpCode { Name = "CompareXToImmediate8Address", Type = InstructionType.Direct },		// 3E
			new OpCode { Name = "CallImmediate16", Type = InstructionType.Absolute },
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "CallSystemFunction4", Type = InstructionType.Stack },															// 41
			new OpCode { Name = "SetImmediate8AddressBit2", Type = InstructionType.Direct },											// 42
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit7Set", Type = InstructionType.ImmediateToDirect },			// 43
			new OpCode { Name = "ExclusiveOrAWithImmediate8Address", Type = InstructionType.Direct },		// 44
			new OpCode { Name = "ExclusiveOrAWithImmediate16Address", Type = InstructionType.Absolute },		// 45
			new OpCode { Name = "ExclusiveOrAWithXAddress", Type = InstructionType.Absolute },		// 46
			new OpCode { Name = "ExclusiveOrAWithImmediate8PlusXPointer", Type = InstructionType.Absolute },		// 47
			new OpCode { Name = "ExclusiveOrAWithImmediate8", Type = InstructionType.Immediate },		// 48
			new OpCode { Name = "ExclusiveOrImmediate8AddressWithImmediate8Address", Type = InstructionType.DirectToDirect },		// 49
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "ShiftImmediate8AddressRight", Type = InstructionType.Direct },		// 4b
			new OpCode { Name = "ShiftImmediate16AddressRight", Type = InstructionType.Absolute },		// 4c
			new OpCode { Name = "PushXToStack", Type = InstructionType.Stack },																			// 4d
			new OpCode { Name = "TestAndClearBits", Type = InstructionType.Absolute },																	// 4e
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "BranchToRelative8IfNotOverflow", Type = InstructionType.Relative },		// 50
			new OpCode { Name = "CallSystemFunction5", Type = InstructionType.Stack },															// 51
			new OpCode { Name = "ClearImmediate8AddressBit2", Type = InstructionType.Direct },										// 52
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit2Clear", Type = InstructionType.ImmediateToDirect },			// 53
			new OpCode { Name = "ExclusiveOrAWithImmediate8PlusXAddress", Type = InstructionType.Absolute },		// 54
			new OpCode { Name = "ExclusiveOrAWithImmediate16PlusXAddress", Type = InstructionType.Absolute },		// 55
			new OpCode { Name = "ExclusiveOrAWithImmediate16PlusYAddress", Type = InstructionType.Absolute },		// 56
			new OpCode { Name = "ExclusiveOrAWithImmediate8PointerPlusY", Type = InstructionType.Absolute },		// 57
			new OpCode { Name = "ExclusiveOrImmediate8AddressWithImmediate8", Type = InstructionType.ImmediateToDirect },		// 58
			new OpCode { Name = "ExclusiveOrXAddressWithYAddress", Type = InstructionType.Absolute },		// 59
			new OpCode { Name = "CompareYAToImmediate8Address", Type = InstructionType.Direct },		// 5a
			new OpCode { Name = "ShiftImmediate8PlusXAddressRight", Type = InstructionType.DirectPlusXIndex },		// 5b
			new OpCode { Name = "ShiftARight", Type = InstructionType.Stack },		// 5c
			new OpCode { Name = "CopyAToX", Type = InstructionType.Stack },		// 5d
			new OpCode { Name = "CompareYToImmediate16Address", Type = InstructionType.Absolute },		// 5E
			new OpCode { Name = "JumpToAbsolute16", Type = InstructionType.Absolute },		// 5f
			new OpCode { Name = "ClearCarryFlag", Type = InstructionType.Stack },
			new OpCode { Name = "CallSystemFunction6", Type = InstructionType.Stack },															// 61
			new OpCode { Name = "SetImmediate8AddressBit3", Type = InstructionType.Direct },											// 62
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit3Set", Type = InstructionType.ImmediateToDirect },			// 63
			new OpCode { Name = "CompareAToImmediate8Address", Type = InstructionType.Direct },		// 64
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "CompareAToXAddress", Type = InstructionType.Absolute },		// 66
			new OpCode { Name = "CompareAToImmediate8PlusXPointer", Type = InstructionType.Absolute },		// 67
			new OpCode { Name = "CompareAToImmediate8", Type = InstructionType.Immediate },		// 68
			new OpCode { Name = "CompareImmediate8AddressToImmediate8Address", Type = InstructionType.DirectToDirect },		// 69
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "RollImmediate8AddressRight", Type = InstructionType.Direct },		// 6b
			new OpCode { Name = "RollImmediate16AddressRight", Type = InstructionType.Absolute },		// 6c
			new OpCode { Name = "PushYToStack", Type = InstructionType.Stack },																								// 6d
			new OpCode { Name = "DecrementImmdiate8AddressAndBranchToRelative8IfNotZero", Type = InstructionType.ImmediateToDirect },		// 6e
			new OpCode { Name = "ReturnFromSubroutine", Type = InstructionType.Stack },																					// 6f
			new OpCode { Name = "BranchToRelative8IfOverflow", Type = InstructionType.Relative },		// 70
			new OpCode { Name = "CallSystemFunction7", Type = InstructionType.Stack },														// 71
			new OpCode { Name = "ClearImmediate8AddressBit3", Type = InstructionType.Direct },										// 72
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit3Clear", Type = InstructionType.ImmediateToDirect },			// 73
			new OpCode { Name = "CompareAToImmediate8PlusXAddress", Type = InstructionType.Absolute },		// 74
			new OpCode { Name = "CompareAToImmediate16PlusXAddress", Type = InstructionType.Absolute },		// 75
			new OpCode { Name = "CompareAToImmediate16PlusYAddress", Type = InstructionType.Absolute },		// 76
			new OpCode { Name = "CompareAToImmediate8PointerPlusY", Type = InstructionType.Absolute },		// 77
			new OpCode { Name = "CompareImmediate8AddressToImmediate8", Type = InstructionType.ImmediateToDirect },		// 78
			new OpCode { Name = "CompareXAddressToYAddress", Type = InstructionType.Absolute },		// 79
			new OpCode { Name = "AddImmediate8AddressToYA", Type = InstructionType.Direct },		// 7a
			new OpCode { Name = "RollImmediate8PlusXAddressRight", Type = InstructionType.Absolute },		// 7b
			new OpCode { Name = "RollARight", Type = InstructionType.Stack },		// 7c
			new OpCode { Name = "CopyXToA", Type = InstructionType.Stack },		// 7d
			new OpCode { Name = "CompareYToImmediate8Address", Type = InstructionType.Direct },		// 7E
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "SetCarryFlag", Type = InstructionType.Stack },																		// 80
			new OpCode { Name = "CallSystemFunction8", Type = InstructionType.Stack },															// 81
			new OpCode { Name = "SetImmediate8AddressBit4", Type = InstructionType.Direct },											// 82
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit4Set", Type = InstructionType.ImmediateToDirect },			// 83
			new OpCode { Name = "AddImmediate8AddressPlusCarryToA", Type = InstructionType.Direct },		// 84
			new OpCode { Name = "AddImmediate16AddressPlusCarryToA", Type = InstructionType.Absolute },		// 85
			new OpCode { Name = "AddXAddressPlusCarryToA", Type = InstructionType.Absolute },		// 86
			new OpCode { Name = "AddImmediate8PlusXPointerPlusCarryToA", Type = InstructionType.Absolute },		// 87
			new OpCode { Name = "AddImmediate8PlusCarryToA", Type = InstructionType.Absolute },		// 88
			new OpCode { Name = "AddImmediate8AddressPlusCarryToImmediate8Address", Type = InstructionType.DirectToDirect },		// 89
			new OpCode { Name = "ExclusiveOrCarryFlagWithImmediate8AddressBit", Type = InstructionType.Absolute },							// 8a
			new OpCode { Name = "DecrementImmediate8Address", Type = InstructionType.Direct },														// 8b
			new OpCode { Name = "DecrementImmediate16Address", Type = InstructionType.Absolute },												// 8c
			new OpCode { Name = "CopyImmediate8ToY", Type = InstructionType.Immediate },																// 8d
			new OpCode { Name = "PullFlagsFromStack", Type = InstructionType.Stack },																		// 8e
			new OpCode { Name = "CopyImmediate8ToImmediate8Address", Type = InstructionType.ImmediateToDirect },						// 8f
			new OpCode { Name = "BranchToRelative8IfNotCarry", Type = InstructionType.Relative },														// 90
			new OpCode { Name = "CallSystemFunction9", Type = InstructionType.Stack },																		// 91
			new OpCode { Name = "ClearImmediate8AddressBit4", Type = InstructionType.Direct },														// 92
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit4Clear", Type = InstructionType.ImmediateToDirect },			// 93
			new OpCode { Name = "AddImmediate16PlusXAddressPlusCarryToA", Type = InstructionType.Absolute },		// 94
			new OpCode { Name = "AddImmediate16PlusXAddressPlusCarryToA", Type = InstructionType.Absolute },		// 95
			new OpCode { Name = "AddImmediate16PlusYAddressPlusCarryToA", Type = InstructionType.Absolute },		// 96
			new OpCode { Name = "AddImmediate8PointerPlusYPlusCarryToA", Type = InstructionType.Absolute },		// 97
			new OpCode { Name = "AddImmediate8PlusCarryToImmediate8Address", Type = InstructionType.ImmediateToDirect },		// 98
			new OpCode { Name = "AddYAddressPlusCarryToXAddress", Type = InstructionType.Absolute },		// 99
			new OpCode { Name = "AddYAToImmediate8Address", Type = InstructionType.Direct },		// 9a
			new OpCode { Name = "DecrementImmediate8PlusXAddress", Type = InstructionType.Immediate },		// 9b
			new OpCode { Name = "DecrementA", Type = InstructionType.Stack },		// 9c
			new OpCode { Name = "CopySPToX", Type = InstructionType.Absolute },		// 9d
			new OpCode { Name = "DivideYAByX", Type = InstructionType.Absolute },		// 9e
			new OpCode { Name = "ExchangeANibbles", Type = InstructionType.Absolute },		// 9f
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "CallSystemFunction10", Type = InstructionType.Stack },													// A1
			new OpCode { Name = "SetImmediate8AddressBit5", Type = InstructionType.Direct },											// A2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit5Set", Type = InstructionType.ImmediateToDirect },			// a3
			new OpCode { Name = "SubtractImmediate8AddressPlusBorrowFromA", Type = InstructionType.Direct },		// A4
			new OpCode { Name = "SubtractImmediate16AddressPlusBorrowFromA", Type = InstructionType.Absolute },		// A5
			new OpCode { Name = "SubtractXAddressPlusBorrowFromA", Type = InstructionType.Absolute },		// A6
			new OpCode { Name = "SubtractImmediate8PlusXPointerPlusBorrowFromA", Type = InstructionType.Absolute },		// A7
			new OpCode { Name = "SubtractImmediate8PlusBorrowFromA", Type = InstructionType.Absolute },		// A8
			new OpCode { Name = "SubtractImmediate8AddressPlusBorrowFromImmediate8Address", Type = InstructionType.DirectToDirect },		// A9
			new OpCode { Name = "CopyImmediate8AddressBitToCarryFlag", Type = InstructionType.Absolute },														// AA
			new OpCode { Name = "IncrementImmediate8Address", Type = InstructionType.Direct },		// ab
			new OpCode { Name = "IncrementImmediate16Address", Type = InstructionType.Absolute },		// ac
			new OpCode { Name = "CompareYToImmediate8", Type = InstructionType.Immediate },		// AD
			new OpCode { Name = "PullAFromStack", Type = InstructionType.Stack },									// ae
			new OpCode { Name = "CopyAToXAddressAndIncrementX", Type = InstructionType.Stack },		// af
			new OpCode { Name = "BranchToRelative8IfCarry", Type = InstructionType.Relative },		// b0
			new OpCode { Name = "CallSystemFunction11", Type = InstructionType.Stack },													// B1
			new OpCode { Name = "ClearImmediate8AddressBit5", Type = InstructionType.Direct },										// B2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit5Clear", Type = InstructionType.ImmediateToDirect },	// B3
			new OpCode { Name = "SubtractImmediate8PlusXAddressPlusBorrowFromA", Type = InstructionType.Absolute },		// B4
			new OpCode { Name = "SubtractImmediate16PlusXAddressPlusBorrowFromA", Type = InstructionType.Absolute },		// B5
			new OpCode { Name = "SubtractImmediate16PlusYAddressPlusBorrowFromA", Type = InstructionType.Absolute },		// B6
			new OpCode { Name = "SubtractImmediate8PointerPlusYPlusBorrowFromA", Type = InstructionType.Absolute },		// B7
			new OpCode { Name = "SubtractImmediate8PlusBorrowFromImmediate8Address", Type = InstructionType.ImmediateToDirect },		// B8
			new OpCode { Name = "SubtractYAddressPlusBorrowFromXAddress", Type = InstructionType.Absolute },		// B9
			new OpCode { Name = "CopyImmediate8AddressToYA", Type = InstructionType.Direct },		// ba
			new OpCode { Name = "IncrementImmediate8PlusXAddress", Type = InstructionType.DirectPlusXIndex },		// bb
			new OpCode { Name = "IncrementA", Type = InstructionType.Stack },		// bc
			new OpCode { Name = "CopyXToSP", Type = InstructionType.Stack },		// bd
			new OpCode { Name = "DecimalAdjustAForSubtraction", Type = InstructionType.Absolute },		// be
			new OpCode { Name = "CopyXAddressToAAndIncrementX", Type = InstructionType.Stack },		// bf
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "SetImmediate8AddressBit6", Type = InstructionType.Direct },											// C2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit6Set", Type = InstructionType.ImmediateToDirect },			// c3
			new OpCode { Name = "CopyAToImmediate8Address", Type = InstructionType.Direct },		// c4
			new OpCode { Name = "CopyAToImmediate16Address", Type = InstructionType.Absolute },		// c5
			new OpCode { Name = "CopyAToXAddress", Type = InstructionType.Stack },		// c6
			new OpCode { Name = "CopyAToImmediate8PlusXPointer", Type = InstructionType.Absolute },			// c7
			new OpCode { Name = "CompareXToImmediate8", Type = InstructionType.Immediate },					// C8
			new OpCode { Name = "CopyXToImmediate16Address", Type = InstructionType.Absolute },				// c9
			new OpCode { Name = "CopyCarryFlagToImmediate8AddressBit", Type = InstructionType.Absolute },	// ca
			new OpCode { Name = "CopyYToImmediate8Address", Type = InstructionType.Direct },					// cb
			new OpCode { Name = "CopyYToImmediate16Address", Type = InstructionType.Absolute },				// cc
			new OpCode { Name = "CopyImmediate8ToX", Type = InstructionType.Immediate },							// cd
			new OpCode { Name = "PullXFromStack", Type = InstructionType.Stack },											// ce
			new OpCode { Name = "MultiplyAByY", Type = InstructionType.Stack },		// cf
			new OpCode { Name = "BranchToRelative8IfNotEqual", Type = InstructionType.Relative },		// d0
			//new OpCode { Name = "BranchToRelative8IfNotZero", Type = InstructionType.Absolute },		// d0
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "ClearImmediate8AddressBit6", Type = InstructionType.Direct },										// d2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit6Clear", Type = InstructionType.ImmediateToDirect },			// d3
			new OpCode { Name = "CopyAToImmediate8PlusXAddress", Type = InstructionType.Immediate },		// d4
			new OpCode { Name = "CopyAToImmediate16PlusXAddress", Type = InstructionType.Absolute },		// d5
			new OpCode { Name = "CopyAToImmediate16PlusYAddress", Type = InstructionType.Absolute },		// d6
			new OpCode { Name = "CopyAToImmediate8PointerPlusY", Type = InstructionType.Immediate },		// d7
			new OpCode { Name = "CopyXToImmediate8Address", Type = InstructionType.Direct },		// d8
			new OpCode { Name = "CopyXToImmediate8PlusYAddress", Type = InstructionType.Immediate },		// d9
			new OpCode { Name = "CopyYAToImmediate8Address", Type = InstructionType.Direct },		// da
			new OpCode { Name = "CopyYToImmediate8PlusXAddress", Type = InstructionType.Immediate },		// db
			new OpCode { Name = "DecrementY", Type = InstructionType.Stack },		// dc
			new OpCode { Name = "CopyYToA", Type = InstructionType.Stack },		// dd
			new OpCode { Name = "BranchIfANotEqualImmediate8PlusXAddress", Type = InstructionType.Immediate },		// de
			new OpCode { Name = "DecimalAdjustAForAddition", Type = InstructionType.Absolute },		// df
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "SetImmediate8AddressBit7", Type = InstructionType.Direct },											// e2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit7Set", Type = InstructionType.ImmediateToDirect },			// e3
			new OpCode { Name = "CopyImmediate8AddressToA", Type = InstructionType.Direct },		// e4
			new OpCode { Name = "CopyImmediate16AddressToA", Type = InstructionType.Absolute },		// e5
			new OpCode { Name = "CopyXAddressToA", Type = InstructionType.Stack },		// e6
			new OpCode { Name = "CopyImmediate8PlusXPointerToA", Type = InstructionType.Immediate },		// e7
			new OpCode { Name = "CopyImmediate8ToA", Type = InstructionType.Immediate },		// e8
			new OpCode { Name = "CopyImmediate16AddressToX", Type = InstructionType.Absolute },		// e9
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "CopyImmediate8AddressToY", Type = InstructionType.Direct },		// eb
			new OpCode { Name = "CopyImmediate16AddressToY", Type = InstructionType.Absolute },		// ec
			new OpCode { Name = "ToggleCarryFlag", Type = InstructionType.Stack },										// ed
			new OpCode { Name = "PullYFromStack", Type = InstructionType.Stack },											// ee
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "BranchToRelative8IfEqual", Type = InstructionType.Relative },		// f0
			//new OpCode { Name = "BranchToRelative8IfZero", Type = InstructionType.Absolute },		// f0
			new OpCode { Name = "", Type = InstructionType.Absolute },
			new OpCode { Name = "ClearImmediate8AddressBit7", Type = InstructionType.Direct },										// f2
			new OpCode { Name = "BranchToRelative8IfImmediate8AddressBit7Clear", Type = InstructionType.ImmediateToDirect },			// f3
			new OpCode { Name = "CopyImmediate8PlusXAddressToA", Type = InstructionType.Immediate },		// f4
			new OpCode { Name = "CopyImmediate16PlusXAddressToA", Type = InstructionType.Absolute },		// f5
			new OpCode { Name = "CopyImmediate16PlusYAddressToA", Type = InstructionType.Absolute },		// f6
			new OpCode { Name = "CopyImmediate8PointerPlusYToA", Type = InstructionType.Immediate },		// f7
			new OpCode { Name = "CopyImmediate8AddressToX", Type = InstructionType.Direct },		// f8
			new OpCode { Name = "CopyImmediate8PlusYAddressToX", Type = InstructionType.Immediate },		// f9
			new OpCode { Name = "CopyImmediate8AddressToImmediate8Address", Type = InstructionType.DirectToDirect },		// fa
			new OpCode { Name = "CopyImmediate8PlusXAddressToY", Type = InstructionType.Immediate },		// fb
			new OpCode { Name = "IncrementY", Type = InstructionType.Stack },		// fc
			new OpCode { Name = "CopyAToY", Type = InstructionType.Stack },		// fd
			new OpCode { Name = "DecrementYAndBranchToRelative8IfNotZero", Type = InstructionType.Relative },		// fe
			new OpCode { Name = "", Type = InstructionType.Absolute },
		};
	}
}
