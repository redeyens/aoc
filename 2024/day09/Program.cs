using System.Security.Cryptography.X509Certificates;

var line = PuzzleInput().First();
var diskSize = 0;
for (int i = 0; i < line.Length; i++)
{
    diskSize += line[i] - '0';
}
var disk = new int[diskSize];
var diskPos = 0;
var fileId = 0;
for (int i = 0; i < line.Length; i++)
{
    var blockCount = line[i] - '0';
    var fileToWrite = -1;
    if (i % 2 == 0)
    {
        fileToWrite = fileId++;
    }
    for (int j = 0; j < blockCount; j++)
    {
        disk[diskPos++] = fileToWrite;
    }
}

var front = 0;
var back = diskSize - 1;

while (back > front)
{
    while (disk[front] >= 0)
    {
        front++;
    }

    while (disk[back] < 0)
    {
        back--;
    }
    (disk[front], disk[back]) = (disk[back], disk[front]);
    front++;
    back--;
}

Console.WriteLine(disk.Where(x => x >= 0).Select((x, i) => (long)x * i).Sum());

Console.WriteLine("day09 completed.");

static IEnumerable<string> TestInput() => GetLinesFromResource("day09.Input.TestInput.txt");
static IEnumerable<string> PuzzleInput() => GetLinesFromResource("day09.Input.PuzzleInput.txt");

static IEnumerable<string> GetLinesFromResource(string name)
{
    using Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name) ?? throw new InvalidOperationException("Could not open input stream!");
    using TextReader inReader = new StreamReader(inStream);

    string? line;
    while ((line = inReader.ReadLine()) != null)
    {
        yield return line;
    }
}
