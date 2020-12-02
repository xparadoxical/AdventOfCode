using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day2
	{
		public static Regex entry = new Regex(@"^(?<min>\d+)-(?<max>\d+) (?<char>.): (?<password>.+)$", RegexOptions.Compiled);

		private IEnumerable<PasswordEntry> entries;

		public Day2(string input)
		{
			entries = input.Lines()
				  .Select(line => entry.Match(line))
				  .Select(match => new PasswordEntry(
					  new Policy(
						  int.Parse(match.Groups["min"].Value),
						  int.Parse(match.Groups["max"].Value),
						  match.Groups["char"].Value[0]),
					  match.Groups["password"].Value));
		}

		public int Part1()
		{
			return entries.Count(entry => entry.Validate());
		}

		public int Part2()
		{
			return entries.Count(entry => entry.NewValidate());
		}

		public sealed record PasswordEntry(Policy Policy, string Password)
		{
			public bool Validate()
			{
				int count = Password.Count(c => c == Policy.Char);
				return count >= Policy.Min && count <= Policy.Max;
			}

			public bool NewValidate()
				=> Password[Policy.Min - 1] == Policy.Char
				^ Password[Policy.Max - 1] == Policy.Char;
		}

		public sealed record Policy(int Min, int Max, char Char);
	}
}
