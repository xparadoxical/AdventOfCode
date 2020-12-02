using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day1Tests
	{
		public const string INPUT = "1721\n979\n366\n299\n675\n1456";

		[TestMethod]
		[DataRow(INPUT, 514579)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day1(input).Part1());
		}

		[TestMethod]
		[DataRow(INPUT, 241861950)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day1(input).Part2());
		}
	}
}
