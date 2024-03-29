﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day24
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<HexCoord> blackTiles = new HashSet<HexCoord>();

            string input = finalInput;
            foreach (var line in GetLines(input))
            {
                var tile = HexCoord.Parse(line);

                if(blackTiles.Contains(tile))
                {
                    blackTiles.Remove(tile);
                }
                else
                {
                    blackTiles.Add(tile);
                }
            }

            var excitations = new Dictionary<HexCoord, int>();
            for (int i = 0; i < 100; i++)
            {
                excitations.Clear();

                foreach (var blackTile in blackTiles)
                {
                    foreach (var neighbor in blackTile.Neighbors())
                    {
                        int count = 0;
                        if(!excitations.TryGetValue(neighbor, out count))
                        {
                            excitations[neighbor] = 1;
                        }
                        else
                        {
                            excitations[neighbor] = count + 1;
                        }
                    }
                }

                var deactivations = blackTiles
                    .Select(t => (coord: t, count: TryGetExcitations(excitations, t)))
                    .Where(e => e.count == 0 || e.count > 2)
                    .Select(e => e.coord)
                    .ToList();
                
                blackTiles.ExceptWith(deactivations);

                var activations = excitations
                    .Where(kvp => kvp.Value == 2)
                    .Select(kvp => kvp.Key);
                
                blackTiles.UnionWith(activations);
            }

            Console.WriteLine("Black tiles remaining {0}.", blackTiles.Count);
        }
	
        private static int TryGetExcitations(Dictionary<HexCoord, int> excitations, HexCoord key)
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
	
        private static readonly string testInput = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew";

        private static readonly string finalInput = @"seeseesweenwseseseeneneseseseseseswse
nwseswswswnwnwsesweswswwsewwneswswswnw
wwnwewswwwnwwwwwenwwsewnwww
neswnewsenwneswneneeswswwneenene
wwsenwwwesenewswswnwwwwswewwwnw
nwwwswnweswseswweswneeswwwwwww
seseswswswswswswswwseswneseswnwseesw
swswwseseswswswswswswswswwenwnwwswwsw
swnwnenewnwenwswsewnenwnweeswneneswsw
swwnenwwwnwnwnwseseewnewwwenwew
nwnenenwnwnenenwnwnewnenwnwnwnwsenwsene
swswswswswswswswswswseswswne
wswwnwsewsewnewnwnwwnewnwnenwnwnw
swenwnenwnwnesenwnenenwnwnwnwnwnwnenwwne
nwwnwnwnwwnwnwnwnesewseswewnweswnwnw
swseswswsenesesewseswswseneswsenweswsesw
wnwnwnwnwsenwnwenenwwnenwneswnwnwwsew
nwnenenenewseneneneenwseenwweswnesw
eseeseneweewesesewesesesewenee
swnweseswwseseseswswsesweswswnwnwsweswse
enwneseeswneseeseeeenesesewsewee
seseswesesenwesenwseswneseseseseseswe
esewswseseseseseswseseesesesesenesewse
neseseseewseseseseseewesewseeesee
nwnwnesenwnwswwnwnwnenwswwnenesweeee
wnwnenenwnwnwnenwnwwsenwnwsenwnwswnenw
neswswswswswswwnesweswswswnwseewswwsw
senwwnewnwnwsenwwwwnenwnwswnenwnenwswnw
seswneneweeweenweneenenenwseee
nenwnwwswwnwswenenwseeenwnwswwnwnwsew
nenwnwewswnwsenewwnwnwswwnwnwwnwse
nwsenweswnwwnwnwneenewnwnwwnwnwnwsw
eseneweseeseeeseseeeewesesesee
nwneseeeweneswnwesweeneneeenenese
wswswswwswwswswneswswnwseswwswswwsw
senwnweeswenweenwenwseneeseswseee
wnenwnwnenwnenwnenwnenwnwe
seswwenwneswwsewswnwseenwnwesenew
neeneeseeneenwneeeneneesenwenene
nenweswnwswswseeswswneeswnewenwwwsw
nenenewnenweswneneswsenwnewnee
nwswsweswenwswswswswseswswwswseeswswse
nweeswswnenwnwenenewnwswswnenene
swnwnenesweenwnenenenwnenwnwnwnenewne
sweswswswswnewswswswswnewswswwswswsw
nwnwnwwewsenwnwnwnwwnwnwwnwnwnwwnw
senenwnenenenesenwnwnwnenenenewswwnwsw
sewnwsweswwwwwwewnwwsewwwnesw
eeeseeneenenwswswenwee
nwseseeeenwseseeswseseese
wwneseneneneneneneseneneneneneenenene
eeeweeseeeeeeesweeeseene
eeeneneeneweneseeneneeenenenewe
wnwsewwwwwwwwnewwnww
nwnwswnwnenwnwneswnwsenwnwsenw
newewseswsenwseswsesenwsesesenwseneswse
weesewseeeeeseeseeeeswesenwe
nwnwnwwwsenwnwnwwnewnwsewwnwnwwnwse
senwseseeseeeeeseseseesesesenw
enenenenenenenenenenwneneneneswneswnenwnw
wswswwwnwnewswwsewwseeweneesww
sewwnewnwwwwwwwwsewwwwnwww
enweeneeeeeneesweeneneeneswsenw
swswswswswswswswsweswswswswwswswswnene
wewwnwnwswwnenwswnwneswnwnweenwnwsee
nwnenenenwnenenwnesweneneneseneewswnew
nwwswnwswewseneewswnwnwenenwwww
wswwnenewesenwwswwsewnenewswwwww
wwwwnwwwwwewwnesewwwwwsww
senwwwseesesenwseseseneseseseesenwswsw
sewseseseseswswsenwseseswnwsesesenesesw
neswneneneneeneenenwneneneeneee
eneenwnenewenwsweswnenwesee
swnewnwnwnwneewsewnwenwnwwwewswwse
eeeeeeeeeeeeeeesenwwse
swswswwswswwwnesewwswnewswwwnewsw
nwwwwswwswwwswnewswwee
neseswnwseswswnwseswswseseswswswseenwse
wseewwwwwsewwwwnewwwwnwne
nenwnwswnwnweenwnwnwnwnwnwnwwnwnwnwnw
wewwnwwwnwnwesewnwwnwnwwnwnwwnw
nwnewwwwnwswwnwnwnwewwnwnwwsenwwnw
eewneeeeeeeneeee
wseseseswseenesenwwsesesesesesenesesese
eeeeeweeeeeneenene
newswneeneeneneenenesenesweswnenee
seswewesenesenenesewsenwwswese
nwswnwnwnwnwwnwneenwe
nenwnwnwnenwnwsenwnwnwnwnwwnwnwnwnwnwse
neneneeswswenenenenwnenwwseswswenw
eeenwsenwnwseseesweeeseswewe
nwwsenwnwnwsenwnwnwnwsenwwwnwnewnwnenw
neseeeeenwneeeneeseenweneeee
nwnwwnwnwwewnewnwwnwnwsenwsenwwsww
nenwnwnwenwwseneswwnwnwnesenwenenwnwwnw
swsweneeeenweeseseeeeeeneenwsw
nenwnwnwseswsenwnwnwnwnwnwnenwwnwnwnwnwnw
senwwseenwwsewnenwwnenwnesewnwnwsee
nenenwneneeneneneswsenenenewnenene
nwswneewenwwneseweneneseneseswsesww
nwnewswesenwseswswneswswwswnwswwswsesw
wnwnwnwwsenwwwwwwwnewnewnwnwwnwse
wewswsewswnwwswswwwnwsewwnewsww
nenwnesenwnewnwnenwnwnenenwnenwswnenwnwene
swswwseswswswswswnwnewswswswwswswsww
nwnwswswnenenwnwsenenwnwnwwnenenwnenenwnw
seswswseswswseeswswswswseswseswnwesww
senewwswwewnewswsenewseenewsww
swnwseewnwnwswwnweseeseeewseneeee
enwnesenenenwneswneneswwnesenwnenwnewnwne
wwwwwwenwwwwwwsewwwwww
eweswnwnwseswwweswwneewnesewnenew
swseswswswseseswswneswswswswseswswswnwswe
seswswwsenwswenesw
nenwneenwnwswnenenwnenwnwnenwwnwnwnenwe
eewewneneweneeswneeseew
nwswneswwswsewswswwswewwswswwseswswne
sewsesesesenweseseweeswseseenw
nwswseswneseewwswswswneswenwswnwwsw
swseseswseseeswseswwwswseseseneesesenwse
wswsewwswwsewneeswwnwewwwwww
nwswseeswswseswnwnwnweneswswswswswwwsesw
swweseseseswneswswswseseswsesesenwswswsw
nwnwnwnwnwnewsenwnwnwsenewwwnwnwnwnw
enewseeewnwswnwseswnenwnwneenenesenene
wneswswswnwneesenwneeenesw
wswwwswnewswswwnewswswswsewwww
swnwseswseseenewswnwneneseneswsweswwsw
eeeeeeeweseneweswseenwseseswe
seswseseseseseseswwswswesese
eseeseseesenwseesesesese
swneswnwesesweseseswswsewswswsesenwse
newwnesewsewewwenwwsw
nenesenenenenwneseneneneneenewne
neswnesenwneneneewnenenenenenenenenene
eneeseeweseseseeseee
swswwwswnwnweswwse
neseenwnesenwnesenwsesewsesewsweeseese
enenenenenwnwnwnenesenwnwswnwnwnene
wwwewswewnewnwwwwwwwwsww
nwnwnwwsewnwnwnwwwsenewnenwwwnwnw
sweeseneeeeeweswewenwseesenenw
weswwnwnwwwsewwwwwewwwwnw
eeseneeesesweee
enweseneweeswneseseswswneesenesw
nwnwnenwnwswnwneenwneewnwse
nwnwsenwseenwseseeswewsweeseeenwe
swneswseswenwneswswsenwswswseswswswneenwsw
sewswnwwnwnewnwwswswewnewnewwnw
nenenweswswswswswswsenenwswswseswewswsw
sesenenwnwnenesenesewwnenenwnenenwsew
sesesewseseseeseseseseesesenesesesewse
neneneneneswnenenenenenwnenenenwneswne
wwsweswswweneseseswswsenwewneew
eeeneeneneeenweeeeeseeneswe
nwswswswnewswswwweswswwswnewsewswsw
nwnwwnwnewnwsenwnwnwwenwnwswwnwnwnwnw
sesenweenwseneeeseeseswswsenesesesw
wwswwwnwwwnwwwwwwwnwwnwnwsee
wwseswsweseneswseswswswswswswsweswnenw
wneeeesweneenenewenenese
swswenwnwswnwenwnenenwnwswnenwnenwsenw
wwswwewnwnwswewnwsewseseswnwnenew
nwnwwnwnwwwnwnwew
swnesweswnewwswnwsweswsenwswswswseene
seseswswwsenwnenwnenenwnwnwnwnenwsesenenw
swswwsweswwswwwswswweewswwwnw
eneeneeeeeeeswseeeeeswnwswe
nwwwwnesewwnwwnwnwnwnwse
neswseswswswseseswswsenwewnwswswnwswswe
seswswswswnweswswsweseswsewswswseswsw
swsesenwseseseswsenesew
swnwnwnwnwnwnwnenwnwenwnwwnwwnwsenwnwnw
seneseswswswseseeswseseswnwswsesesesww
wwwnwnwnwnwwnewnwnwwsenwnwnwnw
wsesenwsweneneseswnewswnewseneesww
eeneneneeneseneeeeeswnwneneeene
eneewswnesewneeeeneneneeenenenee
neeeswnwewneewneswswseswwseneewenw
enwswswsenewsewwswwnwwwswsewww
swswnweenwnwwnwnwnwwnwnwenwwnwswnenw
sweeseeenwneeseeeeeseseseesee
neneenwnenwnenwseenenenewnwswnwsww
nwneneneenenwnenwnwsenwnenwnwnwnwwnwswnwne
nwnwnwnwnwnwwnwnwwnwnwenwwnwnwwswse
swsewsesenwnwseswseesw
seneeneneneneneneneswneeneneeweenee
senwseswnwswwnwneswwseswswwswswswsesw
nenwnwnenenesenewnenwsenesesenenenwnene
wwnwwwewwswseswwwswwneewwwsw
sesesweseseswseswswseeseseseswnwsenwsese
nenewneneneneneswnenenwnesenenenenenenene
wnwnwwnwwnwwenwwnwwwwnww
wnwnwenenwnwneswnwsenwwnwwwwnwww
sweneeneewswnwnwsenenenewweswseew
wswenwnwnwnwnwnwnwnwnwnwnenwwnwnwnwnw
eeneeswnwswswseneesweeenweeenenenw
eswsweeeeneeeeeeneenweeee
wswwneenwsewsewswnwseneswewneewsw
nwenenenwswwswsenwnwnwneswwnwnwwnwnw
sesesesesesesesesesesesesenwsese
eseesweseewseseseeneseseseeeesew
seswnenenenenenenwsenewwnesenenenenene
neenwsweeeenwsewnewseeweeeene
sewenewwwwsewneneseneswseseeesese
seswswseswswswseswswseweeswweswnwswse
swwseseeeeeeeenesesewnwneeesese
eseneeeswswwnwwnesewneswwenwsenw
seenwnweeeeseeesweseeeeeeswse
seneseseseseseseswnesenesesewsew
nenenenewswneneneswsenenenewseeneneswne
sesenesesenewwesenwnwseseswswswseesenw
sewseseeswsenwwnweseneeseseeeswenwe
sewneseweseeeeseseseseseeseeesese
nwnenwnwneneswnwnenenesenenenwwnwnenenwne
senenwnwneewnwseeenwswswwnwnw
wwswwnwwsweseswwwnwsweswwswswsw
sewswswswwneseswseneswnwsweseswwseswsee
esesweseseeswsenenwseseeeseseewsesese
wwwwwsewwwwwwswwwnw
enenwswneneswnewneneseneenenenwswenene
sweseenwneneenwwswneneneneneneeene
nwsenweesenwseenewseseseswenwewse
nwswnwnwnwnwnwnwnwnwnwnwnwnwsenwnwe
nwnwnwnwswesenwnwnwnenenwnenenwnenenenw
swswswewswswswswnwswswswswswwweswswse
wneseswseseseswseneeseesenewnwnwnesw
eswnewnenwswenewneneneenenwneneseswne
nwswweseseeesenwseseenesesewneseesw
neswswesweeeesweneneeswenenwnwsewe
nwnenwnweeeseneseseswesenwenesweneenw
esweneeneeeeeneeeeeeweee
neeneswnewnesewewesenwnenesenenwnene
wwnwwwwwwnwnwseesenwnenwwnwwnw
eswwwnwseneewswswseneswwnwwnwenwne
swswswswwnenewswswswswswwswswneswswsw
swnwswswswseswswswseswswsw
seseesesesesenwseseeseseseesesewe
seswneseswsesweswseseseseswsewswswswsw
swswswnwsenwswsesenwnwnwswseswsesweswse
swseswswseweseswswsesw
nenesenenenenenenenewweneneseenenenene
swseswnwwswsewnewsenwneswswswweww
neeeeweeeeeee
seseseswswsenwseseseseseseswse
enenenwnenenenenenewnwsenwnwswswenesw
nwneseseesweseseeeeswenwseswnwsesesw
wwnwwwseneswwswwswewwwwwwwsw
nwnwswseenwnwenwswswnwnenwneenwwneswese
sewnwwnwnwsesewnewwwwwwewwsww
nwswweswswwswsweeswswseswnwwsww
wewwsewwwwwwwsenwwwwwnwnwwne
swswswswwnwswswswswswnwsweseswswswsww
wnwwnwswnwwnwewwneswswwwenwww
newnenenwwneneswnenwnenweneesenwnwnene
wneswsweswneeneswneneweneeenenee
senwesesenwseseswswseseseseseseseswsese
wwwswswenewsenweswwwswwwnewswnw
nwswswswwswwnwneenwenwewewenwswne
seseseswseenwseswenwneseeweeseeee
ewseseseseseeeeeeseeseeseseenw
neswneneenwnenenenenenenenenewnw
swswwwwwwnewswwsenewnenewwwwsw
swweswswswnewwnwwswwswwenw
senenwwnwnwnwnwesenwnenwswnwnwnwswnwnwwnw
nwnewswwwwnenwnewnwsenenwwwnwnwswsw
nesweeesenwsweeeeesenweenwnee
swwwwewnwwswwseswwweneewwnw
nenwnwenwnwnenwwwsenwsewnwnwwnesw
wseseseseneswnenenwsesesesenwswsese
eenenewneneenweneeswnee
wwwseewwnwnwnwwwwnwnwwsenwswnwnw
newnenweswswnwneseswnenwseneseswnwnee
seneseswseseswswesenwswswswswsese
seesewenwnenwnenewsw
nenenenwnwneswnwnwwsenenwnwnenwnw
neeeeeneneneswwnese
weneneeeswseneeweeneeneeeseeee
seswswnesesesweseswwseswswnwswswseseswsw
eeeseesenenweweneweeenwewwsw
enwnwseseseeewnwsweswnenenwneswew
wnweneenwsenesweneeeeneeneenesee
neswsenwnesenwswwswsenwwsewwwsewne
sewwwwnwsewwnewwwwewwwwww
swswseswswswseseswenwsesw
eeseeneeeenweweeeeeseseeee
nwnenwnwwenwwsewwwnwnwnwwwsewwnw
eneswwneneneneneneneneneseeneneenwne
nenenwewneneneeneswswnenenenesenenene
wnwsweswswswnwnwswenwseseseeswswswnwse
sewnwwnwwnwnwsewnwwnwsewwnwnwwnew
sweswwnwswswwewswnewnwnwsewswesesw
seewnewwwnwnewswwwnwwwwsenwww
swswswneswswswswwswswswseenwswswsweswse
wnesenenenesweseewwneenwnwsweene
senwseswswseswseseseswseseseseswswswnwse
wsenweseseseswneneseswseseseneeswese
esenwsewweseesewseeeswnwesesese
wwswseneswwswwwswswww
seeeewseeseeeneseenesweesesenw
nwnwwwnwnwnwnwnwwnwweswwnwnwwwee
neseeswsenwwseseeeeweneeweseee
nwnenenenenenenenenwswnenenw
nwnwwnweswnwnwnweseenwswnwnwnweswnwwne
newneeneneneswwnwnesenenenewsenenesene
wwswsenewsewwnenwwwewweswewnw
swwneeswwsewnwwwwswwwnewwwwsww
nenenenewneseneeenesewwneswenenwse
nwnwneswnwenwnenwneenenwnewnesenwnwnww
wnwsweswnwsweneeenenenwneeseenwesw
swnewsweswswwswwneswswswsewswswwww
nwnwnwwnwenwswnwwswwwnweenwswnwnw
wwsenwwwnwesenwnwnenwnwwwsweswnww
swwnwenwnwnwnenwnwswnwnwenwnwswnwnwwnenw
seneseneseswswwseswswenwswswswseswneswnwe
seseesenwseswseseswsesesesesenw
swneswnwwnwswwwseswsesww
wnewwwnewswswsewswwwwwwwnesw
nwnwnenwsenwnwnwwswenwnwnwnw
weeswnwswwseeswseeswswswnwsesewsw
wnwwnwwwsewswnewnwwwnwwwwwe
seseneseseseseseseseseseseseneseswsesew
eeneseeewseeseeesweeeeeenee
nwneswnwnwnwnwnwnwnwnwnenwnenw
sesenwseenwswseeeeenwswweesesewnwse
swseseseswswswnesesweswsenw
senwnenwnwwnwnwsenenenwnenwnwsenewnwnw
nenwwneewwswnesenwsesenwsenwnenwsenwse
ewneneswnwswsewwsenwsewnenewnwnwwsw
swswswseswswwnwseswneeswseswsw
eweneeeeeenwseeneswnwseeeneswne
seseseseseseseeseenenewsesenwsesesew
wswswwwswswwwwswswwwseswnesw
swwwswwwneswswwswwneswwwswwww
eswseseswnesewnwswwseeswswswswswsesesesw
swneneeneeneeseeweenwnweenenese
wwnewswswswwswwwsewswswseswnenew
eeeswneneneneneeeneneene
neswnwenenwnwnwnenwnwse
seweseeneeseeese
nwswnwweneenwsenwseswswwnwswneneneene
nwnesenwnwnenenwnenwnenwwwnwswnwenwnw
nenenesenenesenenewnenenenenwnenenenwne
esesesewseeseeesesewsesee
neneseswnesenewwnenewnenwenenenene
swswwwewswwwswwwwswswwwswsene
nwnwnwwewnwnwnwnwnwnwnwwnwseewwsw
neswneenweseneenweneweseesenwweee
nwwneneeenewenenenewneneneneseseneese
sweneneeeweneneeeenene
nweneeswnwnwenwswnewseswswnew
neneeeeneeneneswswnwenenwswenwsee
eeswneeeeeeneenweswseeeeee
senewswswseseseseswseswseswswsesese
seewnwseseeeeeeeeeeeeseese
wwnwwnwwwswswwnwseenewwnwweww
swwwwswwswswwswnwswwswswew
nwseeseeenwsweseenesesesewnwesesee
swswswswswnwsweseneswswnwwswswsw
eenwnweeeeseeeneeeeseeeew
swwswswswnwswswwswswswseswswswswswesw
swnwswswwneeweneseswwsenwswswnewe
nweenwseeseseewswneewseseeesee
swneswneneeneenwneene
wwwnwneswwwwwnwwwwnewwswww
nesweeeeewseneenw
nwnwnwsewnwwnwnwnwenwnenwnwwwwwww
newseneeneneneenenenenenenwnewnesw
nwnwenweswnwnwnwnwesewswnewnwwseew
senwswswswswseewseneswseswseseswswswnw
nwsewwwwnewnwwwswenwwnwnwnwnww
seswwwwwswnewwwwswenwwswswsww
esweeenwenwnweeseeesweeeeee
seeseseseeswesesesenwsesese
swswwweneeweswwnewseswswwwnwewse
nweenenwseenenewnesweneswenwnesewsene
swneswswswswswswswswwswswsweswswswswsw
nwnwnwsewnesenwswnenwnwsesenwnwnwsenwnw
neseswsenwwnenwnwseswenenenenwwnewe
wseseeseseswseseseesesesesesesesenenwnw
newsenenwnwswnwwnwnwnwnwnenwwnwnwswwnw
eneswswneewweneneeeswneseneneeseesw
wwneswwwwwwsweswswwswwwseswww
sewnewnenwwwwwwwwwwswsewwww
nwnwnwnwswnenwsenwnenwneenesenewnenwne
sesenwsweeewnenenwnwswnwnwnwnwnwwsw
seseseswseseswneseneseseswenwsesenwseswse
wwnewnwnwsenwwwwnwnwnwnwwsenwwnwenw
swnwnwnenwnenenenwenenenwneneswnwnwnesene
nwwewwsewwneswwsesenewnewwwwnww
neewnenenweneeneeneneneeswnenenenene
swseswswswseeswwswsesesewseswnwseswnenesw
nenwneneneswnenwneenesenenenenesenwnewne
nenwsesesesesesesesesesesesewsesesesesese
sewswwnenwwnwesewwnwwwwwwewe
nenwnwwsenweswwnwnw
seeeeneenweeswneewnwneeneseene
nenenenenwnenenenenenwswnwnenwnenwe
seseeseswsenwseseesesewsesesesenwsenenw
seneeeseswnewnesenewwsewnwnenenwe
seenwnwnwswswnwsenenwnwwnwnweswnenewe
neenenenenenenewenenesewneneneee
nwswswswswwswwswneswswswsweswswswswwsw
neeneneneswneeeeeneneeeneneeswnw
swwswswseswswswswswwswswwwswneswswesw
swwseswwseswwnenwwswwswswnesw
wswswwswswenwnweswsenwnwwwswewsew
wnwnwnwnwnwnwnwnwnwnwnwnwnwnwnwenw
neeewesewswesewneswseeseseseenene
nwnwswenwneenwnwnwnwnwnwwnenwnwnenw
nwseesesenwesenwswswswnwswweeneswnesw
ewwnwsweeeewwweeeeswwnee
swwswneeeeneeeeeeeenwewesee
nwsesenwnwsewnenwnwswsenenwnewnwseenwnw
neneeneneeneswswenenenenwswnenwnenenene
neneswswnwnwnwnwweneneenenenwnwnenwnw
nwnwenwnwnwnwwnwnwnwswnwnwnesenw
wenweswwwwwwwwwwwnwnwwwwwse
sweneswsewwswsesweswswswswswnwsweswse
swswswswswnwseswswseswenwwswnesweswsw
senwseseeeeweenesenesweseeseeseswse";
    }
}
