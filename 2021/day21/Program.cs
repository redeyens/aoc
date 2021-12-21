using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var sumRolls = new int[] {0, 0, 0, 1, 3, 6, 7, 6, 3, 1};

            var players = PuzzleInput
                .Select(l => l.Split(' ').Skip(4).Select(int.Parse).First() - 1)
                .ToArray();            

            var games = new List<((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames)>();

            games.Add(((players[0], 0, players[1], 0), 1));

            for (int round = 0; games.Where(g => !GameCompleted(g)).Any(); round++)
            {
                games = PlayOneRound(games, round % 2, sumRolls);
            }

            Console.WriteLine(
                games
                    .Select(g => (g.state.p1Score == 21) ? (p:0, g.cntGames) : (p:1, g.cntGames))
                    .GroupBy(s => s.p)
                    .Select(g => g.Select(p => p.cntGames).Sum())
                    .OrderByDescending(c => c)
                    .First()
                );

            Console.WriteLine("day21 completed.");
        }

        private static bool GameCompleted(((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames) game)
        {
            return game.state.p1Score == 21 || game.state.p2Score == 21;
        }

        private static List<((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames)> PlayOneRound(List<((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames)> games, int player, int[] sumRolls)
        {
            return games
                .SelectMany(g => AdvanceGameState(g, player, sumRolls))
                .GroupBy(g => g.state)
                .Select(g => (g.Key, g.Select(g => g.cntGames).Sum()))
                .ToList();
        }

        private static IEnumerable<((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames)> AdvanceGameState(((int p1Pos, int p1Score, int p2Pos, int p2Score) state, long cntGames) game, int player, int[] sumRolls)
        {
            if(GameCompleted(game))
            {
                yield return game;
            }
            else
            {
                for (int totalRoll = 3; totalRoll < 10; totalRoll++)
                {
                    var nextP1Pos = (player == 0) ? (game.state.p1Pos + totalRoll) % 10 : game.state.p1Pos;
                    var nextP2Pos = (player == 1) ? (game.state.p2Pos + totalRoll) % 10 : game.state.p2Pos;
                    var nextP1Score = Math.Min(game.state.p1Score + ((player == 0) ? nextP1Pos + 1 : 0), 21);
                    var nextP2Score = Math.Min(game.state.p2Score + ((player == 1) ? nextP2Pos + 1 : 0), 21);
    
                    yield return ((nextP1Pos, nextP1Score, nextP2Pos, nextP2Score), game.cntGames * sumRolls[totalRoll]);
                }
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day21.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day21.Input.PuzzleInput.txt");
            }
        }

        private static IEnumerable<string> GetLinesFromResource(string name)
        {
            using (Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                using (TextReader inReader = new StreamReader(inStream))
                {
                    string line;
                    while ((line = inReader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
