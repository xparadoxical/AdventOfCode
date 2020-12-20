using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day11Tests
	{
		public const string Input =
@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

		[DataTestMethod]
		[DataRow(Input, 37)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day11(input).Part1());
		}

		[DataTestMethod]
		[DataRow(Input, 26)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day11(input).Part2());
		}
	}
}
