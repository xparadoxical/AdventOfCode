using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year2020
{
	[TestClass]
	public sealed class Day3Tests
	{
		public const string INPUT =@"
..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

		[DataTestMethod]
		[DataRow(INPUT, 7)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day3(input).Part1());
		}

		[DataTestMethod]
		[DataRow(INPUT, 336ul)]
		public void Part2(string input, ulong expected)
		{
			Assert.AreEqual(expected, new Day3(input).Part2());
		}
	}
}
