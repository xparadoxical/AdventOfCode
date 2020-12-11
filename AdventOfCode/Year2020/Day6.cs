using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day6
	{
		private readonly IEnumerable<string[]> groups;

		public Day6(string input)
		{
			groups = Regex.Split(input, @"(?:\r?\n){2}").Select(group => group.Lines());
		}

		public int Part1()
		{
			return groups.Select(group => group.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s), sb => sb.ToString()))
						 .Sum(group => group.Distinct().Count());
		}

		public int Part2()
		{
			return groups.Sum(group => group.Aggregate(
				new { AllYes = Enumerable.Empty<char>(), First = true },
				(acc, yes) => new { AllYes = acc.First ? yes : acc.AllYes.Intersect(yes), First = false },
				allYes => allYes.AllYes.Count())
			);
		}
	}
}
