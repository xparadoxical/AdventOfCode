using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day14Tests
	{
		[DataTestMethod]
		[DataRow(@"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0", 165ul)]
		public void Part1(string input, ulong expected)
		{
			Assert.AreEqual(expected, new Day14(input).Part1());
		}

		[DataTestMethod]
		[DataRow(@"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1", 208ul)]
		public void Part2(string input, ulong expected)
		{
			Assert.AreEqual(expected, new Day14(input).Part2());
		}
	}
}
