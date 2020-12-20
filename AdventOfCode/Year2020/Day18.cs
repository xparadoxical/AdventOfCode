using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public sealed class Day18
	{
		private readonly IEnumerable<string> expressions;

		public Day18(string input)
		{
			expressions = input.Lines();
		}

		public long Part1()
		{
			Operators operators = new()
			{
				new('+', 0, (l, r) => l + r),
				new('*', 0, (l, r) => l * r)
			};

			return expressions.Sum(expr => Evaluate(expr, operators));
		}

		public long Part2()
		{
			Operators operators = new()
			{
				new('+', 1, (l, r) => l + r),
				new('*', 0, (l, r) => l * r)
			};

			return expressions.Sum(expr => Evaluate(expr, operators));
		}

		public static long Evaluate(string input, Operators definitions)
		{
			Stack<char> operators = new();
			Stack<long> numbers = new();

			void DoOperation()
			{
				long r = numbers.Pop();
				long l = numbers.Pop();
				numbers.Push(definitions[operators.Pop()].Calculate(l, r));
			}

			foreach (char token in input)
			{
				if (char.IsDigit(token))
					numbers.Push(token - '0');
				else if (definitions.Contains(token))
				{
					while (operators.Count > 0
						&& operators.Peek() is not '('
						&& definitions[operators.Peek()].Precedence >= definitions[token].Precedence)
					{
						DoOperation();
					}

					operators.Push(token);
				}
				else if (token is '(')
					operators.Push(token);
				else if (token is ')')
				{
					while (operators.Peek() is not '(')
						DoOperation();

					if (operators.Peek() is '(')
						operators.Pop();
				}
			}

			while (operators.Count > 0)
				DoOperation();

			return numbers.Pop();
		}

		public sealed class Operators : List<Operator>
		{
			public Operator this[char c] => this.FirstOrDefault(op => op.Character == c) ?? throw new Exception($"Unknown operator {c}");
			public bool Contains(char c) => this.Any(op => op.Character == c);
		}

		public sealed record Operator(char Character, int Precedence, Func<long, long, long> Calculate);
	}
}
