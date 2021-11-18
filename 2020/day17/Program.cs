using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


public class Program
{
	public static void Main()
	{
		var activatedCells = GetLines(input)
			.SelectMany((l, i) => l.ToCharArray().Select((c, j) => new ValueTuple<ValueTuple<int,int,int,int>, char>(new ValueTuple<int,int,int,int>(i, j, 0, 0), c)))
			.Where(t => t.Item2 == '#')
			.Select(t => t.Item1)
			.ToHashSet();
		
		Console.WriteLine(string.Join("\n", activatedCells.Select(c => string.Format("{0}, {1}, {2}, {3}", c.Item1, c.Item2, c.Item3, c.Item4))));
		
		for(int cycle = 0; cycle < 6; cycle++)
		{
			var excitations = new Dictionary<ValueTuple<int,int,int,int>, int>();
			
			foreach(var cell in activatedCells)
				for(int dx = -1; dx < 2; dx++)
					for(int dy = -1; dy < 2; dy++)
						for(int dz = -1; dz < 2; dz++)
							for(int dw = -1; dw < 2; dw++)
							{
								if(dx  == 0 && dy == 0 && dz == 0 && dw == 0)
									continue;

								var excitedCell = new ValueTuple<int,int,int, int>(cell.Item1 + dx, cell.Item2 + dy, cell.Item3 + dz, cell.Item4 + dw);
								int count = 0;
								if(!excitations.TryGetValue(excitedCell, out count))
									excitations[excitedCell] = 1;
								else
									excitations[excitedCell] = count + 1;
							}
			
			var deactivations = activatedCells
				.Select(c => new ValueTuple<ValueTuple<int,int,int,int>,int>(c, TryGetExcitations(excitations, c)))
				.Where(c => c.Item2 < 2 || c.Item2 > 3)
				.Select(c => c.Item1)
				.ToList();
			
			activatedCells.ExceptWith(deactivations);
			
			var activations = excitations
				.Where(kvp => kvp.Value == 3)
				.Select(kvp => kvp.Key);
			
			activatedCells.UnionWith(activations);
		}
		
		Console.WriteLine("Done result is {0}.", activatedCells.Count);
	}
	
	private static int TryGetExcitations(Dictionary<ValueTuple<int,int,int,int>, int> excitations, ValueTuple<int,int,int,int> key)
	{
		int res = 0;
		if(excitations.TryGetValue(key, out res))
			return res;
		return 0;
	}
	
	private static IEnumerable<string> GetLines(string input)
	{
		var inputReader = new StringReader(input);
		string currentLine = null;
				
		while((currentLine = inputReader.ReadLine()) != null)
			yield return currentLine;
	}
	
	private static string input = @"##..#.#.
###.#.##
..###..#
.#....##
.#..####
#####...
#######.
#.##.#.#";
}
