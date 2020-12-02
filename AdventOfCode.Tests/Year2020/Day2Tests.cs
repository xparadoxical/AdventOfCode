using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day2Tests
	{
		public const string INPUT = "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc";

		[DataTestMethod]
		[DataRow(INPUT, 2)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day2(input).Part1());
		}

		[DataTestMethod]
		[DataRow(INPUT, 1)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day2(input).Part2());
		}
	}
}
