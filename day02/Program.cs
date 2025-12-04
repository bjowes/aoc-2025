using System.Diagnostics;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

bool ValidId(long id)
{
    if (id > 9 && id <= 99) return (id / 10 != id % 10);
    if (id > 999 && id <= 9999) return (id / 100 != id % 100);
    if (id > 99999 && id <= 999999) return (id / 1000 != id % 1000);
    if (id > 9999999 && id <= 99999999) return (id / 10000 != id % 10000);
    if (id > 999999999 && id <= 9999999999) return (id / 100000 != id % 100000);
    return true;
}

void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var ranges = File.ReadAllText(FilePath(file)).Split(",").ToList();
    long invalidIdSum = 0;

    foreach (var range in ranges)
    {
        var start = long.Parse(range.Split("-")[0]);
        var end = long.Parse(range.Split("-")[1]);
        for (long i = start; i <= end; i++)
        {
            if (!ValidId(i))
            {
                //Console.WriteLine("Invalid " + i);
                invalidIdSum += i;
            }
        }
    }

    sw.Stop();
    Console.WriteLine("Invalid id sum: " + invalidIdSum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt");
SolveOne("in.txt");


bool ValidIdTwo(long id)
{
    if (id < 10) return true;

    //if (id > 9 && id <= 99) return (id / 10 != id % 10);
    //if (id > 999 && id <= 9999) return (id / 100 != id % 100);
    //if (id > 99999 && id <= 999999) return (id / 1000 != id % 1000);
    //if (id > 9999999 && id <= 99999999) return (id / 10000 != id % 10000);
    //if (id > 999999999 && id <= 9999999999) return (id / 100000 != id % 100000);

    var str = id.ToString();
    if (str.All(c => c == str[0])) return false;

    if (str.Length > 2 && str.Length % 2 == 0)
    {
        bool all = true;
        var match = str.Substring(0, 2);
        for (int i = 2; all && i < str.Length; i+= 2)
        {
            if (match != str.Substring(i,2)) all = false;
        }
        if (all) return false;
    }

    if (str.Length > 3 && str.Length % 3 == 0)
    {
        bool all = true;
        var match = str.Substring(0, 3);
        for (int i = 3; all && i < str.Length; i += 3)
        {
            if (match != str.Substring(i, 3)) all = false;
        }
        if (all) return false;
    }

    if (str.Length > 4 && str.Length % 4 == 0)
    {
        bool all = true;
        var match = str.Substring(0, 4);
        for (int i = 4; all && i < str.Length; i += 4)
        {
            if (match != str.Substring(i, 4)) all = false;
        }
        if (all) return false;
    }

    if (str.Length > 5 && str.Length % 5 == 0)
    {
        bool all = true;
        var match = str.Substring(0, 5);
        for (int i = 5; all && i < str.Length; i += 5)
        {
            if (match != str.Substring(i, 5)) all = false;
        }
        if (all) return false;
    }

    if (str.Length > 6 && str.Length % 6 == 0)
    {
        bool all = true;
        var match = str.Substring(0, 6);
        for (int i = 6; all && i < str.Length; i += 6)
        {
            if (match != str.Substring(i, 6)) all = false;
        }
        if (all) return false;
    }

    
    return true;
}

void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var ranges = File.ReadAllText(FilePath(file)).Split(",").ToList();
    long invalidIdSum = 0;

    foreach (var range in ranges)
    {
        var start = long.Parse(range.Split("-")[0]);
        var end = long.Parse(range.Split("-")[1]);
        for (long i = start; i <= end; i++)
        {
            if (!ValidIdTwo(i))
            {
                //Console.WriteLine("Invalid " + i);
                invalidIdSum += i;
            }

        }
    }

    sw.Stop();
    Console.WriteLine("Invalid id sum: " + invalidIdSum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt");
SolveTwo("in.txt");

long FindNextInvalidId(long start, long end)
{
    var nextInvalid = end + 1;
    var startStr = start.ToString();
    for (int splitLength = 1; splitLength * 2 <= startStr.Length; splitLength++)
    {
        if (startStr.Length % splitLength != 0) continue;
        string part = startStr.Substring(0, splitLength);
        
        string lowCandidate = "";
        while (lowCandidate.Length < startStr.Length) lowCandidate += part;
        long low = long.Parse(lowCandidate);
        
        if (low >= start && low <= end)
        {
            nextInvalid = Math.Min(nextInvalid, low);
        }
        else if (low < start) {
            string highCandidate = "";
            long highPart = long.Parse(part) + 1;

            while (highCandidate.Length < startStr.Length) highCandidate += highPart;
            long high = long.Parse(highCandidate);

            if (high >= start && high <= end)
            {
                nextInvalid = Math.Min(nextInvalid, high);                
            }
        }
    }

    if (startStr.Length < end.ToString().Length)
    {
        var nextStringLength = "1";
        for (int i = 0; i < startStr.Length; i++) nextStringLength += "0";
        nextInvalid = Math.Min(nextInvalid, FindNextInvalidId(long.Parse(nextStringLength), end));
    }
    return nextInvalid;
}

void SolveTwoB(string file)
{
    Console.WriteLine($"{Project()}.2b Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var ranges = File.ReadAllText(FilePath(file)).Split(",").ToList();
    long invalidIdSum = 0;

    foreach (var range in ranges)
    {
        var start = long.Parse(range.Split("-")[0]);
        var end = long.Parse(range.Split("-")[1]);
        for (long i = start; i <= end; i++)
        {
            var nextInvalid = FindNextInvalidId(i, end);
            if (nextInvalid <= end)
            {
                invalidIdSum += nextInvalid;
            }
            i = nextInvalid;
        }
    }

    sw.Stop();
    Console.WriteLine("Invalid id sum: " + invalidIdSum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwoB("ex.txt");
SolveTwoB("in.txt");
