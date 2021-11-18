using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day22
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = finalInput;

            var player1 = new Queue<int>(
                GetLines(input)
                .Skip(1)
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => Int32.Parse(l))
                );

            var player2 = new Queue<int>(
                GetLines(input)
                .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
                .Skip(2)
                .Select(l => Int32.Parse(l))
                );

            Queue<int> winner = PlayTheGame(player1, player2);

            int score = 0;
            while (winner.Count > 0)
            {
                int w = winner.Count;
                score += w * winner.Dequeue();
            }

            Console.WriteLine("Winner score is {0}", score);
        }

        private static Queue<int> PlayTheGame(Queue<int> player1, Queue<int> player2)
        {
            var history = new HashSet<string>();
            while (player1.Count > 0 && player2.Count > 0)
            {
                var histEntry = string.Format("{0}\n{1}", PrintDeck(player1), PrintDeck(player2));

                if(history.Contains(histEntry))
                {
                    return player1;
                }
                history.Add(histEntry);

                int player1Card = player1.Dequeue();
                int player2Card = player2.Dequeue();

                Queue<int> roundWinner;

                if(player1Card <= player1.Count && player2Card <= player2.Count)
                {
                    var player1Sub = new Queue<int>(player1.Take(player1Card));
                    var player2Sub = new Queue<int>(player2.Take(player2Card));

                    var subGameWinner = PlayTheGame(player1Sub, player2Sub);

                    roundWinner = (player1Sub == subGameWinner) ? player1 : player2;
                }
                else
                {
                    roundWinner = (player1Card > player2Card) ? player1 : player2;
                }

                if (roundWinner == player1)
                {
                    player1.Enqueue(player1Card);
                    player1.Enqueue(player2Card);
                }
                else
                {
                    player2.Enqueue(player2Card);
                    player2.Enqueue(player1Card);
                }
            }

            var winner = (player1.Count > 0) ? player1 : player2;
            return winner;
        }

        private static object PrintDeck(Queue<int> deck)
        {
            return string.Join(",", deck);
        }

        private static IEnumerable<string> GetLines(string input)
	    {
            var inputReader = new StringReader(input);
            string currentLine = null;
                    
            while((currentLine = inputReader.ReadLine()) != null)
                yield return currentLine;
	    }
	
	    private static string testInput = @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";
	    private static string finalInput = @"Player 1:
10
39
16
32
5
46
47
45
48
26
36
27
24
37
49
25
30
13
23
1
9
3
31
14
4

Player 2:
2
15
29
41
11
21
8
44
38
19
12
20
40
17
22
35
34
42
50
6
33
7
18
28
43";

    }
}
