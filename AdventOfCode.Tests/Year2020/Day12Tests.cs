using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day12Tests
	{
		public const string Input = "F10\nN3\nF7\nR90\nF11";

		[DataTestMethod]
		[DataRow(Input, 25)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day12(input).Part1());
		}

		[DataTestMethod]
		[DataRow(Input, 286)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day12(input).Part2());
		}
	}
}
