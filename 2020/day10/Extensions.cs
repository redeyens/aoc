using System.Collections.Generic;

public static class Extensions
{
	public static IEnumerable<int> ToDifferences(this IEnumerable<int> sequence)
	{
		var iter = sequence.GetEnumerator();
		iter.MoveNext();
		int first = iter.Current;
		
		while(iter.MoveNext())
		{
			int second = iter.Current;
			yield return second - first;
			first = second;
		}
	}
	
}