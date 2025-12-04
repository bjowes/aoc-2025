using System.Diagnostics;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var banks = File.ReadAllLines(FilePath(file));
    long sum = 0;

    foreach (var bank in banks)
    {
        var bankPos = bank.ToList();
        var maxFirstDigit = bankPos.Take(bank.Length - 1).Max();
        var firstDigitPos = bankPos.Take(bank.Length - 1).ToList().IndexOf(maxFirstDigit);
        var maxLastDigit = bankPos.Skip(firstDigitPos + 1).Max();
        var joules = (maxFirstDigit - '0') * 10 + (maxLastDigit - '0');
        sum += joules;
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt");
SolveOne("in.txt");


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var banks = File.ReadAllLines(FilePath(file));
    long sum = 0;

    foreach (var bank in banks)
    {
        var bankPos = bank.ToList();
        long joulets = 0;
        var lastDigitPos = -1;
        for (int i = 11; i >= 0; i--)
        {
            var searchSpace = bankPos.Take(bank.Length - i).Skip(lastDigitPos + 1);
            var maxDigit = searchSpace.Max();
            lastDigitPos = (searchSpace.ToList().IndexOf(maxDigit) + lastDigitPos + 1);
            joulets += (maxDigit - '0') * (long)Math.Pow(10, i);
        }
        //Console.WriteLine(joulets);
        sum += joulets;
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 3121910778619
SolveTwo("in.txt");
