using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		Tuple<long,long>[] busses = GetLines(input).Skip(1).Take(1).SelectMany(x => x.Split(','))
			.Select((item, index) => new Tuple<long,string>(index, item.Replace('x',' ')))
			.Where(x => !string.IsNullOrWhiteSpace(x.Item2))
			.Select(x => new Tuple<long,long>(x.Item1, Int64.Parse(x.Item2)))
			.Select(x => new Tuple<long,long>((x.Item2 - (x.Item1 % x.Item2)) % x.Item2, x.Item2))
			.OrderByDescending(x => x.Item2)
			.ToArray();
		
		
		for(int i = 0; i < busses.Length; i++)
		{
			Console.WriteLine("{0}: {1}", busses[i].Item2, busses[i].Item1);
		}
		Console.WriteLine("---------------------------");
		
		long firstDeparture = busses[0].Item1;
		long step = 1;
		
		for(int i = 1; i < busses.Length; i++)
		{
			//Console.WriteLine("{0}, {1}, {2}", firstDeparture, busses[i-1].Item2, busses[i-1].Item1);
			step *= busses[i-1].Item2;
			while(firstDeparture % busses[i].Item2 != busses[i].Item1)
			{
				firstDeparture += step;
				//Console.WriteLine("{0}, {1}", step, firstDeparture);
			}
		}
		
		for(int i = 0; i < busses.Length; i++)
		{
			Console.WriteLine("{0}, {1}, {2}", busses[i].Item2, firstDeparture % busses[i].Item2, busses[i].Item1);
		}
		Console.WriteLine("---------------------------");
		
		Console.WriteLine("Done result is {0}.", firstDeparture);
	}
	
	private static IEnumerable<string> GetLines(string input)
	{
		var inputReader = new StringReader(input);
		string currentLine = null;
				
		while((currentLine = inputReader.ReadLine()) != null)
			yield return currentLine;
	}
	
	private static string input = @"939
19,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,521,x,x,x,x,x,x,x,23,x,x,x,x,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,523,x,x,x,x,x,37,x,x,x,x,x,x,13";
}
