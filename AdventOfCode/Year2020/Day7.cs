#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day7
	{
		private readonly BagDefinitions definitions;

		public Day7(string input)
		{
			var bagDefinitionRegex = new Regex(@"(?<color>[a-z ]+) bags contain(?:(?<child> (?<count>\d+) (?<childColor>[a-z ]+) bags?(?:,|\.))+| no other bags\.)", RegexOptions.Compiled);
			definitions = new BagDefinitions();

			foreach (Match match in bagDefinitionRegex.Matches(input))
			{
				string color = match.Groups["color"].Value;

				if (match.Groups["child"].Success)
				{
					List<BagChild> children = new(match.Groups["child"].Captures.Count);

					for (int i = 0; i < children.Capacity; i++)
					{
						int count = int.Parse(match.Groups["count"].Captures[i].Value);
						string childColor = match.Groups["childColor"].Captures[i].Value;
						children.Add(new BagChild(childColor, count));
					}

					definitions.Add(new Bag(color, children));
				}
				else
				{
					definitions.Add(new Bag(color));
				}
			}
		}

		public int Part1()
		{
			int canHoldShinyGoldBag = 0;

			foreach (var bagDef in definitions)
			{
				if (bagDef.Children is null)
					continue;

				Stack<IEnumerator<BagChild>> stack = new();
				stack.Push(bagDef.Children.GetEnumerator());

				do
				{
					var enumerator = stack.Peek();
					if (enumerator.MoveNext())
					{
						string color = enumerator.Current.Color;
						if (color == "shiny gold")
						{
							canHoldShinyGoldBag++;
							break;
						}

						var newBag = definitions[color];
						if (newBag.Children is not null)
						{
							stack.Push(newBag.Children.GetEnumerator());
						}
					}
					else
					{
						_ = stack.Pop();
						enumerator.Dispose();
					}
				}
				while (stack.Count > 0);

				while (stack.Count > 0)
					stack.Pop()?.Dispose();
			}

			return canHoldShinyGoldBag;
		}

		public int Part2()
		{
			var shinyGold = definitions["shiny gold"];

			Stack<(int?[] subnodeValueCache, IEnumerator<BagChild> enumerator, RefInt index)> stack = new();

			{
				var enumerator = shinyGold.Children!.GetEnumerator();
				int?[] valueCache = new int?[shinyGold.Children!.Count()];
				stack.Push((valueCache, enumerator, -1));
			}

			int count = -1;
			do
			{
				var current = stack.Peek();
				if (current.enumerator.MoveNext())
				{
					current.index++;
					var bagChild = current.enumerator.Current;
					var bagDef = definitions[bagChild.Color];

					if (bagDef.Children is null)
					{
						current.subnodeValueCache[current.index] = bagChild.Count;
					}
					else
					{
						stack.Push((new int?[bagDef.Children.Count()], bagDef.Children.GetEnumerator(), -1));
					}
				}
				else
				{
					_ = stack.Pop();
					if (stack.Count > 0)
					{
						var parent = stack.Peek();
						parent.subnodeValueCache[parent.index] = (current.subnodeValueCache.Sum() + 1) * parent.enumerator.Current.Count;
					}
					else
					{
						count = current.subnodeValueCache.Sum()!.Value;
					}

					current.enumerator.Dispose();
				}
			}
			while (stack.Count > 0);

			return count;
		}

		public sealed class RefInt
		{
			public int Value { get; set; }

			public RefInt(int value) => Value = value;

			public static RefInt operator ++(RefInt a)
			{
				a.Value++;
				return a;
			}

			public static implicit operator RefInt(int i) => new RefInt(i);
			public static implicit operator int(RefInt ri) => ri.Value;
		}

		public sealed class BagDefinitions : ICollection<Bag>
		{
			private readonly HashSet<Bag> colors = new();

			public Bag this[string color]
			{
				get
				{
					var bag = colors.FirstOrDefault(b => b.Color == color);
					if (bag is null)
						throw new KeyNotFoundException();
					return bag;
				}
			}

			public int Count => colors.Count;
			public bool IsReadOnly => false;

			public void Add(Bag item)
			{
				if (!Contains(item))
				{
					bool b = colors.Add(item);
				}
			}

			public void Clear() => colors.Clear();
			public bool Contains(Bag item)
			{
				return colors.Any(bag =>
				{
					if (bag.Color != item.Color)
						return false;
					if (bag.Children is null ^ item.Children is null)
						return false;
					if (bag.Children is null && item.Children is null)
						return true;
					if (bag.Children!.Count() != item.Children!.Count())
						return false;

					return bag.Children!.Zip(item.Children!).All(p => p.First == p.Second);
				});
			}
			public void CopyTo(Bag[] array, int arrayIndex) => colors.CopyTo(array, arrayIndex);
			public IEnumerator<Bag> GetEnumerator() => colors.GetEnumerator();
			public bool Remove(Bag item) => colors.Remove(item);
			IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)colors).GetEnumerator();
		}

		public sealed record Bag(string Color, IEnumerable<BagChild>? Children = null);
		public sealed record BagChild(string Color, int Count);
	}
}
