param (
    $Year = (Get-Date).Year,
    $Day = (Get-Date).Day
)

$Code = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year${Year}
{
	public class Day${Day}
	{
		public Day${Day}(string input)
		{
		}

		public int Part1()
		{
			throw new NotImplementedException();
		}

		public int Part2()
		{
			throw new NotImplementedException();
		}
	}
}
"@;

$Test = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Year${Year}
{
	[TestClass]
	public class Day${Day}Tests
	{
		[DataTestMethod]
		[DataRow("", 0)]
		public void Part1(string input, int expected)
		{
			Assert.AreEqual(expected, new Day${Day}(input).Part1());
		}

		[DataTestMethod]
		[DataRow("", 0)]
		public void Part2(string input, int expected)
		{
			Assert.AreEqual(expected, new Day${Day}(input).Part2());
		}
	}
}
"@

Out-File "${PSScriptRoot}/AdventOfCode/Year${Year}/Day${Day}.cs" -InputObject $Code -Encoding UTF8
Out-File "${PSScriptRoot}/AdventOfCode/Year${Year}/Inputs/Day${Day}.txt" -InputObject "" -Encoding UTF8
Out-File "${PSScriptRoot}/AdventOfCode.Tests/Year${Year}/Day${Day}Tests.cs" -InputObject $Test -Encoding UTF8