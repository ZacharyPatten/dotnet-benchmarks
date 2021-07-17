using BenchmarkDotNet.Running;

namespace StringReverse
{
	public class Program
	{
		public static void Main()
		{
			BenchmarkRunner.Run<StringReverseBenchmarks>();
		}
	}
}