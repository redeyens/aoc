var line = PuzzleInput().First();
var diskSize = 0;
var files = new (long offset, long size)[line.Length / 2 + line.Length % 2];
var fileId = 0;
var freeSpace = new (long offset, long size)[line.Length / 2];
var freeSpaceId = 0;

for (int i = 0; i < line.Length; i++)
{
    var blockSize = line[i] - '0';
    if (i % 2 == 0)
    {
        files[fileId++] = (diskSize, blockSize);
    }
    else
    {
        freeSpace[freeSpaceId++] = (diskSize, blockSize);
    }
    diskSize += blockSize;
}

for (int i = files.Length - 1; i > 0; i--)
{
    for (int j = 0; j < freeSpace.Length; j++)
    {
        if (freeSpace[j].size >= files[i].size && freeSpace[j].offset < files[i].offset)
        {
            files[i].offset = freeSpace[j].offset;
            freeSpace[j].offset += files[i].size;
            freeSpace[j].size -= files[i].size;
            // no need to create free space in place of moved file since we don't plan to move files towards the end of the disk
        }
    }
}

Console.WriteLine(files.Select((file, fileId) => fileId * SumBetween(file.offset, file.offset + file.size - 1)).Sum());

Console.WriteLine("day09 completed.");

static long SumUpTo(long n) => n * (n + 1) / 2;

static long SumBetween(long m, long n) => SumUpTo(n) - SumUpTo(m - 1);

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
