using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day12
	{
		private readonly IEnumerable<(char action, int value)> instructions;

		public Day12(string input)
		{
			instructions = input.Lines().Select(s => (action: s[0], value: int.Parse(s[1..])));
		}

		public int Part1()
		{
			var pos = (x: 0, y: 0);
			var rot = 90;

			foreach ((char action, int value) in instructions)
			{
				(pos, rot) = action switch
				{
					'N' => ((pos.x, pos.y + value), rot),
					'S' => ((pos.x, pos.y - value), rot),
					'E' => ((pos.x + value, pos.y), rot),
					'W' => ((pos.x - value, pos.y), rot),
					'L' => (pos, (rot - value) % 360),
					'R' => (pos, (rot + value) % 360),
					'F' => (rot switch
					{
						0 => (pos.x, pos.y + value),
						90 or -270 => (pos.x + value, pos.y),
						180 or -180 => (pos.x, pos.y - value),
						270 or -90 => (pos.x - value, pos.y),
						_ => throw new Exception($"Invalid rotation {rot}")
					}, rot),
					_ => throw new Exception($"Invalid action {action}")
				};
			}

			return Math.Abs(pos.x) + Math.Abs(pos.y);
		}

		public int Part2()
		{
			var pos = (x: 0, y: 0);
			var way = (x: 10, y: 1);

			foreach ((char action, int value) in instructions)
			{
				(pos, way) = (action, value) switch
				{
					('N', _) => (pos, (way.x, way.y + value)),
					('S', _) => (pos, (way.x, way.y - value)),
					('E', _) => (pos, (way.x + value, way.y)),
					('W', _) => (pos, (way.x - value, way.y)),
					('L', 90) or ('R', 270) => (pos, (-way.y, way.x)),
					('L' or 'R', 180) => (pos, (-way.x, -way.y)),
					('L', 270) or ('R', 90) => (pos, (way.y, -way.x)),
					('F', _) => ((pos.x + way.x * value, pos.y + way.y * value), way),
					_ => throw new Exception($"Unknown instruction: {action}{value}")
				};
			}

			return Math.Abs(pos.x) + Math.Abs(pos.y);
		}
	}
}
