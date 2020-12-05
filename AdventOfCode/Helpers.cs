using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
	public static class Helpers
	{
		public static readonly string[] LineSeparators = new[] { Environment.NewLine, "\n", "\r" };

		public static string[] Lines(this string s)
			=> s.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		public static string Replace(this string s, ICollection<string> oldValues, string newValue)
		{
			foreach (string oldValue in oldValues)
				s = s.Replace(oldValue, newValue);

			return s;
		}
	}
}
