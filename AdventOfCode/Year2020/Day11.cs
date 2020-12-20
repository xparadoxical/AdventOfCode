using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day11
	{
		private readonly char[,] seatLayout;

		public Day11(string input)
		{
			var lines = input.Lines();
			seatLayout = new char[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
				for (int j = 0; j < lines[i].Length; j++)
					seatLayout[i, j] = lines[i][j];
		}

		public int Part1()
		{
			int CountAdjacentOccupied(int r, int c)
			{
				int h = seatLayout.GetUpperBound(0) + 1;
				int w = seatLayout.GetUpperBound(1) + 1;

				List<(int r, int c)> toCheck = new()
				{
					(r - 1, c - 1), (r - 1, c), (r - 1, c + 1),
					(r, c - 1),                 (r, c + 1),
					(r + 1, c - 1), (r + 1, c), (r + 1, c + 1)
				};

				int occupied = 0;
				foreach (var p in toCheck)
					if (p.r >= 0 && p.r < h && p.c >= 0 && p.c < w && seatLayout[p.r, p.c] is '#')
						occupied++;

				return occupied;
			}

			int h = seatLayout.GetUpperBound(0) + 1;
			int w = seatLayout.GetUpperBound(1) + 1;

			while (true)
			{
				List<(int r, int c)> toChange = new();

				for (int i = 0; i < h; i++)
					for (int j = 0; j < w; j++)
						if (seatLayout[i, j] is 'L' && CountAdjacentOccupied(i, j) == 0)
							toChange.Add((i, j));
						else if (seatLayout[i, j] is '#' && CountAdjacentOccupied(i, j) >= 4)
							toChange.Add((i, j));

				if (toChange.Count > 0)
				{
					foreach ((int r, int c) in toChange)
					{
						char seat = seatLayout[r, c];
						seatLayout[r, c] = seat is 'L' ? '#'
										 : seat is '#' ? 'L'
										 : throw new Exception($"Tried to change {seat} at {r},{c}");
					}
				}
				else
					break;
			}

			int occupied = 0;
			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
					if (seatLayout[i, j] is '#')
						occupied++;

			return occupied;
		}

		public int Part2()
		{
			int h = seatLayout.GetUpperBound(0) + 1;
			int w = seatLayout.GetUpperBound(1) + 1;

			int CountVisibleOccupied(int r, int c)
			{

				int occupied = 0;

				void raycastAtASeat(int r, int c, int rd, int cd)
				{
					while (true)
					{
						if (rd > 0) r++;
						else if (rd < 0) r--;

						if (cd > 0) c++;
						else if (cd < 0) c--;

						if (!(r >= 0 && r < h && c >= 0 && c < w))
							break;

						char tile = seatLayout[r, c];
						if (tile is not '.')
						{
							if (tile is '#')
								occupied++;

							break;
						}
					}
				}

				raycastAtASeat(r, c, -1, 0); //↑ up
				raycastAtASeat(r, c, -1, 1); //↗ up-right
				raycastAtASeat(r, c, 0, 1); //→ right
				raycastAtASeat(r, c, 1, 1); //↘ down-right
				raycastAtASeat(r, c, 1, 0); //↓ down
				raycastAtASeat(r, c, 1, -1); //↙ down-left
				raycastAtASeat(r, c, 0, -1); //← left
				raycastAtASeat(r, c, -1, -1); //↖ up-left

				return occupied;
			}

			while (true)
			{
				List<(int r, int c)> toChange = new();

				for (int i = 0; i < h; i++)
					for (int j = 0; j < w; j++)
						if (seatLayout[i, j] is 'L' && CountVisibleOccupied(i, j) == 0)
							toChange.Add((i, j));
						else if (seatLayout[i, j] is '#' && CountVisibleOccupied(i, j) >= 5)
							toChange.Add((i, j));

				if (toChange.Count > 0)
				{
					foreach ((int r, int c) in toChange)
					{
						char seat = seatLayout[r, c];
						seatLayout[r, c] = seat is 'L' ? '#'
										 : seat is '#' ? 'L'
										 : throw new Exception($"Tried to change {seat} at {r},{c}");
					}
				}
				else
					break;
			}

			int occupied = 0;
			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
					if (seatLayout[i, j] is '#')
						occupied++;

			return occupied;
		}
	}
}
