using BenchmarkDotNet.Attributes;
using System;
using Towel;

namespace StringReverse
{
	public class StringReverseBenchmarks
	{
		[Params(5, 10, 50, 100, 500, 1000, 5000, 10000, 50000, 100000)]
		public int N;

		string input;
		string output;
		Random random = new();

		[IterationSetup]
		public void IterationSetup()
		{
			input = random.NextEnglishAlphabeticString(N);
		}

		[IterationCleanup]
		public void IterationCleanup()
		{
			if (output.Length < 5000)
			{
				Console.WriteLine(output);
			}
		}

		[Benchmark]
		public void StringCreateForLoop()
		{
			output = string.Create(input.Length, input, (span, s) =>
			{
				for (int a = 0, b = s.Length - 1; a < s.Length; a++, b--)
				{
					span[a] = s[b];
				}
			});
		}

		[Benchmark]
		public void StringCreateSpanReverse()
		{
			output = string.Create(input.Length, input, (span, s) =>
			{
				Span<char> chars = s.ToCharArray();
				chars.Reverse();
				chars.CopyTo(span);
			});
		}

		[Benchmark]
		public void ToCharArrayReverseNew()
		{
			char[] characters = input.ToCharArray();
			Array.Reverse(characters);
			output = new string(characters);
		}
	}
}
