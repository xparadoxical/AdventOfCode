using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day5
	{
		private readonly string[] passes;

		public Day5(string input)
		{
			passes = input.Lines().ToArray();
		}

		public int Part1()
		{
			int highest = -1;

			foreach (string pass in passes)
			{
				int id = SeatID(pass);
				if (id > highest) 
					highest = id;
			}

			return highest;
		}

		public int Part2()
		{
			var ids = passes.Select(pass => SeatID(pass)).OrderBy(id => id).ToArray();
			for (int i = 0; i < ids.Length - 1; i++)
				if (ids[i+1] != ids[i] + 1)
					return ids[i] + 1;

			throw new ArgumentException();
		}

		public static int SeatID(string boardingPass)
		{
			int x = 0, xl = 8;
			int y = 0, yl = 128;

			foreach (char c in boardingPass)
			{
				if (c == 'F')
					yl /= 2;
				else if (c == 'B')
				{
					yl /= 2;
					y += yl;
				}
				else if (c == 'L')
					xl /= 2;
				else if (c == 'R')
				{
					xl /= 2;
					x += xl;
				}
			}

			if (!(xl == yl && xl == 1)) throw new ArgumentException();

			return y * 8 + x;
		}
	}
}
