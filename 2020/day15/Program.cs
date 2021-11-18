using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
	public static void Main()
	{
		var memory = GetLines(input)
			.SelectMany(x => x.Split(','))
			.Select((x, index) => new Tuple<int, long>(index, Int64.Parse(x)))
			.ToDictionary(x => x.Item2, x => x.Item1 + 1);
		
		long lastNum = 0;
		
		// Console.WriteLine(string.Join("\n", memory.Select(x => string.Format("{0}:{1}", x.Key, x.Value))));
		Console.WriteLine(lastNum);
		
		for(int i = memory.Count + 1; i < 30000000; i++)
		{
			int prevIndex = 0;

			if(memory.TryGetValue(lastNum, out prevIndex))
			{
				memory[lastNum] = i;
				lastNum = i - prevIndex;
			}
			else
			{
				memory[lastNum] = i;
				lastNum = 0;
			}
			
			// Console.WriteLine(lastNum);
		}
		
		Console.WriteLine("Done result is {0}.", lastNum);
	}
	
	
	private static IEnumerable<string> GetLines(string input)
	{
		var inputReader = new StringReader(input);
		string currentLine = null;
				
		while((currentLine = inputReader.ReadLine()) != null)
			yield return currentLine;
	}
	
	private static string input = @"8,0,17,4,1,12";
}
