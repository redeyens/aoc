﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var rulesTemplate = new Regex(@"(.+) bags contain( ((\d+) ([^,\.]+)|no other) bags?[,\.])+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var rules = new Dictionary<string, HashSet<string>>();
            var containedBags = new Dictionary<string, Dictionary<string, int>>();

            foreach (var line in GetLines(finalInput))
            {
                var m = rulesTemplate.Match(line);

                var containerBagName = m.Groups[1].Value;
                var bagContents = m.Groups[5].Captures.Select(c => c.Value).ToHashSet();
                var containedBagsCount = m.Groups[4].Captures
                    .Select(c => Int32.Parse(c.Value))
                    .Zip(m.Groups[5].Captures.Select(c => c.Value))
                    .ToDictionary(x => x.Second, x => x.First);

                containedBags[containerBagName] = containedBagsCount;
                
                foreach (var bag in bagContents)
                {
                    var containers = default(HashSet<string>);
                    if(!rules.TryGetValue(bag, out containers))
                    {
                        containers = new HashSet<string>();
                        rules[bag] = containers;
                    }

                    containers.Add(containerBagName);
                }
            }

            var result = new HashSet<string>();
            var processingQ = new Queue<string>(rules["shiny gold"]);

            while (processingQ.Count > 0)
            {
                var bag = processingQ.Dequeue();
                result.Add(bag);
                foreach (var container in GetContainers(rules, bag))
                {
                    processingQ.Enqueue(container);
                }
            }

            var countingQ = new Queue<(int, string)>();

            countingQ.Enqueue((1, "shiny gold"));
            
            int bagsRequired = 0;

            while (countingQ.Count > 0)
            {
                var bag = countingQ.Dequeue();

                 foreach (var b in containedBags[bag.Item2])
                {
                    var numBags = b.Value * bag.Item1;
                    bagsRequired += numBags;
                    countingQ.Enqueue((numBags, b.Key));
                }
            }

                        
            Console.WriteLine(result.Count);
            Console.WriteLine(bagsRequired);
        }

        private static IEnumerable<string> GetContainers(Dictionary<string, HashSet<string>> rules, string bag)
        {
            var containers = default(HashSet<string>);
            if(rules.TryGetValue(bag, out containers))
            {
                return containers;
            }

            return Enumerable.Empty<string>();
        }

        private static IEnumerable<string> GetLines(string input)
        {
            var inputReader = new StringReader(input);
            string currentLine = null;
                    
            while((currentLine = inputReader.ReadLine()) != null)
                yield return currentLine;
        }
        

        private static readonly string testInput = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

        private static readonly string finalInput = @"posh brown bags contain 5 dim coral bags, 1 plaid blue bag, 2 faded bronze bags, 2 light black bags.
vibrant lime bags contain 3 dull lavender bags, 3 dim crimson bags, 3 mirrored lavender bags, 2 muted cyan bags.
clear olive bags contain 1 wavy gold bag, 4 dim lime bags, 3 dull tomato bags, 5 dark turquoise bags.
dark purple bags contain 5 striped tan bags, 5 bright cyan bags, 3 dark indigo bags.
posh maroon bags contain 3 bright salmon bags.
dim violet bags contain 1 pale violet bag, 1 bright gold bag.
clear gray bags contain 1 bright gray bag.
light brown bags contain 1 light aqua bag, 4 vibrant yellow bags, 5 posh green bags.
muted yellow bags contain 4 drab bronze bags, 2 dull gray bags, 2 vibrant olive bags.
striped orange bags contain 4 mirrored brown bags, 4 plaid olive bags.
striped turquoise bags contain 2 muted indigo bags.
mirrored crimson bags contain no other bags.
muted teal bags contain 4 striped purple bags.
light purple bags contain 5 dim coral bags, 3 striped teal bags, 1 dim teal bag, 4 clear gold bags.
shiny olive bags contain 2 muted cyan bags, 1 wavy white bag, 4 light chartreuse bags, 4 mirrored indigo bags.
dim black bags contain 4 wavy lavender bags, 5 drab blue bags.
pale maroon bags contain 5 vibrant olive bags.
dotted maroon bags contain 2 faded lime bags, 1 clear turquoise bag.
light aqua bags contain 5 dark brown bags, 2 shiny gold bags.
vibrant beige bags contain 2 pale cyan bags, 2 dotted crimson bags, 2 vibrant teal bags.
striped white bags contain 2 shiny gold bags.
light orange bags contain 1 dim coral bag, 2 vibrant coral bags.
dull green bags contain 1 plaid green bag.
shiny turquoise bags contain 3 striped olive bags, 3 drab cyan bags, 2 dim lime bags, 3 muted brown bags.
posh chartreuse bags contain 2 mirrored indigo bags, 3 light black bags, 4 mirrored bronze bags, 3 light gold bags.
drab green bags contain no other bags.
drab plum bags contain 3 pale blue bags, 4 dull teal bags, 1 striped violet bag.
muted indigo bags contain 5 dark turquoise bags.
pale beige bags contain 4 wavy gray bags, 4 faded lime bags, 4 bright beige bags, 1 plaid violet bag.
vibrant blue bags contain 5 drab tan bags, 4 dull tomato bags.
vibrant turquoise bags contain 1 posh lavender bag.
clear maroon bags contain 2 shiny brown bags.
bright crimson bags contain 2 faded indigo bags, 4 dull teal bags.
wavy tan bags contain 5 dotted fuchsia bags, 2 muted fuchsia bags, 2 light white bags.
dim teal bags contain no other bags.
faded plum bags contain 1 faded cyan bag, 4 dull lime bags, 2 vibrant cyan bags.
vibrant orange bags contain 3 plaid salmon bags, 3 mirrored yellow bags.
posh gray bags contain 4 dark indigo bags, 3 light blue bags, 5 light red bags, 3 pale aqua bags.
dull aqua bags contain 1 dark fuchsia bag, 2 faded silver bags.
light bronze bags contain 1 muted indigo bag.
pale coral bags contain 1 plaid tomato bag, 4 faded plum bags, 2 mirrored turquoise bags, 1 posh lavender bag.
pale lime bags contain 1 dotted gray bag, 3 mirrored coral bags, 5 pale silver bags, 5 mirrored yellow bags.
shiny violet bags contain 2 faded orange bags, 3 muted indigo bags, 5 muted violet bags.
vibrant coral bags contain 3 posh fuchsia bags, 1 dim coral bag, 4 faded gold bags, 5 dotted violet bags.
dim magenta bags contain 3 pale purple bags, 4 plaid turquoise bags, 2 bright lavender bags.
dull brown bags contain 5 vibrant turquoise bags, 3 faded bronze bags, 3 striped tan bags, 4 dim gold bags.
bright brown bags contain 5 dark teal bags, 2 posh indigo bags, 4 drab fuchsia bags.
dim tan bags contain 4 dark magenta bags, 1 clear blue bag, 2 vibrant olive bags.
plaid black bags contain 4 pale indigo bags, 3 clear gold bags.
muted plum bags contain 3 faded brown bags, 5 drab orange bags, 5 dull turquoise bags.
light tomato bags contain 3 wavy coral bags, 2 drab green bags, 2 dull crimson bags.
dull cyan bags contain 1 clear blue bag, 3 clear yellow bags.
clear turquoise bags contain 5 pale black bags, 1 pale brown bag, 5 bright beige bags.
faded cyan bags contain 4 mirrored tan bags.
mirrored coral bags contain 4 pale gold bags, 5 wavy gold bags.
muted beige bags contain 4 faded crimson bags.
plaid tan bags contain 5 bright indigo bags.
faded gray bags contain 3 muted salmon bags, 3 shiny maroon bags, 3 faded orange bags.
vibrant teal bags contain 1 drab green bag, 1 pale black bag, 5 faded gold bags.
dim red bags contain 5 striped olive bags, 4 drab salmon bags, 4 dark magenta bags.
bright teal bags contain 4 drab green bags, 3 shiny brown bags, 1 mirrored crimson bag, 1 dotted purple bag.
wavy silver bags contain 4 wavy coral bags, 2 bright aqua bags.
drab gray bags contain 4 drab crimson bags, 4 faded turquoise bags, 3 drab lavender bags, 3 bright bronze bags.
vibrant gold bags contain 2 muted black bags.
faded brown bags contain 3 posh fuchsia bags, 3 faded tan bags, 1 dim olive bag.
pale red bags contain 4 clear orange bags, 3 striped green bags, 5 striped turquoise bags.
faded coral bags contain 3 dim lime bags, 2 pale indigo bags, 1 striped red bag.
muted cyan bags contain 5 vibrant aqua bags, 2 muted violet bags, 3 clear gold bags, 5 pale maroon bags.
bright silver bags contain 3 posh salmon bags, 1 posh silver bag, 1 clear teal bag, 3 dark gold bags.
vibrant magenta bags contain 2 dark violet bags.
mirrored magenta bags contain 4 drab teal bags, 1 dark purple bag.
posh orange bags contain 5 dim teal bags, 5 clear lavender bags, 2 dim coral bags, 3 shiny silver bags.
dotted gray bags contain 1 dim blue bag, 3 striped purple bags, 3 mirrored coral bags, 5 clear purple bags.
dark white bags contain 5 dim teal bags, 5 dark turquoise bags, 3 wavy indigo bags, 5 muted olive bags.
posh black bags contain 5 striped purple bags.
faded teal bags contain 2 muted beige bags, 4 dim blue bags, 3 light purple bags.
light maroon bags contain 5 dotted lime bags, 3 mirrored salmon bags, 2 dark coral bags, 4 muted violet bags.
striped violet bags contain 5 mirrored turquoise bags, 1 light green bag, 1 striped tan bag, 4 faded violet bags.
dotted red bags contain 4 bright aqua bags, 2 dull maroon bags, 3 plaid fuchsia bags, 2 dim green bags.
shiny salmon bags contain 4 pale chartreuse bags, 1 muted tomato bag.
posh beige bags contain 2 drab silver bags, 2 dull purple bags, 4 dotted magenta bags.
faded magenta bags contain 4 bright beige bags, 3 pale purple bags, 2 posh plum bags.
striped teal bags contain 2 light tomato bags.
shiny beige bags contain 3 posh turquoise bags, 4 drab lavender bags, 3 dark magenta bags, 5 dotted teal bags.
dull violet bags contain 2 dull teal bags, 4 shiny fuchsia bags, 2 bright coral bags, 1 pale bronze bag.
light fuchsia bags contain 2 pale crimson bags, 4 bright blue bags.
faded tomato bags contain 4 bright fuchsia bags, 3 posh tan bags, 3 dark blue bags.
posh tan bags contain 2 bright aqua bags, 1 mirrored purple bag, 2 clear lavender bags.
dim cyan bags contain 2 dim beige bags, 2 dotted orange bags, 4 muted orange bags, 3 light plum bags.
plaid chartreuse bags contain 2 light orange bags, 5 dim fuchsia bags, 2 mirrored gray bags, 4 mirrored tan bags.
drab silver bags contain 2 dim blue bags, 2 light lavender bags, 3 wavy tomato bags.
dull lavender bags contain 5 dull magenta bags, 3 dotted tomato bags, 5 faded gold bags, 2 pale tan bags.
drab cyan bags contain 3 pale plum bags.
faded green bags contain 3 light violet bags, 3 pale maroon bags.
plaid red bags contain 1 clear olive bag.
drab tomato bags contain 3 faded yellow bags, 5 striped fuchsia bags, 4 light gold bags, 1 faded bronze bag.
dotted black bags contain 5 vibrant aqua bags.
striped fuchsia bags contain 1 muted olive bag, 2 pale black bags, 2 light teal bags.
light coral bags contain 4 striped fuchsia bags.
posh coral bags contain 5 faded tan bags.
faded blue bags contain 3 dim chartreuse bags, 5 bright coral bags.
plaid green bags contain 5 striped plum bags, 3 faded turquoise bags, 5 bright beige bags.
dark cyan bags contain 2 bright turquoise bags, 4 plaid plum bags, 1 shiny green bag.
posh yellow bags contain 3 pale yellow bags, 3 muted purple bags, 1 light magenta bag, 4 light aqua bags.
mirrored turquoise bags contain 2 vibrant gray bags, 4 drab orange bags.
faded tan bags contain 2 drab orange bags.
posh plum bags contain 1 light gold bag, 4 posh orange bags.
muted chartreuse bags contain 2 shiny violet bags, 1 pale plum bag, 1 dim crimson bag.
muted white bags contain 5 pale brown bags, 5 clear lavender bags, 1 faded bronze bag.
pale plum bags contain 5 dotted violet bags, 5 mirrored gray bags.
plaid tomato bags contain 4 vibrant coral bags, 4 dim aqua bags.
vibrant cyan bags contain 1 shiny lavender bag, 3 faded orange bags, 2 striped fuchsia bags.
shiny silver bags contain no other bags.
vibrant aqua bags contain 3 plaid orange bags, 2 light blue bags.
dotted purple bags contain 4 faded gold bags, 4 clear green bags, 1 mirrored gray bag.
light chartreuse bags contain 1 bright black bag.
mirrored aqua bags contain 4 drab salmon bags, 2 striped red bags.
faded lime bags contain 4 faded gold bags.
clear indigo bags contain 3 dim tomato bags, 1 light purple bag, 3 dark salmon bags, 5 posh teal bags.
wavy violet bags contain 2 posh turquoise bags.
light blue bags contain 1 vibrant turquoise bag, 5 vibrant salmon bags, 2 light gold bags.
clear blue bags contain 4 bright aqua bags.
bright coral bags contain 1 light lavender bag.
light green bags contain 2 pale silver bags, 3 light lavender bags, 2 posh silver bags.
dark lime bags contain 4 muted crimson bags, 3 mirrored teal bags.
mirrored red bags contain 5 dotted cyan bags, 2 dull brown bags.
drab purple bags contain 2 bright salmon bags, 1 striped plum bag.
posh red bags contain 5 drab salmon bags, 5 posh gold bags, 1 vibrant crimson bag, 5 vibrant yellow bags.
clear red bags contain 5 muted white bags, 3 mirrored gold bags, 1 plaid blue bag.
drab fuchsia bags contain 4 drab salmon bags, 5 dark red bags.
dark tomato bags contain 4 plaid orange bags, 1 dull magenta bag, 5 striped aqua bags.
muted crimson bags contain 2 vibrant gray bags.
muted bronze bags contain 4 plaid beige bags, 3 striped red bags, 3 muted olive bags, 5 dotted indigo bags.
faded bronze bags contain 5 dim teal bags.
striped maroon bags contain 5 bright gold bags, 2 clear brown bags, 3 vibrant cyan bags, 4 dotted coral bags.
light cyan bags contain 5 mirrored turquoise bags.
mirrored cyan bags contain 1 muted olive bag, 2 drab salmon bags, 5 muted indigo bags.
plaid lavender bags contain 1 light teal bag, 4 dotted indigo bags, 5 faded green bags, 1 light aqua bag.
light gray bags contain 5 mirrored red bags, 1 plaid magenta bag, 1 pale black bag, 3 clear blue bags.
wavy indigo bags contain 3 dim teal bags, 2 shiny gold bags, 4 dim gold bags.
bright tomato bags contain 1 mirrored gray bag.
pale blue bags contain 3 vibrant teal bags, 3 muted magenta bags, 1 mirrored salmon bag, 4 striped cyan bags.
posh bronze bags contain 1 bright tomato bag, 2 bright teal bags, 1 dark magenta bag, 1 mirrored silver bag.
dotted violet bags contain 4 shiny silver bags, 5 drab green bags.
dark black bags contain 3 vibrant gray bags, 1 faded bronze bag, 2 faded gold bags.
muted gray bags contain 3 wavy orange bags, 2 mirrored tan bags.
shiny chartreuse bags contain 5 bright turquoise bags.
dull red bags contain 5 wavy olive bags.
dark orange bags contain 3 plaid bronze bags, 1 dark blue bag, 1 dim gray bag, 4 pale chartreuse bags.
wavy yellow bags contain 3 plaid indigo bags, 3 dull purple bags.
drab salmon bags contain 3 light blue bags, 2 dull brown bags.
drab beige bags contain 2 pale turquoise bags, 5 light gold bags, 4 bright aqua bags.
dull olive bags contain 2 pale chartreuse bags, 3 dull cyan bags, 5 mirrored teal bags.
wavy crimson bags contain 4 faded orange bags, 5 bright gray bags.
muted maroon bags contain 1 muted salmon bag.
mirrored white bags contain 2 light bronze bags, 3 clear brown bags, 2 dark turquoise bags, 2 dim indigo bags.
wavy gray bags contain 1 light lime bag, 1 faded turquoise bag, 4 muted teal bags, 2 dim crimson bags.
shiny gray bags contain 4 dull cyan bags, 2 clear red bags, 5 dark magenta bags.
dull orange bags contain 1 dull indigo bag.
drab maroon bags contain 5 light red bags, 1 pale orange bag.
shiny blue bags contain 3 striped magenta bags, 2 light violet bags.
pale lavender bags contain 5 faded tan bags, 4 light tan bags, 1 striped coral bag.
wavy magenta bags contain 4 dull crimson bags.
bright maroon bags contain 5 shiny salmon bags.
bright gray bags contain 5 light blue bags, 3 faded gold bags, 5 dull tomato bags, 2 faded silver bags.
muted aqua bags contain 4 mirrored purple bags, 5 light blue bags.
dull plum bags contain 3 shiny purple bags, 3 dim indigo bags.
light indigo bags contain 2 mirrored bronze bags, 4 wavy lime bags.
shiny crimson bags contain 4 dull violet bags.
dull lime bags contain 1 mirrored gray bag, 1 shiny brown bag, 5 dark fuchsia bags, 4 shiny silver bags.
drab chartreuse bags contain 4 muted purple bags, 5 bright magenta bags, 4 dark salmon bags, 3 pale indigo bags.
faded aqua bags contain 1 dotted white bag, 1 drab olive bag.
dotted magenta bags contain 5 wavy tomato bags, 2 striped fuchsia bags, 3 dull blue bags, 3 pale yellow bags.
bright tan bags contain 1 dark blue bag, 5 vibrant turquoise bags.
dotted fuchsia bags contain 2 dull silver bags.
dotted plum bags contain 1 wavy coral bag, 3 vibrant teal bags, 2 dim bronze bags, 1 pale salmon bag.
clear silver bags contain 1 faded orange bag.
faded beige bags contain 4 mirrored orange bags, 4 plaid white bags, 1 muted violet bag.
bright red bags contain 5 posh fuchsia bags, 4 striped cyan bags.
faded yellow bags contain 3 vibrant turquoise bags, 5 bright teal bags, 3 pale white bags.
light red bags contain 4 shiny bronze bags, 2 mirrored gray bags, 5 dark violet bags.
dim orange bags contain 3 dull brown bags, 3 dim turquoise bags, 3 bright chartreuse bags, 1 dark red bag.
posh salmon bags contain 2 faded lavender bags, 1 faded brown bag.
shiny white bags contain 5 clear crimson bags.
shiny black bags contain 2 vibrant coral bags, 2 wavy indigo bags, 4 shiny gold bags, 4 mirrored tan bags.
pale turquoise bags contain 2 dim olive bags.
vibrant maroon bags contain 3 striped turquoise bags, 3 clear magenta bags, 1 dark fuchsia bag, 5 dark brown bags.
wavy coral bags contain 5 vibrant aqua bags.
striped green bags contain 3 light magenta bags, 4 clear magenta bags, 1 light turquoise bag.
dark gray bags contain 3 dark aqua bags.
shiny bronze bags contain 5 vibrant turquoise bags, 1 faded gold bag, 5 dark fuchsia bags, 3 dotted cyan bags.
dull teal bags contain 3 clear cyan bags, 1 bright chartreuse bag, 2 dull gray bags.
dark fuchsia bags contain 1 dull silver bag, 3 dim gold bags, 1 clear teal bag, 5 shiny silver bags.
posh blue bags contain 4 drab teal bags.
drab violet bags contain 3 dim lavender bags, 1 vibrant aqua bag, 4 muted gray bags.
wavy white bags contain 1 dark coral bag, 2 dotted gray bags.
muted coral bags contain 1 vibrant teal bag, 5 dim tan bags, 4 light bronze bags.
dark silver bags contain 5 shiny purple bags, 2 posh indigo bags.
pale bronze bags contain 3 striped white bags, 1 pale aqua bag.
dotted turquoise bags contain 2 dark bronze bags, 2 mirrored turquoise bags, 2 clear yellow bags, 2 posh orange bags.
muted turquoise bags contain 1 faded blue bag, 1 dotted crimson bag.
faded indigo bags contain 5 mirrored blue bags, 3 striped crimson bags, 3 light green bags.
bright salmon bags contain 4 dim gold bags, 1 mirrored red bag.
mirrored violet bags contain 5 dull tomato bags, 4 shiny maroon bags, 3 dark green bags.
plaid blue bags contain 1 bright aqua bag, 1 light gold bag, 4 mirrored turquoise bags, 1 dim gold bag.
wavy green bags contain 5 light white bags.
vibrant gray bags contain 3 drab green bags, 5 faded bronze bags.
vibrant red bags contain 3 shiny lavender bags, 5 light indigo bags, 3 dotted chartreuse bags, 3 bright aqua bags.
dotted tomato bags contain 4 clear green bags, 3 vibrant gray bags.
mirrored teal bags contain 4 posh orange bags, 3 shiny gold bags, 4 clear magenta bags, 4 dull crimson bags.
muted brown bags contain 5 dotted cyan bags, 1 pale turquoise bag, 2 posh olive bags, 2 light black bags.
wavy beige bags contain 1 dull tomato bag.
pale white bags contain 4 drab orange bags, 3 dim tomato bags, 2 clear lavender bags.
mirrored purple bags contain 4 dim lime bags.
plaid orange bags contain 1 faded bronze bag, 3 dotted violet bags.
shiny lime bags contain 4 light green bags, 4 dim orange bags.
vibrant tomato bags contain 4 pale plum bags, 2 pale maroon bags.
bright turquoise bags contain 2 dim gold bags, 5 pale maroon bags, 5 dull tomato bags, 1 mirrored beige bag.
bright lime bags contain 4 clear beige bags.
shiny tan bags contain 4 bright beige bags.
mirrored green bags contain 5 wavy magenta bags.
posh green bags contain 1 plaid aqua bag.
vibrant olive bags contain 4 striped plum bags.
vibrant crimson bags contain 1 dim lavender bag, 5 faded bronze bags, 1 drab green bag, 3 plaid magenta bags.
bright gold bags contain 5 dark purple bags, 4 dark teal bags, 2 dark white bags, 4 striped white bags.
wavy chartreuse bags contain 1 wavy gray bag, 2 faded olive bags.
muted black bags contain 4 shiny gold bags, 4 vibrant turquoise bags, 5 shiny magenta bags.
pale salmon bags contain 4 dotted gray bags, 4 dim turquoise bags, 2 clear silver bags.
drab coral bags contain 3 shiny lime bags, 4 light green bags, 4 vibrant olive bags, 5 bright chartreuse bags.
pale green bags contain 2 clear green bags, 5 shiny green bags.
striped tan bags contain no other bags.
pale silver bags contain 4 shiny silver bags, 5 clear gold bags, 1 dim lime bag.
pale olive bags contain 3 muted fuchsia bags, 3 shiny tan bags.
bright cyan bags contain 4 clear lavender bags, 4 drab green bags, 1 striped tan bag.
drab teal bags contain 3 drab tan bags, 1 shiny bronze bag, 1 light green bag.
muted salmon bags contain 2 plaid fuchsia bags, 1 shiny purple bag, 4 wavy silver bags, 2 dark salmon bags.
shiny tomato bags contain 1 wavy turquoise bag, 2 pale gray bags, 5 bright red bags, 1 dotted aqua bag.
clear cyan bags contain 3 posh tan bags, 4 dim green bags, 3 plaid blue bags.
dim lavender bags contain 5 light coral bags, 5 dull indigo bags.
light gold bags contain 4 faded gold bags, 3 pale plum bags, 2 clear lavender bags, 1 dim lime bag.
muted tan bags contain 2 plaid gray bags, 3 muted fuchsia bags, 4 posh gray bags, 2 mirrored brown bags.
striped olive bags contain 2 dim lime bags, 3 dull tomato bags.
dim green bags contain 4 light blue bags, 5 dark turquoise bags.
pale tan bags contain 1 light indigo bag, 5 shiny chartreuse bags, 5 dim purple bags, 2 faded gold bags.
bright orange bags contain 5 striped white bags, 2 dim lime bags.
faded orange bags contain 1 plaid magenta bag, 2 striped olive bags.
posh olive bags contain 2 bright gold bags, 2 dull tomato bags.
dotted silver bags contain 1 wavy crimson bag, 5 light green bags, 5 striped plum bags, 5 shiny silver bags.
wavy turquoise bags contain 5 striped purple bags, 3 shiny blue bags, 3 dotted indigo bags.
plaid gray bags contain 1 posh teal bag.
dark magenta bags contain 3 striped olive bags.
drab magenta bags contain 2 drab plum bags.
bright beige bags contain 1 clear teal bag, 2 dotted violet bags, 5 posh lavender bags, 5 faded bronze bags.
dark tan bags contain 2 posh indigo bags, 3 light green bags.
muted blue bags contain 1 shiny fuchsia bag, 5 dark white bags, 2 dull brown bags.
wavy fuchsia bags contain 2 dotted salmon bags, 3 striped gray bags, 1 clear silver bag, 3 shiny maroon bags.
light plum bags contain 2 posh chartreuse bags, 3 pale orange bags, 1 striped violet bag.
dull indigo bags contain 3 dim coral bags, 5 bright orange bags, 5 shiny purple bags, 1 posh fuchsia bag.
light olive bags contain 2 clear plum bags, 2 dotted indigo bags, 1 vibrant maroon bag, 2 drab crimson bags.
pale magenta bags contain 4 plaid bronze bags, 4 muted aqua bags.
faded white bags contain 1 shiny salmon bag, 4 drab fuchsia bags, 3 striped cyan bags, 5 light bronze bags.
bright bronze bags contain 2 bright gold bags.
dim chartreuse bags contain 5 wavy beige bags, 1 mirrored silver bag, 5 pale brown bags, 4 pale silver bags.
bright black bags contain 1 light teal bag, 3 shiny bronze bags, 5 faded beige bags, 3 striped magenta bags.
clear coral bags contain 1 pale gold bag, 3 dim tomato bags, 3 dark violet bags, 2 dim purple bags.
dark indigo bags contain 1 vibrant turquoise bag.
mirrored tomato bags contain 1 drab yellow bag, 5 muted olive bags, 3 wavy black bags, 3 dotted lavender bags.
striped plum bags contain 3 faded bronze bags, 1 vibrant gray bag.
bright purple bags contain 3 muted gray bags.
muted gold bags contain 4 light indigo bags, 4 bright tomato bags, 3 striped tomato bags.
faded lavender bags contain 5 wavy indigo bags.
striped beige bags contain 2 striped fuchsia bags, 5 mirrored magenta bags.
faded fuchsia bags contain 1 mirrored fuchsia bag, 1 posh aqua bag, 4 clear coral bags, 3 mirrored magenta bags.
bright aqua bags contain 1 muted white bag.
pale crimson bags contain 3 light purple bags.
wavy aqua bags contain 2 shiny purple bags, 1 posh orange bag, 1 dark red bag, 3 drab lavender bags.
posh violet bags contain 4 wavy teal bags, 4 dull chartreuse bags, 4 wavy salmon bags.
light salmon bags contain 4 mirrored crimson bags, 1 striped red bag.
dim beige bags contain 4 dim turquoise bags, 4 drab fuchsia bags, 2 vibrant tomato bags.
clear yellow bags contain 5 mirrored orange bags, 3 dark teal bags.
pale fuchsia bags contain 1 mirrored bronze bag, 3 posh lavender bags, 2 shiny lavender bags.
posh gold bags contain 1 bright teal bag, 3 light green bags, 4 muted blue bags, 4 drab green bags.
striped aqua bags contain 4 wavy beige bags, 1 dotted indigo bag.
plaid indigo bags contain 5 striped green bags, 5 light purple bags.
muted tomato bags contain 2 posh tomato bags, 1 pale coral bag, 3 muted white bags, 5 dim orange bags.
mirrored silver bags contain 1 faded silver bag, 2 dull bronze bags, 2 dim red bags, 5 posh brown bags.
vibrant silver bags contain 5 striped olive bags, 2 clear olive bags, 5 bright coral bags, 1 plaid fuchsia bag.
dull chartreuse bags contain 1 muted aqua bag.
dotted green bags contain 4 vibrant white bags, 1 dim teal bag.
plaid cyan bags contain 4 faded bronze bags.
dark crimson bags contain 5 shiny purple bags, 1 dotted brown bag, 4 clear coral bags, 1 striped lavender bag.
muted orange bags contain 2 drab cyan bags.
dark salmon bags contain 1 pale black bag, 2 dull brown bags.
striped salmon bags contain 3 dim aqua bags, 1 clear purple bag, 3 muted crimson bags.
vibrant lavender bags contain 3 posh orange bags, 4 posh indigo bags, 3 plaid magenta bags.
shiny purple bags contain 2 dotted violet bags, 5 striped tan bags, 2 pale plum bags.
plaid lime bags contain 4 wavy salmon bags, 2 light magenta bags, 5 striped crimson bags.
drab blue bags contain 3 dull orange bags.
mirrored salmon bags contain 4 pale black bags, 3 posh tan bags, 4 vibrant violet bags.
muted red bags contain 3 light coral bags, 5 muted teal bags, 1 dark blue bag.
dotted olive bags contain 2 clear lime bags, 3 faded bronze bags, 5 shiny silver bags, 4 clear magenta bags.
shiny cyan bags contain 3 light green bags, 2 dull maroon bags, 1 pale aqua bag, 5 mirrored gold bags.
plaid teal bags contain 1 dull teal bag, 5 faded white bags, 4 vibrant purple bags.
dull silver bags contain 2 bright cyan bags, 3 clear green bags.
dim silver bags contain 5 dim orange bags.
dark red bags contain 1 shiny fuchsia bag, 5 drab orange bags, 1 dull lime bag.
dull gray bags contain 5 posh tan bags, 5 mirrored violet bags, 5 clear gold bags, 3 striped olive bags.
mirrored olive bags contain 3 mirrored yellow bags, 4 striped lavender bags.
dull gold bags contain 3 faded beige bags.
dim olive bags contain 5 dull maroon bags, 3 dull beige bags.
dark blue bags contain 4 dim lime bags, 4 muted indigo bags.
posh silver bags contain 3 posh lavender bags, 2 bright beige bags, 5 dim coral bags.
dotted indigo bags contain 2 pale gray bags, 4 light violet bags.
plaid turquoise bags contain 1 pale purple bag, 1 pale brown bag.
striped purple bags contain 2 shiny brown bags.
dull turquoise bags contain 3 dull teal bags, 3 dark coral bags, 4 dull silver bags, 4 faded violet bags.
plaid olive bags contain 3 dim green bags, 1 wavy chartreuse bag, 5 pale crimson bags, 5 mirrored green bags.
clear tomato bags contain 1 posh gray bag, 4 clear teal bags, 2 drab indigo bags.
dull tan bags contain 4 vibrant brown bags, 3 drab orange bags, 5 bright maroon bags.
dark gold bags contain 3 light teal bags.
faded olive bags contain 4 clear gold bags.
posh fuchsia bags contain 2 dull brown bags, 1 dull silver bag, 3 mirrored tan bags, 2 dim coral bags.
dull purple bags contain 1 dim turquoise bag, 3 plaid coral bags, 5 striped lime bags.
bright plum bags contain 3 pale green bags, 2 pale gray bags.
faded violet bags contain 2 plaid white bags.
striped lime bags contain 2 striped cyan bags.
clear lime bags contain 3 faded orange bags, 1 posh lavender bag, 3 dull lime bags.
drab red bags contain 2 dim gold bags, 4 dotted violet bags.
mirrored orange bags contain 2 mirrored gold bags, 1 dull gray bag.
drab white bags contain 1 vibrant silver bag, 4 bright cyan bags, 5 shiny red bags.
wavy red bags contain 4 plaid fuchsia bags, 2 pale maroon bags, 4 wavy indigo bags.
pale gray bags contain 2 dim gold bags.
mirrored blue bags contain 4 dull purple bags, 4 dull bronze bags, 5 faded violet bags.
pale aqua bags contain 2 shiny brown bags, 2 vibrant coral bags, 3 dark green bags, 5 bright beige bags.
plaid violet bags contain 2 dim tomato bags, 5 dim teal bags, 4 drab salmon bags.
shiny magenta bags contain 5 dull beige bags.
striped magenta bags contain 2 drab olive bags, 3 dim chartreuse bags, 3 plaid beige bags, 5 mirrored crimson bags.
faded black bags contain 1 bright coral bag, 1 mirrored magenta bag, 1 pale coral bag, 4 posh bronze bags.
plaid yellow bags contain 2 vibrant gray bags, 1 mirrored cyan bag, 2 light salmon bags, 1 light gold bag.
vibrant brown bags contain 3 light tomato bags, 1 dark red bag.
faded salmon bags contain 2 mirrored orange bags.
light beige bags contain 1 vibrant violet bag, 4 dim green bags, 2 bright aqua bags.
bright magenta bags contain 4 mirrored brown bags, 4 faded white bags.
clear violet bags contain 5 mirrored gold bags, 5 bright lavender bags, 5 dim red bags.
striped blue bags contain 3 clear turquoise bags.
mirrored fuchsia bags contain 2 shiny fuchsia bags, 5 faded bronze bags, 3 dull lime bags, 3 drab purple bags.
drab lime bags contain 4 pale yellow bags, 3 drab salmon bags, 5 clear beige bags.
wavy maroon bags contain 3 drab cyan bags, 5 light turquoise bags.
striped coral bags contain 4 dull magenta bags, 5 posh maroon bags, 3 shiny red bags.
wavy lime bags contain 1 plaid green bag.
dim maroon bags contain 4 faded lime bags, 1 striped olive bag, 4 dull bronze bags.
clear orange bags contain 5 posh bronze bags, 4 striped tan bags, 4 mirrored bronze bags.
vibrant plum bags contain 1 bright white bag, 5 light aqua bags, 4 mirrored fuchsia bags, 2 vibrant purple bags.
drab bronze bags contain 3 light gray bags, 2 faded orange bags, 4 pale black bags.
mirrored brown bags contain 1 light blue bag, 2 posh olive bags, 5 vibrant magenta bags.
mirrored gray bags contain no other bags.
dim aqua bags contain 3 bright tomato bags.
clear aqua bags contain 3 drab indigo bags, 4 light bronze bags, 5 drab teal bags.
dull yellow bags contain 4 dim magenta bags.
shiny indigo bags contain 4 wavy red bags, 3 dim salmon bags, 5 dull brown bags.
striped crimson bags contain 1 light green bag, 2 dull blue bags, 5 dotted olive bags.
mirrored yellow bags contain 4 dull maroon bags, 4 mirrored aqua bags.
drab aqua bags contain 1 drab teal bag, 1 dull gold bag, 4 dim red bags, 3 dim teal bags.
clear green bags contain no other bags.
plaid bronze bags contain 2 vibrant olive bags, 5 wavy green bags, 4 muted indigo bags.
mirrored black bags contain 2 striped red bags, 2 drab tomato bags, 4 shiny maroon bags, 1 striped gray bag.
muted purple bags contain 4 mirrored maroon bags, 2 dull white bags, 3 plaid salmon bags, 1 faded lime bag.
drab brown bags contain 3 vibrant maroon bags, 5 posh indigo bags, 2 light silver bags, 5 bright black bags.
light yellow bags contain 2 posh olive bags, 4 light turquoise bags.
mirrored indigo bags contain 5 pale maroon bags.
drab black bags contain 3 mirrored green bags.
shiny teal bags contain 2 dim plum bags, 4 dotted indigo bags, 1 mirrored lime bag, 2 light green bags.
striped chartreuse bags contain 2 dim olive bags, 1 wavy teal bag.
drab indigo bags contain 3 dull blue bags, 3 bright indigo bags, 1 dull purple bag.
faded crimson bags contain 2 vibrant gray bags.
posh white bags contain 2 shiny violet bags.
wavy purple bags contain 3 drab green bags, 5 muted purple bags, 1 dull teal bag.
dotted lime bags contain 2 drab teal bags.
dotted orange bags contain 2 bright salmon bags.
posh magenta bags contain 3 dotted silver bags.
striped yellow bags contain 1 dull chartreuse bag, 3 dark maroon bags, 3 faded orange bags, 3 shiny salmon bags.
dotted yellow bags contain 1 dim gold bag, 4 clear brown bags.
drab orange bags contain 2 mirrored crimson bags, 2 clear lavender bags, 3 faded bronze bags, 4 posh lavender bags.
dull coral bags contain 5 dim teal bags, 1 light gold bag, 2 drab lime bags, 3 faded lavender bags.
clear gold bags contain 5 drab green bags, 4 dim coral bags, 3 dim teal bags.
dim purple bags contain 4 bright aqua bags, 5 posh lavender bags, 5 vibrant salmon bags, 3 pale black bags.
clear salmon bags contain 1 vibrant orange bag, 1 faded orange bag, 5 dull tomato bags, 3 mirrored turquoise bags.
clear brown bags contain 5 faded bronze bags.
faded maroon bags contain 2 dotted blue bags, 4 plaid fuchsia bags, 5 light purple bags, 4 faded purple bags.
shiny red bags contain 5 mirrored crimson bags, 2 mirrored cyan bags, 2 dark bronze bags, 1 pale plum bag.
faded red bags contain 5 dotted yellow bags, 2 faded bronze bags.
dotted gold bags contain 4 vibrant coral bags, 1 clear turquoise bag.
wavy olive bags contain 5 light violet bags, 5 bright white bags, 3 vibrant chartreuse bags, 1 mirrored silver bag.
dark brown bags contain 3 muted blue bags.
dim salmon bags contain 1 dull silver bag, 5 vibrant purple bags.
dark chartreuse bags contain 3 dull beige bags, 3 light tan bags, 3 vibrant cyan bags.
clear tan bags contain 2 vibrant coral bags.
clear white bags contain 3 faded bronze bags.
dim yellow bags contain 4 plaid coral bags, 1 posh tomato bag, 4 striped turquoise bags.
bright indigo bags contain 4 dull magenta bags.
plaid gold bags contain 2 dark violet bags, 3 shiny blue bags, 5 drab indigo bags, 3 shiny orange bags.
dark aqua bags contain 3 faded indigo bags, 1 pale brown bag, 5 muted chartreuse bags.
light lime bags contain 5 muted teal bags.
dark teal bags contain 1 dark olive bag, 1 drab lavender bag, 2 mirrored purple bags, 1 pale teal bag.
faded turquoise bags contain 1 posh fuchsia bag.
mirrored plum bags contain 2 striped chartreuse bags.
clear beige bags contain 1 plaid blue bag, 5 faded gold bags, 5 mirrored crimson bags, 3 drab lavender bags.
dotted salmon bags contain 1 clear tan bag, 5 mirrored gold bags, 5 faded beige bags.
posh lavender bags contain 2 mirrored gray bags, 3 clear green bags, 5 dim teal bags.
bright white bags contain 1 posh gold bag, 5 mirrored silver bags.
dotted cyan bags contain 5 mirrored gray bags, 2 dull silver bags.
dim tomato bags contain 5 dull brown bags.
dotted brown bags contain 2 pale turquoise bags.
posh cyan bags contain 5 dim coral bags.
muted magenta bags contain 3 posh olive bags, 2 dark indigo bags.
pale purple bags contain 5 dotted cyan bags.
vibrant salmon bags contain 3 dim teal bags, 1 posh lavender bag, 2 pale purple bags, 4 plaid magenta bags.
mirrored gold bags contain 5 dark indigo bags, 4 mirrored indigo bags, 4 dotted cyan bags.
striped brown bags contain 5 posh tomato bags, 5 posh brown bags, 2 faded turquoise bags.
dark bronze bags contain 5 bright chartreuse bags, 4 striped teal bags, 2 muted black bags, 2 muted olive bags.
light silver bags contain 1 plaid cyan bag, 2 vibrant coral bags, 1 muted gray bag, 4 striped purple bags.
pale violet bags contain 3 posh teal bags, 1 dull beige bag, 2 plaid beige bags.
clear teal bags contain no other bags.
posh lime bags contain 1 dark coral bag, 5 bright teal bags, 5 bright cyan bags, 3 posh fuchsia bags.
light crimson bags contain 4 dotted beige bags.
muted lavender bags contain 1 light fuchsia bag, 2 shiny crimson bags, 2 drab purple bags.
plaid crimson bags contain 4 mirrored red bags, 1 posh chartreuse bag, 2 muted indigo bags, 1 plaid lavender bag.
light magenta bags contain 3 drab bronze bags, 1 dull olive bag.
bright yellow bags contain 4 plaid plum bags.
vibrant bronze bags contain 5 dotted red bags, 4 wavy silver bags, 4 dim lime bags.
bright blue bags contain 1 clear white bag, 1 wavy aqua bag.
dim turquoise bags contain 5 pale purple bags, 1 vibrant magenta bag, 1 vibrant turquoise bag, 3 shiny brown bags.
vibrant chartreuse bags contain 4 muted blue bags, 3 pale indigo bags, 1 dark black bag, 1 bright tomato bag.
light tan bags contain 2 mirrored beige bags, 5 dull turquoise bags.
wavy salmon bags contain 5 dull crimson bags, 2 bright salmon bags, 3 muted gold bags.
mirrored maroon bags contain 3 shiny brown bags, 1 shiny fuchsia bag.
pale teal bags contain 1 striped purple bag, 3 drab orange bags, 1 dull silver bag, 1 dark fuchsia bag.
striped tomato bags contain 2 dim orange bags.
light teal bags contain 1 dull tomato bag, 5 clear gold bags.
dark olive bags contain 5 striped white bags, 5 dim red bags.
wavy orange bags contain 1 clear green bag.
plaid brown bags contain 5 dull olive bags, 3 wavy gray bags, 1 dotted violet bag.
dim white bags contain 3 clear tan bags, 1 clear yellow bag, 1 pale crimson bag.
vibrant black bags contain 2 dim fuchsia bags, 1 dull lime bag, 3 faded magenta bags.
dotted aqua bags contain 4 posh tomato bags, 4 pale plum bags, 1 posh coral bag.
striped gold bags contain 5 dim orange bags, 2 drab green bags, 2 striped red bags, 3 muted violet bags.
dim gold bags contain 3 posh lavender bags, 4 drab green bags, 4 dim teal bags.
mirrored bronze bags contain 4 pale aqua bags, 4 faded orange bags.
mirrored beige bags contain 3 wavy gold bags.
dotted teal bags contain 3 dull indigo bags, 4 mirrored gold bags.
dark maroon bags contain 4 dark turquoise bags, 5 mirrored silver bags, 4 wavy bronze bags, 4 clear turquoise bags.
wavy teal bags contain 3 plaid fuchsia bags, 2 drab orange bags, 3 vibrant teal bags.
muted lime bags contain 5 bright blue bags, 3 light salmon bags.
plaid plum bags contain 2 wavy gold bags, 2 pale aqua bags.
striped indigo bags contain 5 faded black bags.
clear lavender bags contain no other bags.
dull tomato bags contain 3 dim lime bags.
shiny yellow bags contain 5 faded coral bags, 2 striped violet bags, 5 clear orange bags.
light lavender bags contain 5 dull maroon bags.
pale chartreuse bags contain 4 clear green bags.
posh turquoise bags contain 2 striped olive bags, 5 bright chartreuse bags.
bright lavender bags contain 4 dark white bags, 5 wavy coral bags.
shiny lavender bags contain 2 wavy lime bags, 2 wavy bronze bags.
light black bags contain 4 pale black bags, 4 pale purple bags, 5 drab lavender bags.
plaid maroon bags contain 1 clear teal bag, 3 clear lavender bags, 1 dim chartreuse bag, 2 vibrant teal bags.
striped cyan bags contain 4 posh fuchsia bags, 4 faded violet bags.
striped lavender bags contain 4 pale yellow bags.
posh purple bags contain 4 light tomato bags, 4 muted crimson bags.
dim crimson bags contain 5 wavy aqua bags, 2 pale maroon bags, 4 faded bronze bags.
muted fuchsia bags contain 2 striped gray bags, 4 posh gray bags, 5 clear white bags, 5 dotted orange bags.
drab turquoise bags contain 2 bright indigo bags.
vibrant purple bags contain 3 mirrored bronze bags.
bright olive bags contain 4 light bronze bags.
shiny plum bags contain 1 muted indigo bag, 2 light lime bags, 4 clear coral bags, 2 dim lavender bags.
dotted beige bags contain 1 plaid plum bag, 3 dark coral bags, 2 wavy indigo bags.
striped black bags contain 3 muted gold bags.
shiny orange bags contain 2 faded blue bags, 3 shiny green bags.
shiny coral bags contain 1 posh orange bag, 4 striped red bags, 4 muted yellow bags, 2 dull lavender bags.
vibrant yellow bags contain 2 dull fuchsia bags, 2 pale coral bags, 1 muted magenta bag, 3 drab turquoise bags.
shiny gold bags contain 5 mirrored crimson bags, 5 mirrored tan bags, 5 drab green bags, 5 shiny silver bags.
bright violet bags contain 2 vibrant white bags, 1 clear green bag, 3 faded yellow bags.
mirrored tan bags contain 2 dark black bags, 4 clear green bags, 2 bright cyan bags, 4 faded bronze bags.
posh indigo bags contain 4 pale plum bags, 1 dark brown bag, 4 dull crimson bags.
pale indigo bags contain 3 posh plum bags.
drab tan bags contain 1 light gray bag, 5 drab orange bags.
muted green bags contain 1 dotted violet bag, 4 dotted cyan bags.
light turquoise bags contain 2 posh brown bags.
dotted blue bags contain 1 posh purple bag, 4 plaid gray bags.
plaid silver bags contain 3 wavy maroon bags, 5 clear black bags, 5 shiny indigo bags, 2 striped gold bags.
dim plum bags contain 3 dotted fuchsia bags, 5 clear yellow bags, 4 striped red bags.
dotted chartreuse bags contain 1 clear yellow bag, 1 dim red bag, 4 dim turquoise bags, 1 mirrored orange bag.
dim blue bags contain 3 bright turquoise bags, 3 light blue bags, 2 wavy red bags.
faded gold bags contain 3 mirrored gray bags, 4 dotted cyan bags, 5 mirrored red bags, 4 shiny silver bags.
dull fuchsia bags contain 1 plaid cyan bag, 3 shiny purple bags.
dotted coral bags contain 5 muted aqua bags, 1 dark orange bag, 3 bright black bags.
striped bronze bags contain 5 mirrored purple bags, 2 wavy aqua bags.
faded silver bags contain 1 bright salmon bag, 1 striped olive bag, 3 vibrant turquoise bags.
vibrant white bags contain 5 muted crimson bags, 4 bright orange bags, 2 dark brown bags.
shiny fuchsia bags contain 2 dull silver bags, 1 wavy indigo bag, 5 shiny gold bags, 2 bright beige bags.
drab crimson bags contain 3 mirrored purple bags, 2 clear magenta bags, 3 light indigo bags, 5 clear brown bags.
posh aqua bags contain 2 dark brown bags, 2 shiny white bags.
pale tomato bags contain 5 posh gray bags, 1 wavy silver bag, 5 drab crimson bags, 2 dotted aqua bags.
wavy bronze bags contain 5 plaid coral bags.
dull white bags contain 4 shiny magenta bags, 1 clear olive bag, 5 posh silver bags, 4 dark blue bags.
shiny aqua bags contain 5 drab green bags, 4 dark maroon bags, 3 striped turquoise bags, 4 pale white bags.
wavy brown bags contain 1 faded gold bag, 2 bright aqua bags, 5 dotted tan bags, 3 vibrant crimson bags.
clear black bags contain 1 dull orange bag, 5 posh fuchsia bags, 2 mirrored cyan bags, 5 shiny gold bags.
posh tomato bags contain 4 pale plum bags.
vibrant fuchsia bags contain 4 drab olive bags, 1 drab bronze bag, 4 dull blue bags.
mirrored lavender bags contain 2 faded cyan bags, 3 mirrored bronze bags, 2 faded tan bags, 5 clear cyan bags.
mirrored lime bags contain 4 dull gray bags, 4 plaid beige bags.
faded chartreuse bags contain 3 light coral bags.
dim brown bags contain 3 pale yellow bags, 4 striped lime bags.
pale gold bags contain 3 striped plum bags.
mirrored chartreuse bags contain 2 striped salmon bags, 4 shiny teal bags, 4 wavy gold bags, 4 light magenta bags.
plaid white bags contain 5 faded orange bags, 4 dull tomato bags, 1 vibrant olive bag.
clear crimson bags contain 5 posh maroon bags, 5 muted white bags, 2 dim fuchsia bags, 4 pale maroon bags.
wavy tomato bags contain 3 dim tan bags, 4 dim orange bags, 1 mirrored tan bag, 4 posh indigo bags.
dim coral bags contain no other bags.
plaid magenta bags contain 2 clear gold bags, 1 posh orange bag.
wavy gold bags contain 1 plaid blue bag, 3 pale purple bags, 3 pale yellow bags.
wavy blue bags contain 2 light white bags, 5 pale magenta bags, 1 plaid lime bag, 5 faded cyan bags.
pale yellow bags contain 2 vibrant gray bags.
clear chartreuse bags contain 1 light gray bag, 1 dark tomato bag.
drab lavender bags contain 4 vibrant salmon bags.
dark turquoise bags contain 5 pale plum bags.
vibrant tan bags contain 5 plaid aqua bags.
vibrant green bags contain 4 vibrant red bags, 3 vibrant tomato bags.
plaid aqua bags contain 5 wavy silver bags, 5 faded silver bags.
pale brown bags contain 1 dark fuchsia bag, 3 drab green bags, 5 bright cyan bags.
drab olive bags contain 1 dull maroon bag, 4 dark maroon bags.
wavy cyan bags contain 2 muted blue bags, 4 clear beige bags.
dotted white bags contain 5 dull orange bags.
shiny green bags contain 2 vibrant gray bags, 3 vibrant tomato bags, 2 dark violet bags.
bright fuchsia bags contain 2 vibrant chartreuse bags, 2 dim orange bags, 4 clear orange bags, 5 posh fuchsia bags.
clear purple bags contain 2 dim gray bags, 1 shiny tan bag.
dotted lavender bags contain 3 posh gray bags, 4 dull beige bags, 5 plaid beige bags, 1 dull teal bag.
dull salmon bags contain 2 muted magenta bags, 4 muted purple bags, 2 light teal bags, 4 vibrant cyan bags.
dark green bags contain 3 clear lavender bags, 5 shiny silver bags.
dull maroon bags contain 2 dark fuchsia bags, 3 mirrored red bags, 3 mirrored gray bags, 5 dim lime bags.
clear bronze bags contain 2 dull turquoise bags, 4 dim olive bags, 3 wavy violet bags, 5 dotted bronze bags.
muted silver bags contain 1 clear green bag, 4 shiny tan bags.
dark lavender bags contain 5 dim gray bags, 4 dark teal bags.
light violet bags contain 2 light blue bags, 4 clear lavender bags, 4 dim teal bags.
vibrant violet bags contain 4 posh plum bags, 5 dark fuchsia bags.
posh teal bags contain 2 pale indigo bags, 5 plaid fuchsia bags, 3 muted gray bags.
posh crimson bags contain 5 bright gold bags.
dark yellow bags contain 4 muted gold bags, 2 dull aqua bags.
drab gold bags contain 4 dark maroon bags, 1 dim turquoise bag, 4 mirrored cyan bags.
dotted tan bags contain 5 plaid turquoise bags, 2 shiny aqua bags.
vibrant indigo bags contain 1 faded plum bag, 5 dark purple bags, 4 drab maroon bags, 3 dull teal bags.
muted olive bags contain 3 dull bronze bags, 3 drab salmon bags, 1 plaid magenta bag, 3 mirrored purple bags.
plaid purple bags contain 1 plaid turquoise bag, 1 muted olive bag, 1 shiny crimson bag.
dim lime bags contain 5 mirrored gray bags, 3 plaid magenta bags, 5 dark fuchsia bags, 2 bright beige bags.
muted violet bags contain 3 bright aqua bags, 2 pale brown bags, 3 dark fuchsia bags.
dark beige bags contain 3 vibrant fuchsia bags, 2 shiny white bags.
dim gray bags contain 3 mirrored violet bags, 1 plaid orange bag, 4 vibrant olive bags, 5 pale white bags.
pale cyan bags contain 4 dark red bags, 2 mirrored beige bags, 5 shiny purple bags, 3 dark fuchsia bags.
dim indigo bags contain 2 faded blue bags, 5 muted bronze bags.
shiny brown bags contain 4 vibrant salmon bags, 3 drab orange bags.
dull black bags contain 5 shiny cyan bags, 5 dark purple bags.
light white bags contain 2 mirrored beige bags, 2 faded turquoise bags.
dim fuchsia bags contain 4 drab salmon bags, 5 striped gray bags, 2 light gray bags.
striped gray bags contain 5 plaid white bags, 5 mirrored purple bags, 2 vibrant aqua bags, 4 muted indigo bags.
wavy lavender bags contain 3 dotted cyan bags, 3 bright chartreuse bags, 5 plaid black bags, 3 dotted gold bags.
dull blue bags contain 2 vibrant olive bags.
dark violet bags contain 4 muted white bags, 4 dim lime bags.
shiny maroon bags contain 3 dotted violet bags, 5 shiny brown bags, 2 light blue bags.
drab yellow bags contain 2 shiny fuchsia bags.
clear fuchsia bags contain 5 wavy gray bags, 4 wavy white bags, 4 drab gray bags, 4 bright indigo bags.
plaid coral bags contain 3 dull lime bags, 2 vibrant olive bags, 4 shiny gold bags.
dull beige bags contain 4 dull blue bags, 2 pale black bags, 5 dim coral bags.
plaid salmon bags contain 1 bright coral bag, 2 bright tomato bags, 4 dim tan bags, 4 shiny purple bags.
dim bronze bags contain 4 posh teal bags, 1 pale salmon bag.
dull magenta bags contain 3 dull lime bags, 2 light gold bags, 2 striped purple bags.
plaid fuchsia bags contain 5 dim lime bags, 5 bright cyan bags, 2 faded silver bags, 1 posh fuchsia bag.
dark plum bags contain 2 plaid green bags.
striped silver bags contain 5 clear silver bags, 2 dotted orange bags, 5 striped fuchsia bags, 4 posh bronze bags.
bright green bags contain 4 bright white bags, 4 mirrored lavender bags, 2 shiny blue bags.
wavy plum bags contain 4 light turquoise bags, 4 shiny violet bags, 5 dim blue bags.
bright chartreuse bags contain 5 dim gold bags.
dotted crimson bags contain 4 wavy beige bags.
dull bronze bags contain 2 muted white bags, 2 faded orange bags, 1 plaid blue bag.
dull crimson bags contain 5 pale black bags, 5 dim lime bags, 5 clear lavender bags, 4 faded orange bags.
striped red bags contain 3 shiny maroon bags, 5 muted yellow bags, 2 faded lavender bags, 2 dark olive bags.
clear magenta bags contain 4 dark coral bags.
plaid beige bags contain 4 vibrant purple bags, 5 drab salmon bags, 2 clear white bags, 5 dull olive bags.
dotted bronze bags contain 5 dull tomato bags, 5 clear lime bags, 2 clear gold bags, 5 mirrored indigo bags.
pale black bags contain 3 faded bronze bags, 3 dotted violet bags.
faded purple bags contain 1 light fuchsia bag, 4 dull violet bags.
clear plum bags contain 2 vibrant gray bags, 4 striped tan bags.
wavy black bags contain 5 mirrored turquoise bags.
dark coral bags contain 5 faded violet bags.
pale orange bags contain 3 mirrored red bags, 4 clear olive bags.";
    }
}
