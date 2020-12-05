using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day5Tests
	{
		[DataTestMethod]
		[DataRow("BFFFBBFRRR\nFFFBBBFRRR\nBBFFBBFRLL", 820)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day5(input).Part1());
		}
	}
}
