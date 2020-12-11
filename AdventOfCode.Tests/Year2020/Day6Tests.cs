using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day6Tests
	{
		public const string Input =
@"abc

a
b
c

ab
ac

a
a
a
a

b";

		[DataTestMethod]
		[DataRow(Input, 11)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day6(input).Part1());
		}

		[DataTestMethod]
		[DataRow(Input, 6)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day6(input).Part2());
		}
	}
}
