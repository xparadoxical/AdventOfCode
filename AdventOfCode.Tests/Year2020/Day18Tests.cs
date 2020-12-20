using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day18Tests
	{
		[DataTestMethod]
		[DataRow("1 + 2 * 3 + 4 * 5 + 6", 71)]
		[DataRow("1 + (2 * 3) + (4 * (5 + 6))", 51)]
		[DataRow("2 * 3 + (4 * 5)", 26)]
		[DataRow("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
		[DataRow("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
		[DataRow("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
		public void Part1(string input, long expected)
		{
			Assert.AreEqual(expected, new Day18(input).Part1());
		}

		[DataTestMethod]
		[DataRow("1 + 2 * 3 + 4 * 5 + 6", 231)]
		[DataRow("1 + (2 * 3) + (4 * (5 + 6))", 51)]
		[DataRow("2 * 3 + (4 * 5)", 46)]
		[DataRow("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
		[DataRow("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
		[DataRow("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
		public void Part2(string input, long expected)
		{
			Assert.AreEqual(expected, new Day18(input).Part2());
		}
	}
}
