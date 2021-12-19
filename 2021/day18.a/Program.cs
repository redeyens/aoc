using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day18.a
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = PuzzleInput.Aggregate(string.Empty, SnailAdd);

            Console.WriteLine(GetMagnitude(result));

            Console.WriteLine("day18.a completed.");
        }

        private static int GetMagnitude(string snailNumber)
        {
            var nums = new Stack<int>();
            
            for (int i = 0; i < snailNumber.Length; i++)
            {
                if(char.IsNumber(snailNumber[i]))
                {
                    nums.Push(snailNumber[i] - '0');
                }
                if(snailNumber[i] == ']')
                {
                    var magnitude = 2 * nums.Pop() + 3 * nums.Pop();
                    nums.Push(magnitude);
                }
            }

            return nums.Pop();
        }

        private static string SnailAdd(string arg1, string arg2)
        {
            if(string.IsNullOrEmpty(arg1))
            {
                return arg2;
            }

            string result = $"[{arg1},{arg2}]";

            return SnailReduce(result);
        }

        private static string SnailReduce(string input)
        {
            string result = input;
            while (TryExplode(result, out result) || TrySplit(result, out result));

            return result;
        }

        private static bool TryExplode(string input, out string result)
        {
            int depth = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == '[')
                {
                    depth++;
                }
                if(input[i] == ']')
                {
                    depth--;
                }
                if(char.IsNumber(input[i]) && depth > 4)
                {
                    int l = 0;
                    while (l < input.Length - i && char.IsNumber(input[i+l]))
                    {
                        l++;
                    }

                    if(input[i+l] == ',')
                    {
                        if(char.IsNumber(input[i+l+1]))
                        {
                            var first = int.Parse(input.Substring(i, l));
                            var leftHalf = input.Substring(0, i - 1);
                            i += l + 1;

                            l = 0;
                            while (l < input.Length - i && char.IsNumber(input[i + l]))
                            {
                                l++;
                            }

                            var second = int.Parse(input.Substring(i, l));
                            var rightHalf = input.Substring(i + l + 1);
                            
                            result = PushLeft(leftHalf, first) + "0" + PushRight(rightHalf, second);
                            return true;
                        }
                    }
                }
            }

            result = input;
            return false;
        }

        private static string PushRight(string input, int num)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int l = 0;
                while (l < input.Length - i && char.IsNumber(input[i + l]))
                {
                    l++;
                }

                if(l > 0)
                {
                    var firstNum = int.Parse(input.Substring(i, l));
                    return input.Substring(0, i) + (firstNum + num).ToString() + input.Substring(i + l);
                }
            }

            return input;
        }

        private static string PushLeft(string input, int num)
        {
            for (int i = input.Length - 1; i >= 0; i--)
            {
                int l = 0;
                while ((i - l) > 0 && char.IsNumber(input[i - l]))
                {
                    l++;
                }

                if(l > 0)
                {
                    var firstNum = int.Parse(input.Substring(i - l + 1, l));
                    return input.Substring(0, i - l + 1) + (firstNum + num).ToString() + input.Substring(i + 1);
                }
            }

            return input;
        }

        private static bool TrySplit(string input, out string result)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int l = 0;
                while (l < input.Length - i && char.IsNumber(input[i+l]))
                {
                    l++;
                }

                if(l > 1)
                {
                    var value = int.Parse(input.Substring(i, l)) / 2.0;
                    var first = (int)Math.Floor(value);
                    var second = (int)Math.Ceiling(value);
                    result = string.Join(string.Empty, input.Substring(0, i), "[", first.ToString(), ",", second.ToString(), "]", input.Substring(i+l));
                    return true;
                }
            }

            result = input;
            return false;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day18.a.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day18.a.Input.PuzzleInput.txt");
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
