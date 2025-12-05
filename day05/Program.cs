using System.Diagnostics;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);


void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;
    var ranges = new List<(long a, long b)>();
    var ingredients = new List<long>();
    bool rangeMode = true;
    foreach (var line in lines)
    {
        if (line.Trim().Length == 0)
        {
            rangeMode = false; continue;
        }
        if (rangeMode)
        {
            ranges.Add((long.Parse(line.Split("-")[0]), long.Parse(line.Split("-")[1])));
        } else
        {
            ingredients.Add(long.Parse(line));
        }
    }
    
    long sum = 0;

    foreach (var ingredient in ingredients)
    {
        foreach (var range in ranges)
        {
            if (ingredient >= range.a && ingredient <= range.b)
            {
                sum++;
                break;
            }
        }
    }


    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt"); // Expect 3
SolveOne("in.txt");


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;
    var ranges = new List<(long a, long b)>();
    foreach (var line in lines)
    {
        if (line.Trim().Length == 0)
        {
            break;
        }
        ranges.Add((long.Parse(line.Split("-")[0]), long.Parse(line.Split("-")[1])));
    }

    var accRanges = new List<(long a, long b)>();
    long sum = 0;
    bool merged = false;
    do
    {
        merged = false;
        for (int i = 0; i < ranges.Count && !merged; i++)
        {
            var r1 = ranges[i];
            for (int j = i + 1; j < ranges.Count; j++)
            {
                var r2 = ranges[j];
                if (r1.a >= r2.a && r1.b <= r2.b) // Inside, skip it
                {
                    continue;
                }
                else if (r1.a > r2.b || r1.b < r2.a)
                {
                    continue;
                }

                if (r1.a < r2.a) // Connected, merge
                {
                    ranges[j] = (r1.a, r2.b);
                    merged = true;
                }
                if (r1.b > r2.b) // Connected, merge
                {
                    ranges[j] = (r2.a, r1.b);
                    merged = true;
                }
            }
        }

    } while (merged);

    do
    {
        merged = false;
        for (int i = 0; i < ranges.Count && !merged; i++)
        {
            var r1 = ranges[i];
            for (int j = i + 1; j < ranges.Count && !merged; j++)
            {
                var r2 = ranges[j];
                if (r1.a >= r2.a && r1.b <= r2.b) // Inside, skip it
                {
                    ranges.RemoveAt(i);
                    merged = true;
                    continue;
                }
            }
        }

    } while (merged);

    foreach (var range in ranges)
    {
        //Console.WriteLine($"{range.a}-{range.b}");
        sum += range.b - range.a + 1;
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 14
SolveTwo("in.txt");
