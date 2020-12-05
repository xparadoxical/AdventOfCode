#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdventOfCode.Year2020
{
	public sealed class Day4
	{
		public readonly string[] RequiredFields = new[] { BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportID };
		public const string BirthYear = "byr";
		public const string IssueYear = "iyr";
		public const string ExpirationYear = "eyr";
		public const string Height = "hgt";
		public const string HairColor = "hcl";
		public const string EyeColor = "ecl";
		public const string PassportID = "pid";

		private readonly IEnumerable<Passport> passports;

		public Day4(string input)
		{
			passports = Regex.Split(input, @"(?:\n{2}|\r{2}|(?:\r\n){2})")
				.Select(passport => passport.Replace(Helpers.LineSeparators, " ")
					.Split(' ')
					.Select(field => field.Split(':'))
					.Select<string[], IField>(fieldKvp => fieldKvp[0] switch
						{
							BirthYear => new Field<int>(BirthYear, new IntValue(4, 1920, 2002), fieldKvp[1]),
							IssueYear => new Field<int>(IssueYear, new IntValue(4, 2010, 2020), fieldKvp[1]),
							ExpirationYear => new Field<int>(ExpirationYear, new IntValue(4, 2020, 2030), fieldKvp[1]),
							Height => new Field<int>(Height, new IntValueWithUnit(("cm", 150, 193), ("in", 59, 76)), fieldKvp[1]),
							HairColor => new Field<string>(HairColor, new HexStringValue(6, "#"), fieldKvp[1]),
							EyeColor => new Field<string>(EyeColor, new OneOfValues("amb", "blu", "brn", "gry", "grn", "hzl", "oth"), fieldKvp[1]),
							PassportID => new Field<int>(PassportID, new IntValue(9, 0), fieldKvp[1]),
							_ => new OptionalField(fieldKvp[0], fieldKvp[1])
						}))
				.Select(fields => new Passport(fields));
		}

		public int Part1()
		{
			return passports.Count(passport => passport.Validate(RequiredFields).present);
		}

		public int Part2()
		{
			return passports.Select(passport => passport.Validate(RequiredFields))
							.Count(result => result.valid && result.present);
		}

		//===================================================================================

		public sealed record Passport(IEnumerable<IField> Fields)
		{
			public (bool present, bool valid) Validate(params string[] keys)
			{
				var byKeys = Fields.Where(field => keys.Contains(field.Key));
				bool present = byKeys.Count() == keys.Length;

				return (present, valid: byKeys.All(field => field.Valid));
			}
		}

		public interface IField
		{
			public abstract string Key { get; }
			public abstract bool Valid { get; }
		}

		[DebuggerDisplay(@"\{Field \{ Type = {Type.GetType().Name}, Key = {Key}, StringValue = {StringValue}, Value = {Value}, Valid = {Valid} \}\}")]
		public record Field<T>(string Key, ValueType<T> Type, string StringValue) : IField
		{
			private bool initialized = false;

			private T? _value;
			public T? Value => InitValue();

			private bool _valid;
			public bool Valid
			{
				get
				{
					_ = InitValue();
					return _valid;
				}
			}

			private T? InitValue()
			{
				if (!initialized)
				{
					_valid = Type.TryParse(StringValue, out _value);
					initialized = true;
				}

				return _value;
			}
		}

		public sealed record OptionalField(string Key, string StringValue)
			: Field<string>(Key, new ValueType<string>(s => (s, true)), StringValue);

		public record ValueType<T>(Func<string, (T? result, bool success)> Parser)
		{
			public bool TryParse(string s, out T? result)
			{
				var (_result, success) = Parser(s);
				result = _result;
				return success;
			}
		}

		public record IntValue(int Digits, int Min = int.MinValue, int Max = int.MaxValue)
			: ValueType<int>(s =>
			{
				int result = default;
				bool success = s.Length == Digits
							&& int.TryParse(s, out result)
							&& result >= Min && result <= Max;

				return (result, success);
			});

		public record IntValueWithUnit(params (string unit, int min, int max)[] Types)
			: ValueType<int>(s =>
			{
				(string? unit, int min, int max) = Types.FirstOrDefault(type => s.EndsWith(type.unit));
				if (unit is null) return (default, false);

				string num = s[..s.IndexOf(unit)];
				bool success = int.TryParse(num, out int result)
							&& result >= min
							&& result <= max;
				return (result, success);
			});

		public record HexStringValue(int Digits, string Prefix = "0x")
			: ValueType<string>(s =>
			{
				if (!s.StartsWith(Prefix)) return (default, false);
				string noPrefix = s[Prefix.Length..];
				return (noPrefix, noPrefix.All(c => HexChars.Contains(char.ToLower(c))));
			})
		{
			public static readonly char[] HexChars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
		}

		public record OneOfValues(params string[] Values)
			: ValueType<string>(s =>
			{
				string? value = Values.FirstOrDefault(v => s == v);
				return (value, value is not null);
			});
	}
}
