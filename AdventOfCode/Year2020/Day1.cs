using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public class Day1
	{
		private readonly int[] input;
		public Day1(string input)
		{
			this.input = input.Lines().Select(int.Parse).ToArray();
		}

		public int Part1()
		{
			for (int i = 0; i < input.Length - 1; i++)
				for (int j = i + 1; j < input.Length; j++)
					if (input[i] + input[j] == 2020)
						return input[i] * input[j];

			throw new Exception("Could not find two entries that sum to 2020.");
		}

		public int Part2()
		{
			for (int i = 0; i < input.Length - 2; i++)
				for (int j = i + 1; j < input.Length - 1; j++)
					for (int k = j + 1; k < input.Length; k++)
						if (input[i] + input[j] + input[k] == 2020)
							return input[i] * input[j] * input[k];

			throw new Exception("Could not find three entries that sum to 2020.");
		}
	}
}
