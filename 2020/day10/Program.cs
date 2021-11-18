using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		int[] baseJoltage = new int[] {0};
		long[] combinations;
		
		var values = baseJoltage.Concat(
				GetLines(input)
				.Select(s => Int32.Parse(s)))
			.OrderBy(num => num)
			.ToList();
		
		combinations = new long[values.Count];
		combinations[0] = 1;
		
		for(int i = 1; i < combinations.Length; i++)
		{
			combinations[i] = combinations[i-1];
			
			if((i-2) >= 0 && (values[i] - values[i-2]) < 4)
				combinations[i] += combinations[i-2];
			
			if((i-3) >= 0 && (values[i] - values[i-3]) < 4)
				combinations[i] += combinations[i-3];
			
			//Console.WriteLine("{0} - {1} : {2}", values[i-1], values[i], combinations[i]);
		}
		
		Console.WriteLine("Done result is {0}.", combinations[combinations.Length-1]);
	}
	
	private static IEnumerable<string> GetLines(string input)
	{
		var inputReader = new StringReader(input);
		string currentLine = null;
				
		while((currentLine = inputReader.ReadLine()) != null)
			yield return currentLine;
	}
	
	private static string input = @"115
134
121
184
78
84
77
159
133
90
71
185
152
165
39
64
85
50
20
75
2
120
137
164
101
56
153
63
70
10
72
37
86
27
166
186
154
131
1
122
95
14
119
3
99
172
111
142
26
82
8
31
53
28
139
110
138
175
108
145
58
76
7
23
83
49
132
57
40
48
102
11
105
146
149
66
38
155
109
128
181
43
44
94
4
169
89
96
60
69
9
163
116
45
59
15
178
34
114
17
16
79
91
100
162
125
156
65";
}
