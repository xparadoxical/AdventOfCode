using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day3
	{
		private readonly string[] map;

		public Day3(string input)
		{
			map = input.Lines().ToArray();
		}

		public int Part1()
		{
			int x = 0, y = 0;
			int trees = 0;
			int width = map[0].Length;

			while (y < map.Length)
			{
				if (map[y][x] == '#')
					trees++;

				x += 3;
				y++;
				if (x >= width)
					x -= width;
			}

			return trees;
		}

		public ulong Part2()
		{
			(int, int)[] slopes = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
			ulong result = 1;

			int width = map[0].Length;
			foreach ((int dx, int dy) in slopes)
			{
				int x = 0, y = 0;
				uint trees = 0;

				while (y < map.Length)
				{
					if (map[y][x] == '#')
						trees++;

					x += dx;
					y += dy;
					if (x >= width)
						x -= width;
				}

				result *= trees;
			}

			return result;
		}
	}
}
