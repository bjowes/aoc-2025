using System.Diagnostics;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

bool OffGrid(int y, int x, bool[][] grid)
{
    return y < 0 || y >= grid.Length || x < 0 || x >= grid[0].Length;
}

int NearbyRolls(int y, int x, bool[][] grid)
{
    int sum = 0;
    if (!OffGrid(y - 1, x - 1, grid) && grid[y - 1][x - 1]) sum++;
    if (!OffGrid(y - 1, x - 0, grid) && grid[y - 1][x - 0]) sum++;
    if (!OffGrid(y - 1, x + 1, grid) && grid[y - 1][x + 1]) sum++;
    if (!OffGrid(y - 0, x - 1, grid) && grid[y - 0][x - 1]) sum++;
    if (!OffGrid(y - 0, x + 1, grid) && grid[y - 0][x + 1]) sum++;
    if (!OffGrid(y + 1, x - 1, grid) && grid[y + 1][x - 1]) sum++;
    if (!OffGrid(y + 1, x - 0, grid) && grid[y + 1][x - 0]) sum++;
    if (!OffGrid(y + 1, x + 1, grid) && grid[y + 1][x + 1]) sum++;
    return sum;
}

void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var grid = File.ReadAllLines(FilePath(file)).Select(r => r.Select(rs => rs == '@').ToArray()).ToArray();
    long sum = 0;

    for (int y = 0; y < grid.Length; y++)
        for (int x = 0; x < grid.Length; x++)
        {
            if (grid[y][x] && NearbyRolls(y, x, grid) < 4) sum++;
        }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt"); // Expect 13
SolveOne("in.txt");


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var grid = File.ReadAllLines(FilePath(file)).Select(r => r.Select(rs => rs == '@').ToArray()).ToArray();
    long sum = 0;

    bool removed = false;
    do
    {
        removed = false;
        for (int y = 0; y < grid.Length; y++)
            for (int x = 0; x < grid.Length; x++)
            {
                if (grid[y][x] && NearbyRolls(y, x, grid) < 4)
                {
                    // Remove roll
                    grid[y][x] = false;
                    sum++;
                    removed = true;
                }
            }

    } while (removed);

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 43
SolveTwo("in.txt");
