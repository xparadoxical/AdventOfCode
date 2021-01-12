#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using RegExtract;

namespace AdventOfCode.Year2020
{
	public sealed class Day14
	{
		private readonly BitMaskProgram program;

		public Day14(string input)
		{
			program = new BitMaskProgram(input.Lines().Select(l =>
			{
				var (mask, address, value) = l.Extract<(string?, int?, ulong?)>(@"(?:mask = ([X01]{36}))|(?:mem\[(\d+)\] = (\d+))");

				return (mask is not null ? Instruction.UpdateMask : Instruction.MemoryWrite, (mask, address, value));
			}));
		}

		public ulong Part1()
		{
			program.Run();
			return program.Memory.Aggregate(0ul, (sum, kvp) => checked(sum + kvp.Value));
		}

		public ulong Part2()
		{
			program.RunV2();
			return program.Memory.Aggregate(0ul, (sum, kvp) => checked(sum + kvp.Value));
		}

		public sealed class BitMaskProgram
		{
			public List<(Instruction, (string? mask, int? address, ulong? value))> Instructions { get; private set; } = new();
			public Dictionary<ulong, ulong> Memory { get; private set; } = new();

			public BitMaskProgram(IEnumerable<(Instruction, (string? mask, int? address, ulong? value))> instructions)
			{
				foreach (var instr in instructions)
					Instructions.Add(instr);
			}

			public void Run()
			{
				BitMask currentBitmask = default;
				foreach (var (instruction, (mask, address, value)) in Instructions)
				{
					if (instruction is Instruction.UpdateMask)
					{
						currentBitmask = new BitMask(mask!);
					}
					else if (instruction is Instruction.MemoryWrite)
					{
						Memory[Convert.ToUInt64(address!.Value)] = currentBitmask.ApplyOn(value!.Value);
					}
					else throw new Exception($"Invalid instruction {instruction}, mask {mask}, address {address}, value {value}");
				}
			}

			public void RunV2()
			{
				BitMask currentBitmask = default;
				foreach (var (instruction, (mask, address, value)) in Instructions)
				{
					if (instruction is Instruction.UpdateMask)
					{
						currentBitmask = new BitMask(mask!);
					}
					else if (instruction is Instruction.MemoryWrite)
					{
						foreach (ulong addr in currentBitmask.FloatingApplyOn((ulong)address!.Value))
							Memory[addr] = value!.Value;
					}
					else throw new Exception($"Invalid instruction {instruction}, mask {mask}, address {address}, value {value}");
				}
			}
		}

		public enum Instruction
		{
			UpdateMask,
			MemoryWrite
		}

		public struct BitMask
		{
			private readonly bool?[] _bits;
			public bool?[] Bits
			{
				get
				{
					bool?[] copy = new bool?[36];
					_bits.CopyTo(copy, 0);
					return copy;
				}
			}

			public BitMask(string str)
			{
				if (str.Length != 36)
					throw new ArgumentException("The length of the string was not 36.", nameof(str));

				_bits = new bool?[36];
				for (int si = _bits.Length - 1, i = 0; si >= 0; si--, i++)
					_bits[i] = str[si] switch
					{
						'0' => false,
						'1' => true,
						'X' => null,
						char c => throw new Exception($"Invalid char {c} in {nameof(str)} at index {si}.")
					};
			}

			public ulong ApplyOn(ulong ul)
			{
				ul &= 0x0000_000F_FFFF_FFFF; //bits 0-35
				ulong bit = 1ul;

				for (int i = 0; i < _bits.Length; i++)
				{
					ul = _bits[i] switch
					{
						true => ul | bit,
						false => ul & ~bit,
						null => ul
					};
					bit <<= 1;
				}

				return ul;
			}

			public UInt64Floating FloatingApplyOn(ulong ul)
			{
				char[] fltBits = Convert.ToString((long)ul, 2)
									.PadLeft(_bits.Length, '0')
									.TakeLast(36)
									.Reverse().ToArray();

				for (int i = 0; i < fltBits.Length; i++)
				{
					fltBits[i] = _bits[i] switch
					{
						true => '1',
						false => fltBits[i],
						null => 'X'
					};
				}

				return new UInt64Floating(fltBits);
			}
		}

		public struct UInt64Floating : IEnumerable<ulong>
		{
			private readonly ulong @base;
			private readonly int[] floatingIndexes;

			public UInt64Floating(char[] bitChars)
			{
				List<int> floating = new();
				for (int i = 0; i < bitChars.Length; i++)
					if (bitChars[i] is 'X')
						floating.Add(i);

				floatingIndexes = floating.ToArray();
				@base = Convert.ToUInt64(new string(bitChars.Reverse().ToArray()).Replace('X', '0'), 2);
			}

			public IEnumerator<ulong> GetEnumerator()
			{
				if (floatingIndexes.Length is 0)
				{
					yield return @base;
					yield break;
				}

				ulong combinations = Convert.ToUInt64(Math.Pow(2, floatingIndexes.Length));
				for (ulong i = 0; i < combinations; i++)
				{
					ulong ret = @base;
					for (int f = 0; f < floatingIndexes.Length; f++)
					{
						int fi = floatingIndexes[f];
						if (((1ul << f) & i) >> f == 1)
						{
							ret |= 1ul << fi;
						}
					}

					yield return ret;
				}
			}
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
